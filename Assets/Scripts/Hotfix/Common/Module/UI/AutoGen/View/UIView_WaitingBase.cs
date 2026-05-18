/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-7 19:41:50*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_WaitingBase : UIViewBase
{
	protected Image UIImg_Bg;
	protected Image UIImg_Icon;

    protected override void BindComponent()
    {
		UIImg_Bg = GO.transform.Find("Root/UIImg_Bg").GetComponent<Image>();
		UIImg_Icon = GO.transform.Find("Root/UIImg_Icon").GetComponent<Image>();

    }
}
