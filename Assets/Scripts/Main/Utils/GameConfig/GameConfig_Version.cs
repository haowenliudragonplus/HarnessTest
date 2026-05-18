using System;
using System.IO;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using Framework;
using Main;
using Newtonsoft.Json;
using UnityEngine;
using YooAsset;

[Serializable]
public class GameConfig_Version
{
    public string version;
    public string versionCode;
    public string appVersion;
}

[Serializable]
public class GameConfig_VersionWrap
{
    public GameConfig_Version AndroidDebug;
    public GameConfig_Version IOSDebug;
    public GameConfig_Version AndroidRelease;
    public GameConfig_Version IOSRelease;
}

public partial class GameConfig
{
    private const string VERSION_CONFIG_NAME = "GameConfig_Version";

    public static async UniTask InitGameConfig_Version()
    {
        await LoadGameConfig_Version();
    }

    private static async UniTask LoadGameConfig_Version()
    {
        string filePath = Path.Combine(GAME_CONFIG_DIR, VERSION_CONFIG_NAME);
        var ta = await ResourceUtils.LoadResorceAsync<TextAsset>(filePath);
        if (ta == null)
            return;

        GameConfig_VersionWrap gameConfig_VersionWrap = JsonConvert.DeserializeObject<GameConfig_VersionWrap>(ta.text);
        GameConfig_Version gameConfig_Version;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameConfig_Version = ConfigurationController.Instance.version == VersionStatus.DEBUG
                ? gameConfig_VersionWrap.IOSDebug
                : gameConfig_VersionWrap.IOSRelease;
        }
        else
        {
            gameConfig_Version = ConfigurationController.Instance.version == VersionStatus.DEBUG
                ? gameConfig_VersionWrap.AndroidDebug
                : gameConfig_VersionWrap.AndroidRelease;
        }
        AddConfig("version", gameConfig_Version.version);
        AddConfig("versionCode", gameConfig_Version.versionCode);
        AddConfig("appVersion", gameConfig_Version.appVersion);

        CLog.Info($"获取本地GameConfig_Version成功\n" +
                  $"version：{gameConfig_Version.version}\n" +
                  $"versionCode：{gameConfig_Version.versionCode}\n" +
                  $"appVersion：{gameConfig_Version.appVersion}\n");
    }

    public static string Version
    {
        get
        {
            var appVersion = GetConfig("version");
            return appVersion;
        }
    }

    public static string VersionCode
    {
        get
        {
            var appVersionCode = GetConfig("versionCode");
            return appVersionCode;
        }
    }

    public static string AppVersion
    {
        get
        {
#if UNITY_EDITOR
            if (YooAssetPlayModeConfiguration.Ins.PlayMode == EPlayMode.HostPlayMode)
            {
                // 如果是Unity编辑器下并且运行模式为联网模式则取Yoo运行配置
                var appVersion = string.Empty;
                if (string.IsNullOrEmpty(YooAssetPlayModeConfiguration.Ins.AppVersion))
                    appVersion = GetConfig("appVersion");
                else
                    appVersion = YooAssetPlayModeConfiguration.Ins.AppVersion;
                return appVersion;
            }
            else
#endif
            {
                var appVersion = GetConfig("appVersion");
                return appVersion;
            }
        }
    }

    /// <summary>
    /// 当前资源版本
    /// </summary>
    public static string CurResVersion
    {
        get
        {
#if UNITY_EDITOR
            if (YooAssetPlayModeConfiguration.Ins.PlayMode == EPlayMode.HostPlayMode)
            {
                // 如果是Unity编辑器下并且运行模式为联网模式则取Yoo运行配置
                var resVersion = string.Empty;
                if (string.IsNullOrEmpty(YooAssetPlayModeConfiguration.Ins.ResVersion))
                    resVersion = PlayerPrefs.GetString("CurResVersion");
                else
                    resVersion = YooAssetPlayModeConfiguration.Ins.ResVersion;
                return resVersion;
            }
            else
#endif
            {
                return PlayerPrefs.GetString("CurResVersion");
            }
        }
        set
        {
            PlayerPrefs.SetString("CurResVersion", value);
        }
    }

    /// <summary>
    /// 上一次资源版本
    /// </summary>
    public static string LastResVersion
    {
        get
        {
            return PlayerPrefs.GetString("LastResVersion");
        }
        set
        {
            PlayerPrefs.SetString("LastResVersion", value);
        }
    }

    /// <summary>
    /// 底包资源版本（StreamingAsset中）
    /// </summary>
    public static string BuildInResVersion
    {
        get
        {
            return PlayerPrefs.GetString("BuildInResVersion");
        }
        set
        {
            PlayerPrefs.SetString("BuildInResVersion", value);
        }
    }
}