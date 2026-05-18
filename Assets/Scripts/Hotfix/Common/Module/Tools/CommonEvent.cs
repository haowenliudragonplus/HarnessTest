using System;
using System.Collections.Generic;
using DragonPlus.Config.Shop;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine;

#region Common

public struct EvtOnApplicationPause : IEvent
{
    public bool pause;
    public EvtOnApplicationPause(bool inPause)
    {
        pause = inPause;
    }
}

#endregion Common

#region IAP

public struct EvtIAPSuccess : IEvent
{
    public Table_Shop_Shop shopCfg;
    public object userData;

    public EvtIAPSuccess(Table_Shop_Shop inTableGameShopConfig, object inObject)
    {
        shopCfg = inTableGameShopConfig;
        userData = inObject;
    }
}

#endregion IAP

#region 道具

/// <summary>
/// 道具变化
/// </summary>
public struct EvtItemChange : IEvent
{
    public EItemType itemType;
    public long delta;
    public bool showAni;
    public bool isAutoAdd;

    public EvtItemChange(EItemType itemType, long delta, bool showAni, bool isAutoAdd = false)
    {
        this.itemType = itemType;
        this.delta = delta;
        this.showAni = showAni;
        this.isAutoAdd = isAutoAdd;
    }
}

/// <summary>
/// 限时道具时间结束
/// </summary>
public struct EvtTimeLimitItemTimeEnd : IEvent
{
    public EItemType itemType;

    public EvtTimeLimitItemTimeEnd(EItemType itemType)
    {
        this.itemType = itemType;
    }
}

#endregion 道具

#region 飞物体效果

/// <summary>
/// 飞物体
/// </summary>
public struct EvtPlayFlyObj : IEvent
{
    public FlyObjData flyObjData;
    public bool isLastInGroup;

    public EvtPlayFlyObj(FlyObjData flyObjData, bool isLastInGroup)
    {
        this.flyObjData = flyObjData;
        this.isLastInGroup = isLastInGroup;
    }
}

/// <summary>
/// 添加飞物体数据
/// </summary>
public struct EvtAddFlyObjData : IEvent
{
    public List<int> itemIdList;
    public List<Vector3> fromScreenPosList;
    public List<Vector3> toScreenPosList;
    public List<int> showCountList; //动画显示的物体数量
    public List<int> realCountList; //真实获取的物体数量
    public List<EFlyObjTag> tagList;

    public EvtAddFlyObjData(List<int> itemIdList, List<Vector3> fromScreenPosList, List<Vector3> toScreenPosList,
        List<int> showCountList, List<int> realCountList, List<EFlyObjTag> tagList = null)
    {
        this.itemIdList = itemIdList;
        this.fromScreenPosList = fromScreenPosList;
        this.toScreenPosList = toScreenPosList;
        this.showCountList = showCountList;
        this.realCountList = realCountList;
        this.tagList = tagList;
    }
}

/// <summary>
/// 本组飞物体完成
/// </summary>
public struct EvtFlyObjGroupComplete : IEvent
{
    public int itemId;
    public EFlyObjTag flyObjTag;

    public EvtFlyObjGroupComplete(int itemId, EFlyObjTag flyObjTag)
    {
        this.itemId = itemId;
        this.flyObjTag = flyObjTag;
    }
}

/// <summary>
/// 单个飞物体完成
/// </summary>
public struct EvtFlyObjSingleComplete : IEvent
{
    public int itemId;
    public float addRealCount;
    public EFlyObjTag flyObjTag;

    public EvtFlyObjSingleComplete(int itemId, float addRealCount, EFlyObjTag flyObjTag)
    {
        this.itemId = itemId;
        this.addRealCount = addRealCount;
        this.flyObjTag = flyObjTag;
    }
}

#endregion 飞物体效果

#region 下载资源

/// <summary>
/// 开始下载资源
/// </summary>
public struct EvtStartDownload : IEvent
{
    public string fileName;
    public long sizeBytes;

    public EvtStartDownload(string fileName, long sizeBytes)
    {
        this.fileName = fileName;
        this.sizeBytes = sizeBytes;
    }
}

/// <summary>
/// 下载资源结束
/// </summary>
public struct EvtDownloadProgress : IEvent
{
    public string tag;
    public int totalDownloadCount;
    public int currentDownloadCount;
    public long totalDownloadBytes;
    public long currentDownloadBytes;

    public EvtDownloadProgress(string tag, int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        this.tag = tag;
        this.totalDownloadCount = totalDownloadCount;
        this.currentDownloadCount = currentDownloadCount;
        this.totalDownloadBytes = totalDownloadBytes;
        this.currentDownloadBytes = currentDownloadBytes;
    }
}

/// <summary>
/// 下载资源结束
/// </summary>
public struct EvtDownloadOver : IEvent
{
    public bool isByDownload;
    public bool isSucceed;
    public string tag;

    public EvtDownloadOver(bool isByDownload, bool isSucceed, string tag)
    {
        this.isByDownload = isByDownload;
        this.isSucceed = isSucceed;
        this.tag = tag;
    }
}

#endregion 下载资源

#region Tip

/// <summary>
/// 显示tip
/// </summary>
public struct EvtShowTip : IEvent
{
    public OpenTipData openData;

    public EvtShowTip(OpenTipData openData)
    {
        this.openData = openData;
    }
}

/// <summary>
/// 关闭tip
/// </summary>
public struct EvtCloseTip : IEvent
{
    public bool isSequenceShow;
    public UIWidgetBase tipWidget;

    public EvtCloseTip(bool isSequenceShow, UIWidgetBase tipWidget)
    {
        this.isSequenceShow = isSequenceShow;
        this.tipWidget = tipWidget;
    }
}

#endregion Tip

#region 多语言

public struct EvtLanguageChange : IEvent
{
}

#endregion 多语言

#region 账号

public struct EvtBindFacebook : IEvent
{
    public bool isSuccess;

    public EvtBindFacebook(bool isSuccess)
    {
        this.isSuccess = isSuccess;
    }
}

public struct EvtBindApple : IEvent
{
    public bool isSuccess;

    public EvtBindApple(bool isSuccess)
    {
        this.isSuccess = isSuccess;
    }
}

#endregion 账号

#region FAQ（联系我们）

public struct EvtFaqSelectQuestion : IEvent
{
    public int id;

    public EvtFaqSelectQuestion(int inId)
    {
        id = inId;
    }
}

public struct EvtFaqQuestionServerBack : IEvent
{
    public int id;
    public EvtFaqQuestionServerBack(int inId)
    {
        id = inId;
    }
}

#endregion FAQ（联系我们）

#region Ad

public struct EvtPlayBanner : IEvent
{
    public eAdBanner bannerType;
    public EvtPlayBanner(eAdBanner bannerType)
    {
        this.bannerType = bannerType;
    }
}

#endregion Ad