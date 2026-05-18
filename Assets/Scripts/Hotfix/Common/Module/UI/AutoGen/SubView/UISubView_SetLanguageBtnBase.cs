/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-6 20:27:38*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UISubView_SetLanguageBtnBase : UISubViewBase
{
	protected Button UIBtn_Select;
	protected Image UIImg_SelectedBG;
	protected Image UIImg_Selected;

    protected override void BindComponent()
    {
		UIBtn_Select = GO.transform.Find("UIBtn_Select").GetComponent<Button>();
		UIImg_SelectedBG = GO.transform.Find("UIImg_SelectedBG").GetComponent<Image>();
		UIImg_Selected = GO.transform.Find("UIImg_Selected").GetComponent<Image>();

    }
}
