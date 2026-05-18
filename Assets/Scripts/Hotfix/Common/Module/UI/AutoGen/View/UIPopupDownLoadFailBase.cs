/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-23 18:5:5*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIPopupDownLoadFailBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_Close;
	protected RectTransform UINode_TopGroup;
	protected TextMeshProUGUI UITxt_TextHint;
	protected Button UIBtn_ButtonOk;

    protected override void BindComponent()
    {
		UITxt_TitleText = GO.transform.Find("Root/UINode_TopGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_Close = GO.transform.Find("Root/UINode_TopGroup/UIBtn_Close").GetComponent<Button>();
		UINode_TopGroup = GO.transform.Find("Root/UINode_TopGroup").GetComponent<RectTransform>();
		UITxt_TextHint = GO.transform.Find("Root/MiddleGroup/UITxt_TextHint").GetComponent<TextMeshProUGUI>();
		UIBtn_ButtonOk = GO.transform.Find("Root/ButtonGroup/UIBtn_ButtonOk").GetComponent<Button>();

    }
}
