/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-6 16:35:55*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_UIStarsBoxBase : UIViewBase
{
	protected Button UIBtn_CloseButton;
	protected TextMeshProUGUI UITxt_PlanText;
	protected Slider UISlider_Start;
	protected Button UIBtn_ContinueButton;

    protected override void BindComponent()
    {
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UITxt_PlanText = GO.transform.Find("Root/InsideGroup/UISlider_Start/UITxt_PlanText").GetComponent<TextMeshProUGUI>();
		UISlider_Start = GO.transform.Find("Root/InsideGroup/UISlider_Start").GetComponent<Slider>();
		UIBtn_ContinueButton = GO.transform.Find("Root/UIBtn_ContinueButton").GetComponent<Button>();

    }
}
