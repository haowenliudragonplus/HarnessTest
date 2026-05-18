/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-16 13:45:6*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;

public class UIWidget_UIShopMoreItemBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_TipsText;
	protected Button UIBtn_PurchaseButton;

    protected override void BindComponent()
    {
		UITxt_TipsText = GO.transform.Find("Root/UIBtn_PurchaseButton/UITxt_TipsText").GetComponent<TextMeshProUGUI>();
		UIBtn_PurchaseButton = GO.transform.Find("Root/UIBtn_PurchaseButton").GetComponent<Button>();

    }
}
