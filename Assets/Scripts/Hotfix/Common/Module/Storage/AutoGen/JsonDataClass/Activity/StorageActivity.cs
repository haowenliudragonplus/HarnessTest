/************************************************
 * Storage class : StorageActivity
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
    public class StorageActivity : StorageBase
    {
        
        // 去广告礼包
        [JsonProperty]
        StorageRemoveAd removeAd = new StorageRemoveAd();
        [JsonIgnore]
        public StorageRemoveAd RemoveAd
        {
            get
            {
                return removeAd;
            }
        }
        // ---------------------------------//
        
    }
}