using DragonPlus.Config.RemoveAd;
using DragonPlus.Core;
using DragonPlus.InAppPurchasing;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

public class ModActivity_RemoveAd : ModuleBase
{
    private StorageRemoveAd storageRemoveAd;

    private UIWidget_Entrance_RemoveAd removeAdEntrance;

    public override void OnStart()
    {
        base.OnStart();

        var openData = new UIView_RemoveAd.OpenData
        {
            removeAdCfg = Game.GetMod<ModActivity_RemoveAd>().GetRemoveAdCfg()
        };
        Game.GetMod<ModPopup>().AddPopup(UIViewName.UIView_RemoveAd, CanShow, openData);
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        storageRemoveAd = Game.GetMod<ModStorage>().GetStorage<StorageActivity>().RemoveAd;
    }

    public void RefreshEntrance()
    {
        var mainView = Game.GetMod<ModUI>().FindView(UIViewName.UIView_HomeMain) as UIView_HomeMain;
        bool show = Game.GetMod<ModActivity_RemoveAd>().CanShow();
        if (show)
        {
            if (removeAdEntrance == null)
            {
                removeAdEntrance = mainView.OpenUIWidget<UIWidget_Entrance_RemoveAd>(mainView.GetActivityEntrancePos(true), false);
            }
        }
        else
        {
            if (removeAdEntrance != null)
            {
                mainView.CloseUIWidget(removeAdEntrance, true);
                removeAdEntrance = null;
            }
        }
    }

    public bool CanShow()
    {
        var removeAdCfg = GetRemoveAdCfg();
        if (removeAdCfg == null)
            return false;
        int curLevel = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1;
        bool canShow = curLevel >= removeAdCfg.UnlockLevel
            && !IsBuy();
        return canShow;
    }

    public bool IsBuy()
    {
        if (SDK<IAP>.Instance.IsInitialized())
        {
            if (!storageRemoveAd.IsBuy)
            {
                var removeAdCfg = GetRemoveAdCfg();
                foreach (var shopId in removeAdCfg.RemoveAdsGiftId)
                {
                    var shopCfg = Game.GetMod<ModIAP>().GetShopCfgById(shopId);
                    if (shopCfg == null)
                        continue;
                    var productId = Game.GetMod<ModIAP>().GetProductId(shopCfg);
                    if (SDK<IAP>.Instance.HasOwnedProduct(productId))
                    {
                        storageRemoveAd.IsBuy = true;
                        break;
                    }
                }
            }
        }

        return storageRemoveAd.IsBuy;
    }

    public Table_RemoveAd_RemoveAd GetRemoveAdCfg()
    {
        var modAd = Game.GetMod<AdSys>();
        var group = modAd.GetCurGroup();
        var mappingCfg = modAd.GetCurMapping();
        if (mappingCfg == null)
            return null;
        int removeAdGroup = mappingCfg.RemoveAdGroup;
        var removeAdCfg = Game.GetMod<ModConfig>().GetConfigs<Table_RemoveAd_RemoveAd>().Find(x => x.GroupId == removeAdGroup);
        return removeAdCfg;
    }

    public override void OnDispose()
    {
        base.OnDispose();
        removeAdEntrance = null;
    }
}
