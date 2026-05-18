/// <summary>
/// 功能解锁条件——大于等于第x关
/// </summary>
public class FunctionUnlockCondition_GreaterThanOrEqualToXLevel : IFunctionUnlockCondition
{
    public bool IsUnlock(int conditionParam, string extraConditionParam)
    {
        //todo logic
        // var curLevelNum = Game.GetMod<ModMahjong>().GetLevelIndex(EMahjongModeType.Main) + 1;
        // var ret = curLevelNum >= conditionParam;
        // return ret;
        return false;
    }
}