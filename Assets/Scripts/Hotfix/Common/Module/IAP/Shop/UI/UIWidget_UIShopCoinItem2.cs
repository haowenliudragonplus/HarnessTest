using Framework;
using TMGame;
using TMPro;
using UnityEngine.UI;

public class UIWidget_UIShopCoinItem2 : UIWidget_UIShopCoinItem2Base
{
    public ShopItemViewParam drivedParam;
    public Text UITxt_Text;

    private bool getByAd;

    protected override void BindComponent()
    {
        base.BindComponent();
        UITxt_Text = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_BuyButton/Text").GetComponent<Text>();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        drivedParam = ViewData as ShopItemViewParam;
        getByAd = string.IsNullOrEmpty(drivedParam.ItemData.Product_id);
        Refresh(drivedParam);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        UIBtn_PurchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        UIBtn_PurchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
    }

    private void Refresh(ShopItemViewParam param)
    {
        drivedParam = param;
        UITxt_TipsText.SetText($"{drivedParam.ItemData.ItemCnt[0]}");
        CoreUtils.SetImg(UIImg_Icon, drivedParam.ItemData.Atlas, drivedParam.ItemData.Icon);
        if (getByAd)
        {
            UINode_AdsButton.gameObject.SetActive(true);
            UINode_BuyButton.gameObject.SetActive(false);
            // var modAd = Game.GetMod<AdSys>();
            //   UITxt_RedTipText.text = (modAd.GetAdRewardCfg(eAdReward.GetCoin).LimitPerDay - Game.GetMod<AdSys>().Model.GetRewardWatchCount((int)eAdReward.GetCoin)).ToString();
        }
        else
        {
            UITxt_Text.text = Game.GetMod<ModIAP>().GetDisplayPrice(drivedParam.ItemData.Id);
            UINode_AdsButton.gameObject.SetActive(false);
            UINode_BuyButton.gameObject.SetActive(true);
        }
    }

    private void OnPurchaseButtonClicked()
    {
        drivedParam.buyOnClickItem.Invoke(drivedParam.ItemData, this);
    }
}