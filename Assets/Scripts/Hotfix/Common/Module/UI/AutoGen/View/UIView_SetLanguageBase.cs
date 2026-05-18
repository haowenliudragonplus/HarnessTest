/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:49:57*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIView_SetLanguageBase : UIViewBase
{
	protected Image UIImg_BG;
	protected Image UIImg_TitleBG;
	protected RectTransform UINode_BGGroup;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_Close;
	protected UIContainer UIContainer_SetLanguageBtn;
	protected ScrollRect UISR_SetLanguageBtn;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("Root/UINode_BGGroup/UIImg_BG").GetComponent<Image>();
		UIImg_TitleBG = GO.transform.Find("Root/UINode_BGGroup/UIImg_TitleBG").GetComponent<Image>();
		UINode_BGGroup = GO.transform.Find("Root/UINode_BGGroup").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UIContainer_SetLanguageBtn =new UIContainer();
		UIContainer_SetLanguageBtn.InternalInit(GO.transform.Find("Root/InsideGroup/UISR_SetLanguageBtn/Viewport/UIContainer_SetLanguageBtn").gameObject , this);
		UISR_SetLanguageBtn = GO.transform.Find("Root/InsideGroup/UISR_SetLanguageBtn").GetComponent<ScrollRect>();

    }
}
