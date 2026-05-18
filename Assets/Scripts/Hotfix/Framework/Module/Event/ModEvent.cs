using System;
using System.Collections.Generic;
using DragonPlus.Core;
using TMGame;

/// <summary>
/// 事件归属类型
/// </summary>
/// 可以统一在某一时刻清空某一归属类型的所有事件
public enum EEventBelongType
{
    Global = 1,

    InGame,//局内
}

/// <summary>
/// 事件模块
/// </summary>
public class ModEvent : ModuleBase
{
    public Dictionary<EEventBelongType, Dictionary<Type, EventData>> eventDataDict; //所有事件

    public override void OnInit()
    {
        base.OnInit();
        eventDataDict = new Dictionary<EEventBelongType, Dictionary<Type, EventData>>();
    }

    public void Register<T>(Action<T> callback, int subId = -1, EEventBelongType belongType = EEventBelongType.Global)
        where T : IEvent
    {
        if (!eventDataDict.TryGetValue(belongType, out var _eventDataDict))
        {
            _eventDataDict = new Dictionary<Type, EventData>();
            eventDataDict.Add(belongType, _eventDataDict);
        }
        var type = typeof(T);
        if (!_eventDataDict.TryGetValue(type, out var _eventData))
        {
            _eventData = new EventData();
            _eventDataDict.Add(type, _eventData);
        }
        _eventData.Add<T>(callback, subId);
    }

    public bool UnRegister<T>(Action<T> callback, int subId = -1, EEventBelongType belongType = EEventBelongType.Global)
        where T : IEvent
    {
        if (!eventDataDict.TryGetValue(belongType, out var _eventDataDict))
            return false;
        var type = typeof(T);
        if (!_eventDataDict.TryGetValue(type, out var _eventData))
            return false;
        bool ret = _eventData.Remove<T>(callback, subId);
        return ret;
    }

    public void Dispatch<T>(T evt, int subId = -1, EEventBelongType belongType = EEventBelongType.Global)
        where T : IEvent
    {
        if (!eventDataDict.TryGetValue(belongType, out var _eventDataDict))
            return;
        var type = typeof(T);
        if (!_eventDataDict.TryGetValue(type, out var eventData))
            return;
        eventData.Dispatch(evt, subId);
    }

    public void UnRegister(EEventBelongType belongType = EEventBelongType.Global)
    {
        eventDataDict.Remove(belongType);
    }

    public void UnRegisterAll()
    {
        eventDataDict.Clear();
    }
}