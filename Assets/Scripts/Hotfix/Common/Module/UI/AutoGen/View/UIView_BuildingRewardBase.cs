/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-19 21:17:9*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;

public class UIView_BuildingRewardBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_Progress;
	protected Image UIImg_Level_Reach;
	protected Image UIImg_UnReach;
	protected TextMeshProUGUI UITxt_NextLevel;
	protected Slider UISlider_Progress;
	protected TextMeshProUGUI UITxt_Title;
	protected Image UIImg_Avatar;
	protected UIContainer UIContainer_BuildingReward;
	protected ScrollRect UISR_ScrollView;
	protected Button UIBtn_Close;

    protected override void BindComponent()
    {
		UITxt_Progress = GO.transform.Find("Root/MiddleGroup/TitleGroup/UISlider_Progress/UITxt_Progress").GetComponent<TextMeshProUGUI>();
		UIImg_Level_Reach = GO.transform.Find("Root/MiddleGroup/TitleGroup/UISlider_Progress/RankGroup/UIImg_Level_Reach").GetComponent<Image>();
		UIImg_UnReach = GO.transform.Find("Root/MiddleGroup/TitleGroup/UISlider_Progress/RankGroup/UIImg_UnReach").GetComponent<Image>();
		UITxt_NextLevel = GO.transform.Find("Root/MiddleGroup/TitleGroup/UISlider_Progress/RankGroup/UITxt_NextLevel").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("Root/MiddleGroup/TitleGroup/UISlider_Progress").GetComponent<Slider>();
		UITxt_Title = GO.transform.Find("Root/MiddleGroup/TitleGroup/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UIImg_Avatar = GO.transform.Find("Root/MiddleGroup/TitleGroup/Avatar/UIImg_Avatar").GetComponent<Image>();
		UIContainer_BuildingReward =new UIContainer();
		UIContainer_BuildingReward.InternalInit(GO.transform.Find("Root/MiddleGroup/RewardGroup/UISR_ScrollView/Viewport/UIContainer_BuildingReward").gameObject , this);
		UISR_ScrollView = GO.transform.Find("Root/MiddleGroup/RewardGroup/UISR_ScrollView").GetComponent<ScrollRect>();
		UIBtn_Close = GO.transform.Find("Root/MiddleGroup/UIBtn_Close").GetComponent<Button>();

    }
}
