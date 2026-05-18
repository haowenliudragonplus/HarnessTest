/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-27 18:7:46*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_BuyEnergyBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected TextMeshProUGUI UITxt_Count;
	protected TextMeshProUGUI UITxt_Time;
	protected TextMeshProUGUI UITxt_NeedCount;
	protected Button UIBtn_Buy;
	protected Button UIBtn_Rv;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UITxt_Count = GO.transform.Find("Root/MiddleGroup/UITxt_Count").GetComponent<TextMeshProUGUI>();
		UITxt_Time = GO.transform.Find("Root/MiddleGroup/TimeGroup/UITxt_Time").GetComponent<TextMeshProUGUI>();
		UITxt_NeedCount = GO.transform.Find("Root/BottomGroup/UIBtn_Buy/Coin/UITxt_NeedCount").GetComponent<TextMeshProUGUI>();
		UIBtn_Buy = GO.transform.Find("Root/BottomGroup/UIBtn_Buy").GetComponent<Button>();
		UIBtn_Rv = GO.transform.Find("Root/BottomGroup/UIBtn_Rv").GetComponent<Button>();

    }
}
