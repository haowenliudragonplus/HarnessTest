using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using DragonPlus.Account;
using DragonPlus.Ad;
using DragonPlus.Ad.Max;
using DragonPlus.Config;
using DragonPlus.Core;
using DragonPlus.GooglePlay;
using DragonPlus.Haptics;
using DragonPlus.InAppPurchasing;
using DragonPlus.Native.Bridge;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonPlus.Tracking;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Launcher;
using UnityEngine;

/// <summary>
/// 游戏初始化
/// </summary>
public class ProcedureInitialize : Procedure
{
    public ProcedureInitialize(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        UIView_Boot.Ins.SetViewState(EBootViewState.None);

        // 初始化log系统
        InitLogService();
        // 初始化Debugger
        InitDebugger();

        // 初始化DragonSDK
        await InitDragonSDK();

        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFteLaunchApp);

        // 初始化启动时多语言配置
        await GameConfig.InitGameConfig_Locale();

        // 检查隐私协议
        await CheckPrivacyPolicy();

        // 设置游戏性能质量
        MatchQuality();

        UIView_Boot.Ins.SetViewState(EBootViewState.Common);

        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFteEnterLoginScreen);

        CLog.Info("----------ProcedureInitialize Success", logColor: ELogColor.Cyan);
    }

    private void InitDebugger()
    {
#if DEBUG || DEVELOPMENT_BUILD
        Debugger.Enable();
#endif
    }

    private void InitLogService()
    {
        // 自定义log
        bool logEnable = true;
        ELogType logTypeMask;
        ELogTag logTagMask;

#if UNITY_EDITOR
        logEnable = GlobalSetting.Ins.LogEnable;
        logTypeMask = GlobalSetting.Ins.LogTypeMask;
        logTagMask = GlobalSetting.Ins.LogTagMask;
        LogService.Add(new FileLog());
#else
        if (Debug.isDebugBuild)
        {
            logEnable = true;
            logTypeMask = (ELogType)~0;
            logTagMask = (ELogTag)~0;
            LogService.Add(new FileLog());
        }
        else
        {
            logEnable = false;
            logTypeMask = (ELogType)~0;
            logTagMask = (ELogTag)~0;
        }
#endif

        LogService.Init(logEnable, logTypeMask, logTagMask);
    }

    private void MatchQuality()
    {
        var useHighQuality = true;
        if (Application.platform == RuntimePlatform.Android)
        {
            if ((SystemInfo.processorFrequency != 0 && SystemInfo.processorFrequency < 1500) || // CPU低于1.5GH
                (SystemInfo.systemMemorySize != 0 && SystemInfo.systemMemorySize < 2048) // 或者内存低于2G
                || (SystemInfo.processorCount != 0 && SystemInfo.processorCount <= 4)
                || SystemInfo.graphicsMemorySize < 512) // 或者图形内存小于512MB
            {
                useHighQuality = false;
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer) //苹果设备不能获取cpu频率，这里通过GPU型号和内存判断
        {
            //https://zh.wikipedia.org/wiki/IOS%E5%92%8CiPadOS%E8%AE%BE%E5%A4%87%E5%88%97%E8%A1%A8
#if UNITY_IOS
                var g = UnityEngine.iOS.Device.generation;
                if (g == UnityEngine.iOS.DeviceGeneration.iPhone4 ||// <1G iphone
                    g == UnityEngine.iOS.DeviceGeneration.iPhone4S ||
                    g == UnityEngine.iOS.DeviceGeneration.iPhone5 ||
                    g == UnityEngine.iOS.DeviceGeneration.iPhone5S ||
                    g == UnityEngine.iOS.DeviceGeneration.iPhone5C ||
                    g == UnityEngine.iOS.DeviceGeneration.iPhone6 ||
                    g == UnityEngine.iOS.DeviceGeneration.iPhone6Plus ||
                    g == UnityEngine.iOS.DeviceGeneration.iPad1Gen ||// <1G ipad
                    g == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPad2Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPad3Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPad4Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPadAir1 ||// <1G ipadAir
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch1Gen ||// <1G ipod
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch2Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch3Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch4Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch5Gen ||
                    g == UnityEngine.iOS.DeviceGeneration.iPodTouch6Gen)
                {
                    useHighQuality = false;
                }
#endif
        }

        QualitySettings.SetQualityLevel(useHighQuality ? 1 : 0);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = useHighQuality ? 60 : 30;
    }

    private async UniTask InitDragonSDK()
    {
        SDK.Install<NativeModule>();
        SDK.Install<StorageModule>();
        SDK.Install<ConfigHubModule>();
        SDK.Install<AccountModule>();
        SDK.Install<NetworkModule>();
        SDK.Install<GooglePlayModule>();
        SDK.Install<TrackingModule>();
        SDK.Install<MaxTrackingModule>();

        SDK.Install<HapticsModule>();
        SDK.Install<IAPModule>();
        SDK.Install<MaxModule>();

        await SDK.Instance.OnGameLaunch();
    }

    private async UniTask CheckPrivacyPolicy()
    {
        await UIView_PrivacyPolicy.CheckShow();
    }
}