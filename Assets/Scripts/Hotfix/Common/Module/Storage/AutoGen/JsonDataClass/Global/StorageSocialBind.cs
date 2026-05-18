/************************************************
 * Storage class : StorageSocialBind
 * This file is can not be modify !!!
 * If there is some problem, ask hong.zhou.
 ************************************************/

using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DragonPlus.Save;
using System.Collections.Generic;

namespace GameStorage
{
    [System.Serializable]
    public class StorageSocialBind : StorageBase
    {
        
        // 苹果账号初次绑定未领奖
        [JsonProperty]
        bool appleFirstBindPendingReward;
        [JsonIgnore]
        public bool AppleFirstBindPendingReward
        {
            get
            {
                return appleFirstBindPendingReward;
            }
            set
            {
                if(appleFirstBindPendingReward != value)
                {
                    appleFirstBindPendingReward = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 苹果账号绑定奖励领取标记
        [JsonProperty]
        bool appleBindRewardReceived;
        [JsonIgnore]
        public bool AppleBindRewardReceived
        {
            get
            {
                return appleBindRewardReceived;
            }
            set
            {
                if(appleBindRewardReceived != value)
                {
                    appleBindRewardReceived = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // FACEBOOK账号初次绑定未领奖
        [JsonProperty]
        bool facebookFirstBindPendingReward;
        [JsonIgnore]
        public bool FacebookFirstBindPendingReward
        {
            get
            {
                return facebookFirstBindPendingReward;
            }
            set
            {
                if(facebookFirstBindPendingReward != value)
                {
                    facebookFirstBindPendingReward = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // FACEBOOK账号绑定奖励领取标记
        [JsonProperty]
        bool facebookBindRewardReceived;
        [JsonIgnore]
        public bool FacebookBindRewardReceived
        {
            get
            {
                return facebookBindRewardReceived;
            }
            set
            {
                if(facebookBindRewardReceived != value)
                {
                    facebookBindRewardReceived = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}