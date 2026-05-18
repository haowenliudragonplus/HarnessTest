/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-9 17:20:45*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_ComboTipBase : UIWidgetBase
{
	protected Image UIImg_Bg;
	protected TextMeshProUGUI UITxt_Score;

    protected override void BindComponent()
    {
		UIImg_Bg = GO.transform.Find("Root/UIImg_Bg").GetComponent<Image>();
		UITxt_Score = GO.transform.Find("Root/UITxt_Score").GetComponent<TextMeshProUGUI>();

    }
}
