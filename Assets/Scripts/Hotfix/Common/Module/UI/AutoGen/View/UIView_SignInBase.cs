/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-2-8 14:29:11*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_SignInBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected Button UIBtn_DayItem1;
	protected Button UIBtn_DayItem2;
	protected Button UIBtn_DayItem3;
	protected Button UIBtn_DayItem4;
	protected Button UIBtn_DayItem5;
	protected Button UIBtn_DayItem6;
	protected Button UIBtn_DayItem7;
	protected Button UIBtn_Claim;
	protected TextMeshProUGUI UITxt_CountDown;
	protected RectTransform UINode_CountDown;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UIBtn_DayItem1 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem1").GetComponent<Button>();
		UIBtn_DayItem2 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem2").GetComponent<Button>();
		UIBtn_DayItem3 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem3").GetComponent<Button>();
		UIBtn_DayItem4 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem4").GetComponent<Button>();
		UIBtn_DayItem5 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem5").GetComponent<Button>();
		UIBtn_DayItem6 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem6").GetComponent<Button>();
		UIBtn_DayItem7 = GO.transform.Find("Root/InsideGroup/UIBtn_DayItem7").GetComponent<Button>();
		UIBtn_Claim = GO.transform.Find("Root/UIBtn_Claim").GetComponent<Button>();
		UITxt_CountDown = GO.transform.Find("Root/UINode_CountDown/UITxt_CountDown").GetComponent<TextMeshProUGUI>();
		UINode_CountDown = GO.transform.Find("Root/UINode_CountDown").GetComponent<RectTransform>();

    }
}
