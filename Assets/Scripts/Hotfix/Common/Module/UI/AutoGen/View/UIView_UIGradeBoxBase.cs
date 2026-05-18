/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-6 14:47:56*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_UIGradeBoxBase : UIViewBase
{
	protected Button UIBtn_CloseButton;
	protected Button UIBtn_ContinueButton;

    protected override void BindComponent()
    {
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIBtn_ContinueButton = GO.transform.Find("Root/UIBtn_ContinueButton").GetComponent<Button>();

    }
}
