using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DG.DemiEditor;
using Framework;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : EditorWindow
{
    private enum TabType
    {
        RandomGenerate,  // 随机生成
        PatternFill,     // 图形填充
    }

    private TabType currentTab = TabType.RandomGenerate;
    private static readonly string[] tabNames = { "随机生成", "图形填充" };

    // ============ 随机生成 配置参数 ============
    private int minWidth = 10;
    private int maxWidth = 10;
    private int minHeight = 10;
    private int maxHeight = 10;
    private int minArrowCount = 500;
    private int maxArrowCount = 500;
    private int minArrowLength = 2;
    private int maxArrowLength = 10;
    private int minTurnProb = 10;
    private int maxTurnProb = 10;

    // ============ 图形填充 配置参数 ============
    private string patternFolder = "Assets/Res/Editor/LevelPatterns";
    private string[] patternImagePaths = System.Array.Empty<string>();
    private string _lastPatternFolder = null;

    // 不可填充的颜色列表（hex，不含#）
    private List<string> excludeColorHexList = new List<string> { "FF00FF" };

    private int batchCount = 1;
    private string outputDir = "Assets/Res/InGame/LevelConfigs/LevelConfigs_Generated";
    private int startId;

    private Vector2 scrollPos;

    private string resultLog = "";

    [MenuItem("Tools/关卡生成器")]
    private static void ShowWindow()
    {
        var window = GetWindow<LevelGenerator>("关卡生成器");
        window.minSize = new Vector2(611, 611);
        window.Show();
    }

    private void OnGUI()
    {
        // 顶部页签
        var prevTab = currentTab;
        currentTab = (TabType)GUILayout.Toolbar((int)currentTab, tabNames, GUILayout.Height(30));
        if (prevTab != currentTab)
            resultLog = "";
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        DrawTab();

        EditorGUILayout.EndScrollView();
    }

    private void DrawTab()
    {
        EditorGUILayout.LabelField("关卡参数", EditorStyles.boldLabel);
        if (currentTab == TabType.RandomGenerate)
        {
            minWidth = EditorGUILayout.IntSlider("最小地图宽度 ", minWidth, 2, 100);
            maxWidth = EditorGUILayout.IntSlider("最大地图宽度", maxWidth, 2, 100);
            minHeight = EditorGUILayout.IntSlider("最小地图高度", minHeight, 2, 100);
            maxHeight = EditorGUILayout.IntSlider("最大地图高度", maxHeight, 2, 100);
        }
        minArrowCount = EditorGUILayout.IntSlider("箭头最小数量", minArrowCount, 1, 500);
        maxArrowCount = EditorGUILayout.IntSlider("箭头最大数量", maxArrowCount, 1, 500);
        minArrowLength = EditorGUILayout.IntSlider("箭头最小长度(占点的数量))", minArrowLength, 2, 200);
        maxArrowLength = EditorGUILayout.IntSlider("箭头最大长度(占点的数量)", maxArrowLength, 2, 200);
        minTurnProb = EditorGUILayout.IntSlider("最小拐弯概率", minTurnProb, 0, 100);
        maxTurnProb = EditorGUILayout.IntSlider("最大拐弯概率", maxTurnProb, 0, 100);
        if (currentTab == TabType.PatternFill)
        {
            EditorGUILayout.Space(25);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("图片文件夹");
            patternFolder = EditorGUILayout.TextField(patternFolder);
            if (patternFolder != _lastPatternFolder)
            {
                _lastPatternFolder = patternFolder;
                RefreshPatternImages();
            }
            EditorGUILayout.LabelField($"图片数量: {patternImagePaths.Length}", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(50);
        EditorGUILayout.LabelField("输出设置", EditorStyles.boldLabel);
        batchCount = EditorGUILayout.IntSlider(currentTab == TabType.RandomGenerate
            ? "批量生成数量"
            : "每个图形生成数量",
             batchCount, 1, 100000);
        if (currentTab == TabType.RandomGenerate)
        {
            startId = EditorGUILayout.IntField("关卡起始id", startId);
        }
        else if (currentTab == TabType.PatternFill)
        {
            EditorGUILayout.LabelField("生成的文件名与图片名一致", EditorStyles.boldLabel);
        }
        outputDir = EditorGUILayout.TextField("输出目录", outputDir);

        if (currentTab == TabType.RandomGenerate)
        {
            int totalPoints = (minWidth + maxWidth) / 2 * (minHeight + maxHeight) / 2;
            int estimatedPoints = (minArrowCount + maxArrowCount) / 2 * (minArrowLength + maxArrowLength) / 2;
            if (estimatedPoints > totalPoints * 0.8f)
                EditorGUILayout.HelpBox(
                    $"预估箭头占用格子数({estimatedPoints})接近或超过网格总格子数({totalPoints})，可能无法放置所有箭头。",
                    MessageType.Warning);
            EditorGUILayout.Space(50);
        }

        if (currentTab == TabType.RandomGenerate)
        {
            if (GUILayout.Button("生成关卡", GUILayout.Height(36)))
            {
                GenerateLevels(batchCount);
            }
        }
        else if (currentTab == TabType.PatternFill)
        {
            if (patternImagePaths.Length > 0)
            {
                EditorGUILayout.Space(20);
                if (GUILayout.Button("生成关卡", GUILayout.Height(36)))
                {
                    GenerateLevels(patternImagePaths.Length * batchCount);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("请输入包含图片的文件夹路径。", MessageType.Info);
            }
        }

        // 结果日志
        if (!string.IsNullOrEmpty(resultLog))
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("生成结果", EditorStyles.boldLabel);
            EditorGUILayout.TextArea(resultLog, GUILayout.MinHeight(60));
        }
    }

    private void GenerateLevels(int count)
    {
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        int successCount = 0;
        int failCount = 0;
        var log = new StringBuilder();
        var successLog = new StringBuilder();
        var errorLog = new StringBuilder();
        log.AppendLine("生成结果：");

        for (int i = 0; i < count; i++)
        {
            if (EditorUtility.DisplayCancelableProgressBar("生成关卡中...", $"正在生成第 {i + 1} 个关卡", (float)(i + 1) / count))
            {
                EditorUtility.ClearProgressBar();
                break;
            }
            int imageIndex = i;
            string levelId;
            if (currentTab == TabType.RandomGenerate)
            {
                levelId = (startId + i).ToString();
            }
            else
            {
                imageIndex = i / batchCount;
                int batchIndex = i % batchCount;
                string baseName = Path.GetFileNameWithoutExtension(patternImagePaths[imageIndex]);
                levelId = batchCount > 1 ? $"{baseName}_{batchIndex}" : baseName;
            }
            var ret = GenerateSingleLevel(imageIndex, levelId, currentTab);
            if (ret.Item1)
            {
                successCount++;
                if (!string.IsNullOrEmpty(ret.Item2))
                {
                    successLog.AppendLine(ret.Item2);
                }
            }
            else
            {
                failCount++;
                errorLog.AppendLine(ret.Item2);
            }
        }

        log.AppendLine($"目标生成{count}个关卡，成功生成{successCount}个，失败{failCount}个。");
        if (successCount > 0)
        {
            if (!string.IsNullOrEmpty(successLog.ToString()))
            {
                log.AppendLine("成功的详情：");
                log.Append(successLog);
            }
        }
        if (failCount > 0)
        {
            log.AppendLine("失败的详情：");
            log.Append(errorLog);
        }
        resultLog = log.ToString();

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }

    // return value 1：成功or失败
    // return value 2：成功或错误信息
    private Tuple<bool, string> GenerateSingleLevel(int index, string levelId, TabType tabType)
    {
        int width = 0;
        int height = 0;

        Texture2D tex = null;
        if (tabType == TabType.PatternFill)
        {
            // 加载图片，确保可读且 NPOT 为 None
            string imgPath = patternImagePaths[index];
            string assetPath = imgPath;
            if (!assetPath.StartsWith("Assets"))
            {
                // 转为相对于项目的 Assets 路径
                string dataPath = Application.dataPath.Replace("\\", "/");
                assetPath = assetPath.Replace("\\", "/");
                if (assetPath.StartsWith(dataPath))
                    assetPath = "Assets" + assetPath.Substring(dataPath.Length);
            }

            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
                return Tuple.Create(false, $"levelId {levelId}，无法加载图片导入器: {assetPath}");

            bool needReimport = false;
            if (!importer.isReadable)
            {
                importer.isReadable = true;
                needReimport = true;
            }
            if (importer.npotScale != TextureImporterNPOTScale.None)
            {
                importer.npotScale = TextureImporterNPOTScale.None;
                needReimport = true;
            }
            if (needReimport)
            {
                importer.SaveAndReimport();
            }

            tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (tex == null)
                return Tuple.Create(false, $"levelId {levelId}，无法加载图片: {assetPath}");

            width = tex.width;
            height = tex.height;
        }
        else if (tabType == TabType.RandomGenerate)
        {
            width = UnityEngine.Random.Range(minWidth, maxWidth + 1);
            height = UnityEngine.Random.Range(minHeight, maxHeight + 1);
        }

        LevelData levelData = new LevelData();
        levelData.id = 0;
        levelData.wide = width;
        levelData.high = height;
        levelData.hp = 3;
        levelData.level = new List<List<int>>();
        levelData.color = new List<string>();

        HashSet<int> availablePointSet = new HashSet<int>();
        for (int i = 0; i < width * height; i++)
        {
            if (tabType == TabType.PatternFill)
            {
                int px = i % width;
                int py = i / width;
                Color32 pixel = tex.GetPixel(px, py);
                string hex = $"{pixel.r:X2}{pixel.g:X2}{pixel.b:X2}";
                if (!excludeColorHexList.Contains(hex))
                {
                    availablePointSet.Add(i);
                }
            }
            else if (tabType == TabType.RandomGenerate)
            {
                availablePointSet.Add(i);
            }
        }

        // 每生成一个箭头，就检查是否仍为DAG
        bool breakFlag = false;
        int arrowCount = UnityEngine.Random.Range(minArrowCount, maxArrowCount + 1);
        for (int i = 0; i < arrowCount; i++)
        {
            if (availablePointSet.Count <= 1)
            {
                breakFlag = true;
                break;
            }

            bool placed = false;
            // 对每条箭头尝试多次放置
            int attemptCount = Mathf.Clamp(availablePointSet.Count, 10, 100);
            for (int attempt = 0; attempt < attemptCount; attempt++)
            {
                var ret = GenerateSingleLine(availablePointSet, width, height);
                if (!ret.Item1)
                    break;

                // 临时加入，检查是否无环
                levelData.level.Add(ret.Item2);
                var check = CheckLevelInvalid(levelData);
                if (check.Item1) // 无环，放置成功
                {
                    availablePointSet.ExceptWith(ret.Item2);
                    placed = true;
                    break;
                }
                else // 有环，尝试翻转这条箭头的方向
                {
                    levelData.level[levelData.level.Count - 1].Reverse();
                    check = CheckLevelInvalid(levelData);
                    if (check.Item1)
                    {
                        availablePointSet.ExceptWith(ret.Item2);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    // 正反都有环，撤回这条箭头，重新生成
                    levelData.level.RemoveAt(levelData.level.Count - 1);
                }
            }

            if (!placed)
            {
                breakFlag = true;
                break;
            }
        }

        if (levelData.level.Count < 1)
        {
            return Tuple.Create(false, $"levelId {levelId}，无法生成足够的箭头（仅生成{levelData.level.Count}条）");
        }

        // 保存前压缩为拐点列表
        for (int i = 0; i < levelData.level.Count; i++)
        {
            levelData.level[i] = CompressToWaypoints(levelData.level[i]);
        }

        // 填充随机颜色
        LevelColorFiller.FillLevelDataColor(levelData);

        // 生成成功，写入文件
        string json = JsonConvert.SerializeObject(levelData, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        });
        string filePath = Path.Combine(outputDir, $"AutoGenLevel_{levelId}.json");
        File.WriteAllText(filePath, json);
        return Tuple.Create(true, breakFlag
            ? $"levelId {levelId}，生成的箭头数量({levelData.level.Count})可能小于目标数量({arrowCount})"
            : "");
    }

    // return value 1：成功or失败
    // return value 2：生成的点列表（成功时）
    // return value 3：成功或错误信息
    private Tuple<bool, List<int>, string> GenerateSingleLine(HashSet<int> availablePointSet, int width, int height)
    {
        List<int> linePointList = new List<int>();
        HashSet<int> linePointSet = new HashSet<int>();
        int length = UnityEngine.Random.Range(minArrowLength, maxArrowLength + 1);

        // 1.选取第一个点和第二个点，确定这是一条合法的线段
        var availableArray = new List<int>(availablePointSet);
        availableArray.Shuffle();
        bool findValidSecondPoint = false;
        int headPoint = -1;
        int secondPoint = -1;
        Direction curDir = Direction.None;
        foreach (var point in availableArray)
        {
            headPoint = point;
            var dirList = ShuffledDirections();
            foreach (var dir in dirList)
            {
                curDir = dir;
                secondPoint = GetNextPoint(width, height, headPoint, dir);
                if (secondPoint != -1
                    && availablePointSet.Contains(secondPoint))
                {
                    findValidSecondPoint = true;
                    break;
                }
            }
            if (findValidSecondPoint)
            {
                break;
            }
        }
        if (!findValidSecondPoint)
        {
            return Tuple.Create(false, (List<int>)null, "没有合法的第二个点可以选择了");
        }
        linePointList.Add(headPoint);
        linePointSet.Add(headPoint);
        linePointList.Add(secondPoint);
        linePointSet.Add(secondPoint);
        // 2.选取剩余身体的点
        int curPoint = secondPoint;
        for (int i = 0; i < length - 2; i++)
        {
            int nextPoint = -1;
            int turnProb = UnityEngine.Random.Range(minTurnProb, maxTurnProb + 1);
            int randomProb = UnityEngine.Random.Range(0, 101);
            if (randomProb > turnProb) //优先正方向填充
            {
                nextPoint = GetNextPoint(width, height, curPoint, curDir);

                if (nextPoint == -1
                    || !availablePointSet.Contains(nextPoint)
                    || linePointSet.Contains(nextPoint))
                {
                    // 如果正方向填充不了，就尝试转向填充
                    var dirList = ShuffledDirections();
                    foreach (var dir in dirList)
                    {
                        if (dir == curDir)
                            continue;
                        nextPoint = GetNextPoint(width, height, curPoint, dir);
                        if (nextPoint != -1
                            && availablePointSet.Contains(nextPoint)
                            && !linePointSet.Contains(nextPoint))
                        {
                            curDir = dir;
                            break;
                        }
                    }
                }
            }
            else //优先转向填充
            {
                var dirList = ShuffledDirections();
                foreach (var dir in dirList)
                {
                    if (dir == curDir)
                        continue;
                    nextPoint = GetNextPoint(width, height, curPoint, dir);
                    if (nextPoint != -1
                        && availablePointSet.Contains(nextPoint)
                        && !linePointSet.Contains(nextPoint))
                    {
                        curDir = dir;
                        break;
                    }
                }
                // 如果转向填充不了，就尝试正方向填充
                if (nextPoint == -1
                    || !availablePointSet.Contains(nextPoint)
                    || linePointSet.Contains(nextPoint))
                {
                    nextPoint = GetNextPoint(width, height, curPoint, curDir);
                }
            }

            if (nextPoint == -1
                || !availablePointSet.Contains(nextPoint)
                || linePointSet.Contains(nextPoint))
            {
                return Tuple.Create(true, linePointList, "");
            }
            else
            {
                linePointList.Add(nextPoint);
                linePointSet.Add(nextPoint);
                curPoint = nextPoint;
            }
        }
        return Tuple.Create(true, linePointList, "");
    }

    /// <summary>
    /// 将全量点列表压缩为拐点列表（方向发生变化的点）
    /// </summary>
    private List<int> CompressToWaypoints(List<int> allPoints)
    {
        if (allPoints.Count <= 2)
            return new List<int>(allPoints);

        var waypoints = new List<int> { allPoints[0] };
        for (int i = 1; i < allPoints.Count - 1; i++)
        {
            int prevDelta = allPoints[i] - allPoints[i - 1];
            int nextDelta = allPoints[i + 1] - allPoints[i];
            if (prevDelta != nextDelta)
                waypoints.Add(allPoints[i]);
        }
        waypoints.Add(allPoints[allPoints.Count - 1]);
        return waypoints;
    }

    private int GetNextPoint(int width, int hight, int currentPoint, Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                currentPoint += width;
                break;
            case Direction.Down:
                currentPoint -= width;
                break;
            case Direction.Left:
                if (currentPoint % width <= 0)
                    return -1;
                currentPoint -= 1;
                break;
            case Direction.Right:
                if (currentPoint % width >= width - 1)
                    return -1;
                currentPoint += 1;
                break;
        }

        if (currentPoint < 0
            || currentPoint >= width * hight)
            return -1; // 越界

        return currentPoint;
    }

    // 可复用的方向缓冲区，每次调用 ShuffleDirectionBuffer 会就地随机洗牌
    private readonly Direction[] _dirBuffer = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

    private Direction[] ShuffledDirections()
    {
        for (int i = _dirBuffer.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (_dirBuffer[i], _dirBuffer[j]) = (_dirBuffer[j], _dirBuffer[i]);
        }
        return _dirBuffer;
    }

    enum Direction
    {
        None = -1,

        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    /// <summary>
    /// 检查关卡是否合法（拓扑排序）
    /// </summary>
    /// 返回值1：是否合法
    /// 返回值2：如果不合法，返回存在环的箭头下标列表
    private Tuple<bool, List<int>> CheckLevelInvalid(LevelData levelData)
    {
        var graph = BuildGraph(levelData);
        var indegreeDict = graph.Item1;
        var adjDict = graph.Item2;

        // 拓扑排序
        List<int> sortList = new List<int>();
        Queue<int> queue = new Queue<int>();
        foreach (var kvp in indegreeDict)
        {
            if (kvp.Value == 0)
            {
                queue.Enqueue(kvp.Key);
            }
        }
        while (queue.Count > 0)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                // 将入度为0的加入结果列表
                int arrowIndex = queue.Dequeue();
                sortList.Add(arrowIndex);

                // 将依赖当前节点的入度-1，如果入度变为0，加入队列
                var adjList = adjDict.TryGetValue(arrowIndex, out var list) ? list : null;
                if (adjList != null)
                {
                    foreach (var adjArrowIndex in adjList)
                    {
                        if (!indegreeDict.ContainsKey(adjArrowIndex))
                            continue;
                        indegreeDict[adjArrowIndex] -= 1;
                        if (indegreeDict[adjArrowIndex] == 0)
                        {
                            queue.Enqueue(adjArrowIndex);
                        }
                    }
                }
            }
        }

        bool ret = sortList.Count == levelData.level.Count;
        if (ret)
        {
            return Tuple.Create(true, (List<int>)null);
        }
        else
        {
            List<int> cycleArrowIndexList = new List<int>();
            foreach (var kvp in indegreeDict)
            {
                if (kvp.Value > 0)
                {
                    cycleArrowIndexList.Add(kvp.Key);
                }
            }
            return Tuple.Create(false, cycleArrowIndexList);
        }
    }

    /// <summary>
    /// 构建关卡的依赖图
    /// </summary>
    private Tuple<Dictionary<int, int>, Dictionary<int, List<int>>> BuildGraph(LevelData levelData)
    {
        // 计算每个点所属的箭头下标
        Dictionary<int, int> point2ArrowIndexDict = new Dictionary<int, int>();
        for (int i = 0; i < levelData.level.Count; i++)
        {
            foreach (var point in levelData.level[i])
            {
                point2ArrowIndexDict[point] = i;
            }
        }
        // 构建入度表和邻接表
        Dictionary<int, int> arrowIndex2IndegreeDict = new Dictionary<int, int>();
        Dictionary<int, List<int>> blockArrowIndex2ArrowIndexDict = new Dictionary<int, List<int>>();
        for (int i = 0; i < levelData.level.Count; i++)
        {
            int tempI = i;
            var arrowData = levelData.level[i];
            int headPoint = arrowData[0];
            int secondPoint = arrowData[1];
            int hx = headPoint % levelData.wide;
            int hy = headPoint / levelData.wide;
            int sx = secondPoint % levelData.wide;
            int sy = secondPoint / levelData.wide;
            int moveX = hx - sx;
            int moveY = hy - sy;

            int tempX = hx + moveX;
            int tempY = hy + moveY;

            HashSet<int> blockArrowIndexSet = new HashSet<int>();
            while (tempX >= 0 && tempX < levelData.wide && tempY >= 0 && tempY < levelData.high)
            {
                int point = tempY * levelData.wide + tempX;
                if (point2ArrowIndexDict.TryGetValue(point, out var _arrowIndex))
                {
                    blockArrowIndexSet.Add(_arrowIndex);
                }

                tempX += moveX;
                tempY += moveY;
            }

            if (blockArrowIndexSet.Count <= 0)
            {
                if (!arrowIndex2IndegreeDict.ContainsKey(tempI))
                {
                    arrowIndex2IndegreeDict[tempI] = 0;
                }
            }
            else
            {
                foreach (var blockArrowIndex in blockArrowIndexSet)
                {
                    if (!arrowIndex2IndegreeDict.ContainsKey(tempI))
                    {
                        arrowIndex2IndegreeDict[tempI] = 0;
                    }
                    arrowIndex2IndegreeDict[tempI] += 1;

                    if (!blockArrowIndex2ArrowIndexDict.TryGetValue(blockArrowIndex, out var _arrowIndexList))
                    {
                        _arrowIndexList = new List<int>();
                        blockArrowIndex2ArrowIndexDict[blockArrowIndex] = _arrowIndexList;
                    }
                    _arrowIndexList.Add(tempI);
                }
            }
        }

        return Tuple.Create(arrowIndex2IndegreeDict, blockArrowIndex2ArrowIndexDict);
    }

    private void RefreshPatternImages()
    {
        if (string.IsNullOrEmpty(patternFolder))
        {
            patternImagePaths = System.Array.Empty<string>();
            return;
        }

        // 支持相对路径（相对于项目根目录）和绝对路径
        string fullPath = Path.IsPathRooted(patternFolder)
            ? patternFolder
            : Path.GetFullPath(Path.Combine(Application.dataPath, "..", patternFolder));

        if (!Directory.Exists(fullPath))
        {
            patternImagePaths = System.Array.Empty<string>();
            return;
        }

        var extensions = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
            { ".png", ".jpg", ".jpeg", ".bmp", ".tga", ".psd" };
        var files = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories);
        var result = new List<string>();
        foreach (var f in files)
        {
            if (extensions.Contains(Path.GetExtension(f)))
                result.Add(f);
        }
        patternImagePaths = result.ToArray();
    }

}
