using System.Collections.Generic;
using DragonPlus.Config.Common;
using Framework;
using TMGame;

public static class Config_Common
{
    /// <summary>
    /// 获取item配置
    /// </summary>
    private static Dictionary<EItemType, Table_Common_Item> itemCfgDict = new Dictionary<EItemType, Table_Common_Item>();
    public static Table_Common_Item GetItemCfg(EItemType itemType, bool showError = true)
    {
        if (itemCfgDict.TryGetValue(itemType, out var _itemCfg))
            return _itemCfg;
        var cfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_Item>();
        foreach (var cfg in cfgList)
        {
            if (cfg.Id == (int)itemType)
            {
                itemCfgDict.TryAdd((EItemType)cfg.Id, cfg);
                return cfg;
            }
        }
        if (showError)
        {
            CLog.Error($"没有找到配置，itemType：{itemType}");
        }
        return null;
    }

    /// <summary>
    /// 获取
    /// </summary>
    private static Dictionary<int, Table_Common_Popups> popupsCfgDict = new Dictionary<int, Table_Common_Popups>();
    public static Table_Common_Popups GetPopupsCfg(int uiViewId, bool showError = true)
    {
        if (popupsCfgDict.TryGetValue(uiViewId, out var _cfg))
            return _cfg;
        var cfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_Popups>();
        foreach (var cfg in cfgList)
        {
            if (cfg.UiViewId == uiViewId)
            {
                popupsCfgDict.TryAdd(uiViewId, cfg);
                return cfg;
            }
        }
        if (showError)
        {
            CLog.Error($"没有找到配置，uiViewId：{uiViewId}");
        }
        return null;
    }
}