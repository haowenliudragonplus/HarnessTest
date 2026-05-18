using System.Collections;
using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 协程归属类型
/// </summary>
public enum ECoroutineBelongType
{
    Global = 1,

    InGame,//局内
}

public class ModCoroutine : ModuleBase
{
    private MonoBehaviour monoBehaviourHolder;

    private Dictionary<ECoroutineBelongType, List<CoroutineHandler>> coroutineHandleDict = new Dictionary<ECoroutineBelongType, List<CoroutineHandler>>(); //所有协程

    private List<CoroutineHandler> coroutineHandlerList_Temp = new List<CoroutineHandler>();

    public override void OnStart()
    {
        base.OnStart();
        monoBehaviourHolder = Object.FindObjectOfType<Boot>();
    }

    /// <summary>
    /// 开启一个协程
    /// </summary>
    public CoroutineHandler StartCoroutine(IEnumerator coroutine, ECoroutineBelongType belongType = ECoroutineBelongType.Global)
    {
        if (coroutine == null)
        {
            CLog.Error("协程为null，不能执行");
            return null;
        }

        CoroutineHandler handle = new CoroutineHandler(monoBehaviourHolder, coroutine, belongType, InternalRemove);
        handle.Start();

        if (!coroutineHandleDict.TryGetValue(belongType, out var _coroutineList))
        {
            _coroutineList = new List<CoroutineHandler>();
            coroutineHandleDict.Add(belongType, _coroutineList);
        }
        _coroutineList.Add(handle);

        return handle;
    }

    /// <summary>
    /// 停止某个协程
    /// </summary>
    public void StopCoroutine(CoroutineHandler coroutineHandler)
    {
        coroutineHandler?.Stop();
    }

    /// <summary>
    /// 停止某个归属类型的协程
    /// </summary>
    public void StopCoroutine(ECoroutineBelongType belongType)
    {
        if (!coroutineHandleDict.TryGetValue(belongType, out var _list))
            return;
        _list.CopyListNonAlloc(coroutineHandlerList_Temp);
        foreach (var handler in coroutineHandlerList_Temp)
        {
            StopCoroutine(handler);
        }
        coroutineHandlerList_Temp.Clear();
    }

    /// <summary>
    /// 停止所有协程
    /// </summary>
    public void StopAllCoroutines()
    {
        if (coroutineHandleDict.Count == 0)
            return;
        var tempDict = new Dictionary<ECoroutineBelongType, List<CoroutineHandler>>(coroutineHandleDict);
        foreach (var list in tempDict.Values)
        {
            list.CopyListNonAlloc(coroutineHandlerList_Temp);
            foreach (var handler in coroutineHandlerList_Temp)
            {
                StopCoroutine(handler);
            }
        }
        coroutineHandleDict.Clear();
        coroutineHandlerList_Temp.Clear();
        tempDict.Clear();
    }

    private void InternalRemove(CoroutineHandler handler)
    {
        var belongType = handler.BelongType;
        if (coroutineHandleDict.TryGetValue(belongType, out var _list))
        {
            _list.Remove(handler);
            if (_list.Count == 0)
            {
                coroutineHandleDict.Remove(belongType);
            }
        }
    }

    public override void OnDispose()
    {
        base.OnDispose();
        StopAllCoroutines();
    }
}