using System;
using System.Collections.Generic;
using System.Text;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;

/// <summary>
/// ABTest类型
/// </summary>
public enum EABTestType
{
    HpOrStep = 1,//血量或者步数
    Level = 2,//关卡配置
    Level_V16 = 3,//V16版本的关卡配置
    Step_V14 = 4,//步数
    LongPress = 5,//长按
    
}

/// <summary>
/// ABTest分组类型
/// </summary>
public enum EABTestGroup
{
    Group0 = -1,
    Group1 = 0,
    Group2 = 1,
    Group3 = 2,
    Group4 = 3,
    Group5 = 4,
    Group6 = 5,
    Gruop7 = 6,
    Group8 = 7,
    Group9 = 8,
    Group10 = 9,
    Group11 = 10,
    Group12 = 11,
}

/// <summary>
/// ABTest模块
/// </summary>
public class ModABTest : ModuleBase
{
    private const string FiexedPrefix = "Fixed_";
    public const EABTestGroup DefaultGroup = EABTestGroup.Group0;

    private StorageCommon storageCommon;
    private Dictionary<string, Table_Common_ABTest> abTestKey2ABTestCfgCache = new Dictionary<string, Table_Common_ABTest>();

    public override void OnInit()
    {
        var abTestCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_ABTest>();
        foreach (var abTestCfg in abTestCfgList)
        {
            abTestKey2ABTestCfgCache.TryAdd(abTestCfg.AbTestKey, abTestCfg);
        }
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
        // 通过客户端本地随机设置分组
        SetAllABTestGroupByClient();
        // 请求服务器下发的ABTest分组
        RequestABTestConfig();
    }

    /// <summary>
    /// 请求ABTest分组
    /// </summary>
    public void RequestABTestConfig()
    {
        try
        {
            SDK<IRemoteRequest>.Instance.HandleRequest(new CGetABTestConfig(), (SGetABTestConfig response) =>
            {
                CLog.Info($"获取到服务器ABTest分组配置，获取到的个数：{response.AbtestConfig.Count}");

                var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
                var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
                foreach (var kv in response.AbtestConfig)
                {
                    CLog.Info($"获取到服务器的ABTest分组配置：{kv.Key}，分组：{kv.Value.Group}");
                    if (!abTestKey2ABTestCfgCache.TryGetValue(kv.Key, out var _abTestCfg))
                    {
                        CLog.Error("获取到服务器ABTest分组配置失败，未找到对应的ABTest配置：" + kv.Key);
                        continue;
                    }
                    if (_abTestCfg.ClientGroupPool != null && _abTestCfg.ClientGroupPool.Count > 0)
                        continue;

                    if (_abTestCfg.EnableFixed)
                    {
                        // 固化分组
                        FixedABTestGroup((EABTestType)_abTestCfg.Id, (EABTestGroup)int.Parse(kv.Value.Group));
                    }
                    else
                    {
                        // 非固化，可以重新赋值
                        storageCommon.Abtests[kv.Key] = kv.Value.Group;
                        CLog.Info($"根据服务器返回的ABTest数据添加到非固化分组：{kv.Key}，{kv.Value.Group}");
                    }
                }
            }, (errorCode, errorMsg, response) =>
            {
                //
                CLog.Error("获取到服务器ABTest分组配置失败" + errorMsg);
            });
        }
        catch (Exception e)
        {
            CLog.Error("获取到服务器ABTest分组配置异常，" + e.Message);
        }
    }

    /// <summary>
    /// 获取ABTest分组
    /// </summary>
    public EABTestGroup GetABTestGroup(EABTestType abTestType)
    {
        var abTestCfg = GetABTestCfg(abTestType);
        if (abTestCfg == null)
            return DefaultGroup;

        try
        {
            // 如果确定了具体分组，并且应用到全部用户，则直接返回具体分组
            if (abTestCfg.Finish && abTestCfg.ForceUseGroupForJoin && abTestCfg.ForceUseGroup >= 0)
                return (EABTestGroup)abTestCfg.ForceUseGroup;

            EABTestGroup abTestGroup;
            if (abTestCfg.EnableFixed)
            {
                string fixedKey = $"{FiexedPrefix}{abTestCfg.AbTestKey}";
                if (storageCommon.Abtests.ContainsKey(fixedKey))
                {
                    abTestGroup = (EABTestGroup)(int.Parse(storageCommon.Abtests[fixedKey]));
                }
                else
                {
                    if (abTestCfg.Finish && abTestCfg.ForceUseGroup >= 0)
                    {
                        abTestGroup = (EABTestGroup)abTestCfg.ForceUseGroup;
                    }
                    else
                    {
                        abTestGroup = (EABTestGroup)abTestCfg.DefaultGroup;
                    }
                }
            }
            else
            {
                string serverkey = abTestCfg.AbTestKey;
                if (storageCommon.Abtests.ContainsKey(serverkey))
                {
                    abTestGroup = (EABTestGroup)(int.Parse(storageCommon.Abtests[serverkey]));
                }
                else
                {
                    if (abTestCfg.Finish && abTestCfg.ForceUseGroup >= 0)
                    {
                        abTestGroup = (EABTestGroup)abTestCfg.ForceUseGroup;
                    }
                    else
                    {
                        abTestGroup = (EABTestGroup)abTestCfg.DefaultGroup;
                    }
                }
            }
            return abTestGroup;
        }
        catch (Exception e)
        {
            CLog.Error($"获取ABTest分组异常，{abTestType}");
            return DefaultGroup;
        }
    }

    public Table_Common_ABTest GetABTestCfg(EABTestType abTestType)
    {
        var cfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_ABTest>((int)abTestType);
        if (cfg != null)
            return cfg;
        CLog.Error($"获取不到 [{abTestType}] ABTest类型的配置");
        return null;
    }

    public Table_Common_ABTest GetABTestCfg(string abTestKey)
    {
        if (abTestKey2ABTestCfgCache.TryGetValue(abTestKey, out var _cfg))
            return _cfg;
        CLog.Error($"获取不到 [{abTestKey}] ABTestKey的配置");
        return null;
    }

    #region 固化相关

    /// <summary>
    /// 固化ABTest分组
    /// </summary>
    /// force：是否强制设置
    public void FixedABTestGroup(EABTestType abTestType, EABTestGroup abTestGroup, bool force = false)
    {
        var abTestCfg = GetABTestCfg(abTestType);
        if (abTestCfg == null)
            return;
        if (!abTestCfg.EnableFixed)
            return;

        string serverkey = abTestCfg.AbTestKey;
        string fixedKey = $"{FiexedPrefix}{serverkey}";
        // 强制固化分组，任何条件都不检查
        if (force)
        {
            storageCommon.Abtests[fixedKey] = ((int)abTestGroup).ToString();
            CLog.Info($"强制固化分组，key：{fixedKey}，分组：{(int)abTestGroup}");
            return;
        }

        if (!storageCommon.Abtests.ContainsKey(fixedKey))
        {
            // 不满足参与条件，不设置分组
            if (!CheckFirstAppVersionValid(abTestType))
                return;

            storageCommon.Abtests.Add(fixedKey, ((int)abTestGroup).ToString());
            CLog.Info($"固化分组，key：{fixedKey}，分组：{(int)abTestGroup}");
        }
    }

    /// <summary>
    /// 检查参与分组的最小和最大首次登录app版本号是否符合要求
    /// </summary>
    private bool CheckFirstAppVersionValid(EABTestType abTestType)
    {
        var abTestCfg = GetABTestCfg(abTestType);
        if (abTestCfg == null)
            return false;
        var StorageClientCommon = Game.GetMod<ModStorage>().GetStorage<StorageClientCommon>();
        try
        {
            int firstAppVersionNum = int.Parse(StorageClientCommon.UserData.FirstAppVersion.Replace("v", ""));
            var ret = false;
            if (abTestCfg.MaxFirstAppVersion <= 0)
            {
                ret = firstAppVersionNum >= abTestCfg.MinFirstAppVersion;
            }
            else
            {
                ret = firstAppVersionNum >= abTestCfg.MinFirstAppVersion && firstAppVersionNum <= abTestCfg.MaxFirstAppVersion;
            }
            return ret;
        }
        catch (Exception e)
        {
            CLog.Error("检查参与客户端分组的最小和最大首次登录app版本号异常，" + e);
            return false;
        }
        return false;
    }

    #endregion 固化相关

    #region 客户端分组相关

    /// <summary>
    /// 设置所有通过客户端随机分组的的ABTest
    /// </summary>
    private void SetAllABTestGroupByClient()
    {
        var abTestCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_ABTest>();
        foreach (var cfg in abTestCfgList)
        {
            if (!cfg.EnableFixed)
                continue;
            if (cfg.ClientGroupPool == null || cfg.ClientGroupPool.Count <= 0)
                continue;
            int randomGroupIndex = UnityEngine.Random.Range(0, cfg.ClientGroupPool.Count);
            int randomGroup = cfg.ClientGroupPool[randomGroupIndex];
            FixedABTestGroup((EABTestType)cfg.Id, (EABTestGroup)randomGroup);
        }
    }

    #endregion 客户端分组相关

    public string LogAllABTest()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kvp in storageCommon.Abtests)
        {
            sb.Append(kvp.Key + ":" + kvp.Value + ",");
        }
        return sb.ToString();
    }
}