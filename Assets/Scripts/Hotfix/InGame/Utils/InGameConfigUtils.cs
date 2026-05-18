using System.Collections;
using System.Collections.Generic;
using DragonPlus.Config.InGame;
using UnityEngine;

/// <summary>
/// 局内配置工具类
/// </summary>
public static class InGameConfigUtils
{
    /// <summary>
    /// 获取关卡配置列表（根据某个模式）
    /// </summary>
    public static List<Table_InGame_Level> GetLevelCfgList(EInGameModeType modeType, bool forceGet = true)
    {
        string jsonFileName = string.Empty;
        var abTestGroup = Game.GetMod<ModABTest>().GetABTestGroup(EABTestType.Level_V16);
        switch (abTestGroup)
        {
            case EABTestGroup.Group1:
                jsonFileName = "ingame_level";
                break;
            case EABTestGroup.Group2:
                jsonFileName = "ingame_level_1";
                break;
            case EABTestGroup.Group3:
                jsonFileName = "ingame_level_2";
                break;
            default:
                jsonFileName = "ingame_level";
                break;
        }
        List<Table_InGame_Level> cfgList = null;
        if (forceGet)
        {
            cfgList = Game.GetMod<ModConfig>().GetConfigs<Table_InGame_Level>(jsonFileName, true);
        }
        else
        {
            cfgList = Game.GetMod<ModConfig>().GetConfigs<Table_InGame_Level>();
        }
        cfgList.Sort((x, y) => x.Id.CompareTo(y.Id));
        return cfgList;
    }

    /// <summary>
    /// 提示道具在多少秒后没操作可点击的箭头后出现
    /// </summary>
    private static int hintCd;
    public static int HintCd
    {
        get
        {
            if (hintCd == 0)
            {
                hintCd = Game.GetMod<ModConfig>().GetConstConfig<Table_InGame_Global, int>("hintCd");
            }
            return hintCd;
        }
    }
}
