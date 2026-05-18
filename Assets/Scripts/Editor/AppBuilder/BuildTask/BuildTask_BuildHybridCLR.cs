using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using HybridCLR.Editor;
using HybridCLR.Editor.AOT;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Settings;
using UnityEditor;

/// <summary>
/// 构建HybridCLR
/// </summary>
public class BuildTask_BuildHybridCLR : BuildTaskNode
{
    public const string PatchedAOTAssemblyBinaryFilePath = "Assets/Resources/PatchedAOT"; //补充元数据dll的文件路径
    private const string HotfixAssemblyBinaryFilePath = "Assets/Res/Code"; //热更dll的文件路径

    private bool isBuildAssetBundle; //是否为构建ab包

    public BuildTask_BuildHybridCLR(bool isBuildAssetBundle, string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
        this.isBuildAssetBundle = isBuildAssetBundle;
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        // 检查HybridCLR是否安装
        var installer = new HybridCLR.Editor.Installer.InstallerController();
        if (!installer.HasInstalledHybridCLR())
        {
            installer.InstallDefaultHybridCLR();
            AssetDatabase.Refresh();
        }
        // 设置HybridCLR配置
        HybridCLRSettings.Instance.hotUpdateAssemblies = new string[GlobalSetting.Ins.HotfixAssemblyList.Length];
        for (int i = 0; i < GlobalSetting.Ins.HotfixAssemblyList.Length; i++)
        {
            HybridCLRSettings.Instance.hotUpdateAssemblies[i] = GlobalSetting.Ins.HotfixAssemblyList[i].Replace(".dll", "");
        }
        HybridCLRSettings.Instance.externalHotUpdateAssembliyDirs = GlobalSetting.Ins.ExternalHotfixAssemblyDirList;
        HybridCLRSettings.Save();
        // 构建HybridCLR
        if (isBuildAssetBundle)
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target, !buildParam.isRelease);
        }
        else
        {
            PrebuildCommand.GenerateAll();
        }
        // 将程序集拷贝到项目中
        CopyAssemblyToRightPath(isBuildAssetBundle);

        Log("构建HybridCLR完成");
    }

    /// <summary>
    /// 将程序集拷贝到项目中
    /// </summary>
    private static void CopyAssemblyToRightPath(bool onlyHotfixAssembly)
    {
        // 有源码的热更dll输出路径
        var hotUpdateOutputDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(EditorUserBuildSettings.activeBuildTarget);
        // 无源码的热更dll路径
        var externalDirs = HybridCLRSettings.Instance.externalHotUpdateAssembliyDirs ?? System.Array.Empty<string>();

        if (!Directory.Exists(HotfixAssemblyBinaryFilePath))
        {
            Directory.CreateDirectory(HotfixAssemblyBinaryFilePath);
        }

        // 处理所有热更DLL（无论是源码编译出来的，还是外部DLL）
        for (var i = 0; i < GlobalSetting.Ins.HotfixAssemblyList.Length; i++)
        {
            var dllName = GlobalSetting.Ins.HotfixAssemblyList[i];
            // 先从编译输出目录找
            var sourceFile = Path.Combine(hotUpdateOutputDir, dllName);
            if (!File.Exists(sourceFile))
            {
                // 如果编译输出目录没有，再从externalHotUpdateAssembliyDirs中配置的目录里找(纯DLL情况)
                foreach (var dir in externalDirs)
                {
                    if (string.IsNullOrEmpty(dir))
                        continue;

                    sourceFile = Path.Combine(dir, dllName);
                    if (File.Exists(sourceFile))
                        break;
                }
            }
            if (string.IsNullOrEmpty(sourceFile) || !File.Exists(sourceFile))
                continue;

            var dest = Path.Combine(HotfixAssemblyBinaryFilePath, dllName + ".bytes");
            File.Copy(sourceFile, dest, true);
        }

        if (!onlyHotfixAssembly)
        {
            var aotSourceDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(EditorUserBuildSettings.activeBuildTarget);
            string aotOptimizeDir = $"{SettingsUtil.HybridCLRDataDir}/StrippedAOTAssembly_Optimize/{EditorUserBuildSettings.activeBuildTarget}";
            for (var i = 0; i < GlobalSetting.Ins.PatchedAOTAssemblyList.Length; i++)
            {
                var sourceFile = Path.Combine(aotSourceDir, GlobalSetting.Ins.PatchedAOTAssemblyList[i]);
                if (!File.Exists(sourceFile))
                    continue;
                // 剔除优化工作
                string aotOptimizePath = Path.Combine(aotOptimizeDir, GlobalSetting.Ins.PatchedAOTAssemblyList[i]);
                AOTAssemblyMetadataStripper.Strip(sourceFile, aotOptimizePath);
                // 将剔除优化后的dll拷贝到目标文件夹
                if (!Directory.Exists(PatchedAOTAssemblyBinaryFilePath))
                {
                    Directory.CreateDirectory(PatchedAOTAssemblyBinaryFilePath);
                }
                var dest = Path.Combine(PatchedAOTAssemblyBinaryFilePath, GlobalSetting.Ins.PatchedAOTAssemblyList[i] + ".bytes");
                File.Copy(aotOptimizePath, dest, true);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}