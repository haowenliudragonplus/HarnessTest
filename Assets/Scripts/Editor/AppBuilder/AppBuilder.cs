using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEditor;

/// <summary>
/// 处理打包
/// </summary>
public class AppBuilder
{
    private static BehaviourSequence buildSequence; //打包序列
    private static AppBuilderParam appBuilderParam; //打包参数

    public static async void JenkinsBuild()
    {
        appBuilderParam = new AppBuilderParam();

        string JobName = string.Empty;
        string BuildType = string.Empty;
        // 解析shell传入的参数
        foreach (string commandLineArg in System.Environment.GetCommandLineArgs())
        {
            var args = commandLineArg.Split(':');
            if (args[0] == "JobName")
                JobName = args[1];
            if (args[0] == "BuildType")
                BuildType = args[1];
        }
        bool isRelease = JobName.ToLower().Contains("release");
        bool isIos = JobName.ToLower().Contains("ios");
        appBuilderParam.isIos = isIos;
        appBuilderParam.isRelease = isRelease;
        CLog.Info($"获取构建参数完成\nBuildType：{BuildType}\nisRelease：{isRelease}\nisIos：{isIos}" +
                  $"\n{DateTime.Now}", ELogTag.AppBuild);
        // 根据构建类型进行构建
        switch (BuildType)
        {
            case "BuildAPK":
                await BuildAPK();
                break;

            case "BuildIPA":
                await BuildXcode();
                break;

            case "BuildBundle":
                await BuildAssetBundle();
                break;

            case "BuildAAB":
                await BuildAAB();
                break;

            default:
                await BuildAPK();
                break;
        }
    }

    /// <summary>
    /// 构建ab包
    /// </summary>
    public static async UniTask BuildAssetBundle()
    {
        buildSequence = new BehaviourSequence();
        buildSequence.AddNode(new BuildTask_FetchGameConfigVersion("FetchGameConfigVersion"));
        buildSequence.AddNode(new BuildTask_EncryptGlobalSetting("EncryptGlobalSetting"));
        buildSequence.AddNode(new BuildTask_BuildHybridCLR(true, "BuildHybridCLR"));
        buildSequence.AddNode(new BuildTask_BuildAssetBundle("BuildAssetBundle"));
        await buildSequence.Run(appBuilderParam);
    }

    /// <summary>
    /// 构建apk
    /// </summary>
    public static async UniTask BuildAPK()
    {
        buildSequence = new BehaviourSequence();
        buildSequence.AddNode(new BuildTask_FetchGameConfigVersion("FetchGameConfigVersion"));
        buildSequence.AddNode(new BuildTask_EncryptGlobalSetting("EncryptGlobalSetting"));
        buildSequence.AddNode(new BuildTask_SetUpProjectSetting(false, "SetUpProjectSetting"));
        buildSequence.AddNode(new BuildTask_SetUpDragonSDK("SetUpDragonSDK"));
        buildSequence.AddNode(new BuildTask_BuildHybridCLR(false, "BuildHybridCLR"));
        buildSequence.AddNode(new BuildTask_BuildAssetBundle("BuildAssetBundle"));
        buildSequence.AddNode(new BuildTask_BuildApk("BuildApk"));
        await buildSequence.Run(appBuilderParam);
    }

    /// <summary>
    /// 构建AAB
    /// </summary>
    public static async UniTask BuildAAB()
    {
        buildSequence = new BehaviourSequence();
        buildSequence.AddNode(new BuildTask_FetchGameConfigVersion("FetchGameConfigVersion"));
        buildSequence.AddNode(new BuildTask_EncryptGlobalSetting("EncryptGlobalSetting"));
        buildSequence.AddNode(new BuildTask_SetUpProjectSetting(true, "SetUpProjectSetting"));
        buildSequence.AddNode(new BuildTask_SetUpDragonSDK("SetUpDragonSDK"));
        buildSequence.AddNode(new BuildTask_BuildHybridCLR(false, "BuildHybridCLR"));
        buildSequence.AddNode(new BuildTask_BuildAssetBundle("BuildAssetBundle"));
        buildSequence.AddNode(new BuildTask_BuildAAB("BuildAAB"));
        await buildSequence.Run(appBuilderParam);
    }

    /// <summary>
    /// 构建Xcode工程
    /// </summary>
    public static async UniTask BuildXcode()
    {
        buildSequence = new BehaviourSequence();
        buildSequence.AddNode(new BuildTask_FetchGameConfigVersion("FetchGameConfigVersion"));
        buildSequence.AddNode(new BuildTask_EncryptGlobalSetting("EncryptGlobalSetting"));
        buildSequence.AddNode(new BuildTask_SetUpProjectSetting(false, "SetUpProjectSetting"));
        buildSequence.AddNode(new BuildTask_SetUpDragonSDK("SetUpDragonSDK"));
        buildSequence.AddNode(new BuildTask_BuildHybridCLR(false, "BuildHybridCLR"));
        buildSequence.AddNode(new BuildTask_BuildAssetBundle("BuildAssetBundle"));
        buildSequence.AddNode(new BuildTask_CopyDiffFile("CopyDiffFile"));
        buildSequence.AddNode(new BuildTask_CopyDiffCode("CopyDiffCode"));
        buildSequence.AddNode(new BuildTask_BuildXcode("BuildXcode"));
        await buildSequence.Run(appBuilderParam);
    }

    #region 编辑器工具

    [MenuItem("AppBuilder/Player/Build Android Debug")]
    private static async UniTask Editor_BuildPlayer_AndroidDebug()
    {
        appBuilderParam = new AppBuilderParam();
        appBuilderParam.isRelease = false;
        appBuilderParam.isIos = false;
        await BuildAPK();
    }

    [MenuItem("AppBuilder/Player/Build Android Release(AAB)")]
    private static async UniTask Editor_BuildPlayer_AndroidRelease()
    {
        appBuilderParam = new AppBuilderParam();
        appBuilderParam.isRelease = true;
        appBuilderParam.isIos = false;
        await BuildAAB();
    }

    [MenuItem("AppBuilder/AssetBundle/Build AssetBundle Debug(当前平台)")]
    private static async UniTask Editor_BuildAssetBundle_Debug()
    {
        appBuilderParam = new AppBuilderParam();
        appBuilderParam.isRelease = false;
        appBuilderParam.isIos = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
        await BuildAssetBundle();
    }

    [MenuItem("AppBuilder/AssetBundle/Build AssetBundle Release(当前平台)")]
    private static async UniTask Editor_BuildAssetBundle_Release()
    {
        appBuilderParam = new AppBuilderParam();
        appBuilderParam.isRelease = true;
        appBuilderParam.isIos = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
        await BuildAssetBundle();
    }

    #endregion 编辑器工具
}

public class AppBuilderParam : BehaviourSequenceParam
{
    public GameConfig_Version GameConfigVersion; //版本号信息
    public bool isRelease; //是否为release
    public bool isIos; //是否为ios
}