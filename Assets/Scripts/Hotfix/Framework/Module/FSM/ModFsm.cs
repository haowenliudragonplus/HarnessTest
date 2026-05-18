using System;
using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;

public class ModFsm : ModuleBase
{
    private Dictionary<Type, FsmStateBase> stateDict = new Dictionary<Type, FsmStateBase>();

    public FsmStateBase DefaultState { get; private set; } //默认状态
    public FsmStateBase CurState { get; private set; } //当前状态
    public FsmStateBase PreState { get; private set; } //上一个状态

    protected bool isBoot;
    protected bool isPause;

    public void SetDefaultState<T>()
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (!stateDict.TryGetValue(type, out var _state))
        {
            CLog.Error($"找不到此state，先添加state：{type.FullName}");
            return;
        }
        DefaultState = _state;
        CurState = _state;
    }

    public void StartFsm(FsmStateEnterParam enterParam = null)
    {
        if (isBoot)
            return;
        isBoot = true;
        if (CurState == null)
        {
            CLog.Error($"至少添加一个状态才能启动状态机");
            return;
        }
        ChangeState(CurState.GetType(), true, enterParam);
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    public void AddState<T>(FsmStateInitParam initParam = null)
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (stateDict.ContainsKey(type))
        {
            CLog.Error($"状态已存在，不能重复添加状态，state：{type}");
            return;
        }

        var state = new T();

        // 默认第一个添加的状态为初始状态
        if (stateDict.Count <= 0)
        {
            DefaultState = state;
            CurState = state;
        }

        stateDict.Add(type, state);
        state.OnInit(initParam);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// forceChange：为true则可以自身向自身转换
    public void ChangeState<T>(bool forceChange = false, FsmStateEnterParam enterParam = null)
        where T : FsmStateBase
    {
        var type = typeof(T);
        ChangeState(type, forceChange, enterParam);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// forceChange：为true则可以自身向自身转换
    public async void ChangeState(Type type, bool forceChange = false, FsmStateEnterParam enterParam = null)
    {
        if (!isBoot)
        {
            CLog.Error($"状态机未启动，先启动状态机");
            return;
        }
        if (!stateDict.TryGetValue(type, out var _state))
        {
            CLog.Error($"找不到此state，先添加state：{type.FullName}");
            return;
        }
        if (CurState.GetType() == type && !forceChange)
        {
            CLog.Error($"不能切换到自身状态，state：{type.FullName}");
            return;
        }

        PreState = CurState;
        await CurState.PreOnExit();
        CurState?.OnExit();
        CurState = _state;
        await CurState.PreOnEnter(enterParam);
        CurState?.OnEnter(enterParam);
        CurState.IsFirstEnter = false;

        ClearMemory();
    }

    public bool CheckState<T>()
        where T : FsmStateBase
    {
        var type1 = CurState.GetType();
        var type2 = typeof(T);
        var ret = type1 == type2;
        return ret;
    }

    public override void FixedUpdate(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurState?.OnFixedUpdate(deltaTime);
    }

    public override void Update(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurState?.OnUpdate(deltaTime);
    }

    public override void LateUpdate(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurState?.OnLateUpdate(deltaTime);
    }

    public void Pause()
    {
        if (!isBoot)
            return;
        if (isPause)
            return;
        CurState?.OnPause();
    }

    public void Resume()
    {
        if (!isBoot)
            return;
        if (!isPause)
            return;
        CurState?.OnResume();
    }

    public override void OnDispose()
    {
        CurState?.OnExit();
        foreach (var state in stateDict.Values)
        {
            state?.Dispose();
        }
        // stateDict.Clear();
    }

    private void ClearMemory()
    {
        Resources.UnloadUnusedAssets();
    }
}