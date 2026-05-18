/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-10-10 14:39:35*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_ContactUsItem2Base : UIWidgetBase
{
	protected Text UIOldTxt_Text;
	protected Text UIOldTxt_TimeText;

    protected override void BindComponent()
    {
		UIOldTxt_Text = GO.transform.Find("UILayooutV_Image/UILayooutV_Image/UIOldTxt_Text").GetComponent<Text>();
		UIOldTxt_TimeText = GO.transform.Find("UILayooutV_Image/UILayooutV_Image/UIOldTxt_TimeText").GetComponent<Text>();

    }
}
