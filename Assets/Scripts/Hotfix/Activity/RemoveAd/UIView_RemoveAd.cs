using DragonPlus.Config.RemoveAd;
using DragonPlus.Config.Shop;
using Framework;
using UnityEngine.UI;

public class UIView_RemoveAd : UIView_RemoveAdBase
{
    public class OpenData
    {
        public Table_RemoveAd_RemoveAd removeAdCfg;
    }

    private OpenData openData;

    private ModActivity_RemoveAd modRemoveAd;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenData;
        modRemoveAd = Game.GetMod<ModActivity_RemoveAd>();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.onClick.AddListener(OnCloseBtn);
        UIBtn_BuyButton.onClick.AddListener(OnBuyBtn);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        RegisterEvent<EvtIAPSuccess>(OnIAPSuccess);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        UIOldTxt_Price.text = Game.GetMod<ModIAP>().GetDisplayPrice(openData.removeAdCfg.RemoveAdsGiftId[0]);
    }

    private void OnBuyBtn()
    {
        Game.GetMod<ModIAP>().Purchase(openData.removeAdCfg.RemoveAdsGiftId[0]);
    }

    private void OnIAPSuccess(EvtIAPSuccess evt)
    {
        if (evt.shopCfg.ShopType != (int)EShopType.RemoveAd)
            return;

        Close();
    }

    private void OnCloseBtn()
    {
        Close();
    }
}
