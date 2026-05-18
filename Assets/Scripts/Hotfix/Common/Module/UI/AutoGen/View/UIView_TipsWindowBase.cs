/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-11 16:39:19*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_TipsWindowBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_PlayButton;
	protected Button UIBtn_PlaySkip;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("Root/WindowsGroup/UIBtn_Close").GetComponent<Button>();
		UITxt_TitleText = GO.transform.Find("Root/WindowsGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_PlayButton = GO.transform.Find("Root/WindowsGroup/UIBtn_PlayButton").GetComponent<Button>();
		UIBtn_PlaySkip = GO.transform.Find("Root/WindowsGroup/UIBtn_PlaySkip").GetComponent<Button>();

    }
}
