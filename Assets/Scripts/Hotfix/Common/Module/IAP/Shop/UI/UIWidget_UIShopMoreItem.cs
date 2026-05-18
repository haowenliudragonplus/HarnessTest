using Framework;
using UnityEngine.UI;

public class UIWidget_UIShopMoreItem : UIWidget_UIShopMoreItemBase
{
    private UIShopMoreItemParam _action;
    protected override void OnCreate()
    {
        base.OnCreate();
        _action = ViewData as UIShopMoreItemParam;
        UIBtn_PurchaseButton.onClick.RemoveAllListeners();
        UIBtn_PurchaseButton.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        _action?.OnClickItem?.Invoke();
    }
}
