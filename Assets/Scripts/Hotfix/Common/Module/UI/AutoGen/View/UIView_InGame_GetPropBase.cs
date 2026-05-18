/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-12-18 16:9:31*****/
/*****************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView_InGame_GetPropBase : UIViewBase
{
	protected UISubView_ResourceBar UISubView_ResourceBar;
	protected RectTransform UINode_Empty;
	protected Image UIImg_propIcon;
	protected TextMeshProUGUI UITxt_Desc;
	protected TextMeshProUGUI UITxt_Count;
	protected TextMeshProUGUI UITxt_Price_Buy;
	protected Button UIBtn_Buy;
	protected Button UIBtn_ViewAD;
	protected TextMeshProUGUI UITxt_Price_NoAds;
	protected Button UIBtn_NoAdsBuy;
	protected RectTransform UINode_ADSExist;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;

    protected override void BindComponent()
    {
		UISubView_ResourceBar =new UISubView_ResourceBar();
		UISubView_ResourceBar.InternalInit(this, "UISubView_ResourceBar");
		UISubView_ResourceBar.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_ResourceBar").gameObject);
		UINode_Empty = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UINode_Empty").GetComponent<RectTransform>();
		UIImg_propIcon = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/InsideGroup/BG/Img_Frame/UIImg_propIcon").GetComponent<Image>();
		UITxt_Desc = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/InsideGroup/UITxt_Desc").GetComponent<TextMeshProUGUI>();
		UITxt_Count = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/InsideGroup/UITxt_Count").GetComponent<TextMeshProUGUI>();
		UITxt_Price_Buy = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist/UIBtn_Buy/UITxt_Price_Buy").GetComponent<TextMeshProUGUI>();
		UIBtn_Buy = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist/UIBtn_Buy").GetComponent<Button>();
		UIBtn_ViewAD = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist/UIBtn_ViewAD").GetComponent<Button>();
		UITxt_Price_NoAds = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist/UIBtn_NoAdsBuy/UITxt_Price_NoAds").GetComponent<TextMeshProUGUI>();
		UIBtn_NoAdsBuy = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist/UIBtn_NoAdsBuy").GetComponent<Button>();
		UINode_ADSExist = GO.transform.Find("Root/BG_Mask/Group/UINode_ADSExist").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();

    }
}
