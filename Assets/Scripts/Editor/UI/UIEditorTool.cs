using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DragonPlus.Config.Common;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class UIEditorTool
{
    public const string UIVIEWNAME_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewNameTemplate.txt"; //UIViewName模板路径
    public const string UIVIEWNAME_GENCODE_PATH = "Assets/Scripts/Hotfix/Common/Module/UI/AutoGen/UIViewName"; //自动生成UIViewName的文件夹
    private const string UIVIEWNAME_DEFINE_TEMPLATE = "\tpublic const int #VIEWNAME# = #VIEWID#;\n"; //界面名称定义模板
    /// <summary>
    /// 生成UIViewName
    /// </summary>
    [MenuItem(EditorConst.GenUIViewName, false, EditorConst.Priority_GenUIViewName)]
    private static void GenUIViewName()
    {
        if (!IOUtils.FileExist(UIVIEWNAME_TEMPLATE_PATH))
        {
            EditorUtils.ShowDialogWindow("生成失败", $"{UIVIEWNAME_TEMPLATE_PATH}中不存在UIViewName模板");
            return;
        }
        StringBuilder str = new StringBuilder();

        string uiViewCfgJsonPath = "Assets/Res/Common/Configs/Common/common_uiview.json";
        TextAsset ta_UIViewCfg = AssetDatabase.LoadAssetAtPath<TextAsset>(uiViewCfgJsonPath);
        if (ta_UIViewCfg == null)
        {
            EditorUtils.ShowDialogWindow("生成失败", $"加载UIView配置失败，{uiViewCfgJsonPath}");
            return;
        }
        List<Table_Common_UIView> uiViewCfgs = JsonConvert.DeserializeObject<List<Table_Common_UIView>>(ta_UIViewCfg.text);
        string resourceCfgJsonPath = "Assets/Res/Common/Configs/Common/common_resource.json";
        TextAsset ta_ResourceCfg = AssetDatabase.LoadAssetAtPath<TextAsset>(resourceCfgJsonPath);
        if (ta_ResourceCfg == null)
        {
            EditorUtils.ShowDialogWindow("生成失败", $"加载Resource配置失败，{resourceCfgJsonPath}");
            return;
        }
        List<Table_Common_Resource> uiResourceCfgs = JsonConvert.DeserializeObject<List<Table_Common_Resource>>(ta_ResourceCfg.text);
        foreach (var uiViewCfg in uiViewCfgs)
        {
            string uiViewNameDefineStr = UIVIEWNAME_DEFINE_TEMPLATE;
            var resourceCfg = uiResourceCfgs.Find(v => v.Id == uiViewCfg.ResourceId);
            if (resourceCfg == null)
            {
                Debug.LogError("Resource表中不存在Id为" + uiViewCfg.ResourceId + "的配置");
                continue;
            }
            string resKey = resourceCfg.ResourceKey;
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWNAME#", Path.GetFileName(resKey));
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWID#", uiViewCfg.Id.ToString());
            str.Append(uiViewNameDefineStr + "\n");
        }
        string uiViewNmaeCode = File.ReadAllText(UIVIEWNAME_TEMPLATE_PATH);
        uiViewNmaeCode = uiViewNmaeCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        uiViewNmaeCode = uiViewNmaeCode.Replace("#UIVIEWNAMEDEFINE#", str.ToString());
        string filePath = $"{UIVIEWNAME_GENCODE_PATH}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, uiViewNmaeCode);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtils.ShowDialogWindow("生成成功", $"生成路径\n{filePath}");
    }

    /// <summary>
    /// 打开生成UI信息文件夹
    /// </summary>
    [MenuItem(EditorConst.OpenGenUIInfoDir, false, EditorConst.Priority_OpenGenUIInfoDir)]
    public static void OpenGenUIInfoDir()
    {
        IOUtils.OpenFolder(Application.dataPath + "/../", GenerateUIEditor.UIGENINFOARCHIVEPATH);
    }
}