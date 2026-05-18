using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using Google.Android.AppBundle.Editor;
using UnityEditor;
using UnityEngine;
using YooAsset;
using YooAsset.Editor;

/// <summary>
/// 构建AAB
/// </summary>
public class BuildTask_BuildAAB : BuildTaskNode
{
    public BuildTask_BuildAAB(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        List<string> levelList = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
        {
            if (!EditorBuildSettings.scenes[i].enabled)
                continue;
            levelList.Add(EditorBuildSettings.scenes[i].path);
        }
        string fileName = PlayerSettings.productName + ".aab";

        string androidOutputDir = Application.dataPath + "/../AndroidExport";
        if (Directory.Exists(androidOutputDir))
        {
            Directory.Delete(androidOutputDir, true);
        }
        Directory.CreateDirectory(androidOutputDir);
        string androidOutputPath = androidOutputDir + $"/{fileName}";
        Log($"构建安卓的路径：{androidOutputPath}");

        // 将AssetBundle资源拷贝到独立文件夹
        string buildInAssetBundleDir = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
        string androidAssetPackOutputPath = Directory.GetParent(androidOutputPath) + "/" + YooAssetSettingsData.Setting.DefaultYooFolderName;
        Directory.Move(buildInAssetBundleDir, androidAssetPackOutputPath);
        Log($"移动到的路径：{androidAssetPackOutputPath}");
        
        //
        var buildPlayerOptions = AndroidBuildHelper.CreateBuildPlayerOptions(androidOutputPath);
        buildPlayerOptions.options = !buildParam.isRelease ? BuildOptions.Development : BuildOptions.None;
        AssetPackConfig assetPackConfig = new AssetPackConfig();
        assetPackConfig.AddAssetsFolder("InstallTimeAssetPack", androidAssetPackOutputPath, AssetPackDeliveryMode.InstallTime);
        System.Threading.Thread.Sleep(1000);
        if (!string.IsNullOrEmpty(PlayerSettings.Android.keystoreName) &&
            ConfigurationController.Instance.AndroidKeyStorePath != PlayerSettings.Android.keystoreName)
        {
            IOUtils.CopyFile(ConfigurationController.Instance.AndroidKeyStorePath, PlayerSettings.Android.keystoreName);
        }
        var result = Bundletool.BuildBundle(buildPlayerOptions, assetPackConfig, true);
        if (!string.IsNullOrEmpty(PlayerSettings.Android.keystoreName) &&
            ConfigurationController.Instance.AndroidKeyStorePath != PlayerSettings.Android.keystoreName)
        {
            IOUtils.DeleteFile(PlayerSettings.Android.keystoreName);
        }
        Log($"打包AAB完成：{result}");
    }
}