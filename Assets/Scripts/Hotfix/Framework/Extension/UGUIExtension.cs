using System.Collections;
using System.Collections.Generic;
using DragonPlus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UGUIExtension
{
    public static void Reset(this RectTransform rt)
    {
        if (rt == null)
            return;
        rt.transform.localPosition = Vector2.zero;
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = Vector3.one;
        rt.rotation = Quaternion.identity;
    }

    /// <summary>
    /// 设置TMP的材质
    /// </summary>
    public static void SetTMPMat(this TextMeshProUGUI tmp, string matSuffix)
    {
        if (tmp == null)
            return;

        string curLanguageName = Game.GetMod<ModLanguage>().CurLanguage;
        curLanguageName = StringUtils.FirstCharToUpper(curLanguageName);
        string matKey = $"LocaleFont_{curLanguageName} SDF {matSuffix}";
        var mat = Game.GetMod<ModAsset>().GetRes<Material>(matKey).GetInstance(tmp.gameObject);
        if (mat == null)
            return;
        tmp.GetComponent<LocalizeTextMeshProUGUI>().SetMaterialSuffix(matSuffix);
    }

    private const int MaxCharCount = 16000; //65535 / 4 = 16383.75
    private const string UpperMaxCharContent = "<color=red>Exception！！！！！\nText content is too long！！！！！</color>";

    /// <summary>
    /// 设置TMP的材质
    /// </summary>
    public static void SetTMPMat(this TextMeshPro tmp, string matSuffix)
    {
        if (tmp == null)
            return;

        string curLanguageName = Game.GetMod<ModLanguage>().CurLanguage;
        curLanguageName = StringUtils.FirstCharToUpper(curLanguageName);
        string matKey = $"LocaleFont_{curLanguageName} SDF {matSuffix}";
        var mat = Game.GetMod<ModAsset>().GetRes<Material>(matKey).GetInstance(tmp.gameObject);
        if (mat == null)
            return;
        tmp.GetComponent<LocalizeTextMeshPro>().SetMaterialSuffix(matSuffix);
        tmp.fontMaterial = mat;
    }

    /// <summary>
    /// 设置Text内容（防止字符顶点数过多报错）
    /// </summary>
    /// 这是一个粗略的估算，并不完全精确，富文本、特殊字符等都可能影响最终顶点数
    public static void SetTextSafely(this Text text, string content)
    {
        if (text == null)
            return;

        if (content != null && content.Length > MaxCharCount)
        {
            string str = UpperMaxCharContent;
            str += "\n";
            str += content.Substring(0, MaxCharCount);
            text.text = str;
        }
        else
        {
            text.text = content;
        }
    }

    /// <summary>
    /// 设置TMP内容（防止字符顶点数过多报错）
    /// </summary>
    /// 这是一个粗略的估算，并不完全精确，富文本、特殊字符等都可能影响最终顶点数
    public static void SetTMPSafely(this TextMeshProUGUI tmp, string content)
    {
        if (tmp == null)
            return;

        if (content != null && content.Length > MaxCharCount)
        {
            string str = UpperMaxCharContent;
            str += "\n";
            str += content.Substring(0, MaxCharCount);
            tmp.text = str;
        }
        else
        {
            tmp.text = content;
        }
    }
}