/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-2-9 21:39:9*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIView_RemoveAdBase : UIViewBase
{
	protected RectTransform UINode_Empty;
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_TitleGroup;
	protected Button UIBtn_CloseButton;
	protected TextMeshProUGUI UITxt_TipsText;
	protected TextMeshProUGUI UITxt_TipsText2;
	protected Text UIOldTxt_Price;
	protected Button UIBtn_BuyButton;

    protected override void BindComponent()
    {
		UINode_Empty = GO.transform.Find("Root/BG_ReplaceDiff/UINode_Empty").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/UINode_TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_TitleGroup = GO.transform.Find("Root/UINode_TitleGroup").GetComponent<RectTransform>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UITxt_TipsText = GO.transform.Find("Root/MiddleGruop/UITxt_TipsText").GetComponent<TextMeshProUGUI>();
		UITxt_TipsText2 = GO.transform.Find("Root/MiddleGruop/UITxt_TipsText2").GetComponent<TextMeshProUGUI>();
		UIOldTxt_Price = GO.transform.Find("Root/UIBtn_BuyButton/UIOldTxt_Price").GetComponent<Text>();
		UIBtn_BuyButton = GO.transform.Find("Root/UIBtn_BuyButton").GetComponent<Button>();

    }
}
