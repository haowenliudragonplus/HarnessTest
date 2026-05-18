/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-2-26 20:56:39*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIWidget_EndlessGiftItemBase : UIWidgetBase
{
	protected Image UIImg_BG;
	protected Image UIImg_ClaimedBG;
	protected RectTransform UINode_BubbleGroup;
	protected Image UIImg_Received;
	protected Button UIBtn_ClaimButton;
	protected Button UIBtn_BuyButton;
	protected Image UIImg_Lock;
	protected RectTransform UINode_ButtonGroup;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("UIImg_BG").GetComponent<Image>();
		UIImg_ClaimedBG = GO.transform.Find("UIImg_ClaimedBG").GetComponent<Image>();
		UINode_BubbleGroup = GO.transform.Find("UINode_BubbleGroup").GetComponent<RectTransform>();
		UIImg_Received = GO.transform.Find("UINode_ButtonGroup/UIImg_Received").GetComponent<Image>();
		UIBtn_ClaimButton = GO.transform.Find("UINode_ButtonGroup/UIBtn_ClaimButton").GetComponent<Button>();
		UIBtn_BuyButton = GO.transform.Find("UINode_ButtonGroup/UIBtn_BuyButton").GetComponent<Button>();
		UIImg_Lock = GO.transform.Find("UINode_ButtonGroup/UIImg_Lock").GetComponent<Image>();
		UINode_ButtonGroup = GO.transform.Find("UINode_ButtonGroup").GetComponent<RectTransform>();

    }
}
