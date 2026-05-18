// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Besure.Chen
// Date : 2024/01/04/17:22
// Ver : 1.0.0
// Description : LocalNotificationHandler.cs
// ChangeLog :
// **********************************************

using System;
using DragonPlus;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using UnityEngine;

namespace TMGame
{
    public class LocalNotificationHandler
    {
        protected LocalNotificationId Id;
        protected LocalNotificationStorage _localNotificationStorage;

        public LocalNotificationHandler(LocalNotificationId id)
        {
            Id = id;
            _localNotificationStorage = new LocalNotificationStorage(this);
        }

        public void Initialization()
        {
            if (_localNotificationStorage != null)
            {
                _localNotificationStorage.CheckSendNotificationSuccess();
            }
        }

        public bool CheckValid()
        {
            return !_localNotificationStorage.IsTodayMaxNotifications() && GetScheduleDelayMillisecond() > 0;
        }

        public bool IsScheduleToday()
        {
            try
            {
                //System.ArgumentOutOfRangeException: Value to add was out of range.
                var scheduleDelayMillisecond = GetScheduleDelayMillisecond();

                if (scheduleDelayMillisecond > 24 * 3600 * 1000)
                    return false;

                var scheduleTime = DateTime.Today.AddMilliseconds(scheduleDelayMillisecond);

                return scheduleTime.Date == DateTime.Today.Date;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public void Execute()
        {
            var delaySeconds = (long)GetScheduleDelayMillisecond();

            if (delaySeconds < 5 * 60 * 1000)
            {
                delaySeconds += 5 * 60 * 1000;
            }

            var item = GetNotificationItem();

            if (item == null)
                return;

            MobileNotificationManager.SendNotification(GetNotificationId(), delaySeconds, item.Item1,
                item.Item2, new Color32(0xff, 0x44, 0x44, 255));

            _localNotificationStorage.RecordHandlerData(delaySeconds);

            Log.Debug($"LocalNotification {GetType()} Schedule Success,DelaySeconds:{delaySeconds / 1000}|{item.Item1}/{item.Item2}");
        }

        public void CancelNotification()
        {
            _localNotificationStorage.CancelNotification();
        }

        public virtual long GetScheduleDelayMillisecond()
        {
            return 0;
        }

        // protected float GetUpComingActivityDelaySeconds<T>(string activityType) where T: ActivityBase
        // {
        //     if (Client.Get<ActivityController>().GetUpComingActivityTakeTime(activityType) > 0)
        //     {
        //         return Client.Get<ActivityController>().GetUpComingActivityTakeTime(activityType);
        //     }
        //     return 0;
        // }
        //
        // protected float GetClosingActivityDelaySeconds<T>(string activityType) where T: ActivityBase
        // {
        //     var activity =
        //         Client.Get<ActivityController>().GetDefaultActivity(activityType) as T;
        //     if (activity == null || !activity.IsValid())
        //         return 0;
        //     return GetBeforeLastDayDelaySeconds(activity.GetCountDown());
        // }

        //protected float GetBeforeLastDayDelaySeconds(float countDown)
        // {
        //     if (countDown < TMUtility.SecondsOfOneDay)
        //     {
        //         return 0f;
        //     }
        //
        //     if (countDown > TMUtility.SecondsOfOneDay * 1.5f)
        //     {
        //         return countDown - TMUtility.SecondsOfOneDay * 1.5f;
        //     }
        //
        //     if (countDown > TMUtility.SecondsOfOneDay && countDown < TMUtility.SecondsOfOneDay * 2f)
        //     {
        //         return 900f;
        //     }
        //
        //     return 0;
        // }

        public int GetNotificationId()
        {
            return (int)Id;
        }

        public int GetNotificationTotal()
        {
            return 1;
        }

        public Tuple<string, string> GetNotificationItem()
        {
            return null;
            // var infoList = GameConfigManager.Instance.NotificationInfoList;
            //
            // for (var i = 0; i < infoList.Count; i++)
            // {
            //     if (infoList[i].ScheduleId == (int) Id)
            //     {
            //         var title = CoreUtils.GetLocalization(infoList[i].NotificationLocaleTitleKey);
            //         var content = CoreUtils.GetLocalization(infoList[i].NotificationLocaleContentKey);
            //         return new Tuple<string, string>(title, content);
            //     }
            // }
            //
            // return new Tuple<string, string>("TestTitle", "TestContent");
        }
    }

    public class EnergyFullNotificationHandler : LocalNotificationHandler
    {
        public EnergyFullNotificationHandler()
            : base(LocalNotificationId.ENERGY_FULL)
        {
        }

        public override long GetScheduleDelayMillisecond()
        {
            return 0;
            //return  Game.GetMod<EnergySys>().GetFullEnergyNeedWaitTime();
        }
    }

    public class ActivityOpenHandler : LocalNotificationHandler
    {
        public ActivityOpenHandler()
            : base(LocalNotificationId.ACTIVITY)
        {
        }

        public override long GetScheduleDelayMillisecond()
        {
            var delayTime1 = Game.GetMod<ActivitySys>().GetUpcomingActivityTime(ActivityType.WinStreak);
            var delayTime2 = Game.GetMod<ActivitySys>().GetUpcomingActivityTime(ActivityType.GoldenLeague);
            var delayTime3 = Game.GetMod<ActivitySys>().GetUpcomingActivityTime(ActivityType.SpeedRace);

            delayTime1 = Math.Max(delayTime1, 0);
            delayTime2 = Math.Max(delayTime2, 0);
            delayTime3 = Math.Max(delayTime3, 0);

            return (long)Math.Min(delayTime3, Math.Min(delayTime1, delayTime2));
        }
    }

    public class PassOpenNotificationHandler : LocalNotificationHandler
    {
        public PassOpenNotificationHandler()
            : base(LocalNotificationId.PASS_OPEN)
        {
        }

        public override long GetScheduleDelayMillisecond()
        {
            return (long)Game.GetMod<ActivitySys>().GetUpcomingActivityTime(ActivityType.GoldenPass);
        }
    }

    public class ComingBackNotificationHandler : LocalNotificationHandler
    {
        public ComingBackNotificationHandler()
            : base(LocalNotificationId.COMMING_BACK)
        {
        }

        public override long GetScheduleDelayMillisecond()
        {
            return TimeUtils.SecPerDay * 2 * 1000;
        }
    }

    public class SignInNotificationHandler : LocalNotificationHandler
    {
        public SignInNotificationHandler()
            : base(LocalNotificationId.DAILY_GIFT)
        {
        }
    }
}