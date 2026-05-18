/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-3 11:8:24*****/
/*****************************/

using Framework;
using UnityEngine;

public class UIView_TipBase : UIViewBase
{
	protected RectTransform UINode_Top;
	protected RectTransform UINode_Bottom;
	protected RectTransform UINode_Mid;

    protected override void BindComponent()
    {
		UINode_Top = GO.transform.Find("Root/UINode_Top").GetComponent<RectTransform>();
		UINode_Bottom = GO.transform.Find("Root/UINode_Bottom").GetComponent<RectTransform>();
		UINode_Mid = GO.transform.Find("Root/UINode_Mid").GetComponent<RectTransform>();

    }
}
