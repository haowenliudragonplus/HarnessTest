using System;
using System.Collections.Generic;
using System.Threading;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using UnityEngine;
using YooAsset;

namespace TMGame
{
    public abstract class ActivityBase
    {
        protected bool downloadStatus = false;

        protected string activityType;

        protected string activityId;

        protected long StartTime { get; set; }

        protected long EndTime { get; set; }

        protected long RewardEndTime { get; set; }

        protected bool ManualEnd { get; set; }

        protected string StorageKey => "Activity_" + activityId;

        private CancellationTokenSource _cancellation;

        private bool _initedFromServer;

        protected ActivitySys _activitySys;

        public void OnCreate(string inActivityType, Activity serverActivity, ActivitySys inActivitySys)
        {
            activityType = inActivityType;
            activityId = serverActivity.ActivityId;
            _activitySys = inActivitySys;

            StartTime = (long)serverActivity.StartTime;
            EndTime = (long)serverActivity.EndTime;
            RewardEndTime = (long)serverActivity.RewardEndTime;
            ManualEnd = serverActivity.ManualEnd;

            _initedFromServer = true;

            InitConfig(serverActivity.Config);

            SubscribeEvents();
            SetExpireTimeCallback();
            SetCollectingTimeCallback();

            Game.GetMod<ModEvent>().Dispatch(new EventActivityCreate(activityType));

#if UNITY_EDITOR
            downloadStatus = true;
            Game.GetMod<ModEvent>().Dispatch(new EventActivityEntrance(activityType));
#endif
        }

        protected abstract void InitConfig(string config);
        protected abstract bool CheckUnlock();

        protected virtual void SubscribeEvents()
        {
        }
        public abstract UIWidgetBase CreateActivityEntrance(UIBase uiBase, Transform widgetName);

        public string GetActivityId()
        {
            return activityId;
        }

        protected abstract string GetAssetLabelName();

        #region Download

        /// <summary>
        /// 创建资源下载器
        /// </summary>
        private ResourceDownloaderOperation CreateResourceDownloader(string[] tags)
        {
            var package = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);
            var downloader = package.CreateResourceDownloader(tags, 3, 3);
            return downloader;
        }

        public void DownloadAsset()
        {
            CLog.Info($"{GetAssetLabelName()} ------ CheckUnlock : {CheckUnlock()}");
            if (CheckUnlock())
            {
                var assetLabel = GetAssetLabelName();
                CheckAndDownloadAssets(assetLabel);
            }
        }

        private async void CheckAndDownloadAssets(string assetLabel)
        {
            var result = Game.GetMod<ModAsset>().CheckTagNeedDownload(assetLabel);

            if (result == EHasAssetResult.AssetOnline)
            {
                CLog.Info($"Begin download {assetLabel}");
                var downloader = CreateResourceDownloader(new string[] { assetLabel });
                downloader.BeginDownload();
                await downloader.Task;

                if (downloader.Status == EOperationStatus.Succeed)
                {
                    CLog.Info($"Activity ------ Download {assetLabel} succeed!!!!!!!!!");
                    SetAssetDownloadStatus();
                }
                else
                {
                    CLog.Info($"Activity ------ Download {assetLabel} failed!!!!!!!!!");
                }
            }
            else
            {
                if (result == EHasAssetResult.Invalid)
                {
                    CLog.Info($"Activity ------ No {assetLabel} AssetsBundle----------");
                }
                else
                {
                    CLog.Info($"Activity ------ Download {assetLabel} succeed!!!!!!!!!");
                    SetAssetDownloadStatus();
                }
            }
        }

        public virtual void SetAssetDownloadStatus()
        {
            if (downloadStatus)
            {
                return;
            }
            var activityAssetLabel = GetAssetLabelName();
            var result = Game.GetMod<ModAsset>().CheckTagNeedDownload(activityAssetLabel);
            if (result == EHasAssetResult.AssetOnDisk)
            {
                CLog.Info($"{activityAssetLabel} set download status true");
                downloadStatus = true;
                Game.GetMod<ModEvent>().Dispatch(new EventActivityEntrance(activityType));
            }
            else
            {
                CLog.Info($"{activityAssetLabel} set download status false");
            }
        }

        #endregion

        protected virtual void SetCollectingTimeCallback()
        {
            var leftTime = GetActivityLeftTime();
            if (leftTime / 1000 > 0)
            {
                CoreUtils.WaitSeconds(leftTime / 1000,
                    () => { Game.GetMod<ModEvent>().Dispatch(new EventActivityUpdate(activityType)); }, default, true).Forget();
            }
        }

        /// <summary>
        /// 使用CountDown倒计时的活动按各活动需求重写此方法
        /// </summary>
        protected virtual void SetExpireTimeCallback()
        {
            var leftTime = GetActivityRewardLeftTime();
            if (_cancellation != null)
            {
                _cancellation.Cancel();
                _cancellation = null;
            }

            _cancellation = new CancellationTokenSource();

            CoreUtils.WaitSeconds(leftTime / 1000, OnExpire, _cancellation.Token, true).Forget();
        }

        protected virtual void OnExpire()
        {
            Game.GetMod<ActivitySys>().RemoveActivity(activityType, activityId);

            Game.GetMod<ModEvent>().Dispatch(new EventActivityExpire(activityType, activityId));

            CleanUp();
        }

        /// <summary>
        /// 活动剩余时间
        /// </summary>
        /// <returns></returns>
        public ulong GetActivityLeftTime()
        {
            var left = (long)EndTime - (long)SDK<INetwork>.Instance.GetServerTime();
            if (left < 0)
                left = 0;
            return (ulong)left;
        }

        /// <summary>
        /// 活动开启状态
        /// </summary>
        /// <returns></returns>
        public virtual bool IsActivityOpened()
        {
            if (!_initedFromServer)
                return false;
            var startTimeOk = SDK<INetwork>.Instance.GetServerTime() > StartTime;
            var endTimeOk = SDK<INetwork>.Instance.GetServerTime() < EndTime;
            return downloadStatus && !ManualEnd && startTimeOk && endTimeOk;
        }

        /// <summary>
        /// 活动领奖剩余时间
        /// </summary>
        /// <returns></returns>
        public ulong GetActivityRewardLeftTime()
        {
            var left = (long)RewardEndTime - (long)SDK<INetwork>.Instance.GetServerTime();
            if (left < 0)
                left = 0;
            return (ulong)left;
        }

        /// <summary>
        /// 活动是否在领奖期间
        /// </summary>
        /// <returns></returns>
        public virtual bool IsActivityInReward()
        {
            if (!_initedFromServer)
                return false;

            var startTimeOk = SDK<INetwork>.Instance.GetServerTime() > EndTime;
            var endTimeOk = SDK<INetwork>.Instance.GetServerTime() < RewardEndTime;

            return downloadStatus && !ManualEnd && startTimeOk && endTimeOk;
        }

        /// <summary>
        /// 活动剩余时间的字符串显示
        /// </summary>
        /// <returns></returns>
        public string GetActivityLeftTimeString()
        {
            return TimeUtils.FormatTime((long)GetActivityLeftTime());
        }

        private void CleanUp()
        {
            DisableUpdate();
        }

        #region Update

        protected bool updateEnabled = false;

        /// <summary>
        /// 0:0.9f ,1:0.5f, 2:every frame
        /// </summary>
        /// <param name="updateInterval"></param>
        protected void EnableUpdate(int updateInterval = 0)
        {
            if (updateEnabled)
            {
                DisableUpdate();
            }

            updateEnabled = true;
        }

        protected void DisableUpdate()
        {
            if (updateEnabled)
            {
                updateEnabled = false;
            }
        }

        protected virtual void Update()
        {
        }

        #endregion
    }
}