/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-4 10:33:23*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_ASMR_MainSelectBase : UIWidgetBase
{
	protected Image UIImg_IconImage;
	protected TextMeshProUGUI UITxt_Title;
	protected HorizontalLayoutGroup UILayoutH_CoinGroup;
	protected RectTransform UINode_BlackImage;
	protected TextMeshProUGUI UITxt_DecText;
	protected RectTransform UINode_DecGroup;
	protected TextMeshProUGUI UITxt_PlayText;
	protected Button UIBtn_PlayButton;
	protected TextMeshProUGUI UITxt_HelpText;
	protected Button UIBtn_HelpButton;

    protected override void BindComponent()
    {
		UIImg_IconImage = GO.transform.Find("UIImg_IconImage").GetComponent<Image>();
		UITxt_Title = GO.transform.Find("Image/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UILayoutH_CoinGroup = GO.transform.Find("UILayoutH_CoinGroup").GetComponent<HorizontalLayoutGroup>();
		UINode_BlackImage = GO.transform.Find("UINode_BlackImage").GetComponent<RectTransform>();
		UITxt_DecText = GO.transform.Find("UINode_DecGroup/UITxt_DecText").GetComponent<TextMeshProUGUI>();
		UINode_DecGroup = GO.transform.Find("UINode_DecGroup").GetComponent<RectTransform>();
		UITxt_PlayText = GO.transform.Find("UIBtn_PlayButton/UITxt_PlayText").GetComponent<TextMeshProUGUI>();
		UIBtn_PlayButton = GO.transform.Find("UIBtn_PlayButton").GetComponent<Button>();
		UITxt_HelpText = GO.transform.Find("UIBtn_HelpButton/UITxt_HelpText").GetComponent<TextMeshProUGUI>();
		UIBtn_HelpButton = GO.transform.Find("UIBtn_HelpButton").GetComponent<Button>();

    }
}
