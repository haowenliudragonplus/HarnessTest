// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Besure.Chen
// Date : 2024/01/04/17:27
// Ver : 1.0.0
// Description : LocalNotificationStorage.cs
// ChangeLog :
// **********************************************

using System;
using DragonPlus.Core;
using DragonPlus.Save;
using UnityEngine;

namespace TMGame
{
    public class LocalNotificationStorage
    {
        private static int weekDay;
        private LocalNotificationHandler _handler;

        public static int DAILY_MAX_NOTIFICATIONS = 4;
        private const string prefixIndex = "LocalNotification_Index_";
        private const string prefixSendTime = "LocalNotification_SendTime_";
        private const string prefixTodayTotalCount = "LocalNotification_TodayTotal_Count_";
        private const string prefixExecuteLeftTime = "LocalNotification_Execute_LeftTime_";

        public LocalNotificationStorage(LocalNotificationHandler handler)
        {
            _handler = handler;
        }

        static LocalNotificationStorage()
        {
            //清除当天外的本地推送数量
            weekDay = (int) DateTime.Now.DayOfWeek;
            for (int i = 0; i <= 7; i++)
            {
                if (weekDay != i)
                {
                    PlayerPrefs.SetInt(KeyWrapper($"{prefixTodayTotalCount}{i}"), 0);
                }
            }
        }
        public void CheckSendNotificationSuccess()
        {
            var notificationId = _handler.GetNotificationId();
            var strTime = PlayerPrefs.GetString(KeyWrapper($"{prefixSendTime}{notificationId}"), "");
            var delayTime = PlayerPrefs.GetFloat(KeyWrapper($"{prefixExecuteLeftTime}{notificationId}"), 0f);
            if (!string.IsNullOrEmpty(strTime) && delayTime > 0)
            {
                var startTime = DateTime.Parse(strTime);
                //超过时间就算已经发送成功本地推送
                if ((DateTime.Now - startTime).TotalMilliseconds > delayTime * 1000)
                {
                    AddNotificationIndex(); //索引加一
                    AddTodayTotalNotificationCount(); //每天发送的总数加一
                    ClearRecord(); //清空记录
                }
            }
        }

        public static int GetTodayTotalNotificationCount()
        {
            return PlayerPrefs.GetInt(KeyWrapper($"{prefixTodayTotalCount}{weekDay}"), 0);
        }

        public static void AddTodayTotalNotificationCount()
        {
            PlayerPrefs.SetInt(KeyWrapper($"{prefixTodayTotalCount}{weekDay}"),
                GetTodayTotalNotificationCount() + 1);
        }

        public int GetNotificationIndex()
        {
            var index = PlayerPrefs.GetInt(KeyWrapper($"{prefixIndex}{_handler.GetNotificationId()}"), 0);
            if (index >= _handler.GetNotificationTotal())
            {
                index = 0;
                PlayerPrefs.SetInt(KeyWrapper($"{prefixIndex}{_handler.GetNotificationId()}"), 0);
            }

            return index;
        }

        public void AddNotificationIndex()
        {
            var index = (GetNotificationIndex() + 1) % _handler.GetNotificationTotal();
            PlayerPrefs.SetInt(KeyWrapper($"{prefixIndex}{_handler.GetNotificationId()}"), index);
        }

        public void RecordHandlerData(long delaySeconds)
        {
            var notificationId = _handler.GetNotificationId();
            //记录推送的索引
            PlayerPrefs.SetInt(KeyWrapper($"{prefixIndex}{notificationId}"), GetNotificationIndex());
            //记录推送的时间
            PlayerPrefs.SetString(KeyWrapper($"{prefixSendTime}{notificationId}"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //记录功能开启剩余时间
            PlayerPrefs.SetFloat(KeyWrapper($"{prefixExecuteLeftTime}{notificationId}"), delaySeconds);
        }

        public void ClearRecord()
        {
            var notificationId = _handler.GetNotificationId();
            PlayerPrefs.DeleteKey(KeyWrapper($"{prefixSendTime}{notificationId}"));
            PlayerPrefs.DeleteKey(KeyWrapper($"{prefixExecuteLeftTime}{notificationId}"));
        }

        public void CancelNotification()
        {
            MobileNotificationManager.CancelScheduledNotification(_handler.GetNotificationId());
            //LocalNotification.CancelNotification(_handler.GetNotificationId());
            ClearRecord();
        }

        private static string KeyWrapper(string key)
        {
            return $"{SDK<IStorage>.Instance.Get<StorageCommon>().PlayerId}_{key}";
        }

        public bool IsTodayMaxNotifications()
        {
            return GetTodayTotalNotificationCount() >= DAILY_MAX_NOTIFICATIONS;
        }
    }
}