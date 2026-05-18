/************************************************
 * Storage class : StorageAd
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
    public class StorageAd : StorageBase
    {
        
        // 激励视频解锁状态
        [JsonProperty]
        StorageDictionary<int,bool> unlockRewardState = new StorageDictionary<int,bool>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,bool> UnlockRewardState
        {
            get
            {
                return unlockRewardState;
            }
        }
        // ---------------------------------//
        
        // 插频解锁状态
        [JsonProperty]
        StorageDictionary<int,bool> unlockInterState = new StorageDictionary<int,bool>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,bool> UnlockInterState
        {
            get
            {
                return unlockInterState;
            }
        }
        // ---------------------------------//
        
        // 重置状态间隔时间
        [JsonProperty]
        long resetStateTime;
        [JsonIgnore]
        public long ResetStateTime
        {
            get
            {
                return resetStateTime;
            }
            set
            {
                if(resetStateTime != value)
                {
                    resetStateTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 激励视频次数状态
        [JsonProperty]
        StorageDictionary<int,int> rewardWatchCount = new StorageDictionary<int,int>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,int> RewardWatchCount
        {
            get
            {
                return rewardWatchCount;
            }
        }
        // ---------------------------------//
        
        // 激励视频时间状态
        [JsonProperty]
        StorageDictionary<int,long> rewardWatchLastTimeStamp = new StorageDictionary<int,long>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,long> RewardWatchLastTimeStamp
        {
            get
            {
                return rewardWatchLastTimeStamp;
            }
        }
        // ---------------------------------//
        
        // 插屏次数状态
        [JsonProperty]
        StorageDictionary<int,int> interWatchCount = new StorageDictionary<int,int>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,int> InterWatchCount
        {
            get
            {
                return interWatchCount;
            }
        }
        // ---------------------------------//
        
        // 插屏时间状态
        [JsonProperty]
        StorageDictionary<int,long> interWatchLastTimeStamp = new StorageDictionary<int,long>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,long> InterWatchLastTimeStamp
        {
            get
            {
                return interWatchLastTimeStamp;
            }
        }
        // ---------------------------------//
        
        // 上一次播放插屏的时间（全局的）
        [JsonProperty]
        long lastTime_Interstitial_Common;
        [JsonIgnore]
        public long LastTime_Interstitial_Common
        {
            get
            {
                return lastTime_Interstitial_Common;
            }
            set
            {
                if(lastTime_Interstitial_Common != value)
                {
                    lastTime_Interstitial_Common = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}