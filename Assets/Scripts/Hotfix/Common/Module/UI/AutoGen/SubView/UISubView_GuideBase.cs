/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-16 16:59:17*****/
/*****************************/

using Framework;

public class UISubView_GuideBase : UISubViewBase
{
	protected UISubView_GuideMaskLayer UISubView_GuideMaskLayer;
	protected UISubView_GuideNPCText_Top UISubView_GuideNPCText_Top;
	protected UISubView_GuideNPCText_Mid UISubView_GuideNPCText_Mid;
	protected UISubView_GuideArrow UISubView_GuideArrow;
	protected UISubView_GuideFinger UISubView_GuideFinger;

    protected override void BindComponent()
    {
		UISubView_GuideMaskLayer =new UISubView_GuideMaskLayer();
		UISubView_GuideMaskLayer.InternalInit(this, "UISubView_GuideMaskLayer");
		UISubView_GuideMaskLayer.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_GuideMaskLayer").gameObject);
		UISubView_GuideNPCText_Top =new UISubView_GuideNPCText_Top();
		UISubView_GuideNPCText_Top.InternalInit(this, "UISubView_GuideNPCText_Top");
		UISubView_GuideNPCText_Top.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_GuideNPCText_Top").gameObject);
		UISubView_GuideNPCText_Mid =new UISubView_GuideNPCText_Mid();
		UISubView_GuideNPCText_Mid.InternalInit(this, "UISubView_GuideNPCText_Mid");
		UISubView_GuideNPCText_Mid.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_GuideNPCText_Mid").gameObject);
		UISubView_GuideArrow =new UISubView_GuideArrow();
		UISubView_GuideArrow.InternalInit(this, "UISubView_GuideArrow");
		UISubView_GuideArrow.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_GuideArrow").gameObject);
		UISubView_GuideFinger =new UISubView_GuideFinger();
		UISubView_GuideFinger.InternalInit(this, "UISubView_GuideFinger");
		UISubView_GuideFinger.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_GuideFinger").gameObject);

    }
}
