/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-11-30 14:19:41*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_PopupNoMoneyBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected Button UIBtn_ButtonStart;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("Root/WindowsGroup/UIBtn_Close").GetComponent<Button>();
		UIBtn_ButtonStart = GO.transform.Find("Root/WindowsGroup/UIBtn_ButtonStart").GetComponent<Button>();

    }
}
