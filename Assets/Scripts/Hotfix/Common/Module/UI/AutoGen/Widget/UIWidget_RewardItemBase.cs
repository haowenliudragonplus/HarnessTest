/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-17 14:18:32*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_RewardItemBase : UIWidgetBase
{
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_Text;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("UIImg_Icon").GetComponent<Image>();
		UITxt_Text = GO.transform.Find("UITxt_Text").GetComponent<TextMeshProUGUI>();

    }
}
