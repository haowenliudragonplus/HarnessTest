/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-26 14:51:12*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_UnlockRoomBase : UIViewBase
{
	protected Image UIImg_LockIcon;
	protected TextMeshProUGUI UITxt_LockTextTitle;
	protected Image UIImg_Mask;
	protected RectTransform UINode_LockGroup;
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_TextTitle;
	protected Image UIImg_FinishIcon;
	protected RectTransform UINode_FinishGroup;
	protected Button UIBtn_ButtonUnlock;
	protected Button UIBtn_ButtonContinue;

    protected override void BindComponent()
    {
		UIImg_LockIcon = GO.transform.Find("Root/MiddleGroup/UINode_LockGroup/UIImg_LockIcon").GetComponent<Image>();
		UITxt_LockTextTitle = GO.transform.Find("Root/MiddleGroup/UINode_LockGroup/NameGroup/UITxt_LockTextTitle").GetComponent<TextMeshProUGUI>();
		UIImg_Mask = GO.transform.Find("Root/MiddleGroup/UINode_LockGroup/UIImg_Mask").GetComponent<Image>();
		UINode_LockGroup = GO.transform.Find("Root/MiddleGroup/UINode_LockGroup").GetComponent<RectTransform>();
		UIImg_Icon = GO.transform.Find("Root/MiddleGroup/UINode_FinishGroup/UIImg_Icon").GetComponent<Image>();
		UITxt_TextTitle = GO.transform.Find("Root/MiddleGroup/UINode_FinishGroup/NameGroup/UITxt_TextTitle").GetComponent<TextMeshProUGUI>();
		UIImg_FinishIcon = GO.transform.Find("Root/MiddleGroup/UINode_FinishGroup/UIImg_FinishIcon").GetComponent<Image>();
		UINode_FinishGroup = GO.transform.Find("Root/MiddleGroup/UINode_FinishGroup").GetComponent<RectTransform>();
		UIBtn_ButtonUnlock = GO.transform.Find("Root/MiddleGroup/UIBtn_ButtonUnlock").GetComponent<Button>();
		UIBtn_ButtonContinue = GO.transform.Find("Root/MiddleGroup/UIBtn_ButtonContinue").GetComponent<Button>();

    }
}
