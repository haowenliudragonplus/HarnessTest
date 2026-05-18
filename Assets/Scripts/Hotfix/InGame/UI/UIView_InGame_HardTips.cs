using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIView_InGame_HardTips : UIView_InGame_HardTipsBase
{
    private InGameModeBase mode;

    private Animator ani;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        enableVirbrate = false;
        mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        ani = GO.GetComponent<Animator>();
    }

    protected override async void OnOpen()
    {
        base.OnOpen();
    }

    public async UniTask PlayAni()
    {
        UINode_HardGroup.gameObject.SetActive(false);
        UINode_SuperHardGroup.gameObject.SetActive(false);
        string aniName = string.Empty;
        switch ((EIngameDifficultType)mode.Data.LevelCfg.DifficultyType)
        {
            case EIngameDifficultType.Hard:
                UINode_HardGroup.gameObject.SetActive(true);
                aniName = "UILevelPreparation_Open";
                break;
            case EIngameDifficultType.SuperHard:
                UINode_SuperHardGroup.gameObject.SetActive(true);
                aniName = "UINode_SuperHardGroup";
                break;
        }
        await ani.PlayAni(aniName, ignoreTimeScale: true);
    }
}
