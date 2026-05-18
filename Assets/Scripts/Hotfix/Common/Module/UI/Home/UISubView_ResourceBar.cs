using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_ResourceBar : UISubView_ResourceBarBase
{
    public void SetView(List<UIWidget_ResourceBarItem.OpenData> resourceBarItemDataList)
    {
        UIContainer_ResourceBar.Refresh<UIWidget_ResourceBarItem>(resourceBarItemDataList.ToArray(), false);
    }

    public Transform GetResourceIconTransByItemId(EItemType itemId)
    {
        foreach (var w in UIContainer_ResourceBar.widgets)
        {
            var ww = w as UIWidget_ResourceBarItem;
            if(ww ==null) continue;
            if (ww.ItemType == (int)itemId) return ww.ResourceIconTrans;
        }
        CLog.Error($"飞资源 {itemId.ToString()} 的目标位置没找到，请确保HomeMain的{GetType().Name}中已经注册了这个资源");
        return null;
    }
}