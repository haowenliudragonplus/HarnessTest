/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-10-10 14:42:56*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIView_ContactUsItemBase : UIWidgetBase
{
	protected Text UIOldTxt_Text;
	protected RectTransform UINode__ContactGroup;
	protected Text UIOldTxt_TimeText;
	protected RectTransform UINode_vImage;

    protected override void BindComponent()
    {
		UIOldTxt_Text = GO.transform.Find("UILayooutV_Image/UINode_vImage/UIOldTxt_Text").GetComponent<Text>();
		UINode__ContactGroup = GO.transform.Find("UILayooutV_Image/UINode_vImage/UINode__ContactGroup").GetComponent<RectTransform>();
		UIOldTxt_TimeText = GO.transform.Find("UILayooutV_Image/UINode_vImage/UIOldTxt_TimeText").GetComponent<Text>();
		UINode_vImage = GO.transform.Find("UILayooutV_Image/UINode_vImage").GetComponent<RectTransform>();

    }
}
