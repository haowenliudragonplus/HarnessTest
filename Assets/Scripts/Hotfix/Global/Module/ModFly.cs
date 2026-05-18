using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using Framework;
using TMGame;
using UnityEngine;

public class FlyObjData
{
    public int itemId;
    public Vector3 fromUIPos;
    public Vector3 toUIPos;
    public int showCount;
    public int realCount;
    public EFlyObjTag flyObjTag;
    public int Order
    {
        get
        {
            var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);
            var order = cfg.FlyOrder;
            return order;
        }
    }

    public FlyObjData(int itemId, Vector3 fromUIPos, Vector3 toUIPos, int showCount, int realCount,
        EFlyObjTag flyObjTag = EFlyObjTag.Common)
    {
        this.itemId = itemId;
        this.fromUIPos = fromUIPos;
        this.toUIPos = toUIPos;
        this.showCount = showCount;
        this.realCount = realCount;
        this.flyObjTag = flyObjTag;
    }
}

/// <summary>
/// 飞物体标签
/// </summary>
public enum EFlyObjTag
{
    Common,
}

/// <summary>
/// 飞物体模块
/// </summary>
public class ModFly : ModuleBase
{
    private Dictionary<EItemType, int> toFlyItemDict = new Dictionary<EItemType, int>(); //将要飞的道具

    private Queue<List<FlyObjData>> flyObjDataSequence = new Queue<List<FlyObjData>>();
    private bool flyObjShowing;

    public override void OnInit()
    {
        base.OnInit();
        RegisterEvent<EvtAddFlyObjData>(OnAddFlyObjData);
    }

    public override void OnStart()
    {
        base.OnStart();
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Tip);
    }

    private void OnAddFlyObjData(EvtAddFlyObjData evt)
    {
        if (!GameUtils.IsSameCount(evt.itemIdList.Count, evt.fromScreenPosList.Count, evt.toScreenPosList.Count, evt.showCountList.Count, evt.realCountList.Count))
        {
            CLog.Error("飞物体数据不匹配！！！！！");
            for (int i = 0; i < evt.itemIdList.Count; i++)
            {
                EvtFlyObjGroupComplete evtFlyObjGroupComplete = new EvtFlyObjGroupComplete()
                {
                    itemId = evt.itemIdList[i],
                    flyObjTag = evt.tagList.Count <= i ? EFlyObjTag.Common : evt.tagList[i]
                };
                Game.GetMod<ModEvent>().Dispatch(evtFlyObjGroupComplete);
            }
            return;
        }

        // 将本次所有飞物体数据添加进队列
        List<FlyObjData> tempList = new List<FlyObjData>();
        for (int i = 0; i < evt.itemIdList.Count; i++)
        {
            var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(evt.itemIdList[i]);
            if (cfg == null)
            {
                CLog.Error($"飞物体数据中添加了不存在的物品id：{evt.itemIdList[i]}");
                continue;
            }

            Vector3 fromUIPos = CTUtils.Screen2UILocal(evt.fromScreenPosList[i], Game.GetMod<ModUI>().UIRect, Game.GetMod<ModUI>().UICamera);
            Vector3 toUIPos = CTUtils.Screen2UILocal(evt.toScreenPosList[i], Game.GetMod<ModUI>().UIRect, Game.GetMod<ModUI>().UICamera);
            FlyObjData flyItemData = new FlyObjData(evt.itemIdList[i], fromUIPos, toUIPos, evt.showCountList[i], evt.realCountList[i],
                evt.tagList.Count <= i ? EFlyObjTag.Common : evt.tagList[i]);
            tempList.Add(flyItemData);
        }
        // 排序
        tempList.Sort((x, y) => x.Order.CompareTo(y.Order));
        Dictionary<int, List<FlyObjData>> tempDict = new();
        foreach (var flyObjData in tempList)
        {
            if (!tempDict.TryGetValue(flyObjData.Order, out var _dataList))
            {
                _dataList = new List<FlyObjData>();
                tempDict.Add(flyObjData.Order, _dataList);
            }
            _dataList.Add(flyObjData);
        }
        // 最终添加
        foreach (var v in tempDict.Values)
        {
            flyObjDataSequence.Enqueue(v);
        }

        CheckFlyObjSequence();
    }

    private void CheckFlyObjSequence()
    {
        if (flyObjDataSequence.Count == 0)
            return;
        if (flyObjShowing)
            return;
        var flyObjDataList = flyObjDataSequence.Dequeue();
        flyObjShowing = true;
        for (int i = 0; i < flyObjDataList.Count; i++)
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtPlayFlyObj(flyObjDataList[i], i == flyObjDataList.Count - 1));
        }
    }

    public void AddItem(EItemType itemType, int itemCount)
    {
        if (toFlyItemDict.ContainsKey(itemType))
        {
            toFlyItemDict[itemType] += itemCount;
        }
        else
        {
            toFlyItemDict[itemType] = itemCount;
        }
    }

    public int GetItemCount(EItemType itemTpe)
    {
        if (toFlyItemDict.TryGetValue(itemTpe, out var _count))
            return _count;
        return 0;
    }

    public void ClearToFlyItemDict()
    {
        toFlyItemDict.Clear();
    }

    public async UniTask CheckToFlyItemDict()
    {
        var coinAmount = 0;
        var boosterItem = new List<ItemData>();
        var keyAmount = 0;
        foreach (var item in toFlyItemDict)
        {
            var itemCfg = new ItemData();
            itemCfg.id = (int)item.Key;
            itemCfg.amount = item.Value;
            if ((EItemType)itemCfg.id == EItemType.Key)
            {
                keyAmount += itemCfg.amount;
            }
            else if ((EItemType)itemCfg.id == EItemType.Coin)
            {
                coinAmount += itemCfg.amount;
            }
            else
            {
                boosterItem.Add(itemCfg);
            }
        }
        if (coinAmount > 0)
        {
            var flyCoinAmount = Mathf.Min(coinAmount, 10);
            Game.GetMod<FlySys>().FlyItem((int)EItemType.Coin, coinAmount, Vector2.zero,
                FlyTarget.GetTarget((int)EItemType.Coin).position, null);
            await CoreUtils.WaitSeconds(flyCoinAmount * 0.15f, false);
        }
        if (keyAmount > 0)
        {
            var flyCoinAmount = Mathf.Min(keyAmount, 10);
            Game.GetMod<FlySys>().FlyItem((int)EItemType.Key, keyAmount, Vector2.zero,
                FlyTarget.GetTarget((int)EItemType.Key).position, null);
            await CoreUtils.WaitSeconds(flyCoinAmount * 0.15f, false);
        }
        var listPos = Game.GetMod<FlySys>().GetRewardPos(boosterItem);
        for (int i = 0; i < boosterItem.Count; i++)
        {
            var startPos = listPos[i];
            Game.GetMod<FlySys>().PlaySettleRewardAnimationForBooster(boosterItem[i], startPos, i + 1, false);

            await CoreUtils.WaitSeconds(0.15f, true);
        }
        //await CoreUtils.WaitSeconds((float)37 / 30, true);
    }
}