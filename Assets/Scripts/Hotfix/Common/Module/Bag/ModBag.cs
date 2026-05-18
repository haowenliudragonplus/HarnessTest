using System;
using System.Collections.Generic;
using System.Linq;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

public class ItemData
{
    public int id;
    public EItemType itemType => (EItemType)id;
    public Table_Common_Item itemCfg => Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(id);
    public int amount;
}

public class ModBag : ModuleBase
{
    private const string ITEM_KEY = "currency_";

    private StorageClientCommon StorageClientCommon;

    private Dictionary<int, long> itemId2LastAutoAddTimeDict = new Dictionary<int, long>(); //道具id — 上一次自动获得的时间
    private Dictionary<int, long> itemId2TimeLimitEndTimeDict = new Dictionary<int, long>(); //道具id — 限时道具结束时间

    public override void OnInit()
    {
        base.OnInit();
        StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        InitData();
    }

    private void InitData()
    {
        // 缓存起来是为了存档中带了key存储，避免update中每次都操作字符串
        foreach (var kvp in StorageClientCommon.Currency)
        {
            int itemId = int.Parse(kvp.Key.Replace(ITEM_KEY, ""));
            // 初始化存档中可以自动获得的道具上一次自动获得的时间
            if (kvp.Value.LastAutoAddTime > 0
                && !IsNumLimit((EItemType)itemId))
            {
                itemId2LastAutoAddTimeDict[itemId] = kvp.Value.LastAutoAddTime;
            }
            // 初始化存档中限时道具的结束时间
            if (kvp.Value.TimeLimitEndTime > 0
                && kvp.Value.TimeLimitEndTime > TimeUtils.GetServerTimeStamp())
            {
                itemId2TimeLimitEndTimeDict[itemId] = kvp.Value.TimeLimitEndTime;
            }
        }
    }

    /// <summary>
    /// 获取物品数量
    /// </summary>
    public int GetItemCount(EItemType itemType)
    {
        var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        if (cfg.IsTimeLimitItem)
        {
            CLog.Error($"道具是限时道具，无法获取数量，itemType：{itemType}");
            return 0;
        }

        var storageItem = GetStorageItem(itemType);
        if (storageItem == null)
            return 0;
        var count = storageItem.DecryptItemCount();
        return count;
    }

    /// <summary>
    /// 获取限时道具的结束剩余时间（秒）
    /// </summary>
    /// 如果是普通道具则获取对应的限时道具类型
    /// 如果是限时道具则使用自身道具类型
    public long GetTimeLimitLeftTime(EItemType itemType)
    {
        EItemType infinityItemType = itemType;
        var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        if (cfg.IsTimeLimitItem)
        {
            infinityItemType = itemType;
        }
        else
        {
            var infinityItemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(cfg.TimeLimitItemId);
            if (infinityItemCfg == null)
            {
                CLog.Error($"获取限时道具的结束剩余时间失败，道具对应的限时道具配置为null，itemType：{itemType}");
                return 0;
            }
            infinityItemType = (EItemType)infinityItemCfg.Id;
        }

        var storageItem = GetStorageItem(infinityItemType);
        if (storageItem == null)
            return 0;

        long leftTime = storageItem.TimeLimitEndTime - TimeUtils.GetServerTimeStamp();
        leftTime = (long)Mathf.Max(0, leftTime);
        return leftTime;
    }

    #region 添加道具

    /// <summary>
    /// 添加道具
    /// </summary>
    /// 如果是普通道具则加数量，如果是限时道具则加时间（秒）
    /// sendEvtItemChange：是否刷新道具显示
    public void AddItem(EItemType itemType, long addNum,
        BIHelper.ItemChangeReasonArgs changeReason, bool isAutoAdd = false, bool sendEvtItemChange = true)
    {
        if (addNum == 0)
            return;

        var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        var storageItem = GetStorageItem(itemType);
        if (storageItem == null)
        {
            storageItem = new StorageCurrency();
            string key = GetItemKey(itemType);
            StorageClientCommon.Currency.Add(key, storageItem);
        }

        // 限时道具
        if (itemCfg.IsTimeLimitItem)
        {
            long curTimeStamp = TimeUtils.GetServerTimeStamp();
            if (storageItem.TimeLimitEndTime < curTimeStamp)
            {
                if (itemCfg.NumLimit > 0)
                {
                    addNum = (long)Mathf.Clamp(addNum, 0, itemCfg.NumLimit);
                }
                storageItem.TimeLimitEndTime = curTimeStamp + addNum;
            }
            else
            {
                if (itemCfg.NumLimit > 0)
                {
                    addNum = storageItem.TimeLimitEndTime - curTimeStamp + addNum > itemCfg.NumLimit
                        ? itemCfg.NumLimit - (storageItem.TimeLimitEndTime - curTimeStamp)
                        : addNum;
                }
                storageItem.TimeLimitEndTime += addNum;
            }

            // 记录限时道具的结束时间
            itemId2TimeLimitEndTimeDict[(int)itemType] = storageItem.TimeLimitEndTime;
        }
        // 普通道具
        else
        {
            if (itemCfg.NumLimit > 0)
            {
                if (storageItem.DecryptItemCount() + addNum > itemCfg.NumLimit)
                {
                    addNum = itemCfg.NumLimit - storageItem.DecryptItemCount();
                }
            }
            storageItem.EncryptItemCount(storageItem.DecryptItemCount() + (int)addNum);

            // 记录上一次自动添加的时间
            if ((isAutoAdd || (itemCfg.AutoAddInterval > 0 && storageItem.LastAutoAddTime <= 0))
                && !IsNumLimit(itemType))
            {
                storageItem.LastAutoAddTime = TimeUtils.GetServerTimeStamp();
                itemId2LastAutoAddTimeDict[itemCfg.Id] = storageItem.LastAutoAddTime;
            }
        }
        if (addNum != 0 && sendEvtItemChange)
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtItemChange(itemType, (int)addNum, true, isAutoAdd));
        }

        // bi 
        if (addNum != 0)
        {
            long totalNum = itemCfg.IsTimeLimitItem
                ? GetTimeLimitLeftTime(itemType)
                : GetItemCount(itemType);
            BIHelper.SendItemChangeEvent(itemType, addNum, (ulong)totalNum, changeReason);
        }
    }

    /// <summary>
    /// 添加道具
    /// </summary>
    /// 如果是普通道具则加数量，如果是限时道具则加时间（秒）
    public void AddItem(List<int> itemTypeList, List<int> addNumList,
        BIHelper.ItemChangeReasonArgs changeReason, bool isAutoAdd = false, bool sendEvtItemChange = true)
    {
        if (itemTypeList.Count != addNumList.Count)
        {
            CLog.Error("添加道具失败，道具类型列表和数量列表长度不一致");
            return;
        }
        for (int i = 0; i < itemTypeList.Count; i++)
        {
            AddItem((EItemType)itemTypeList[i], addNumList[i], changeReason, isAutoAdd, sendEvtItemChange);
        }
    }

    /// <summary>
    /// 添加道具
    /// </summary>
    /// 如果是普通道具则加数量，如果是限时道具则加时间（秒）
    public void AddItem(ItemData itemData,
        BIHelper.ItemChangeReasonArgs changeReason, bool isAutoAdd = false, bool sendEvtItemChange = true)
    {
        AddItem(itemData.itemType, itemData.amount, changeReason, isAutoAdd, sendEvtItemChange);
    }

    /// <summary>
    /// 添加道具
    /// </summary>
    /// 如果是普通道具则加数量，如果是限时道具则加时间（秒）
    public void AddItem(List<ItemData> itemDataList,
        BIHelper.ItemChangeReasonArgs changeReason, bool isAutoAdd = false, bool sendEvtItemChange = true)
    {
        if (itemDataList == null)
            return;
        foreach (var itemData in itemDataList)
        {
            if (itemData == null)
                continue;
            AddItem(itemData, changeReason, isAutoAdd, sendEvtItemChange);
        }
    }

    #endregion 添加道具

    #region 消耗道具

    /// <summary>
    /// 消耗道具
    /// </summary>
    /// 如果是普通道具则消耗数量，如果是限时道具则消耗时间
    public bool ConsumeItem(EItemType itemType, long consumeNum,
        BIHelper.ItemChangeReasonArgs changeReason,
        Action onSuccess = null, Action onFailed = null)
    {
        if (consumeNum <= 0)
        {
            onSuccess?.Invoke();
            return true;
        }

        bool canAfford = CanAfford(itemType, consumeNum);
        if (canAfford)
        {
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
            var storageItem = GetStorageItem(itemType);
            // 限时道具
            if (itemCfg.IsTimeLimitItem)
            {
                storageItem.TimeLimitEndTime -= consumeNum;

                // 记录限时道具的结束时间
                itemId2TimeLimitEndTimeDict[(int)itemType] = storageItem.TimeLimitEndTime;
            }
            // 普通道具
            else
            {
                long count = storageItem.DecryptItemCount() - consumeNum;
                storageItem.EncryptItemCount(count);

                // 记录上一次自动添加的时间
                if (itemCfg.AutoAddInterval > 0
                    && !IsNumLimit(itemType)
                    && storageItem.LastAutoAddTime <= 0)
                {
                    storageItem.LastAutoAddTime = TimeUtils.GetServerTimeStamp();
                    itemId2LastAutoAddTimeDict[itemCfg.Id] = storageItem.LastAutoAddTime;
                }
            }
            onSuccess?.Invoke();
            Game.GetMod<ModEvent>().Dispatch(new EvtItemChange(itemType, -consumeNum, true));

            // bi
            long totalNum = itemCfg.IsTimeLimitItem
                ? GetTimeLimitLeftTime(itemType)
                : GetItemCount(itemType);
            BIHelper.SendItemChangeEvent(itemType, -consumeNum, (ulong)totalNum, changeReason);

            return true;
        }
        else
        {
            onFailed?.Invoke();
            return false;
        }
    }

    #endregion 消耗道具

    #region 自动获得道具相关

    /// <summary>
    /// 获取自动获得的剩余时间
    /// </summary>
    public long GetAutoAddLeftTime(EItemType itemType)
    {
        EItemType infinityItemType = itemType;
        var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        if (itemCfg.IsTimeLimitItem)
        {
            CLog.Error($"道具是限时道具，无法获取自动加数量的剩余时间，itemType：{itemType}");
            return 0;
        }
        var storageItem = GetStorageItem(infinityItemType);
        if (storageItem == null)
            return 0;
        if (storageItem.LastAutoAddTime <= 0)
            return 0;
        var leftTime = storageItem.LastAutoAddTime + itemCfg.AutoAddInterval - TimeUtils.GetServerTimeStamp();
        leftTime = (long)Mathf.Max(0, leftTime);
        return leftTime;
    }

    #endregion 自动获得道具相关

    /// <summary>
    /// 当前数量是否足够
    /// </summary>
    public bool CanAfford(EItemType itemType, long consumeNum)
    {
        if (consumeNum <= 0)
            return true;
        var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        long curNum = cfg.IsTimeLimitItem
            ? GetTimeLimitLeftTime(itemType)
            : GetItemCount(itemType);
        return GetItemCount(itemType) >= consumeNum;
    }

    /// <summary>
    /// 数量是否上限
    /// </summary>
    /// 如果是普通道具判断数量，如果是限时道具则判断剩余时间（秒）
    public bool IsNumLimit(EItemType itemType)
    {
        var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)itemType);
        var storageItem = GetStorageItem(itemType);
        if (itemCfg.NumLimit <= 0)
            return false;
        // 限时道具
        bool isNumLimit = false;
        if (itemCfg.IsTimeLimitItem)
        {
            isNumLimit = storageItem.TimeLimitEndTime - TimeUtils.GetServerTimeStamp() >= itemCfg.NumLimit;
        }
        // 普通道具
        else
        {
            isNumLimit = GetItemCount(itemType) >= itemCfg.NumLimit;
        }
        return isNumLimit;
    }

    private string GetItemKey(EItemType itemType)
    {
        return ITEM_KEY + (int)itemType;
    }

    /// <summary>
    /// 获取StorageItem存档类
    /// </summary>
    private StorageCurrency GetStorageItem(EItemType itemType)
    {
        string key = GetItemKey(itemType);
        if (!StorageClientCommon.Currency.TryGetValue(key, out var _storageItem))
            return null;
        return _storageItem;
    }

    #region ItemPackage

    /// <summary>
    /// 获取道具组配置
    /// </summary>
    public Table_Common_ItemPackage GetItemPackageCfg(int itemPackageType)
    {
        var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_ItemPackage>(itemPackageType);
        if (cfg == null)
        {
            CLog.Error($"获取物品组配置失败，itemPackageType：{itemPackageType}");
            return null;
        }
        return cfg;
    }

    /// <summary>
    /// 获取道具组配置
    /// </summary>
    public Table_Common_ItemPackage GetItemPackageCfg(EItemPackageType itemPackageType)
    {
        var cfg = GetItemPackageCfg((int)itemPackageType);
        return cfg;
    }

    #endregion ItemPackage

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        // 处理自动获得道具
        for (int i = 0; i < itemId2LastAutoAddTimeDict.Count; i++)
        {
            var itemId = itemId2LastAutoAddTimeDict.Keys.ElementAt(i);
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);
            var lastAutoAddTime = itemId2LastAutoAddTimeDict.Values.ElementAt(i);
            if (lastAutoAddTime > 0
                && TimeUtils.GetServerTimeStamp() >= lastAutoAddTime + itemCfg.AutoAddInterval)
            {
                // 自动加道具
                int addCount = (int)((TimeUtils.GetServerTimeStamp() - lastAutoAddTime) / itemCfg.AutoAddInterval)
                               * itemCfg.AutoAddCount;
                AddItem((EItemType)itemId, addCount,
                    new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.None), true);
                var storageItem = GetStorageItem((EItemType)itemId);
                if (IsNumLimit((EItemType)itemId))
                {
                    storageItem.LastAutoAddTime = 0;
                    itemId2LastAutoAddTimeDict[itemId] = 0;
                }
                else
                {
                    storageItem.LastAutoAddTime = TimeUtils.GetServerTimeStamp();
                    itemId2LastAutoAddTimeDict[itemCfg.Id] = storageItem.LastAutoAddTime;
                }
            }
        }
        // 处理限时道具时间结束
        for (int i = 0; i < itemId2TimeLimitEndTimeDict.Count; i++)
        {
            var itemId = itemId2TimeLimitEndTimeDict.Keys.ElementAt(i);
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);
            var timeLimitEndTime = itemId2TimeLimitEndTimeDict.Values.ElementAt(i);
            if (timeLimitEndTime > 0
                && TimeUtils.GetServerTimeStamp() >= timeLimitEndTime)
            {
                Game.GetMod<ModEvent>().Dispatch(new EvtTimeLimitItemTimeEnd((EItemType)itemId));
                itemId2TimeLimitEndTimeDict[itemId] = 0;
            }
        }
    }
}

/// <summary>
/// 物品类型（对应表里的id）
/// </summary>
public enum EItemType
{
    None,
    //通用
    Coin = 1, //金币
    Energy = 2, //体力
    EnergyInfinity = 3, //无限体力

    //
    Shuffle = 101, //洗牌
    Hint = 102, //提示
    AddSlot = 103, //加槽
    Clear = 104,//清扫

    Key = 8, //装修币
    WeeklyChallengeCollect = 201, //周挑战收集
    WeeklyChallengeBuff = 202, //周挑战buff
    Avatar = 17, //头像
    AvatarFrame = 18, //头像框
}

/// <summary>
/// 道具组类型
/// </summary>
public enum EItemPackageType
{
    Init = 1,
}

public static class StorageItemExtension
{
    /// <summary>
    /// 加密物品数量
    /// </summary>
    public static void EncryptItemCount(this StorageCurrency storageItem, long value)
    {
        if (DecryptItemCount(storageItem) == value)
            return;

        if (value <= 0)
        {
            storageItem.Vc0 = 0.0f;
            storageItem.Vc1 = 0;
        }
        else
        {
            storageItem.Vc1 = (int)Math.Floor(UnityEngine.Random.Range(0.0f, 1.0f) * value);
            storageItem.Vc0 = (value - storageItem.Vc1) / 8.0f;
        }
        storageItem.Profile.Instance.UpdateLocalVersion();
        storageItem.Profile.Instance.ForceSaveToDisk();
    }

    /// <summary>
    /// 解密物品数量
    /// </summary>
    public static int DecryptItemCount(this StorageCurrency storageItem)
    {
        return (int)Math.Round(8.0f * storageItem.Vc0 + storageItem.Vc1);
    }
}