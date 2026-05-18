using System;
using System.Collections.Generic;
using DragonPlus.Ad;
using DragonPlus.Ad.Max;
using DragonPlus.Config;
using DragonPlus.Config.Common;
using DragonPlus.ConfigHub.Ad;
using DragonPlus.Core;
using DragonPlus.InAppPurchasing;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using UnityEngine;

namespace TMGame
{
    public enum eAdInterstitial
    {
        None = 0, //
        CompleteLevel = 1, //主线-关卡完成后点击<tap to countine>
        GameOver = 2, //主线-失败面，点击<restart>
        QuitLevel = 3, //局内设置界面，点击<restart>
    }

    public enum eAdReward
    {
        None = 0, //
        // AddSlot = 1, //加孔
        // Hint = 2, //提示道具
        // Clear = 3, //清扫道具
        // Shuffle = 4,//刷新道具
        // Box = 5,    //预留盒子
        // DoubleWin = 6,//奖励翻倍
        // Revive = 7, //复活
        Revive = 1, //失败看广告复活
        Hint,//提示
    }

    public enum eAdBanner
    {
        None = 0,
        InGame = 1, //局内
    }

    public class AdModel
    {
        private StorageAd _storageBase;

        public AdModel()
        {
            _storageBase = SDK<IStorage>.Instance.Get<StorageAd>();
        }

        public bool CheckUnlockRewardContainsKey(int key)
        {
            return _storageBase.UnlockRewardState.ContainsKey(key);
        }

        public bool GetUnlockRewardStateKeyValue(int key)
        {
            return _storageBase.UnlockRewardState[key];
        }

        public void SetUnlockRewardStateKeyValue(int key, bool value)
        {
            _storageBase.UnlockRewardState[key] = value;
        }

        public void AddKeyRewardState(int key)
        {
            _storageBase.UnlockRewardState.Add(key, false);
        }

        public long GetResetStateTime()
        {
            return _storageBase.ResetStateTime;
        }

        public void ResetState()
        {
            _storageBase.ResetStateTime = SDKUtil.TimeDate.GetTomorrowTimestamp();
            _storageBase.RewardWatchLastTimeStamp.Clear();
            _storageBase.RewardWatchCount.Clear();
            _storageBase.InterWatchLastTimeStamp.Clear();
            _storageBase.InterWatchCount.Clear();
            Debug.Log($"[AD] ResetState.");
        }

        public long GetRewardWatchLastTimeStamp(int key)
        {
            long lastTimeStamp;
            _storageBase.RewardWatchLastTimeStamp.TryGetValue(key, out lastTimeStamp);
            return lastTimeStamp;
        }

        public int GetRewardWatchCount(int key)
        {
            int count;
            _storageBase.RewardWatchCount.TryGetValue(key, out count);
            return count;
        }

        public bool CheckRewardWatchCountContainsKey(int key)
        {
            return _storageBase.RewardWatchCount.ContainsKey(key);
        }

        public void AddRewardWatchCountKey(int key)
        {
            _storageBase.RewardWatchCount.Add(key, 0);
        }

        public void UpdateRewardWatchCountKeyValue(int key, int count)
        {
            _storageBase.RewardWatchCount[key] = _storageBase.RewardWatchCount[key] + count;
        }

        public bool CheckRewardWatchLastTimeStampContainsKey(int key)
        {
            return _storageBase.RewardWatchLastTimeStamp.ContainsKey(key);
        }

        public void AddRewardWatchLastTimeStampKey(int key, long nowStamp)
        {
            _storageBase.RewardWatchLastTimeStamp.Add(key, nowStamp);
        }

        public void UpdateRewardWatchLastTimeStampValue(int key, long nowStamp)
        {
            _storageBase.RewardWatchLastTimeStamp[key] = nowStamp;
        }

        public bool CheckUnlockInterContainsKey(int key)
        {
            return _storageBase.UnlockInterState.ContainsKey(key);
        }

        public bool GetUnlockInterStateKeyValue(int key)
        {
            return _storageBase.UnlockInterState[key];
        }

        public void SetUnlockInterStateKeyValue(int key, bool value)
        {
            _storageBase.UnlockInterState[key] = value;
        }

        public void AddKeyInterState(int key)
        {
            _storageBase.UnlockInterState.Add(key, false);
        }

        public bool CheckInterWatchCount(int key)
        {
            return _storageBase.InterWatchCount.ContainsKey(key);
        }

        public void AddInterWatchCountKey(int key)
        {
            _storageBase.InterWatchCount.Add(key, 0);
        }

        public void AddInterWatchCountValue(int key, int value)
        {
            _storageBase.InterWatchCount[key] = _storageBase.InterWatchCount[key] + value;
        }

        public int GetInterWatchCountValue(int key)
        {
            int count;
            _storageBase.InterWatchCount.TryGetValue(key, out count);
            return count;
        }

        public bool CheckInterWatchLastTimeStampContainsKey(int key)
        {
            return _storageBase.InterWatchLastTimeStamp.ContainsKey(key);
        }

        public void AddInterWatchLastTimeStampKey(int key, long nowStamp)
        {
            _storageBase.InterWatchLastTimeStamp.Add(key, nowStamp);
        }

        public void UpdateInterWatchLastTimeStampValue(int key, long nowStamp)
        {
            _storageBase.InterWatchLastTimeStamp[key] = nowStamp;
        }

        public long GetInterWatchLastTimeStamp(int key)
        {
            long lastTimeStamp;
            _storageBase.InterWatchLastTimeStamp.TryGetValue(key, out lastTimeStamp);
            return lastTimeStamp;
        }

        public void RecordInterWatchLastTimeStamp(eAdInterstitial pos)
        {
            int key = (int)pos;
            var nowStamp = SDKUtil.TimeDate.CurrentTimeInSecond();
            if (!_storageBase.InterWatchLastTimeStamp.ContainsKey(key)) _storageBase.InterWatchLastTimeStamp.Add(key, nowStamp);
            _storageBase.InterWatchLastTimeStamp[key] = nowStamp;
        }

        public StorageAd GetStorage()
        {
            return _storageBase;
        }

        /// <summary>
        /// 获取剩余广告次数
        /// </summary>
        public int GetRemainCount_AdReward(eAdReward adRewardType)
        {
            var modAd = Game.GetMod<AdSys>();
            int remainCount = modAd.GetAdRewardCfg(adRewardType).LimitPerDay - Game.GetMod<AdSys>().Model.GetRewardWatchCount((int)adRewardType);
            return remainCount;
        }
    }

    public class AdSys : ModuleBase
    {
        private Dictionary<int, AdReward> AdRewardConfigs = new Dictionary<int, AdReward>();
        private Dictionary<int, AdInterstitial> AdInterstitialConfigs = new Dictionary<int, AdInterstitial>();
        private Dictionary<int, AdBanner> AdBannerConfigs = new Dictionary<int, AdBanner>();

        private AdModel _model;
        public AdModel Model => _model;

        private bool _isInit;

        private Action<AdPlayResult, string> _rewardCallBack = null;

        private bool _showingBanner;

        public bool ShowingBanner
        {
            get
            {
                return _showingBanner;
            }
            set
            {
                _showingBanner = value;
            }
        }

        // 测试rv广告模式
        private bool inDebugRVMode;
        public bool InDebugRVMode
        {
            get
            {
                if (Main.GameUtils.IsInEditorEnv())
                    return true;
                else if (!Main.GameUtils.IsDevelopmentEnv())
                    inDebugRVMode = false;
                return inDebugRVMode;
            }
            set
            {
                inDebugRVMode = value;
            }
        }

        public override void OnLoginSuccess()
        {
            base.OnLoginSuccess();

            _model = new AdModel();
            ResetConfig();

            if (_isInit)
                return;
            _isInit = true;
            /////////////激励视频////////////////////
            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.RewardVideo,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdNewuserlevel,
                pos => !IsRewardUnlock((eAdReward)Enum.Parse(typeof(eAdReward), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.RewardVideo,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason
                    .CommonMonetizationEventReasonAdSeperateCooldown,
                pos => IsRewardInCD((eAdReward)Enum.Parse(typeof(eAdReward), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.RewardVideo,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdOverdisplay,
                pos => IsRewardCountReachUpperLimit((eAdReward)Enum.Parse(typeof(eAdReward), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.RewardVideo,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdNofill,
                pos => !SDK<IAdProvider>.Instance.IsRewardVideoReady() && !Main.GameUtils.IsInEditorEnv() && !InDebugRVMode);

            ////////插屏///////
            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdNewuserlevel,
                pos => !IsInterUnlock((eAdInterstitial)Enum.Parse(typeof(eAdInterstitial), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdPaid,
                pos => IsRemoveAd());

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdCommonCooldown,
                pos => IsInterstitialInCD_Common((eAdInterstitial)Enum.Parse(typeof(eAdInterstitial), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdSeperateCooldown,
                pos => IsInterInCD((eAdInterstitial)Enum.Parse(typeof(eAdInterstitial), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdOverdisplay,
                pos => IsInterCountReachUpperLimit((eAdInterstitial)Enum.Parse(typeof(eAdInterstitial), pos)));

            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Interstitial,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdNofill,
                pos => !SDK<IAdProvider>.Instance.IsInterstitialReady() && !Main.GameUtils.IsInEditorEnv());

            ///////////////Banner////////////////////
            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Banner,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdNofill,
                pos => !IsBannerUnlock((eAdBanner)Enum.Parse(typeof(eAdBanner), pos)));
            SDK<IAdProvider>.Instance.RegisterFailDelegate(AD_Type.Banner,
                BiEventCommon.Types.CommonMonetizationAdEventFailedReason.CommonMonetizationEventReasonAdPaid,
                pos => IsRemoveAd());
        }

        private void OnEventConfigHubUpdated(EventConfigHubUpdatedEvent evt)
        {
            ResetConfig();
        }

        /// <summary>
        /// 重置配置
        /// </summary>
        private void ResetConfig()
        {
            Debug.Log("Fetch Remote Config, ResetConfig");
            AdRewardConfigs.Clear();
            var adRewardAll = AdConfigManager.Instance.GetConfig<AdReward>();
            var cfgAdReward = adRewardAll.FindAll(c => c.GroupId == GetAdRewardCurrentGroup());
            foreach (var cfg in cfgAdReward)
            {
                AdRewardConfigs[cfg.PlaceId] = cfg;
            }

            AdInterstitialConfigs.Clear();
            var adInterstitialAll = AdConfigManager.Instance.GetConfig<AdInterstitial>();
            var cfgAdInterstitial = adInterstitialAll.FindAll(c => c.GroupId == GetAdInterstitialCurrentGroup());
            foreach (var cfg in cfgAdInterstitial)
            {
                AdInterstitialConfigs[cfg.PlaceId] = cfg;
            }

            AdBannerConfigs.Clear();
            var adBannerAll = AdConfigManager.Instance.GetConfig<AdBanner>();
            var cfgAdBanner = adBannerAll.FindAll(c => c.GroupId == GetBannerCurrentGroup());
            foreach (var cfg in cfgAdBanner)
            {
                AdBannerConfigs[cfg.PlaceId] = cfg;
            }
        }

        public int GetCurGroup()
        {
            var groups = AdConfigManager.Instance.GetConfig<Mapping>();
            if (groups != null && groups.Count > 0)
            {
                var group = groups[0].UserGroup;
                return group;
            }
            return 0;
        }

        /// <summary>
        /// 获取当前mapping
        /// </summary>
        public DragonPlus.ConfigHub.Ad.Mapping GetCurMapping()
        {
            var groups = GetMappingList();
            if (groups != null && groups.Count > 0)
            {
                return groups[0];
            }
            return null;
        }

        private List<DragonPlus.ConfigHub.Ad.Mapping> GetMappingList()
        {
            return AdConfigManager.Instance.GetConfig<DragonPlus.ConfigHub.Ad.Mapping>();
        }

        public override void OnInit()
        {
            base.OnInit();
            EventBus.Subscribe<EventConfigHubUpdatedEvent>(OnEventConfigHubUpdated);
        }

        /// <summary>
        /// 获取 AdReward 当前用户分层
        /// </summary>
        /// <returns></returns>
        public int GetAdRewardCurrentGroup()
        {
            var groups = AdConfigManager.Instance.GetConfig<Mapping>();
            if (groups != null && groups.Count > 0)
            {
                //Debug.Log($"RewardGroup:{groups[0].AdReward}");
                return groups[0].AdReward;
            }
            //Debug.Log($"RewardGroup:Default 100");
            return 300;
        }

        /// <summary>
        /// 获取 AdInterstitial 当前用户分层
        /// </summary>
        /// <returns></returns>
        public int GetAdInterstitialCurrentGroup()
        {
            var groups = AdConfigManager.Instance.GetConfig<Mapping>();
            if (groups != null && groups.Count > 0) return groups[0].AdInterstitial;
            return 200;
        }

        /// <summary>
        /// 获取 Banner 当前用户分层
        /// </summary>
        /// <returns></returns>
        public int GetBannerCurrentGroup()
        {
            var groups = AdConfigManager.Instance.GetConfig<Mapping>();
            if (groups != null && groups.Count > 0) return groups[0].AdBanner;
            return 100;
        }

        /// <summary>
        /// 激励视频是否解锁
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsRewardUnlock(eAdReward pos)
        {
            int key = (int)pos;
            if (!_model.CheckUnlockRewardContainsKey(key))
            {
                _model.AddKeyRewardState(key);
            }

            if (!AdRewardConfigs.ContainsKey(key))
            {
                return false;
            }
            int unlockLevel = AdRewardConfigs[key].UnlockLevel;
            if (Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1 >= unlockLevel)
            {
                _model.SetUnlockRewardStateKeyValue(key, true);
            }
            return _model.GetUnlockRewardStateKeyValue(key);
        }

        /// <summary>
        /// 激励视频是否在CD中
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsRewardInCD(eAdReward pos)

        {
            TryToResetState();
            int key = (int)pos;
            long lastTimeStamp = _model.GetRewardWatchLastTimeStamp(key);
            AdReward cfg;
            AdRewardConfigs.TryGetValue(key, out cfg);
            long tempValue = cfg != null ? cfg.ShowInterval : 0;
            return SDKUtil.TimeDate.CurrentTimeInSecond() - lastTimeStamp < tempValue;
        }

        /// <summary>
        /// 尝试重置状态
        /// </summary>
        private void TryToResetState()
        {
            var currentTime = SDKUtil.TimeDate.CurrentTimeInSecond();
            if (currentTime > _model.GetResetStateTime())
            {
                _model.ResetState();
                ResetConfig();
            }
        }

        /// <summary>
        /// 激励视频次数是否已达上限
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsRewardCountReachUpperLimit(eAdReward pos)
        {
            TryToResetState();
            int key = (int)pos;
            int count = _model.GetRewardWatchCount(key);
            AdReward cfg;
            AdRewardConfigs.TryGetValue(key, out cfg);
            long tempValue = cfg != null ? cfg.LimitPerDay : 0;
            if (cfg != null)
            {
                Debug.Log($"RewardCountLimit:Count:{tempValue},cfgId:{cfg.Id}");
            }
            else
            {
                Debug.Log($"RewardCountLimit:{tempValue}");
            }
            return count >= tempValue;
        }

        /// <summary>
        /// 激励视频播放后回调
        /// </summary>
        /// <param name="pos"></param>
        private void OnRewardPlay(eAdReward pos)
        {
            int key = (int)pos;
            if (!_model.CheckRewardWatchCountContainsKey(key))
            {
                _model.AddRewardWatchCountKey(key);
            }

            _model.UpdateRewardWatchCountKeyValue(key, 1);

            var nowStamp = SDKUtil.TimeDate.CurrentTimeInSecond();
            if (!_model.CheckRewardWatchLastTimeStampContainsKey(key))
                _model.AddRewardWatchLastTimeStampKey(key, nowStamp);
            _model.UpdateRewardWatchLastTimeStampValue(key, nowStamp);
        }

        /// <summary>
        /// 插频是否解锁
        /// </summary>
        public bool IsInterUnlock(eAdInterstitial pos)
        {
            int key = (int)pos;

            if (!_model.CheckUnlockInterContainsKey(key))
            {
                _model.AddKeyInterState(key);
            }

            if (!AdInterstitialConfigs.ContainsKey(key))
            {
                return false;
            }

            int unlockLevel = AdInterstitialConfigs[key].UnlockLevel;
            if (Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1 >= unlockLevel &&
                !_model.GetUnlockInterStateKeyValue(key))
                _model.SetUnlockInterStateKeyValue(key, true);
            return _model.GetUnlockInterStateKeyValue(key);
        }

        /// <summary>
        /// 插屏播放前回调
        /// </summary>
        /// <param name="pos"></param>
        private void OnInterPlay(eAdInterstitial pos)
        {
            int key = (int)pos;
            if (!_model.CheckInterWatchCount(key))
            {
                _model.AddInterWatchCountKey(key);
            }

            _model.AddInterWatchCountValue(key, 1);

            var nowStamp = SDKUtil.TimeDate.CurrentTimeInSecond();
            if (!_model.CheckInterWatchLastTimeStampContainsKey(key))
                _model.AddInterWatchLastTimeStampKey(key, nowStamp);
            _model.UpdateInterWatchLastTimeStampValue(key, nowStamp);

            _model.GetStorage().LastTime_Interstitial_Common = TimeUtils.GetServerTimeStamp();
        }

        public void RecordInterWatchLastTimeStamp(eAdInterstitial pos)
        {
            _model.RecordInterWatchLastTimeStamp(pos);
        }

        /// <summary>
        /// 插屏是否在CD中
        /// </summary>
        private bool IsInterInCD(eAdInterstitial pos)
        {
            TryToResetState();
            int key = (int)pos;
            long lastTimeStamp = _model.GetInterWatchLastTimeStamp(key);
            AdInterstitial cfg;
            AdInterstitialConfigs.TryGetValue(key, out cfg);
            int cd = 0;
            if (cfg != null)
            {
                cd = AdInterstitialConfigs[key].ShowInterval;
            }
            return SDKUtil.TimeDate.CurrentTimeInSecond() - lastTimeStamp < cd;
        }

        /// <summary>
        /// 插屏是否在CD中（全局的）
        /// </summary>
        private bool IsInterstitialInCD_Common(eAdInterstitial pos)
        {
            TryToResetState();

            long lastTime_Interstitial_Common = _model.GetStorage().LastTime_Interstitial_Common;
            if (lastTime_Interstitial_Common <= 0)
                return false;

            int key = (int)pos;
            AdInterstitial cfg;
            AdInterstitialConfigs.TryGetValue(key, out cfg);
            int cd = 0;
            if (cfg != null)
            {
                cd = AdInterstitialConfigs[key].InterstitiaADGlobalCD;
            }
            bool ret = TimeUtils.GetServerTimeStamp() - lastTime_Interstitial_Common < cd;
            return ret;
        }

        /// <summary>
        /// 插屏次数是否已达上限
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsInterCountReachUpperLimit(eAdInterstitial pos)
        {
            TryToResetState();
            int key = (int)pos;
            int count = _model.GetInterWatchCountValue(key);
            AdInterstitial cfg;
            AdInterstitialConfigs.TryGetValue(key, out cfg);
            long tempValue = cfg != null ? cfg.LimitPerDay : 0;
            return count >= tempValue;
        }

        public int GetRewardLastCount(eAdReward pos)
        {
            TryToResetState();
            int key = (int)pos;
            int count = _model.GetRewardWatchCount(key);
            AdReward cfg;
            AdRewardConfigs.TryGetValue(key, out cfg);
            long tempValue = cfg != null ? cfg.LimitPerDay : 0;
            return (int)(tempValue - count);
        }

        public bool ShouldShowRV(eAdReward pos, bool bi = true)
        {
            return SDK<IAdProvider>.Instance.ShouldShowRewardVideo(pos.ToString(), bi);
        }

        public bool ShouldShowInterstitial(eAdInterstitial pos, bool withBI = true)
        {
            return SDK<IAdProvider>.Instance.ShouldShowInterstitial(pos.ToString(), withBI);
        }

        public bool TryShowRewardedVideo(eAdReward pos, Action<AdPlayResult, string> cb)
        {
            if (InDebugRVMode)
            {
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    title = "Debug播放广告",
                    content = "是否播放广告",
                    onMidBtn = () =>
                    {
                        if (cb != null) _rewardCallBack = cb;
                        OnRewardPlay(pos);
                        _rewardCallBack?.Invoke(AdPlayResult.Success, "");
                    },
                    showMidBtn = true,
                    useText = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
                return true;
            }

            //
            if (!_isInit)
            {
                CLog.Error("广告模块未初始化!!!!!");
                return false;
            }

            CLog.Error($"尝试播放激励视频广告：[{pos}]");
            if (SDK<IAdProvider>.Instance.TryShowRewardVideo(pos.ToString(), (b, s) =>
                {
                    OnRewardPlay(pos);
                    _rewardCallBack?.Invoke(b, s);
                    Game.GetMod<ModAudio>().UnPauseBGM();
                }))
            {
                if (cb != null) _rewardCallBack = cb;
                Game.GetMod<ModAudio>().PauseBGM();
                Game.GetMod<ModAudio>().StopAllSound();
                return true;
            }
            else
            {
                Game.GetMod<ModTip>().ShowTip(CoreUtils.GetLocalization("adbox_no_reward"));
                return false;
            }
        }

        public bool TryShowInterstitial(eAdInterstitial pos)
        {
            if (!_isInit)
            {
                Debug.Log($"[AD] TryShowInterstitial [0] : {pos}");
                return false;
            }

            var lv = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main).ToString();
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventInterstitialShowOpportunity, lv,
                ((int)pos).ToString());
            Debug.Log($"[AD] TryShowInterstitial [1] : {pos}");
            if (SDK<IAdProvider>.Instance.TryShowInterstitial(pos.ToString(), (b, s) =>
                {
                    Debug.Log($"[AD] TryShowInterstitial [4] : {pos}");
                    Game.GetMod<ModAudio>().UnPauseBGM();
                    if (b == AdPlayResult.Success)
                    {
                        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventInterstitialShow, lv,
                            ((int)pos).ToString());
                    }
                }))
            {
                Debug.Log($"[AD] TryShowInterstitial [2] : {pos}");
                Game.GetMod<ModAudio>().PauseBGM();
                Game.GetMod<ModAudio>().StopAllSound();
                OnInterPlay(pos);
                return true;
            }

            Debug.Log($"[AD] TryShowInterstitial [3] : {pos}");
            return false;
        }

        public bool IsRemoveAd()
        {
            bool b = Game.GetMod<ModActivity_RemoveAd>().IsBuy();
            return b;
        }

        public bool IsInterstitialReady(eAdInterstitial pos)
        {
            return SDK<IAdProvider>.Instance.IsInterstitialReady();
        }

        public bool IsRewardVideoReady(eAdReward pos)
        {
            return SDK<IAdProvider>.Instance.IsRewardVideoReady();
        }

        /// <summary>
        /// banner是否解锁
        /// </summary>
        private bool IsBannerUnlock(eAdBanner pos)
        {
            int key = (int)pos;
            var isUnlock = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1 >= AdBannerConfigs[key].UnlockLevel;
            return isUnlock;
        }

        public bool IsRemoveBannerAd()
        {
            return false;
        }

        public void TryShowCloseBanner()
        {
            // if (Game.GetMod<UIManager>().GetWindow<UITripleMatchOut>() == null && ShowingBanner)
            // {
            //    TryShowBanner(eAdBanner.TMatch);
            // }
        }

        public bool TryShowBanner(eAdBanner pos)
        {
            if (!_isInit)
            {
                Debug.Log($"[AD] TryShowBanner [0] : {pos}");
                return false;
            }

#if UNITY_EDITOR
            _showingBanner = true;
            OnBannerPlay(pos);
            return true;
#else
            Debug.Log($"[AD] TryShowBanner : {pos}");
            if (SDK<IAdProvider>.Instance.TryShowBanner(pos.ToString()))
            {
                Debug.Log($"[AD] TryShowBanner success : {pos}");
                _showingBanner = true;
                OnBannerPlay(pos);
                return true;
            }
            Debug.Log($"[AD] TryShowBanner fail : {pos}");
            return false;
#endif
        }

        public void HideBanner()
        {
            _showingBanner = false;
#if UNITY_EDITOR
            return;
#endif
            SDK<IAdProvider>.Instance.HideBanner();
        }

        /// <summary>
        /// Banner播放前回调
        /// </summary>
        private void OnBannerPlay(eAdBanner pos)
        {
            CLog.Info("OnBannerPlay");
            int key = (int)pos;
            Game.GetMod<ModEvent>().Dispatch(new EvtPlayBanner(pos));
            // if (!StorageAd.InterWatchCount.ContainsKey(key)) StorageAd.InterWatchCount.Add(key, 0);
            // StorageAd.InterWatchCount[key]++;
            //
            // var nowStamp = Utils.TotalSeconds();
            // if(!StorageAd.InterWatchLastTimeStamp.ContainsKey(key)) StorageAd.InterWatchLastTimeStamp.Add(key, nowStamp);
            // StorageAd.InterWatchLastTimeStamp[key] = nowStamp;
            //
            // interCommonCDStamp = 0;
        }

        public AdReward GetAdRewardCfg(eAdReward adRewardType)
        {
            if (AdRewardConfigs.TryGetValue((int)adRewardType, out var _cfg))
                return _cfg;
            return null;
        }

        public override void OnDispose()
        {
            base.OnDispose();
            EventBus.Unsubscribe<EventConfigHubUpdatedEvent>(OnEventConfigHubUpdated);
        }
    }
}