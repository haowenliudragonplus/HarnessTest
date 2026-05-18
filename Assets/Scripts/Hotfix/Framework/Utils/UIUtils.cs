using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUtils
{
    /// <summary>
    /// 是否点击到了UI
    /// </summary>
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    
    // public static void AdaptiveBanner(RectTransform rootTrans)
    // {
    //     if (!Game.GetMod<AdSys>().ShowingBanner)
    //         return;
    //     float bannerHeight = GameUtils.GetBannerHeight();
    //     float offset = bannerHeight * Game.GetMod<UIMgr>().UIRect.rect.height / Screen.height;
    //     rootTrans.offsetMin = new Vector2(0, offset);
    // }
}