/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-9 20:4:45*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;

public class UIWidget_TipBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_Text;
	protected Image UIImg_Bg;

    protected override void BindComponent()
    {
		UITxt_Text = GO.transform.Find("UIImg_Bg/UITxt_Text").GetComponent<TextMeshProUGUI>();
		UIImg_Bg = GO.transform.Find("UIImg_Bg").GetComponent<Image>();

    }
}
