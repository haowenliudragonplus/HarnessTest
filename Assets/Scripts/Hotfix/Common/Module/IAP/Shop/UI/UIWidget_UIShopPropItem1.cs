using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Config.Common;
using Framework;
using TMGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWidget_UIShopPropItem1 : UIWidget_UIShopPropItem1Base
{
    private LocalizeTextMeshProUGUI _tagText;
    private Text _prizeText;
    //[UIBinder("ItemsGroup")] private Transform _itemsGroup;

    //  [UIBinder("Root/Icon/NumberText")] private TextMeshProUGUI _item1NumberText;

    public ShopItemViewParam olddrivedParam;
    public ShopItemViewParam drivedParam;
    private readonly List<GameObject> _itemList = new List<GameObject>();

    protected override void OnCreate()
    {
        base.OnCreate();
        _tagText = UINode_Tag.transform.Find("TagText").GetComponent<LocalizeTextMeshProUGUI>();
        _prizeText = GO.transform.Find("Root/UIBtn_PurchaseButton/BuyButton/Text").GetComponent<Text>();
        drivedParam = ViewData as ShopItemViewParam;
        olddrivedParam = drivedParam;
        Refresh(drivedParam);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        UIBtn_PurchaseButton.onClick.AddListener(OnPurchaseButtonClick);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        UIBtn_PurchaseButton.onClick.RemoveListener(OnPurchaseButtonClick);
    }

    public void Refresh(ShopItemViewParam param)
    {
        _itemList.Clear();

        olddrivedParam = drivedParam;
        drivedParam = param;
        UITxt_TitleText.SetText(
            CoreUtils.GetLocalization(drivedParam.ItemData.Name));
        CoreUtils.SetImg(UIImg_Icon,drivedParam.ItemData.Atlas, drivedParam.ItemData.Icon);
        if (drivedParam.ItemData.Market == 0 &&
            drivedParam.ItemData.ShowDiscount == 0)
        {
             UINode_Tag.gameObject.SetActive(false);
        }
        else
        {
            UINode_Tag.gameObject.SetActive(true);
            if (drivedParam.ItemData.ShowDiscount > 0)
                _tagText.SetText($"-{drivedParam.ItemData.ShowDiscount}%");
            else if (drivedParam.ItemData.Market == 1)
                _tagText.SetText(CoreUtils.GetLocalization("UI_iap_market_1"));
            else if (drivedParam.ItemData.Market == 2)
                _tagText.SetText(CoreUtils.GetLocalization("UI_iap_market_2"));
            else if (drivedParam.ItemData.Market == 3)
                _tagText.SetText(CoreUtils.GetLocalization("UI_iap_market_3"));
        }

        UITxt_RewardText.SetText(drivedParam.ItemData.ItemCnt[0].ToString());


        int index = 1;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var node = i == 0 ? UINode_ItemsGroup1 : UINode_ItemsGroup2;
                var itemObj = node.Find("Item" + (j+1)).gameObject;
                if (index >= drivedParam.ItemData.ItemId.Count)
                {
                    itemObj.SetActive(false);
                    index += 1;
                    continue;
                }

                _itemList.Add(itemObj);
                var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(drivedParam.ItemData.ItemId[index]);
                
                var img = itemObj.transform.Find("Icon").GetComponent<Image>();
                if (cfg!=null)
                {
                    
                    CoreUtils.SetImg(img,cfg.Icon);
                    itemObj.transform.Find("NumberText").GetComponent<TMP_Text>().SetText(
                        cfg.IsTimeLimitItem
                            ? TimeUtils.FormatTime((long)(drivedParam.ItemData.ItemCnt[index] ))
                            : "x" + drivedParam.ItemData.ItemCnt[index].ToString());
                }
                index += 1;
            }
        }
        _prizeText.text = Game.GetMod<ModIAP>().GetDisplayPrice(drivedParam.ItemData.Id); 
    }

    private void OnPurchaseButtonClick()
    {
        drivedParam.buyOnClickItem.Invoke(drivedParam.ItemData, this);
    }
}