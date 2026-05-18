using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 关卡可解性检测工具
/// 检测 LevelConfigs 目录下的关卡是否有解（依赖图是否为DAG）
/// </summary>
public class LevelSolvabilityChecker : EditorWindow
{
    private string levelConfigsDir = "Assets/Res/InGame/LevelConfigs";
    private string searchPattern = "*.json";
    private Vector2 scrollPos;
    private string resultLog = "";

    [MenuItem("Tools/关卡可解性检测")]
    private static void ShowWindow()
    {
        var window = GetWindow<LevelSolvabilityChecker>("关卡可解性检测");
        window.minSize = new Vector2(611, 611);
        window.Show();
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.LabelField("检测设置", EditorStyles.boldLabel);
        levelConfigsDir = EditorGUILayout.TextField("关卡目录", levelConfigsDir);
        searchPattern = EditorGUILayout.TextField("文件匹配", searchPattern);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("开始检测", GUILayout.Height(36)))
        {
            CheckAllLevels();
        }

        if (!string.IsNullOrEmpty(resultLog))
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("检测结果", EditorStyles.boldLabel);
            EditorGUILayout.TextArea(resultLog, GUILayout.MinHeight(200));
        }

        EditorGUILayout.EndScrollView();
    }

    private void CheckAllLevels()
    {
        if (!Directory.Exists(levelConfigsDir))
        {
            resultLog = $"目录不存在：{levelConfigsDir}";
            return;
        }

        string[] files = Directory.GetFiles(levelConfigsDir, searchPattern, SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            resultLog = $"在 {levelConfigsDir} 下未找到匹配 {searchPattern} 的文件";
            return;
        }

        int totalCount = files.Length;
        int solvableCount = 0;
        int unsolvableCount = 0;
        int errorCount = 0;
        var unsolvableLog = new StringBuilder();
        var errorLog = new StringBuilder();

        try
        {
            for (int i = 0; i < totalCount; i++)
            {
                string filePath = files[i];
                string fileName = Path.GetFileName(filePath);

                if (EditorUtility.DisplayCancelableProgressBar(
                    "检测关卡可解性",
                    $"正在检测 {fileName} ({i + 1}/{totalCount})",
                    (float)(i + 1) / totalCount))
                {
                    break;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var levelData = JsonConvert.DeserializeObject<LevelData>(json);
                    if (levelData == null || levelData.level == null || levelData.level.Count == 0)
                    {
                        errorCount++;
                        errorLog.AppendLine($"  [解析异常] {fileName}：关卡数据为空或无箭头");
                        continue;
                    }

                    if (CheckSolvable(levelData))
                    {
                        solvableCount++;
                    }
                    else
                    {
                        unsolvableCount++;
                        unsolvableLog.AppendLine($"  [无解] {fileName}");
                    }
                }
                catch (Exception e)
                {
                    errorCount++;
                    errorLog.AppendLine($"  [异常] {fileName}：{e.Message}");
                }
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        var log = new StringBuilder();
        log.AppendLine($"共检测 {totalCount} 个文件：有解 {solvableCount} 个，无解 {unsolvableCount} 个，异常 {errorCount} 个。");
        if (unsolvableCount > 0)
        {
            log.AppendLine("\n无解关卡详情：");
            log.Append(unsolvableLog);
        }
        if (errorCount > 0)
        {
            log.AppendLine("\n异常详情：");
            log.Append(errorLog);
        }
        resultLog = log.ToString();
    }

    /// <summary>
    /// 检测关卡是否有解（依赖图是否为DAG）
    /// </summary>
    private bool CheckSolvable(LevelData levelData)
    {
        // 1. 将拐点列表展开为全量点列表，构建 点→箭头索引 映射
        Dictionary<int, int> point2ArrowIndex = new Dictionary<int, int>();
        for (int i = 0; i < levelData.level.Count; i++)
        {
            var expandedPoints = ExpandWaypoints(levelData.level[i], levelData.wide);
            foreach (var point in expandedPoints)
            {
                point2ArrowIndex[point] = i;
            }
        }

        // 2. 构建依赖图（入度表 + 邻接表）
        int arrowCount = levelData.level.Count;
        Dictionary<int, int> indegreeDict = new Dictionary<int, int>();
        Dictionary<int, List<int>> adjDict = new Dictionary<int, List<int>>();

        for (int i = 0; i < arrowCount; i++)
        {
            if (!indegreeDict.ContainsKey(i))
                indegreeDict[i] = 0;

            // 计算箭头的移动方向（从头部指向移动方向）
            var arrowData = levelData.level[i];
            int headPoint = arrowData[0];
            int secondPoint = arrowData[1];
            int hx = headPoint % levelData.wide;
            int hy = headPoint / levelData.wide;
            int sx = secondPoint % levelData.wide;
            int sy = secondPoint / levelData.wide;

            // 归一化方向（兼容拐点格式，两点可能相距多格）
            int dx = hx - sx;
            int dy = hy - sy;
            int moveX = dx != 0 ? (dx > 0 ? 1 : -1) : 0;
            int moveY = dy != 0 ? (dy > 0 ? 1 : -1) : 0;

            // 从箭头头部沿移动方向向前探测
            int tempX = hx + moveX;
            int tempY = hy + moveY;
            HashSet<int> blockArrowIndexSet = new HashSet<int>();

            while (tempX >= 0 && tempX < levelData.wide && tempY >= 0 && tempY < levelData.high)
            {
                int point = tempY * levelData.wide + tempX;
                if (point2ArrowIndex.TryGetValue(point, out var blockArrowIndex))
                {
                    if (blockArrowIndex != i)
                        blockArrowIndexSet.Add(blockArrowIndex);
                }
                tempX += moveX;
                tempY += moveY;
            }

            // 构建边：blockArrow 消除后才能消除当前箭头
            foreach (var blockArrowIndex in blockArrowIndexSet)
            {
                indegreeDict[i] += 1;

                if (!adjDict.TryGetValue(blockArrowIndex, out var adjList))
                {
                    adjList = new List<int>();
                    adjDict[blockArrowIndex] = adjList;
                }
                adjList.Add(i);

                if (!indegreeDict.ContainsKey(blockArrowIndex))
                    indegreeDict[blockArrowIndex] = 0;
            }
        }

        // 3. 拓扑排序检测是否有环
        Queue<int> queue = new Queue<int>();
        foreach (var kvp in indegreeDict)
        {
            if (kvp.Value == 0)
                queue.Enqueue(kvp.Key);
        }

        int sortedCount = 0;
        while (queue.Count > 0)
        {
            int arrowIndex = queue.Dequeue();
            sortedCount++;

            if (adjDict.TryGetValue(arrowIndex, out var adjList))
            {
                foreach (var adj in adjList)
                {
                    if (!indegreeDict.ContainsKey(adj))
                        continue;
                    indegreeDict[adj]--;
                    if (indegreeDict[adj] == 0)
                        queue.Enqueue(adj);
                }
            }
        }

        return sortedCount == arrowCount;
    }

    /// <summary>
    /// 将拐点列表展开为箭头经过的所有格子索引
    /// </summary>
    private static List<int> ExpandWaypoints(List<int> waypoints, int width)
    {
        if (waypoints == null || waypoints.Count == 0)
            return new List<int>();

        if (waypoints.Count == 1)
            return new List<int>(waypoints);

        var allPoints = new List<int> { waypoints[0] };

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            int from = waypoints[i];
            int to = waypoints[i + 1];
            int fx = from % width, fy = from / width;
            int tx = to % width, ty = to / width;
            int dx = tx - fx, dy = ty - fy;
            int stepX = dx != 0 ? (dx > 0 ? 1 : -1) : 0;
            int stepY = dy != 0 ? (dy > 0 ? 1 : -1) : 0;

            int cx = fx + stepX, cy = fy + stepY;
            while (cx != tx || cy != ty)
            {
                allPoints.Add(cy * width + cx);
                cx += stepX;
                cy += stepY;
            }
            allPoints.Add(to);
        }

        return allPoints;
    }
}
