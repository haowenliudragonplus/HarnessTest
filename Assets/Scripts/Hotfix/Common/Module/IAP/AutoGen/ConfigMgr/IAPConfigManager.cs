// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * IAP ConfigHub Manager class : IAPConfigManager
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMGame;
using DragonPlus.Config;
using DragonPlus.Core;
using Framework;

namespace DragonPlus.ConfigHub.IAP
{
    public class IAPConfigManager : IConfig
    {     
        
        public static IAPConfigManager Instance
        {
            get
            {
                return SDK<IConfigHub>.Instance.Get<IAPConfigManager>();
            }
        } 

        public  string Guid => "config_iap";
        public  int VersionMinIOS => 16;
        public  int VersionMinAndroid => 16;

        public MetaData MetaData { get; set;}

        public IConfigCacheHandler CacheHandler { get; set;}

        protected  List<string> SubModules => new List<string> { 
            "Mapping",
            "RemoveAd",
        };
        private readonly Dictionary<Type, string> typeToEnum = new Dictionary<Type,string> { 
            [typeof(Mapping)] = "Mapping",
            [typeof(RemoveAd)] = "RemoveAd",
        };
        private List<Mapping> MappingList;
        private List<RemoveAd> RemoveAdList;
        
 
        public bool IsRemote {get; set;}
        public bool IsLoaded {get;set;}

        public List<T> GetConfig<T>(CacheOperate cacheOp = CacheOperate.None, long cacheDuration = -1)
        {
            if (!IsLoaded)
                SDK<IConfigHub>.Instance.LoadConfig(Guid);

            CacheHandler.ProcessCache(this, cacheOp, cacheDuration);
           
            List<T> cfg;
            var subModule = typeToEnum[typeof(T)];
            switch (subModule)
            { 
                case "Mapping": cfg = MappingList as List<T>; break;
                case "RemoveAd": cfg = RemoveAdList as List<T>; break;
                default: throw new ArgumentOutOfRangeException(nameof(subModule), subModule, null);
            }
            return cfg;
        }

        protected bool CheckTable(Hashtable table)
        {   
            if (!table.ContainsKey("mapping")) return false;
            if (!table.ContainsKey("removead")) return false;
            return true;
        }

        private bool TryParseJsonData(string configJson)
        {
            try
            {
                if (string.IsNullOrEmpty(configJson))
                    return false;
                var table = JsonConvert.DeserializeObject<Hashtable>(configJson);
                if (table == null || !CheckTable(table))
                    return false;
                foreach (var subModule in SubModules)
                {
                    switch (subModule)
                    { 
                        case "Mapping": MappingList = JsonConvert.DeserializeObject<List<Mapping>>(JsonConvert.SerializeObject(table["mapping"])); break;
                        case "RemoveAd": RemoveAdList = JsonConvert.DeserializeObject<List<RemoveAd>>(JsonConvert.SerializeObject(table["removead"])); break;
                        default: throw new ArgumentOutOfRangeException(nameof(subModule), subModule, null);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error when parse json:{e}");
                return false;
            }
        }

        public void InitConfig(MetaData metaData, string jsonData = null)
        {
            IsRemote = true;
            if (metaData == null || !TryParseJsonData(jsonData))
            {
                var ta = Game.GetMod<ModAsset>().GetRes<TextAsset>("usergroup_iap").GetInstance(Game.DontDestoryRoot);
                if (!TryParseJsonData(ta.text))
                {
                    Log.Error("Load usergroup_iap error!");
                    return;
                }
                IsRemote = false;
                metaData = CacheHandler.GetMetaDataCached(this);
                if (metaData!=null)
                {
                    CLog.Info("读取本地配置-config_iap："+metaData);
                }
            }
            
            MetaData = metaData;
            
            PropertyInfo pInfo;
            foreach (var subModule in SubModules)
            {
                if (IsRemote)
                    continue;

                switch (subModule)
                { 
                    case "Mapping": 
                        pInfo = typeof(Mapping).GetProperty("UserGroup");
                        if (pInfo != null && pInfo.PropertyType == typeof(int))
                            MappingList = MappingList.FindAll(cfg => (int)pInfo.GetValue(cfg) == metaData.GroupId);
                        break;
                    case "RemoveAd": 
                        pInfo = typeof(RemoveAd).GetProperty("UserGroup");
                        if (pInfo != null && pInfo.PropertyType == typeof(int))
                            RemoveAdList = RemoveAdList.FindAll(cfg => (int)pInfo.GetValue(cfg) == metaData.GroupId);
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(subModule), subModule, null);
                }
            }
            
            IsLoaded = true;
#if DEVELOPMENT_BUILD           
            Log.Info($"InitConfig:{CacheHandler.GetDebugString(this,false)}");
#endif            
        }

        private List<Rules> RulesList;
       
        public bool HasGroup(int groupId)
        {
            if (RulesList == null || RulesList.Count == 0)
            {
                var ta = Game.GetMod<ModAsset>().GetRes<TextAsset>("usergroup_iap").GetInstance(Game.DontDestoryRoot);
                if (string.IsNullOrEmpty(ta.text))
                {
                    Log.Error("Load usergroup_iap error!");
                    return false;
                }
                var table = JsonConvert.DeserializeObject<Hashtable>(ta.text);
                RulesList = JsonConvert.DeserializeObject<List<Rules>>(JsonConvert.SerializeObject(table["rules"]));
            }
            return RulesList.Exists(r => r.GroupId == groupId);
        }
    }
}