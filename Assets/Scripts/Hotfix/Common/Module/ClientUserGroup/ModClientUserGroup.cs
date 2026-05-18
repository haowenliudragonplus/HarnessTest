using System.Collections;
using System.Collections.Generic;
using DragonPlus.Config.ClientUserGroup;
using UnityEngine;

/// <summary>
/// 客户端用户分层模块
/// </summary>
public class ModClientUserGroup : ModuleBase
{
    // override void OnInit()
    // {
    //     base.OnInit();
    // }

    public void UpdateClientUserGroup()
    {
        var mappingCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_ClientUserGroup_Mapping>();
        foreach (var cfg in mappingCfgList)
        {
            foreach(var rule in cfg.PlayerRuleList)
            {
                
            }
        }
    }

    // private bool CheckCondition()
    // {

    // }
}

/// <summary>
/// 客户端用户分层规则类型
/// </summary>
public enum EClientUserGroupRuleType
{
    MaxLevel = 1,//最大关卡
    TotalPayment = 2,//总付费
    AveragePayment = 3,//平均付费
    DaysSinceLastPayment = 4,//距离上一次付费的天数
    ActiveDays = 5,//活跃天数
    AveragePassLevel_3 = 6,//3日内平均通关数
    AveragePlayRV_3 = 7,//3日内平均每局RV次数
    PaymentCount = 100,//付费次数
}

/// <summary>
/// 客户端用户分层规则操作符类型
/// </summary>
public enum EClientUserGroupRuleOp
{
    LessThan = 1,//小于
    GreaterThan = 2,//大于
    EqualTo = 3,//等于
    NotEqualTo = 4,//不等于
    LessThanOrEqualTo = 5,//小于等于
    GreaterThanOrEqualTo = 6,//大于等于
}
