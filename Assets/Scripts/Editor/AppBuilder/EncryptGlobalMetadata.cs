using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
#if UNITY_ANDROID
using UnityEditor.Android;
#endif
using Random = System.Random;

/// <summary>
/// 双端统一 global-metadata.dat 加密
/// iOS: PostProcessBuild 在 Xcode 工程导出后执行
/// Android 预处理: PreprocessBuild 在 IL2CPP 编译前把解密补丁覆盖到 HybridCLR 本地 IL2CPP 源
/// Android 后处理: IPostGenerateGradleAndroidProject 在 Gradle 工程生成后再次覆盖兜底，并加密 metadata
/// </summary>
public class EncryptGlobalMetadata :
    IPreprocessBuildWithReport,
    IPostprocessBuildWithReport
#if UNITY_ANDROID
    , IPostGenerateGradleAndroidProject
#endif
{
    private const int SafeSizeBytes = 1024;
    private const int CodeLen = 16;

    // 解密特征字符串，用于校验补丁是否真正写入
    private const string DecryptSignature = "g_cacheDecodeHeader";

    // 补丁在 il2cpp 源目录内的相对前缀（需要被去掉才能映射到本地 il2cpp）
    private const string Il2CppOutputPrefix = "Il2CppOutputProject/IL2CPP/";

    // callbackOrder 与 iOS PostProcessBuild(10001) 保持一致
    public int callbackOrder => 10001;

    // -----------------------------------------------------------------------
    // 全平台预处理：Android 平台在 IL2CPP 编译前覆盖本地 IL2CPP 源
    // -----------------------------------------------------------------------

    public void OnPreprocessBuild(BuildReport report)
    {
#if UNITY_ANDROID
        if (report.summary.platform != BuildTarget.Android)
            return;

        PatchLocalIl2CppBeforeCompile();
#endif
    }

    // -----------------------------------------------------------------------
    // iOS: PostProcessBuild 在 Xcode 工程导出后执行
    // -----------------------------------------------------------------------

    public void OnPostprocessBuild(BuildReport report)
    {
#if UNITY_IOS
        if (report.summary.platform != BuildTarget.iOS)
            return;

        string pathToBuiltProject = report.summary.outputPath;

        // 拷贝项目根目录下 il2cpp 文件夹中的 .cpp 文件到 Xcode 工程（覆盖）
        CopyIl2CppCppFilesToXcode(pathToBuiltProject);

        // 加密 metadata
        string metadataPath = Path.Combine(pathToBuiltProject, "Data/Managed/Metadata/global-metadata.dat");
        TryEncodeMetaData(metadataPath);
#endif
    }

    // -----------------------------------------------------------------------
    // Android: IPostGenerateGradleAndroidProject 入口（Gradle 工程生成后加密）
    // -----------------------------------------------------------------------

#if UNITY_ANDROID
    public void OnPostGenerateGradleAndroidProject(string path)
    {
        // path 可能是 unityLibrary 目录，也可能是父级导出目录
        string gradleLibPath = Directory.Exists(path) ? path : FindAndroidGradleLibraryPath();
        if (gradleLibPath == null)
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android: 无法确定 Gradle 工程目录，回调路径: {path}");
        }

        string srcMainDir = Path.Combine(gradleLibPath, "src/main");
        if (!Directory.Exists(srcMainDir))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android: src/main 目录不存在: {srcMainDir}");
        }

        // 加密 global-metadata.dat
        string metadataPath = FindAndroidMetadataPath(srcMainDir);
        if (metadataPath == null)
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android: 未找到 global-metadata.dat，请检查 Gradle 工程结构。src/main: {srcMainDir}");
        }

        TryEncodeMetaData(metadataPath);
    }

    private static string FindAndroidGradleLibraryPath()
    {
        // 兜底：在 Library 目录下搜索 unityLibrary 目录
        string libraryDir = Path.GetFullPath(Application.dataPath + "/../Library");
        if (!Directory.Exists(libraryDir))
            return null;

        var candidates = Directory.GetDirectories(libraryDir, "unityLibrary", SearchOption.AllDirectories);
        if (candidates.Length == 1)
        {
            Debug.Log($"[EncryptGlobalMetadata] Android: Library 兜底命中 {candidates[0]}");
            return candidates[0];
        }

        return null;
    }
#endif

    // -----------------------------------------------------------------------
    // Android 预处理：覆盖 HybridCLR 本地 IL2CPP 源（在 IL2CPP 编译 libil2cpp.so 之前）
    // 源: il2cpp/Il2CppOutputProject/IL2CPP/**/*.cpp
    // 目标: HybridCLRData/LocalIl2CppData-{WindowsEditor}/il2cpp/**/*.cpp
    //   (去掉 Il2CppOutputProject/IL2CPP/ 前缀)
    // -----------------------------------------------------------------------

    private static void PatchLocalIl2CppBeforeCompile()
    {
        string patchSourceDir = Path.GetFullPath(
            Path.Combine(Application.dataPath, "../il2cpp"));

        if (!Directory.Exists(patchSourceDir))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android PreBuild: 补丁源目录不存在: {patchSourceDir}");
        }

        string expectedPatchRelative = $"Il2CppOutputProject/IL2CPP/libil2cpp/vm/MetadataLoader.cpp";
        string expectedPatchFull = Path.Combine(patchSourceDir, expectedPatchRelative);
        if (!File.Exists(expectedPatchFull))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android PreBuild: 未找到解密补丁 MetadataLoader.cpp，中止打包。" +
                $"\n预期路径: {expectedPatchFull}");
        }

        // 校验补丁源包含解密特征
        string patchContent = File.ReadAllText(expectedPatchFull);
        if (!patchContent.Contains(DecryptSignature))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android PreBuild: 补丁源 MetadataLoader.cpp 不含解密特征 '{DecryptSignature}'，" +
                $"请检查 {expectedPatchFull}");
        }

        // 本地 IL2CPP 目录 (HybridCLR useGlobalIl2cpp=0 时使用)
        string localIl2CppDir = Path.GetFullPath(
            Path.Combine(Application.dataPath,
                $"../HybridCLRData/LocalIl2CppData-{Application.platform}/il2cpp"));

        if (!Directory.Exists(localIl2CppDir))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android PreBuild: HybridCLR 本地 IL2CPP 目录不存在，" +
                $"请先安装 HybridCLR。预期路径: {localIl2CppDir}");
        }

        // 只拷贝 Il2CppOutputProject/IL2CPP/ 下的 .cpp 文件，
        // 目标路径去掉 Il2CppOutputProject/IL2CPP/ 前缀后映射到本地 il2cpp 根
        string outputPrefixDir = Path.Combine(patchSourceDir,
            Il2CppOutputPrefix.Replace('/', Path.DirectorySeparatorChar));

        if (!Directory.Exists(outputPrefixDir))
        {
            throw new BuildFailedException(
                $"[EncryptGlobalMetadata] Android PreBuild: 补丁源子目录不存在: {outputPrefixDir}");
        }

        var cppFiles = new DirectoryInfo(outputPrefixDir).GetFiles("*.cpp", SearchOption.AllDirectories);
        int copied = 0;

        foreach (var file in cppFiles)
        {
            // 去掉 Il2CppOutputProject/IL2CPP/ 前缀，得到目标相对路径
            string relativePath = file.FullName
                .Substring(outputPrefixDir.Length)
                .TrimStart(Path.DirectorySeparatorChar, '/');

            string destPath = Path.Combine(localIl2CppDir, relativePath);
            string destDir = Path.GetDirectoryName(destPath);

            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            File.Copy(file.FullName, destPath, true);
            copied++;
        }

        Debug.Log($"[EncryptGlobalMetadata] Android PreBuild: 已覆盖本地 IL2CPP 解密补丁，" +
                  $"共 {copied} 个 .cpp 文件 → {localIl2CppDir}");
    }

    // -----------------------------------------------------------------------
    // 公共：查找 Android metadata 路径（从 src/main 开始搜索）
    // -----------------------------------------------------------------------

    private static string FindAndroidMetadataPath(string srcMainDir)
    {
        // 优先检查常见路径
        string preferred = Path.Combine(srcMainDir,
            "assets/bin/Data/Managed/Metadata/global-metadata.dat");
        if (File.Exists(preferred))
        {
            Debug.Log($"[EncryptGlobalMetadata] Android: 命中优先路径 {preferred}");
            return preferred;
        }

        // 回退：递归搜索，只取路径含 Managed/Metadata 的候选项
        var candidates = Directory.GetFiles(srcMainDir, "global-metadata.dat", SearchOption.AllDirectories)
            .Where(p => p.Replace('\\', '/').Contains("Managed/Metadata"))
            .ToList();

        if (candidates.Count == 1)
        {
            Debug.Log($"[EncryptGlobalMetadata] Android: 递归搜索命中 {candidates[0]}");
            return candidates[0];
        }

        if (candidates.Count > 1)
        {
            Debug.LogError($"[EncryptGlobalMetadata] Android: 找到多个候选项，无法确定唯一目标，中止加密：" +
                           $"\n{string.Join("\n", candidates)}");
            return null;
        }

        return null;
    }

    // -----------------------------------------------------------------------
    // iOS: 拷贝 il2cpp/**/*.cpp 补丁到 Xcode 工程根目录
    // -----------------------------------------------------------------------

    private static void CopyIl2CppCppFilesToXcode(string xcodeProjectPath)
    {
        string sourceDir = Path.GetFullPath(Application.dataPath + "/../il2cpp");
        if (!Directory.Exists(sourceDir))
        {
            Debug.LogWarning($"[EncryptGlobalMetadata] iOS: il2cpp 补丁目录不存在，跳过拷贝: {sourceDir}");
            return;
        }

        string targetDir = Path.GetFullPath(xcodeProjectPath);
        var cppFiles = new DirectoryInfo(sourceDir).GetFiles("*.cpp", SearchOption.AllDirectories);

        foreach (var file in cppFiles)
        {
            string destPath = file.FullName.Replace(sourceDir, targetDir);
            string destDir = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            File.Copy(file.FullName, destPath, true);
        }

        Debug.Log($"[EncryptGlobalMetadata] iOS: 拷贝 il2cpp .cpp 文件完成，共 {cppFiles.Length} 个");
    }

    // -----------------------------------------------------------------------
    // 公共：加密入口（仅校验存在与最小长度）
    // -----------------------------------------------------------------------

    /// <summary>
    /// 对 global-metadata.dat 就地加密。
    /// </summary>
    public static void TryEncodeMetaData(string fullFileName)
    {
        if (!File.Exists(fullFileName))
        {
            Debug.LogError($"[EncryptGlobalMetadata] 文件不存在: {fullFileName}");
            return;
        }

        var sourceBytes = File.ReadAllBytes(fullFileName);

        if (sourceBytes.Length <= SafeSizeBytes + 4 + CodeLen)
        {
            Debug.LogError($"[EncryptGlobalMetadata] 文件过小，无法加密: {fullFileName}（长度 {sourceBytes.Length}）");
            return;
        }

        EncodeMetaData(sourceBytes, fullFileName);
        Debug.Log($"[EncryptGlobalMetadata] 加密完成: {fullFileName}");
    }

    // -----------------------------------------------------------------------
    // 内部：核心加密算法
    // 布局：[前 1024 字节原文] [随机1] [codeLen=16] [随机1] [随机1] [16字节 key] [剩余字节 XOR key]
    // -----------------------------------------------------------------------

    private static void EncodeMetaData(byte[] sourceBytes, string outputPath)
    {
        var random = new Random();

        var code = new byte[CodeLen];
        for (var i = 0; i < CodeLen; i++)
            code[i] = (byte)random.Next(0, 255);

        var encodedData = new byte[sourceBytes.Length + CodeLen + 4];

        // 前 1024 字节原文
        Buffer.BlockCopy(sourceBytes, 0, encodedData, 0, SafeSizeBytes);

        // 偏移 1024：标记头（4 字节）
        encodedData[SafeSizeBytes] = (byte)random.Next(1, 255);
        encodedData[SafeSizeBytes + 1] = (byte)CodeLen;
        encodedData[SafeSizeBytes + 2] = (byte)random.Next(1, 255);
        encodedData[SafeSizeBytes + 3] = (byte)random.Next(1, 255);

        // 偏移 1028：16 字节 key
        for (var i = 0; i < CodeLen; i++)
            encodedData[SafeSizeBytes + 4 + i] = code[i];

        // 偏移 1044：剩余内容 XOR key
        int contentStart = SafeSizeBytes + 4 + CodeLen;
        for (var i = 0; i < sourceBytes.Length - SafeSizeBytes; i++)
            encodedData[contentStart + i] = (byte)(sourceBytes[i + SafeSizeBytes] ^ code[i % CodeLen]);

        File.WriteAllBytes(outputPath, encodedData);
    }
}
