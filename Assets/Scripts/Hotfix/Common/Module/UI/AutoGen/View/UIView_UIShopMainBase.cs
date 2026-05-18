/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-12-23 19:31:2*****/
/*****************************/

using Framework;

public class UIView_UIShopMainBase : UIViewBase
{
	protected UISubView_CoinPage UISubView_CoinPage;
	protected UISubView_ShopTopGroup UISubView_ShopTopGroup;

    protected override void BindComponent()
    {
		UISubView_CoinPage =new UISubView_CoinPage();
		UISubView_CoinPage.InternalInit(this, "UISubView_CoinPage");
		UISubView_CoinPage.InternalCreateWithoutInstantiate(GO.transform.Find("Root/UISubView_CoinPage").gameObject);
		UISubView_ShopTopGroup =new UISubView_ShopTopGroup();
		UISubView_ShopTopGroup.InternalInit(this, "UISubView_ShopTopGroup");
		UISubView_ShopTopGroup.InternalCreateWithoutInstantiate(GO.transform.Find("Root/UISubView_ShopTopGroup").gameObject);

    }
}
