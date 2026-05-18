using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonPlus.Tracking;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using Google.Protobuf;
using TMGame;
using UnityEngine;

// bi表需要打的内容：
//ThirdPartyTracking 市场需求
//Common  
//Levelinfo 公司统一分析的
//GameEventType 
//ItemChangeReason 
//Item
public class BIHelper
{
    /// <summary>
    /// 总的发送bi事件接口
    /// </summary>
    public static void SendEvent(IMessage message)
    {
        try
        {
            var common = new BiEventArrowPuzzle1.Types.Common();
            if (Game.GameInited)
            {
                var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
                // var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
                common = new BiEventArrowPuzzle1.Types.Common
                {

                    // CurrentCoin = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Coin),
                    // CurrentEnergy = (ulong)Game.GetMod<EnergySys>().GetEnergy(),
                    // MaxAreaId = (uint)RoomManager.Instance.CurRoomId,
                    MaxLevelId = (ulong)Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main),
                    SettingLanguage = storageCommon.Locale,
                    Offline = !Main.GameUtils.HasNetwork(),
                    //InfinityEnergy = Game.GetMod<EnergySys>().IsInfiniteEnergy(),
                    Adcommonid = Game.GetMod<AdSys>().GetCurGroup().ToString(),
                    // CurrentMagnet = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Magnet),
                    // CurrentBroom = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Broom),
                    // CurrentWindmill = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Compass),
                    // CurrentFrozen = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Frozen),
                    // Iap = StorageClientCommon.Shop.LastBuyTime == 0
                    //     ? false
                    //     : TMUtility.IsSameDay((ulong)SDK<INetwork>.Instance.GetServerTime() / 1000, (ulong)(StorageClientCommon.Shop.LastBuyTime / 1000)),
                    // CurrentIapgroupid = Game.GetMod<IAPSys>().GetClientGroupModel().GetClientGroupId(),
                    // CurrentIaprulesid = StorageClientCommon.ClientGroup.ClientType,

                    // CurrentShuffle = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Shuffle),
                    // CurrentHint = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Hint),
                    // CurrentClean = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Clear),
                    // CurrentAddslot = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.AddSlot),
                    // CurrentCoin = (ulong)Game.GetMod<ModBag>().GetItemCount(EItemType.Coin)
                };
            }

            var biEvent = new BiEventArrowPuzzle1() { Common = common };
            var messageName = message.GetType().Name;
            biEvent.GetType().InvokeMember(messageName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, biEvent,
                new object[] { message });
            SDK<ITracking>.Instance.SendBiTracking(biEvent);
        }
        catch (Exception e)
        {
            CLog.Error(e.ToString());
        }
    }

    #region ItemChangeReason

    public struct ItemChangeReasonArgs
    {
        public BiEventArrowPuzzle1.Types.ItemChangeReason reason;
        public string data1;
        public string data2;
        public string data3;

        public ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason reason)
        {
            this.reason = reason;
            data1 = null;
            data2 = null;
            data3 = null;
        }
    }

    public static void SendItemChangeEvent(EItemType itemType, long amount, ulong current,
        ItemChangeReasonArgs args)
    {
        var itemChangeEvent = new BiEventArrowPuzzle1.Types.ItemChange
        {
            Item = GetBiItemType(itemType),
            Reason = args.reason,
            Amount = amount,
            Current = current,
        };
        if (!string.IsNullOrEmpty(args.data1))
        {
            itemChangeEvent.Data1 = args.data1;
        }

        if (!string.IsNullOrEmpty(args.data2))
        {
            itemChangeEvent.Data2 = args.data2;
        }

        if (!string.IsNullOrEmpty(args.data3))
        {
            itemChangeEvent.Data3 = args.data3;
        }

        SendEvent(itemChangeEvent);
    }

    public static BiEventArrowPuzzle1.Types.Item GetBiItemType(EItemType itemType)
    {
        BiEventArrowPuzzle1.Types.Item retItemType = itemType switch
        {
            // EItemType.Shuffle => BiEventArrowPuzzle1.Types.Item.Shuffle,
            // EItemType.Hint => BiEventArrowPuzzle1.Types.Item.Hint,
            // EItemType.Clear => BiEventArrowPuzzle1.Types.Item.Clean,
            // EItemType.AddSlot => BiEventArrowPuzzle1.Types.Item.Addslot,
            EItemType.Coin => BiEventArrowPuzzle1.Types.Item.Coin,
            _ => BiEventArrowPuzzle1.Types.Item.None
        };
        return retItemType;
    }

    #endregion ItemChangeReasonArgs

    #region GameEvent

    public static void SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType gameEventType,
        string data1 = null, string data2 = null, string data3 = null, string data4 = null)
    {
        var gameEvent = new BiEventArrowPuzzle1.Types.GameEvent
        {
            GameEventType = gameEventType,
        };

        if (!string.IsNullOrEmpty(data1))
        {
            gameEvent.Data1 = data1;
        }

        if (!string.IsNullOrEmpty(data2))
        {
            gameEvent.Data2 = data2;
        }

        if (!string.IsNullOrEmpty(data3))
        {
            gameEvent.Data3 = data3;
        }

        if (!string.IsNullOrEmpty(data4))
        {
            gameEvent.Data4 = data4;
        }

        if (IsFirstBI(gameEventType))
        {
            if (!IsOnceBiFinished(gameEventType))
            {
                SendEvent(gameEvent);
                SetOnceBIFinished(gameEventType);
            }
        }
        else
        {
            SendEvent(gameEvent);
        }
    }

    public static void SendGameEvent_ViewShow(int viewId, int showType, int giftPackId = -1)
    {
        // SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventPageShow,
        //     viewId.ToString(), showType.ToString(), giftPackId.ToString());
    }

    /// <summary>
    /// 点击界面上的任意按钮
    /// </summary>
    /// showType 1：主动弹出 2：其他
    public static void SendGameEvent_ViewClickBtn(bool byClose, int viewId, int showType, int giftPackId = -1)
    {
        // if (!byClose)
        // {
        //     SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventIapShopIdClick,
        //         viewId.ToString(), showType.ToString(), giftPackId.ToString());
        // }
        // else
        // {
        //     SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventPageClose,
        //         viewId.ToString(), showType.ToString(), giftPackId.ToString());
        // }
    }

    public static void SendGameEvent_ShowRvBtn(eAdReward adRewardType, int itemId = -1)
    {
        // int curLevelId = Game.GetMod<ModFindTM>().GeCurMainLevelIndex();
        // SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvShow,
        //     curLevelId.ToString(), ((int)adRewardType).ToString(), itemId.ToString());
    }

    public static void SendGameEvent_ClickRvBtn(eAdReward adRewardType, int itemId = -1)
    {
        // int curLevelId = Game.GetMod<ModFindTM>().GeCurMainLevelIndex();
        // SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvClick,
        //     curLevelId.ToString(), ((int)adRewardType).ToString(), itemId.ToString());
    }

    private static bool IsFirstBI(BiEventArrowPuzzle1.Types.GameEventType et)
    {
        return et.ToString().Contains("Fte");
    }

    private static bool IsOnceBiFinished(BiEventArrowPuzzle1.Types.GameEventType et)
    {
        return SDK<IStorage>.Instance.Get<StorageClientCommon>().Bi.ContainsKey((int)et);
    }

    private static void SetOnceBIFinished(BiEventArrowPuzzle1.Types.GameEventType et)
    {
        var biStorage = SDK<IStorage>.Instance.Get<StorageClientCommon>().Bi;
        if (!biStorage.ContainsKey((int)et))
        {
            biStorage.Add((int)et, (int)et);
        }
    }

    #endregion GameEvent

    #region LevelInfo

    /// <summary>
    /// LevelInfo的打点时机类型
    /// </summary>
    public enum ELevelInfoType
    {
        [Description("enter")]
        Enter = 1,
        [Description("pass")]
        Pass,
        [Description("fail")]
        Fail,
        [Description("quit")]
        Quit,
        [Description("restart")]
        Restart,
    }

    public static void SendLevelInfo(InGameDataBase mode, ELevelInfoType levelInfoType)
    {
        var storageMahjong = SDK<IStorage>.Instance.Get<StorageMahjongScrew>();
        BiEventArrowPuzzle1.Types.LevelInfo levelInfo = new BiEventArrowPuzzle1.Types.LevelInfo
        {
            LevelCount = (uint)(mode.LevelNum),
            LevelId = (uint)(mode.LevelIndex),
            LevelDifficulty = (uint)mode.LevelCfg.DifficultyType,
            LevelTime = (uint)(int)mode.Duration,
            LevelResult = levelInfoType.ToDes(),
            EnterTime = (uint)mode.GetEnterCount(mode.ModeType, mode.LevelIndex),
            ContinueWin = storageMahjong.WinSteakCountDict.TryGetValue((int)mode.ModeType, out var _winStreakCount) ? (uint)_winStreakCount : 0,
            // ContinueFail = storageMahjong.LoseSteakCountDict.TryGetValue((int)mode.ModeType, out var _loseStreakCount) ? (uint)_loseStreakCount : 0,
            AuxiliarylineNumber = (uint)mode.AuxiliaryCount,
            LevelType = (uint)mode.ModeType,
            // ReviveCountOutspace = (uint)mode.ReviveCount,
            FinishCount = (uint)mode.RemainArrowCount,
            TotalCount = (uint)mode.TotalArrowCount,
            ReviveNumberRv = mode.Group_HpOrStep != EABTestGroup.Group2 ? (uint)mode.ReviveCount : 0,
            Hint = (uint)mode.HintCount,
            TotalStepcount = mode.Group_HpOrStep == EABTestGroup.Group2 ? (uint)mode.Step : 0,
            LeftStepcount = mode.Group_HpOrStep == EABTestGroup.Group2 ? (uint)mode.CurStep : 0,
            ReviveCountNostep = mode.Group_HpOrStep == EABTestGroup.Group2 ? (uint)mode.ReviveCount : 0,
        };

        SendEvent(levelInfo);
    }

    #endregion LevelInfo

    #region ThirdPartyTracking

    private static void SendThirdPartyTracking(string gameEventType)
    {
        SDK<ITracking>.Instance.SendThirdPartyTracking(gameEventType);
    }

    public static void SendAdjustTracking(string gameEventType, Dictionary<string, object> parameters)
    {
        SDK<IAdjustAPI>.Instance.TrackEvent(gameEventType, parameters);
    }

    public static void SendTrackingEvent_PassLevel(int levelNum)
    {
        switch (levelNum)
        {
            case 5:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV5");
                break;

            case 10:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV10");
                break;

            case 15:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV15");
                break;

            case 20:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV20");
                break;

            case 30:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV30");
                break;

            case 40:
                SendThirdPartyTracking("GAME_EVENT_PASS_LV40");
                break;
        }
    }

    public static void SendTrackingEvent_CompleteDeco(int area)
    {
        switch (area)
        {
            case 1:
                SendThirdPartyTracking("GAME_EVENT_Complete_House1Area1");
                break;

            case 2:
                SendThirdPartyTracking("GAME_EVENT_Complete_House1Area2");
                break;
        }
    }

    #endregion ThirdPartyTracking

    #region Decoration

    /// <summary>
    /// Decoration的打点时机类型
    /// </summary>
    public enum EDecorationInfoType
    {
        [Description("enter_room")]
        Enter = 1,
        [Description("decoration_room")]
        BuyDecorationNode,
        [Description("change_style")]
        ChangeNode,
        [Description("complete_room")]
        CompleteRoom,
    }

    public static void SendDecorationInfo(EDecorationInfoType decorationInfoType, int roomId, int nodeId)
    {
        BiEventArrowPuzzle1.Types.Decoration decorationInfo = new BiEventArrowPuzzle1.Types.Decoration();
        decorationInfo.Feature = decorationInfoType.ToDes();
        decorationInfo.RoomId = (uint)roomId;
        decorationInfo.RoomItemId = nodeId.ToString();
        SendEvent(decorationInfo);
    }

    #endregion Decoration
}