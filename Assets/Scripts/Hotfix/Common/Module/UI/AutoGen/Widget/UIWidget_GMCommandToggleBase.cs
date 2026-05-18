/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-8-16 17:49:10*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_GMCommandToggleBase : UIWidgetBase
{
	protected Text UIOldTxt_Name;
	protected Button UIBtn_Tip;
	protected Toggle UIToggle_Command;

    protected override void BindComponent()
    {
		UIOldTxt_Name = GO.transform.Find("UIToggle_Command/UIOldTxt_Name").GetComponent<Text>();
		UIBtn_Tip = GO.transform.Find("UIToggle_Command/UIBtn_Tip").GetComponent<Button>();
		UIToggle_Command = GO.transform.Find("UIToggle_Command").GetComponent<Toggle>();

    }
}
