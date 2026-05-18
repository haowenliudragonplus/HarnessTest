/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-12-17 14:43:52*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIWidget_UIShopCoinItem2Base : UIWidgetBase
{
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_TipsText;
	protected RectTransform UINode_BuyButton;
	protected TextMeshProUGUI UITxt_FreeText;
	protected TextMeshProUGUI UITxt_RedTipText;
	protected RectTransform UINode_TipsGroup;
	protected RectTransform UINode_AdsButton;
	protected Button UIBtn_PurchaseButton;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("Root/UIBtn_PurchaseButton/UIImg_Icon").GetComponent<Image>();
		UITxt_TipsText = GO.transform.Find("Root/UIBtn_PurchaseButton/UITxt_TipsText").GetComponent<TextMeshProUGUI>();
		UINode_BuyButton = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_BuyButton").GetComponent<RectTransform>();
		UITxt_FreeText = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_AdsButton/UITxt_FreeText").GetComponent<TextMeshProUGUI>();
		UITxt_RedTipText = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_AdsButton/UINode_TipsGroup/UITxt_RedTipText").GetComponent<TextMeshProUGUI>();
		UINode_TipsGroup = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_AdsButton/UINode_TipsGroup").GetComponent<RectTransform>();
		UINode_AdsButton = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_AdsButton").GetComponent<RectTransform>();
		UIBtn_PurchaseButton = GO.transform.Find("Root/UIBtn_PurchaseButton").GetComponent<Button>();

    }
}
