using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonPlus.Core.Editor;
using Framework;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Build;
using UnityEditor.iOS;
using UnityEngine;

/// <summary>
/// 写入项目配置 
/// </summary>
public class BuildTask_SetUpProjectSetting : BuildTaskNode
{
    private bool isBuildAAB;

    public BuildTask_SetUpProjectSetting(bool isBuildAAB, string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
        this.isBuildAAB = isBuildAAB;
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

        // 打包选项
        PlayerSettings.SetIl2CppCodeGeneration(target == BuildTarget.Android
                ? NamedBuildTarget.Android
                : NamedBuildTarget.iOS,
            !buildParam.isRelease
                ? Il2CppCodeGeneration.OptimizeSize
                : Il2CppCodeGeneration.OptimizeSpeed);
        PlayerSettings.SetIl2CppCompilerConfiguration(target == BuildTarget.Android
                ? NamedBuildTarget.Android
                : NamedBuildTarget.iOS,
            Il2CppCompilerConfiguration.Release);
        PlayerSettings.SetManagedStrippingLevel(target == BuildTarget.Android
            ? BuildTargetGroup.Android
            : BuildTargetGroup.iOS, (ManagedStrippingLevel)GlobalSetting.Ins.ManagedStrippingLevel);
        EditorUserBuildSettings.development = !buildParam.isRelease;
        // 设置项目信息
        PlayerSettings.bundleVersion = buildParam.GameConfigVersion.version;
        var defaultIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.DefaultIconsPath);
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new[] { defaultIcon });
        if (target == BuildTarget.Android)
        {
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            SetGradle();
            PlayerSettings.Android.bundleVersionCode = int.Parse(buildParam.GameConfigVersion.versionCode);
            PlayerSettings.Android.useCustomKeystore = ConfigurationController.Instance.AndroidKeyStoreUseConfiguration;
            PlayerSettings.Android.keystoreName = ConfigurationController.Instance.AndroidKeyStorePath;
            PlayerSettings.Android.keystorePass = ConfigurationController.Instance.AndroidKeyStorePass;
            PlayerSettings.Android.keyaliasName = ConfigurationController.Instance.AndroidKeyStoreAlias;
            PlayerSettings.Android.keyaliasPass = ConfigurationController.Instance.AndroidKeyStoreAliasPass;
            PlayerSettings.Android.minSdkVersion = (AndroidSdkVersions)GlobalSetting.Ins.SupportMinAndroidVersion;
            PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)GlobalSetting.Ins.TargetAndroidVersion;
            EditorUserBuildSettings.buildAppBundle = isBuildAAB;
            if (GlobalSetting.Ins.AndroidIconsPath != null && GlobalSetting.Ins.AndroidIconsPath.Length > 0)
            {
                // Adaptive
                SetIcon(BuildTargetGroup.Android, AndroidPlatformIconKind.Adaptive,
                    GlobalSetting.Ins.AndroidIconsPath.Length >= 1
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.AndroidIconsPath[0])
                        : defaultIcon);
                // Round
                SetIcon(BuildTargetGroup.Android, AndroidPlatformIconKind.Round,
                    GlobalSetting.Ins.AndroidIconsPath.Length >= 2
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.AndroidIconsPath[1])
                        : defaultIcon);
                // Legacy
                SetIcon(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy,
                    GlobalSetting.Ins.AndroidIconsPath.Length >= 3
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.AndroidIconsPath[2])
                        : defaultIcon);
            }
        }
        else if (target == BuildTarget.iOS)
        {
            PlayerSettings.iOS.buildNumber = buildParam.GameConfigVersion.versionCode;
            PlayerSettings.iOS.appleEnableAutomaticSigning = true;
            PlayerSettings.iOS.targetOSVersionString = GlobalSetting.Ins.SupportMinIosVersion;
            PlayerSettings.iOS.appleDeveloperTeamID = GlobalSetting.Ins.AppleTeamID;
            if (GlobalSetting.Ins.IosIconsPath != null && GlobalSetting.Ins.IosIconsPath.Length > 0)
            {
                // Application
                SetIcon(BuildTargetGroup.iOS, iOSPlatformIconKind.Application,
                    GlobalSetting.Ins.IosIconsPath.Length >= 1
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.IosIconsPath[0])
                        : defaultIcon);
                // Spotlight
                SetIcon(BuildTargetGroup.iOS, iOSPlatformIconKind.Spotlight,
                    GlobalSetting.Ins.IosIconsPath.Length >= 2
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.IosIconsPath[1])
                        : defaultIcon);
                // Settings
                SetIcon(BuildTargetGroup.iOS, iOSPlatformIconKind.Settings,
                    GlobalSetting.Ins.IosIconsPath.Length >= 3
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.IosIconsPath[2])
                        : defaultIcon);
                // Notifications
                SetIcon(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification,
                    GlobalSetting.Ins.IosIconsPath.Length >= 4
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.IosIconsPath[3])
                        : defaultIcon);
                // Marketing
                SetIcon(BuildTargetGroup.iOS, iOSPlatformIconKind.Marketing,
                    GlobalSetting.Ins.IosIconsPath.Length >= 5
                        ? AssetDatabase.LoadAssetAtPath<Texture2D>(GlobalSetting.Ins.IosIconsPath[4])
                        : defaultIcon);
            }
        }
        try
        {
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
        catch (Exception e)
        {
        }
        // 设置宏
        string defineSymbolString = string.Empty;
        if (!buildParam.isRelease)
        {
            for (int i = 0; i < GlobalSetting.Ins.Debug_DefineSymbolList.Length; i++)
            {
                defineSymbolString += GlobalSetting.Ins.Debug_DefineSymbolList[i] + ";";
            }
        }
        else
        {
            for (int i = 0; i < GlobalSetting.Ins.Release_DefineSymbolList.Length; i++)
            {
                defineSymbolString += GlobalSetting.Ins.Release_DefineSymbolList[i] + ";";
            }
        }
        if (target == BuildTarget.Android)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defineSymbolString);
        }
        else if (target == BuildTarget.iOS)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defineSymbolString);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Log("项目配置设置成功");
    }

    /// <summary>
    /// 设置图标
    /// </summary>
    private void SetIcon(BuildTargetGroup targetGroup, PlatformIconKind kind, Texture2D tex)
    {
        var platformIcons = PlayerSettings.GetPlatformIcons(targetGroup, kind);
        for (int i = 0; i < platformIcons.Length; i++)
        {
            var texArray = new Texture2D[platformIcons[i].maxLayerCount];
            for (int j = 0; j < texArray.Length; j++)
            {
                texArray[j] = tex;
            }
            platformIcons[i].SetTextures(texArray);
        }
        PlayerSettings.SetPlatformIcons(targetGroup, kind, platformIcons);
    }

    /// <summary>
    /// 设置gradle路径
    /// </summary>
    private void SetGradle()
    {
#if UNITY_ANDROID
        var gradlePath = AndroidExternalToolsSettings.gradlePath;
        if (string.IsNullOrEmpty(gradlePath))
        {
            CLog.Info($"gradle路径为空\n{DateTime.Now}", ELogTag.AppBuild);
            return;
        }

        gradlePath = gradlePath.Replace("\\", "/");
        var theIndex = gradlePath.LastIndexOf("/");
        if (theIndex <= -1)
            return;

        gradlePath = gradlePath.Substring(0, theIndex) + "/gradle8.7";
        if (!Directory.Exists(gradlePath))
        {
            CLog.Info($"机器上没有gradle8.7\n{DateTime.Now}", ELogTag.AppBuild);
            return;
        }

        AndroidExternalToolsSettings.gradlePath = gradlePath;
        CLog.Info($"设置gradle成功：{gradlePath}\n{DateTime.Now}", ELogTag.AppBuild);
#endif
    }
}