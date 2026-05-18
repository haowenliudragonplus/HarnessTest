using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Newtonsoft.Json;
using UnityEngine;
using YooAsset;
using Network = DragonPlus.Network.Network;

namespace TMGame
{
    public class ActivityType
    {
        public const string WinStreak = "OPS_EVENT_TYPE_WIN_STREAK";
        public const string GoldenPass = "OPS_EVENT_TYPE_GOLDEN_PASS";
        public const string SpeedRace = "OPS_EVENT_TYPE_CAR_RACE";
        public const string GoldenLeague = "OPS_EVENT_TYPE_GOLDEN_LEAGUE";
        public const string TripleGiftPack = "OPS_EVENT_TYPE_TRIPLE_GIFT_PACK";
        public const string EndlessGiftPack = "OPS_EVENT_TYPE_ENDLESS_GIFT_PACK";
        public const string EndlessGiftPackLT = "OPS_EVENT_TYPE_ENDLESS_GIFT_PACK_LT";
    }

    // 检查活动数据是否改变的数据结构
    public class ActivityCheckData
    {
        public string ActivityId;
        public ulong StartTime;
        public ulong EndTime;
        public ulong RewardEndTime;
        public bool ManualEnd;
    }

    public class ActivitySys : ModuleBase
    {
        private string _activityData = "";
        private string _activityDataFetchedTime = "0";

        //是否初始化过数据,如果初始化过不应该重新从本地再次初始化
        private bool initData;
        private bool dataHasReceived = false;
        public bool DataHasReceived => dataHasReceived;

        private CancellationTokenSource _source;

        // 缓存的  活动数据是否改变的数据结构
        private Dictionary<string, ActivityCheckData> _activityCheckDatas =
            new Dictionary<string, ActivityCheckData>();

        private Dictionary<string, Dictionary<string, ActivityBase>> _activity =
            new Dictionary<string, Dictionary<string, ActivityBase>>();

        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnDispose()
        {
            base.OnDispose();
            _activity.Clear();
            _activityCheckDatas.Clear();
            _activityData = "";
            _activityDataFetchedTime = "0";
            initData = false;
            dataHasReceived = false;
        }

        private ActivityBase GetActivity(string activityType)
        {
            if (string.IsNullOrEmpty(activityType))
            {
                return null;
            }

            Dictionary<string, ActivityBase> map;
            _activity.TryGetValue(activityType, out map);

            if (map == null || map.Count == 0)
                return null;

            return map[map.First().Key];
        }

        public T GetActivity<T>(string activityType) where T : ActivityBase
        {
            if (string.IsNullOrEmpty(activityType))
            {
                return null;
            }

            Dictionary<string, ActivityBase> map;
            _activity.TryGetValue(activityType, out map);

            if (map == null || map.Count == 0)
                return null;

            return (T)map[map.First().Key];
        }

        public void RequestActivityData(bool force)
        {
            var localServerData = _activityData;
            var timeout = _activityDataFetchedTime;

            SGetActivities serverActivities = null;
            var severActivityDataFetchCd = Game.GetMod<ModConfig>()
                .GetConstConfig<Table_Common_Global, int>("SeverActivityDataFetchCD");
            if (IsLocalConfigValid(timeout, severActivityDataFetchCd) && !force)
            {
                CLog.Info("活动---使用本地已缓存活动配置：" + localServerData);
                try
                {
                    serverActivities = JsonConvert.DeserializeObject<SGetActivities>(localServerData);
                }
                catch (Exception e)
                {
                    CLog.Error("活动---本地已缓存活动配置格式错误 : " + e.StackTrace);
                }
            }

            if (serverActivities == null)
            {
                var c = new CGetActivities();
                if (!string.IsNullOrEmpty(GameConfig.AppVersion)
                    && ulong.TryParse(GameConfig.AppVersion.Replace("v", ""), out var _version))
                {
                    c.ClientResourceVersion = _version;
                }
                CLog.Info($"活动---开始请求服务器活动配置，请求版本参数：{c.ClientResourceVersion}");
                SDK<IRemoteRequest>.Instance.HandleRequest<CGetActivities, SGetActivities>(c, activities =>
                {
                    ParseActivityServerData(activities);
                    var json = JsonConvert.SerializeObject(activities);
                    CLog.Info("活动---获取到服务器活动配置： " + json);
                    _activityData = json;
                    _activityDataFetchedTime = SDKUtil.TimeDate.CurrentTimeInMilliseconds().ToString();

#if !UNITY_EDITOR
                 BeginDownloadActivityAsset();
#endif
                    dataHasReceived = true;
                }, (arg1, arg2, arg3) =>
                {
                    CLog.Info(string.Format("活动: 获取服务器活动配置出错 " + "error code : {0}, string : {1}, message : {2}",
                        arg1.ToString(), arg2, arg3));
                    dataHasReceived = false;
                });
                return;
            }

            if (!initData)
            {
                CLog.Info("活动---活动: 使用本地缓存活动配置");
                ParseActivityServerData(serverActivities);
            }
            else
            {
                CLog.Info("活动---活动: 未拉取服务器数据,本地也初始化过了,放弃初始化");
            }
#if !UNITY_EDITOR
            BeginDownloadActivityAsset();
#endif
            dataHasReceived = true;
        }

        private static bool IsLocalConfigValid(string timeout, int CdTime)
        {
            if (string.IsNullOrEmpty(timeout))
            {
                return false;
            }

            long expire = 0;

            try
            {
                expire = long.Parse(timeout);
            }
            catch (Exception)
            {
                return false;
            }

            var severActivityDataFetchCd = Game.GetMod<ModConfig>()
                .GetConstConfig<Table_Common_Global, int>("SeverActivityDataFetchCD");
            if (expire + severActivityDataFetchCd >
                (float)SDKUtil.TimeDate.CurrentTimeInSecond())
            {
                return true;
            }

            return false;
        }

        private void ParseActivityServerData(SGetActivities data)
        {
            initData = true;

            foreach (var activity in data.Activities)
            {
                var activityType = activity.ActivityType;

                // 检查是否活动数据是否没有改变
                if (_activityCheckDatas.ContainsKey(activityType))
                {
                    var activityCheckData = _activityCheckDatas[activityType];
                    if (activity.ActivityId == activityCheckData.ActivityId
                        && activity.StartTime == activityCheckData.StartTime
                        && activity.EndTime == activityCheckData.EndTime
                        && activity.RewardEndTime == activityCheckData.RewardEndTime
                        && activity.ManualEnd == activityCheckData.ManualEnd && GetActivity(activityType) != null
                       )
                    {
                        // 完全一致,不执行活动初始化

                        CLog.Info("活动---活动: 完全一致,不执行活动初始化  " + activity.ActivityId);

                        continue;
                    }
                }
                else
                {
                    _activityCheckDatas[activityType] = new ActivityCheckData();
                }

                // 不一致,执行活动初始化
                CLog.Info("活动---活动: 不一致,执行活动初始化  " + activity.ActivityId);

                var checkData = _activityCheckDatas[activityType];
                checkData.ActivityId = activity.ActivityId;
                checkData.StartTime = activity.StartTime;
                checkData.EndTime = activity.EndTime;
                checkData.RewardEndTime = activity.RewardEndTime;
                checkData.ManualEnd = activity.ManualEnd;

                if (CheckActivityIsOpen((long)activity.StartTime, (long)activity.EndTime, (long)activity.RewardEndTime))
                {
                    var activityName =
                        GetTitleCase(activityType.Replace("OPS_EVENT_TYPE_", "").Replace("_", " ").ToLower());
                    var classType = Type.GetType($"TMGame.Activity_{activityName}");

                    if (classType != null)
                    {
                        CreateActivity(classType, activity, activityType);
                        Game.GetMod<ModEvent>().Dispatch(new EventActivityOnCreate(activityType));
                    }
                }

                Game.GetMod<ModEvent>().Dispatch(new EventActivityUpdate(activityType));
            }

            SetLeftTimeFinishCallback(data);
        }

        private string GetTitleCase(string name)
        {
            var result = "";
            var strings = name.Split(" ".ToCharArray());
            for (int i = 0; i < strings.Length; i++)
            {
                var arr = strings[i].ToCharArray();
                arr[0] = char.ToUpper(arr[0]);
                for (int j = 0; j < arr.Length; j++)
                {
                    result += string.Format($"{arr[j]}");
                }
            }

            return result;
        }

        private void AddActivity(string type, string id, ActivityBase activity)
        {
            if (activity == null)
                return;

            Dictionary<string, ActivityBase> map;
            _activity.TryGetValue(type, out map);
            if (map == null)
            {
                map = new Dictionary<string, ActivityBase>();
                _activity.Add(type, map);
            }

            map[id] = activity;
        }

        public void RemoveActivity(string type, string id)
        {
            if (string.IsNullOrWhiteSpace(type))
                return;

            if (string.IsNullOrWhiteSpace(id))
                return;

            Dictionary<string, ActivityBase> map;
            _activity.TryGetValue(type, out map);
            if (map == null)
                return;
            ActivityBase activity;
            map.TryGetValue(id, out activity);
            if (activity != null)
                map.Remove(id);

            if (map.Count == 0)
                _activity.Remove(type);
        }

        private ActivityBase CreateActivity(Type classType, Activity activity, string activityType)
        {
            try
            {
                CLog.Info("开启活动:" + activityType);
                ActivityBase curActivity = Activator.CreateInstance(classType) as ActivityBase;
                AddActivity(activityType, activity.ActivityId, curActivity);
                curActivity?.OnCreate(activityType, activity, this);
                return curActivity;
            }
            catch (Exception e)
            {
                CLog.Error(e.Message);
                throw;
            }
        }

        // 活动开启状态
        private bool CheckActivityIsOpen(long startTime, long endTime, long rewardEndTime)
        {
            var serverTime = SDK<INetwork>.Instance.GetServerTime();
            var startTimeOk = serverTime > startTime;
            var endTimeOk = serverTime < endTime;
            var rewardEndTimeOk = serverTime < rewardEndTime;

            return (startTimeOk && endTimeOk) || (startTimeOk && rewardEndTimeOk);
        }

        private void SetLeftTimeFinishCallback(SGetActivities data)
        {
            if (_source != null)
            {
                _source.Cancel();
            }
            else
            {
                _source = new CancellationTokenSource();
            }

            var severActivityDataFetchCd = Game.GetMod<ModConfig>()
                .GetConstConfig<Table_Common_Global, int>("SeverActivityDataFetchCD");
            float minLeftTime = severActivityDataFetchCd;

            foreach (var activity in data.Activities)
            {
                if (GetActivity(activity.ActivityType) != null)
                    continue;
                var leftTime = Mathf.Max(0, (long)activity.StartTime - SDK<INetwork>.Instance.GetServerTime()) / 1000;
                if (leftTime > 0)
                {
                    if (leftTime < minLeftTime)
                    {
                        minLeftTime = leftTime;
                    }
                }
            }

            CoreUtils.WaitSeconds(minLeftTime, async () => { RequestActivityData(true); }, _source.Token, true)
                .Forget();
        }

        public ulong GetUpcomingActivityTime(string activityType)
        {
            if (_activityCheckDatas != null && _activityCheckDatas.ContainsKey(activityType))
            {
                var checkData = _activityCheckDatas[activityType];
                var timeToOpen = (long)checkData.StartTime - (long)SDKRef<Network>.GetRef().GetServerTime();

                return timeToOpen > 0 ? (ulong)timeToOpen : 0;
            }

            return 0;
        }

        #region Download

        private void BeginDownloadActivityAsset()
        {
            foreach (var activities in _activity)
            {
                var activity = GetActivity(activities.Key);
                activity.DownloadAsset();
            }
        }

        #endregion
    }
}