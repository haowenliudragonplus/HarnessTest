using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 拷贝差异文件到StreamingAsset下
/// </summary>
public class BuildTask_CopyDiffFile : BuildTaskNode
{
    public BuildTask_CopyDiffFile(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public async override UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        IOUtils.DeleteDirectory(Path.Combine(Application.streamingAssetsPath, Path.GetFileName(DiffFileGenerator.DiffFileGenerator.DiffFileDir)));
        if (buildParam.isRelease)
        {
            string scrDir = DiffFileGenerator.DiffFileGenerator.DiffFileDir;
            string destDir = Application.streamingAssetsPath;
            IOUtils.CopyFolder(scrDir, destDir);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Log("拷贝差异文件到StreamingAsset下完成");
        }
    }
}