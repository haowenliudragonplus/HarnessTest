/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-7 11:36:5*****/
/*****************************/

using Framework;
using UnityEngine;

public class UIView_InGame_HardTipsBase : UIViewBase
{
	protected RectTransform UINode_Top;
	protected RectTransform UINode_Bottom;
	protected RectTransform UINode_HardGroup;
	protected RectTransform UINode_SuperHardGroup;

    protected override void BindComponent()
    {
		UINode_Top = GO.transform.Find("Root/UINode_HardGroup/UINode_Top").GetComponent<RectTransform>();
		UINode_Bottom = GO.transform.Find("Root/UINode_HardGroup/UINode_Bottom").GetComponent<RectTransform>();
		UINode_HardGroup = GO.transform.Find("Root/UINode_HardGroup").GetComponent<RectTransform>();
		UINode_SuperHardGroup = GO.transform.Find("Root/UINode_SuperHardGroup").GetComponent<RectTransform>();

    }
}
