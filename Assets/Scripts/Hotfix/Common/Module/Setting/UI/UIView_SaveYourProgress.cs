using System;
using DragonPlus;
using DragonPlus.Account;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using GameStorage;
using UnityEngine;

/// <summary>
/// 保存进度界面
/// </summary>
public class UIView_SaveYourProgress : UIView_SaveYourProgressBase
{
    private const string m_AppleLoginKey = "&key.UI_save_ios_button_apple";
    private const string m_FacebookLoginKey = "&key.UI_save_ios_button_fb";
    private const string m_ConnectedKey = "&key.UI_button_connected";

    protected override void RegisterGameEvent()
    {
        base.RemoveGameEvent();
        RegisterEvent<EvtOnApplicationPause>(OnApplicationPause);
        RegisterEvent<EvtBindFacebook>(OnBindFacebook);
        RegisterEvent<EvtBindApple>(OnBindApple);
    }

    public void OnApplicationPause(EvtOnApplicationPause evt)
    {
        if (!evt.pause)
        {
            Game.GetMod<ModUI>().CloseWaiting();
        }
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.onClick.AddListener(OnCloseBtn);
        UIBtn_BindApple.onClick.AddListener(OnBindAppleBtn);
        UIBtn_BindFb.onClick.AddListener(OnBindFbBtn);
    }

    private void RefreshView()
    {
        var hasBindFacebook = SDK<IAccount>.Instance.HasBindFacebook();
        var hasBindApple = SDK<IAccount>.Instance.HasBindApple();
        UIBtn_BindApple.gameObject.SetActive(SDK<IAccount>.Instance.CanBindApple());
        if (hasBindApple)
        {
            UIBtn_BindApple.enabled = false;
            UITxt_BindApple.text = CoreUtils.GetLocalization(m_ConnectedKey);
        }
        else
        {
            UIBtn_BindApple.enabled = true;
            UITxt_BindApple.text = CoreUtils.GetLocalization(m_AppleLoginKey);
        }
        if (hasBindFacebook)
        {
            UIBtn_BindFb.enabled = false;
            UITxt_BindFb.text = CoreUtils.GetLocalization(m_ConnectedKey);
        }
        else
        {
            UIBtn_BindFb.enabled = true;
            UITxt_BindFb.text = CoreUtils.GetLocalization(m_FacebookLoginKey);
        }
    }

    private void OnBindFbBtn()
    {
        if (!Main.GameUtils.HasNetwork())
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_fb_login_fail_tips"),
                showMidBtn = true,
                showCloseBtn = false,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            return;
        }

        Game.GetMod<ModUI>().ShowWaiting();
        Game.GetMod<ModAccount>().BindFacebook(() => { Close(); });
    }

    private void OnBindAppleBtn()
    {
        if (!Main.GameUtils.HasNetwork())
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_fb_login_fail_tips"),
                showMidBtn = true,
                showCloseBtn = false,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            return;
        }

        Game.GetMod<ModUI>().ShowWaiting();
        Game.GetMod<ModAccount>().BindApple(() =>
        {
            CLog.Error("11 cancle bindapple");
            Close();
        });
    }

    private void OnBindFacebook(EvtBindFacebook evt)
    {
        if (!evt.isSuccess)
            return;
        Close();
    }

    private void OnBindApple(EvtBindApple evt)
    {
        if (!evt.isSuccess)
            return;
        Close();
    }

    private void OnCloseBtn()
    {
        Close();
    }
}