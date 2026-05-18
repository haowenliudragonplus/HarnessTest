// **********************************************
// Copyright(c) 2024 by com.dragonplus
// All right reserved
// 
// Author : Jian.Wang
// Date : 2024/08/03/10:19
// Ver : 0.0.1
// Description : BIWrapper.cs
// ChangeLog :
// **********************************************

using DragonPlus.Core;
using DragonPlus.Tracking;

namespace DragonU3DSDK.Network.BI
{
    public class ThirdPartyTrackingConfig
    {
        public string eventName;
        public bool enableAdjust;
        public bool enableFirebase;
        public bool enableFacebook;
        public string adjustEventToken;
    }

    public class BIManager : Singleton<BIManager>
    {
        public void AddThirdPartyTrackingConfig(string gameEvent, ThirdPartyTrackingConfig config)
        {
            SDK<ITracking>.Instance.AddThirdPartyTrackingConfig(gameEvent,
                new DragonPlus.Tracking.ThirdPartyTrackingConfig()
                {
                    eventName = config.eventName,
                    adjustEventToken = config.adjustEventToken,
                    enableAdjust = config.enableAdjust,
                    enableFacebook = config.enableFacebook,
                    enableFirebase = config.enableFirebase
                });
        }
    }
}