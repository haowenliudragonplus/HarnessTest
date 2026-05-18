using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine.UI;

public class IapSuccessPopupParams
{
    public EvtIAPSuccess iapSuccess;
}

public class UIView_ShopPurchase : UIView_ShopPurchaseBase
{
    private IapSuccessPopupParams _popupParams;

    private bool isShopInGame = false;
    private bool isClicked = false;
    protected override void OnCreate()
    {
        base.OnCreate();
        isClicked = false;
        _popupParams = (IapSuccessPopupParams)ViewData;
        Game.GetMod<ModUI>().Close(UIViewName.UIView_UIShopMain);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        UIBtn_PlayButton.onClick.AddListener(OnContinueButtonClicked);
        UIBtn_CloseButton.onClick.AddListener(OnContinueButtonClicked);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        UIBtn_PlayButton.onClick.RemoveListener(OnContinueButtonClicked);
        UIBtn_CloseButton.onClick.RemoveListener(OnContinueButtonClicked);
    }

    private List<ItemData> GetItemsFromShopConfig()
    {
        var shopConfig = _popupParams.iapSuccess.shopCfg;
        var items = new List<ItemData>();

        if (shopConfig != null && shopConfig.ItemId != null && shopConfig.ItemId.Count > 0)
        {
            for (int i = 0; i < shopConfig.ItemId.Count; i++)
            {
                if (shopConfig.ItemCnt[i] > 0)
                {
                    ItemData itemData = new ItemData();
                    itemData.id = shopConfig.ItemId[i];
                    itemData.amount = shopConfig.ItemCnt[i];
                    if (shopConfig.ItemCnt[i] > 0)
                    {
                        items.Add(itemData);
                    }
                }
            }
        }

        return items;
    }

    private async void OnContinueButtonClicked()
    {
        if (isClicked) return;
        isClicked = true;
        var items = GetItemsFromShopConfig();

        Close();
        if (items.Count > 0)
        {
            // if (Game.GetMod<ModFsm>().CheckState<FsmState_FindTM>())
            // {
            //     foreach (var item in items)
            //     {
            //         Game.GetMod<ModEvent>().Dispatch(new EventCurrencyChange(item.GetItemType(), false, item.Amount));
            //     }
            // }
            // else
            {
                await Game.GetMod<FlySys>().FlyRewardItems(items, true);
            }
        }
    }
}