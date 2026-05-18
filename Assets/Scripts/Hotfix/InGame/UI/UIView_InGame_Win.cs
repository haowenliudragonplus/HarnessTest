using System.Collections.Generic;
using DragonPlus.Ad;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_InGame_Win : UIView_InGame_WinBase
{
    public class EnterParam
    {
        public Dictionary<EItemType, int> winRewards;
    }

    private EnterParam paraData;
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        paraData = viewData as EnterParam;
        playSound = false;
    }

    private bool rvClicked = false;
    protected override void OnOpen()
    {
        base.OnOpen();
        if (paraData == null) return;

        Game.GetMod<ModUI>().Close(UIViewName.UIView_InGame_Setting);

        // if (paraData.winRewards.ContainsKey(EItemType.Coin))
        // {
        //     UITxt_GoldCount.text = paraData.winRewards[EItemType.Coin].ToString();
        // }
        // bool haveRv = Game.GetMod<AdSys>().IsRewardUnlock(eAdReward.DoubleWin);
        UIBtn_ViewAD.gameObject.SetActive(false);
        Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_win);
        int ranNum = Random.Range(1, 10);
        UITxt_Title.text = CoreUtils.GetLocalization($"UI_end_title_{ranNum}");
        InGameUtils.RegisterTimer(2.5f, onComplete: (v) =>
        {
            Game.GetMod<AdSys>().TryShowInterstitial(eAdInterstitial.CompleteLevel);
            Close();
            int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
            Game.GetMod<ModInGame>().EnterGame(levelIndex, EInGameModeType.Main);
        }, ignoreTimeScale: true);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        /*UIBtn_ViewAD.onClick.AddListener(() =>
        {
            var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvClick, mode.Data.LevelNum.ToString(), ((int)eAdReward.DoubleWin).ToString(), ((int)EItemType.Coin).ToString());
            Game.GetMod<AdSys>().TryShowRewardedVideo(eAdReward.DoubleWin, (result, s) =>
            {
                BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventRvShow, mode.Data.LevelNum.ToString(), ((int)eAdReward.DoubleWin).ToString(), ((int)EItemType.Coin).ToString());
                if (result == AdPlayResult.Success)
                {
                    var coinCnt = paraData.winRewards[EItemType.Coin];
                    Game.GetMod<ModBag>().AddItem(EItemType.Coin, coinCnt, new BIHelper.ItemChangeReasonArgs { reason = BiEventArrowPuzzle1.Types.ItemChangeReason.Ads, data1 = ((int)eAdReward.DoubleWin).ToString(), data2 = ((int)EItemType.Coin).ToString(), data3 = mode.Data.LevelNum.ToString() });
                    if (mode.Data.LevelNum >= InGameConst.GuideLevelCount)
                    {
                        Game.GetMod<ModFly>().AddItem(EItemType.Coin, coinCnt);
                    }
                    Close(true, (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode.ExitGame);
                }
            });
        });*/
        // UIBtn_Continue.onClick.AddListener(() =>
        // {
        //     Game.GetMod<AdSys>().TryShowInterstitial(eAdInterstitial.CompleteLevel);
        //     Close();
        //     var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        //     if (mode.Data.LevelNum < InGameConst.GuideLevelCount)
        //     {
        //         int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
        //         Game.GetMod<ModInGame>().EnterGame(levelIndex, EInGameModeType.Main);
        //     }
        //     else
        //     {
        //         mode.ExitGame();
        //     }
        // });
    }
}
