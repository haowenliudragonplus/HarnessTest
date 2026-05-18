using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using GameStorage;
using TMGame;

public class PopupData
{
    public int uiViewId;
    public object openData;
    public Func<bool> chechFun;
}

/// <summary>
/// 弹窗
/// </summary>
public class ModPopup : ModuleBase
{
    private StoragePopups storagePopups;

    //todo：后面扩展可以添加不同归属的弹窗，不局限于在主界面弹
    private List<PopupData> popupDataList = new List<PopupData>(); //弹窗列表

    private bool inPopups;

    private bool firstCheck = true;

    public override void OnInit()
    {
        base.OnInit();

        RegisterEvent<EvtEnterInGame>(OnEnterInGame);
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        InitData();

        bool isSameDay = TimeUtils.IsSameDay(SDK<IStorage>.Instance.Get<StorageClientCommon>().UserData.LastLoginTime, TimeUtils.GetServerTimeStamp());
        if (!isSameDay)
        {
            storagePopups.DailyPopCountDict.Clear();
        }
    }

    private void InitData()
    {
        storagePopups = SDK<IStorage>.Instance.Get<StorageClientCommon>().Popups;
        var popupsCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_Popups>();
        foreach (var popupsCfg in popupsCfgList)
        {
            if (!storagePopups.EnterLevelTimePopDict.ContainsKey(popupsCfg.UiViewId))
            {
                storagePopups.EnterLevelTimePopDict.Add(popupsCfg.UiViewId, 0);
            }
        }
    }

    /// <summary>
    /// 添加弹窗
    /// </summary>
    public void AddPopup(int uiViewId, Func<bool> checkFun, object openData = null)
    {
        PopupData popupData = new PopupData();
        popupData.uiViewId = uiViewId;
        popupData.chechFun = checkFun;
        popupData.openData = openData;
        popupDataList.Add(popupData);

        Sort();
    }

    /// <summary>
    /// 检查弹窗
    /// </summary>
    public async UniTask CheckPopup()
    {
        if (popupDataList.Count <= 0)
            return;

        foreach (var popupData in popupDataList)
        {
            bool needPopups = false;
            bool needPopup_Internal = popupData.chechFun(); //UI类内部判断是否需要弹出
            if (needPopup_Internal)
            {
                if (!CheckDailyPopCountLimit(popupData.uiViewId)
                    && (CheckDailyPop(popupData.uiViewId) || CheckEnterLevelTime(popupData.uiViewId)))
                {
                    if (Game.GetMod<ModUI>().FindTopView() is UIView_HomeMain)
                    {
                        needPopups = true;
                    }
                }
                // needPopups = true;
            }
            else
            {
            }
            if (!needPopups)
                continue;

            await PopupUIView(popupData);
        }

        firstCheck = false;
    }

    /// <summary>
    /// 检查进入关卡次数
    /// </summary>
    private bool CheckEnterLevelTime(int uiViewId)
    {
        if (storagePopups.EnterLevelTimePopDict == null)
            return false;
        if (!storagePopups.EnterLevelTimePopDict.TryGetValue(uiViewId, out var enterLevelTime))
            return false;
        var popupsCfg = Config_Common.GetPopupsCfg(uiViewId);
        if (popupsCfg == null)
            return false;
        bool ret = enterLevelTime >= popupsCfg.LevelFinishRequired;
        return ret;
    }

    /// <summary>
    /// 检查是否每日弹出
    /// </summary>
    private bool CheckDailyPop(int uiViewId)
    {
        if (storagePopups.DailyPopDict == null)
            return false;
        var popupsCfg = Config_Common.GetPopupsCfg(uiViewId);
        if (firstCheck && (popupsCfg == null || popupsCfg.Showscene1 == 0))
            return false;
        if (!storagePopups.LastTimePopDict.TryGetValue(uiViewId, out var _lastTime))
            return true;
        bool isSameDay = TimeUtils.IsSameDay(_lastTime / 1000, TimeUtils.GetLocalTimeStamp() / 1000);
        return !isSameDay;
    }

    private bool CheckDailyPopCountLimit(int uiViewId)
    {
        if (storagePopups.DailyPopCountDict == null)
            return false;
        if (!storagePopups.DailyPopCountDict.TryGetValue(uiViewId, out var _popCount))
            return false;
        var popupsCfg = Config_Common.GetPopupsCfg(uiViewId);
        if (popupsCfg == null)
            return false;
        bool popLimit = _popCount >= popupsCfg.Showscene4;
        return popLimit;
    }

    private async UniTask PopupUIView(PopupData popupData)
    {
        if (popupData.chechFun())
        {
            Game.GetMod<ModUI>().OpenSync(popupData.uiViewId, popupData.openData);
        }
        SetState(true);
        if (storagePopups.EnterLevelTimePopDict.ContainsKey(popupData.uiViewId))
        {
            storagePopups.EnterLevelTimePopDict[popupData.uiViewId] = 0;
        }
        else
        {
            storagePopups.EnterLevelTimePopDict.Add(popupData.uiViewId, 0);
        }
        if (storagePopups.DailyPopCountDict.ContainsKey(popupData.uiViewId))
        {
            storagePopups.DailyPopCountDict[popupData.uiViewId]++;
        }
        else
        {
            storagePopups.DailyPopCountDict.Add(popupData.uiViewId, 1);
        }
        if (storagePopups.LastTimePopDict.ContainsKey(popupData.uiViewId))
        {
            storagePopups.LastTimePopDict[popupData.uiViewId] = TimeUtils.GetLocalTimeStamp();
        }
        else
        {
            storagePopups.LastTimePopDict.Add(popupData.uiViewId, TimeUtils.GetLocalTimeStamp());
        }
        while (inPopups)
        {
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// 设置弹窗状态
    /// </summary>
    public void SetState(bool state)
    {
        inPopups = state;
    }

    private void Sort()
    {
        popupDataList.Sort((x, y) =>
        {
            var cfg_X = Config_Common.GetPopupsCfg(x.uiViewId);
            var cfg_Y = Config_Common.GetPopupsCfg(y.uiViewId);
            int v_X = cfg_X == null ? -999 : cfg_X.Priority;
            int v_Y = cfg_Y == null ? -999 : cfg_Y.Priority;
            int ret = v_Y.CompareTo(v_X);
            return ret;
        });
    }

    private void OnEnterInGame(EvtEnterInGame evt)
    {
        for (int i = 0; i < storagePopups.EnterLevelTimePopDict.Count; i++)
        {
            storagePopups.EnterLevelTimePopDict[storagePopups.EnterLevelTimePopDict.ElementAt(i).Key]++;
        }
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }
}