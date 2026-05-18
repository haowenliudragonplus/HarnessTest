using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Config;
using DragonPlus.Config;
using DragonPlus.ConfigHub.Ad;
using DragonPlus.ConfigHub.IAP;
using DragonPlus.Core;
using Newtonsoft.Json;
using TMGame;
using UnityEngine;

public class ModConfig : ModuleBase
{
    private Dictionary<Type, List<ConfigBase>> configListCache = new Dictionary<Type, List<ConfigBase>>(); //配置列表缓存
    private Dictionary<Type, Dictionary<int, ConfigBase>> configCache = new Dictionary<Type, Dictionary<int, ConfigBase>>(); //单个配置缓存

    private HashSet<string> censoredWordList = new HashSet<string>(); //敏感词列表

    /// <summary>
    /// 读取常量配置表
    /// </summary>
    public FieldType GetConstConfig<ConfigType, FieldType>(string cfgKey)
        where ConfigType : ConfigBase
    {
        try
        {
            var obj = GetConfigs<ConfigType>()[0];
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.ToLower() == cfgKey.ToLower())
                {
                    return (FieldType)prop.GetValue(obj);
                }
            }
            Debug.LogError($"读取{typeof(ConfigType)}表的{typeof(FieldType)}类型的key：{cfgKey}失败");
            return default(FieldType);
        }
        catch (Exception e)
        {
            Debug.LogError($"读取{typeof(ConfigType)}表的{typeof(FieldType)}类型的key：{cfgKey}失败，{e}");
            return default(FieldType);
        }
    }

    /// <summary>
    /// 读取单行配置
    /// </summary>
    public T GetConfig<T>(int id, bool showError = true)
        where T : ConfigBase
    {
        Type type = typeof(T);
        if (!configCache.TryGetValue(type, out var rowDict))
        {
            rowDict = new Dictionary<int, ConfigBase>();
            List<T> configs = GetConfigs<T>();
            if (configs == null)
            {
                if (showError)
                {
                    Debug.LogError($"读取{type}表的id：{id}失败");
                }
                return null;
            }
            foreach (var config in configs)
            {
                if (rowDict.ContainsKey(config.GetId()))
                {
                    Debug.LogError($"{type}表的id：{config.GetId()}重复");
                    continue;
                }
                rowDict.Add(config.GetId(), config);
            }
            configCache.Add(type, rowDict);
        }

        if (rowDict.TryGetValue(id, out var rowCfg))
        {
            return rowCfg as T;
        }
        if (showError)
        {
            Debug.LogError($"读取{type}表的id：{id}失败");
        }
        return null;
    }

    /// <summary>
    /// 读取配置列表
    /// </summary>
    public List<T> GetConfigs<T>(string forceFileName = "", bool forceGet = false)
        where T : ConfigBase
    {
        Type type = typeof(T);
        string fileName = string.IsNullOrEmpty(forceFileName)
            ? typeof(T).Name.Replace("Table_", "").ToLower()
            : forceFileName;
        if (forceGet || !configListCache.TryGetValue(type, out var _configList))
        {
            TextAsset jsonFile = Game.GetMod<ModAsset>().GetRes<TextAsset>(fileName).GetInstance(Game.DontDestoryRoot);
            if (jsonFile == null)
            {
                return null;
            }
            var list = JsonConvert.DeserializeObject<List<T>>(jsonFile.text);
            _configList = list.ToList_BaseType<T, ConfigBase>();
            configListCache[type] = _configList;
        }
        var ret = _configList.ToList_DeriveType<T, ConfigBase>();
        return ret;
    }

    /// <summary>
    /// **********加载分层配置
    /// </summary>
    private void LoadGroupConfig()
    {
        SDK<IConfigHub>.Instance.RegisterConfig<AdConfigManager>();
        SDK<IConfigHub>.Instance.RegisterConfig<IAPConfigManager>();

        SDK<IConfigHub>.Instance.LoadAllConfig();
    }

    private void LoadCensorWordConfig()
    {
        var ta = Game.GetMod<ModAsset>().GetRes<TextAsset>("forbidden").GetInstance(Game.DontDestoryRoot);
        censoredWordList = new HashSet<string>(Regex.Split(ta.text, "\r\n|\r|\n"));
    }

    /// <summary>
    /// 审查是否存在敏感词
    /// </summary>
    public bool CensorText(string text)
    {
        string textToLower = text.ToLower();
        foreach (string censoredWord in censoredWordList)
        {
            if (textToLower.Contains(censoredWord))
                return true;
        }
        return false;
    }

    public override void OnInit()
    {
        base.OnInit();
        // 加载敏感词配置
        LoadCensorWordConfig();
        // 加载分层配置
        LoadGroupConfig();
    }

    public override void OnDispose()
    {
        base.OnDispose();
        configCache?.Clear();
    }
}