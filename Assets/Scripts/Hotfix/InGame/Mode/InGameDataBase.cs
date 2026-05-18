using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.InGame;
using DragonPlus.Save;
using Framework;
using TMGame;
using UnityEngine;

/// <summary>
/// 局内数据基类
/// </summary>
public class InGameDataBase
{
    public InGameModeBase Mode { get; private set; } //模式
    public float Duration { get; private set; } // 关卡用时

    public int LevelIndex { get; private set; } //关卡index
    public int LevelNum => LevelIndex + 1; //关卡id
    public EInGameModeType ModeType { get; private set; } //模式类型
    public Table_InGame_Level LevelCfg { get; private set; } //关卡配置
    public string JsonFileName { get; private set; } //关卡Json文件名
    public LevelData LevelData { get; private set; } //关卡布局数据

    public EABTestGroup Group_HpOrStep;//Hp和步数的ABTest分组
    public BoolProperty HasCannotCompleteCarInMove = new BoolProperty();//是否有不能完成的车正在移动
    public int Hp { get; private set; }//血量
    public int CurHp { get; private set; }//当前血量
    public int Step { get; private set; }//步数
    public int CurStep { get; private set; }//当前步数
    public bool canOperate = true;//是否可以操作

    public Vector2 SpawnStartPos { get; private set; }//生成的起始坐标
    public bool InHint;//是否在提示中
    public int AuxiliaryCount { get; set; } // 使用辅助线道具数量（点击出现一次提示算一次）
    public int HintCount { get; set; } // 使用提示道具数量（点击出现一次提示算一次）

    private float unSelectValidArrowTime;//没有选择合法箭头的时间

    private Dictionary<EItemType, int> itemUseCountDict = new Dictionary<EItemType, int>(); //道具栏元素使用次数

    public int TempWinStreakCount { get; private set; } //临时存的当前连胜次数
    public int TempLoseStreakCount { get; private set; } //临时存的当前连败次数
    public int ReviveCount { get; set; } // 复活次数
    public int RemainArrowCount => arrowEntityList.Count; // 剩余箭头数量
    public int TotalArrowCount => LevelData.level.Count; // 总箭头数量

    public void Init(InGameModeBase mode, FsmState_InGame.EnterParam enterParam)
    {
        Mode = mode;
        InitData_Common(enterParam.levelIndex, enterParam.modeType, enterParam.forceJsonFileName);
        InitData_Temp();
    }

    public void InitData_Common(int levelIndex, EInGameModeType modeType, string foreceJsonFileName)
    {
        LevelIndex = levelIndex;
        ModeType = modeType;
        LevelCfg = Mode.ModInGame.GetLevelCfg(levelIndex, modeType);
        JsonFileName = string.IsNullOrEmpty(foreceJsonFileName)
            ? LevelCfg.Json
            : foreceJsonFileName;
        LevelData = Mode.ModInGame.GetLevelLayoutData(JsonFileName);

        Group_HpOrStep = Game.GetMod<ModABTest>().GetABTestGroup(EABTestType.HpOrStep);
        Hp = LevelData.hp;
        CurHp = Hp;
        Step = LevelCfg.Move;
        if (Step <= 0)
        {
            Step = 100;
            CLog.Error("关卡配置的步数有误，levelIndex:" + levelIndex);
        }
        CurStep = Step;

        // 计算生成的起始坐标（左下角）
        int maxIndex = 0;
        LevelData.level.ForEach(item =>
        {
            int maxIndex_Temp = item.Max();
            if (maxIndex_Temp > maxIndex)
            {
                maxIndex = maxIndex_Temp;
            }
        });
        int maxIndexY = maxIndex / LevelData.wide;
        int configY = LevelData.high - 1;
        int offset = configY - maxIndexY;
        float totalWidth = (LevelData.wide - 1) * InGameConst.PointSpacing;
        float totalHeight = (LevelData.high - 1) * InGameConst.PointSpacing;
        SpawnStartPos = new Vector2(-totalWidth / 2, -totalHeight / 2 + offset * InGameConst.PointSpacing / 2);

        SaveEnterCount(modeType, levelIndex);
    }

    public void InitData_Temp()
    {
        if (!Mode.StorageMahjongScrew.WinSteakCountDict.ContainsKey((int)ModeType))
        {
            Mode.StorageMahjongScrew.WinSteakCountDict.Add((int)ModeType, 0);
        }
        TempWinStreakCount = Mode.StorageMahjongScrew.WinSteakCountDict[(int)ModeType];

        if (!Mode.StorageMahjongScrew.LoseSteakCountDict.ContainsKey((int)ModeType))
        {
            Mode.StorageMahjongScrew.LoseSteakCountDict.Add((int)ModeType, 0);
        }
        TempLoseStreakCount = Mode.StorageMahjongScrew.LoseSteakCountDict[(int)ModeType];

        ResetTempData();
    }

    /// <summary>
    /// 重置临时数据（将一些数据先重置，关卡胜利时再还原，防止利用中途退出数据不刷新）
    /// </summary>
    protected virtual void ResetTempData()
    {
        Mode.StorageMahjongScrew.WinSteakCountDict[(int)ModeType] = 0;
        Mode.StorageMahjongScrew.LoseSteakCountDict[(int)ModeType] = TempLoseStreakCount + 1;
    }

    public virtual void Dispose()
    {

    }

    #region 局内元素相关

    public HashSet<int> visiblePointIndexList = new HashSet<int>();//可视的点下标列表
    private Dictionary<int, PointEntity> pointIndex2PointEntityDict = new();//位置下标 - 点实体
    public void AddPointEntity(int pointIndex, PointEntity pointEntity)
    {
        if (pointIndex2PointEntityDict.ContainsKey(pointIndex))
        {
            CLog.Error($"不能添加重复的点，pointIndex：{pointIndex}");
            return;
        }
        pointIndex2PointEntityDict.Add(pointIndex, pointEntity);
    }
    public PointEntity GetPointEntity(int pointIndex)
    {
        if (pointIndex2PointEntityDict.TryGetValue(pointIndex, out var _pointEntity))
        {
            return _pointEntity;
        }
        return null;
    }
    public List<PointEntity> GetAllPointEntityList()
    {
        return new List<PointEntity>(pointIndex2PointEntityDict.Values);
    }

    private List<ArrowEntity> arrowEntityList_Temp = new List<ArrowEntity>();
    private List<ArrowEntity> arrowEntityList = new List<ArrowEntity>();//所有箭头实体
    public void AddArrowEntity(ArrowEntity arrowEntity)
    {
        if (arrowEntityList.Contains(arrowEntity))
        {
            CLog.Error($"不能添加重复的箭头实体，arrowEntity：{arrowEntity}");
            return;
        }
        arrowEntityList.Add(arrowEntity);
    }
    public void RemoveArrowEntity(ArrowEntity arrowEntity)
    {
        arrowEntityList.Remove(arrowEntity);
    }
    public List<ArrowEntity> GetArrowEntityList()
    {
        return arrowEntityList;
    }

    #endregion 局内元素相关

    public void ReduceHp(int reduce = 1)
    {
        if (Group_HpOrStep == EABTestGroup.Group2)
            return;
        if (Mode.ModInGame.EnableInvincibleMode)
            return;

        CurHp -= reduce;
        Game.GetMod<ModEvent>().Dispatch(new EvtRefreshHp(CurHp));
        Mode.CheckGameFail();
    }

    public void SetHp(int hp)
    {
        CurHp = hp;
        Game.GetMod<ModEvent>().Dispatch(new EvtRefreshHp(CurHp));
    }

    public void ReduceStep(int reduce = 1)
    {
        if (Group_HpOrStep != EABTestGroup.Group2)
            return;
        if (Mode.ModInGame.EnableInvincibleMode)
            return;

        CurStep -= reduce;
        Game.GetMod<ModEvent>().Dispatch(new EvtRefreshStep(CurStep));
        Mode.CheckGameFail();
    }

    public void SetStep(int step)
    {
        CurStep = step;
        Game.GetMod<ModEvent>().Dispatch(new EvtRefreshStep(CurStep));
    }

    public void AddStep(int add = 1)
    {
        CurStep += add;
        Game.GetMod<ModEvent>().Dispatch(new EvtRefreshStep(CurStep));
    }

    public ArrowEntity FindHintArrow()
    {
        ArrowEntity ret = null;
        var canMoveCompleteArrowEntityList = new List<ArrowEntity>();
        foreach (var arrowEntity in arrowEntityList)
        {
            if (arrowEntity.ArrowData.State != EArrowState.Idle)
                continue;
            if (!arrowEntity.ArrowData.CheckCanCompleteMove())
                continue;
            canMoveCompleteArrowEntityList.Add(arrowEntity);
        }
        if (canMoveCompleteArrowEntityList.Count <= 0)
            return ret;
        foreach (var arrowEntity in canMoveCompleteArrowEntityList)
        {
            if (arrowEntity.ArrowData.BeHint)
                continue;
            ret = arrowEntity;
            break;
        }
        if (ret == null)
        {
            ret = canMoveCompleteArrowEntityList.GetRandomValue();
        }
        return ret;
    }

    /// <summary>
    /// 是否有正在移动的箭头
    /// </summary>
    public bool HasInMoveArrow()
    {
        foreach (var arrowEntity in arrowEntityList)
        {
            if (arrowEntity == null)
                continue;
            if (arrowEntity.ArrowData.State == EArrowState.Idle)
                continue;
            return true;
        }
        return false;
    }

    public int GetEnterCount(EInGameModeType modeType, int levelIndex)
    {
        if (!Mode.StorageMahjongScrew.EnterCountDict.TryGetValue((int)modeType, out var _dict))
            return 0;
        if (!_dict.TryGetValue(levelIndex, out var _count))
            return 0;
        return _count;
    }

    private void SaveEnterCount(EInGameModeType modeType, int levelIndex, int addCount = 1)
    {
        if (!Mode.StorageMahjongScrew.EnterCountDict.TryGetValue((int)modeType, out var _dict))
        {
            _dict = new StorageDictionary<int, int>();
            Mode.StorageMahjongScrew.EnterCountDict.Add((int)modeType, _dict);
        }
        if (!_dict.TryGetValue(levelIndex, out var _count))
        {
            _dict.Add(levelIndex, 0);
        }
        _dict[levelIndex] += addCount;
    }

    public void SaveWinSteakCount()
    {
        TempWinStreakCount++;
        Mode.StorageMahjongScrew.WinSteakCountDict[(int)ModeType] = TempWinStreakCount;
        Mode.StorageMahjongScrew.LoseSteakCountDict[(int)ModeType] = 0;
    }

    public void Update(float deltTime)
    {
        Duration += deltTime;
        CheckHintBtn(deltTime);

        arrowEntityList.CopyListNonAlloc(arrowEntityList_Temp);
        foreach (var arrowEntity in arrowEntityList_Temp)
        {
            arrowEntity?.OnUpdate(deltTime);
        }
    }

    #region 引导相关

    public string GuidingId { get; set; }
    public bool IsGuidingCameraZoom;

    public void RegisterGuideInfo()
    {
        var guideMod = Game.GetMod<GuideSys>();
        if (LevelNum == 1 && !guideMod.IsFinished("GUIDE_101"))
        {
            if (this.LevelNum == 1 && arrowEntityList.Count > 1)
            {
                guideMod.RegisterTarget(GuideTargetType.ClickArrowEntity, arrowEntityList[1].Holder.transform);
                GuidingId = "GUIDE_101";
            }

            Mode.CameraInput.CanInput = false;
            guideMod.Trigger(GuideTrigger.LevelStart, LevelNum.ToString());
            return;
        }

        if (LevelNum == 6 && !guideMod.IsFinished("GUIDE_102"))
        {
            var target = new GameObject();
            target.transform.SetParent(Mode.GameRoot.transform);
            target.transform.position = new Vector3(0, 0, 0);
            target.SetLayer(InGameConst.LayerName_InGame_Element);
            guideMod.RegisterTarget(GuideTargetType.ZoomCamera, target.transform);
            GuidingId = "GUIDE_102";
            IsGuidingCameraZoom = true;
            GameUtils.SetEventSystemEnable(false);
            Mode.CameraInput.DisplayGuideZoomAnimation(() =>
            {
                guideMod.Trigger(GuideTrigger.LevelStart, LevelNum.ToString());
                GameUtils.SetEventSystemEnable(true);
                IsGuidingCameraZoom = false;
            });
            return;
        }

        // 触发UI_InGame上的引导
        guideMod.Trigger(GuideTrigger.LevelStart, LevelNum.ToString());
    }

    public void TryFinishArrowGuide()
    {
        var guideMod = Game.GetMod<GuideSys>();
        if (guideMod.IsShowingGuide() && guideMod.GetCurGuideId() == GuidingId)
        {
            Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.ClickArrowEntity);
        }
    }

    public void TryFinishZoomGuide()
    {
        var guideMod = Game.GetMod<GuideSys>();
        if (guideMod.IsShowingGuide() && guideMod.GetCurGuideId() == GuidingId)
        {
            Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.ZoomCamera);
        }
    }

    public void CheckCloseZoomGuide()
    {
        if (GuidingId == "GUIDE_102")
            TryFinishZoomGuide();
    }

    #endregion

    public async UniTask DisplayLineEnterAnim()
    {
        foreach (var arrowEntity in arrowEntityList)
        {
            arrowEntity.DisplayAnimateLine();
        }

        await UniTask.WaitUntil(() => arrowEntityList.All(tt => tt.IsEnterAniOver));
        await CoreUtils.WaitSeconds(0.1f, false);
    }

    #region 提示

    private void CheckHintBtn(float deltTime)
    {
        unSelectValidArrowTime += deltTime;
        if (unSelectValidArrowTime >= InGameConfigUtils.HintCd)
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtShowHintBtn());
            unSelectValidArrowTime = 0;
        }
    }

    public void ResetUnSelectValidArrowTime()
    {
        unSelectValidArrowTime = 0;
    }

    #endregion 提示
}