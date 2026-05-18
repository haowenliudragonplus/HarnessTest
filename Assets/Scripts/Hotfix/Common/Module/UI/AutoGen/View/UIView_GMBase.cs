/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-8-16 17:48:55*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_GMBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_Title;
	protected RectTransform UINode_Title;
	protected InputField UIIF_ParamInputField1;
	protected InputField UIIF_ParamInputField2;
	protected InputField UIIF_ParamInputField3;
	protected VerticalLayoutGroup UILayoutV_InputField;
	protected ScrollRect UISR_Option;
	protected RectTransform UINode_Option;
	protected ScrollRect UISR_Command;
	protected RectTransform UINode_Command;
	protected Button UIBtn_Close;

    protected override void BindComponent()
    {
		UITxt_Title = GO.transform.Find("Root/UINode_Title/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UINode_Title = GO.transform.Find("Root/UINode_Title").GetComponent<RectTransform>();
		UIIF_ParamInputField1 = GO.transform.Find("Root/UILayoutV_InputField/UIIF_ParamInputField1").GetComponent<InputField>();
		UIIF_ParamInputField2 = GO.transform.Find("Root/UILayoutV_InputField/UIIF_ParamInputField2").GetComponent<InputField>();
		UIIF_ParamInputField3 = GO.transform.Find("Root/UILayoutV_InputField/UIIF_ParamInputField3").GetComponent<InputField>();
		UILayoutV_InputField = GO.transform.Find("Root/UILayoutV_InputField").GetComponent<VerticalLayoutGroup>();
		UISR_Option = GO.transform.Find("Root/UINode_Option/UISR_Option").GetComponent<ScrollRect>();
		UINode_Option = GO.transform.Find("Root/UINode_Option").GetComponent<RectTransform>();
		UISR_Command = GO.transform.Find("Root/UINode_Command/UISR_Command").GetComponent<ScrollRect>();
		UINode_Command = GO.transform.Find("Root/UINode_Command").GetComponent<RectTransform>();
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();

    }
}
