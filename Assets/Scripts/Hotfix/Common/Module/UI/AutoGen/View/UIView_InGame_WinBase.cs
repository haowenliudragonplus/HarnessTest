/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-8 10:18:6*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_InGame_WinBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_Title;
	protected TextMeshProUGUI UITxt_GoldCount;
	protected TextMeshProUGUI UITxt_Desc;
	protected RectTransform UINode_WinDesc;
	protected Slider UISlider_Progress;
	protected TextMeshProUGUI UITxt_Level;
	protected Button UIBtn_BoxIcon;
	protected Image UIImg_RawardsBG;
	protected HorizontalLayoutGroup UILayoutH_Rawards;
	protected RectTransform UINode_RewardGroup;
	protected RectTransform UINode_BoxGroup;
	protected TextMeshProUGUI UITxt_Date;
	protected RectTransform UINode_DailyChallenge;
	protected TextMeshProUGUI UITxt_BtnLevel;
	protected Button UIBtn_NextLevel;
	protected TextMeshProUGUI UITxt_BtnContinue;
	protected Button UIBtn_Continue;
	protected TextMeshProUGUI UITxt_Multiple;
	protected Button UIBtn_ViewAD;
	protected TextMeshProUGUI UITxt_HardBtnLevel;
	protected Button UIBtn_HardNextLevel;

    protected override void BindComponent()
    {
		UITxt_Title = GO.transform.Find("Root/TitleGroup/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UITxt_GoldCount = GO.transform.Find("Root/InsideGroup/UINode_WinDesc/UITxt_GoldCount").GetComponent<TextMeshProUGUI>();
		UITxt_Desc = GO.transform.Find("Root/InsideGroup/UINode_WinDesc/UITxt_Desc").GetComponent<TextMeshProUGUI>();
		UINode_WinDesc = GO.transform.Find("Root/InsideGroup/UINode_WinDesc").GetComponent<RectTransform>();
		UISlider_Progress = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UISlider_Progress").GetComponent<Slider>();
		UITxt_Level = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UITxt_Level").GetComponent<TextMeshProUGUI>();
		UIBtn_BoxIcon = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UIBtn_BoxIcon").GetComponent<Button>();
		UIImg_RawardsBG = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UINode_RewardGroup/UIImg_RawardsBG").GetComponent<Image>();
		UILayoutH_Rawards = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UINode_RewardGroup/UILayoutH_Rawards").GetComponent<HorizontalLayoutGroup>();
		UINode_RewardGroup = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup/UINode_RewardGroup").GetComponent<RectTransform>();
		UINode_BoxGroup = GO.transform.Find("Root/InsideGroup/UINode_BoxGroup").GetComponent<RectTransform>();
		UITxt_Date = GO.transform.Find("Root/InsideGroup/UINode_DailyChallenge/UITxt_Date").GetComponent<TextMeshProUGUI>();
		UINode_DailyChallenge = GO.transform.Find("Root/InsideGroup/UINode_DailyChallenge").GetComponent<RectTransform>();
		UITxt_BtnLevel = GO.transform.Find("Root/ButtonGroup/UIBtn_NextLevel/UITxt_BtnLevel").GetComponent<TextMeshProUGUI>();
		UIBtn_NextLevel = GO.transform.Find("Root/ButtonGroup/UIBtn_NextLevel").GetComponent<Button>();
		UITxt_BtnContinue = GO.transform.Find("Root/ButtonGroup/UIBtn_Continue/UITxt_BtnContinue").GetComponent<TextMeshProUGUI>();
		UIBtn_Continue = GO.transform.Find("Root/ButtonGroup/UIBtn_Continue").GetComponent<Button>();
		UITxt_Multiple = GO.transform.Find("Root/ButtonGroup/UIBtn_ViewAD/UITxt_Multiple").GetComponent<TextMeshProUGUI>();
		UIBtn_ViewAD = GO.transform.Find("Root/ButtonGroup/UIBtn_ViewAD").GetComponent<Button>();
		UITxt_HardBtnLevel = GO.transform.Find("Root/ButtonGroup/UIBtn_HardNextLevel/UITxt_HardBtnLevel").GetComponent<TextMeshProUGUI>();
		UIBtn_HardNextLevel = GO.transform.Find("Root/ButtonGroup/UIBtn_HardNextLevel").GetComponent<Button>();

    }
}
