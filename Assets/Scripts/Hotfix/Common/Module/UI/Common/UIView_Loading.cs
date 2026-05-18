using System;
using DG.Tweening;
using Framework;
using TMGame;
using UnityEngine.UI;

public class UIView_Loading : UIView_LoadingBase
{
    public class OpenData
    {
        public Action onComplete;
    }

    private OpenData openData;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        enableVirbrate = false;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        openData = ViewData as OpenData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        UISlider_Progress.value = 0;
        UISlider_Progress.DOValue(1, 1.5f).SetEase(Ease.Linear).OnUpdate(() =>
        {
            var value = (int)(UISlider_Progress.value / 1f * 100);
            UITxt_Progress.text = CoreUtils.GetLocalization("UI_loading_loadStr", value);
        }).OnComplete(() =>
        {
            Game.GetMod<ModUI>().Close(UIViewName.UIView_Loading);
            openData.onComplete?.Invoke();
        });
    }
}