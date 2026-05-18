/************************************************
 * Storage class : StorageMahjongScrew
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
    public class StorageMahjongScrew : StorageBase
    {
        
        // 每个模式对应的关卡下标
        [JsonProperty]
        StorageDictionary<int,int> levelIndexDict = new StorageDictionary<int,int>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,int> LevelIndexDict
        {
            get
            {
                return levelIndexDict;
            }
        }
        // ---------------------------------//
        
        // 每个模式对应的连赢次数
        [JsonProperty]
        StorageDictionary<int,int> winSteakCountDict = new StorageDictionary<int,int>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,int> WinSteakCountDict
        {
            get
            {
                return winSteakCountDict;
            }
        }
        // ---------------------------------//
        
        // 每个模式对应的连败次数
        [JsonProperty]
        StorageDictionary<int,int> loseSteakCountDict = new StorageDictionary<int,int>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,int> LoseSteakCountDict
        {
            get
            {
                return loseSteakCountDict;
            }
        }
        // ---------------------------------//
        
        // 进入关卡次数记录
        [JsonProperty]
        StorageDictionary<int,Dictionary<int,int>> enterCountDict = new StorageDictionary<int,Dictionary<int,int>>(true, false);
        [JsonIgnore]
        public StorageDictionary<int,Dictionary<int,int>> EnterCountDict
        {
            get
            {
                return enterCountDict;
            }
        }
        // ---------------------------------//
        
    }
}