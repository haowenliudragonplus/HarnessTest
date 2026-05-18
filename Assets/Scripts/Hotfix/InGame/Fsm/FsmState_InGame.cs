using System.Collections;
using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;

/// <summary>
/// 局内状态
/// </summary>
public class FsmState_InGame : FsmStateBase
{
    public class EnterParam : FsmStateEnterParam
    {
        public EInGameModeType modeType;
        public int levelIndex;

        // GM用
        public string forceJsonFileName;
    }

    public InGameModeBase Mode { get; private set; }

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);
        //Game.GetMod<AdSys>().TryShowBanner(eAdBanner.Traffic);
        //Game.GetMod<ModABTest>().RequestABTestConfig();
        var param = enterParam as EnterParam;
        switch (param.modeType)
        {
            case EInGameModeType.Main:
                Mode = new InGameModeBase();
                break;

            default:
                Mode = new InGameModeBase();
                break;
        }
        Mode.InitData(param);
    }

    public override void OnExit()
    {
        base.OnExit();
        Mode?.OnDispose();
        Game.GetMod<AdSys>().HideBanner();
        Game.GetMod<ModAudio>().StopAllSound();
        Game.GetMod<ModUI>().Close(UIViewName.UIView_InGame_Main);
    }
}
