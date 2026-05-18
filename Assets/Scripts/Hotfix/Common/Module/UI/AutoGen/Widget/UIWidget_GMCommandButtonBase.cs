/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-8-16 17:49:4*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_GMCommandButtonBase : UIWidgetBase
{
	protected Text UIOldTxt_Name;
	protected Button UIBtn_Tip;
	protected Button UIBtn_Command;

    protected override void BindComponent()
    {
		UIOldTxt_Name = GO.transform.Find("UIBtn_Command/UIOldTxt_Name").GetComponent<Text>();
		UIBtn_Tip = GO.transform.Find("UIBtn_Command/UIBtn_Tip").GetComponent<Button>();
		UIBtn_Command = GO.transform.Find("UIBtn_Command").GetComponent<Button>();

    }
}
