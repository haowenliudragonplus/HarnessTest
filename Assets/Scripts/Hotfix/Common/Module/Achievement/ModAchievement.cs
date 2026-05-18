using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonPlus.Core;
using Framework;
using GameStorage;
using System.Linq;
using DragonPlus.Config.Common;
using DragonU3DSDK.Network.API.Protocol;

/// <summary>
/// 成就类型枚举
/// </summary>
public enum EAchievementType
{
    /// <summary>
    /// 在主线关卡完成{0}关
    /// </summary>
    CompleteMainLevel = 1,

    /// <summary>
    /// 挑战{0}天
    /// </summary>
    ChallengeDays = 2,

    /// <summary>
    /// 连续登陆{0}天并完成任意1次关卡
    /// </summary>
    ConsecutiveLoginAndComplete = 3,

    /// <summary>
    /// 满星，无复活完成{0}关卡
    /// </summary>
    CompleteWithFullStarsNoRevive = 4,

    /// <summary>
    /// 不使用提示的情况下通过{0}关
    /// </summary>
    CompleteWithoutHints = 5,

    /// <summary>
    /// 在任意关卡复活{0}次
    /// </summary>
    ReviveInAnyLevel = 6,

    /// <summary>
    /// 没有复活状态下，只剩下一颗心通过关卡{0}次
    /// </summary>
    CompleteWithOneHeartNoRevive = 7,

    /// <summary>
    /// 全程不使用任何道具通过{0}关
    /// </summary>
    CompleteWithoutItems = 8,

    /// <summary>
    /// 一天内连续挑战{0}关
    /// </summary>
    ConsecutiveChallengeInOneDay = 9,
}

public class ModAchievement : ModuleBase
{
    public const string Atlas_Achievement = "Atlas_Achievement";
    private StorageAchievement storageAchievement;
    private List<Table_Common_Achievement> achievements;
    public bool IsAddAchievement = false;
    /// <summary>
    /// 按类型分类的成就字典：Key=成就类型，Value=该类型下的成就列表（按Id排序）
    /// </summary>
    private Dictionary<EAchievementType, List<Table_Common_Achievement>> achievementsByType;
    private List<EAchievementType> unlockedTypes;

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        RegisterEvent<EvtAchievementCompleted>(EvtAchievementCompleted);
        RegisterEvent<EvtAchievementRevival>(EvtAchievementRevival);
        storageAchievement = Game.GetMod<ModStorage>().GetStorage<StorageInGame>().Achievement;
        achievements = Game.GetMod<ModConfig>().GetConfigs<Table_Common_Achievement>();
        // 将成就按类型分类存储
        CategorizeAchievements();
        RefreshAchievements();
        IsAddAchievement = false;
    }

    public void RefreshAchievements()
    {
        if (TimeUtils.GetDayIntervalByTimeStamp(storageAchievement.CheckinTime,TimeUtils.GetServerTimeStamp())!= 1
            &&TimeUtils.GetDayIntervalByTimeStamp(storageAchievement.CheckinTime,TimeUtils.GetServerTimeStamp())!= 0
            &&storageAchievement.CheckinTime !=0)
        {
            //中断了，重置进度
            storageAchievement.CheckinTime = 0;
            storageAchievement.DailyAttendanceCheckin = 0;
        }
        CLog.Info($"Achievement全勤打卡 {storageAchievement.CheckinTime} {TimeUtils.GetServerTimeStamp()}");
        if (!TimeUtils.IsSameDay(TimeUtils.GetServerTimeStamp(), storageAchievement.WinStreakExpertTime))
        {
            // 新的一天，重置进度
            storageAchievement.WinStreakExpert = 0;
            storageAchievement.WinStreakExpertTime = TimeUtils.GetServerTimeStamp();
        }
        CLog.Info($"Achievement每日挑战 {storageAchievement.WinStreakExpertTime} {TimeUtils.GetServerTimeStamp()}");
        if (!storageAchievement.FirstOpen)
        {
            storageAchievement.FirstOpen = true;
            int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
            achievementsByType.TryGetValue(EAchievementType.CompleteMainLevel, out var achievementList);
            if (achievementList == null)
                return;
            storageAchievement.PassingExpert = levelIndex;
            storageAchievement.PassingExpert = CheckAndClaimAchievements(achievementList,
                EAchievementType.CompleteMainLevel,
                storageAchievement.PassingExpert);
        }
    }

    /// <summary>
    /// 将成就按类型分类存储到字典中
    /// </summary>
    private void CategorizeAchievements()
    {
        achievementsByType = new Dictionary<EAchievementType, List<Table_Common_Achievement>>();
        unlockedTypes = new List<EAchievementType>();
        // 遍历所有成就，分配到对应的类型
        foreach (var achievement in achievements)
        {
            EAchievementType type = (EAchievementType)achievement.Type;

            if (!achievementsByType.ContainsKey(type))
            {
                achievementsByType[type] = new List<Table_Common_Achievement>();
            }
            if (!unlockedTypes.Contains(type))
            {
                unlockedTypes.Add(type);
            }
            achievementsByType[type].Add(achievement);
        }

        // 对每个类型的成就列表按Id排序
        foreach (var type in achievementsByType.Keys.ToList())
        {
            achievementsByType[type] = achievementsByType[type]
                .OrderBy(a => a.Id)
                .ToList();
        }
        unlockedTypes = unlockedTypes
            .OrderBy(type => achievementsByType[type][0].Order)
            .ToList();
        CLog.Info(
            $"[ModAchievement] Categorized {achievements.Count} achievements into {achievementsByType.Count} types");
    }

    private void EvtAchievementCompleted(EvtAchievementCompleted evt)
    {
        if (IsAchievementTypeUnlocked(EAchievementType.CompleteMainLevel))
            OnMainLevelComplete();

        if (IsAchievementTypeUnlocked(EAchievementType.ChallengeDays))
            OnDailyChallenge();

        if (IsAchievementTypeUnlocked(EAchievementType.ConsecutiveLoginAndComplete))
            OnConsecutiveLoginAndComplete();

        if (IsAchievementTypeUnlocked(EAchievementType.CompleteWithFullStarsNoRevive))
            OnFullStarsNoRevive(evt.heartNum == 3, evt.revivalNum == 0);

        if (IsAchievementTypeUnlocked(EAchievementType.CompleteWithoutHints))
            OnCompleteWithoutHints(evt.hintNum == 0);

        if (IsAchievementTypeUnlocked(EAchievementType.CompleteWithOneHeartNoRevive))
            OnOneHeartNoRevive(evt.heartNum == 1, evt.revivalNum == 0);

        if (IsAchievementTypeUnlocked(EAchievementType.CompleteWithoutItems))
            OnCompleteWithoutItems(evt is { hintNum: 0, auxiliaryLines: 0 });

        if (IsAchievementTypeUnlocked(EAchievementType.ConsecutiveChallengeInOneDay))
            OnConsecutiveChallengeInOneDay();
    }

    private bool IsAchievementTypeUnlocked(EAchievementType type)
    {
        if (!achievementsByType.TryGetValue(type, out var achievementList))
            return false;

        if (achievementList == null || achievementList.Count == 0)
            return false;
        int currentLevel = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main)+1;
        return currentLevel >= achievementList[0].UnlockLevel;
    }

    private void EvtAchievementRevival(EvtAchievementRevival evt)
    {
        if (IsAchievementTypeUnlocked(EAchievementType.ReviveInAnyLevel))
            OnReviveInAnyLevel();
    }

    /// <summary>
    /// 主线关卡完成回调
    /// </summary>
    private void OnMainLevelComplete()
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.CompleteMainLevel, out var achievementList))
        {
            CLog.Warning("[ModAchievement] Storage or achievements not initialized!");
            return;
        }

        if (achievementList.Count == 0)
        {
            return;
        }

        // 进度+1
        storageAchievement.PassingExpert += 1;
        // 检查是否完成任何一个成就
        storageAchievement.PassingExpert = CheckAndClaimAchievements(achievementList,
            EAchievementType.CompleteMainLevel,
            storageAchievement.PassingExpert);
    }

    /// <summary>
    /// 挑战天数：每次登录并完成1次关卡，进度+1（不统计是否间断）
    /// </summary>
    private void OnDailyChallenge()
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.ChallengeDays, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;
        if (!TimeUtils.IsSameDay(TimeUtils.GetServerTimeStamp(), storageAchievement.DailyChallengeTime))
        {
            storageAchievement.DailyChallenge += 1;
            storageAchievement.DailyChallengeTime = TimeUtils.GetServerTimeStamp();
            storageAchievement.DailyChallenge = CheckAndClaimAchievements(achievementList,
                EAchievementType.ChallengeDays, storageAchievement.DailyChallenge);
        }
    }

    /// <summary>
    /// 连续登录并完成关卡：不间断连续登录，并且每天至少完成1次关卡，进度+1
    /// </summary>
    private void OnConsecutiveLoginAndComplete()
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.ConsecutiveLoginAndComplete, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;
        if (TimeUtils.GetDayIntervalByTimeStamp(storageAchievement.CheckinTime,TimeUtils.GetServerTimeStamp()) == 0)
        {
            return;
        }

        if (TimeUtils.GetDayIntervalByTimeStamp(storageAchievement.CheckinTime,TimeUtils.GetServerTimeStamp()) == 1 ||
            storageAchievement.CheckinTime == 0)
        {
            storageAchievement.DailyAttendanceCheckin += 1;
            storageAchievement.CheckinTime = TimeUtils.GetServerTimeStamp();
            storageAchievement.DailyAttendanceCheckin = CheckAndClaimAchievements(achievementList,
                EAchievementType.ConsecutiveLoginAndComplete,
                storageAchievement.DailyAttendanceCheckin);
        }
        else
        {
            // 中断了，重置进度
            storageAchievement.DailyAttendanceCheckin = 1;
            storageAchievement.CheckinTime = TimeUtils.GetServerTimeStamp();
        }
    }

    /// <summary>
    /// 满星无复活完成关卡：在主线关卡，满血并且没有复活时完成关卡，进度+1
    /// </summary>
    /// <param name="isFullHp">是否满血</param>
    /// <param name="noRevive">是否没有复活</param>
    private void OnFullStarsNoRevive(bool isFullHp, bool noRevive)
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.CompleteWithFullStarsNoRevive, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        if (isFullHp && noRevive)
        {
            storageAchievement.PerfectClear += 1;
            storageAchievement.PerfectClear = CheckAndClaimAchievements(achievementList,
                EAchievementType.CompleteWithFullStarsNoRevive,
                storageAchievement.PerfectClear);
        }
    }

    /// <summary>
    /// 不使用提示通过关卡：在主线关卡，不使用提示的情况下通过1次，进度+1
    /// </summary>
    /// <param name="noHintUsed">是否没有使用提示</param>
    private void OnCompleteWithoutHints(bool noHintUsed)
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.CompleteWithoutHints, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        if (noHintUsed)
        {
            storageAchievement.LevelLegend += 1;
            CheckAndClaimAchievements(achievementList, EAchievementType.CompleteWithoutHints,
                storageAchievement.LevelLegend);
        }
    }

    /// <summary>
    /// 任意关卡复活：在主线关卡，任意关卡复活1次，进度+1
    /// </summary>
    private void OnReviveInAnyLevel()
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.ReviveInAnyLevel, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        storageAchievement.ComebackPlayer += 1;
        storageAchievement.ComebackPlayer = CheckAndClaimAchievements(achievementList,
            EAchievementType.ReviveInAnyLevel,
            storageAchievement.ComebackPlayer);
    }

    /// <summary>
    /// 一颗心无复活通关：在主线关卡，没有复活，只剩一颗心通关1次，进度+1
    /// </summary>
    /// <param name="oneHeartLeft">是否只剩一颗心</param>
    /// <param name="noRevive">是否没有复活</param>
    private void OnOneHeartNoRevive(bool oneHeartLeft, bool noRevive)
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.CompleteWithOneHeartNoRevive, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        if (oneHeartLeft && noRevive)
        {
            storageAchievement.HeartPoundingMoment += 1;
            storageAchievement.HeartPoundingMoment = CheckAndClaimAchievements(achievementList,
                EAchievementType.CompleteWithOneHeartNoRevive,
                storageAchievement.HeartPoundingMoment);
        }
    }

    /// <summary>
    /// 不使用道具通过关卡：在主线关卡，全程不使用提示和辅助线通过1次，进度+1
    /// </summary>
    /// <param name="noItemsUsed">是否没有使用道具</param>
    private void OnCompleteWithoutItems(bool noItemsUsed)
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.CompleteWithoutItems, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        if (noItemsUsed)
        {
            storageAchievement.UltimateMaster += 1;
            storageAchievement.UltimateMaster = CheckAndClaimAchievements(achievementList,
                EAchievementType.CompleteWithoutItems,
                storageAchievement.UltimateMaster);
        }
    }

    /// <summary>
    /// 一天内连续挑战关卡：一天内，完成X关卡，进度+1
    /// </summary>
    private void OnConsecutiveChallengeInOneDay()
    {
        if (storageAchievement == null ||
            !achievementsByType.TryGetValue(EAchievementType.ConsecutiveChallengeInOneDay, out var achievementList))
            return;

        if (achievementList.Count == 0)
            return;

        if (TimeUtils.IsSameDay(TimeUtils.GetServerTimeStamp(), storageAchievement.WinStreakExpertTime))
        {
            storageAchievement.WinStreakExpert += 1;
            storageAchievement.WinStreakExpert = CheckAndClaimAchievements(achievementList,
                EAchievementType.ConsecutiveChallengeInOneDay,
                storageAchievement.WinStreakExpert);
        }
        else
        {
            // 新的一天，重置进度
            storageAchievement.WinStreakExpert = 1;
            storageAchievement.WinStreakExpertTime = TimeUtils.GetServerTimeStamp();
        }
    }

    /// <summary>
    /// 通用方法：检查并领取成就
    /// </summary>
    /// <param name="achievementList">成就列表</param>
    /// <param name="type">成就类型</param>
    /// <param name="currentProgress">当前进度</param>
    private int CheckAndClaimAchievements(List<Table_Common_Achievement> achievementList, EAchievementType type,
        int currentProgress)
    {
        foreach (var achievement in achievementList)
        {
            // 如果该成就还未领取，且当前进度已达到要求
            if (!storageAchievement.AchievementClaimable.ContainsKey(achievement.Id) &&
                !storageAchievement.CollectedAchievement.ContainsKey(achievement.Id))
            {
                if (currentProgress >= achievement.Count)
                {
                    // 标记成就为可领取（记录完成时间戳）
                    storageAchievement.AchievementClaimable.Add(achievement.Id, TimeUtils.GetServerTimeStamp());
                    currentProgress -= achievement.Count;
                    int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1;
                    BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventAchievementComplete,
                        levelIndex.ToString(), achievement.Id.ToString());
                    CLog.Info(
                        $"[ModAchievement] Achievement claimable: ID={achievement.Id}, Name={achievement.Name}, Type={type}, Progress={currentProgress}/{achievement.Count}");
                    Game.GetMod<ModTip>().ShowTip(achievement.Id.ToString(), ETipType.Achievement, ETipPosType.Top);
                }
                else
                {
                    break;
                }
            }
        }

        Game.GetMod<ModEvent>().Dispatch(new EvtAchievementRefreshUI());
        return currentProgress;
    }


    /// <summary>
    /// 领取成就
    /// </summary>
    /// <param name="achievementId">成就ID</param>
    /// <returns>是否领取成功</returns>
    public bool ClaimAchievement(int achievementId)
    {
        if (storageAchievement == null)
        {
            CLog.Warning("[ModAchievement] Storage not initialized!");
            return false;
        }

        // 检查是否在可领取列表内
        if (!storageAchievement.AchievementClaimable.ContainsKey(achievementId))
        {
            CLog.Warning($"[ModAchievement] Achievement {achievementId} is not claimable!");
            return false;
        }

        // 获取成就配置信息
        var achievement = achievements.FirstOrDefault(a => a.Id == achievementId);
        if (achievement == null)
        {
            CLog.Warning($"[ModAchievement] Achievement config {achievementId} not found!");
            return false;
        }

        // 存入已领取完成列表（记录领取时间戳）
        storageAchievement.CollectedAchievement.Add(achievementId,
            storageAchievement.AchievementClaimable[achievementId]);
        // 从可领取列表移除
        storageAchievement.AchievementClaimable.Remove(achievementId);
        int levelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main) + 1;
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventAchievementClaim, levelIndex.ToString(),
            achievementId.ToString());
        CLog.Info($"[ModAchievement] Achievement claimed successfully: ID={achievementId}, Name={achievement.Name}");
        IsAddAchievement = true;
        Game.GetMod<ModEvent>().Dispatch(new EvtAchievementRefreshUI());
        return true;
    }

    /// <summary>
    /// 获取展示成就列表
    /// 规则：
    /// 1. 每个类型返回一个成就ID
    /// 2. 如果一个类型的所有成就都已完成，返回该类型ID最大的成就
    /// 3. 如果没有都完成，优先返回可领取的成就
    /// 4. 没有可领取的，返回最接近未完成的成就
    /// </summary>
    /// <returns>展示成就ID列表</returns>
    public List<int> GetDisplayAchievementList()
    {
        if (storageAchievement == null || achievementsByType == null)
        {
            CLog.Warning("[ModAchievement] Storage or achievements not initialized!");
            return new List<int>();
        }

        var displayList = new List<int>();
        var allCompletedTypes = new List<int>();
        var allCollectedIds = new HashSet<int>(storageAchievement.CollectedAchievement.Keys);
        // 遍历字典中的每个类型
        foreach (var kvp in unlockedTypes)
        {
            var type = kvp;
            var achievementList = achievementsByType[kvp];

            if (achievementList == null || achievementList.Count == 0)
                continue;
            if (!IsAchievementTypeUnlocked(type))
                continue;
            // 检查该类型是否所有成就都已完成
            bool allCompleted = true;
            foreach (var achievement in achievementList)
            {
                if (!allCollectedIds.Contains(achievement.Id))
                {
                    allCompleted = false;
                    break;
                }
            }

            int? selectedId = null;

            if (allCompleted)
            {
                allCompletedTypes.Add(achievementList.Max(a => a.Id));
            }
            else
            {
                // 没有都完成，优先查找可领取的成就
                foreach (var achievement in achievementList)
                {
                    if (storageAchievement.AchievementClaimable.ContainsKey(achievement.Id))
                    {
                        selectedId = achievement.Id;
                        break;
                    }
                }

                if (!selectedId.HasValue)
                {
                    // 没有可领取的，找最接近未完成的成就
                    selectedId = GetClosestUncompletedAchievement(achievementList, type);
                }
            }

            if (selectedId.HasValue)
            {
                displayList.Add(selectedId.Value);
            }
        }
        displayList.AddRange(allCompletedTypes);
        CLog.Info(
            $"[ModAchievement] Display list: {displayList.Count} achievements (including {allCollectedIds.Count} collected)");
        return displayList;
    }

    /// <summary>
    /// 获取下一个未完成的成就（成就列表已按ID排序，返回第一个未完成的）
    /// </summary>
    /// <param name="achievementList">成就列表</param>
    /// <param name="type">成就类型</param>
    /// <returns>成就ID，如果没有则返回null</returns>
    private int? GetClosestUncompletedAchievement(List<Table_Common_Achievement> achievementList, EAchievementType type)
    {
        if (achievementList == null || achievementList.Count == 0)
            return null;

        // 成就已按ID排序，直接返回第一个未完成的成就
        foreach (var achievement in achievementList)
        {
            if (!storageAchievement.CollectedAchievement.ContainsKey(achievement.Id))
            {
                return achievement.Id;
            }
        }

        return null;
    }


    /// <summary>
    /// 设置成就类型进度并尝试领取成就
    /// </summary>
    /// <param name="type">成就类型</param>
    /// <param name="progress">要设置的进度值</param>
    /// <returns>更新后的进度值</returns>
    public void SetAchievementTypeProgress(int type, int progress)
    {
        EAchievementType typeEnum = (EAchievementType)type;
        if (storageAchievement == null || !achievementsByType.TryGetValue(typeEnum, out var achievementList))
        {
            CLog.Warning($"[ModAchievement] Storage not initialized or achievements type {typeEnum} not found!");
            return;
        }

        if (achievementList.Count == 0)
        {
            return;
        }

        // 根据成就类型更新对应的进度值
        switch (typeEnum)
        {
            case EAchievementType.CompleteMainLevel:
                storageAchievement.PassingExpert = progress;
                storageAchievement.PassingExpert =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.PassingExpert);
                break;
            case EAchievementType.ChallengeDays:
                storageAchievement.DailyChallenge = progress;
                storageAchievement.DailyChallenge =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.DailyChallenge);
                break;
            case EAchievementType.ConsecutiveLoginAndComplete:
                storageAchievement.DailyAttendanceCheckin = progress;
                storageAchievement.DailyAttendanceCheckin = CheckAndClaimAchievements(achievementList, typeEnum,
                    storageAchievement.DailyAttendanceCheckin);
                break;
            case EAchievementType.CompleteWithFullStarsNoRevive:
                storageAchievement.PerfectClear = progress;
                storageAchievement.PerfectClear =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.PerfectClear);
                break;
            case EAchievementType.CompleteWithoutHints:
                storageAchievement.LevelLegend = progress;
                storageAchievement.LevelLegend =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.LevelLegend);
                break;
            case EAchievementType.ReviveInAnyLevel:
                storageAchievement.ComebackPlayer = progress;
                storageAchievement.ComebackPlayer =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.ComebackPlayer);
                break;
            case EAchievementType.CompleteWithOneHeartNoRevive:
                storageAchievement.HeartPoundingMoment = progress;
                storageAchievement.HeartPoundingMoment = CheckAndClaimAchievements(achievementList, typeEnum,
                    storageAchievement.HeartPoundingMoment);
                break;
            case EAchievementType.CompleteWithoutItems:
                storageAchievement.UltimateMaster = progress;
                storageAchievement.UltimateMaster =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.UltimateMaster);
                break;
            case EAchievementType.ConsecutiveChallengeInOneDay:
                storageAchievement.WinStreakExpert = progress;
                storageAchievement.WinStreakExpert =
                    CheckAndClaimAchievements(achievementList, typeEnum, storageAchievement.WinStreakExpert);
                break;
            default:
                CLog.Error($"[ModAchievement] Invalid achievement type: {typeEnum}");
                break;
        }
    }

    public string GetCollectedAchievementTime(int achievementId)
    {
        if (storageAchievement.CollectedAchievement.ContainsKey(achievementId))
        {
            return TimeUtils.FormatDateTime(storageAchievement.CollectedAchievement[achievementId], "yyyy/MM/dd");
        }

        return "";
    }

    public Table_Common_Achievement GetAchievementById(int achievementId)
    {
        return achievements.FirstOrDefault(achievement => achievement.Id == achievementId);
    }

    public List<int> GetCollectedAchievement()
    {
        var list = storageAchievement.CollectedAchievement
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        return list;
    }

    public int GetRedAchievement()
    {
        return storageAchievement.AchievementClaimable.Count;
    }

    public long GetAchievementClaimableTime(int achievementId)
    {
        if (storageAchievement.AchievementClaimable.ContainsKey(achievementId))
        {
            return storageAchievement.AchievementClaimable[achievementId];
        }

        return 0;
    }

    public int GetCurAchievementCount(int type)
    {
        switch (type)
        {
            case 1:
                return storageAchievement.PassingExpert;
            case 2:
                return storageAchievement.DailyChallenge;
            case 3:
                return storageAchievement.DailyAttendanceCheckin;
            case 4:
                return storageAchievement.PerfectClear;
            case 5:
                return storageAchievement.LevelLegend;
            case 6:
                return storageAchievement.ComebackPlayer;
            case 7:
                return storageAchievement.HeartPoundingMoment;
            case 8:
                return storageAchievement.UltimateMaster;
            case 9:
                return storageAchievement.WinStreakExpert;
            default:
                CLog.Error($"[ModAchievement] Invalid achievement type: {type}");
                return 0;
        }
    }
}