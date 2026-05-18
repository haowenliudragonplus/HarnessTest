/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-13 13:47:40*****/
/*****************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView_AchievementMainBase : UIViewBase
{
	protected RectTransform UINode_TitleGroup;
	protected RectTransform UINode_Task_UnSelected;
	protected RectTransform UINode_Task_Selected;
	protected RectTransform UINode_RedDot;
	protected Button UIBtn_Task;
	protected RectTransform UINode_MyAchievements_UnSelected;
	protected RectTransform UINode_MyAchievements_Selected;
	protected RectTransform UINode_RedDot2;
	protected Button UIBtn_MyAchievements;
	protected TextMeshProUGUI UITxt_MedalCount;
	protected RectTransform UINode_Desc;
	protected RectTransform UINode_AchievementTopItem;
	protected RectTransform UINode_LabelGroup;
	protected RectTransform UINode_Content_01;
	protected RectTransform UINode_TaskList;
	protected RectTransform UINode_Content_02;
	protected RectTransform UINode_AchievementList;
	protected Image UIImg_RedDot;
	protected Button UIBtn_Achievement;
	protected Button UIBtn_Home;
	protected Button UIBtn_Lock;
	protected RectTransform UINode_Bubble;
	protected RectTransform UINode_Lock;
	protected RectTransform UINode_Bottom;

    protected override void BindComponent()
    {
		UINode_TitleGroup = GO.transform.Find("Root/UINode_TitleGroup").GetComponent<RectTransform>();
		UINode_Task_UnSelected = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_Task/UINode_Task_UnSelected").GetComponent<RectTransform>();
		UINode_Task_Selected = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_Task/UINode_Task_Selected").GetComponent<RectTransform>();
		UINode_RedDot = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_Task/UINode_RedDot").GetComponent<RectTransform>();
		UIBtn_Task = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_Task").GetComponent<Button>();
		UINode_MyAchievements_UnSelected = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_MyAchievements/UINode_MyAchievements_UnSelected").GetComponent<RectTransform>();
		UINode_MyAchievements_Selected = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_MyAchievements/UINode_MyAchievements_Selected").GetComponent<RectTransform>();
		UINode_RedDot2 = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_MyAchievements/UINode_RedDot2").GetComponent<RectTransform>();
		UIBtn_MyAchievements = GO.transform.Find("Root/UINode_LabelGroup/UIBtn_MyAchievements").GetComponent<Button>();
		UITxt_MedalCount = GO.transform.Find("Root/UINode_LabelGroup/UINode_AchievementTopItem/UITxt_MedalCount").GetComponent<TextMeshProUGUI>();
		UINode_Desc = GO.transform.Find("Root/UINode_LabelGroup/UINode_AchievementTopItem/UINode_Desc").GetComponent<RectTransform>();
		UINode_AchievementTopItem = GO.transform.Find("Root/UINode_LabelGroup/UINode_AchievementTopItem").GetComponent<RectTransform>();
		UINode_LabelGroup = GO.transform.Find("Root/UINode_LabelGroup").GetComponent<RectTransform>();
		UINode_Content_01 = GO.transform.Find("Root/MiddleGruop/UINode_TaskList/Viewport/UINode_Content_01").GetComponent<RectTransform>();
		UINode_TaskList = GO.transform.Find("Root/MiddleGruop/UINode_TaskList").GetComponent<RectTransform>();
		UINode_Content_02 = GO.transform.Find("Root/MiddleGruop/UINode_AchievementList/Viewport/UINode_Content_02").GetComponent<RectTransform>();
		UINode_AchievementList = GO.transform.Find("Root/MiddleGruop/UINode_AchievementList").GetComponent<RectTransform>();
		UIImg_RedDot = GO.transform.Find("Root/UINode_Bottom/UIBtn_Achievement/UIImg_RedDot").GetComponent<Image>();
		UIBtn_Achievement = GO.transform.Find("Root/UINode_Bottom/UIBtn_Achievement").GetComponent<Button>();
		UIBtn_Home = GO.transform.Find("Root/UINode_Bottom/UIBtn_Home").GetComponent<Button>();
		UIBtn_Lock = GO.transform.Find("Root/UINode_Bottom/UINode_Lock/UIBtn_Lock").GetComponent<Button>();
		UINode_Bubble = GO.transform.Find("Root/UINode_Bottom/UINode_Lock/UINode_Bubble").GetComponent<RectTransform>();
		UINode_Lock = GO.transform.Find("Root/UINode_Bottom/UINode_Lock").GetComponent<RectTransform>();
		UINode_Bottom = GO.transform.Find("Root/UINode_Bottom").GetComponent<RectTransform>();

    }
}
