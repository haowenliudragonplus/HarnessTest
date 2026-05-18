using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DragonPlus.Config.Common;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResourceEditorTool
{
    public const string RESOURCENAME_TEMPLATE_PATH = "Assets/Scripts/Editor/Resource/Template/ResourceNameTemplate.txt"; //ResourceName模板路径
    public const string RESOURCENAME_GENCODE_PATH = "Assets/Scripts/Hotfix/Common/AutoGen/ResourceName"; //自动生成ResourceName的文件夹
    private const string RESOURCENAME_DEFINE_TEMPLATE = "\tpublic const int #RESOURCENAME# = #RESOURCEID#;\n"; //资源名称定义模板
    /// <summary>
    /// 生成ResourceName
    /// </summary>
    [MenuItem(EditorConst.GenResourceName, false, EditorConst.Priority_GenResourceName)]
    private static void GenResourceName()
    {
        if (!IOUtils.FileExist(RESOURCENAME_TEMPLATE_PATH))
        {
            EditorUtils.ShowDialogWindow("生成失败", $"{RESOURCENAME_TEMPLATE_PATH}中不存在ResourceName模板", "确定");
            return;
        }
        StringBuilder nameDefineStr = new StringBuilder();
        StringBuilder errorStr = new StringBuilder();
        List<string> resourceKeyCache = new List<string>();

        string resourceCfgJsonPath = "Assets/Res/Common/Configs/Common/common_resource.json";
        TextAsset ta_ResourceCfg = AssetDatabase.LoadAssetAtPath<TextAsset>(resourceCfgJsonPath);
        if (ta_ResourceCfg == null)
            return;
        List<Table_Common_Resource> resourceCfgList = JsonConvert.DeserializeObject<List<Table_Common_Resource>>(ta_ResourceCfg.text);
        foreach (var cfg in resourceCfgList)
        {
            if (resourceKeyCache.Contains(cfg.ResourceKey))
            {
                errorStr.AppendLine($"重复的ResourceKey：{cfg.ResourceKey})");
                continue;
            }
            resourceKeyCache.Add(cfg.ResourceKey);
            string resourceNameDefineStr = RESOURCENAME_DEFINE_TEMPLATE;
            string resourceKey;
            if (cfg.ResourceType == (int)EResourceType.Texture)
            {
                resourceKey = cfg.ResourceKey;
                resourceKey = resourceKey.Replace("[", "_");
                resourceKey = resourceKey.Replace("]", "");
            }
            else
            {
                resourceKey = cfg.ResourceKey;
            }
            resourceNameDefineStr = resourceNameDefineStr.Replace("#RESOURCENAME#", resourceKey);
            resourceNameDefineStr = resourceNameDefineStr.Replace("#RESOURCEID#", cfg.Id.ToString());
            nameDefineStr.Append(resourceNameDefineStr + "\n");
        }

        string resourceNameCode = File.ReadAllText(RESOURCENAME_TEMPLATE_PATH);
        resourceNameCode = resourceNameCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        resourceNameCode = resourceNameCode.Replace("#RESOURCENAMEDEFINE#", nameDefineStr.ToString());
        string filePath = $"{RESOURCENAME_GENCODE_PATH}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, resourceNameCode);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string retStr = !string.IsNullOrEmpty(errorStr.ToString())
            ? $"生成路径\n{filePath}\n\n错误信息：\n{errorStr}"
            : $"生成路径\n{filePath}";
        EditorUtils.ShowDialogWindow("生成成功", retStr);
    }

    /// <summary>
    /// 移除所选文件夹下所有预制体上的Missing脚本
    /// </summary>
    [MenuItem(EditorConst.RemoveMonoBehavioursWithMissingScript, priority = EditorConst.Priority_RemoveMonoBehavioursWithMissingScript)]
    private static void RemoveMonoBehavioursWithMissingScript()
    {
        try
        {
            var prefabPathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.PrefabResSuffix);
            List<string> modifyPrefabList = new List<string>();
            for (int i = 0; i < prefabPathList.Count; i++)
            {
                string prefabPath = prefabPathList[i];
                var prefabInstance = PrefabUtility.LoadPrefabContents(prefabPath);
                if (EditorUtility.DisplayCancelableProgressBar("正在分析预制体", $"正在分析{Path.GetFileNameWithoutExtension(prefabPath)} {i + 1}/{prefabPathList.Count}", (i + 1) * 1f / prefabPathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                int removeCount = 0;
                removeCount = EditorUtils.DeleteAllMissingMonoBehaviour(prefabInstance, removeCount);
                if (removeCount > 0)
                {
                    PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
                    modifyPrefabList.Add(Path.GetFileName(prefabPath));
                }
                PrefabUtility.UnloadPrefabContents(prefabInstance);
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"移除预制体上丢失的脚本完成，共操作" + modifyPrefabList.Count + "个预制体：");
            foreach (var modifyPrefab in modifyPrefabList)
            {
                sb.AppendLine(modifyPrefab);
            }
            EditorUtils.ShowDialogWindow($"移除预制体上丢失的脚本完成", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow("移除预制体上丢失的脚本失败", e.ToString());
        }
    }

    /// <summary>
    /// 删除Animator组件
    /// </summary>
    [MenuItem(EditorConst.DeleteAnimator, false, EditorConst.Priority_DeleteAnimator)]
    private static void DeleteAnimator()
    {
        var pathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.PrefabResSuffix);
        EditorUtils.DeleteComponent<Animator>(pathList);
    }

    /// <summary>
    /// 替换组件 Button->GameButton
    /// </summary>
    [MenuItem(EditorConst.ReplaceButton2GameButton, false, EditorConst.Priority_ReplaceButton2GameButton)]
    private static void ReplaceButton2GameButton()
    {
        var pathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.PrefabResSuffix);
        EditorUtils.ReplaceComponent<Button, GameButton>(pathList);
    }
}