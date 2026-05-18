using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刘海屏适配
/// </summary>
public class BangsScreenAdaptation : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
            return;
        float offset = Screen.height - Screen.safeArea.yMax;
// #if UNITY_IOS
//         offset = safeAreaOffset/2;
// #endif
        float scaleRatio = 1.0f * 1366 / Screen.height;
        offset *= scaleRatio;
        rect.offsetMax = new Vector2(0, -offset);
    }
}