/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-3-17 20:35:48*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UISubView_WeeklyChallengeBase : UISubViewBase
{
	protected Slider UISlider_WeeklyChallenge;
	protected TextMeshProUGUI UITxt_WeeklyChallengeProgress;
	protected TextMeshProUGUI UITxt_ContnessText;
	protected RectTransform UINode_WeeklyChallengeContnessGroup;
	protected TextMeshProUGUI UITxt_TagText;
	protected RectTransform UINode_WeeklyChallengeTagGroup;
	protected TextMeshProUGUI UITxt_DoubleText;
	protected RectTransform UINode_WeeklyChallengeDoubleGroup;
	protected Image UIImg_WeeklyChallenge;
	protected TextMeshProUGUI UITxt_WeeklyChallengeRewardNum;
	protected RectTransform UINode_WeeklyChallengeRewardNum;
	protected RectTransform UINode_Double;
	protected RectTransform UINode_Countless;
	protected Image UIImg_WeeklyChallengeReward;
	protected TextMeshProUGUI UITxt_Time;
	protected RectTransform UINode_Time1;
	protected RectTransform UINode_NonetworkGroup1;
	protected RectTransform UINode_WeeklyChallengeNormalGroup;
	protected TextMeshProUGUI UITxt_WeeklyLockDecsTxt;
	protected TextMeshProUGUI UITxt_WeeklyFinishTxt;
	protected RectTransform UINode_NonetworkGroup2;
	protected RectTransform UINode_WeeklyChallengeLockGroup;

    protected override void BindComponent()
    {
		UISlider_WeeklyChallenge = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/UISlider_WeeklyChallenge").GetComponent<Slider>();
		UITxt_WeeklyChallengeProgress = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/UITxt_WeeklyChallengeProgress").GetComponent<TextMeshProUGUI>();
		UITxt_ContnessText = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeContnessGroup/UITxt_ContnessText").GetComponent<TextMeshProUGUI>();
		UINode_WeeklyChallengeContnessGroup = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeContnessGroup").GetComponent<RectTransform>();
		UITxt_TagText = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeTagGroup/UITxt_TagText").GetComponent<TextMeshProUGUI>();
		UINode_WeeklyChallengeTagGroup = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeTagGroup").GetComponent<RectTransform>();
		UITxt_DoubleText = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeDoubleGroup/UITxt_DoubleText").GetComponent<TextMeshProUGUI>();
		UINode_WeeklyChallengeDoubleGroup = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge/UINode_WeeklyChallengeDoubleGroup").GetComponent<RectTransform>();
		UIImg_WeeklyChallenge = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallenge").GetComponent<Image>();
		UITxt_WeeklyChallengeRewardNum = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallengeReward/UINode_WeeklyChallengeRewardNum/UITxt_WeeklyChallengeRewardNum").GetComponent<TextMeshProUGUI>();
		UINode_WeeklyChallengeRewardNum = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallengeReward/UINode_WeeklyChallengeRewardNum").GetComponent<RectTransform>();
		UINode_Double = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallengeReward/UINode_Double").GetComponent<RectTransform>();
		UINode_Countless = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallengeReward/UINode_Countless").GetComponent<RectTransform>();
		UIImg_WeeklyChallengeReward = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/IconGroup/UIImg_WeeklyChallengeReward").GetComponent<Image>();
		UITxt_Time = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/UINode_Time1/UITxt_Time").GetComponent<TextMeshProUGUI>();
		UINode_Time1 = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/UINode_Time1").GetComponent<RectTransform>();
		UINode_NonetworkGroup1 = GO.transform.Find("UINode_WeeklyChallengeNormalGroup/UINode_NonetworkGroup1").GetComponent<RectTransform>();
		UINode_WeeklyChallengeNormalGroup = GO.transform.Find("UINode_WeeklyChallengeNormalGroup").GetComponent<RectTransform>();
		UITxt_WeeklyLockDecsTxt = GO.transform.Find("UINode_WeeklyChallengeLockGroup/UITxt_WeeklyLockDecsTxt").GetComponent<TextMeshProUGUI>();
		UITxt_WeeklyFinishTxt = GO.transform.Find("UINode_WeeklyChallengeLockGroup/UITxt_WeeklyFinishTxt").GetComponent<TextMeshProUGUI>();
		UINode_NonetworkGroup2 = GO.transform.Find("UINode_WeeklyChallengeLockGroup/UINode_NonetworkGroup2").GetComponent<RectTransform>();
		UINode_WeeklyChallengeLockGroup = GO.transform.Find("UINode_WeeklyChallengeLockGroup").GetComponent<RectTransform>();

    }
}
