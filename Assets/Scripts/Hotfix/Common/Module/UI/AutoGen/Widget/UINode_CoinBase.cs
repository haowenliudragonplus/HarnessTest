/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-4 10:29:20*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UINode_CoinBase : UIWidgetBase
{
	protected Image UIImg_CoinIcon;
	protected TextMeshProUGUI UITxt_NumText;

    protected override void BindComponent()
    {
		UIImg_CoinIcon = GO.transform.Find("UIImg_CoinIcon").GetComponent<Image>();
		UITxt_NumText = GO.transform.Find("UITxt_NumText").GetComponent<TextMeshProUGUI>();

    }
}
