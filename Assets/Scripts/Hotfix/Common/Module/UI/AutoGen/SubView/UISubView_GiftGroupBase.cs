/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-16 18:49:51*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UISubView_GiftGroupBase : UISubViewBase
{
	protected TextMeshProUGUI UITxt_GiftText;
	protected Button UIBtn_TipsButton;
	protected TextMeshProUGUI UITxt_ProgressText;
	protected RectTransform UINode_Unlock;
	protected RectTransform UINode_Lock;

    protected override void BindComponent()
    {
		UITxt_GiftText = GO.transform.Find("UITxt_GiftText").GetComponent<TextMeshProUGUI>();
		UIBtn_TipsButton = GO.transform.Find("UIBtn_TipsButton").GetComponent<Button>();
		UITxt_ProgressText = GO.transform.Find("UINode_Unlock/Progress/UITxt_ProgressText").GetComponent<TextMeshProUGUI>();
		UINode_Unlock = GO.transform.Find("UINode_Unlock").GetComponent<RectTransform>();
		UINode_Lock = GO.transform.Find("UINode_Lock").GetComponent<RectTransform>();

    }
}
