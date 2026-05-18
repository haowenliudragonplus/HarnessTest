using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DragonPlus.Config;
using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using GameStorage;
using Newtonsoft.Json;
using TMGame;
using TMPro;
using UnityEngine;

public class ModLanguage : ModuleBase
{
    private StorageCommon storageCommon;

    public string CurLanguage { get; private set; } //当前语言

    private Dictionary<string, Dictionary<string, string>> languageConfigDict = new Dictionary<string, Dictionary<string, string>>(); //所有语言的配置

    private Dictionary<string, TMP_FontAsset> TMPFontAssetDict = new Dictionary<string, TMP_FontAsset>(); //所有TMP字体配置缓存
    private Dictionary<string, Material> TMPMatDict = new Dictionary<string, Material>(); //TMP材质缓存
    private bool isTMPSettingLoaded;
    private Dictionary<string, TMPSettingParam> TMPSettingDict = new Dictionary<string, TMPSettingParam>(); //所有TMP设置

    public override void OnInit()
    {
        base.OnInit();
        InitAllLanguageConfig();
        Game.GetMod<ModEvent>().Register<EvtLanguageChange>(OnLanguageChange);
    }

    private void OnLanguageChange(EvtLanguageChange evt)
    {
        Game.BackToLogin();
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
        InitCurLanguage();
    }

    /// <summary>
    /// 初始化当前语言
    /// </summary>
    private void InitCurLanguage()
    {
        storageCommon.OrigLocale = GameConfig.OrigLanguage;
        storageCommon.Locale = GameConfig.CurLanguage;
        CurLanguage = GameConfig.CurLanguage;
    }

    /// <summary>
    /// 设置当前语言
    /// </summary>
    public void SetCurLanguage(string language)
    {
        GameConfig.SetLanguage(language);
        CurLanguage = language;
        storageCommon.Locale = language;
    }

    /// <summary>
    /// 获取所有支持的语言类型
    /// </summary>
    public List<ELanguageType> GetSupportLanguageTypeList()
    {
        List<ELanguageType> ret = new List<ELanguageType>();
        foreach (ELanguageType language in Enum.GetValues(typeof(ELanguageType)))
        {
            if (language == ELanguageType.None)
                continue;
            if (!GlobalSetting.Ins.SupportLanguage.HasFlag(language))
                continue;
            ret.Add(language);
        }
        return ret;
    }

    /// <summary>
    /// 初始化所有语言的配置
    /// </summary>
    private void InitAllLanguageConfig()
    {
        Dictionary<string, List<GameConfig_Locale>> languageConfigListDict = GetAllLanguageConfigListDict();
        foreach (var kvp in languageConfigListDict)
        {
            var languageAbbr = kvp.Key;
            if (!languageConfigDict.TryGetValue(languageAbbr, out var _configDict))
            {
                _configDict = new Dictionary<string, string>();
                languageConfigDict.Add(languageAbbr, _configDict);
            }
            foreach (var config in kvp.Value)
            {
                if (string.IsNullOrEmpty(config.key)
                    || string.IsNullOrEmpty(config.value))
                    continue;
                if (_configDict.ContainsKey(config.key))
                    continue;
                _configDict.Add(config.key, config.value);
            }
        }
    }

    public Dictionary<string, List<GameConfig_Locale>> GetAllLanguageConfigListDict()
    {
        Dictionary<string, List<GameConfig_Locale>> ret = new Dictionary<string, List<GameConfig_Locale>>();
        foreach (var languageType in GetSupportLanguageTypeList())
        {
            var languageAbbr = languageType.ToDes();
            if (!ret.TryGetValue(languageAbbr, out var _configList))
            {
                _configList = new List<GameConfig_Locale>();
                ret.Add(languageAbbr, _configList);
            }
            var configName = $"locale_{languageAbbr}";
            var ta = Game.GetMod<ModAsset>().GetRes<TextAsset>(configName).GetInstance(Game.DontDestoryRoot);
            if (ta == null)
                continue;
            var configList = JsonConvert.DeserializeObject<List<GameConfig_Locale>>(ta.text);
            if (configList == null || configList.Count <= 0)
                continue;
            foreach (var config in configList)
            {
                _configList.Add(config);
            }
        }
        return ret;
    }

    /// <summary>
    /// 获取语言本地化值
    /// </summary>
    public string GetLocalization(string languageAbbr, string languageKey, params object[] args)
    {
        try
        {
            languageKey = languageKey.Trim();
            if (string.IsNullOrEmpty(languageKey))
                return string.Empty;
            if (languageKey.StartsWith("&key", StringComparison.InvariantCulture))
                languageKey = languageKey.Substring(5);
            if (!languageConfigDict.TryGetValue(languageAbbr, out var _configDict))
                return string.Empty;
            _configDict.TryGetValue(languageKey, out var _languageValue);
            if (string.IsNullOrEmpty(_languageValue))
                return $"<color=red>NotFound</color>:{languageKey}";
            var ret = string.Empty;
            if (args.Length > 0)
            {
                ret = string.Format(_languageValue, args);
            }
            else
            {
                ret = _languageValue;
            }
            return ret;
        }
        catch (Exception e)
        {
            CLog.Error($"获取本地化语言值错误，languageKey：{languageKey}");
            throw;
        }
    }

    /// <summary>
    /// 获取TMP字体配置
    /// </summary>
    public TMP_FontAsset GetTMPFontAsset(string languageAbbr)
    {
        string fontAssetName = $"LocaleFont_{StringUtils.FirstCharToUpper(languageAbbr)} SDF";
        TMP_FontAsset fontAsset = null;
        if (TMPFontAssetDict.TryGetValue(fontAssetName, out fontAsset))
        {
            return fontAsset;
        }
        else
        {
            fontAsset = Game.GetMod<ModAsset>().GetRes<TMP_FontAsset>(fontAssetName).GetInstance(Game.DontDestoryRoot);
            if (fontAsset != null)
            {
                TMPFontAssetDict.Add(fontAssetName, fontAsset);
            }
            return fontAsset;
        }
    }

    /// <summary>
    /// 获取TMP材质
    /// </summary>
    public Material GetTMPMat(string languageAbbr, string suffix)
    {
        string matName = $"LocaleFont_{StringUtils.FirstCharToUpper(languageAbbr)} SDF {suffix}";
        Material mat = null;
        if (TMPMatDict.TryGetValue(matName, out mat))
        {
            return mat;
        }
        else
        {
            mat = Game.GetMod<ModAsset>().GetRes<Material>(matName).GetInstance(Game.DontDestoryRoot);
            if (mat != null)
            {
                TMPMatDict.Add(matName, mat);
            }
            return mat;
        }
    }

    #region TMP设置

    /// <summary>
    /// 加载所有TMP设置
    /// </summary>
    private void InitAllTMPSetting()
    {
        if (isTMPSettingLoaded)
            return;

        TMPSettingDict.Clear();
        List<TMPSettingParam> paramList = new List<TMPSettingParam>();
        foreach (var languageType in GetSupportLanguageTypeList())
        {
            string key = $"TMP Settings {StringUtils.FirstCharToUpper(languageType.ToDes())}";
            TMPSettings tmpSetting = Game.GetMod<ModAsset>().GetRes<TMPSettings>(key).GetInstance(Game.DontDestoryRoot);
            if (tmpSetting && tmpSetting.List != null)
                paramList.AddRange(tmpSetting.List);
        }

        for (int i = 0; i < paramList.Count; i++)
        {
            TMPSettingParam param = paramList[i];
            TMPSettingDict.TryAdd(param.Name, param);
        }
        isTMPSettingLoaded = true;
    }

    /// <summary>
    /// 获取TMP设置
    /// </summary>
    public TMPSettingParam GetTMPSetting(string suffix)
    {
        InitAllTMPSetting();
        string matName = $"LocaleFont_{CurLanguage} SDF {suffix}";
        TMPSettingParam findedParam;
        if (TMPSettingDict.ContainsKey(matName))
            findedParam = TMPSettingDict[matName];
        else
        {
            findedParam = new TMPSettingParam();
        }
        return findedParam;
    }

    #endregion TMP设置
}

public static class LanguageNameAttributeExtension
{
    public static string ToLanguageName(this Enum e)
    {
        FieldInfo fi = e.GetType().GetField(e.ToString());
        LanguageNameAttribute attribute = Attribute.GetCustomAttribute(fi, typeof(LanguageNameAttribute)) as LanguageNameAttribute;
        return attribute == null
            ? e.ToString()
            : attribute.name;
    }
}