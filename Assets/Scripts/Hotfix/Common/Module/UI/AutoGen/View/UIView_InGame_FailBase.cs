/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-4-27 10:10:33*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIView_InGame_FailBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;
	protected Button UIBtn_Restart;
	protected RectTransform UINode_WatchAds;
	protected RectTransform UINode_NoAds;
	protected Button UIBtn_Revive;
	protected Image UIImg_AddSteps;
	protected Image UIImg_HeartIcon;
	protected RectTransform UINode_Heart;
	protected TextMeshProUGUI UITxt_Desc;
	protected RectTransform UINode_Fail;

    protected override void BindComponent()
    {
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIBtn_Restart = GO.transform.Find("Root/ButtonGroup/UIBtn_Restart").GetComponent<Button>();
		UINode_WatchAds = GO.transform.Find("Root/ButtonGroup/UIBtn_Revive/UINode_WatchAds").GetComponent<RectTransform>();
		UINode_NoAds = GO.transform.Find("Root/ButtonGroup/UIBtn_Revive/UINode_NoAds").GetComponent<RectTransform>();
		UIBtn_Revive = GO.transform.Find("Root/ButtonGroup/UIBtn_Revive").GetComponent<Button>();
		UIImg_AddSteps = GO.transform.Find("Root/UINode_Fail/UIImg_AddSteps").GetComponent<Image>();
		UIImg_HeartIcon = GO.transform.Find("Root/UINode_Fail/UINode_Heart/UIImg_HeartIcon").GetComponent<Image>();
		UINode_Heart = GO.transform.Find("Root/UINode_Fail/UINode_Heart").GetComponent<RectTransform>();
		UITxt_Desc = GO.transform.Find("Root/UINode_Fail/UITxt_Desc").GetComponent<TextMeshProUGUI>();
		UINode_Fail = GO.transform.Find("Root/UINode_Fail").GetComponent<RectTransform>();

    }
}
