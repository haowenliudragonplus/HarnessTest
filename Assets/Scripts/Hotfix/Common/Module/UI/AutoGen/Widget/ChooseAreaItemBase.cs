/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-5 15:49:27*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChooseAreaItemBase : UIWidgetBase
{
	protected Image UIImg_BG;
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_ProgressNormalLabel;
	protected Slider UISlider_Progress;
	protected Button UIBtn_ContinueButton;
	protected RectTransform UINode_NormalGroup;
	protected Button UIBtn_ViewButton;
	protected Image UIImg_FinishIcon;
	protected TextMeshProUGUI UITxt_FinishText;
	protected RectTransform UINode_CompletedGroup;
	protected RectTransform UINode_FinishGroup;
	protected Image UIImg_Mask;
	protected TextMeshProUGUI UITxt_ProgressLabel;
	protected Slider UISlider_LockProgress;
	protected Image UIImg_LockIcon;
	protected RectTransform UINode_LockGroup;
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_NameGroup;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("UIImg_BG").GetComponent<Image>();
		UIImg_Icon = GO.transform.Find("UIImg_Icon").GetComponent<Image>();
		UITxt_ProgressNormalLabel = GO.transform.Find("UINode_NormalGroup/UISlider_Progress/UITxt_ProgressNormalLabel").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("UINode_NormalGroup/UISlider_Progress").GetComponent<Slider>();
		UIBtn_ContinueButton = GO.transform.Find("UINode_NormalGroup/UIBtn_ContinueButton").GetComponent<Button>();
		UINode_NormalGroup = GO.transform.Find("UINode_NormalGroup").GetComponent<RectTransform>();
		UIBtn_ViewButton = GO.transform.Find("UINode_FinishGroup/UIBtn_ViewButton").GetComponent<Button>();
		UIImg_FinishIcon = GO.transform.Find("UINode_FinishGroup/UINode_CompletedGroup/UIImg_FinishIcon").GetComponent<Image>();
		UITxt_FinishText = GO.transform.Find("UINode_FinishGroup/UINode_CompletedGroup/UITxt_FinishText").GetComponent<TextMeshProUGUI>();
		UINode_CompletedGroup = GO.transform.Find("UINode_FinishGroup/UINode_CompletedGroup").GetComponent<RectTransform>();
		UINode_FinishGroup = GO.transform.Find("UINode_FinishGroup").GetComponent<RectTransform>();
		UIImg_Mask = GO.transform.Find("UINode_LockGroup/UIImg_Mask").GetComponent<Image>();
		UITxt_ProgressLabel = GO.transform.Find("UINode_LockGroup/UISlider_LockProgress/UITxt_ProgressLabel").GetComponent<TextMeshProUGUI>();
		UISlider_LockProgress = GO.transform.Find("UINode_LockGroup/UISlider_LockProgress").GetComponent<Slider>();
		UIImg_LockIcon = GO.transform.Find("UINode_LockGroup/UIImg_LockIcon").GetComponent<Image>();
		UINode_LockGroup = GO.transform.Find("UINode_LockGroup").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("UINode_NameGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_NameGroup = GO.transform.Find("UINode_NameGroup").GetComponent<RectTransform>();

    }

   
}
