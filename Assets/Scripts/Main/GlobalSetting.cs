using System;
using System.IO;
using System.Security.Cryptography;
using Framework;
using Main;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YooAsset;

//[CreateAssetMenu(fileName = "GlobalSetting", menuName = "GlobalSetting", order = 0)]
public class GlobalSetting : ScriptableObject
{
    [Header("-----Log配置（只在编辑器下使用此配置）-----")]
    public bool LogEnable;
    public ELogType LogTypeMask;
    public ELogTag LogTagMask;

    [Space(11)]
    [Header("资源包名（需要与yoo资源配置相同）")]
    public string DefaultPackageName = "GamePackage";
    [Header("内置资源标签（需要与yoo资源配置相同）")]
    public string BuildInResTag = "BuildIn";

    [Space(11)]
    [Header("支持的语言")]
    public ELanguageType SupportLanguage;

    [Space(11)]
    [Header("热更dll列表")]
    public string[] HotfixAssemblyList;
    [Header("额外的热更dll目录列表（纯DLL情况）")]
    public string[] ExternalHotfixAssemblyDirList;
    [Header("补充元数据dll列表")]
    public string[] PatchedAOTAssemblyList;

    [Space(11)]
    [Header("-----项目设置-----")]
    [Header("支持的最小Android版本")]
    public int SupportMinAndroidVersion;
    [Header("目标Android版本")]
    public int TargetAndroidVersion;
    [Header("支持的最小IOS版本")]
    public string SupportMinIosVersion;
    [Header("代码裁剪等级")]
    [Tooltip("0:Disabled\n1:Low\n2:Medium\n3:High\n4:Minimal")]
    public int ManagedStrippingLevel;
    [Header("正式包的宏")]
    public string[] Release_DefineSymbolList;
    [Header("测试包的宏")]
    public string[] Debug_DefineSymbolList;
    [JsonIgnore]
    [Header("默认APP图标")]
    public Texture2D DefaultIcon;
    [HideInInspector]
    public string DefaultIconsPath;
    [Header("安卓图标（Adaptive、Round、Legacy）")]
    [JsonIgnore]
    public Texture2D[] AndroidIcons;
    [HideInInspector]
    public string[] AndroidIconsPath;
    [Header("苹果图标（Application、Spotlight、Settings、Notifications、Marketing）")]
    [JsonIgnore]
    public Texture2D[] IOSIcons;
    [HideInInspector]
    public string[] IosIconsPath;

    [Space(11)]
    [Header("苹果TeamID")]
    public string AppleTeamID;

    [Space(11)]
    [Header("资源是否加密")]
    public bool ResEncrypt;
    [Header("资源的AES加密密钥")]
    public string Res_AESKey;

    #region GlobalSetting自身加密解密

    private const string GlobalSettingFilePath = "Assets/Settings/GlobalSetting.asset"; //全局配置文件路径
    private const string GlobalSettingCiphetextFilePath = "Resources/Settings/GlobalSetting.txt"; //全局配置文件密文路径
    private const string GlobalSetting_AESKey = "wodemimahenchang"; //GlobalSetting加密解密的密钥

#if UNITY_EDITOR
    /// <summary>
    /// 加密并保存到文件
    /// </summary>
    public static void EncryptAndSaveToFile()
    {
        GlobalSetting globalSetting = AssetDatabase.LoadAssetAtPath<GlobalSetting>(GlobalSettingFilePath);
        if (globalSetting == null)
        {
            CLog.Error($"全局配置文件不存在，{GlobalSettingFilePath}");
        }
        // 处理不能被序列化的字段
        {
            globalSetting.DefaultIconsPath = AssetDatabase.GetAssetPath(globalSetting.DefaultIcon);
            globalSetting.AndroidIconsPath = new string[globalSetting.AndroidIcons.Length];
            for (int i = 0; i < globalSetting.AndroidIconsPath.Length; i++)
            {
                globalSetting.AndroidIconsPath[i] = AssetDatabase.GetAssetPath(globalSetting.AndroidIcons[i]);
            }
            globalSetting.IosIconsPath = new string[globalSetting.IOSIcons.Length];
            for (int i = 0; i < globalSetting.IosIconsPath.Length; i++)
            {
                globalSetting.IosIconsPath[i] = AssetDatabase.GetAssetPath(globalSetting.IOSIcons[i]);
            }
        }
        var jsonStr = JsonConvert.SerializeObject(globalSetting);
        var ciphetextBase64 = AESUtils.EncryptToBase64(jsonStr, GlobalSetting_AESKey, CipherMode.CBC);
        string ciphetextFilePath = Path.Combine(Application.dataPath, GlobalSettingCiphetextFilePath);
        using (FileStream fs = new FileStream(ciphetextFilePath, FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(ciphetextBase64);
            }
        }
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
#endif

    /// <summary>
    /// 解密并转换为ScriptableObject
    /// </summary>
    private static GlobalSetting DecryptToScriptableObject()
    {
#if UNITY_EDITOR
        var globalSetting = AssetDatabase.LoadAssetAtPath<GlobalSetting>(GlobalSettingFilePath);
        return globalSetting;
#else
        var pathInResources = GlobalSettingCiphetextFilePath.Replace("Resources/", "")
            .Replace(".txt", "");
        var ta = Resources.Load<TextAsset>(pathInResources);
        if (ta == null)
        {
            CLog.Error($"不存在全局配置文件，路径：{GlobalSettingCiphetextFilePath}");
            return null;
        }
        var byteArray = AESUtils.DecryptFromBase64(ta.text, GlobalSetting_AESKey, CipherMode.CBC);
        string jsonStr = System.Text.Encoding.UTF8.GetString(byteArray);
        var so = JsonConvert.DeserializeObject<GlobalSetting>(jsonStr);
        return so;
#endif
    }

    #endregion GlobalSetting自身加密解密

    private static GlobalSetting ins;
    public static GlobalSetting Ins
    {
        get
        {
            if (ins == null)
            {
                ins = DecryptToScriptableObject();
            }
            return ins;
        }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(GlobalSetting))]
public class GlobalSettingEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("保存配置", GUILayout.Height(50)))
        {
            GlobalSetting.EncryptAndSaveToFile();
        }
        base.OnInspectorGUI();
    }
}
#endif