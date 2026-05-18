/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-3-2 15:42:4*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_InGame_MainBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_Level;
	protected RectTransform UINode_NormalLevel;
	protected RectTransform UINode_HardLevel;
	protected RectTransform UINode_SuperHardLevel;
	protected RectTransform UINode_Level;
	protected Button UIBtn_Setting;
	protected Button UIBtn_Hint;
	protected RectTransform UINode_TemporarySlot;
	protected RectTransform UINode_Heart01;
	protected RectTransform UINode_Heart02;
	protected RectTransform UINode_Heart03;
	protected RectTransform UINode_HeartGroup;
	protected TextMeshProUGUI UITxt_Steps;
	protected RectTransform UINode_Top;
	protected Image UIImg_Hide;
	protected Image UIImg_Show;
	protected Button UIBtn_Auxiliary;
	protected Button UIBtn_NextLevel;
	protected Button UIBtn_LastLevel;
	protected RectTransform UINode_ItemContent;
	protected RectTransform UINode_BottomTop;
	protected RectTransform UINode_Bottom;
	protected RectTransform UINode_ScreenLight;
	protected RectTransform UINode_BoundMax;
	protected RectTransform UINode_BoundMin;
	protected RectTransform UINode_Points;

    protected override void BindComponent()
    {
		UITxt_Level = GO.transform.Find("Root/UINode_Top/UINode_Level/UINode_NormalLevel/UITxt_Level").GetComponent<TextMeshProUGUI>();
		UINode_NormalLevel = GO.transform.Find("Root/UINode_Top/UINode_Level/UINode_NormalLevel").GetComponent<RectTransform>();
		UINode_HardLevel = GO.transform.Find("Root/UINode_Top/UINode_Level/UINode_HardLevel").GetComponent<RectTransform>();
		UINode_SuperHardLevel = GO.transform.Find("Root/UINode_Top/UINode_Level/UINode_SuperHardLevel").GetComponent<RectTransform>();
		UINode_Level = GO.transform.Find("Root/UINode_Top/UINode_Level").GetComponent<RectTransform>();
		UIBtn_Setting = GO.transform.Find("Root/UINode_Top/UIBtn_Setting").GetComponent<Button>();
		UIBtn_Hint = GO.transform.Find("Root/UINode_Top/UIBtn_Hint").GetComponent<Button>();
		UINode_TemporarySlot = GO.transform.Find("Root/UINode_Top/UINode_TemporarySlot").GetComponent<RectTransform>();
		UINode_Heart01 = GO.transform.Find("Root/UINode_Top/UINode_HeartGroup/UINode_Heart01").GetComponent<RectTransform>();
		UINode_Heart02 = GO.transform.Find("Root/UINode_Top/UINode_HeartGroup/UINode_Heart02").GetComponent<RectTransform>();
		UINode_Heart03 = GO.transform.Find("Root/UINode_Top/UINode_HeartGroup/UINode_Heart03").GetComponent<RectTransform>();
		UINode_HeartGroup = GO.transform.Find("Root/UINode_Top/UINode_HeartGroup").GetComponent<RectTransform>();
		UITxt_Steps = GO.transform.Find("Root/UINode_Top/UITxt_Steps").GetComponent<TextMeshProUGUI>();
		UINode_Top = GO.transform.Find("Root/UINode_Top").GetComponent<RectTransform>();
		UIImg_Hide = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent/UIBtn_Auxiliary/UIImg_Hide").GetComponent<Image>();
		UIImg_Show = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent/UIBtn_Auxiliary/UIImg_Show").GetComponent<Image>();
		UIBtn_Auxiliary = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent/UIBtn_Auxiliary").GetComponent<Button>();
		UIBtn_NextLevel = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent/UIBtn_NextLevel").GetComponent<Button>();
		UIBtn_LastLevel = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent/UIBtn_LastLevel").GetComponent<Button>();
		UINode_ItemContent = GO.transform.Find("Root/UINode_Bottom/FooterPanel/UINode_ItemContent").GetComponent<RectTransform>();
		UINode_BottomTop = GO.transform.Find("Root/UINode_Bottom/UINode_BottomTop").GetComponent<RectTransform>();
		UINode_Bottom = GO.transform.Find("Root/UINode_Bottom").GetComponent<RectTransform>();
		UINode_ScreenLight = GO.transform.Find("Root/UINode_ScreenLight").GetComponent<RectTransform>();
		UINode_BoundMax = GO.transform.Find("Root/UINode_Points/UINode_BoundMax").GetComponent<RectTransform>();
		UINode_BoundMin = GO.transform.Find("Root/UINode_Points/UINode_BoundMin").GetComponent<RectTransform>();
		UINode_Points = GO.transform.Find("Root/UINode_Points").GetComponent<RectTransform>();

    }
}
