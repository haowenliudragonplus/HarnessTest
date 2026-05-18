using System;
using System.Collections.Generic;
using DragonPlus.Core;

/// <summary>
/// 管理每个事件类型中的事件
/// </summary>
public class EventData
{
    private Dictionary<int, List<Delegate>> eventDict = new Dictionary<int, List<Delegate>>();

    public bool Add<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        if (!eventDict.TryGetValue(subId, out var _eventList))
        {
            _eventList = new List<Delegate>();
            eventDict.Add(subId, _eventList);
        }
        _eventList.Add(callback);
        return true;
    }

    public bool Remove<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        if (!eventDict.TryGetValue(subId, out var _eventList))
            return false;

        bool ret = _eventList.Remove(callback);
        return ret;
    }

    public void Dispatch<T>(T evt, int subId = -1)
        where T : IEvent
    {
        if (!eventDict.TryGetValue(subId, out var _eventList))
            return;

        List<Delegate> eventList_Temp = new List<Delegate>();
        _eventList.CopyListNonAlloc(eventList_Temp);
        foreach (var e in eventList_Temp)
        {
            var callback = e as Action<T>;
            callback?.Invoke(evt);
        }
    }
}