/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-16 12:9:9*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIWidget_UIShopADItemBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_TitleText_left;
	protected Text UIOldTxt_Text_Price_Left;
	protected Button UIBtn_PurchaseButton_left;
	protected TextMeshProUGUI UITxt_TitleText_right;
	protected TextMeshProUGUI UITxt_NumberText;
	protected TextMeshProUGUI UITxt_TagText;
	protected RectTransform UINode_Tag;
	protected Text UIOldTxt_Text_Right_Right;
	protected Button UIBtn_PurchaseButton_right;

    protected override void BindComponent()
    {
		UITxt_TitleText_left = GO.transform.Find("Root1/UIBtn_PurchaseButton_left/UITxt_TitleText_left").GetComponent<TextMeshProUGUI>();
		UIOldTxt_Text_Price_Left = GO.transform.Find("Root1/UIBtn_PurchaseButton_left/BuyButton/UIOldTxt_Text_Price_Left").GetComponent<Text>();
		UIBtn_PurchaseButton_left = GO.transform.Find("Root1/UIBtn_PurchaseButton_left").GetComponent<Button>();
		UITxt_TitleText_right = GO.transform.Find("Root2/UIBtn_PurchaseButton_right/UITxt_TitleText_right").GetComponent<TextMeshProUGUI>();
		UITxt_NumberText = GO.transform.Find("Root2/UIBtn_PurchaseButton_right/ADSGroup/Item2/UITxt_NumberText").GetComponent<TextMeshProUGUI>();
		UITxt_TagText = GO.transform.Find("Root2/UIBtn_PurchaseButton_right/UINode_Tag/UITxt_TagText").GetComponent<TextMeshProUGUI>();
		UINode_Tag = GO.transform.Find("Root2/UIBtn_PurchaseButton_right/UINode_Tag").GetComponent<RectTransform>();
		UIOldTxt_Text_Right_Right = GO.transform.Find("Root2/UIBtn_PurchaseButton_right/BuyButton/UIOldTxt_Text_Right_Right").GetComponent<Text>();
		UIBtn_PurchaseButton_right = GO.transform.Find("Root2/UIBtn_PurchaseButton_right").GetComponent<Button>();

    }
}
