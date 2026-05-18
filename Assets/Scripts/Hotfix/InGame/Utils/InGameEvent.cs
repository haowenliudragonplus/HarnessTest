using System.Collections;
using System.Collections.Generic;
using DragonPlus.Core;
using UnityEngine;

public struct EvtEnterInGame : IEvent
{
    public InGameModeBase mode;

    public EvtEnterInGame(InGameModeBase mode)
    {
        this.mode = mode;
    }
}

public struct EvtRefreshHp : IEvent
{
    public int curHp;

    public EvtRefreshHp(int curHp)
    {
        this.curHp = curHp;
    }
}

public struct EvtRefreshStep : IEvent
{
    public int curStep;

    public EvtRefreshStep(int curStep)
    {
        this.curStep = curStep;
    }
}

public struct EvtShowHintBtn : IEvent
{
    
}

public struct EvtAchievementCompleted : IEvent
{
    public int heartNum; // 剩余心数
    public int hintNum;  // 提示次数
    public int auxiliaryLines; // 辅助线次数
    public int revivalNum;   // 复活次数
}

public struct EvtAchievementRevival : IEvent
{
    
}

public struct EvtAchievementRefreshUI : IEvent
{
    
}

public struct EvtAchievementRefreshRed : IEvent
{
    
}