using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

/// <summary>
/// 主界面状态
/// </summary>
public class FsmState_Home : FsmStateBase
{
    public override void OnInit(FsmStateInitParam initParam = null)
    {
        base.OnInit(initParam);
        Game.GetMod<ModCamera>().AddOverlayCamera(Game.GetMod<ModUI>().UICamera);
    }

    public override async void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);

        Game.GetMod<ModAudio>().PlayBGM(1);
        Game.GetMod<ModCamera>().AddOverlayCamera(Camera.main, 0);
        //更新活动
        //Game.GetMod<ActivitySys>().RequestActivityData(false);

        // 打开主界面
        var uiBase = Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_HomeMain);
        UIView_Boot.IsBootSuccess = true;
        //ShowTopGuide();
        //await AwaitShowTopGuide();

        // -----开始 主界面效果
        GameUtils.SetEventSystemEnable(false);
        // 飞道具
        await Game.GetMod<ModFly>().CheckToFlyItemDict();
        Game.GetMod<ModFly>().ClearToFlyItemDict();
        // 飞星星宝箱
        // if (Game.GetMod<ModFsm>().PreState is FsmState_Mahjong )
        // {
        //     var uiMain = uiBase as UIView_HomeMain;
        //     uiMain?.GO.GetComponent<Animator>().Play("Main_Interface_disappear");
        // }
        GameUtils.SetEventSystemEnable(true);
        await ShowGuide();
        // 弹窗逻辑
        await Game.GetMod<ModPopup>().CheckPopup();

        //-----结束 主界面效果

        // 第一关引导没完成，直接进入关卡
        int curLevelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
        // cehua:首次启动直接进第一关。（第一关新手引导未结束，每次启动都直接进第一关）
        if (curLevelIndex + 1 == 1 && !Game.GetMod<GuideSys>().IsFinished("GUIDE_101"))
        {
            Game.GetMod<ModInGame>().EnterGame(curLevelIndex, EInGameModeType.Main);
        }
    }

    private void ShowTopGuide()
    {

    }

    private async UniTask AwaitShowTopGuide()
    {
        var guide = Game.GetMod<GuideSys>();
        while (!string.IsNullOrEmpty(guide.GetCurGuideId()))
        {
            await UniTask.Yield();
        }
    }

    private async UniTask ShowGuide()
    {
    }

    public override void OnExit()
    {
        base.OnExit();
        Game.GetMod<ModCamera>().RemoveOverlayCamera(Camera.main);
        Game.GetMod<ModUI>().Close(UIViewName.UIView_HomeMain, false);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}