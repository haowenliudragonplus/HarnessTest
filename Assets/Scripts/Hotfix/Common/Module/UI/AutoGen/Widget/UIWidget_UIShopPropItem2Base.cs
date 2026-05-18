/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-16 12:11:0*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIWidget_UIShopPropItem2Base : UIWidgetBase
{
	protected Image UIImg_Icon;
	protected RectTransform UINode_Tag;
	protected TextMeshProUGUI UITxt_RewardText;
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_ItemsGroup1;
	protected RectTransform UINode_ItemsGroup2;
	protected Button UIBtn_PurchaseButton;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("Root/UIBtn_PurchaseButton/UIImg_Icon").GetComponent<Image>();
		UINode_Tag = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_Tag").GetComponent<RectTransform>();
		UITxt_RewardText = GO.transform.Find("Root/UIBtn_PurchaseButton/UITxt_RewardText").GetComponent<TextMeshProUGUI>();
		UITxt_TitleText = GO.transform.Find("Root/UIBtn_PurchaseButton/Img_Title_bg/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_ItemsGroup1 = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_ItemsGroup1").GetComponent<RectTransform>();
		UINode_ItemsGroup2 = GO.transform.Find("Root/UIBtn_PurchaseButton/UINode_ItemsGroup2").GetComponent<RectTransform>();
		UIBtn_PurchaseButton = GO.transform.Find("Root/UIBtn_PurchaseButton").GetComponent<Button>();

    }
}
