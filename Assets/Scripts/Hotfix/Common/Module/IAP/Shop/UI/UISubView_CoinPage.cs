using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DragonPlus.Ad;
using DragonPlus.Config.Common;
using DragonPlus.Config.Shop;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShopItemViewParam
{
    public Table_Shop_Shop ItemData;
    public Action<Table_Shop_Shop, UIWidgetBase> buyOnClickItem;
}

public class UIShopMoreItemParam
{
    public Action OnClickItem;
}

public class UISubView_CoinPage : UISubView_CoinPageBase
{
    private ModIAP modIAP;

    private Transform _content;

    private UIWidget_UIShopADItem _noAdPackWidget;
    private UIWidget_UIShopMoreItem _buttonShowMoreObj;
    private List<UIWidget_UIShopPropItem1> uiWidget_PackItem1 = new();
    private List<UIWidget_UIShopPropItem2> uiWidget_PackItem2 = new();
    private List<UIWidget_UIShopCoinItem2> uiWidget_CoinItem = new();

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        modIAP = Game.GetMod<ModIAP>();
    }

    protected override void BindComponent()
    {
        base.BindComponent();
        _content = GO.transform.Find("Viewport/Content");
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        Game.GetMod<ModEvent>().Register<EvtIAPSuccess>(OnIAPSuccess);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        Game.GetMod<ModEvent>().UnRegister<EvtIAPSuccess>(OnIAPSuccess);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        InitAllShopItem();
    }

    private void OnIAPSuccess(EvtIAPSuccess evt)
    {

    }

    private void InitAllShopItem()
    {
        ClearShopItem();
        InitShopItemAll();
    }

    private void InitShopItemAll()
    {
        List<Table_Shop_Shop> tableShops = modIAP.GetDisplayShopCfgList();
        foreach (var shopConfig in tableShops)
        {
            LoadContent(shopConfig);
        }
    }

    private void LoadContent(Table_Shop_Shop shopItem)
    {
        var shopItemViewParam = new ShopItemViewParam()
        {
            ItemData = shopItem,
            buyOnClickItem = BuyOnClick
        };
        var type = shopItemViewParam.ItemData.ShopType;
        switch ((EShopType)type)
        {
            case EShopType.Coin:
                LoadCoinItem(shopItemViewParam);
                break;

            case EShopType.Pack:
                LoadPackItem(shopItemViewParam);
                break;

            case EShopType.RemoveAd:
                LoadNoAdItem(shopItemViewParam);
                break;
        }
    }

    private void ClearShopItem()
    {
        if (_noAdPackWidget != null)
        {
            CloseUIWidget(_noAdPackWidget, true);
            _noAdPackWidget = null;
        }
        if (_buttonShowMoreObj != null)
        {
            CloseUIWidget(_buttonShowMoreObj, true);
            _buttonShowMoreObj = null;
        }
        for (int i = 0; i < uiWidget_PackItem1.Count; i++)
        {
            CloseUIWidget(uiWidget_PackItem1[i], true);
        }
        uiWidget_PackItem1.Clear();
        for (int i = 0; i < uiWidget_PackItem2.Count; i++)
        {
            CloseUIWidget(uiWidget_PackItem2[i], true);
        }
        uiWidget_PackItem2.Clear();
        for (int i = 0; i < uiWidget_CoinItem.Count; i++)
        {
            CloseUIWidget(uiWidget_CoinItem[i], true);
        }
        uiWidget_CoinItem.Clear();
        for (int i = 0; i < coinItemParents.Count; i++)
        {
            Object.Destroy(coinItemParents[i].gameObject);
        }
        coinItemParents.Clear();
    }

    private List<Transform> coinItemParents = new List<Transform>();

    Transform GetCoinParent(int id)
    {
        if (id >= coinItemParents.Count)
        {
            var clone = GameObject.Instantiate(UINode_CoinGroup, UINode_CoinGroup.parent);
            coinItemParents.Add(clone);
            return clone;
        }
        return coinItemParents[id];
    }

    private void LoadCoinItem(ShopItemViewParam shopItemViewParam)
    {
        var pId = (uiWidget_CoinItem.Count / 3);
        var parent = GetCoinParent(pId);
        parent.gameObject.SetActive(true);
        var itemUI = OpenUIWidget<UIWidget_UIShopCoinItem2>(parent, false, shopItemViewParam);
        uiWidget_CoinItem.Add(itemUI);
    }

    private void LoadPackItem(ShopItemViewParam shopItemViewParam)
    {
        if (shopItemViewParam.ItemData.BgType == 1)
        {
            var itemUI = OpenUIWidget<UIWidget_UIShopPropItem1>(_content, false, shopItemViewParam);
            uiWidget_PackItem1.Add(itemUI);
        }
        else
        {
            var itemUI = OpenUIWidget<UIWidget_UIShopPropItem2>(_content, false, shopItemViewParam);
            uiWidget_PackItem2.Add(itemUI);
        }
    }

    private void LoadNoAdItem(ShopItemViewParam shopItemViewParam)
    {
        if (_noAdPackWidget == null)
        {
            _noAdPackWidget = OpenUIWidget<UIWidget_UIShopADItem>(_content, false, shopItemViewParam);
        }
        _noAdPackWidget.AddNoAdItem(shopItemViewParam);
    }
    private void BuyOnClick(Table_Shop_Shop itemData, UIWidgetBase view)
    {
        bool isByAd = string.IsNullOrEmpty(itemData.Product_id);
        if (isByAd)
        {
            // BIHelper.SendGameEvent_ClickRvBtn(eAdReward.GetCoin);
            // Game.GetMod<AdSys>().TryShowRewardedVideo(eAdReward.GetCoin, (result, str) =>
            // {
            //     if (result == AdPlayResult.Success)
            //     {
            //         if (_shopTypes.Contains((ShopType)itemData.ShopType))
            //         {
            //             List<Table_Common_Item> items = new List<Table_Common_Item>();
            //             for (int i = 0; i < itemData.ItemId.Count; i++)
            //             {
            //                 var item = TMItemUtility.GenerateDummyItem(itemData.ItemId[i], itemData.ItemCnt[i]);
            //                 items.Add(item);
            //             }
            //             Game.GetMod<UserProfileSys>().SettleRewards(items, new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.Ads)
            //             {
            //                 data1 = ((int)eAdReward.GetCoin).ToString(),
            //             });
            //
            //             IAPSuccess iapSuccess = new IAPSuccess();
            //             iapSuccess.TableGameShopConfig = itemData;
            //             var iapSuccessPopupParams = new IapSuccessPopupParams();
            //             iapSuccessPopupParams.iapSuccess = iapSuccess;
            //             Game.GetMod<UIMgr>().OpenSync(UIViewName.UIView_ShopPurchase, iapSuccessPopupParams);
            //         }
            //     }
            // });
        }
        else
        {
            Game.GetMod<ModIAP>().Purchase(itemData.Id, view);
        }
    }
}