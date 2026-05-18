using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DragonPlus;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

/// <summary>
/// 主界面
/// </summary>
public class UIView_HomeMain : UIView_HomeMainBase
{
    private ModBag modBag;

    private List<UIWidget_LevelItem> _levelItems = new List<UIWidget_LevelItem>();
    private float m_time = 0;
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Set.onClick.AddListener(OnSetBtn);
        UIBtn_ButtonPlay.onClick.AddListener(OnPlayBtn);
        UIBtn_Achievement.onClick.AddListener(OnAchievementBtn);
        UIBtn_Lock.onClick.AddListener(LockBtn);
    }

    private void OnAchievementBtn()
    {
        Game.GetMod<ModAchievement>().RefreshAchievements();
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_AchievementMain);
    }

    private void LockBtn()
    {
        if (m_time==0)
        {
            m_time = 2f;
            UINode_Bubble.gameObject.SetActive(true);
            
        }
    }
    
    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        RegisterEvent<EvtLanguageChange>(OnEventLanguageChange);
        RegisterEvent<FinishGuideEvent>(OnFinishGuideEvent);
        RegisterEvent<EvtIAPSuccess>(OnIAPSuccess);
        RegisterEvent<EvtAchievementRefreshUI>(RefreshAchievement);
        RegisterEvent<EvtAchievementRefreshRed>(RefreshRedAchievement);
    }

    protected override void OnCreate()
    {
        base.OnCreate(); ;
        modBag = Game.GetMod<ModBag>();

        // FlyTarget.AddTarget(EItemType.Energy, UIImg_Infinite.transform);
        // FlyTarget.AddTarget(EItemType.EnergyInfinity, UIImg_Infinite.transform);
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        enableVirbrate = false;
        playSound = false;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (UINode_Bubble.gameObject.activeSelf)
        {
            m_time-= Time.deltaTime;
            if (m_time<=0)
            {
                m_time = 0;
                UINode_Bubble.gameObject.SetActive(false);
            }
        }
       
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // FlyTarget.RemoveTarget(EItemType.Energy, UIImg_Infinite.transform);
        // FlyTarget.RemoveTarget(EItemType.EnergyInfinity, UIImg_Infinite.transform);
       // FlyTarget.RemoveTarget(EItemType.Coin, UISubView_ResourceBar.GetResourceIconTransByItemId(EItemType.Coin));

        if (_levelItems.Count > 0)
        {
            foreach (var uiWidgetLevelItem in _levelItems)
            {
                CloseUIWidget(uiWidgetLevelItem, true);
            }
            _levelItems.Clear();
        }
    }

    private void RefreshView()
    {
        RefreshView_InGame();
        RefreshView_ResourceBar();
        RefreshView_ActivityEntrance();
        RefreshAchievement(new EvtAchievementRefreshUI());
        UINode_Hand.gameObject.SetActive(false);
    }
    
    private void RefreshAchievement(EvtAchievementRefreshUI evt)
    { 
        UIImg_RedDot.gameObject.SetActive(Game.GetMod<ModAchievement>().GetRedAchievement() > 0||
                                          Game.GetMod<ModAchievement>().IsAddAchievement);
    }

    private void RefreshRedAchievement(EvtAchievementRefreshRed evt)
    { 
        UIImg_RedDot.gameObject.SetActive(Game.GetMod<ModAchievement>().GetRedAchievement() > 0||
                                          Game.GetMod<ModAchievement>().IsAddAchievement);
    }
    
    private void RefreshView_InGame()
    {
        UITxt_Play.SetText(CoreUtils.GetLocalization("UI_button_play"));
        int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
        var info = Game.GetMod<ModInGame>().GetLevelCfg(levelIndex, EInGameModeType.Main, false);
        UINode_Hard.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.Hard);
        UINode_SuperHard.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.SuperHard);

        RefreshView_LevelScroll(levelIndex + 1);
    }

    private void RefreshView_LevelScroll(int curLevel)
    {
        int startLevel = curLevel == 1 ? 1 : curLevel - 1;
        int endLevel = startLevel + 9;    // 有循环关 不会出现无关卡的情况

        if (_levelItems.Count == 0)
        {
            for (int i = endLevel; i >= startLevel; i--)
            {
                var item = OpenUIWidget<UIWidget_LevelItem>(UINode_LevelContent, false, i);
                _levelItems.Add(item);
            }
        }
        else
        {
            for (int i = 0; i < _levelItems.Count; i++)
            {
                var item = _levelItems[i];
                item.SetData(endLevel - i);
            }
        }

        UINode_LevelContent.anchoredPosition = new Vector2(0, -336);
    }

    private void OnEventLanguageChange(EvtLanguageChange evt)
    {

    }

    private void OnIAPSuccess(EvtIAPSuccess evt)
    {
        RefreshView_ActivityEntrance();
    }

    private void OnFinishGuideEvent(FinishGuideEvent obj)
    {
    }
    
    #region Button

    private void OnPlayBtn()
    {
        int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
        Game.GetMod<ModInGame>().EnterGame(levelIndex, EInGameModeType.Main);
    }

    private void OnSetBtn()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Setting);
    }

    #endregion Button

    #region 资源栏

    private void RefreshView_ResourceBar()
    {
        return;
        List<UIWidget_ResourceBarItem.OpenData> resourceBarItemDataList = new List<UIWidget_ResourceBarItem.OpenData>();
        resourceBarItemDataList.Add(new UIWidget_ResourceBarItem.OpenData
        {
            itemId = (int)EItemType.Coin,
            showAddBtn = true
        });
        // resourceBarItemDataList.Add(new UIWidget_ResourceBarItem.OpenData
        // {
        //     itemId = (int)EItemType.Energy,
        //     showAddBtn = true
        // });
        UISubView_ResourceBar.SetView(resourceBarItemDataList);
        FlyTarget.AddTarget(EItemType.Coin, UISubView_ResourceBar.GetResourceIconTransByItemId(EItemType.Coin));
    }

    #endregion 资源栏

    #region 入口

    private void RefreshView_ActivityEntrance()
    {
        // --------------------------------------先这样写，后面完善一套完整活动系统

        // 去广告礼包
        Game.GetMod<ModActivity_RemoveAd>().RefreshEntrance();
    }

    public Transform GetActivityEntrancePos(bool right)
    {
        var trans = right
            ? UINode_Entrance_Right.transform
            : UINode_Entrance_Left.transform;
        return trans;
    }

    #endregion 入口
}