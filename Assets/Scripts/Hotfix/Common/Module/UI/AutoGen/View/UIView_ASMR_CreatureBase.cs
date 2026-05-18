/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-23 16:5:33*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_ASMR_CreatureBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_TitleGroup;
	protected RectTransform UINode_SkeletonGraphic;
	protected RectTransform UINode_Help;
	protected Button UIBtn_Button;
	protected TextMeshProUGUI UITxt_DescUnlock;
	protected Button UIBtn_ButtonGray;
	protected RectTransform UINode_LevelGroup;
	protected RectTransform UINode_Content;
	protected Button UIBtn_LeftButton;
	protected Button UIBtn_RightButton;
	protected Button UIBtn_CloseButton;
	protected Button UIBtn_SkipButton;
	protected RectTransform UINode_SpineGroup;
	protected TextMeshProUGUI UITxt_DecText;
	protected TextMeshProUGUI UITxt_ProgressText;
	protected Slider UISlider_Progress;

    protected override void BindComponent()
    {
		UITxt_TitleText = GO.transform.Find("Root/UINode_TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_TitleGroup = GO.transform.Find("Root/UINode_TitleGroup").GetComponent<RectTransform>();
		UINode_SkeletonGraphic = GO.transform.Find("Root/UINode_Content/Creature/UINode_SkeletonGraphic").GetComponent<RectTransform>();
		UINode_Help = GO.transform.Find("Root/UINode_Content/Creature/UINode_Help").GetComponent<RectTransform>();
		UIBtn_Button = GO.transform.Find("Root/UINode_Content/Creature/UIBtn_Button").GetComponent<Button>();
		UITxt_DescUnlock = GO.transform.Find("Root/UINode_Content/Creature/UITxt_DescUnlock").GetComponent<TextMeshProUGUI>();
		UIBtn_ButtonGray = GO.transform.Find("Root/UINode_Content/Creature/UIBtn_ButtonGray").GetComponent<Button>();
		UINode_LevelGroup = GO.transform.Find("Root/UINode_Content/Creature/UINode_LevelGroup").GetComponent<RectTransform>();
		UINode_Content = GO.transform.Find("Root/UINode_Content").GetComponent<RectTransform>();
		UIBtn_LeftButton = GO.transform.Find("Root/UIBtn_LeftButton").GetComponent<Button>();
		UIBtn_RightButton = GO.transform.Find("Root/UIBtn_RightButton").GetComponent<Button>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIBtn_SkipButton = GO.transform.Find("Root/UINode_SpineGroup/UIBtn_SkipButton").GetComponent<Button>();
		UINode_SpineGroup = GO.transform.Find("Root/UINode_SpineGroup").GetComponent<RectTransform>();
		UITxt_DecText = GO.transform.Find("Root/UITxt_DecText").GetComponent<TextMeshProUGUI>();
		UITxt_ProgressText = GO.transform.Find("Root/UISlider_Progress/UITxt_ProgressText").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("Root/UISlider_Progress").GetComponent<Slider>();

    }
}
