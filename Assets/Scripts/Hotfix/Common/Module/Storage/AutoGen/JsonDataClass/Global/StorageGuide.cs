/************************************************
 * Storage class : StorageGuide
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
    public class StorageGuide : StorageBase
    {
        
        // 已完成的引导
        [JsonProperty]
        StorageDictionary<string,string> guideFinished = new StorageDictionary<string,string>(false, false);
        [JsonIgnore]
        public StorageDictionary<string,string> GuideFinished
        {
            get
            {
                return guideFinished;
            }
        }
        // ---------------------------------//
        
        // 正在进行的ID
        [JsonProperty]
        int guidingId;
        [JsonIgnore]
        public int GuidingId
        {
            get
            {
                return guidingId;
            }
            set
            {
                if(guidingId != value)
                {
                    guidingId = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}