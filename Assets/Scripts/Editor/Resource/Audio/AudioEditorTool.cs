using System.Collections.Generic;
using System.IO;
using System.Text;
using DragonPlus.Config.Common;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 声音编辑器工具
/// </summary>
public class AudioEditorTool
{
    public const string AUDIONAME_TEMPLATE_PATH = "Assets/Scripts/Editor/Resource/Audio/Template/AudioNameTemplate.txt"; //AudioName模板路径
    public const string AUDIONAME_GENCODE_PATH = "Assets/Scripts/Hotfix/Common/AutoGen/AudioName"; //自动生成AudioName的文件夹
    private const string AUDIONAME_DEFINE_TEMPLATE = "\tpublic const int #AUDIONAME# = #AUDIOID#;\n"; //声音名称定义模板
    /// <summary>
    /// 生成AudioName
    /// </summary>
    [MenuItem(EditorConst.GenAudioName, false, EditorConst.Priority_GenAudioName)]
    private static void GenAudioName()
    {
        if (!IOUtils.FileExist(AUDIONAME_TEMPLATE_PATH))
        {
            EditorUtils.ShowDialogWindow("生成失败", $"{AUDIONAME_TEMPLATE_PATH}中不存在AudioName模板", "确定");
            return;
        }
        StringBuilder nameDefineStr = new StringBuilder();
        StringBuilder errorStr = new StringBuilder();
        List<int> audioKeyCache = new List<int>();

        string resourceCfgJsonPath = "Assets/Res/Common/Configs/Common/common_resource.json";
        TextAsset ta_ResourceCfg = AssetDatabase.LoadAssetAtPath<TextAsset>(resourceCfgJsonPath);
        if (ta_ResourceCfg == null)
            return;
        List<Table_Common_Resource> resourceCfgList = JsonConvert.DeserializeObject<List<Table_Common_Resource>>(ta_ResourceCfg.text);
        string audioCfgJsonPath = "Assets/Res/Common/Configs/Common/common_audio.json";
        TextAsset ta_AudioCfg = AssetDatabase.LoadAssetAtPath<TextAsset>(audioCfgJsonPath);
        if (ta_AudioCfg == null)
            return;
        List<Table_Common_Audio> audioCfgList = JsonConvert.DeserializeObject<List<Table_Common_Audio>>(ta_AudioCfg.text);
        foreach (var audioCfg in audioCfgList)
        {
            var resourceCfg = resourceCfgList.Find(cfg => cfg.Id == audioCfg.AudioResourceId);
            if (resourceCfg == null)
            {
                errorStr.AppendLine($"Resource表中没有此声音资源，AudioId：{audioCfg.Id})");
                continue;
            }
            if (audioKeyCache.Contains(audioCfg.Id))
            {
                errorStr.AppendLine($"重复的AudioId，AudioId：{audioCfg.Id})");
                continue;
            }
            if (resourceCfg.ResourceType != (int)EResourceType.Audio)
            {
                errorStr.AppendLine($"不是Audio类型，AudioId：{audioCfg.Id})");
                continue;
            }

            audioKeyCache.Add(audioCfg.Id);
            string audioNameDefineStr = AUDIONAME_DEFINE_TEMPLATE;
            string resourceKey = resourceCfg.ResourceKey;
            resourceKey = resourceKey.Replace("/", "_");

            audioNameDefineStr = audioNameDefineStr.Replace("#AUDIONAME#", resourceKey);
            audioNameDefineStr = audioNameDefineStr.Replace("#AUDIOID#", audioCfg.Id.ToString());
            nameDefineStr.Append(audioNameDefineStr + "\n");
        }

        string resourceNameCode = File.ReadAllText(AUDIONAME_TEMPLATE_PATH);
        resourceNameCode = resourceNameCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        resourceNameCode = resourceNameCode.Replace("#AUDIONAMEDEFINE#", nameDefineStr.ToString());
        string filePath = $"{AUDIONAME_GENCODE_PATH}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, resourceNameCode);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string retStr = !string.IsNullOrEmpty(errorStr.ToString())
            ? $"生成路径\n{filePath}\n\n错误信息：\n{errorStr}"
            : $"生成路径\n{filePath}";
        EditorUtils.ShowDialogWindow("生成成功", retStr);
    }
}