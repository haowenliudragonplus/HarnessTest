/************************************************
 * Storage class : StoragePopups
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
    public class StoragePopups : StorageBase
    {
        
        // 今天是否弹出
        [JsonProperty]
        StorageDictionary<int,bool> dailyPopDict = new StorageDictionary<int,bool>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,bool> DailyPopDict
        {
            get
            {
                return dailyPopDict;
            }
        }
        // ---------------------------------//
        
        // 进入关卡次数
        [JsonProperty]
        StorageDictionary<int,int> enterLevelTimePopDict = new StorageDictionary<int,int>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,int> EnterLevelTimePopDict
        {
            get
            {
                return enterLevelTimePopDict;
            }
        }
        // ---------------------------------//
        
        // 上一次弹出的时间
        [JsonProperty]
        StorageDictionary<int,long> lastTimePopDict = new StorageDictionary<int,long>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,long> LastTimePopDict
        {
            get
            {
                return lastTimePopDict;
            }
        }
        // ---------------------------------//
        
        // 每天弹出次数
        [JsonProperty]
        StorageDictionary<int,int> dailyPopCountDict = new StorageDictionary<int,int>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,int> DailyPopCountDict
        {
            get
            {
                return dailyPopCountDict;
            }
        }
        // ---------------------------------//
        
    }
}