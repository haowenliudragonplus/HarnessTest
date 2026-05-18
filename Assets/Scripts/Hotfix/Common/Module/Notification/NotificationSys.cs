// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Besure.Chen
// Date : 2023/07/10/11:17
// Ver : 1.0.0
// Description : NotificationSys.cs
// ChangeLog :
// **********************************************

using System.Collections.Generic;
using DragonPlus.Account;
using DragonPlus.Core;
using DragonPlus.Native.Bridge;


namespace TMGame
{
    public class NotificationSys //: LogicSys
    {
        
        protected List<LocalNotificationHandler> localNotificationHandlers;
         
        public bool IsNotificationOn()
        {
            return SDK<INative>.Instance.IsUserNotificationEnabled();
        }

        // public override void Start()
        // {
        //     base.Start();
        //     
        //     SDKUtil.Unity.StartCoroutine(MobileNotificationManager.Init());
        //     RegisterLocalNotificationHandlers();
        //     
        //     SDKUtil.Unity.AddOnApplicationPauseListener(OnApplicationPause);
        // }

        private void RegisterLocalNotificationHandlers()
        {
            localNotificationHandlers = new List<LocalNotificationHandler>
            {
                new EnergyFullNotificationHandler(),
                new ComingBackNotificationHandler(),
                new ActivityOpenHandler(),
                new PassOpenNotificationHandler(),
                new SignInNotificationHandler(),
            //    new TestNotificationHandler()
            };
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                if(Game.GetMod<ModFsm>().CheckState<FsmState_Home>())
                    PushLocalNotifications();
            }
            else
            {
                ClearNotifications();
            }
        }
       
        public void PushLocalNotifications()
        {
            //未登录不弹出推送
            if (!SDK<IAccount>.Instance.HasLogin) 
                return;
            
            ClearNotifications();

            int notificationCount = LocalNotificationStorage.GetTodayTotalNotificationCount();
            for (int i = 0; i < localNotificationHandlers.Count; i++)
            {
                var localNotificationHandler = localNotificationHandlers[i];

                if (localNotificationHandler.CheckValid())
                {
                    //当天的本地推送不能超过4条
                    if (localNotificationHandler.IsScheduleToday())
                    {
                        if (notificationCount < LocalNotificationStorage.DAILY_MAX_NOTIFICATIONS)
                        {
                            notificationCount++;
                            localNotificationHandler.Execute();
                        }
                    }
                    else
                    {
                        localNotificationHandler.Execute();
                    }
                }
            }
        }
        
        private void ClearNotifications()
        {
            if (localNotificationHandlers != null)
            {
                for (int i = 0; i < localNotificationHandlers.Count; i++)
                {
                    localNotificationHandlers[i].Initialization();
                }   
                for (int i = 0; i < localNotificationHandlers.Count; i++)
                {
                    var localNotificationHandler = localNotificationHandlers[i];
                    localNotificationHandler.CancelNotification();
                }
            }

            MobileNotificationManager.ClearScheduledNotifications();  
        }
    }
}