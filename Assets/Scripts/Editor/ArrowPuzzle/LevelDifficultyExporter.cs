using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 关卡难度指标导出工具
/// 指标：地图x、地图y、占用总格子、总箭头数、平均遮挡率、难度分
/// </summary>
public class LevelDifficultyExporter : EditorWindow
{
    private const string DefaultExtension = ".json";
    private string folderPath = "Assets/Res/InGame/LevelConfigs";
    private string extension = DefaultExtension;
    private string resultMessage = "";
    private MessageType resultType = MessageType.None;

    [MenuItem("Tools/导出关卡难度指标到CSV")]
    private static void ShowWindow()
    {
        var window = GetWindow<LevelDifficultyExporter>("关卡难度导出");
        window.minSize = new Vector2(611, 611);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("关卡难度指标导出", EditorStyles.boldLabel);
        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        folderPath = EditorGUILayout.TextField("关卡文件夹路径", folderPath);
        if (GUILayout.Button("浏览", GUILayout.Width(50)))
        {
            string selected = EditorUtility.OpenFolderPanel("选择关卡文件夹", folderPath, "");
            if (!string.IsNullOrEmpty(selected))
                folderPath = selected;
        }
        EditorGUILayout.EndHorizontal();

        extension = EditorGUILayout.TextField("文件后缀", extension);

        GUILayout.Space(10);
        EditorGUILayout.HelpBox("难度分 = 平均遮挡率 × 箭头数量", MessageType.Info);

        GUILayout.Space(10);
        if (GUILayout.Button("导出CSV", GUILayout.Height(30)))
        {
            ExportLevelDifficultyToCSV();
        }

        GUILayout.Space(10);
        if (resultType != MessageType.None)
        {
            EditorGUILayout.HelpBox(resultMessage, resultType);
        }
    }

    private void ExportLevelDifficultyToCSV()
    {
        resultMessage = "";
        resultType = MessageType.None;

        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
        {
            resultMessage = $"文件夹不存在: {folderPath}";
            resultType = MessageType.Error;
            return;
        }

        string[] levelFiles = Directory.GetFiles(folderPath, "*" + extension, SearchOption.AllDirectories);

        if (levelFiles.Length == 0)
        {
            resultMessage = $"在路径 {folderPath} 中未找到 {extension} 文件";
            resultType = MessageType.Error;
            return;
        }

        StringBuilder csv = new StringBuilder();
        csv.AppendLine("关卡id,地图x,地图y,占用总格子,总箭头数,平均箭头长度,初始可移动箭头数,平均遮挡率,难度分");

        // 按文件名排序，确保输出顺序一致
        System.Array.Sort(levelFiles);

        int processedCount = 0;

        foreach (string filePath in levelFiles)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                LevelData data = JsonConvert.DeserializeObject<LevelData>(jsonContent);

                if (data == null || data.level == null || data.level.Count == 0)
                {
                    Debug.LogWarning($"关卡文件数据为空，已跳过: {filePath}");
                    continue;
                }

                // ====== 1. 箭头总数 ======
                int arrowCount = data.level.Count;

                // ====== 展开每条箭头的所有占用格子 ======
                var allArrowCells = new List<HashSet<int>>(arrowCount);
                var arrowHeads = new List<int>(arrowCount);
                var arrowDirs = new List<Vector2Int>(arrowCount);

                for (int a = 0; a < arrowCount; a++)
                {
                    List<int> turnPoints = data.level[a];

                    // 展开拐点为全部占用格子
                    HashSet<int> cells = ExpandTurnPointsToCells(turnPoints, data.wide);
                    allArrowCells.Add(cells);

                    // 箭头头部
                    arrowHeads.Add(turnPoints[0]);

                    // 箭头移动方向 = (头部坐标 - 第二个拐点坐标).normalized
                    if (turnPoints.Count >= 2)
                    {
                        int x0 = turnPoints[0] % data.wide;
                        int y0 = turnPoints[0] / data.wide;
                        int x1 = turnPoints[1] % data.wide;
                        int y1 = turnPoints[1] / data.wide;
                        Vector2Int dir = NormalizeDir(new Vector2Int(x0 - x1, y0 - y1));
                        arrowDirs.Add(dir);
                    }
                    else
                    {
                        arrowDirs.Add(Vector2Int.zero);
                    }
                }


                // ====== 2. 占用总格子（所有箭头占用格子之和）======
                int totalCellCount = 0;
                foreach (var cells in allArrowCells)
                    totalCellCount += cells.Count;

                // 平均箭头长度
                float avgArrowLength = totalCellCount / (float)arrowCount;

                // ====== 构建格子 -> 箭头索引映射（用于遮挡检测） ======
                var cellToArrowIndex = new Dictionary<int, int>();
                for (int a = 0; a < allArrowCells.Count; a++)
                {
                    foreach (int cell in allArrowCells[a])
                    {
                        cellToArrowIndex[cell] = a;
                    }
                }

                // ====== 5. 平均遮挡率 = 每个箭头的遮挡箭头数之和 / 箭头总数 ======
                // 遮挡定义：从箭头头部沿移动方向射出，射线路径上遇到的其他箭头数量
                int totalBlockingCount = 0;
                int initialMovableCount = 0;
                for (int a = 0; a < arrowCount; a++)
                {
                    int blockingArrows = CountBlockingArrows(
                        a, arrowHeads[a], arrowDirs[a],
                        data.wide, data.high,
                        allArrowCells, cellToArrowIndex);
                    totalBlockingCount += blockingArrows;
                    if (blockingArrows == 0)
                        initialMovableCount++;
                }
                float avgBlockingRate = totalBlockingCount / (float)arrowCount;

                // ====== 4. 难度分 = 平均遮挡率 * 箭头数量 ======
                float difficultyScore = avgBlockingRate * arrowCount;

                // ====== 写入CSV行 ======
                string levelName = Path.GetFileNameWithoutExtension(filePath);
                csv.AppendLine($"{levelName},{data.wide},{data.high},{totalCellCount},{arrowCount},{avgArrowLength:F2},{initialMovableCount},{avgBlockingRate:F2},{difficultyScore:F2}");
                processedCount++;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"处理关卡文件 {filePath} 时出错: {e.Message}\n{e.StackTrace}");
            }
        }

        // 保存CSV文件
        string outputPath = Path.Combine(Application.dataPath, "../LevelDifficulty.csv");
        File.WriteAllText(outputPath, csv.ToString(), Encoding.UTF8);

        string fullOutputPath = Path.GetFullPath(outputPath);
        resultMessage = $"导出成功！共处理 {processedCount} 个关卡\n输出位置: {fullOutputPath}";
        resultType = MessageType.Info;
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 将拐点列表展开为箭头占用的所有格子集合
    /// </summary>
    private static HashSet<int> ExpandTurnPointsToCells(List<int> turnPoints, int wide)
    {
        var cells = new HashSet<int>();
        if (turnPoints == null || turnPoints.Count == 0)
            return cells;

        cells.Add(turnPoints[0]);

        for (int i = 0; i < turnPoints.Count - 1; i++)
        {
            int from = turnPoints[i];
            int to = turnPoints[i + 1];

            int fromX = from % wide, fromY = from / wide;
            int toX = to % wide, toY = to / wide;

            if (fromX == toX)
            {
                // 垂直方向
                int step = toY > fromY ? wide : -wide;
                for (int c = from; c != to; c += step)
                    cells.Add(c);
            }
            else
            {
                // 水平方向
                int step = toX > fromX ? 1 : -1;
                for (int c = from; c != to; c += step)
                    cells.Add(c);
            }

            cells.Add(to);
        }

        return cells;
    }

    /// <summary>
    /// 计算指定箭头在移动方向上被多少条其他箭头遮挡
    /// </summary>
    private static int CountBlockingArrows(
        int arrowIndex, int headCell, Vector2Int moveDir,
        int wide, int high,
        List<HashSet<int>> allArrowCells, Dictionary<int, int> cellToArrowIndex)
    {
        if (moveDir == Vector2Int.zero)
            return 0;

        var blockingArrowSet = new HashSet<int>();

        int x = headCell % wide;
        int y = headCell / wide;

        // 从箭头头部的下一格开始，沿移动方向逐格检测
        x += moveDir.x;
        y += moveDir.y;

        while (x >= 0 && x < wide && y >= 0 && y < high)
        {
            int cell = y * wide + x;

            if (cellToArrowIndex.TryGetValue(cell, out int otherArrowIndex)
                && otherArrowIndex != arrowIndex)
            {
                blockingArrowSet.Add(otherArrowIndex);
            }

            x += moveDir.x;
            y += moveDir.y;
        }

        return blockingArrowSet.Count;
    }

    /// <summary>
    /// 将方向向量归一化为单位方向 (只支持上下左右四个方向)
    /// </summary>
    private static Vector2Int NormalizeDir(Vector2Int v)
    {
        if (v.x != 0)
            return new Vector2Int(v.x > 0 ? 1 : -1, 0);
        if (v.y != 0)
            return new Vector2Int(0, v.y > 0 ? 1 : -1);
        return Vector2Int.zero;
    }
}
