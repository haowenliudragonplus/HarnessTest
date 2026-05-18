/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-2-27 16:9:4*****/
/*****************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWidget_EndlessGiftPackLTBase : UIWidgetBase
{
	protected RectTransform UINode_VFX;
	protected Slider UISlider_BG;
	protected TextMeshProUGUI UITxt_Text;
	protected TextMeshProUGUI UITxt_TimeText;
	protected RectTransform UINode_TipsBG;
	protected RectTransform UINode_free;
	protected Button UIBtn_ClaimButton;

    protected override void BindComponent()
    {
		UINode_VFX = GO.transform.Find("UISlider_BG/UINode_VFX").GetComponent<RectTransform>();
		UISlider_BG = GO.transform.Find("UISlider_BG").GetComponent<Slider>();
		UITxt_Text = GO.transform.Find("UINode_TipsBG/UITxt_Text").GetComponent<TextMeshProUGUI>();
		UITxt_TimeText = GO.transform.Find("UINode_TipsBG/UITxt_TimeText").GetComponent<TextMeshProUGUI>();
		UINode_TipsBG = GO.transform.Find("UINode_TipsBG").GetComponent<RectTransform>();
		UINode_free = GO.transform.Find("UIBtn_ClaimButton/UINode_free").GetComponent<RectTransform>();
		UIBtn_ClaimButton = GO.transform.Find("UIBtn_ClaimButton").GetComponent<Button>();

    }
}
