using System;
using System.Collections.Generic;
using TMGame;
using UnityEngine;

/// <summary>
/// 计时器归属类型
/// </summary>
/// 可以统一在某一时刻清空某一归属类型的所有计时器
public enum ETimerBelongType
{
    Global = 1,
    Audio,

    InGame,//局内
}

public class TimerTask
{
    private float duration;
    private bool ignoreTimeScale;
    private int loopCount = 1; //-1代表无限循环
    private Action<int> onComplete;
    private Action onSet;
    private Action<float> onUpdate;
    private MonoBehaviour monoBehaviourHolder;
    private bool hasMonoBehaviourHolder;
    public ETimerBelongType BelongType { get; private set; }

    private bool isPause;
    private bool isDispose;
    private bool isDone;
    public bool IsComplete
    {
        get
        {
            return isDispose || isDone || (hasMonoBehaviourHolder && monoBehaviourHolder == null);
        }
    }

    private float leftTime;

    public void Set(float duration, bool ignoreTimeScale, int loopCount, Action onSet, Action<float> onUpdate, Action<int> onComplete,
        MonoBehaviour monoBehaviourHolder, ETimerBelongType belongType)
    {
        this.duration = duration;
        this.ignoreTimeScale = ignoreTimeScale;
        this.loopCount = loopCount;
        this.onSet = onSet;
        this.onUpdate = onUpdate;
        this.onComplete = onComplete;
        this.monoBehaviourHolder = monoBehaviourHolder;
        hasMonoBehaviourHolder = monoBehaviourHolder != null;
        BelongType = belongType;

        SetDuration(duration);
        this.onSet?.Invoke();
    }

    public void Dispose()
    {
        isDispose = true;
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void Update()
    {
        try
        {
            if (IsComplete)
                return;
            if (isPause)
                return;

            leftTime -= GetDeltaTime();
            onUpdate?.Invoke(leftTime);
            if (leftTime <= 0)
            {
                onComplete?.Invoke(loopCount);

                if (loopCount == -1)
                {
                    Reset();
                }
                else
                {
                    loopCount--;
                    if (loopCount <= 0)
                    {
                        isDone = true;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private float GetDeltaTime()
    {
        float deltaTime = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        return deltaTime;
    }

    public float GetLeftTime()
    {
        return leftTime;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
        leftTime = duration;
    }

    public void Reset()
    {
        leftTime = duration;
    }
}

public class ModTimer : ModuleBase
{
    private List<TimerTask> timerTaskList;
    private List<TimerTask> timerTaskList_Temp;
    private Dictionary<ETimerBelongType, List<TimerTask>> timerTaskDict;

    public override void OnInit()
    {
        base.OnInit();
        timerTaskList = new List<TimerTask>();
        timerTaskDict = new Dictionary<ETimerBelongType, List<TimerTask>>();
        timerTaskList_Temp = new List<TimerTask>();
    }

    public TimerTask Register(float duration, bool ignoreTimeScale = false, int loopCount = 1, Action onSet = null, Action<float> onUpdate = null, Action<int> onComplete = null,
        MonoBehaviour monoHolder = null, ETimerBelongType belongType = ETimerBelongType.Global)
    {
        TimerTask timerTask = new TimerTask();
        timerTask.Set(duration, ignoreTimeScale, loopCount, onSet, onUpdate, onComplete, monoHolder, belongType);
        timerTaskList.Add(timerTask);
        if (!timerTaskDict.TryGetValue(belongType, out var _timerTaskList))
        {
            _timerTaskList = new List<TimerTask>();
            timerTaskDict.Add(belongType, _timerTaskList);
        }
        _timerTaskList.Add(timerTask);
        return timerTask;
    }

    public void PauseAll()
    {
        foreach (var timerTask in timerTaskList)
        {
            timerTask.Pause();
        }
    }

    public void ResumeAll()
    {
        foreach (var timerTask in timerTaskList)
        {
            timerTask.Resume();
        }
    }

    public void DisposeAll()
    {
        foreach (var timerTask in timerTaskList)
        {
            timerTask.Dispose();
        }
        timerTaskList.Clear();
    }

    public void DisposeAll(ETimerBelongType belongType)
    {
        if (!timerTaskDict.TryGetValue(belongType, out var _timerTaskList))
            return;
        foreach (var timerTask in _timerTaskList)
        {
            timerTask.Dispose();
            timerTaskList.Remove(timerTask);
        }
        timerTaskDict.Remove(belongType);
    }

    private void UpdateAll()
    {
        timerTaskList.CopyListNonAlloc(timerTaskList_Temp);
        foreach (var timerTask in timerTaskList_Temp)
        {
            timerTask.Update();
            if (timerTask.IsComplete)
            {
                timerTaskList.Remove(timerTask);
                if (timerTaskDict.TryGetValue(timerTask.BelongType, out var _timerTaskList))
                {
                    _timerTaskList.Remove(timerTask);
                    if (_timerTaskList.Count == 0)
                    {
                        timerTaskDict.Remove(timerTask.BelongType);
                    }
                }
            }
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        UpdateAll();
    }
}