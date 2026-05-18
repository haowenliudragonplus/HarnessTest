using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public static class SpriteRendererUtils
{
    public static void SetSoringLayer(GameObject go, string layerName, bool includeChild = true)
    {
        if (string.IsNullOrEmpty(layerName))
        {
            CLog.Error("layerName不能为null");
            return;
        }
        var com_SpriteRenderer = go.GetComponent<SpriteRenderer>(false);
        if (com_SpriteRenderer != null)
        {
            com_SpriteRenderer.sortingLayerName = layerName;
        }
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                SetSoringLayer(childGo, layerName, true);
            }
        }
    }

    public static void SetOrderInLayer(GameObject go, int orderInLayer, bool isOverlay = true, bool includeChild = true)
    {
        var com_SpriteRenderer = go.GetComponent<SpriteRenderer>(false);
        if (com_SpriteRenderer != null)
        {
            if (isOverlay)
            {
                com_SpriteRenderer.sortingOrder = orderInLayer;
            }
            else
            {
                com_SpriteRenderer.sortingOrder += orderInLayer;
            }
        }
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                SetOrderInLayer(childGo, orderInLayer, isOverlay, true);
            }
        }
    }
}