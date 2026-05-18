/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-3 19:30:33*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_ASMR_UIMainBase : UISubViewBase
{
	protected Image UIImg_BG;
	protected TextMeshProUGUI UITxt_ComingSoon;
	protected RectTransform UINode_ComingSoon;
	protected TextMeshProUGUI UITxt_Title;
	protected Button UIBtn_TitleButton;
	protected RectTransform UINode_Content;
	protected TextMeshProUGUI UITxt_PlayText;
	protected Button UIBtn_Play;
	protected TextMeshProUGUI UITxt_Desc;
	protected RectTransform UINode_MoreGroup;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("Root/UINode_ComingSoon/UIImg_BG").GetComponent<Image>();
		UITxt_ComingSoon = GO.transform.Find("Root/UINode_ComingSoon/UITxt_ComingSoon").GetComponent<TextMeshProUGUI>();
		UINode_ComingSoon = GO.transform.Find("Root/UINode_ComingSoon").GetComponent<RectTransform>();
		UITxt_Title = GO.transform.Find("Root/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UIBtn_TitleButton = GO.transform.Find("Root/UIBtn_TitleButton").GetComponent<Button>();
		UINode_Content = GO.transform.Find("Root/Scroll View/Viewport/UINode_Content").GetComponent<RectTransform>();
		UITxt_PlayText = GO.transform.Find("Root/UINode_MoreGroup/UIBtn_Play/UITxt_PlayText").GetComponent<TextMeshProUGUI>();
		UIBtn_Play = GO.transform.Find("Root/UINode_MoreGroup/UIBtn_Play").GetComponent<Button>();
		UITxt_Desc = GO.transform.Find("Root/UINode_MoreGroup/UITxt_Desc").GetComponent<TextMeshProUGUI>();
		UINode_MoreGroup = GO.transform.Find("Root/UINode_MoreGroup").GetComponent<RectTransform>();

    }
}
