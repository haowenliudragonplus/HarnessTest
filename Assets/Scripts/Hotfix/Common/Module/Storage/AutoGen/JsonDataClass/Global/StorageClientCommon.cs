/************************************************
 * Storage class : StorageClientCommon
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
    public class StorageClientCommon : StorageBase
    {
        
        // 用户数据
        [JsonProperty]
        StorageUserData userData = new StorageUserData();
        [JsonIgnore]
        public StorageUserData UserData
        {
            get
            {
                return userData;
            }
        }
        // ---------------------------------//
         
        // 用于存储金币，道具
        [JsonProperty]
        StorageDictionary<string,StorageCurrency> currency = new StorageDictionary<string,StorageCurrency>(false, false);
        [JsonIgnore]
        public StorageDictionary<string,StorageCurrency> Currency
        {
            get
            {
                return currency;
            }
        }
        // ---------------------------------//
        
        // 社交账号绑定奖励存档
        [JsonProperty]
        StorageSocialBind socialBind = new StorageSocialBind();
        [JsonIgnore]
        public StorageSocialBind SocialBind
        {
            get
            {
                return socialBind;
            }
        }
        // ---------------------------------//
        
        // 记录玩家引导进度
        [JsonProperty]
        StorageGuide guide = new StorageGuide();
        [JsonIgnore]
        public StorageGuide Guide
        {
            get
            {
                return guide;
            }
        }
        // ---------------------------------//
        
        // 弹窗存档
        [JsonProperty]
        StoragePopups popups = new StoragePopups();
        [JsonIgnore]
        public StoragePopups Popups
        {
            get
            {
                return popups;
            }
        }
        // ---------------------------------//
        
        // 商店购买信息
        [JsonProperty]
        StorageShop shop = new StorageShop();
        [JsonIgnore]
        public StorageShop Shop
        {
            get
            {
                return shop;
            }
        }
        // ---------------------------------//
        
        // 扩展数据
        [JsonProperty]
        StorageDictionary<string,string> extension = new StorageDictionary<string,string>(false, false);
        [JsonIgnore]
        public StorageDictionary<string,string> Extension
        {
            get
            {
                return extension;
            }
        }
        // ---------------------------------//
         
        // 扩展数据
        [JsonProperty]
        StorageDictionary<string,StorageExtensionData> extensionData = new StorageDictionary<string,StorageExtensionData>(false, false);
        [JsonIgnore]
        public StorageDictionary<string,StorageExtensionData> ExtensionData
        {
            get
            {
                return extensionData;
            }
        }
        // ---------------------------------//
        
        // BI数据
        [JsonProperty]
        StorageDictionary<int,int> bi = new StorageDictionary<int,int>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,int> Bi
        {
            get
            {
                return bi;
            }
        }
        // ---------------------------------//
        
    }
}