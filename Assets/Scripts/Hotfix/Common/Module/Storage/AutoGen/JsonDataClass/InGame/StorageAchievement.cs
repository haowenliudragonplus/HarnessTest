/************************************************
 * Storage class : StorageAchievement
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
    public class StorageAchievement : StorageBase
    {
        
        // 通关达人
        [JsonProperty]
        int passingExpert;
        [JsonIgnore]
        public int PassingExpert
        {
            get
            {
                return passingExpert;
            }
            set
            {
                if(passingExpert != value)
                {
                    passingExpert = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 每日挑战
        [JsonProperty]
        int dailyChallenge;
        [JsonIgnore]
        public int DailyChallenge
        {
            get
            {
                return dailyChallenge;
            }
            set
            {
                if(dailyChallenge != value)
                {
                    dailyChallenge = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 每日挑战时间
        [JsonProperty]
        long dailyChallengeTime;
        [JsonIgnore]
        public long DailyChallengeTime
        {
            get
            {
                return dailyChallengeTime;
            }
            set
            {
                if(dailyChallengeTime != value)
                {
                    dailyChallengeTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 全勤打卡 
        [JsonProperty]
        int dailyAttendanceCheckin;
        [JsonIgnore]
        public int DailyAttendanceCheckin
        {
            get
            {
                return dailyAttendanceCheckin;
            }
            set
            {
                if(dailyAttendanceCheckin != value)
                {
                    dailyAttendanceCheckin = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 全勤打卡时间
        [JsonProperty]
        long checkinTime;
        [JsonIgnore]
        public long CheckinTime
        {
            get
            {
                return checkinTime;
            }
            set
            {
                if(checkinTime != value)
                {
                    checkinTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 完美通关
        [JsonProperty]
        int perfectClear;
        [JsonIgnore]
        public int PerfectClear
        {
            get
            {
                return perfectClear;
            }
            set
            {
                if(perfectClear != value)
                {
                    perfectClear = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 关卡传奇
        [JsonProperty]
        int levelLegend;
        [JsonIgnore]
        public int LevelLegend
        {
            get
            {
                return levelLegend;
            }
            set
            {
                if(levelLegend != value)
                {
                    levelLegend = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 逆转玩家
        [JsonProperty]
        int comebackPlayer;
        [JsonIgnore]
        public int ComebackPlayer
        {
            get
            {
                return comebackPlayer;
            }
            set
            {
                if(comebackPlayer != value)
                {
                    comebackPlayer = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 心跳时刻
        [JsonProperty]
        int heartPoundingMoment;
        [JsonIgnore]
        public int HeartPoundingMoment
        {
            get
            {
                return heartPoundingMoment;
            }
            set
            {
                if(heartPoundingMoment != value)
                {
                    heartPoundingMoment = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 终极大师
        [JsonProperty]
        int ultimateMaster;
        [JsonIgnore]
        public int UltimateMaster
        {
            get
            {
                return ultimateMaster;
            }
            set
            {
                if(ultimateMaster != value)
                {
                    ultimateMaster = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 连胜专家
        [JsonProperty]
        int winStreakExpert;
        [JsonIgnore]
        public int WinStreakExpert
        {
            get
            {
                return winStreakExpert;
            }
            set
            {
                if(winStreakExpert != value)
                {
                    winStreakExpert = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 连胜专家时间
        [JsonProperty]
        long winStreakExpertTime;
        [JsonIgnore]
        public long WinStreakExpertTime
        {
            get
            {
                return winStreakExpertTime;
            }
            set
            {
                if(winStreakExpertTime != value)
                {
                    winStreakExpertTime = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
        // 完成成就
        [JsonProperty]
        StorageDictionary<int,long> collectedAchievement = new StorageDictionary<int,long>(true, true);
        [JsonIgnore]
        public StorageDictionary<int,long> CollectedAchievement
        {
            get
            {
                return collectedAchievement;
            }
        }
        // ---------------------------------//
        
        // 可以领取的成就
        [JsonProperty]
        StorageDictionary<int,long> achievementClaimable = new StorageDictionary<int,long>(true, true);
        [JsonIgnore]
        public StorageDictionary<int,long> AchievementClaimable
        {
            get
            {
                return achievementClaimable;
            }
        }
        // ---------------------------------//
        
        // 首次激活
        [JsonProperty]
        bool firstOpen;
        [JsonIgnore]
        public bool FirstOpen
        {
            get
            {
                return firstOpen;
            }
            set
            {
                if(firstOpen != value)
                {
                    firstOpen = value;
                    Profile.Instance.UpdateLocalVersion();
                    Profile.Instance.ForceSaveToDisk();
                    Profile.Instance.ForceSyncToRemote();
                }
            }
        }
        // ---------------------------------//
        
    }
}