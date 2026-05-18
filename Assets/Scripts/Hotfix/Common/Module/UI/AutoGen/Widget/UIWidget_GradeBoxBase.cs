/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-3-11 13:59:0*****/
/*****************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWidget_GradeBoxBase : UIWidgetBase
{
	protected RectTransform UINode_VFX;
	protected Slider UISlider_BG;
	protected TextMeshProUGUI UITxt_Text;
	protected TextMeshProUGUI UITxt_TimeText;
	protected Button UIBtn_ClaimButton;

    protected override void BindComponent()
    {
		UINode_VFX = GO.transform.Find("UISlider_BG/UINode_VFX").GetComponent<RectTransform>();
		UISlider_BG = GO.transform.Find("UISlider_BG").GetComponent<Slider>();
		UITxt_Text = GO.transform.Find("TipsBG/UITxt_Text").GetComponent<TextMeshProUGUI>();
		UITxt_TimeText = GO.transform.Find("TipsBG/UITxt_TimeText").GetComponent<TextMeshProUGUI>();
		UIBtn_ClaimButton = GO.transform.Find("UIBtn_ClaimButton").GetComponent<Button>();

    }
}
