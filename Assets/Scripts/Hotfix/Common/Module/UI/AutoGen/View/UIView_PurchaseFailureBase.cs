/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-2-25 9:51:38*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_PurchaseFailureBase : UIViewBase
{
	protected Button UIBtn_CloseButton;
	protected TextMeshProUGUI UITxt_HelpText;
	protected Button UIBtn_PlayButton;

    protected override void BindComponent()
    {
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UITxt_HelpText = GO.transform.Find("Root/InsideGroup/UITxt_HelpText").GetComponent<TextMeshProUGUI>();
		UIBtn_PlayButton = GO.transform.Find("Root/UIBtn_PlayButton").GetComponent<Button>();

    }
}
