/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-9 14:54:31*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UISubView_DecorationMainBase : UISubViewBase
{
	protected Button UIBtn_FullScreenClose;
	protected RectTransform UINode_SafeArea;
	protected RectTransform UINode_PlaceBtnClone;
	protected Button UIBtn_CloseButton;
	protected Button UIBtn_CloseSelectButton;
	protected HorizontalLayoutGroup UILayoutH_FurnituresLayout;
	protected Button UIBtn_SureSelectButton;
	protected Button UIBtn_ADSButton;
	protected Button UIBtn_ADSLoadingButton;
	protected Button UIBtn_ADSButtonBottomLong;
	protected Button UIBtn_ADSLoadingButtonBottomLong;
	protected Button UIBtn_CollectionJumpButton;
	protected TextMeshProUGUI UITxt_DecorationText;
	protected TextMeshProUGUI UITxt_RewardText;
	protected TextMeshProUGUI UITxt_BottomDecorationText;
	protected RectTransform UINode_SelectUI;
	protected RectTransform UINode_FurniturePointButton;
	protected Button UIBtn_RoomList;
	protected Button UIBtn_Back;
	protected RectTransform UINode_ScreenOrigin;

    protected override void BindComponent()
    {
		UIBtn_FullScreenClose = GO.transform.Find("UIBtn_FullScreenClose").GetComponent<Button>();
		UINode_SafeArea = GO.transform.Find("UINode_SafeArea").GetComponent<RectTransform>();
		UINode_PlaceBtnClone = GO.transform.Find("UINode_PlaceBtnClone").GetComponent<RectTransform>();
		UIBtn_CloseButton = GO.transform.Find("CloseButton/UIBtn_CloseButton").GetComponent<Button>();
		UIBtn_CloseSelectButton = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/CloseSelectButton/UIBtn_CloseSelectButton").GetComponent<Button>();
		UILayoutH_FurnituresLayout = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/UILayoutH_FurnituresLayout").GetComponent<HorizontalLayoutGroup>();
		UIBtn_SureSelectButton = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/SureSelectButton/UIBtn_SureSelectButton").GetComponent<Button>();
		UIBtn_ADSButton = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/ADSButton/UIBtn_ADSButton").GetComponent<Button>();
		UIBtn_ADSLoadingButton = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/ADSLoadingButton/UIBtn_ADSLoadingButton").GetComponent<Button>();
		UIBtn_ADSButtonBottomLong = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/ADSButtonBottomLong/UIBtn_ADSButtonBottomLong").GetComponent<Button>();
		UIBtn_ADSLoadingButtonBottomLong = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/ADSLoadingButtonBottomLong/UIBtn_ADSLoadingButtonBottomLong").GetComponent<Button>();
		UIBtn_CollectionJumpButton = GO.transform.Find("UINode_SelectUI/SelectFurnituresGroup/CollectionJumpButton/UIBtn_CollectionJumpButton").GetComponent<Button>();
		UITxt_DecorationText = GO.transform.Find("UINode_SelectUI/DecorationGroup/UITxt_DecorationText").GetComponent<TextMeshProUGUI>();
		UITxt_RewardText = GO.transform.Find("UINode_SelectUI/DecorationGroup/UITxt_RewardText").GetComponent<TextMeshProUGUI>();
		UITxt_BottomDecorationText = GO.transform.Find("UINode_SelectUI/DecBottomGroup/UITxt_BottomDecorationText").GetComponent<TextMeshProUGUI>();
		UINode_SelectUI = GO.transform.Find("UINode_SelectUI").GetComponent<RectTransform>();
		UINode_FurniturePointButton = GO.transform.Find("UINode_ScreenOrigin/UINode_FurniturePointButton").GetComponent<RectTransform>();
		UIBtn_RoomList = GO.transform.Find("UINode_ScreenOrigin/UIBtn_RoomList").GetComponent<Button>();
		UIBtn_Back = GO.transform.Find("UINode_ScreenOrigin/UIBtn_Back").GetComponent<Button>();
		UINode_ScreenOrigin = GO.transform.Find("UINode_ScreenOrigin").GetComponent<RectTransform>();

    }
}
