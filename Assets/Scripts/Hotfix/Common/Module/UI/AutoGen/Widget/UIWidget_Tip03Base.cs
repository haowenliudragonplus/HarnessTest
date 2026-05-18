/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-9 14:5:46*****/
/*****************************/

using Framework;
using TMPro;

public class UIWidget_Tip03Base : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_Text;

    protected override void BindComponent()
    {
		UITxt_Text = GO.transform.Find("UITxt_Text").GetComponent<TextMeshProUGUI>();

    }
}
