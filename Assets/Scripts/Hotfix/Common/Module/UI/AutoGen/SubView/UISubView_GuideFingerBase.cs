/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 15:32:53*****/
/*****************************/

using Framework;
using UnityEngine;

public class UISubView_GuideFingerBase : UISubViewBase
{
	protected RectTransform UINode_DragHandNode;
	protected RectTransform UINode_DragArrowNode;
	protected RectTransform UINode_DragHandNode1;
	protected RectTransform UINode_DragDoubleHandNode;

    protected override void BindComponent()
    {
		UINode_DragHandNode = GO.transform.Find("UINode_DragHandNode").GetComponent<RectTransform>();
		UINode_DragArrowNode = GO.transform.Find("UINode_DragArrowNode").GetComponent<RectTransform>();
		UINode_DragHandNode1 = GO.transform.Find("UINode_DragHandNode1").GetComponent<RectTransform>();
		UINode_DragDoubleHandNode = GO.transform.Find("UINode_DragDoubleHandNode").GetComponent<RectTransform>();

    }
}
