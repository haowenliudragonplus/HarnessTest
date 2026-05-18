/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-18 13:44:37*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_ChooseRoomBase : UIViewBase
{
	protected Image UIImg_BlackBG;
	protected Image UIImg_BG;
	protected TextMeshProUGUI UITxt_TextTitle;
	protected Button UIBtn_BackButton;
	protected RectTransform UINode_TopGroup;
	protected RectTransform UINode_AreaContent;
	protected RectTransform UINode_RoomGroup;
	protected Image UIImg_SingleBG;
	protected Image UIImg_RoomBG;
	protected Image UIImg_SliderBG;
	protected RectTransform UINode_BGGroup;
	protected Image UIImg_RoomIcon;
	protected TextMeshProUGUI UITxt_AreaLabel;
	protected Slider UISlider_Progress;
	protected RectTransform UINode_RoomIconGroup;
	protected RectTransform UINode_Content;
	protected RectTransform UINode_SingleRoomGroup;

    protected override void BindComponent()
    {
		UIImg_BlackBG = GO.transform.Find("UIImg_BlackBG").GetComponent<Image>();
		UIImg_BG = GO.transform.Find("Root/UIImg_BG").GetComponent<Image>();
		UITxt_TextTitle = GO.transform.Find("Root/UINode_TopGroup/UITxt_TextTitle").GetComponent<TextMeshProUGUI>();
		UIBtn_BackButton = GO.transform.Find("Root/UINode_TopGroup/UIBtn_BackButton").GetComponent<Button>();
		UINode_TopGroup = GO.transform.Find("Root/UINode_TopGroup").GetComponent<RectTransform>();
		UINode_AreaContent = GO.transform.Find("Root/UINode_RoomGroup/Scroll View/Viewport/UINode_AreaContent").GetComponent<RectTransform>();
		UINode_RoomGroup = GO.transform.Find("Root/UINode_RoomGroup").GetComponent<RectTransform>();
		UIImg_SingleBG = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UINode_BGGroup/UIImg_SingleBG").GetComponent<Image>();
		UIImg_RoomBG = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UINode_BGGroup/UIImg_RoomBG").GetComponent<Image>();
		UIImg_SliderBG = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UINode_BGGroup/UIImg_SliderBG").GetComponent<Image>();
		UINode_BGGroup = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UINode_BGGroup").GetComponent<RectTransform>();
		UIImg_RoomIcon = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UIImg_RoomIcon").GetComponent<Image>();
		UITxt_AreaLabel = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UISlider_Progress/UITxt_AreaLabel").GetComponent<TextMeshProUGUI>();
		UISlider_Progress = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup/UISlider_Progress").GetComponent<Slider>();
		UINode_RoomIconGroup = GO.transform.Find("Root/UINode_SingleRoomGroup/UINode_RoomIconGroup").GetComponent<RectTransform>();
		UINode_Content = GO.transform.Find("Root/UINode_SingleRoomGroup/Scroll View/Viewport/UINode_Content").GetComponent<RectTransform>();
		UINode_SingleRoomGroup = GO.transform.Find("Root/UINode_SingleRoomGroup").GetComponent<RectTransform>();

    }
}
