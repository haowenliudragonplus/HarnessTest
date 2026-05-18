using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;

public class FsmStateInitParam
{
}

public class FsmStateEnterParam
{
}

public abstract class FsmStateBase
{
    private Dictionary<Type, FsmStateBase> subStateDict = new Dictionary<Type, FsmStateBase>();

    protected FsmStateBase DefaultState { get; private set; } //默认子状态
    public FsmStateBase CurSubState { get; private set; } //当前子状态
    public FsmStateBase PreSubState { get; private set; } //上一个子状态

    protected bool isBoot;
    protected bool isPause;

    public bool IsFirstEnter { get; set; } = true;

    #region 子状态机

    public void SetDefaultSubState<T>()
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (!subStateDict.TryGetValue(type, out var _state))
        {
            CLog.Error($"找不到此子state，先添加state：{type.FullName}");
            return;
        }
        DefaultState = _state;
        CurSubState = _state;
    }

    public void StartSubFsm(FsmStateEnterParam enterParam = null)
    {
        if (isBoot)
            return;
        isBoot = true;
        if (CurSubState == null)
        {
            CLog.Error($"至少添加一个子状态才能启动子状态机");
            return;
        }
        ChangeSubState(CurSubState.GetType(), true, enterParam);
    }

    /// <summary>
    /// 添加子状态
    /// </summary>
    public void AddSubState<T>(FsmStateInitParam initParam = null)
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (subStateDict.ContainsKey(type))
        {
            CLog.Error($"子状态已存在，不能重复添加子状态，state：{type}");
            return;
        }

        var state = new T();

        // 默认第一个添加的状态为初始状态
        if (subStateDict.Count <= 0)
        {
            DefaultState = state;
            CurSubState = state;
        }

        subStateDict.Add(type, state);
        state.OnInit(initParam);
    }

    /// <summary>
    /// 移除子状态
    /// </summary>
    public bool RemoveSubState<T>()
        where T : FsmStateBase
    {
        var type = typeof(T);
        bool ret = subStateDict.Remove(type);
        return ret;
    }

    public void RemoveAllSubState()
    {
        isBoot = false;
        CurSubState?.OnExit();
        foreach (var state in subStateDict.Values)
        {
            state?.Dispose();
        }
        subStateDict.Clear();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    public void ChangeSubState<T>(bool forceChange = false, FsmStateEnterParam enterParam = null)
        where T : FsmStateBase
    {
        var type = typeof(T);
        ChangeSubState(type, forceChange, enterParam);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    public async void ChangeSubState(Type type, bool forceChange = false, FsmStateEnterParam enterParam = null)
    {
        if (!isBoot)
        {
            CLog.Error($"子状态机未启动，先启动子状态机");
            return;
        }
        if (!subStateDict.TryGetValue(type, out var _state))
        {
            CLog.Error($"找不到此子state，先添加子state：{type.FullName}");
            return;
        }
        if (CurSubState.GetType() == type && !forceChange)
        {
            CLog.Error($"不能切换到自身状态，state：{type.FullName}");
            return;
        }
        CLog.Info($" fsm 切状态 {CurSubState.GetType()} --> {type}");

        PreSubState = CurSubState;
        await CurSubState.PreOnExit();
        CurSubState?.OnExit();
        CurSubState = _state;
        await CurSubState.PreOnEnter(enterParam);
        CurSubState?.OnEnter(enterParam);
        CurSubState.IsFirstEnter = false;
    }

    public bool CheckSubState<T>()
        where T : FsmStateBase
    {
        var type1 = CurSubState.GetType();
        var type2 = typeof(T);
        var ret = type1 == type2;
        return ret;
    }

    public void Pause()
    {
        if (!isBoot)
            return;
        if (isPause)
            return;
        CurSubState?.OnPause();
    }

    public void Resume()
    {
        if (!isBoot)
            return;
        if (!isPause)
            return;
        CurSubState?.OnResume();
    }

    #endregion 子状态机

    #region 生命周期

    public virtual void OnInit(FsmStateInitParam initParam = null)
    {
    }

    public virtual async UniTask PreOnEnter(FsmStateEnterParam enterParam = null)
    {
    }

    public virtual void OnEnter(FsmStateEnterParam enterParam = null)
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }

    public virtual void OnFixedUpdate(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurSubState?.OnFixedUpdate(deltaTime);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurSubState?.OnUpdate(deltaTime);
    }

    public virtual void OnLateUpdate(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurSubState?.OnLateUpdate(deltaTime);
    }

    public virtual async UniTask PreOnExit()
    {
    }

    public virtual void OnExit()
    {
    }

    #endregion 生命周期

    public virtual void Dispose()
    {
        CurSubState?.OnExit();
        foreach (var state in subStateDict.Values)
        {
            state?.Dispose();
        }
        subStateDict.Clear();
    }
}