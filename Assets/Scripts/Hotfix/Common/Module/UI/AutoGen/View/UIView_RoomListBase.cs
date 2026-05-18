/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-6 15:30:54*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIView_RoomListBase : UIViewBase
{
	protected Image UIImg_BlackBG;
	protected Image UIImg_BG;
	protected Button UIBtn_BackButton;
	protected RectTransform UINode_TopGroup;
	protected RectTransform UINode_Content;
	protected RectTransform UINode_RoomGroup;

    protected override void BindComponent()
    {
		UIImg_BlackBG = GO.transform.Find("UIImg_BlackBG").GetComponent<Image>();
		UIImg_BG = GO.transform.Find("Root/UIImg_BG").GetComponent<Image>();
		UIBtn_BackButton = GO.transform.Find("Root/UINode_TopGroup/UIBtn_BackButton").GetComponent<Button>();
		UINode_TopGroup = GO.transform.Find("Root/UINode_TopGroup").GetComponent<RectTransform>();
		UINode_Content = GO.transform.Find("Root/UINode_RoomGroup/Scroll View/Viewport/UINode_Content").GetComponent<RectTransform>();
		UINode_RoomGroup = GO.transform.Find("Root/UINode_RoomGroup").GetComponent<RectTransform>();

    }
}
