// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/03/12:07
// Ver : 1.0.0
// Description : Events.cs
// ChangeLog :
// **********************************************

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using DragonPlus.Config.Shop;
using DragonPlus.Core;
using Object = System.Object;

namespace TMGame
{
    public struct EventEnergyChange : IEvent
    {
        public int current;
        public int delta;
        public bool isInfinite;

        public EventEnergyChange(int inCurrent, int inDelta, bool inIsInfinite)
        {
            current = inCurrent;
            delta = inDelta;
            isInfinite = inIsInfinite;
        }
    }

    public struct EventSingIn : IEvent
    {
        
    }
    public struct EventMonthDailyChallenge : IEvent
    {
        public string date;

        public EventMonthDailyChallenge(string inDate)
        {
            date = inDate;
        }
    }
    
    public struct EventMonthFlower : IEvent
    {
        public string date;

        public EventMonthFlower(string inDate)
        {
            date = inDate;
        }
    }
    
    public struct EventDailyChallenge : IEvent
    {
        
    }
    public struct EventCurrencyFlyAniEnd : IEvent
    {
        public EItemType EItemType;

        public EventCurrencyFlyAniEnd(EItemType inEItemType)
        {
            EItemType = inEItemType;
        }
    }

    public struct WindowOpenEvent : IEvent
    {
    }
    public struct WindowCloseEvent : IEvent
    {
    }
    public struct WindowOpenCompleteEvent : IEvent
    {
    }
    public struct WindowCloseCompleteEvent : IEvent
    {
    }
    
    public struct SelectThemeEvent : IEvent
    {
        public int themeId;
        public int bgId;

        public SelectThemeEvent(int inThemeId, int inBgId)
        {
            themeId = inThemeId;
            bgId = inBgId;
        }
    }

    public struct RefreshThemeEvent : IEvent
    {
        public int themeId;
        public int bgId;

        public RefreshThemeEvent(int inThemeId, int inBgId)
        {
            themeId = inThemeId;
            bgId = inBgId;
        }
    }
    
    #region Activity

    public struct EventActivityCreate : IEvent
    {
        public string activityType;

        public EventActivityCreate(string inActivityType)
        {
            activityType = inActivityType;
        }
    }

    public struct EventActivityExpire : IEvent
    {
        public string activityType;
        public string activityId;

        public EventActivityExpire(string inActivityType, string inActivityId)
        {
            activityType = inActivityType;
            activityId = inActivityId;
        }
    }

    public struct EventActivityUpdate : IEvent
    {
        public string activityType;
        public EventActivityUpdate(string inActivityType)
        {
            activityType = inActivityType;
        }
    }
    public struct EventActivityOnCreate : IEvent
    {
        public string activityType;
        public EventActivityOnCreate(string inActivityType)
        {
            activityType = inActivityType;
        }
    }
    public struct EventActivityEntrance : IEvent
    {
        public string activityType;

        public EventActivityEntrance(string inActivityType)
        {
            activityType = inActivityType;
        }
    }
    
    public struct EventEndlessGiftPackClick : IEvent
    {
        public int id;

        public EventEndlessGiftPackClick(int value)
        {
            id = value;
        }
    }
    
    public struct EventStarChallengeRefresh : IEvent
    {
    }
    public struct EventCollectGuildRefresh : IEvent
    {
    }
    public struct EventBuyRechargeGiftPackSuccess : IEvent
    {
    }

    #endregion
    
    public struct EventAvatarChange : IEvent
    {
    }

    public struct EventActiveChange : IEvent
    {
    }
    public struct EventWeekChange : IEvent
    {
    }
    
    public struct EventAdBox : IEvent
    {
        
    }

    public struct EventTryShowGuideTip : IEvent
    {
    }

    public struct FinishGuideEvent : IEvent
    {
        public string GuideId;

        public FinishGuideEvent(string guideId)
        {
            GuideId = guideId;
        }
    }
}