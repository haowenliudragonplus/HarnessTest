/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-13 12:5:16*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_NoticeBase : UIViewBase
{
	protected Image UIImg_BG;
	protected Image UIImg_TitleBG;
	protected Button UIBtn_Close;
	protected TextMeshProUGUI UITxt_Title;
	protected Text UIOldTxt_Title;
	protected TextMeshProUGUI UITxt_Content;
	protected Text UIOldTxt_Content;
	protected ScrollRect UISR_Content;
	protected TextMeshProUGUI UITxt_LeftBtn;
	protected Button UIBtn_Left;
	protected TextMeshProUGUI UITxt_RightBtn;
	protected Button UIBtn_Right;
	protected RectTransform UINode_Btn2;
	protected TextMeshProUGUI UITxt_MidBtn;
	protected Button UIBtn_Mid;
	protected RectTransform UINode_Btn1;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("Root/BgPopupBoand/UIImg_BG").GetComponent<Image>();
		UIImg_TitleBG = GO.transform.Find("Root/BgPopupBoand/UIImg_TitleBG").GetComponent<Image>();
		UIBtn_Close = GO.transform.Find("Root/BgPopupBoand/UIBtn_Close").GetComponent<Button>();
		UITxt_Title = GO.transform.Find("Root/BgPopupBoand/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UIOldTxt_Title = GO.transform.Find("Root/BgPopupBoand/UIOldTxt_Title").GetComponent<Text>();
		UITxt_Content = GO.transform.Find("Root/MiddleGroup/UISR_Content/Viewport/UITxt_Content").GetComponent<TextMeshProUGUI>();
		UIOldTxt_Content = GO.transform.Find("Root/MiddleGroup/UISR_Content/Viewport/UIOldTxt_Content").GetComponent<Text>();
		UISR_Content = GO.transform.Find("Root/MiddleGroup/UISR_Content").GetComponent<ScrollRect>();
		UITxt_LeftBtn = GO.transform.Find("Root/UINode_Btn2/UIBtn_Left/UITxt_LeftBtn").GetComponent<TextMeshProUGUI>();
		UIBtn_Left = GO.transform.Find("Root/UINode_Btn2/UIBtn_Left").GetComponent<Button>();
		UITxt_RightBtn = GO.transform.Find("Root/UINode_Btn2/UIBtn_Right/UITxt_RightBtn").GetComponent<TextMeshProUGUI>();
		UIBtn_Right = GO.transform.Find("Root/UINode_Btn2/UIBtn_Right").GetComponent<Button>();
		UINode_Btn2 = GO.transform.Find("Root/UINode_Btn2").GetComponent<RectTransform>();
		UITxt_MidBtn = GO.transform.Find("Root/UINode_Btn1/UIBtn_Mid/UITxt_MidBtn").GetComponent<TextMeshProUGUI>();
		UIBtn_Mid = GO.transform.Find("Root/UINode_Btn1/UIBtn_Mid").GetComponent<Button>();
		UINode_Btn1 = GO.transform.Find("Root/UINode_Btn1").GetComponent<RectTransform>();

    }
}
