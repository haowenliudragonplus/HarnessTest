using System.Collections.Generic;
using DragonPlus;
using Framework;
using TMGame;
using UnityEngine.UI;

public class UIWidget_UIShopADItem : UIWidget_UIShopADItemBase
{
    private List<ShopItemViewParam> paramList;
    
    public void AddNoAdItem(ShopItemViewParam paramData)
    {
        if (paramList == null) paramList = new List<ShopItemViewParam>();
        paramList.Add(paramData);
        if (paramList.Count > 1)
        {
            UIBtn_PurchaseButton_left.onClick.AddListener(() =>
            {
                paramList[0].buyOnClickItem.Invoke(paramList[0].ItemData, this);
            });

            UIBtn_PurchaseButton_right.onClick.AddListener(() =>
            {
                paramList[1].buyOnClickItem.Invoke(paramList[1].ItemData, this);
            });
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        for (int i = 0; i < paramList.Count; i++)
        {
            if (i > 1) break;
            var paramData = paramList[i];
            string priceStr = Game.GetMod<ModIAP>().GetDisplayPrice(paramData.ItemData.Id);
            
            if (i == 1)
            {
                UIOldTxt_Text_Right_Right.text = priceStr;
                UITxt_TitleText_right.SetText(CoreUtils.GetLocalization(paramData.ItemData.Name));
                if (paramData.ItemData.ItemCnt != null &&
                    paramData.ItemData.ItemCnt.Count >= 0)
                {
                    UITxt_NumberText.SetText($"{paramData.ItemData.ItemCnt[0].ToString()}");
                }

                if (paramData.ItemData.Market != 0)
                {
                    if (paramData.ItemData.ShowDiscount > 0)
                        UITxt_TagText.SetText($"-{paramData.ItemData.ShowDiscount}%");
                    else if (paramData.ItemData.Market == 1)
                        UITxt_TagText.SetText(CoreUtils.GetLocalization("UI_iap_market_1"));
                    else if (paramData.ItemData.Market == 2)
                        UITxt_TagText.SetText(CoreUtils.GetLocalization("UI_iap_market_2"));
                    else if (paramData.ItemData.Market == 3)
                        UITxt_TagText.SetText(CoreUtils.GetLocalization("UI_iap_market_3"));
                }
            }
            else
            {
                UIOldTxt_Text_Price_Left.text = priceStr;
                UITxt_TitleText_left.SetText(CoreUtils.GetLocalization(paramData.ItemData.Name));
            }
        }
    }
    
    protected override void OnClose()
    {
        base.OnClose();
        UIBtn_PurchaseButton_left.onClick.RemoveAllListeners();
        UIBtn_PurchaseButton_right.onClick.RemoveAllListeners();
    }
}