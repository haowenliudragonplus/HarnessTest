using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 箭头数量导出工具
/// </summary>
public class ArrowCountExporter
{
    //[MenuItem("Tools/导出关卡箭头数量到CSV")]
    private static void ExportArrowCountsToCSV()
    {
        // 获取所有关卡JSON文件
        string levelConfigsPath = "Assets/Res/InGame/LevelConfigs";
        string[] levelFiles = Directory.GetFiles(levelConfigsPath, "level_*.json");

        if (levelFiles.Length == 0)
        {
            Debug.LogError($"在路径 {levelConfigsPath} 中未找到关卡文件");
            return;
        }

        // 准备CSV内容
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("id,箭头数量,showImage,type");

        // 统计每个关卡的箭头数量
        foreach (string levelFilePath in levelFiles)
        {
            try
            {
                // 解析关卡ID
                string fileName = Path.GetFileNameWithoutExtension(levelFilePath);
                string levelIdStr = fileName.Replace("level_", "");
                if (!int.TryParse(levelIdStr, out int levelId))
                {
                    Debug.LogWarning($"无法解析关卡ID: {fileName}");
                    continue;
                }

                // 读取关卡JSON文件
                string jsonContent = File.ReadAllText(levelFilePath);
                LevelData levelData = JsonConvert.DeserializeObject<LevelData>(jsonContent);

                if (levelData == null)
                {
                    Debug.LogWarning($"无法解析关卡 {levelId} 的数据");
                    continue;
                }

                // 获取箭头数量
                int arrowCount = levelData.level != null ? levelData.level.Count : 0;

                // 获取showImage状态
                bool showImageBool = levelData.showImage != 0;

                // 获取type值
                int typeValue = levelData.type;

                // 添加到CSV
                csvContent.AppendLine($"{levelId},{arrowCount},{showImageBool},{typeValue}");

                Debug.Log($"关卡 {levelId}: {arrowCount} 个箭头, showImage={showImageBool}, type={typeValue}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"处理关卡文件 {levelFilePath} 时出错: {e.Message}");
            }
        }

        // 保存CSV文件
        string outputPath = Path.Combine(Application.dataPath, "../LevelArrowCounts.csv");
        File.WriteAllText(outputPath, csvContent.ToString(), Encoding.UTF8);

        Debug.Log($"箭头数量统计已导出到: {outputPath}");
        AssetDatabase.Refresh();
    }
}