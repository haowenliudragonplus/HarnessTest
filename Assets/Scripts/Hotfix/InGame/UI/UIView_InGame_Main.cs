using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DragonPlus.Ad;
using DragonPlus.Ad.Max;
using DragonPlus.Ad.Max.Tracking;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIView_InGame_Main : UIView_InGame_MainBase
{
    private InGameModeBase mode;

    private const string NumNodePath = "CounterPanel";
    private const string AddImgPath = "CounterPanel/Img_Add";

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        enableVirbrate = false;
        mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        InitData_HpNodeList();

        if (!Game.GetMod<GuideSys>().IsFinished("GUIDE_103"))
        {
            Game.GetMod<GuideSys>().RegisterTarget(GuideTargetType.ClickItem, UIBtn_Auxiliary.transform);
        }
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent(); ;
        RegisterEvent<EvtPlayBanner>(OnPlayBanner);
        RegisterGameEvent_Other();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_NextLevel.onClick.AddListener(() => { Game.GetMod<ModInGame>().NextLevel(mode.Data.ModeType); });
        UIBtn_LastLevel.onClick.AddListener(() => { Game.GetMod<ModInGame>().LastLevel(mode.Data.ModeType); });
        //UIBtn_Back.onClick.AddListener(OnBackBtn);
        UIBtn_Setting.onClick.AddListener(OnSettingBtn);
        UIBtn_Auxiliary.onClick.AddListener(OnAuxiliaryBtn);
        UIBtn_Hint.onClick.AddListener(OnHintBtn);
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        RefreshView();
    }

    private void RefreshView()
    {
        UINode_ScreenLight.gameObject.SetActive(false);
        UIBtn_Hint.gameObject.SetActive(false);
        RefreshView_DifficultSign();
        UITxt_Steps.gameObject.SetActive(false);
        UINode_HeartGroup.gameObject.SetActive(false);
        if (mode.Data.Group_HpOrStep == EABTestGroup.Group2)
        {
            UITxt_Steps.gameObject.SetActive(true);
            RefreshView_Steps();
        }
        else
        {
            UINode_HeartGroup.gameObject.SetActive(true);
            RefreshView_Hp();
        }
        RefreshView_Hint();
        RefreshView_GM();
    }

    // /// <summary>
    // /// 刷新道具按钮
    // /// </summary>
    // private void RefreshView_ItemBtn(EItemType itemType)
    // {
    //     var btnTrans = GetItemBtnTrans(itemType);
    //     if (btnTrans == null)
    //         return;

    //     btnTrans.transform.Find("CounterPanel").gameObject.SetActive(true);
    //     var UINode_Lock = btnTrans.transform.Find("Lock").gameObject;
    //     var UINode_Count = btnTrans.transform.Find("CounterPanel").gameObject;
    //     var UITxt_Num = btnTrans.transform.Find("CounterPanel/CountText").GetComponent<Text>();
    //     var UIImg_Add = btnTrans.transform.Find("CounterPanel/Img_Add").GetComponent<Image>();
    //     var UIImg_Infinite = btnTrans.transform.Find("Img_Infinite").GetComponent<Image>();
    //     UINode_Count.transform.Find("vfx_lm_baodian_tx").gameObject.SetActive(false);
    //     UITxt_Num.gameObject.SetActive(false);
    //     UIImg_Add.gameObject.SetActive(false);
    //     UINode_Lock.gameObject.SetActive(false);
    //     UIImg_Infinite.gameObject.SetActive(false);
    //     UINode_Count.gameObject.SetActive(false);
    //     int unlockLevel = GetItemUnlockLevel(itemType);
    //     bool isUnlock = mode.Data.LevelNum >= unlockLevel;
    //     bool isInfinite = mode.Data.LevelNum == unlockLevel;
    //     if (isUnlock)
    //     {
    //         if (isInfinite)
    //         {
    //             UIImg_Infinite.gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             UINode_Count.gameObject.SetActive(true);
    //             if (Game.GetMod<ModBag>().GetItemCount(itemType) > 0)
    //             {
    //                 UITxt_Num.text = Game.GetMod<ModBag>().GetItemCount(itemType).ToString();
    //                 UITxt_Num.gameObject.SetActive(true);
    //             }
    //             else
    //             {
    //                 UIImg_Add.gameObject.SetActive(true);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         UINode_Lock.gameObject.SetActive(true);
    //         var UITxt_UnlockLevel = UINode_Lock.transform.Find("Txt_UnLockLevel").GetComponent<TextMeshProUGUI>();
    //         UITxt_UnlockLevel.text = CoreUtils.GetLocalization("UI_common_level", unlockLevel);
    //     }
    // }

    private void RefreshView_GM()
    {
        UIBtn_NextLevel.gameObject.SetActive(Main.GameUtils.IsDevelopmentEnv());
        UIBtn_LastLevel.gameObject.SetActive(Main.GameUtils.IsDevelopmentEnv());
    }

    #region Button

    //private void OnItemBtn(EItemType itemType)
    //{
    // if (InGameUtils.IsWinOrFail())
    //     return;

    // int unlockLevel = GetItemUnlockLevel(itemType);
    // int itemCount = Game.GetMod<ModBag>().GetItemCount(itemType);

    // // 判断是否因为道具不足无法使用
    // if (itemCount <= 0 && mode.Data.LevelNum > unlockLevel)
    // {
    //     BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventBuyItemPop, ((int)itemType).ToString());
    //     var propName = (int)itemType;
    //     Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_GetProp, new UIView_InGame_GetPropParams
    //     {
    //         PropName = (BuyPropItem)propName,
    //         OnPropGetSuccess =
    //         (rv) =>
    //         {
    //             RefreshView_ItemBtn(itemType);
    //         }
    //     });
    // }
    // else if ((mode.Data.LevelNum == unlockLevel)
    //     || (itemCount > 0 && mode.Data.LevelNum >= unlockLevel))
    // {
    //     // 触发道具效果
    //     if (mode.UseItem(itemType))
    //     {
    //         Game.GetMod<ModEvent>().Dispatch<EvtUseItem>(new EvtUseItem(itemType));
    //         if (mode.Data.LevelNum != unlockLevel)
    //         {
    //             Game.GetMod<ModBag>().ConsumeItem(itemType, 1, new BIHelper.ItemChangeReasonArgs()
    //             {
    //                 reason = BiEventArrowPuzzle1.Types.ItemChangeReason.UseItem,
    //                 data1 = ((int)itemType).ToString(),
    //                 data2 = mode.Data.LevelNum.ToString()
    //             });
    //         }
    //     }
    // }
    //}

    private void OnSettingBtn()
    {
        if (!string.IsNullOrEmpty(Game.GetMod<GuideSys>().GetCurGuideId()))
            return;
        if (InGameUtils.IsWinOrFail())
            return;
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_Setting);
    }

    private void OnBackBtn()
    {
        if (InGameUtils.IsWinOrFail())
            return;

        mode.ExitGame();
    }

    #endregion Button

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // public Transform GetItemBtnTrans(EItemType itemType)
    // {
    //     switch (itemType)
    //     {
    //         case EItemType.Shuffle:
    //             return UIBtn_Shuffle.transform;
    //         case EItemType.Hint:
    //             return UIBtn_Hint.transform;
    //         case EItemType.AddSlot:
    //             return UIBtn_AddSlot.transform;
    //         case EItemType.Clear:
    //             return UIBtn_Clear.transform;
    //         default:
    //             return null;
    //     }
    // }

    private void OnPlayBanner(EvtPlayBanner evt)
    {
        if (!Game.GetMod<AdSys>().ShowingBanner)
            return;
        float uiPosBannerY = CTUtils.Screen2UILocal(Vector3.up * GameUtils.GetBannerHeight(), ViewRootRect, Game.GetMod<ModUI>().UICamera).y;
        UINode_Bottom.transform.localPosition = Vector3.up * uiPosBannerY;
    }

    //-----------------

    private List<RectTransform> hpNodeList = new List<RectTransform>();

    private void InitData_HpNodeList()
    {
        hpNodeList.Add(UINode_Heart01);
        hpNodeList.Add(UINode_Heart02);
        hpNodeList.Add(UINode_Heart03);
    }

    private void RefreshView_Steps()
    {
        UITxt_Steps.text = CoreUtils.GetLocalization("UI_Level_Moves", mode.Data.CurStep);
    }

    private void RefreshView_Hp()
    {
        for (int i = 0; i < hpNodeList.Count; i++)
        {
            var animator = hpNodeList[i].GetComponent<Animator>();
            if (mode.Data.CurHp >= i + 1)
            {
                animator.PlayAni("heart_idle").Forget();
            }
            else
            {
                var icon = hpNodeList[i].transform.Find("Icon");
                if (!icon.gameObject.activeSelf)
                    continue;
                animator.PlayAni("posui").Forget();
                // InGameUtils.RegisterTimer(animator.GetClipTime("posui"), ignoreTimeScale: true);
            }
        }
    }

    private void OnReduceHp(EvtRefreshHp evt)
    {
        RefreshView_Hp();
    }

    private void OnRefreshStep(EvtRefreshStep evt)
    {
        RefreshView_Steps();
    }

    private void RegisterGameEvent_Other()
    {
        RegisterEvent<EvtRefreshHp>(OnReduceHp);
        RegisterEvent<EvtShowHintBtn>(OnShowHintBtn);
        RegisterEvent<EvtRefreshStep>(OnRefreshStep);
    }

    public void RefreshView_Hint()
    {
        UIImg_Hide.gameObject.SetActive(!mode.Data.InHint);
        UIImg_Show.gameObject.SetActive(mode.Data.InHint);
    }

    private void RefreshView_DifficultSign()
    {
        UINode_NormalLevel.gameObject.SetActive(false);
        UINode_HardLevel.gameObject.SetActive(false);
        UINode_SuperHardLevel.gameObject.SetActive(false);
        switch ((EIngameDifficultType)mode.Data.LevelCfg.DifficultyType)
        {
            case EIngameDifficultType.Easy:
                UITxt_Level.text = CoreUtils.GetLocalization("UI_common_level", mode.Data.LevelNum);
                UINode_NormalLevel.gameObject.SetActive(true);
                break;
            case EIngameDifficultType.Hard:
                UINode_HardLevel.transform.Find("LevelTxt").GetComponent<TextMeshProUGUI>().text = CoreUtils.GetLocalization("UI_common_level", mode.Data.LevelNum);
                UINode_HardLevel.gameObject.SetActive(true);
                break;
            case EIngameDifficultType.SuperHard:
                UINode_SuperHardLevel.transform.Find("LevelTxt").GetComponent<TextMeshProUGUI>().text = CoreUtils.GetLocalization("UI_common_level", mode.Data.LevelNum);
                UINode_SuperHardLevel.gameObject.SetActive(true);
                break;
        }
    }

    private void OnAuxiliaryBtn()
    {
        mode.Data.InHint = !mode.Data.InHint;
        RefreshView_Hint();
        mode.RefreshHintState(mode.Data.InHint);

        if (mode.Data.InHint)
        {
            mode.Data.AuxiliaryCount++;
            Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.ClickItem);
        }
        else
        {
            SDK<IMaxTracking>.Instance.TrackUseProp();
        }
    }

    private void OnHintBtn()
    {
        var lv = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main).ToString();
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvClick, lv, ((int)eAdReward.Hint).ToString());
        Game.GetMod<AdSys>().TryShowRewardedVideo(eAdReward.Hint, (result, s) =>
        {
            if (this == null || GO == null)
                return;

            if (result == AdPlayResult.Success)
            {
                mode.Data.HintCount++;
                UIBtn_Hint.gameObject.SetActive(false);
                mode.ShowHint();

                SDK<IMaxTracking>.Instance.TrackUseProp();
            }
        });
    }

    public async UniTask PlayColliderOtherEffect()
    {
        UINode_ScreenLight.gameObject.SetActive(true);
        var ani = UINode_ScreenLight.gameObject.GetComponent<Animator>();
        try
        {
            await ani.PlayAni("screenlight", ignoreTimeScale: true);
            UINode_ScreenLight.gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            UINode_ScreenLight.gameObject.SetActive(false);
        }
    }

    /// UI对于棋盘安全区域边界 
    public (Vector3, Vector3) GetUISafeBoardBounds()
    {
        var screenSafeMax = CTUtils.World2Screen(UINode_BoundMax.position, Game.GetMod<ModUI>().UICamera);
        var screenSafeMin = CTUtils.World2Screen(UINode_BoundMin.position, Game.GetMod<ModUI>().UICamera);
        return (screenSafeMax, screenSafeMin);
    }

    public void RefreshView_HintBtn()
    {
        if (UIBtn_Hint.gameObject.activeSelf)
            return;

        bool b = Game.GetMod<AdSys>().ShouldShowRV(eAdReward.Hint, false);
        if (b)
        {
            UIBtn_Hint.gameObject.SetActive(true);
            var lv = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main).ToString();
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvShow, lv, ((int)eAdReward.Hint).ToString());
            SDK<IMaxTracking>.Instance.TrackRewardedAdOpportunity();
        }
    }

    private void OnShowHintBtn(EvtShowHintBtn evt)
    {
        RefreshView_HintBtn();
    }
}
