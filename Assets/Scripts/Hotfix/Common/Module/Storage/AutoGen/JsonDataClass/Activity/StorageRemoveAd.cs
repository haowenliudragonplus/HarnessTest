/************************************************
 * Storage class : StorageRemoveAd
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
    public class StorageRemoveAd : StorageBase
    {
        
        // 是否购买去广告
        [JsonProperty]
        bool isBuy;
        [JsonIgnore]
        public bool IsBuy
        {
            get
            {
                return isBuy;
            }
            set
            {
                if(isBuy != value)
                {
                    isBuy = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}