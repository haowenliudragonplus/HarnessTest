#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ModGM : ModuleBase
{
    private GameObject gmEntranceGo;

    public Dictionary<string, GMBase> gmDataDict { get; private set; } = new Dictionary<string, GMBase>(); //GM数据字典

    public override void OnStart()
    {
        base.OnStart();

        CreateGMEntrance();

        InitGM();
    }

    /// <summary>
    /// 创建GM入口（FPS）
    /// </summary>
    private void CreateGMEntrance()
    {
        if (gmEntranceGo != null)
            return;
        gmEntranceGo = GameUtils.CreateGameObject("UITxt_FPS", Game.GetMod<ModUI>().UICanvas.transform, false,
            typeof(RectTransform), typeof(Text), typeof(Canvas), typeof(GraphicRaycaster), typeof(Button),
            typeof(GMEntrance));
        gmEntranceGo.SetLayer(CommonConst.Layer_UI);
        var com_Canvas = gmEntranceGo.GetComponent<Canvas>();
        com_Canvas.overrideSorting = true;
        com_Canvas.sortingOrder = 11111;
        var comText = gmEntranceGo.GetComponent<Text>();
        comText.fontStyle = FontStyle.Bold;
        comText.alignment = TextAnchor.UpperLeft;
        comText.fontSize = 31;
        comText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        var com_RectTransform = comText.GetComponent<RectTransform>();
        com_RectTransform.pivot = Vector2.one;
        com_RectTransform.anchorMin = Vector2.one;
        com_RectTransform.anchorMax = Vector2.one;
        com_RectTransform.anchoredPosition = new Vector2(-11, -11);
        com_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        com_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150);
    }

    #region GM工具

    private void InitGM()
    {
        RegisterGM<CommonGM>("通用");
        RegisterGM<InGameGM>("局内");
        RegisterGM<ActivityGM>("活动");
        RegisterGM<OtherGM>("其他");
    }

    /// <summary>
    /// 注册GM工具
    /// </summary>
    private void RegisterGM<T>(string category)
        where T : GMBase
    {
        if (gmDataDict.ContainsKey(category))
        {
            CLog.Error($"不能注册相同名称的GM工具：{category}");
            return;
        }
        var gm = (GMBase)Activator.CreateInstance(typeof(T));
        gm.Init(category);
        gmDataDict.Add(category, gm);
    }

    #endregion GM工具

    public override void OnDispose()
    {
        base.OnDispose();
        Object.Destroy(gmEntranceGo);
        gmDataDict?.Clear();
    }
}

#endif