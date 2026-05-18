/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:49:41*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIView_SaveYourProgressBase : UIViewBase
{
	protected RectTransform UINode_Empty;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;
	protected TextMeshProUGUI UITxt_TooltipText;
	protected TextMeshProUGUI UITxt_BindFb;
	protected Button UIBtn_BindFb;
	protected TextMeshProUGUI UITxt_BindApple;
	protected Button UIBtn_BindApple;

    protected override void BindComponent()
    {
		UINode_Empty = GO.transform.Find("Root/BG_ReplaceDiff/UINode_Empty").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UITxt_TooltipText = GO.transform.Find("Root/UITxt_TooltipText").GetComponent<TextMeshProUGUI>();
		UITxt_BindFb = GO.transform.Find("Root/ButtonGroup/UIBtn_BindFb/UITxt_BindFb").GetComponent<TextMeshProUGUI>();
		UIBtn_BindFb = GO.transform.Find("Root/ButtonGroup/UIBtn_BindFb").GetComponent<Button>();
		UITxt_BindApple = GO.transform.Find("Root/ButtonGroup/UIBtn_BindApple/UITxt_BindApple").GetComponent<TextMeshProUGUI>();
		UIBtn_BindApple = GO.transform.Find("Root/ButtonGroup/UIBtn_BindApple").GetComponent<Button>();

    }
}
