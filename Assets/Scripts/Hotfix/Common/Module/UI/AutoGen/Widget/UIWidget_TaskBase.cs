/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-10-15 14:4:19*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;

public class UIWidget_TaskBase : UIWidgetBase
{
	protected RectTransform UINode_bg_default;
	protected RectTransform UINode_bg_get;
	protected TextMeshProUGUI UITxt_Des;
	protected RectTransform UINode_tip;

    protected override void BindComponent()
    {
		UINode_bg_default = GO.transform.Find("Root/UINode_bg_default").GetComponent<RectTransform>();
		UINode_bg_get = GO.transform.Find("Root/UINode_bg_get").GetComponent<RectTransform>();
		UITxt_Des = GO.transform.Find("Root/UITxt_Des").GetComponent<TextMeshProUGUI>();
		UINode_tip = GO.transform.Find("Root/UINode_tip").GetComponent<RectTransform>();

    }
}
