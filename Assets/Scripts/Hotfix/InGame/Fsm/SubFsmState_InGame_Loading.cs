using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Framework;
using TMGame;
using UnityEngine;

public class SubFsmState_InGame_Loading : FsmStateBase
{
    public class EnterParam : FsmStateEnterParam
    {
        public bool isEnter;
    }

    private EnterParam param;

    public override async UniTask PreOnEnter(FsmStateEnterParam enterParam = null)
    {
        param = enterParam as EnterParam;

        GameUtils.SetEventSystemEnable(false);
        if (param.isEnter)
        {
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_Main);

            Game.GetMod<AdSys>().TryShowBanner(eAdBanner.InGame);
            Game.GetMod<ModFsm>().CurState.ChangeSubState<SubFsmState_InGame_Prepare>();
            Game.GetMod<ModAudio>().PlaySound(3);
        }
        else
        {
            Game.GetMod<AdSys>().HideBanner();
            Game.GetMod<ModFsm>().ChangeState<FsmState_Home>();
        }
        GameUtils.SetEventSystemEnable(true);
    }
}
