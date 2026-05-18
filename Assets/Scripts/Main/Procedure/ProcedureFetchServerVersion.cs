using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Launcher;
using Main;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 获取服务器上的版本信息
/// </summary>
public class ProcedureFetchServerVersion : Procedure
{
    public ProcedureFetchServerVersion(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        // 读取本地版本文件
        await GameConfig.InitGameConfig_Version();
        // 读取远端版本文件
        await FetchServerVersion();
    }

    private async UniTask FetchServerVersion()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CLog.Error($"ProcedureFetchServerVersion failed，请检查网络连接");
            return;
        }

        var playerId = "0";
        var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
        if (storageCommon != null)
        {
            playerId = storageCommon.PlayerId.ToString();
        }
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventCheckUpdateStart);
        var url = $@"{ConfigurationController.Instance.APIServerURL}/version?id={playerId}&code={GameConfig.AppVersion}&platform={SDKUtil.Device.GetPlatform()}";
        CLog.Info($"versionServerUrl url:{url}");
        UnityWebRequest request;
        try
        {
            request = await HttpUtils.Get(url);
            if (request.result == UnityWebRequest.Result.Success)
            {
                var content = request.downloadHandler.text;
                CLog.Info($"获取到远端版本信息，content：{content}");
                if (string.IsNullOrEmpty(content))
                    return;
                var versionInfoDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                if (versionInfoDict != null && versionInfoDict.Keys.Count > 0)
                {
                    var appMinVersion = "";
                    var resMinVersion = "";
#if UNITY_ANDROID
                    if (versionInfoDict.TryGetValue("client-android-version", out var _appMinVersion))
                    {
                        appMinVersion = _appMinVersion;
                    }
#else
                    if (versionInfoDict.TryGetValue("client-ios-version", out var _appMinVersion))
                    {
                        appMinVersion = _appMinVersion;
                    }
#endif
                    CLog.Info($"获取到远端版本信息：\n" +
                              $"appMinVersion：{appMinVersion}");

                    // 检查app版本（检查强更）
                    BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventCheckUpdateFinish);
                    int.TryParse(appMinVersion.Replace("v", ""), out int appMinVersionNum);
                    int.TryParse(GameConfig.AppVersion.Replace("v", ""), out int appVersion);
                    if (appMinVersionNum > appVersion)
                    {
                        // 弹强更
                        UIView_MessageBox.Ins.ShowMessageBox(GameConfig.GetLocaleStr("UI_update_app_desc_text"),
                            showRightBtn: false, leftBtnName: GameConfig.GetLocaleStr("UI_update_app_btn_update"), closeSelf: false,
                            onLeftBtn: () =>
                            {
                                BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventCheckUpdateOpenStore);
                                var url = ConfigurationController.Instance.UPDATE_URL;
                                GameUtils.OpenURL(url);
                            });
                        while (true)
                        {
                            await UniTask.Yield();
                        }
                    }
                    CLog.Info($"----------ProcedureFetchServerVersion Success\n本地缓存的资源版本：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
                }

                request.Abort();
                request.Dispose();
            }
            else
            {
                BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventCheckUpdateFailure);
                CLog.Error($"获取远端版本信息失败，检查网络连接：{request.error}");
            }
        }
        catch (Exception e)
        {
            CLog.Error($"获取远端版本信息失败，检查网络连接：{e}");
        }
    }
}