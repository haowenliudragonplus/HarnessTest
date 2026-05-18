using System;
using System.Collections;
using Framework;
using UnityEngine;

public class CoroutineHandler
{
    private MonoBehaviour monoBehaviourHolder;

    public IEnumerator Coroutine { get; private set; } //协程
    public ECoroutineBelongType BelongType { get; private set; } //归属类型

    private Coroutine runCoroutine; //执行的协程

    private Action<CoroutineHandler> onRemove;

    private bool isRunning;

    public CoroutineHandler(MonoBehaviour monoBehaviourHolder, IEnumerator coroutine, ECoroutineBelongType belongType, Action<CoroutineHandler> onRemove)
    {
        this.monoBehaviourHolder = monoBehaviourHolder;
        Coroutine = coroutine;
        BelongType = belongType;
        this.onRemove = onRemove;
        isRunning = false;
    }

    public void Start()
    {
        if (isRunning)
        {
            CLog.Error("协程正在运行，不可重复执行");
            return;
        }
        isRunning = true;
        runCoroutine = monoBehaviourHolder.StartCoroutine(Run());
    }

    public void Stop()
    {
        isRunning = false;
        monoBehaviourHolder.StopCoroutine(runCoroutine);
        Finish();
    }

    private void Finish()
    {
        onRemove?.Invoke(this);
        Coroutine = null;
    }

    private IEnumerator Run()
    {
        IEnumerator e = Coroutine;
        yield return Coroutine;

        Finish();
    }
}