using Cysharp.Threading.Tasks;
using Framework;
using TMGame;

public class SubFsmState_InGame_Prepare : FsmStateBase
{
    private InGameModeBase Mode;

    public override async UniTask PreOnEnter(FsmStateEnterParam enterParam = null)
    {
        Mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        if (Mode.IsDispose)
            return;

        Game.GetMod<ModAudio>().PlaySound(3);

        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_Main);

        Game.GetMod<AdSys>().TryShowBanner(eAdBanner.InGame);

        Mode.SpawnGame();

        // // 判断是否需要展示困难提示 2026-01-13 18:25:30 cehua:去掉恶魔提示
        // if (Mode.Data.LevelCfg.DifficultyType != (int)EIngameDifficultType.Easy)
        // {
        //     var view = Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_HardTips) as UIView_InGame_HardTips;
        //     await view.PlayAni();
        //     Game.GetMod<ModUI>().Close(UIViewName.UIView_InGame_HardTips);
        // }

        if (Mode.Data.LevelNum > 1)
        {
            await Mode.Data.DisplayLineEnterAnim();
        }

        Mode.Data.RegisterGuideInfo();
    }

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);
        Mode.CurState.ChangeSubState<SubFsmState_InGame_Playing>();
    }
}