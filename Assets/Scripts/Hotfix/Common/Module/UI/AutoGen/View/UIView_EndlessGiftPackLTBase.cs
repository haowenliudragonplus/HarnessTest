/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-2-25 17:1:31*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_EndlessGiftPackLTBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_TitleGroup;
	protected TextMeshProUGUI UITxt_TimeText;
	protected TextMeshProUGUI UITxt_TimeText1;
	protected RectTransform UINode_TimeGroup;
	protected TextMeshProUGUI UITxt_DescText;
	protected Button UIBtn_CloseBtn;
	protected RectTransform UINode_TopGroup;
	protected RectTransform UINode_ItemPos1;
	protected RectTransform UINode_ItemPos2;
	protected RectTransform UINode_ItemPos3;
	protected RectTransform UINode_ItemPos4;
	protected RectTransform UINode_ItemPos5;
	protected RectTransform UINode_ItemPos6;
	protected RectTransform UINode_Item;
	protected RectTransform UINode_RewardGroup;

    protected override void BindComponent()
    {
		UITxt_TitleText = GO.transform.Find("Root/UINode_TopGroup/UINode_TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_TitleGroup = GO.transform.Find("Root/UINode_TopGroup/UINode_TitleGroup").GetComponent<RectTransform>();
		UITxt_TimeText = GO.transform.Find("Root/UINode_TopGroup/UINode_TimeGroup/UITxt_TimeText").GetComponent<TextMeshProUGUI>();
		UITxt_TimeText1 = GO.transform.Find("Root/UINode_TopGroup/UINode_TimeGroup/UITxt_TimeText1").GetComponent<TextMeshProUGUI>();
		UINode_TimeGroup = GO.transform.Find("Root/UINode_TopGroup/UINode_TimeGroup").GetComponent<RectTransform>();
		UITxt_DescText = GO.transform.Find("Root/UINode_TopGroup/UITxt_DescText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseBtn = GO.transform.Find("Root/UINode_TopGroup/UIBtn_CloseBtn").GetComponent<Button>();
		UINode_TopGroup = GO.transform.Find("Root/UINode_TopGroup").GetComponent<RectTransform>();
		UINode_ItemPos1 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos1").GetComponent<RectTransform>();
		UINode_ItemPos2 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos2").GetComponent<RectTransform>();
		UINode_ItemPos3 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos3").GetComponent<RectTransform>();
		UINode_ItemPos4 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos4").GetComponent<RectTransform>();
		UINode_ItemPos5 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos5").GetComponent<RectTransform>();
		UINode_ItemPos6 = GO.transform.Find("Root/UINode_RewardGroup/UINode_ItemPos6").GetComponent<RectTransform>();
		UINode_Item = GO.transform.Find("Root/UINode_RewardGroup/UINode_Item").GetComponent<RectTransform>();
		UINode_RewardGroup = GO.transform.Find("Root/UINode_RewardGroup").GetComponent<RectTransform>();

    }
}
