/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-20 11:50:22*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIView_AreaTaskMainBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_Close;
	protected RectTransform UINode_TitleGroup;
	protected TextMeshProUGUI UITxt_DecsText;
	protected GridLayoutGroup UILayoutHV_Task;
	protected RectTransform UINode_TaskGroup;
	protected TextMeshProUGUI UITxt_ProgressLabel;
	protected Slider UISlider_Progress;
	protected Button UIBtn_BoxIcon;
	protected Button UIBtn_BoxBottomIcon;
	protected Button UIBtn_BoxTopIcon;
	protected Button UIBtn_BoxIconOpen;
	protected HorizontalLayoutGroup UILayoutH_Rawards;
	protected Image UIImg_RawardsBG;
	protected RectTransform UINode_RewardGroup;
	protected RectTransform UINode_BoxGroup;
	protected TextMeshProUGUI UITxt_ComeText;
	protected RectTransform UINode_CommingSoon;
	protected RectTransform UINode_MiddleGroup;
	protected TextMeshProUGUI UITxt_HouseName;
	protected TextMeshProUGUI UITxt_HouseNowName;
	protected TextMeshProUGUI UITxt_HouseText;
	protected Button UIBtn_HouseButton;
	protected RectTransform UINode_House;
	protected TextMeshProUGUI UITxt_AsmrName;
	protected TextMeshProUGUI UITxt_AsmrText;
	protected Button UIBtn_AsmrButton;
	protected RectTransform UINode_Makeover;
	protected RectTransform UINode_BottomGroup;

    protected override void BindComponent()
    {
		UITxt_TitleText = GO.transform.Find("Root/UINode_TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_Close = GO.transform.Find("Root/UINode_TitleGroup/UIBtn_Close").GetComponent<Button>();
		UINode_TitleGroup = GO.transform.Find("Root/UINode_TitleGroup").GetComponent<RectTransform>();
		UITxt_DecsText = GO.transform.Find("Root/UINode_MiddleGroup/UITxt_DecsText").GetComponent<TextMeshProUGUI>();
		UILayoutHV_Task = GO.transform.Find("Root/UINode_MiddleGroup/UINode_TaskGroup/Scroll View/Viewport/UILayoutHV_Task").GetComponent<GridLayoutGroup>();
		UINode_TaskGroup = GO.transform.Find("Root/UINode_MiddleGroup/UINode_TaskGroup").GetComponent<RectTransform>();
		UITxt_ProgressLabel = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UISlider_Progress/UITxt_ProgressLabel").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UISlider_Progress").GetComponent<Slider>();
		UIBtn_BoxIcon = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UIBtn_BoxIcon").GetComponent<Button>();
		UIBtn_BoxBottomIcon = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UIBtn_BoxBottomIcon").GetComponent<Button>();
		UIBtn_BoxTopIcon = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UIBtn_BoxTopIcon").GetComponent<Button>();
		UIBtn_BoxIconOpen = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UIBtn_BoxIconOpen").GetComponent<Button>();
		UILayoutH_Rawards = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UINode_RewardGroup/UILayoutH_Rawards").GetComponent<HorizontalLayoutGroup>();
		UIImg_RawardsBG = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UINode_RewardGroup/UIImg_RawardsBG").GetComponent<Image>();
		UINode_RewardGroup = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup/UINode_RewardGroup").GetComponent<RectTransform>();
		UINode_BoxGroup = GO.transform.Find("Root/UINode_MiddleGroup/UINode_BoxGroup").GetComponent<RectTransform>();
		UITxt_ComeText = GO.transform.Find("Root/UINode_MiddleGroup/UINode_CommingSoon/UITxt_ComeText").GetComponent<TextMeshProUGUI>();
		UINode_CommingSoon = GO.transform.Find("Root/UINode_MiddleGroup/UINode_CommingSoon").GetComponent<RectTransform>();
		UINode_MiddleGroup = GO.transform.Find("Root/UINode_MiddleGroup").GetComponent<RectTransform>();
		UITxt_HouseName = GO.transform.Find("Root/UINode_BottomGroup/UINode_House/UITxt_HouseName").GetComponent<TextMeshProUGUI>();
		UITxt_HouseNowName = GO.transform.Find("Root/UINode_BottomGroup/UINode_House/UITxt_HouseNowName").GetComponent<TextMeshProUGUI>();
		UITxt_HouseText = GO.transform.Find("Root/UINode_BottomGroup/UINode_House/UIBtn_HouseButton/UITxt_HouseText").GetComponent<TextMeshProUGUI>();
		UIBtn_HouseButton = GO.transform.Find("Root/UINode_BottomGroup/UINode_House/UIBtn_HouseButton").GetComponent<Button>();
		UINode_House = GO.transform.Find("Root/UINode_BottomGroup/UINode_House").GetComponent<RectTransform>();
		UITxt_AsmrName = GO.transform.Find("Root/UINode_BottomGroup/UINode_Makeover/UITxt_AsmrName").GetComponent<TextMeshProUGUI>();
		UITxt_AsmrText = GO.transform.Find("Root/UINode_BottomGroup/UINode_Makeover/UIBtn_AsmrButton/UITxt_AsmrText").GetComponent<TextMeshProUGUI>();
		UIBtn_AsmrButton = GO.transform.Find("Root/UINode_BottomGroup/UINode_Makeover/UIBtn_AsmrButton").GetComponent<Button>();
		UINode_Makeover = GO.transform.Find("Root/UINode_BottomGroup/UINode_Makeover").GetComponent<RectTransform>();
		UINode_BottomGroup = GO.transform.Find("Root/UINode_BottomGroup").GetComponent<RectTransform>();

    }
}
