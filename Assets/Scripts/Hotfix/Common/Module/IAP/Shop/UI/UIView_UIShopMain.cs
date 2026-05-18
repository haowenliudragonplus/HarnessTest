using System;
using System.Collections.Generic;
using DG.Tweening;
using DragonPlus.Core;
using Framework;
using GameStorage;
using TMGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIView_UIShopMain : UIView_UIShopMainBase
{
    public class OpenData
    {

    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
    }
}