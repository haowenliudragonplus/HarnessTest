/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-3-11 10:25:59*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;

public class UIWidget_NoADS1Base : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_Text;
	protected TextMeshProUGUI UITxt_TimeText;
	protected RectTransform UINode_TipsBG;

    protected override void BindComponent()
    {
		UITxt_Text = GO.transform.Find("UINode_TipsBG/UITxt_Text").GetComponent<TextMeshProUGUI>();
		UITxt_TimeText = GO.transform.Find("UINode_TipsBG/UITxt_TimeText").GetComponent<TextMeshProUGUI>();
		UINode_TipsBG = GO.transform.Find("UINode_TipsBG").GetComponent<RectTransform>();

    }
}
