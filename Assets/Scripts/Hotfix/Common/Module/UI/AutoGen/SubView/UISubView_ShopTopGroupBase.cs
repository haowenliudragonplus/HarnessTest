/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-12-23 19:31:2*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UISubView_ShopTopGroupBase : UISubViewBase
{
	protected Button UIBtn_Close;
	protected UISubView_ResourceBar UISubView_ResourceBar;

    protected override void BindComponent()
    {
		UIBtn_Close = GO.transform.Find("UIBtn_Close").GetComponent<Button>();
		UISubView_ResourceBar =new UISubView_ResourceBar();
		UISubView_ResourceBar.InternalInit(this, "UISubView_ResourceBar");
		UISubView_ResourceBar.InternalCreateWithoutInstantiate(GO.transform.Find("UISubView_ResourceBar").gameObject);

    }
}
