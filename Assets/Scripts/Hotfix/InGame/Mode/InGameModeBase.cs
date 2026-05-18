using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using DragonPlus.Ad.Max.Tracking;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 局内模式基类
/// </summary>
public class InGameModeBase
{
    public GameObject Root { get; private set; } //场景根节点（为了在获取数据前有一个实体去挂载引用）
    public GameObject GameRoot { get; protected set; } //游戏根节点
    public Transform TopAreaTrans { get; protected set; }//顶部区域节点
    public Transform ElementArea { get; protected set; }//元素区域节点
    public Camera BgCamera { get; protected set; } //背景相机
    public Camera ElementCamera { get; protected set; } //元素相机
    public Camera TopAreaCamera { get; protected set; } //顶部区域相机
    public Camera GuideCamera { get; protected set; } //引导相机

    public CameraInputCtrl CameraInput { get; protected set; } //相机移动控制
    // public CameraComponent CameraComponent { get; protected set; } //相机移动控制

    public InGameDataBase Data { get; protected set; } //局内数据
    public StorageMahjongScrew StorageMahjongScrew { get; private set; } //存档
    public ModInGame ModInGame { get; private set; } //局内模块
    public FsmState_InGame CurState { get; private set; } //当前状态机状态

    public bool IsDispose { get; private set; }

    #region 初始化

    public virtual void InitData(FsmState_InGame.EnterParam enterParam)
    {
        ModInGame = Game.GetMod<ModInGame>();
        StorageMahjongScrew = Game.GetMod<ModStorage>().GetStorage<StorageMahjongScrew>();
        CurState = Game.GetMod<ModFsm>().CurState as FsmState_InGame;

        SpawnGame_Pre();

        //
        CreateData(enterParam);

        RegisterEvent();

        InitFsm();

        BIHelper.SendLevelInfo(Data, BIHelper.ELevelInfoType.Enter);
        Game.GetMod<ModEvent>().Dispatch(new EvtEnterInGame(this));

        SDK<IMaxTracking>.Instance.TrackLevelStart(Data.LevelNum);
    }

    protected virtual void CreateData(FsmState_InGame.EnterParam enterParam)
    {
        Data = new InGameDataBase();
        Data.Init(this, enterParam);
    }

    protected virtual void RegisterEvent()
    {

    }

    protected virtual void InitFsm()
    {
        CurState.AddSubState<SubFsmState_InGame_Prepare>();
        CurState.AddSubState<SubFsmState_InGame_Playing>();
        CurState.AddSubState<SubFsmState_InGame_Pause>();
        CurState.AddSubState<SubFsmState_InGame_Win>();
        CurState.AddSubState<SubFsmState_InGame_Fail>();
        CurState.StartSubFsm();
    }

    #region 生成游戏场景

    /// <summary>
    /// 生成游戏场景（前）
    /// </summary>
    public virtual void SpawnGame_Pre()
    {
        Root = new GameObject("GameRoot");
    }

    /// <summary>
    /// 生成游戏场景
    /// </summary>
    public virtual async void SpawnGame()
    {
        // 生成根节点
        GameRoot = Game.GetMod<ModAsset>().GetGameObject(InGameConst.Prefab_GameRoot).GetInstance();
        GameRoot.ResetLocal();
        GameRoot.transform.SetParent(Root.transform, false);
        // 处理相机相关
        BgCamera = GameRoot.transform.Find("BgCamera").GetComponent<Camera>();
        ElementCamera = GameRoot.transform.Find("ElementCamera").GetComponent<Camera>();
        TopAreaCamera = GameRoot.transform.Find("TopAreaCamera").GetComponent<Camera>();
        GuideCamera = GameRoot.transform.Find("GuideCamera").GetComponent<Camera>();
        Game.GetMod<ModCamera>().AddOverlayCamera(BgCamera, 0);
        Game.GetMod<ModCamera>().AddOverlayCamera(ElementCamera, 1);
        Game.GetMod<ModCamera>().AddOverlayCamera(TopAreaCamera, 2);
        Game.GetMod<ModCamera>().AddOverlayCamera(GuideCamera, 4);
        ElementCamera.orthographicSize = InGameConst.CameraSizeMin;
        // CameraComponent = new CameraComponent(this);
        CameraInput = new CameraInputCtrl(ElementCamera, Data);

        // 实例化元素区域
        {
            ElementArea = new GameObject("ElementArea").transform;
            ElementArea.SetParent(GameRoot.transform, false);
            // 生成箭头实体
            GameObject arrowRoot = new GameObject("ArrowRoot");
            arrowRoot.transform.SetParent(ElementArea.transform, false);
            for (int i = 0; i < Data.LevelData.level.Count; i++)
            {
                GameObject arrowGo = new GameObject();
                if (Main.GameUtils.IsInEditorEnv())
                {
                    arrowGo.name = $"Arrow_{Data.LevelData.level[i][0]}";
                }
                arrowGo.transform.SetParent(arrowRoot.transform, false);
                string colorHex = Data.LevelData.color == null || Data.LevelData.color.Count <= 0
                    ? "#FFFFFF"
                    : Data.LevelData.color[i];
                ArrowEntity arrowEntity = new ArrowEntity(arrowGo, Data.LevelData.level[i], colorHex);
                var triggerComponent = arrowGo.AddComponent<TriggerComponent>();
                var rigidbody = arrowGo.AddComponent<Rigidbody2D>();
                rigidbody.gravityScale = 0;
                rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                Data.AddArrowEntity(arrowEntity);
            }
            // 生成点实体
            GameObject pointRoot = new GameObject("PointRoot");
            pointRoot.transform.SetParent(ElementArea.transform, false);
            for (int i = 0; i < Data.LevelData.wide; i++)
            {
                for (int j = 0; j < Data.LevelData.high; j++)
                {
                    var pointGo = Game.GetMod<ModAsset>().GetGameObject(InGameConst.Prefab_Point).GetInstance();
                    pointGo.transform.SetParent(pointRoot.transform, false);
                    Vector2 pos = Data.SpawnStartPos + new Vector2(InGameConst.PointSpacing * i, InGameConst.PointSpacing * j);
                    pointGo.transform.localPosition = pos;
                    int pointIndex = InGameUtils.GetPointIndex(new Vector2(i, j));
                    if (Main.GameUtils.IsInEditorEnv())
                    {
                        pointGo.name = $"Point_{pointIndex}_({i},{j})";
                    }
                    PointEntity pointEntity = new PointEntity(pointGo, pointIndex);
                    Data.AddPointEntity(pointIndex, pointEntity);
                }
            }

            // 所有元素都置为InGame_Element层
            ElementArea.gameObject.SetLayer(InGameConst.LayerName_InGame_Element, true);
        }
    }

    #endregion 生成游戏场景

    #endregion 初始化

    /// <summary>
    /// 复活
    /// </summary>
    public virtual void ReviveGame(SubFsmState_InGame_Fail.EnterParam param)
    {
        var failType = param.failType;
        switch (failType)
        {
            case EInGameFailType.NoHp:
                var stepGroup = Game.GetMod<ModABTest>().GetABTestGroup(EABTestType.Step_V14);
                if (stepGroup == EABTestGroup.Group2)
                {
                    Data.SetHp(1);
                }
                else
                {
                    Data.SetHp(Data.Hp);
                }

                break;

            case EInGameFailType.NoStep:
                Data.AddStep(5);
                break;
        }

        Data.canOperate = true;
        Data.ReviveCount++;
        Game.GetMod<ModEvent>().Dispatch(new EvtAchievementRevival());
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventReliveSuccess, $"{Data.ReviveCount}", $"{Data.LevelNum}");
        CurState.ChangeSubState<SubFsmState_InGame_Playing>();
    }

    /// <summary>
    /// 重玩游戏
    /// </summary>
    public void RePlayGame()
    {
        ModInGame.EnterGame(Data.LevelIndex, Data.ModeType);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public virtual void ExitGame()
    {
        Game.GetMod<ModFsm>().ChangeState<FsmState_Home>();
    }

    public void RefreshHintState(bool b)
    {
        foreach (var arrowEntity in Data.GetArrowEntityList())
        {
            if (arrowEntity == null)
                continue;
            arrowEntity.RefreshHintLine(b);
        }
    }

    public void ShowHint()
    {
        Data.ResetUnSelectValidArrowTime();

        var validArrow = Data.FindHintArrow();
        if (validArrow == null)
            return;

        validArrow?.BeHint();
    }

    #region 胜利和失败相关

    public virtual bool CheckGameWin()
    {
        if (Data.HasInMoveArrow())
            return false;

        int remainArrowEntityCount = Data.GetArrowEntityList().Count;
        if (remainArrowEntityCount > 0)
            return false;

        HandleGameWin();
        return true;
    }

    public virtual void HandleGameWin()
    {
        if (!CurState.CheckSubState<SubFsmState_InGame_Playing>())
            return;
        Game.GetMod<ModInGame>().AddLevelIndex(Data.ModeType);
        Data.SaveWinSteakCount();

        BIHelper.SendLevelInfo(Data, BIHelper.ELevelInfoType.Pass);
        SDK<IMaxTracking>.Instance.TrackLevelComplete(Data.LevelNum);
        BIHelper.SendAdjustTracking("GAME_EVENT_PASS_LEVEL", new Dictionary<string, object>()
        {
            { "level", Data.LevelNum },
        });

        Data.CheckCloseZoomGuide();
        CurState.ChangeSubState<SubFsmState_InGame_Win>();
    }

    public virtual bool CheckGameFail()
    {
        EInGameFailType failType;
        switch (Data.Group_HpOrStep)
        {
            case EABTestGroup.Group1:
                if (Data.CurHp > 0)
                    return false;
                failType = EInGameFailType.NoHp;
                break;
            case EABTestGroup.Group2:
                if (Data.CurStep > 0
                    || (Data.CurStep <= 0 && Data.GetArrowEntityList().Count <= 1))
                    return false;
                failType = EInGameFailType.NoStep;
                break;
            default:
                if (Data.CurHp > 0)
                    return false;
                failType = EInGameFailType.NoHp;
                break;
        }

        HandleGameFail(failType);
        return true;
    }

    public virtual async void HandleGameFail(EInGameFailType failType)
    {
        Data.canOperate = false;
        await CoreUtils.WaitSeconds(0.5f, true);
        if (!CurState.CheckSubState<SubFsmState_InGame_Playing>())
            return;

        Data.CheckCloseZoomGuide();

        SDK<IMaxTracking>.Instance.TrackLevelComplete(Data.LevelNum);
        SubFsmState_InGame_Fail.EnterParam data = new SubFsmState_InGame_Fail.EnterParam()
        {
            failType = failType,
            reviveTimes = Data.ReviveCount
        };
        CurState.ChangeSubState<SubFsmState_InGame_Fail>(false, data);
        // BIHelper.SendLevelInfo(Data, BIHelper.ELevelInfoType.Fail);
    }

    #endregion 胜利和失败相关

    public virtual void OnDispose()
    {
        IsDispose = true;
        Game.GetMod<ModCamera>().RemoveOverlayCamera(BgCamera);
        Game.GetMod<ModCamera>().RemoveOverlayCamera(ElementCamera);
        Game.GetMod<ModCamera>().RemoveOverlayCamera(TopAreaCamera);
        Game.GetMod<ModCamera>().RemoveOverlayCamera(GuideCamera);
        CurState.RemoveAllSubState();
        Data?.Dispose();
        Game.GetMod<ModCoroutine>().StopCoroutine(ECoroutineBelongType.InGame);
        Game.GetMod<ModTimer>().DisposeAll(ETimerBelongType.InGame);
        Game.GetMod<ModEvent>().UnRegister(EEventBelongType.InGame);
        Object.Destroy(Root);
    }
}
