/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-17 10:59:22*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIView_ContactUsBase : UIViewBase
{
	protected RectTransform UINode_Empty;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;
	protected InputField UIIF_InputField_Email;
	protected RectTransform UINode_EmailGroup;
	protected RectTransform UINode_Content;
	protected RectTransform UINode_MidImage;
	protected InputField UIIF_InputField_input_info;
	protected Text UIOldTxt_TipText;
	protected RectTransform UINode_Input;
	protected Button UIBtn_UpgradeButton;
	protected RectTransform UINode_ButtonEnterGroup;

    protected override void BindComponent()
    {
		UINode_Empty = GO.transform.Find("Root/BG_ReplaceDiff/UINode_Empty").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIIF_InputField_Email = GO.transform.Find("Root/UINode_EmailGroup/UIIF_InputField_Email").GetComponent<InputField>();
		UINode_EmailGroup = GO.transform.Find("Root/UINode_EmailGroup").GetComponent<RectTransform>();
		UINode_Content = GO.transform.Find("Root/UINode_MidImage/CoinsEnough/Scroll View/Viewport/UINode_Content").GetComponent<RectTransform>();
		UINode_MidImage = GO.transform.Find("Root/UINode_MidImage").GetComponent<RectTransform>();
		UIIF_InputField_input_info = GO.transform.Find("Root/UINode_ButtonEnterGroup/UIIF_InputField_input_info").GetComponent<InputField>();
		UIOldTxt_TipText = GO.transform.Find("Root/UINode_ButtonEnterGroup/UINode_Input/Image/UIOldTxt_TipText").GetComponent<Text>();
		UINode_Input = GO.transform.Find("Root/UINode_ButtonEnterGroup/UINode_Input").GetComponent<RectTransform>();
		UIBtn_UpgradeButton = GO.transform.Find("Root/UINode_ButtonEnterGroup/UIBtn_UpgradeButton").GetComponent<Button>();
		UINode_ButtonEnterGroup = GO.transform.Find("Root/UINode_ButtonEnterGroup").GetComponent<RectTransform>();

    }
}
