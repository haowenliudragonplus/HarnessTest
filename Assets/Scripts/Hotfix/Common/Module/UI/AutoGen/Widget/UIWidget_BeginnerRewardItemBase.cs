/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-22 19:36:30*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_BeginnerRewardItemBase : UIWidgetBase
{
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_NumText;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("UIImg_Icon").GetComponent<Image>();
		UITxt_NumText = GO.transform.Find("UITxt_NumText").GetComponent<TextMeshProUGUI>();

    }
}
