using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// 构建APK
/// </summary>
public class BuildTask_BuildApk : BuildTaskNode
{
    public BuildTask_BuildApk(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
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
        string fileName = PlayerSettings.productName + ".apk";

        string androidOutputDir = Application.dataPath + "/../AndroidExport";
        if (Directory.Exists(androidOutputDir))
        {
            Directory.Delete(androidOutputDir, true);
        }
        Directory.CreateDirectory(androidOutputDir);
        string androidOutputPath = androidOutputDir + $"/{fileName}";
        Log($"构建安卓的路径：{androidOutputPath}");

        Log($"开始打包APK");
        StringBuilder sb = new StringBuilder();
        BuildReport buildReport = BuildPipeline.BuildPlayer(levelList.ToArray(), androidOutputPath, BuildTarget.Android,
            !buildParam.isRelease ? BuildOptions.Development : BuildOptions.None);
        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            sb.AppendLine("----------构建中每一步的耗时----------");
            foreach (var step in buildReport.steps)
            {
                if (step.duration.TotalSeconds < 1)
                    continue;
                sb.AppendLine($"[Depth：{step.depth}]，[Step：{step.name}] : {step.duration.TotalSeconds:F2}秒");
            }
            sb.AppendLine("----------------------------------");
        }
        Log($"打包APK完成：{buildReport.summary.result.ToString()}\n{sb.ToString()}");
    }
}