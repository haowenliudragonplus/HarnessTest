// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/10/16:27
// Ver : 1.0.0
// Description : UIGetReward.cs
// ChangeLog :
// **********************************************

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using Newtonsoft.Json;
using TMGame;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIBoxRewardParam
{
    public List<Table_Common_Item> itemDatas; //奖励列表
    public bool fly = true;
    public bool autoAddReward = true;
    public bool autoChangeItemOnView = false;
    public Action flyEndAction = null;
    public Action closeAction = null;
    public BIHelper.ItemChangeReasonArgs changeReason;
}

public class UIGetRewardParam
{
    public List<ItemData> itemDatas; //item
    public bool autoAddReward = true; //是否自动加上奖励
    public bool fly = true; //是否飞
    public bool abortUserClick = false;
    public bool autoChangeItemOnView = false;
    public Action closeAction; //界面关闭时的回调

    public Action<UniTaskCompletionSource> actionBeforeFly;
    public Action flyEndAction;

    public BIHelper.ItemChangeReasonArgs changeReason;
}

public class UICommonReward : UIViewBase
{
}