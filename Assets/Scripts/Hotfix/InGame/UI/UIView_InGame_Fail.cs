using DragonPlus.Ad;
using DragonPlus.Ad.Max.Tracking;
using DragonPlus.Config.InGame;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_InGame_Fail : UIView_InGame_FailBase
{
    public class UIView_Fail_Param
    {
        public SubFsmState_InGame_Fail.EnterParam enterParam;
    }

    private UIView_Fail_Param paramData;
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        paramData = viewData as UIView_Fail_Param;
        playSound = false;
    }

    private int ClickTimes = 0;

    private bool _isFreeRevive = false;

    protected override void OnOpen()
    {
        base.OnOpen();
        if (paramData == null) return;

        Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_lose);
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        if (mode.Data.Group_HpOrStep == EABTestGroup.Group2)
        {
            UITxt_Desc.text = CoreUtils.GetLocalization("UI_revive_continue_tips_01");
            UIImg_HeartIcon.gameObject.SetActive(false);
            UIImg_AddSteps.gameObject.SetActive(true);
        }
        else
        {
            UITxt_Desc.text = CoreUtils.GetLocalization("UI_revive_continue_tips");
            UIImg_HeartIcon.gameObject.SetActive(true);
            UIImg_AddSteps.gameObject.SetActive(false);
        }
        var levelId = mode?.Data.LevelNum ?? 10;
        _isFreeRevive = levelId <= Game.GetMod<ModConfig>().GetConstConfig<Table_InGame_Global, int>("FreeRevive");
        UINode_WatchAds.gameObject.SetActive(!_isFreeRevive);
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvShow, mode.Data.LevelNum.ToString(), ((int)eAdReward.Revive).ToString());
        UINode_NoAds.gameObject.SetActive(_isFreeRevive);
        if (!_isFreeRevive)
        {
            SDK<IMaxTracking>.Instance.TrackRewardedAdOpportunity();
        }

        UINode_Heart.gameObject.SetActive(false, true);
        var stepGroup = Game.GetMod<ModABTest>().GetABTestGroup(EABTestType.Step_V14);
        UINode_Heart.gameObject.SetActive(true);
        if (stepGroup == EABTestGroup.Group2)
        {
            UINode_Heart.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            UINode_Heart.gameObject.SetActive(true, true);
        }


        BIHelper.SendLevelInfo(mode.Data, BIHelper.ELevelInfoType.Fail);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.gameObject.SetActive(false);
        // UIBtn_CloseButton.onClick.AddListener(OnCloseClick);
        UIBtn_Restart.onClick.AddListener(OnRePlayBtn);
        UIBtn_Revive.onClick.AddListener(OnReviveBtn);
    }

    private void OnRePlayBtn()
    {
        Game.GetMod<AdSys>().TryShowInterstitial(eAdInterstitial.GameOver);
        Close();
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        BIHelper.SendLevelInfo(mode.Data, BIHelper.ELevelInfoType.Restart);
        mode.RePlayGame();
    }

    private void OnReviveBtn()
    {
        if (_isFreeRevive)
        {
            var state1 = Game.GetMod<ModFsm>().CurState as FsmState_InGame;
            Close();
            state1.Mode.ReviveGame(paramData.enterParam);
            return;
        }


        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRelive, $"{mode.Data.ReviveCount + 1}", $"{mode.Data.LevelNum}");
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvClick, mode.Data.LevelNum.ToString(), ((int)eAdReward.Revive).ToString());
        Game.GetMod<AdSys>().TryShowRewardedVideo(eAdReward.Revive, (result, s) =>
        {
            if (this == null || GO == null)
                return;

            if (result == AdPlayResult.Success)
            {
                var state = Game.GetMod<ModFsm>().CurState as FsmState_InGame;
                Close();
                state.Mode.ReviveGame(paramData.enterParam);
            }
        });
    }

    private void OnCloseClick()
    {
        Close();
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        mode.ExitGame();

        // var state = Game.GetMod<ModFsm>().CurState as FsmState_InGame;
        // if (ClickTimes == 0)
        // {
        //     var p = state.Mode.BoxArea.GetBoxProgress();
        //     UIImg_Slider.fillAmount = p;
        //     var s = Mathf.RoundToInt(p * 100);
        //     UITxt_SliderNumber.text = $"{Mathf.RoundToInt(p * 100)}%";
        //     UINode_Fail_Settlement.gameObject.SetActive(true);
        //     UINode_Fail_Settlement_Second.gameObject.SetActive(false);
        //     UINode_OneBtn.gameObject.SetActive(false);
        //     UINode_TwoBtns.gameObject.SetActive(true);
        //     UINode_Fail_Buy.gameObject.SetActive(false);
        // }
        // if (ClickTimes == 1)
        // {
        //     UINode_Fail_Settlement.gameObject.SetActive(false);
        //     UINode_Fail_Settlement_Second.gameObject.SetActive(true);
        //     UINode_OneBtn.gameObject.SetActive(true);
        //     UINode_TwoBtns.gameObject.SetActive(false);
        //     UITxt_TitleText.text = CoreUtils.GetLocalization("UI_level_desc_20");
        // }
        // if (ClickTimes == 2)
        // {
        //     Game.GetMod<AdSys>().TryShowInterstitial(eAdInterstitial.QuitGame);
        //     Close(true, () =>
        //     {
        //         (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode.ExitGame();
        //         // Game.GetMod<ModInGame>().EnterGame(state.Mode.Data.LevelIndex, state.Mode.Data.ModeType);
        //     });
        // }

        // ClickTimes++;
    }


    void OnCoinBuyClick()
    {
        // int price = 0;
        // var cfgs = Game.GetMod<ModConfig>().GetConfigs<Table_InGame_Revive>();
        // var state = (Game.GetMod<ModFsm>().CurState as FsmState_InGame);
        // //state.Mode.Data.OnReviveClick();
        // foreach (var c in cfgs)
        // {
        //     if (c.Times == Mathf.Clamp(paramData.reviveTimes, 0, 2))
        //     {
        //         price = c.ReviveCost;
        //     }
        // }
        // if (Game.GetMod<ModBag>().CanAfford(EItemType.Coin, price))
        // {
        //     Game.GetMod<ModBag>().ConsumeItem(EItemType.Coin, price, new BIHelper.ItemChangeReasonArgs
        //     {
        //         reason = BiEventArrowPuzzle1.Types.ItemChangeReason.Revive, 
        //         data1 = state.Mode.Data.ReviveCount .ToString(),
        //         data2 = state.Mode.Data.LevelNum.ToString()
        //     }, OnReviveSuccess);
        // }
        // else
        // {
        //     // Close(true, () =>
        //     // {
        //     // });
        //     Game.GetMod<ModFunctionJump>().Jump(2); 
        // }
        // //BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRelive, state.Mode.Data.ReviveClickCount.ToString(), state.Mode.Data.LevelNum.ToString());
    }

}
