using GameStorage;
using UnityEngine;

/// <summary>
/// 功能解锁条件——注册时长大于x秒
/// </summary>
public class FunctionUnlockCondition_RegisterDurationGreaterThanXSec : IFunctionUnlockCondition
{
    public bool IsUnlock(int conditionParam, string extraConditionParam)
    {
        return false;
        // var firstLoginTime = Game.GetMod<ModStorage>().GetStorage<StorageClientCommon>().UserData.FirstLoginTime;
        // var ret = curLevelIndex > conditionParam;
        // return ret;
    }
}