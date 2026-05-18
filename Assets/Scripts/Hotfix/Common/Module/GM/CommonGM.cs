#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Text;
using DragonPlus.Core;
using DragonPlus.Haptics;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;
using TMPro;
using UnityEngine;

/// <summary>
/// 通用GM
/// </summary>
public class CommonGM : GMBase
{
    protected override void RegisterAllCommand()
    {
        #region 用户相关

        // 显示用户信息
        GetGMBuilder("用户/显示用户信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var storageCommon = Game.GetMod<ModStorage>().GetStorage<StorageCommon>();
                var StorageClientCommon = Game.GetMod<ModStorage>().GetStorage<StorageClientCommon>();
                ulong playerId = storageCommon.PlayerId;
                string infoStr = string.Empty;
                infoStr += $"游戏id（数字）:{playerId}\n";
                infoStr += $"游戏id（字符串）:{SDKUtil.Misc.PlayerIdToString(playerId)}\n";
                infoStr += $"Version：{GameConfig.Version}\n";
                infoStr += $"VersionCode：{GameConfig.VersionCode}\n";
                infoStr += $"AppVersion：{GameConfig.AppVersion}\n";
                infoStr += $"首次登录时App版本：{StorageClientCommon.UserData.FirstAppVersion}\n";
                infoStr += $"首次登录时Res版本：{StorageClientCommon.UserData.FirstResVersion}\n";
                infoStr += $"上一次登录时App版本：{StorageClientCommon.UserData.LastAppVersion}\n";
                infoStr += $"上一次登录时Res版本：{StorageClientCommon.UserData.LastResVersion}\n";
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "用户信息",
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                    alignmentType = TextAlignmentOptions.Left,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 清空完整存档
        GetGMBuilder("用户/清空完整存档")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                Game.GetMod<ModStorage>().ClearServerStorage();
                Game.GetMod<ModStorage>().ClearLocalStorage();
                Game.Quit();
            })
            .SetTipStr("清空完整存档后，游戏将会退出")
            .Register();

        // 加道具
        GetGMBuilder("用户/加道具")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                if (!int.TryParse(s1, out int _itemType))
                    return;
                if (!int.TryParse(s2, out int _count))
                    return;
                if (_itemType <= 0)
                    return;
                Game.GetMod<ModBag>().AddItem((EItemType)_itemType, _count,
                    new BIHelper.ItemChangeReasonArgs()
                    {
                        reason = BiEventArrowPuzzle1.Types.ItemChangeReason.Debug,
                        data1 = _itemType.ToString(),
                        data2 = _count.ToString(),
                    });
            })
            .SetTipStr("参数1：道具ID\n参数2：道具数量")
            .SetCloseViewAfterExecute()
            .Register();

        // 设置debug购买
        GetGMBuilder("用户/设置Debug购买", EGMCommandType.Toggle)
            .SetOnToggleChanged((b) => { Game.GetMod<ModIAP>().InDebugPayMode = b; })
            .SetGetToggleInitStateEvent(() => Game.GetMod<ModIAP>().InDebugPayMode)
            .Register();

        // 设置debug RV
        GetGMBuilder("用户/设置Debug RV", EGMCommandType.Toggle)
            .SetOnToggleChanged((b) => { Game.GetMod<AdSys>().InDebugRVMode = b; })
            .SetGetToggleInitStateEvent(() => Game.GetMod<AdSys>().InDebugRVMode)
            .SetTipStr("开启后，所有激励视频均可跳过\n在编辑器下默认都是开启状态")
            .Register();

        #endregion 用户相关

        #region 设备相关

        // 显示设备信息
        GetGMBuilder("设备/显示设备信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                string infoStr = string.Empty;
                infoStr += $"网络是否连接：{Main.GameUtils.HasNetwork()}\n";
                infoStr += $"系统语言：{Application.systemLanguage}\n";
                infoStr += $"设备型号：{SystemInfo.deviceModel}\n";
                infoStr += $"自定义设备名称：{SystemInfo.deviceName}\n";
                infoStr += $"设备类型：{SystemInfo.deviceType}\n";
                infoStr += $"设备唯一标识：{SystemInfo.deviceUniqueIdentifier}\n";
                infoStr += $"系统运行内存（MB）：{SystemInfo.systemMemorySize}\n";
                infoStr += $"操作系统：{SystemInfo.operatingSystem}\n";
                infoStr += $"操作系统类型：{SystemInfo.operatingSystemFamily}\n";
                infoStr += $"处理器类型：{SystemInfo.processorType}\n";
                infoStr += $"处理器核数：{SystemInfo.processorCount}\n";
                infoStr += $"处理器频率（MHz）：{SystemInfo.processorFrequency}\n";
                infoStr += $"显卡设备ID：{SystemInfo.graphicsDeviceID}\n";
                infoStr += $"显卡名称:{SystemInfo.graphicsDeviceName}\n";
                infoStr += $"显卡类型：{SystemInfo.graphicsDeviceType}\n";
                infoStr += $"显卡供应商：{SystemInfo.graphicsDeviceVendor}\n";
                infoStr += $"显卡供应商ID：{SystemInfo.graphicsDeviceVendorID}\n";
                infoStr += $"显卡版本：{SystemInfo.graphicsDeviceVersion}\n";
                infoStr += $"显卡内存（MB）：{SystemInfo.graphicsMemorySize}\n";
                infoStr += $"电池电量：{SystemInfo.batteryLevel}\n";
                infoStr += $"电池状态：{SystemInfo.batteryStatus}\n";
                infoStr += $"支持的渲染目标数量：{SystemInfo.supportedRenderTargetCount}\n";
                infoStr += $"支持的最大图片尺寸：{SystemInfo.maxTextureSize}\n";
                infoStr += $"是否支持多线程渲染：{SystemInfo.graphicsMultiThreaded}\n";
                infoStr += $"是否支持震动反馈：{SystemInfo.supportsVibration}\n";
                infoStr += $"是否支持陀螺仪：{SystemInfo.supportsGyroscope}\n";
                infoStr += $"是否支持位置服务：{SystemInfo.supportsLocationService}\n";
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "设备信息",
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                    fontSize = 25,
                    alignmentType = TextAlignmentOptions.Left,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 显示App信息
        GetGMBuilder("设备/显示App信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                string infoStr = string.Empty;
                infoStr += $"Unity版本：{Application.unityVersion}\n";
                infoStr += $"是否为Debug版本：{Main.GameUtils.IsDevelopmentEnv()}\n";
                infoStr += $"当前平台：{Application.platform}\n";
                infoStr += $"分辨率：{Screen.currentResolution}\n";
                infoStr += $"DPI：{Screen.dpi}\n";
                infoStr += $"屏幕旋转类型：{Screen.orientation}\n";
                infoStr += $"性能质量类型：{QualitySettings.names[QualitySettings.GetQualityLevel()]}（{QualitySettings.GetQualityLevel()}）\n";
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "App信息",
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                    fontSize = 25,
                    alignmentType = TextAlignmentOptions.Left,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        #endregion 设备相关

        #region 分层相关

        // 显示用户分层信息
        GetGMBuilder("分层/显示用户分层信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                ulong playerId = SDK<IStorage>.Instance.Get<StorageCommon>().PlayerId;
                string infoStr = string.Empty;
                infoStr += $"当前AD总用户分组：{Game.GetMod<AdSys>().GetCurGroup()}\n";
                infoStr += $"当前RewardAD用户分组：{Game.GetMod<AdSys>().GetAdRewardCurrentGroup()}\n";
                infoStr += $"当前InterstitialAD用户分组：{Game.GetMod<AdSys>().GetAdInterstitialCurrentGroup()}\n";
                infoStr += $"当前BannerAD用户分组：{Game.GetMod<AdSys>().GetBannerCurrentGroup()}\n";
                infoStr += $"当前IAP总用户分组：{Game.GetMod<ModIAP>().GetCurGroup()}\n";
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "用户分层信息",
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        #endregion 分层相关

        #region ABTest相关

        // 显示ABTest信息
        GetGMBuilder("ABTest/显示ABTest信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var abTestDict = SDK<IStorage>.Instance.Get<StorageCommon>().Abtests;
                string infoStr = string.Empty;
                foreach (var kvp in abTestDict)
                {
                    infoStr += $"分组key：{kvp.Key}，分组值：{kvp.Value}\n";
                }
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "ABTest信息",
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                    fontSize = 30,
                    alignmentType = TextAlignmentOptions.Left,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 设置客户端ABTest分组（完整key）
        GetGMBuilder("ABTest/设置客户端ABTest分组\n（完整key）")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var commonStorage = Game.GetMod<ModStorage>().GetStorage<StorageCommon>();
                if (!int.TryParse(s2, out int _abTestGroup))
                    return;
                if (commonStorage.Abtests.ContainsKey(s1))
                {
                    commonStorage.Abtests[s1] = _abTestGroup.ToString();
                }
            })
            .SetTipStr("参数1：ABTest分组完整key\n参数2：ABTest分组值")
            .Register();

        // 设置客户端ABTest分组（类型）
        GetGMBuilder("ABTest/设置客户端ABTest分组\n（类型）")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var commonStorage = Game.GetMod<ModStorage>().GetStorage<StorageCommon>();
                if (!int.TryParse(s1, out int _abTestType))
                    return;
                if (!int.TryParse(s2, out int _abTestGroup))
                    return;
                Game.GetMod<ModABTest>().FixedABTestGroup((EABTestType)_abTestType, (EABTestGroup)_abTestGroup, true);
            })
            .SetTipStr("参数1：ABTest类型\n参数2：ABTest分组值")
            .Register();

        #endregion ABTest相关

        #region 游戏相关

        // 设置时间缩放
        GetGMBuilder("游戏/设置时间缩放")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                if (!float.TryParse(s1, out float _timeScale))
                    return;
                Time.timeScale = _timeScale;
            })
            .SetTipStr("参数1：时间缩放值（默认1）")
            .Register();

        // 测试震动
        GetGMBuilder("游戏/测试震动")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                if (!float.TryParse(s1, out float _type))
                    return;
                try
                {
                    SDK<IHaptics>.Instance.Haptics((HapticTypes)_type);
                }
                catch (Exception e)
                {
                }
            })
            .SetTipStr("Selection=0\nSuccess,\nWarning\nFailure\nLight\nMedium\nHeavy\nNone")
            .Register();

        #endregion 游戏相关

        #region 检查工具相关

        // 检查多语言value为null
        GetGMBuilder("检查工具/检查多语言value为null")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var languageConfigListDict = Game.GetMod<ModLanguage>().GetAllLanguageConfigListDict();
                if (languageConfigListDict == null)
                    return;
                StringBuilder sb = new StringBuilder();
                foreach (var kvp in languageConfigListDict)
                {
                    string language = kvp.Key;
                    foreach (var config in kvp.Value)
                    {
                        if (config.key == "公式" || config.key == "Title")
                            continue;
                        if (!string.IsNullOrEmpty(config.value))
                            continue;
                        sb.AppendLine($"[{language}] key：{config.key}");
                    }
                }

                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "检查多语言value为null",
                    showMidBtn = true,
                    content = sb.ToString(),
                    enableScroll = true,
                    fontSize = 25,
                    alignmentType = TextAlignmentOptions.Left,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 检查多语言重复key
        GetGMBuilder("检查工具/检查多语言重复key")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var languageConfigListDict = Game.GetMod<ModLanguage>().GetAllLanguageConfigListDict();
                if (languageConfigListDict == null)
                    return;
                StringBuilder sb = new StringBuilder();
                foreach (var kvp in languageConfigListDict)
                {
                    string language = kvp.Key;
                    List<string> tempKeyList = new List<string>();
                    foreach (var config in kvp.Value)
                    {
                        if (tempKeyList.Contains(config.key))
                        {
                            sb.AppendLine($"[{language}] key：{config.key}");
                            continue;
                        }
                        tempKeyList.Add(config.key);
                    }
                }

                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "检查多语言重复key结果",
                    showMidBtn = true,
                    content = sb.ToString(),
                    enableScroll = true,
                    fontSize = 25,
                    alignmentType = TextAlignmentOptions.Left,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 检查丢失字符
        GetGMBuilder("检查工具/检查丢失字符")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var languageConfigListDict = Game.GetMod<ModLanguage>().GetAllLanguageConfigListDict();
                StringBuilder sb = new StringBuilder();
                var supportLanguageTypeList = Game.GetMod<ModLanguage>().GetSupportLanguageTypeList();
                foreach (var languageType in supportLanguageTypeList)
                {
                    var tmpFontAsset = Game.GetMod<ModLanguage>().GetTMPFontAsset(languageType.ToDes());
                    if (tmpFontAsset == null)
                        continue;
                    if (!languageConfigListDict.TryGetValue(languageType.ToDes(), out var _configList))
                        continue;
                    foreach (var config in _configList)
                    {
                        if (config.key == "Custom") //过滤掉自定义的中文
                            continue;
                        if (string.IsNullOrEmpty(config.value))
                            continue;
                        foreach (var c in config.value)
                        {
                            if (tmpFontAsset.HasCharacter(c))
                                continue;
                            sb.AppendLine($"[{languageType.ToDes()}] key：{config.key}，char：{c}");
                        }
                    }
                }

                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "检查丢失字符结果",
                    showMidBtn = true,
                    content = sb.ToString(),
                    enableScroll = true,
                    fontSize = 25,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        #endregion 检查工具相关

        GetGMBuilder("显示MediationDebugger")
            .SetOnButtonClick((s1, s2, s3) => { MaxSdk.ShowMediationDebugger(); })
            .SetCloseViewAfterExecute()
            .Register();

        GetGMBuilder("隐藏GM入口")
            .SetOnButtonClick((s1, s2, s3) => { Game.GetMod<ModGM>().OnDispose(); })
            .SetCloseViewAfterExecute()
            .Register();
    }
}

#endif