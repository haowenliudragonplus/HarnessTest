using System;
using System.Collections.Generic;
using DragonPlus.Core;

public class ModuleBase : EventListenerHolder
{
    public virtual void OnInit()
    {
        RegisterEvent();
    }

    public virtual void RegisterEvent()
    {
    }

    public virtual void OnStart()
    {

    }

    public virtual void FixedUpdate(float deltaTime)
    {
    }

    public virtual void Update(float deltaTime)
    {
    }

    public virtual void LateUpdate(float deltaTime)
    {
    }

    public virtual void OnLoginSuccess()
    {
    }

    public virtual void OnDispose()
    {
        UnRegisterAllEvent();
    }
}