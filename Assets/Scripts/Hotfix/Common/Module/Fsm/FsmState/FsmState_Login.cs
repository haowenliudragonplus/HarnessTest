using System.Collections;
using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;

/// <summary>
/// 登录状态
/// </summary>
public class FsmState_Login : FsmStateBase
{
    public class EnterParam : FsmStateEnterParam
    {
        public bool reLogin; //是否模拟登陆走进度条
    }

    public override void OnInit(FsmStateInitParam initParam = null)
    {
        base.OnInit(initParam);
        Input.multiTouchEnabled = true;
    }

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);
        if (enterParam == null)
        {
            Game.GetMod<ModAccount>().Login();
            return;
        }
        EnterParam param = (EnterParam)enterParam;
        if (param.reLogin)
        {
            UIView_Loading.OpenData openData = new UIView_Loading.OpenData()
            {
                onComplete = () => { Game.GetMod<ModAccount>().Login(); }
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Loading, openData);
        }
        else
        {
            Game.GetMod<ModAccount>().Login();
        }
    }
}