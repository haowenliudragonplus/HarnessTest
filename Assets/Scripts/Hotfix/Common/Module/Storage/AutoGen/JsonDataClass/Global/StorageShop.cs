/************************************************
 * Storage class : StorageShop
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
    public class StorageShop : StorageBase
    {
        
        // 购买次数
        [JsonProperty]
        StorageDictionary<int,int> purchaseCount = new StorageDictionary<int,int>(false, false);
        [JsonIgnore]
        public StorageDictionary<int,int> PurchaseCount
        {
            get
            {
                return purchaseCount;
            }
        }
        // ---------------------------------//
        
        // 上次购买时间
        [JsonProperty]
        long lastBuyTime;
        [JsonIgnore]
        public long LastBuyTime
        {
            get
            {
                return lastBuyTime;
            }
            set
            {
                if(lastBuyTime != value)
                {
                    lastBuyTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 购买记录
        [JsonProperty]
        StorageList<StorageShopPurchaseInfo>  shopPurchaseInfoList = new StorageList<StorageShopPurchaseInfo> (false, false);
        [JsonIgnore]
        public StorageList<StorageShopPurchaseInfo>  ShopPurchaseInfoList
        {
            get
            {
                return shopPurchaseInfoList;
            }
        }
        // ---------------------------------//
        
        // 总付费(美分)
        [JsonProperty]
        int totalPayCents;
        [JsonIgnore]
        public int TotalPayCents
        {
            get
            {
                return totalPayCents;
            }
            set
            {
                if(totalPayCents != value)
                {
                    totalPayCents = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 上次付费(美分)
        [JsonProperty]
        int lastPayCents;
        [JsonIgnore]
        public int LastPayCents
        {
            get
            {
                return lastPayCents;
            }
            set
            {
                if(lastPayCents != value)
                {
                    lastPayCents = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 付费总次数
        [JsonProperty]
        int totalPayCount;
        [JsonIgnore]
        public int TotalPayCount
        {
            get
            {
                return totalPayCount;
            }
            set
            {
                if(totalPayCount != value)
                {
                    totalPayCount = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}