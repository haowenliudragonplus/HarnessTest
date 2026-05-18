/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-8 16:31:13*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWidget_AchievementTaskItemBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_AchievementName;
	protected TextMeshProUGUI UITxt_TaskDesc;
	protected RectTransform UINode_InProgress;
	protected Button UIBtn_Receive;
	protected TextMeshProUGUI UITxt_AchievementName_Completed;
	protected TextMeshProUGUI UITxt_TaskDesc_Completed;
	protected RectTransform UINode_Completed;
	protected Image UIImg_AchievementMedalIcon;
	protected TextMeshProUGUI UITxt_Progress;
	protected Slider UISlider_Progress;

    protected override void BindComponent()
    {
		UITxt_AchievementName = GO.transform.Find("Root/UITxt_AchievementName").GetComponent<TextMeshProUGUI>();
		UITxt_TaskDesc = GO.transform.Find("Root/UITxt_TaskDesc").GetComponent<TextMeshProUGUI>();
		UINode_InProgress = GO.transform.Find("Root/BtnGroup/UINode_InProgress").GetComponent<RectTransform>();
		UIBtn_Receive = GO.transform.Find("Root/BtnGroup/UIBtn_Receive").GetComponent<Button>();
		UITxt_AchievementName_Completed = GO.transform.Find("Root/UINode_Completed/UITxt_AchievementName_Completed").GetComponent<TextMeshProUGUI>();
		UITxt_TaskDesc_Completed = GO.transform.Find("Root/UINode_Completed/UITxt_TaskDesc_Completed").GetComponent<TextMeshProUGUI>();
		UINode_Completed = GO.transform.Find("Root/UINode_Completed").GetComponent<RectTransform>();
		UIImg_AchievementMedalIcon = GO.transform.Find("Root/UIImg_AchievementMedalIcon").GetComponent<Image>();
		UITxt_Progress = GO.transform.Find("Root/UISlider_Progress/UITxt_Progress").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("Root/UISlider_Progress").GetComponent<Slider>();

    }
}
