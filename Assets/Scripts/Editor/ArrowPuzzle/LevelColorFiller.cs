using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 关卡颜色填充工具
/// 根据箭头数量规则确定颜色种类数，填充到每个关卡配置文件的 color 字段（数组）
/// </summary>
public class LevelColorFiller
{
    private static readonly string[] ColorList =
    {
        "#eb6912", "#9272f9", "#dc316f", "#49b06b", "#eab10e",
        "#e260ac", "#4e79f1", "#b224ed", "#28b6d0", "#d83d3d"
    };

    /// <summary>
    /// 箭头数量 → 颜色种类 规则表
    /// (箭头个数下限, 箭头个数上限, 颜色种类)
    /// </summary>
    private static readonly (int min, int max, int colorCount)[] ColorCountRules =
    {
        (1,   1,   1),
        (2,   2,   2),
        (3,   3,   3),
        (4,   20,  3),
        (21,  30,  4),
        (31,  50,  5),
        (51,  80,  6),
        (81,  120, 7),
        (121, 160, 8),
        (161, 999, 9),
    };

    /// <summary>
    /// 根据箭头总数查找需要的颜色种类数
    /// </summary>
    private static int GetRequiredColorCount(int arrowCount)
    {
        foreach (var rule in ColorCountRules)
        {
            if (arrowCount >= rule.min && arrowCount <= rule.max)
                return rule.colorCount;
        }
        // 超出规则范围时使用最大颜色种类
        return ColorCountRules.Last().colorCount;
    }



    /// <summary>
    /// 从颜色列表中随机选取 count 个不重复的颜色
    /// </summary>
    private static string[] PickDistinctColors(string[] colorList, int count)
    {
        if (count >= colorList.Length)
            return colorList.ToArray();

        List<string> pool = new List<string>(colorList);
        string[] picked = new string[count];
        for (int i = 0; i < count; i++)
        {
            int idx = Random.Range(0, pool.Count);
            picked[i] = pool[idx];
            pool.RemoveAt(idx);
        }
        return picked;
    }

    /// <summary>
    /// 为关卡生成颜色数组，保证恰好使用 requiredColorCount 种不同颜色
    /// </summary>
    private static JArray GenerateColorArray(int lineCount, int requiredColorCount)
    {
        // 选取所需数量的不同颜色
        string[] selectedColors = PickDistinctColors(ColorList, requiredColorCount);

        JArray colorArray = new JArray();

        if (lineCount <= 0)
            return colorArray;

        // 如果线段数 < 所需颜色种数，则只能用 lineCount 种
        int actualColorCount = Mathf.Min(requiredColorCount, lineCount);
        if (actualColorCount < requiredColorCount)
        {
            Debug.LogWarning($"线段数 {lineCount} 小于所需颜色种类 {requiredColorCount}，仅使用 {actualColorCount} 种颜色");
            selectedColors = selectedColors.Take(actualColorCount).ToArray();
        }

        // 先保证每种颜色至少出现一次
        List<string> result = new List<string>();
        for (int i = 0; i < actualColorCount; i++)
        {
            result.Add(selectedColors[i]);
        }

        // 剩余位置从 selectedColors 中随机填充
        for (int i = actualColorCount; i < lineCount; i++)
        {
            result.Add(selectedColors[Random.Range(0, actualColorCount)]);
        }

        // 打乱顺序，避免前面几个总是不同颜色
        for (int i = result.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string tmp = result[i];
            result[i] = result[j];
            result[j] = tmp;
        }

        foreach (string c in result)
        {
            colorArray.Add(c);
        }

        return colorArray;
    }

    /// <summary>
    /// 为 LevelData 填充 color 字段（供外部调用）
    /// </summary>
    public static void FillLevelDataColor(LevelData levelData)
    {
        int lineCount = levelData.level != null ? levelData.level.Count : 0;
        int requiredColorCount = GetRequiredColorCount(lineCount);
        string[] selectedColors = PickDistinctColors(ColorList, requiredColorCount);

        int actualColorCount = Mathf.Min(requiredColorCount, lineCount);
        if (actualColorCount < requiredColorCount)
            selectedColors = selectedColors.Take(actualColorCount).ToArray();

        List<string> result = new List<string>();
        // 先保证每种颜色至少出现一次
        for (int i = 0; i < actualColorCount; i++)
            result.Add(selectedColors[i]);
        // 剩余位置随机填充
        for (int i = actualColorCount; i < lineCount; i++)
            result.Add(selectedColors[Random.Range(0, actualColorCount)]);
        // 打乱顺序
        for (int i = result.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string tmp = result[i];
            result[i] = result[j];
            result[j] = tmp;
        }

        levelData.color = result;
    }

    [MenuItem("Tools/填充关卡颜色字段(随机)")]
    private static void FillLevelColors()
    {
        string levelConfigsPath = "Assets/Res/InGame/LevelConfigs";
        string[] levelFiles = Directory.GetFiles(levelConfigsPath, "level_*.json", SearchOption.AllDirectories);

        if (levelFiles.Length == 0)
        {
            Debug.LogError($"在路径 {levelConfigsPath} 中未找到关卡文件");
            return;
        }

        if (!EditorUtility.DisplayDialog("确认填充", $"即将为 {levelFiles.Length} 个关卡文件随机填充颜色，是否继续？", "确定", "取消"))
            return;

        int successCount = 0;
        int failCount = 0;
        int totalCount = levelFiles.Length;

        try
        {
            for (int i = 0; i < totalCount; i++)
            {
                string levelFilePath = levelFiles[i];
                string fileName = Path.GetFileName(levelFilePath);
                EditorUtility.DisplayProgressBar("填充关卡颜色", $"正在处理 {fileName} ({i + 1}/{totalCount})", (float)(i + 1) / totalCount);

                try
                {
                    string jsonContent = File.ReadAllText(levelFilePath);
                    JObject jsonObj = JObject.Parse(jsonContent);

                    // 1. 先清空之前的 color 数组
                    if (jsonObj.ContainsKey("color"))
                    {
                        jsonObj.Remove("color");
                    }

                    // 2. 计算箭头（线段）数量
                    JArray levelArray = jsonObj["level"] as JArray;
                    int lineCount = levelArray != null ? levelArray.Count : 0;

                    // 3. 根据箭头数量规则确定需要的颜色种类数
                    int requiredColorCount = GetRequiredColorCount(lineCount);

                    // 4. 生成颜色数组（保证恰好使用 requiredColorCount 种颜色）
                    JArray colorArray = GenerateColorArray(lineCount, requiredColorCount);
                    jsonObj["color"] = colorArray;

                    Debug.Log($"关卡 {fileName}: {lineCount} 个箭头 → 使用 {requiredColorCount} 种颜色");

                    // 写回文件（保持格式化）
                    File.WriteAllText(levelFilePath, jsonObj.ToString(Newtonsoft.Json.Formatting.Indented));
                    successCount++;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"处理关卡文件 {levelFilePath} 时出错: {e.Message}");
                    failCount++;
                }
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        Debug.Log($"关卡颜色填充完成！成功: {successCount}, 失败: {failCount}");
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/清除关卡颜色字段")]
    private static void ClearLevelColors()
    {
        string levelConfigsPath = "Assets/Res/InGame/LevelConfigs";
        string[] levelFiles = Directory.GetFiles(levelConfigsPath, "level_*.json");

        if (levelFiles.Length == 0)
        {
            Debug.LogError($"在路径 {levelConfigsPath} 中未找到关卡文件");
            return;
        }

        if (!EditorUtility.DisplayDialog("确认清除", $"即将清除 {levelFiles.Length} 个关卡文件的颜色字段，是否继续？", "确定", "取消"))
            return;

        int successCount = 0;
        int totalCount = levelFiles.Length;

        try
        {
            for (int i = 0; i < totalCount; i++)
            {
                string levelFilePath = levelFiles[i];
                string fileName = Path.GetFileName(levelFilePath);
                EditorUtility.DisplayProgressBar("清除关卡颜色", $"正在处理 {fileName} ({i + 1}/{totalCount})", (float)(i + 1) / totalCount);

                try
                {
                    string jsonContent = File.ReadAllText(levelFilePath);
                    JObject jsonObj = JObject.Parse(jsonContent);

                    if (jsonObj.ContainsKey("color"))
                    {
                        jsonObj.Remove("color");
                        File.WriteAllText(levelFilePath, jsonObj.ToString(Newtonsoft.Json.Formatting.Indented));
                        successCount++;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"处理关卡文件 {levelFilePath} 时出错: {e.Message}");
                }
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        Debug.Log($"已清除 {successCount} 个关卡的 color 字段");
        AssetDatabase.Refresh();
    }
}
