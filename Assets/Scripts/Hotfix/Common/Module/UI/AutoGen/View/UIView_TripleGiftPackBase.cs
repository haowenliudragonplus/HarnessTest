/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-2-14 15:52:14*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_TripleGiftPackBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected TextMeshProUGUI UITxt_CountDown;
	protected RectTransform UINode_Bottle1;
	protected RectTransform UINode_Bottle2;
	protected RectTransform UINode_Bottle3;
	protected TextMeshProUGUI UITxt_Discount;
	protected Button UIBtn_Buy;
	protected Button UIBtn_GiftBox1;
	protected Button UIBtn_GiftBox2;
	protected Button UIBtn_GiftBox3;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("Root/TopGroup/UIBtn_Close").GetComponent<Button>();
		UITxt_CountDown = GO.transform.Find("Root/TopGroup/TimeGroup/UITxt_CountDown").GetComponent<TextMeshProUGUI>();
		UINode_Bottle1 = GO.transform.Find("Root/MiddleGroup/RewardGroup/UINode_Bottle1").GetComponent<RectTransform>();
		UINode_Bottle2 = GO.transform.Find("Root/MiddleGroup/RewardGroup/UINode_Bottle2").GetComponent<RectTransform>();
		UINode_Bottle3 = GO.transform.Find("Root/MiddleGroup/RewardGroup/UINode_Bottle3").GetComponent<RectTransform>();
		UITxt_Discount = GO.transform.Find("Root/MiddleGroup/ButtonGroup/UIBtn_Buy/Discount/UITxt_Discount").GetComponent<TextMeshProUGUI>();
		UIBtn_Buy = GO.transform.Find("Root/MiddleGroup/ButtonGroup/UIBtn_Buy").GetComponent<Button>();
		UIBtn_GiftBox1 = GO.transform.Find("Root/MiddleGroup/ButtonGroup/UIBtn_GiftBox1").GetComponent<Button>();
		UIBtn_GiftBox2 = GO.transform.Find("Root/MiddleGroup/ButtonGroup/UIBtn_GiftBox2").GetComponent<Button>();
		UIBtn_GiftBox3 = GO.transform.Find("Root/MiddleGroup/ButtonGroup/UIBtn_GiftBox3").GetComponent<Button>();

    }
}
