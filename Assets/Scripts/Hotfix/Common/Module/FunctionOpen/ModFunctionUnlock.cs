using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using Framework;
using TMGame;

/// <summary>
/// 功能解锁类型
/// </summary>
/// ----------在此扩展
public enum EFunctionUnlockType
{
    
}

/// <summary>
/// 功能解锁条件类型
/// </summary>
/// ----------在此扩展
public enum EFunctionUnlockConditionType
{
    GreaterThanOrEqualToXLevel = 1, //大于第x关
    LessThanXLevel = 2, //小于第x关
    RegisterDurationGreaterThanXSec, //注册时长大于x秒
    RegisterDurationLessThanXSec, //注册时长小于x秒
}

/// <summary>
/// 功能解锁模块
/// </summary>
public class ModFunctionUnlock : ModuleBase
{
    /// <summary>
    /// 是否解锁
    /// </summary>
    public bool IsUnlock(EFunctionUnlockType functionUnlockType)
    {
        var config = Game.GetMod<ModConfig>().GetConfig<Table_Common_FunctionUnlock>((int)functionUnlockType);
        if (config == null)
            return false;
        if (config.FunctionUnlockConditionTypeList.Count != config.FunctionUnlockConditionParamList.Count)
        {
            CLog.Error($"配置表中，功能解锁条件类型数量和功能解锁条件参数数量不一致");
            return false;
        }
        if (config.FunctionUnlockConditionTypeList == null || config.FunctionUnlockConditionTypeList.Count == 0)
            return true;
        bool isUnlock = true;
        for (int i = 0; i < config.FunctionUnlockConditionTypeList.Count; i++)
        {
            EFunctionUnlockConditionType conditionType = (EFunctionUnlockConditionType)config.FunctionUnlockConditionTypeList[i];
            var condition = GetFunctionUnlockCondition(conditionType);
            if (condition == null)
            {
                isUnlock = false;
                break;
            }
            string extraConditionParam = (config.FunctionUnlockConditionExtraParamList == null
                                          || config.FunctionUnlockConditionExtraParamList.Count <= 0
                                          || config.FunctionUnlockConditionExtraParamList.Count < i + 1)
                ? string.Empty
                : config.FunctionUnlockConditionExtraParamList[i];
            bool b = condition.IsUnlock(config.FunctionUnlockConditionParamList[i], extraConditionParam);
            if (!b)
            {
                isUnlock = false;
                break;
            }
        }
        return isUnlock;
    }

    /// <summary>
    /// 获取功能解锁条件类型
    /// </summary>
    ///----------在此扩展
    private IFunctionUnlockCondition GetFunctionUnlockCondition(EFunctionUnlockConditionType conditionType)
    {
        IFunctionUnlockCondition condition = null;
        switch (conditionType)
        {
            case EFunctionUnlockConditionType.GreaterThanOrEqualToXLevel:
                condition = new FunctionUnlockCondition_GreaterThanOrEqualToXLevel();
                break;

            case EFunctionUnlockConditionType.LessThanXLevel:
                condition = new FunctionUnlockCondition_LessThanXLevel();
                break;

            case EFunctionUnlockConditionType.RegisterDurationGreaterThanXSec:
                condition = new FunctionUnlockCondition_RegisterDurationGreaterThanXSec();
                break;

            case EFunctionUnlockConditionType.RegisterDurationLessThanXSec:
                condition = new FunctionUnlockCondition_RegisterDurationLessThanXSec();
                break;
        }
        return condition;
    }

    /// <summary>
    /// 检查功能解锁弹窗
    /// </summary>
    /// todo lhw：后续重构弹窗逻辑后再完善
    public async UniTask CheckFunctionOpenPopuops()
    {
        List<Table_Common_FunctionUnlock> checkList = new List<Table_Common_FunctionUnlock>();
        var functionUnlockCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_FunctionUnlock>();
        foreach (var cfg in functionUnlockCfgList)
        {
            if (!cfg.IsShowPopups)
                continue;
            checkList.Add(cfg);
        }
        checkList.Sort((x, y) => y.PopupsOrder.CompareTo(x.PopupsOrder));

        // 添加到弹窗逻辑
    }
}