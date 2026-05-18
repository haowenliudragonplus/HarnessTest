using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 加密GlobalSetting
/// </summary>
public class BuildTask_EncryptGlobalSetting : BuildTaskNode
{
    public BuildTask_EncryptGlobalSetting(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;
        if (!buildParam.isRelease)
        {
            // debug包默认必须加上中文
            bool hasChinese = GlobalSetting.Ins.SupportLanguage.HasFlag(ELanguageType.ChineseSimplified);
            if (!hasChinese)
            {
                GlobalSetting.Ins.SupportLanguage |= ELanguageType.ChineseSimplified;
            }
            EditorUtility.SetDirty(GlobalSetting.Ins);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        GlobalSetting.EncryptAndSaveToFile();
        Log("加密GlobalSetting完成");
    }
}