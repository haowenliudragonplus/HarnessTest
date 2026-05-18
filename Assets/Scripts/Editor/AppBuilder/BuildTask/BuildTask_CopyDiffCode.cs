using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 拷贝差异代码
/// </summary>
public class BuildTask_CopyDiffCode : BuildTaskNode
{
    public BuildTask_CopyDiffCode(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public async override UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        string diffCodeDir = "Assets/Scripts/Main";
        IOUtils.DeleteDirectory(Path.Combine(diffCodeDir, "MyLaunch"));
        if (buildParam.isRelease)
        {
            string scrDir = Application.dataPath + "/../../DiffCode/MyLaunch";
            string destDir = diffCodeDir;
            IOUtils.CopyFolder(scrDir, destDir);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Log("拷贝差异代码完成");
        }
    }
}