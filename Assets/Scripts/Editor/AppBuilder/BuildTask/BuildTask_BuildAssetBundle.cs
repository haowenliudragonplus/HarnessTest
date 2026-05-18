using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using YooAsset;
using YooAsset.Editor;

/// <summary>
/// 构建AssetBundle
/// </summary>
public class BuildTask_BuildAssetBundle : BuildTaskNode
{
#if UNITY_IOS
    public const string BundleFolderName = "ios";
#elif UNITY_ANDROID
    public const string BundleFolderName = "android";
#endif
    private static string ToUploadAssetBundleDir = Application.dataPath + "/../ToUploadAssetBundle/" + BundleFolderName; //待上传到资源服的文件夹

    public BuildTask_BuildAssetBundle(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        string yooDir = Path.Combine(Application.streamingAssetsPath, YooAssetSettingsData.GetDefaultYooFolderName());
        IOUtils.DeleteDirectory(yooDir);

        // 构建前解密（恢复重要文件为非加密状态）
        RestoreImportantFilesForBuild();

        // 先对某些文件加密一层
        EnhancementEncryption();

        // yoo构建
        ScriptableBuildParameters buildParameters = new ScriptableBuildParameters();
        buildParameters.BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
        buildParameters.BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
        buildParameters.BuildPipeline = EBuildPipeline.ScriptableBuildPipeline.ToString();
        buildParameters.BuildBundleType = (int)EBuildBundleType.AssetBundle;
        buildParameters.BuildTarget = EditorUserBuildSettings.activeBuildTarget;
        buildParameters.PackageName = GlobalSetting.Ins.DefaultPackageName;
        buildParameters.PackageVersion = GetPackageVersion();
        buildParameters.VerifyBuildingResult = true;
        buildParameters.EnableSharePackRule = true;
        buildParameters.FileNameStyle = EFileNameStyle.HashName;
        buildParameters.BuildinFileCopyOption = EBuildinFileCopyOption.ClearAndCopyByTags;
        buildParameters.BuildinFileCopyParams = GlobalSetting.Ins.BuildInResTag;
        buildParameters.CompressOption = ECompressOption.LZ4;
        buildParameters.ClearBuildCacheFiles = false; //不清理构建缓存，启用增量构建，可以提高打包速度！
        buildParameters.UseAssetDependencyDB = true; //使用资源依赖关系数据库，可以提高打包速度！
        buildParameters.ManifestProcessServices = new ManifestProcessServices();
        buildParameters.ManifestRestoreServices = new ManifestRestoreServices();
        if (GlobalSetting.Ins.ResEncrypt)
        {
            buildParameters.EncryptionServices = new EncryptionYooBundle_AES();
        }
        if (Directory.Exists(buildParameters.BuildOutputRoot))
            Directory.Delete(buildParameters.BuildOutputRoot, true);

        ScriptableBuildPipeline pipeline = new ScriptableBuildPipeline();
        var result = pipeline.Run(buildParameters, true);
        if (!result.Success)
        {
            throw new Exception($"YooAsset BuildFailed : {result.ErrorInfo}");
        }
        Log("构建ab包完成");

        RestoreEncryptedImportantFiles(restoredFiles);

        // 拷贝AssetBundle资源到将要上传到资源服的文件夹中
        CopyToToUploadAssetBundleDir(result.OutputPackageDirectory, buildParam.GameConfigVersion.appVersion);

        // 创建资源版本号文件（GM后台判断上传资源时需要）
        CreateResVersionTxt(buildParam.GameConfigVersion.appVersion);
    }

    private void CopyToToUploadAssetBundleDir(string sourceDir, string appVersion)
    {
        if (Directory.Exists(ToUploadAssetBundleDir))
            Directory.Delete(ToUploadAssetBundleDir, true);
        Directory.CreateDirectory(ToUploadAssetBundleDir);
        var sourceDirectory = sourceDir;
        var targetDirectory = Path.Combine(ToUploadAssetBundleDir, appVersion);
        EditorTools.CopyDirectory(sourceDirectory, targetDirectory);
        Log("将构建的ab包拷贝到待上传资源服的目录下");
    }

    /// <summary>
    /// 获取bundle版本号（每次打包都不同）
    /// </summary>
    private string GetPackageVersion()
    {
        int totalMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
        return DateTime.Now.ToString("yyyy-MM-dd") + "-" + totalMinutes;
    }

    /// <summary>
    /// 创建资源版本号文件（GM后台判断上传资源时需要）
    /// </summary>
    private void CreateResVersionTxt(string appVersion)
    {
        if (!Directory.Exists(ToUploadAssetBundleDir))
            Directory.CreateDirectory(ToUploadAssetBundleDir);

        var versionInfo = new VersionInfo();
        versionInfo.Version = appVersion;
        var json = JsonConvert.SerializeObject(versionInfo);

        string versionFilePath = $"{ToUploadAssetBundleDir}/Version.{appVersion.Replace("v", "")}.txt";
        if (!File.Exists(versionFilePath))
        {
            var fileStream = File.Create(versionFilePath);
            fileStream.Close();
        }
        File.WriteAllText(versionFilePath, json);
    }

    /// <summary>
    /// 增强版加密（某些文件需要先加密一层，增加破解成本）
    /// </summary>
    private void EnhancementEncryption()
    {
        try
        {
            // 定义要处理的目录
            string[] targetDirectories = new string[]
            {
                "Assets/Res/InGame/LevelConfigs",
            };

            // 遍历每个目录
            foreach (string targetDir in targetDirectories)
            {
                if (!Directory.Exists(targetDir))
                {
                    continue;
                }

                // 获取目录下所有 json 文件
                string[] jsonFiles = Directory.GetFiles(targetDir, "*.json", SearchOption.AllDirectories);
                Log($"{targetDir}目录下需要增强加密的文件数量：" + jsonFiles.Length);

                int succussCount = 0;
                foreach (string jsonFile in jsonFiles)
                {
                    string jsonContent = File.ReadAllText(jsonFile);
                    string newJsonFilePath = Path.ChangeExtension(jsonFile, ".txt");
                    // 先压缩
                    byte[] compressedBytes;
                    using (var ms = new MemoryStream())
                    {
                        using (var gzip = new GZipStream(ms, CompressionMode.Compress, true))
                        {
                            var plainBytes = Encoding.UTF8.GetBytes(jsonContent);
                            gzip.Write(plainBytes, 0, plainBytes.Length);
                        }
                        compressedBytes = ms.ToArray();
                    }
                    //
                    string ciphertextBase64 = AESUtils.EncryptToBase64(compressedBytes, GameSafeCenter.EnhancementEncryptKey, CipherMode.CBC);
                    File.WriteAllText(newJsonFilePath, ciphertextBase64, new UTF8Encoding(false));
                    // 删除原来的 json 文件
                    File.Delete(jsonFile);
                    // 删除对应的.meta 文件
                    string metaFile = jsonFile + ".meta";
                    if (File.Exists(metaFile))
                    {
                        File.Delete(metaFile);
                    }
                    succussCount++;
                }

                Log($"{targetDir}目录下增强加密成功的文件数量：" + succussCount);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            throw new Exception($"加密失败，错误信息：{e.Message}");
        }
    }

    private List<RestoredImportantFile> restoredFiles = new List<RestoredImportantFile>();
    /// <summary>
    /// 构建前解密（恢复重要文件为非加密状态）
    /// </summary>
    private void RestoreImportantFilesForBuild()
    {
        string localEncryptKey = GameSafeCenter.LoadLocalEncryptKey();
        if (string.IsNullOrWhiteSpace(localEncryptKey))
        {
            throw new BuildFailedException($"[RestoreImportantFilesForBuild] 解密失败，本地没有密钥文件");
        }

        List<EncryptedImportantFile> encryptedFiles = CollectEncryptedImportantFiles();
        if (encryptedFiles.Count == 0)
        {
            Log("[RestoreImportantFilesForBuild] 没有发现需要解密的文件，跳过此步骤");
            return;
        }
        Log($"[RestoreImportantFilesForBuild] 发现{encryptedFiles.Count}个需要解密的文件，准备解密");

        int successCount = 0;
        foreach (EncryptedImportantFile encryptedFile in encryptedFiles)
        {
            if (!GameSafeCenter.TryDecryptImportantFileWithoutFallback(encryptedFile.EncryptedText, localEncryptKey, out var _plaintext))
            {
                throw new BuildFailedException($"[RestoreImportantFilesForBuild] 文件解密失败：{encryptedFile.AssetPath}");
            }

            restoredFiles.Add(new RestoredImportantFile
            {
                AssetPath = encryptedFile.AssetPath,
                OriginalBytes = encryptedFile.OriginalBytes
            });

            try
            {
                File.WriteAllText(encryptedFile.AssetPath, _plaintext, new UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"[RestoreImportantFilesForBuild] 文件写入异常：{encryptedFile.AssetPath}\n{ex.Message}");
            }

            successCount++;
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        Log($"[RestoreImportantFilesForBuild] 共需要解密{encryptedFiles.Count}个文件，已使用本地密钥成功解密还原了{successCount}个重要文件");
    }

    /// <summary>
    /// 构建后恢复加密
    /// </summary>
    private void RestoreEncryptedImportantFiles(List<RestoredImportantFile> restoredFiles)
    {
        if (restoredFiles == null || restoredFiles.Count == 0)
            return;

        var failures = new List<string>();
        for (int i = restoredFiles.Count - 1; i >= 0; i--)
        {
            RestoredImportantFile restoredFile = restoredFiles[i];
            try
            {
                File.WriteAllBytes(restoredFile.AssetPath, restoredFile.OriginalBytes);
            }
            catch (Exception ex)
            {
                failures.Add($"{restoredFile.AssetPath}: {ex.Message}");
            }
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        restoredFiles.Clear();

        if (failures.Count > 0)
            throw new BuildFailedException($"[RestoreEncryptedImportantFiles] 恢复重要文件为加密状态有异常:\n{string.Join("\n", failures)}");

        Log("[RestoreEncryptedImportantFiles] 构建后已恢复重要文件为加密状态");
    }

    private List<EncryptedImportantFile> CollectEncryptedImportantFiles()
    {
        var encryptedFiles = new List<EncryptedImportantFile>();
        string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets" });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath))
                continue;
            if (assetPath.Contains("GroupC"))
                continue;

            string encryptedText;
            try
            {
                encryptedText = File.ReadAllText(assetPath, Encoding.UTF8);
                if (!GameSafeCenter.TryGetCiphertext(encryptedText, GameSafeCenter.ImportantFileEncryptPrefix, out var _ciphertext))
                    continue;
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"[CollectEncryptedImportantFiles] 读取密文异常 {assetPath}\n{ex.Message}");
            }
            byte[] originalBytes;
            try
            {
                originalBytes = File.ReadAllBytes(assetPath);
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"[CollectEncryptedImportantFiles] 读取字节异常: {assetPath}\n{ex.Message}");
            }

            encryptedFiles.Add(new EncryptedImportantFile
            {
                AssetPath = assetPath,
                OriginalBytes = originalBytes,
                EncryptedText = encryptedText
            });
        }

        return encryptedFiles;
    }

    private class EncryptedImportantFile
    {
        public string AssetPath;
        public byte[] OriginalBytes;
        public string EncryptedText;
    }

    private class RestoredImportantFile
    {
        public string AssetPath;
        public byte[] OriginalBytes;
    }

    private class VersionInfo
    {
        public string Version;
    }
}

/// <summary>
/// Bundle加密
/// </summary>
public class EncryptionYooBundle_AES : IEncryptionServices
{
    public EncryptResult Encrypt(EncryptFileInfo fileInfo)
    {
        var byteArray = File.ReadAllBytes(fileInfo.FileLoadPath);
        var ciphertextBase64 = AESUtils.EncryptToBase64(byteArray, GlobalSetting.Ins.Res_AESKey, CipherMode.CBC);

        EncryptResult result = new EncryptResult();
        result.Encrypted = true;
        result.EncryptedData = Convert.FromBase64String(ciphertextBase64);
        return result;
    }
}

/// <summary>
/// Manifest加密
/// </summary>
public class ManifestProcessServices : IManifestProcessServices
{
    public byte[] ProcessManifest(byte[] fileData)
    {
        return XORUtils.Crypto(fileData, GlobalSetting.Ins.Res_AESKey);
    }
}
