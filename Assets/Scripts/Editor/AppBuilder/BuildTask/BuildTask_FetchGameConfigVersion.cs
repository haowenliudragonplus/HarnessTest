using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 获取游戏版本信息
/// </summary>
public class BuildTask_FetchGameConfigVersion : BuildTaskNode
{
    private static string GameConfigVersionFilePath = Application.dataPath + "/Resources/GameConfig/GameConfig_Version.txt"; //游戏版本配置文件

    public BuildTask_FetchGameConfigVersion(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        GameConfigVersionFilePath = IOUtils.ReconstructPath(GameConfigVersionFilePath);
        string content = File.ReadAllText(GameConfigVersionFilePath);
        GameConfig_VersionWrap gameConfig_VersionWarp = JsonUtility.FromJson<GameConfig_VersionWrap>(content);
        buildParam.GameConfigVersion = buildParam.isIos
            ? (buildParam.isRelease ? gameConfig_VersionWarp.IOSRelease : gameConfig_VersionWarp.IOSDebug)
            : (buildParam.isRelease ? gameConfig_VersionWarp.AndroidRelease : gameConfig_VersionWarp.AndroidDebug);
        Log("获取游戏版本信息成功");
    }
}