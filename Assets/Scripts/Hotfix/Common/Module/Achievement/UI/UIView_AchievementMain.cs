using System.Collections.Generic;
using Framework;
using GameStorage;
using UnityEngine;
using UnityEngine.UI;

public class UIView_AchievementMain : UIView_AchievementMainBase
{
    private int m_type;
    private ModAchievement m_modAchievement;
    private List<UIWidget_AchievementItem> m_achievementItemList = new List<UIWidget_AchievementItem>();
    private List<UIWidget_AchievementTaskItem> m_achievementTaskList = new List<UIWidget_AchievementTaskItem>();
    private float m_time;
    protected override void BindComponent()
    {
        base.BindComponent();
        UIBtn_Task.onClick.AddListener(OnClick_Task);
        UIBtn_MyAchievements.onClick.AddListener(OnClick_Achievement);
        UIBtn_Home.onClick.AddListener(CloseView);
        UIBtn_Lock.onClick.AddListener(LockBtn);
        RegisterEvent<EvtAchievementRefreshUI>(EvtAchievementRefreshUI);
    }

    private void EvtAchievementRefreshUI(EvtAchievementRefreshUI evt)
    {
        OnClick_MyAchievements();
        OnClick_Task();
        UINode_RedDot.gameObject.SetActive(m_modAchievement.GetRedAchievement()>0);
        UINode_RedDot2.gameObject.SetActive(m_modAchievement.IsAddAchievement);
    }

    private void LockBtn()
    {
        if (m_time==0)
        {
            m_time = 2f;
            UINode_Bubble.gameObject.SetActive(true);
            
        }
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

    private void CloseView()
    {
        Game.GetMod<ModUI>().Close(this);
    }
    
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        m_type = 0;
        m_modAchievement = Game.GetMod<ModAchievement>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshBtn();
        RefreshAchievementTaskList();
        UINode_RedDot.gameObject.SetActive(m_modAchievement.GetRedAchievement()>0);
        UINode_RedDot2.gameObject.SetActive(m_modAchievement.IsAddAchievement);
        UINode_AchievementTopItem.gameObject.SetActive(false);
    }
    
    private void RefreshBtn()
    {
        UINode_Task_UnSelected.gameObject.SetActive(m_type == 1);
        UINode_Task_Selected.gameObject.SetActive(m_type == 0);
        UINode_MyAchievements_UnSelected.gameObject.SetActive(m_type == 0);
        UINode_MyAchievements_Selected.gameObject.SetActive(m_type == 1);
        UINode_TaskList.gameObject.SetActive(m_type == 0);
        UINode_AchievementList.gameObject.SetActive(m_type == 1);
    }
    
    private void OnClick_Task()
    {
        if (m_type==0)
            return;
        m_type = 0;
        RefreshBtn();
        RefreshAchievementTaskList();
        UINode_AchievementTopItem.gameObject.SetActive(false);
    }
    
    private void OnClick_MyAchievements()
    {
        if (m_type==1)
            return;
        m_type = 1;
        RefreshBtn();
        RefreshAchievementList();
        UINode_AchievementTopItem.gameObject.SetActive(true);
    }
    
    private void OnClick_Achievement()
    {
        m_modAchievement.IsAddAchievement = false;
        UINode_RedDot2.gameObject.SetActive(m_modAchievement.IsAddAchievement);
        Game.GetMod<ModEvent>().Dispatch(new EvtAchievementRefreshRed());
        OnClick_MyAchievements();
    }
    
    private void RefreshAchievementTaskList()
    {
        var info = m_modAchievement.GetDisplayAchievementList();
       
        for (int i = 0; i < info.Count; i++)
        {
            if (i<m_achievementTaskList.Count)
            {
                m_achievementTaskList[i].RefreshWight(info[i]);
            }
            else
            {
                var item = OpenUIWidget<UIWidget_AchievementTaskItem>(UINode_Content_01.transform, false);
                item.RefreshWight(info[i]);
                m_achievementTaskList.Add(item);
            }
            m_achievementTaskList[i].GO.SetActive(true);
        }
        if (info.Count<m_achievementTaskList.Count)
        {
            for (int i = info.Count; i < m_achievementTaskList.Count; i++)
            {
                m_achievementTaskList[i].GO.SetActive(false);
            }
        }
        var scrollRect = UINode_TaskList.GetComponent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
    
    private void RefreshAchievementList()
    {
        var info = m_modAchievement.GetCollectedAchievement();
        UITxt_MedalCount.SetText(CoreUtils.GetLocalization("UI_Achievement_number",info.Count));
        UINode_Desc.gameObject.SetActive(info.Count==0);
        for (int i = 0; i < info.Count; i++)
        {
            if (i<m_achievementItemList.Count)
            {
                m_achievementItemList[i].RefreshWight(info[i]);
            }
            else
            {
                var item = OpenUIWidget<UIWidget_AchievementItem>(UINode_Content_02.transform, false);
                item.RefreshWight(info[i]);
                m_achievementItemList.Add(item);
            }
            m_achievementItemList[i].GO.SetActive(true);
        }
        if (info.Count<m_achievementItemList.Count)
        {
            for (int i = info.Count; i < m_achievementItemList.Count; i++)
            {
                m_achievementItemList[i].GO.SetActive(false);
            }
        }
        var scrollRect = UINode_AchievementList.GetComponent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
    
    protected override void OnClose()
    {
        base.OnClose();
        m_achievementTaskList.Clear();
        m_achievementItemList.Clear();
    }
    
}
