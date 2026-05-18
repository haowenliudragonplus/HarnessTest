using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleSystemUtils
{
    public static float GetDuration(GameObject go)
    {
        var psList = go.GetComponentsInChildren<ParticleSystem>(true);
        float ret = 0;
        foreach (var ps in psList)
        {
            if (ps.main.loop)
                return 0;
            float v1 = Mathf.Max(ps.main.duration, ps.main.startLifetime.constant);
            float v = (v1 + ps.main.startDelay.constant) / ps.main.simulationSpeed;
            if (v > ret)
                ret = v;
        }
        return ret;
    }

    public static void SetOrderInLayer(GameObject go, int orderInLayer, bool isOverlay = true, bool includeChild = true)
    {
        var psRenderer = go.GetComponent<ParticleSystemRenderer>(true);
        if (psRenderer != null)
        {
            if (isOverlay)
            {
                psRenderer.sortingOrder = orderInLayer;
            }
            else
            {
                psRenderer.sortingOrder += orderInLayer;
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

    public static void SetSortingLayer(GameObject go, string layerName, bool includeChild)
    {
        var psRenderer = go.GetComponent<ParticleSystemRenderer>(true);
        if (psRenderer != null)
        {
            psRenderer.sortingLayerName = layerName;
        }
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                SetSortingLayer(childGo, layerName, true);
            }
        }
    }
}