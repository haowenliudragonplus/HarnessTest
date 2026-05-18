using System;
using System.Collections.Generic;
using DragonPlus.Core;
using DragonPlus.Haptics;
using DragonU3DSDK.Network.API;
using Framework;
using TMGame;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Game
{
    public static GameObject DontDestoryRoot;

    private readonly static Dictionary<Type, ModuleBase> moduleDict = new(); //Module字典

    public static bool GameInited { get; set; } //游戏是否初始化完成

    /// <summary>
    /// 热更域App主入口。
    /// </summary>
    public static void Entrance()
    {
        //try
        {
            // 永不删除的空节点
            DontDestoryRoot = new GameObject("DontDestroyRoot");
            Object.DontDestroyOnLoad(DontDestoryRoot);

            RegisterModule();

            GetMod<ModFsm>().AddState<FsmState_Login>();
            GetMod<ModFsm>().AddState<FsmState_Home>();
            GetMod<ModFsm>().AddState<FsmState_InGame>();
            GetMod<ModFsm>().StartFsm();
        }
        // catch (Exception e)
        // {
        //     Debug.LogError(e.Message);
        // }
    }

    /// <summary>
    /// 注册所有模块（手动控制Module的注册顺序）
    /// </summary>
    /// 带//的表示已经整理完善过
    /// 下面是整理好的，不必要不用修改！！！！！
    private static void RegisterModule()
    {
        RegisterModule<ModEvent>();//
        RegisterModule<ModAccount>();//
        RegisterModule<ModStorage>();//
        RegisterModule<ModAsset>();//
        RegisterModule<ModConfig>();//
        RegisterModule<ModTimer>();//
        RegisterModule<ModCoroutine>();//
        RegisterModule<ModABTest>();//

        RegisterModule<ModIAP>();
        RegisterModule<AdSys>();

        RegisterModule<ModFsm>();//
        RegisterModule<ModLanguage>();//
        RegisterModule<ModBag>();//
        RegisterModule<ModCamera>();//
        RegisterModule<ModPopup>();
        RegisterModule<ModTip>();//
        RegisterModule<ModFly>();
        RegisterModule<ModAudio>();//
        RegisterModule<ModFunctionUnlock>();//
        RegisterModule<ModFunctionJump>();//
        //RegisterModule<FaqSys>();//
        //RegisterModule<ContactUsLogic>();//
        RegisterModule<ActivitySys>();
        RegisterModule<ModUI>();//
        RegisterModule<GuideSys>();

        RegisterModule_UnCommon();
        RegisterModule_Activity();

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        RegisterModule<ModGM>();//
#endif
    }

    /// <summary>
    /// 注册额外Module（不是通用的模块注册在这里）
    /// </summary>
    private static void RegisterModule_UnCommon()
    {
        RegisterModule<FlySys>();
        RegisterModule<ModInGame>();
        RegisterModule<ModAchievement>();
    }

    private static void RegisterModule_Activity()
    {
        RegisterModule<ModActivity_RemoveAd>();
    }

    #region 获取Module

    public static T GetMod<T>()
        where T : ModuleBase
    {
        if (moduleDict.TryGetValue(typeof(T), out var module))
        {
            return (T)module;
        }
        return null;
    }

    #endregion 获取Module

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// 返回登录界面
    /// </summary>
    public static void BackToLogin()
    {
        SDKUtil.Unity.StopAllCoroutines();
        GetMod<ModUI>().CloseAll(true);
        Dispose();

        FsmState_Login.EnterParam stateData = new FsmState_Login.EnterParam()
        {
            reLogin = true,
        };
        GetMod<ModFsm>().ChangeState<FsmState_Login>(false, stateData);
    }

    #region 注册Module

    /// <summary>
    /// 注册Module
    /// </summary>
    private static void RegisterModule<T>()
        where T : ModuleBase, new()
    {
        if (moduleDict.ContainsKey(typeof(T)))
        {
            CLog.Error($"Module已存在，不能重复注册，Module：{typeof(T)}");
            return;
        }

        var c = new T();
        c.OnInit();
        moduleDict.Add(typeof(T), c);
    }

    #endregion 注册Module

    #region 生命周期

    public static void Start()
    {
        foreach (var module in moduleDict.Values)
        {
            module?.OnStart();
        }

        Loader.OnFixedUpdate += OnFixedUpdate;
        Loader.OnUpdate += OnUpdate;
        Loader.OnLateUpdate += OnLateUpdate;
        Loader.OnApplicatePause += OnApplicationPause;
        Loader.OnApplicateQuit += OnApplicationQuit;
    }

    private static void OnFixedUpdate()
    {
        if (!GameInited)
            return;

        float fdt = Time.fixedDeltaTime;
        foreach (var module in moduleDict.Values)
        {
            module?.FixedUpdate(fdt);
        }
    }

    private static void OnUpdate()
    {
        if (!GameInited)
            return;

        GameObjectPool.OnUpdate();
        ReferencePool.OnUpdate();

        float dt = Time.deltaTime;
        foreach (var module in moduleDict.Values)
        {
            module?.Update(dt);
        }
    }

    private static void OnLateUpdate()
    {
        if (!GameInited)
            return;

        float dt = Time.deltaTime;
        foreach (var module in moduleDict.Values)
        {
            module?.LateUpdate(dt);
        }
    }

    private static void OnApplicationPause(bool isPause)
    {
        Game.GetMod<ModEvent>().Dispatch(new EvtOnApplicationPause(isPause));

        if (isPause)
            Game.GetMod<ModStorage>().ForceSaveToLocal();
    }

    public static void OnLoginSuccess()
    {
        foreach (var module in moduleDict.Values)
        {
            module?.OnLoginSuccess();
        }
    }

    private static void OnApplicationQuit()
    {
        Game.GetMod<ModStorage>().ForceSaveToLocal();
    }

    public static void Dispose()
    {
        Loader.OnFixedUpdate -= OnFixedUpdate;
        Loader.OnUpdate -= OnUpdate;
        Loader.OnLateUpdate -= OnLateUpdate;
        Loader.OnApplicatePause -= OnApplicationPause;
        Loader.OnApplicateQuit -= OnApplicationQuit;

        foreach (var module in moduleDict.Values)
        {
            module?.OnDispose();
        }
    }

    #endregion 生命周期
}