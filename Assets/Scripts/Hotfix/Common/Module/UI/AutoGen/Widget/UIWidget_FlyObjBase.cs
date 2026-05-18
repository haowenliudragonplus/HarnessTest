/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-5-7 14:45:13*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_FlyObjBase : UIWidgetBase
{
	protected Image UIImg_Icon;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("Node/UIImg_Icon").GetComponent<Image>();

    }
}
