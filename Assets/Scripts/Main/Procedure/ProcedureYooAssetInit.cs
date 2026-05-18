using System.IO;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using Framework;
using UnityEngine;
using YooAsset;

/// <summary>
/// 初始化yooasset
/// </summary>
public class ProcedureYooAssetInit : Procedure
{
    public static EPlayMode PlayMode { get; private set; } //运行模式

    public static bool IsCoverInstall { get; private set; } //本次是否为覆盖安装

    public ProcedureYooAssetInit(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        // 获取运行模式
#if UNITY_EDITOR
        PlayMode = YooAssetPlayModeConfiguration.Ins.PlayMode;
#else
        PlayMode = EPlayMode.HostPlayMode;
#endif
        // 判断水印是否发生变化（如果水印发生变化，则说明是覆盖安装后首次打开游戏）
        IsCoverInstall = CheckAppFootPrint();

        // 初始化资源系统
        YooAssets.Initialize();
        // 创建资源包
        var defaultPackage = YooAssets.TryGetPackage(GlobalSetting.Ins.DefaultPackageName);
        if (defaultPackage == null)
            defaultPackage = YooAssets.CreatePackage(GlobalSetting.Ins.DefaultPackageName);
        // 设置该资源包为默认的资源包
        YooAssets.SetDefaultPackage(defaultPackage);
        // 构建资源包
        {
            InitializationOperation initOperation = null;
            // 编辑器模拟模式
            if (PlayMode == EPlayMode.EditorSimulateMode)
            {
                var buildResult = EditorSimulateModeHelper.SimulateBuild(defaultPackage.PackageName);
                var packageRoot = buildResult.PackageRootDirectory;
                var editorFileSystemParams = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
                var initParameters = new EditorSimulateModeParameters();
                initParameters.EditorFileSystemParameters = editorFileSystemParams;
                initOperation = defaultPackage.InitializeAsync(initParameters);
            }
            // 联机运行模式
            else if (PlayMode == EPlayMode.HostPlayMode)
            {
                string hostServerURl = GetHostServerURL();
                CLog.Info($"host url：{hostServerURl}");
                IRemoteServices remoteServices = new RemoteServices(hostServerURl, hostServerURl);
                IDecryptionServices decryptionServices = GlobalSetting.Ins.ResEncrypt
                    ? new DecryptionYooBundle_AES()
                    : null;
                IManifestRestoreServices manifestRestoreServices = new ManifestRestoreServices();
                var cacheFileSystemParams = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices, decryptionServices);
                cacheFileSystemParams.AddParameter(FileSystemParametersDefine.INSTALL_CLEAR_MODE, EOverwriteInstallClearMode.None);
                cacheFileSystemParams.AddParameter(FileSystemParametersDefine.MANIFEST_SERVICES, manifestRestoreServices);
                var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters(decryptionServices);
                buildinFileSystemParams.AddParameter(FileSystemParametersDefine.COPY_BUILDIN_PACKAGE_MANIFEST, true);

                var initParameters = new HostPlayModeParameters();
                initParameters.BuildinFileSystemParameters = Main.GameUtils.IsInEditorEnv()
                    ? null
                    : buildinFileSystemParams;
                initParameters.CacheFileSystemParameters = cacheFileSystemParams;
                initOperation = defaultPackage.InitializeAsync(initParameters);
            }

            await initOperation.Task;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                CLog.Info("----------ProcedureAssetModuleInitialize Success", logColor: ELogColor.Cyan);
            }
            else
            {
                CLog.Error("ProcedureAssetModuleInitialize failed" + initOperation.Error);
            }
        }
    }

    private string GetHostServerURL()
    {
        string bundleFolderName = string.Empty;
#if UNITY_IOS
        bundleFolderName = "ios";
#elif UNITY_ANDROID
        bundleFolderName = "android";
#endif
        var url = ConfigurationController.Instance.ResServerURL +
                  $"{bundleFolderName}/{GameConfig.AppVersion}";
        return url;
    }

    /// <summary>
    /// 检查水印变化
    /// </summary>
    private bool CheckAppFootPrint()
    {
        if (Main.GameUtils.IsInEditorEnv())
            return false;

        string footPrintFilePath = Path.Combine(Application.persistentDataPath,
            YooAssetSettingsData.GetDefaultYooFolderName(),
            GlobalSetting.Ins.DefaultPackageName,
            "ManifestFiles",
            "ApplicationFootPrint.bytes");
        if (!File.Exists(footPrintFilePath))
        {
            CLog.Info("无水印文件，本次不是覆盖安装");
            return false;
        }
        else
        {
            var footPrint = File.ReadAllText(footPrintFilePath);
            if (string.IsNullOrEmpty(footPrint))
            {
                CLog.Info("水印为空，本次不是覆盖安装");
                return false;
            }
            if (footPrint == Application.buildGUID)
            {
                CLog.Info("水印无变化，本次不是覆盖安装");
                return false;
            }
            CLog.Info("水印发生变化，本次是覆盖安装");
            return true;
        }
    }
}