using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Core;
using Framework;
using TMGame;

public class UISubView_ShopTopGroup : UISubView_ShopTopGroupBase
{
    protected override void OnOpen()
    {
        base.OnOpen();
        List<UIWidget_ResourceBarItem.OpenData> data = new List<UIWidget_ResourceBarItem.OpenData>();
        data.Add(new UIWidget_ResourceBarItem.OpenData { itemId = (int)EItemType.Coin, showAddBtn = false });
        UISubView_ResourceBar.SetView(data);
        FlyTarget.AddTarget(EItemType.Coin, UISubView_ResourceBar.GetResourceIconTransByItemId(EItemType.Coin));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        FlyTarget.RemoveTarget(EItemType.Coin, UISubView_ResourceBar.GetResourceIconTransByItemId(EItemType.Coin));
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Close.onClick.AddListener(() =>
        {
            Game.GetMod<ModUI>().Close(UIViewName.UIView_UIShopMain);
        });
    }
}
