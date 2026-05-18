/************************************************
 * Storage class : StorageUserData
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
    public class StorageUserData : StorageBase
    {
        
        // 首次登录时间
        [JsonProperty]
        long firstLoginTime;
        [JsonIgnore]
        public long FirstLoginTime
        {
            get
            {
                return firstLoginTime;
            }
            set
            {
                if(firstLoginTime != value)
                {
                    firstLoginTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 首次登录APP版本
        [JsonProperty]
        string firstAppVersion = "";
        [JsonIgnore]
        public string FirstAppVersion
        {
            get
            {
                return firstAppVersion;
            }
            set
            {
                if(firstAppVersion != value)
                {
                    firstAppVersion = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 首次登录资源版本
        [JsonProperty]
        string firstResVersion = "";
        [JsonIgnore]
        public string FirstResVersion
        {
            get
            {
                return firstResVersion;
            }
            set
            {
                if(firstResVersion != value)
                {
                    firstResVersion = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 上一次登录时间
        [JsonProperty]
        long lastLoginTime;
        [JsonIgnore]
        public long LastLoginTime
        {
            get
            {
                return lastLoginTime;
            }
            set
            {
                if(lastLoginTime != value)
                {
                    lastLoginTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 上一次登录APP版本
        [JsonProperty]
        string lastAppVersion = "";
        [JsonIgnore]
        public string LastAppVersion
        {
            get
            {
                return lastAppVersion;
            }
            set
            {
                if(lastAppVersion != value)
                {
                    lastAppVersion = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 上一次登录资源版本
        [JsonProperty]
        string lastResVersion = "";
        [JsonIgnore]
        public string LastResVersion
        {
            get
            {
                return lastResVersion;
            }
            set
            {
                if(lastResVersion != value)
                {
                    lastResVersion = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 音乐开关
        [JsonProperty]
        bool musicClose;
        [JsonIgnore]
        public bool MusicClose
        {
            get
            {
                return musicClose;
            }
            set
            {
                if(musicClose != value)
                {
                    musicClose = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 音效开关
        [JsonProperty]
        bool soundEffectClose;
        [JsonIgnore]
        public bool SoundEffectClose
        {
            get
            {
                return soundEffectClose;
            }
            set
            {
                if(soundEffectClose != value)
                {
                    soundEffectClose = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 震动开关
        [JsonProperty]
        bool vibrateClose;
        [JsonIgnore]
        public bool VibrateClose
        {
            get
            {
                return vibrateClose;
            }
            set
            {
                if(vibrateClose != value)
                {
                    vibrateClose = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 是否提交了删除账号请求
        [JsonProperty]
        bool isSubmitDeleteAccount;
        [JsonIgnore]
        public bool IsSubmitDeleteAccount
        {
            get
            {
                return isSubmitDeleteAccount;
            }
            set
            {
                if(isSubmitDeleteAccount != value)
                {
                    isSubmitDeleteAccount = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}