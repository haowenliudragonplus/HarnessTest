using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 点实体
/// </summary>
public class PointEntity
{
    private static Color IdleColor = ColorUtils.Hex2Color("#b4b9d7");//静止的颜色
    private static Color PassedColor = ColorUtils.Hex2Color("#434a67");//经过后的颜色

    public GameObject Holder { get; private set; } //实体
    public PointData PointData { get; private set; }// 数据

    private SpriteRenderer spriteRenderer;

    public PointEntity(GameObject holder, int pointIndex)
    {
        Holder = holder;
        PointData = new PointData(this, pointIndex);

        Init();
    }

    private void Init()
    {
        Holder.gameObject.SetActive(false);
        spriteRenderer = Holder.transform.Find("Root/Dot").GetComponent<SpriteRenderer>();
        spriteRenderer.color = IdleColor;
    }

    /// <summary>
    /// 播放缩放动画
    /// </summary>
    private Sequence sequence_ZoomAni;
    public void PlayZoomAni()
    {
        if (!PointData.Visible)
            return;

        Holder.gameObject.SetActive(true);
        spriteRenderer.color = PassedColor;
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        var root = Holder.transform.GetChild(0).gameObject;
        sequence_ZoomAni = DOTween.Sequence();
        sequence_ZoomAni.Append(root.transform.DOScale(2f, 0.15f));
        sequence_ZoomAni.Append(root.transform.DOScale(1f, 0.3f));
    }

    /// <summary>
    /// 播放胜利动画
    /// </summary>
    private Sequence sequence_WinAni;
    public void PlayWinAni(Action onComplete)
    {
        if (!PointData.Visible)
            return;

        Holder.gameObject.SetActive(true);
        spriteRenderer.color = PassedColor;
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        var root = Holder.transform.GetChild(0).gameObject;
        sequence_WinAni = DOTween.Sequence();
        sequence_WinAni.Append(root.transform.DOScale(2f, 0.1f));
        sequence_WinAni.Append(root.transform.DOScale(0f, 0.16f));
        sequence_WinAni.SetUpdate(true);
        sequence_WinAni.OnComplete(() => { onComplete?.Invoke(); });
    }

    public void OnDestroy()
    {
        sequence_ZoomAni?.Kill(true);
        sequence_WinAni?.Kill(true);
    }
}

/// <summary>
/// 点数据
/// </summary>
public class PointData
{
    public PointEntity Entity { get; private set; } //实体
    public int PointIndex { get; private set; }//点下标
    public bool Visible { get; private set; }//是否可视

    public PointData(PointEntity entity, int pointIndex)
    {
        Entity = entity;
        PointIndex = pointIndex;

        InitData();
    }

    private void InitData()
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;

        Visible = mode.Data.visiblePointIndexList.Contains(PointIndex);
    }
}
