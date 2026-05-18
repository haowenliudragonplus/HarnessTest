using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class SubFsmState_InGame_Fail : FsmStateBase
{
    private EnterParam param;
    public class EnterParam : FsmStateEnterParam
    {
        public EInGameFailType failType;
        public int reviveTimes;
    }

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);
    }

    public override async UniTask PreOnEnter(FsmStateEnterParam enterParam = null)
    {
        param = enterParam as EnterParam;
        var enterData = new UIView_InGame_Fail.UIView_Fail_Param
        {
            enterParam = param,
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_Fail, enterData);
    }
}
