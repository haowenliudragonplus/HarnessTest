using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonPlus.Save;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 语言类型
/// </summary>
[Flags]
public enum ELanguageType
{
    None = 0,
    [LanguageName("English")]
    [Description("en")]
    English = 1 << 0,
    [LanguageName("Français")]
    [Description("fr")]
    French = 1 << 1,
    [LanguageName("Deutsch")]
    [Description("de")]
    German = 1 << 2,
    [LanguageName("Português")]
    [Description("pt")]
    Portuguese = 1 << 3,
    [LanguageName("Español")]
    [Description("es")]
    Spanish = 1 << 4,
    [LanguageName("Italiano")]
    [Description("it")]
    Italian = 1 << 5,
    [LanguageName("Bahasa Indonesia")]
    [Description("id")]
    Indonesian = 1 << 6,
    [LanguageName("Русский")]
    [Description("ru")]
    Russian = 1 << 7,
    [LanguageName("Tiếng Việt")]
    [Description("vi")]
    Vietnamese = 1 << 8,
    [LanguageName("Türkçe")]
    [Description("tr")]
    Turkish = 1 << 9,
    [LanguageName("ภาษาไทย")]
    [Description("th")]
    Thai = 1 << 10,
    [LanguageName("日本語")]
    [Description("jp")]
    Japanese = 1 << 11,
    [LanguageName("한국어")]
    [Description("kr")]
    Korea = 1 << 12,
    [LanguageName("简体中文")]
    [Description("zh")]
    ChineseSimplified = 1 << 13,
    [LanguageName("繁體中文")]
    [Description("zht")]
    ChineseTradition = 1 << 14,
    [LanguageName("हिन्दी")]
    [Description("hi")]
    Hindi = 1 << 15,
    [LanguageName("Nederlands")]
    [Description("nl")]
    Dutch = 1 << 16,
    [LanguageName("Bahasa Melayu")]
    [Description("ms")]
    Malaysia = 1 << 17,
    [LanguageName("اللغة العربية")]
    [Description("ar")]
    Arabic = 1 << 18,
}

public class GameConfig_Locale
{
    public string key { get; set; }
    public string value { get; set; }
}

public partial class GameConfig
{
    public static string CurLanguage { get; private set; } //当前语言
    public static string OrigLanguage { get; private set; } //初始语言（系统语言）
    private static bool isSetLanguage;

    public const ELanguageType DefaultLanguage = ELanguageType.English; //默认语言

    private static Dictionary<string, string> localizationDict = new Dictionary<string, string>();

    public static async UniTask InitGameConfig_Locale()
    {
        SetCurLanguage();
        await LoadGameConfig_Locale();
    }

    public static string GetLocaleStr(string key, params object[] args)
    {
        if (!localizationDict.TryGetValue(key, out var _value))
            return string.Empty;
        string ret = string.Format(_value, args);
        return ret;
    }

    private static async UniTask LoadGameConfig_Locale()
    {
        string filePath = Path.Combine(GAME_CONFIG_DIR, $"LocaleConfig/locale_loading_{CurLanguage}");
        var ta = await ResourceUtils.LoadResorceAsync<TextAsset>(filePath);
        if (ta == null)
            return;

        List<GameConfig_Locale> gameConfig_LocalizationList = JsonConvert.DeserializeObject<List<GameConfig_Locale>>(ta.text);
        if (gameConfig_LocalizationList == null)
            return;
        foreach (var cfg in gameConfig_LocalizationList)
        {
            if (string.IsNullOrEmpty(cfg.value))
                continue;
            if (localizationDict.ContainsKey(cfg.key))
                continue;
            localizationDict.Add(cfg.key, cfg.value);
        }
    }

    /// <summary>
    /// 设置当前的语言
    /// </summary>
    private static void SetCurLanguage()
    {
        if (isSetLanguage)
            return;

        var storage = SDK<IStorage>.Instance.Get<StorageCommon>();
        OrigLanguage = GetSystemLanguage();
        storage.OrigLocale = OrigLanguage;
        if (string.IsNullOrEmpty(storage.Locale))
        {
            SetLanguage(OrigLanguage);
        }
        else
        {
            if (IsSupport(storage.Locale))
            {
                SetLanguage(storage.Locale);
            }
            else
            {
                SetLanguage(OrigLanguage);
            }
        }
        isSetLanguage = true;
    }

    public static void SetLanguage(string language)
    {
        var languageAbbr = DefaultLanguage.ToDes();
        if (!string.IsNullOrEmpty(language)
            && languageAbbr != ELanguageType.None.ToDes()
            && IsSupport(language))
        {
            languageAbbr = language;
        }
        if (CurLanguage == languageAbbr)
            return;
        CurLanguage = languageAbbr;
        SDK<IStorage>.Instance.Get<StorageCommon>().Locale = languageAbbr;
    }

    /// <summary>
    /// 是否支持某个语言
    /// </summary>
    private static bool IsSupport(string languageAbbr)
    {
        foreach (ELanguageType language in Enum.GetValues(typeof(ELanguageType)))
        {
            var memberInfo = typeof(ELanguageType).GetMember(language.ToString()).FirstOrDefault();
            if (memberInfo == null)
                continue;
            var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if (descriptionAttribute != null && descriptionAttribute.Description == languageAbbr)
            {
                bool isSupport = GlobalSetting.Ins.SupportLanguage.HasFlag(language);
                return isSupport;
            }
        }
        return false;
    }

    public static string GetSystemLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Afrikaans: return "af";

            case SystemLanguage.Arabic: return "ar";

            case SystemLanguage.Basque: return "eu";

            case SystemLanguage.Belarusian: return "be";

            case SystemLanguage.Bulgarian: return "bg";

            case SystemLanguage.Catalan: return "ca";

            case SystemLanguage.Chinese: return "zh";

            case SystemLanguage.ChineseSimplified: return "zh";

            case SystemLanguage.ChineseTraditional: return "zht";

            case SystemLanguage.Czech: return "cs";

            case SystemLanguage.Danish: return "da";

            case SystemLanguage.Dutch: return "nl";

            case SystemLanguage.English: return "en";

            case SystemLanguage.Estonian: return "et";

            case SystemLanguage.Faroese: return "fo";

            case SystemLanguage.Finnish: return "fi";

            case SystemLanguage.French: return "fr";

            case SystemLanguage.German: return "de";

            case SystemLanguage.Greek: return "el";

            case SystemLanguage.Hebrew: return "he";

            case SystemLanguage.Icelandic: return "is";

            case SystemLanguage.Indonesian: return "id";

            case SystemLanguage.Japanese: return "jp";

            case SystemLanguage.Korean: return "kr";

            case SystemLanguage.Latvian: return "lv";

            case SystemLanguage.Lithuanian: return "lt";

            case SystemLanguage.Norwegian: return "no";

            case SystemLanguage.Polish: return "pl";

            case SystemLanguage.Portuguese: return "pt";

            case SystemLanguage.Romanian: return "ro";

            case SystemLanguage.Russian: return "ru";

            case SystemLanguage.SerboCroatian: return "hr";

            case SystemLanguage.Slovak: return "sk";

            case SystemLanguage.Slovenian: return "sl";

            case SystemLanguage.Spanish: return "es";

            case SystemLanguage.Swedish: return "sv";

            case SystemLanguage.Thai: return "th";

            case SystemLanguage.Turkish: return "tr";

            case SystemLanguage.Ukrainian: return "uk";

            case SystemLanguage.Vietnamese: return "vi";

            case SystemLanguage.Hungarian: return "hu";

            case SystemLanguage.Italian: return "it";

            case SystemLanguage.Unknown: return "en";
        }

        return "en";
    }
}

public class LanguageNameAttribute : Attribute
{
    public string name;

    public LanguageNameAttribute(string name)
    {
        this.name = name;
    }
}