using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DragonPlus.Config.InGame;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using UnityEngine;

public class SubFsmState_InGame_Win : FsmStateBase
{
    private InGameModeBase mode;

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);

        mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame)?.Mode;
        if (mode!=null)
        {
            var achievementEvt = new EvtAchievementCompleted
            {
                heartNum = mode.Data.CurHp,
                auxiliaryLines = mode.Data.AuxiliaryCount,
                hintNum = mode.Data.HintCount,
                revivalNum = mode.Data.ReviveCount
            };
            Game.GetMod<ModEvent>().Dispatch(achievementEvt);
        }
       
        Game.GetMod<ModUI>().Close(UIViewName.UIView_InGame_Setting);

        // string key = "";
        // switch ((EIngameDifficultType)mode.Data.LevelCfg.DifficultyType)
        // {
        //     case EIngameDifficultType.Easy:
        //         key = "winRewardCoinNumberSimple";
        //         break;
        //     case EIngameDifficultType.Hard:
        //         key = "winRewardCoinNumberHard";
        //         break;
        //     case EIngameDifficultType.SuperHard:
        //         key = "winRewardCoinNumberSuperHard";
        //         break;
        // }
        // var rewardCnt = Game.GetMod<ModConfig>().GetConstConfig<Table_InGame_Global, int>(key);
        // Game.GetMod<ModBag>().AddItem(EItemType.Coin, rewardCnt, new BIHelper.ItemChangeReasonArgs
        // {
        //     reason = BiEventArrowPuzzle1.Types.ItemChangeReason.LevelWin,
        //     data1 = ((int)EItemType.Coin).ToString(),
        //     data2 = rewardCnt.ToString(),
        //     data3 = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode.Data.LevelNum.ToString()
        // });

        // if (mode.Data.LevelNum >= InGameConst.GuideLevelCount)
        // {
        //     Game.GetMod<ModFly>().AddItem(EItemType.Coin, rewardCnt);
        // }

        mode.CameraInput.FocusToCenter();
        PlayWinWaveAnimation(() =>
        {
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_InGame_Win, new UIView_InGame_Win.EnterParam
            {

            });
        });
    }

    private void PlayWinWaveAnimation(Action onComplete)
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        var allPoints = mode.Data.GetAllPointEntityList();

        if (allPoints.Count == 0)
            return;

        // 计算中心点（网格坐标）
        Vector2 centerGridPos = Vector2.zero;
        foreach (var point in allPoints)
        {
            centerGridPos += InGameUtils.GetUnitVector2(point.PointData.PointIndex);
        }
        centerGridPos /= allPoints.Count;

        // 按距离分组，创建连续的波纹效果
        Dictionary<int, List<PointEntity>> distanceGroups = new Dictionary<int, List<PointEntity>>();

        // 计算每个点的距离并分组
        foreach (var point in allPoints)
        {
            Vector2 gridPos = InGameUtils.GetUnitVector2(point.PointData.PointIndex);
            float distance = Vector2.Distance(gridPos, centerGridPos);
            int distanceKey = Mathf.FloorToInt(distance);

            if (!distanceGroups.TryGetValue(distanceKey, out var _groupList))
            {
                _groupList = new List<PointEntity>();
                distanceGroups[distanceKey] = _groupList;
            }
            _groupList.Add(point);
        }

        // 按距离从小到大排序
        var groupedPoints = distanceGroups.OrderBy(kvp => kvp.Key)
                                          .Select(kvp => kvp.Value)
                                          .ToList();

        // 统计所有会播放动画的点数量
        int totalAnimatingPoints = 0;
        foreach (var group in groupedPoints)
        {
            foreach (var point in group)
            {
                if (point.PointData.Visible)
                {
                    totalAnimatingPoints++;
                }
            }
        }
        if (totalAnimatingPoints == 0)
        {
            onComplete?.Invoke();
            return;
        }

        // 使用计数器跟踪完成的动画数量
        int completedAnimations = 0;

        // 依次播放每一圈的动画，创建波纹扩散效果
        float delay = 0f;
        float delayIncrement = 0.05f; // 波纹扩散间隔，越小越平滑

        for (int i = 0; i < groupedPoints.Count; i++)
        {
            var groupList = groupedPoints[i].ToList();
            InGameUtils.RegisterTimer(delay, onComplete: (v) =>
            {
                foreach (var point in groupList)
                {
                    point.PlayWinAni(() =>
                    {
                        completedAnimations++;
                        if (completedAnimations >= totalAnimatingPoints)
                        {
                            onComplete?.Invoke();
                        }
                    });
                }
            });
            delay += delayIncrement;
        }
    }
}
