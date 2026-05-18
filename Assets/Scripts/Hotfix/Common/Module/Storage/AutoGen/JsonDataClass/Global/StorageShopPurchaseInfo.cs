/************************************************
 * Storage class : StorageShopPurchaseInfo
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
    public class StorageShopPurchaseInfo : StorageBase
    {
        
        // 商品ID
        [JsonProperty]
        int id;
        [JsonIgnore]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if(id != value)
                {
                    id = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 价格
        [JsonProperty]
        string price = "";
        [JsonIgnore]
        public string Price
        {
            get
            {
                return price;
            }
            set
            {
                if(price != value)
                {
                    price = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 商品类型
        [JsonProperty]
        int shopType;
        [JsonIgnore]
        public int ShopType
        {
            get
            {
                return shopType;
            }
            set
            {
                if(shopType != value)
                {
                    shopType = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 购买时候的时间戳
        [JsonProperty]
        ulong purchasedTimeStamp;
        [JsonIgnore]
        public ulong PurchasedTimeStamp
        {
            get
            {
                return purchasedTimeStamp;
            }
            set
            {
                if(purchasedTimeStamp != value)
                {
                    purchasedTimeStamp = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}