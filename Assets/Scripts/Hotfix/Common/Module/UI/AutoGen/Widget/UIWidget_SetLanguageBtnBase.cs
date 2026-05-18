/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:50:30*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_SetLanguageBtnBase : UIWidgetBase
{
	protected Button UIBtn_Select;
	protected Image UIImg_SelectedBG;

    protected override void BindComponent()
    {
		UIBtn_Select = GO.transform.Find("UIBtn_Select").GetComponent<Button>();
		UIImg_SelectedBG = GO.transform.Find("UIImg_SelectedBG").GetComponent<Image>();

    }
}
