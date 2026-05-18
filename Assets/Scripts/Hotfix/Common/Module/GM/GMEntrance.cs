#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using System.Text;
using Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.UI;

/// <summary>
/// GM入口
/// </summary>
public class GMEntrance : MonoBehaviour
{
    private Button UIBTN_GM;

    private Text UITxt_FPS;
    private StringBuilder sb_FPS = new StringBuilder();

    private StringBuilder sb_Status = new StringBuilder();
    private Text UITxt_Stats;
    private bool showStats; //是否显示性能统计信息
    private bool dragState;//本次是否为拖拽状态
    private bool longPressState;//本次是否为长按状态

    private void Awake()
    {
        UIBTN_GM = gameObject.GetComponent<Button>();
        UITxt_FPS = gameObject.GetComponent<Text>();
        UIBTN_GM.onClick.AddListener(OnGMBtn);
        UIEventTriggerCenter.RegisterOnLongPress(UIBTN_GM.gameObject, OnLongPressGMBtn, 3, false);
        UIEventTriggerCenter.RegisterOnBeginDrag(UIBTN_GM.gameObject, OnBeginDrag);
        UIEventTriggerCenter.RegisterOnDrag(UIBTN_GM.gameObject, OnDrag);
        UIEventTriggerCenter.RegisterOnEndDrag(UIBTN_GM.gameObject, OnEndDrag);
        UIEventTriggerCenter.RegisterOnPointerDown(UIBTN_GM.gameObject, OnPointerDown);
    }

    private void Start()
    {
        fpsBuffer = new float[BufferSecRange];
    }

    private void Update()
    {
        // FPS
        CalcFPS();
        sb_FPS.Clear();
        sb_FPS.AppendLine($"<color={GetFPSColorHex(curFPS)}>当前FPS:{MathUtils.Round(curFPS)}</color>");
        sb_FPS.AppendLine($"<color={GetFPSColorHex(minFPS)}>最小FPS:{MathUtils.Round(minFPS)}</color>");
        sb_FPS.AppendLine($"<color={GetFPSColorHex(maxFPS)}>最大FPS:{MathUtils.Round(maxFPS)}</color>");
        sb_FPS.AppendLine($"<color={GetFPSColorHex(avgFPS)}>平均FPS:{MathUtils.Round(avgFPS)}</color>");
        UITxt_FPS.text = sb_FPS.ToString();

        // 性能统计信息
        if (showStats)
        {
            CalcStatsInfo();
            if (UITxt_Stats == null)
            {
                CreateStatsTextComponent();
            }
            UITxt_Stats.text = sb_Status.ToString();
            UITxt_Stats?.gameObject.SetActive(true);
        }
        else
        {
            UITxt_Stats?.gameObject.SetActive(false);
        }
    }

    #region FPS

    // 缓存最近几秒的帧率
    private float curFPS; //当前帧率
    private float minFPS; //最小帧率
    private float maxFPS; //最大帧率
    private float avgFPS; //平均帧率
    private float[] fpsBuffer;
    private int fpsBufferIndex;
    private const int BufferSecRange = 3;

    private int fpsCounter;
    private float lastUpdateFPSTime;

    private void CalcFPS()
    {
        fpsCounter++;
        float intervalTime = Time.realtimeSinceStartup - lastUpdateFPSTime;
        if (intervalTime >= 1)
        {
            curFPS = fpsCounter * 1f / intervalTime;
            fpsCounter = 0;
            lastUpdateFPSTime = Time.realtimeSinceStartup;

            fpsBuffer[fpsBufferIndex++ % BufferSecRange] = curFPS;
        }

        // 计算最小、最大、平均帧率
        float sum = 0;
        float min = int.MaxValue;
        float max = int.MinValue;
        int validCount = 0;
        for (int i = 0; i < BufferSecRange; i++)
        {
            if (fpsBuffer[i] == 0)
                continue;
            validCount++;
            float fps = fpsBuffer[i];
            sum += fps;
            min = Mathf.Min(min, fps);
            max = Mathf.Max(max, fps);
        }
        avgFPS = sum / validCount;
        minFPS = min;
        maxFPS = max;
    }

    private string GetFPSColorHex(float fps)
    {
        if (fps > 50)
            return "#00FF00";
        if (fps > 30)
            return "#FFFF00";
        return "#FF0000";
    }

    #endregion FPS

    #region Status

    private void CalcStatsInfo()
    {
        sb_Status.Clear();
        // 应用内存相关
        long memoryTotal = Profiler.GetTotalReservedMemoryLong() / (1024 * 1024);
        long memoryUsed = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024);
        // GPU相关
        long gpuMemoryUsed = Profiler.GetAllocatedMemoryForGraphicsDriver() / (1024 * 1024);
        // GC相关
        long gcUsed = Profiler.GetMonoUsedSizeLong() / (1024 * 1024);
        long gcTotal = Profiler.GetMonoHeapSizeLong() / (1024 * 1024);
        int currentGen0 = GC.CollectionCount(0);
        int currentGen1 = GC.CollectionCount(1);
        int currentGen2 = GC.CollectionCount(2);
        int gcCollections = currentGen0 + currentGen1 + currentGen2;
        sb_Status.AppendLine($"应用内存：{memoryUsed}MB/{memoryTotal}MB");
        sb_Status.AppendLine($"GPU内存：{gpuMemoryUsed}MB");
        sb_Status.AppendLine($"GC堆内存：{gcUsed}MB/{gcTotal}MB");
        sb_Status.AppendLine($"GC回收次数：{gcCollections}（{currentGen0}|{currentGen1}|{currentGen2}）");
    }

    /// <summary>
    /// 创建统计Text组件
    /// </summary>
    private void CreateStatsTextComponent()
    {
        GameObject statusGo = GameUtils.CreateGameObject("UITxt_Stats", Game.GetMod<ModUI>().UICanvas.transform, false,
            typeof(RectTransform), typeof(Text), typeof(Canvas));
        statusGo.SetLayer(CommonConst.Layer_UI);
        var com_Canvas = statusGo.GetComponent<Canvas>();
        com_Canvas.overrideSorting = true;
        com_Canvas.sortingOrder = 11111;
        UITxt_Stats = statusGo.GetComponent<Text>();
        UITxt_Stats.fontStyle = FontStyle.Bold;
        UITxt_Stats.alignment = TextAnchor.LowerLeft;
        UITxt_Stats.fontSize = 40;
        UITxt_Stats.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        UITxt_Stats.raycastTarget = false;
        var com_RectTransform = UITxt_Stats.GetComponent<RectTransform>();
        com_RectTransform.pivot = Vector2.zero;
        com_RectTransform.anchorMin = Vector2.zero;
        com_RectTransform.anchorMax = Vector2.zero;
        com_RectTransform.anchoredPosition = new Vector2(0, 100); //往上一点，别被banner挡住
        com_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        com_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }

    #endregion Status

    private void OnGMBtn()
    {
        if (dragState
            || longPressState)
            return;

        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_GM);
    }

    private void OnLongPressGMBtn(GameObject go)
    {
        if (dragState)
            return;

        showStats = !showStats;
        longPressState = true;
    }

    public void OnBeginDrag(GameObject go, PointerEventData eventData)
    {
        dragState = true;
    }

    public void OnDrag(GameObject go, PointerEventData eventData)
    {
        Vector3 uiPos = CTUtils.Screen2UILocal(eventData.position, Game.GetMod<ModUI>().UIRect, Game.GetMod<ModUI>().UICamera);
        uiPos.z = 0;
        UIBTN_GM.transform.localPosition = uiPos;
    }

    public void OnEndDrag(GameObject go, PointerEventData eventData)
    {
        dragState = false;
    }

    public void OnPointerDown(GameObject go, PointerEventData eventData)
    {
        longPressState = false;
    }
}

#endif