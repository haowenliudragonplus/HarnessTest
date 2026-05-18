/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-5-7 14:45:28*****/
/*****************************/

using Framework;
using TMPro;

public class UIWidget_FlyObjCountBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_NumberText;

    protected override void BindComponent()
    {
		UITxt_NumberText = GO.transform.Find("UITxt_NumberText").GetComponent<TextMeshProUGUI>();

    }
}
