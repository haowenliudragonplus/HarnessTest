using System;
using System.Collections.Generic;
using System.Reflection;
using DragonPlus.Core;
using Framework;

public class EventListenerHolder
{
    private List<Action> unRegisterActionCache = new List<Action>();

    protected void RegisterEvent<T>(Action<T> callback, int subId = -1, EEventBelongType belongType = EEventBelongType.Global)
        where T : IEvent
    {
        Game.GetMod<ModEvent>().Register(callback, subId, belongType);
        unRegisterActionCache.Add(() => Game.GetMod<ModEvent>().UnRegister(callback, subId, belongType));
    }

    protected void UnRegisterAllEvent()
    {
        if (unRegisterActionCache == null || unRegisterActionCache.Count == 0)
            return;

        foreach (var action in unRegisterActionCache)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                CLog.Error($"Failed to unregister event: {ex}");
            }
        }
        unRegisterActionCache.Clear();
    }
}