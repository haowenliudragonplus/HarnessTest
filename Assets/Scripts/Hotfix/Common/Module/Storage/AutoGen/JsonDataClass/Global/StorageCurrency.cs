/************************************************
 * Storage class : StorageCurrency
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
    public class StorageCurrency : StorageBase
    {
        
        // 加密存储
        [JsonProperty]
        float _vc0;
        [JsonIgnore]
        public float Vc0
        {
            get
            {
                return _vc0;
            }
            set
            {
                if(_vc0 != value)
                {
                    _vc0 = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    
                }
            }
        }
        // ---------------------------------//
        
        // 加密存储
        [JsonProperty]
        int _vc1;
        [JsonIgnore]
        public int Vc1
        {
            get
            {
                return _vc1;
            }
            set
            {
                if(_vc1 != value)
                {
                    _vc1 = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    
                }
            }
        }
        // ---------------------------------//
        
        // 限时结束的时间戳
        [JsonProperty]
        long timeLimitEndTime;
        [JsonIgnore]
        public long TimeLimitEndTime
        {
            get
            {
                return timeLimitEndTime;
            }
            set
            {
                if(timeLimitEndTime != value)
                {
                    timeLimitEndTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    
                }
            }
        }
        // ---------------------------------//
        
        // 上一次自动增加的时间戳
        [JsonProperty]
        long lastAutoAddTime;
        [JsonIgnore]
        public long LastAutoAddTime
        {
            get
            {
                return lastAutoAddTime;
            }
            set
            {
                if(lastAutoAddTime != value)
                {
                    lastAutoAddTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}