using System;
using DragonPlus.Account;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;

public class ModAccount : ModuleBase
{
    /// <summary>
    /// 登录
    /// </summary>
    public void Login()
    {
        SDK<IAccount>.Instance.Login(OnLoginSuccess);
    }

    /// <summary>
    /// 登录成功
    /// </summary>
    private async void OnLoginSuccess(bool success = true, string reason = "")
    {
        CLog.Error($"登录完成，状态：{success}，reason：{reason}");
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFteConnectSeverSuccess);

        InitUserData();
        await SDK.Instance.OnUserLoginFinish();
        Game.OnLoginSuccess();

        Game.GameInited = true;

        Game.Start();
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventLogin);
        Game.GetMod<ModFsm>().ChangeState<FsmState_Home>();
    }

    /// <summary>
    /// 绑定Facebook
    /// </summary>
    public void BindFacebook(Action cancelCallback)
    {
#if UNITY_EDITOR
        OnBindFbResult(true, "");
#else
        SDK<IAccount>.Instance.BindFacebook(OnBindFbResult, cancelCallback);
#endif
    }

    public void BindApple(Action cancelCallback)
    {
#if UNITY_EDITOR
        OnBindAppleResult(true, "");
#else
        SDK<IAccount>.Instance.BindApple(OnBindAppleResult, cancelCallback);
#endif
    }

    private void OnBindFbResult(bool isSuccess, string reason)
    {
        Game.GetMod<ModUI>().CloseWaiting();

        if (isSuccess)
        {
            if (!SDK<IStorage>.Instance.Get<StorageClientCommon>().SocialBind.FacebookBindRewardReceived)
            {
                SDK<IStorage>.Instance.Get<StorageClientCommon>().SocialBind.FacebookBindRewardReceived = true;

                // // 发奖
                // var rewardCfg = Game.GetMod<ConfigMgr>().GetConfig<Table_Common_GlobalReward>((int)GlobalRewardType.FBBind);
                // UIGetRewardParam param = new UIGetRewardParam();
                // param.itemDatas = TMItemUtility.GlobalRewardToItems(rewardCfg);
                // param.changeReason = new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.FbBind);
                // TMItemUtility.DispenseReward(param.itemDatas, param.changeReason);
                // Game.GetMod<UIMgr>().OpenSync(UIViewName.UICommonReward, param);
            }
        }
        else
        {
            //BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventLoginFacebookFailed);
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_bind_fb_fail_authen_error_text"),
                showCloseBtn = false,
                showMidBtn = true,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
        }
        Game.GetMod<ModEvent>().Dispatch(new EvtBindFacebook(isSuccess));
    }

    private void OnBindAppleResult(bool isSuccess, string reason)
    {
        Game.GetMod<ModUI>().CloseWaiting();

        if (isSuccess)
        {
        }
        else
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_bind_apple_fail_authen_error_text"),
                showCloseBtn = false,
                showMidBtn = true,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
        }
        Game.GetMod<ModEvent>().Dispatch(new EvtBindApple(isSuccess));
    }

    private void InitUserData()
    {
        // 新用户初始化
        var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
        if (StorageClientCommon.UserData.FirstLoginTime == 0)
        {
            // 设置初始用户数据
            StorageClientCommon.UserData.FirstLoginTime = TimeUtils.GetServerTimeStamp();
            StorageClientCommon.UserData.FirstAppVersion = GameConfig.AppVersion;
            StorageClientCommon.UserData.FirstResVersion = GameConfig.CurResVersion;

            // 添加新用户初始道具
            var initItemPackageCfg = Game.GetMod<ModBag>().GetItemPackageCfg(EItemPackageType.Init);
            if (initItemPackageCfg is { ItemIdList: { Count: > 0 } })
            {
                Game.GetMod<ModBag>().AddItem(initItemPackageCfg.ItemIdList, initItemPackageCfg.ItemCountList,
                    new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.CreateProfile));
            }

            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFteCreateProfileSuccess);
        }

        StorageClientCommon.UserData.LastLoginTime = TimeUtils.GetServerTimeStamp();
        StorageClientCommon.UserData.LastAppVersion = GameConfig.AppVersion;
        StorageClientCommon.UserData.LastResVersion = GameConfig.CurResVersion;
    }
}