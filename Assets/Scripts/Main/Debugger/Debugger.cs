using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;

/// <summary>
/// 调试器
/// </summary>
public class Debugger : MonoBehaviour
{
    private static Vector2 RefResolution = new Vector2(720, 1280); //参照的分辨率
    private float pxRatio; //像素比例
    private float pxRatio_V; //垂直像素比例

    private static bool enableDebugger; //是否启用调试器

    private void Awake()
    {
        InitSetting();
    }

    /// <summary>
    /// 启用调试器
    /// </summary>
    public static void Enable()
    {
        enableDebugger = true;
        Application.logMessageReceived += LogCallBack;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= LogCallBack;
    }

    private void Start()
    {
        Init();
        CalcPxRatio();
    }

    private void Init()
    {
        lastBlockClick = blockClick;
    }

    #region GUI Style

    // 通用Style
    private GUIStyle labelStyle_Common;
    private GUIStyle textFieldStyle_Gray_Common;
    private GUIStyle toggleStyle_Short_Common;
    private GUIStyle toggleStyle_Mid_Common;
    private GUIStyle toggleStyle_Long_Common;
    private GUIStyle btnStyle_Common;
    private GUIStyle btnStyle_Common_Square;
    private GUIStyle scrollBarStyle_Vertical_Common;
    private GUIStyle scrollBarThumbStyle_Vertical_Common;
    private GUIStyle scrollBarStyle_Horizontal_Common;
    private GUIStyle scrollBarThumbStyle_Horizontal_Common;
    private GUIStyle toggleStyle_Square_Common;

    // 非通用Style
    private GUIStyle btnStyle_Transparent;
    private GUIStyle textStyle_StackContent;
    private GUIStyle textStyle_StackContent_Bold;
    private GUIStyle textStyle_Mini_Warning;
    private GUIStyle textStyle_Mini_Error;
    private GUIStyle textStyle_LogContentArea_UnSelected;
    private GUIStyle textStyle_LogContentArea_Selected;
    private GUIStyle areaStyle_LogAndStackContent;
    private GUIStyle areaStyle_Setting;
    private GUIStyle textStyle_ClearFilter;
    private GUIStyle toggleStyle_Setting;

    //
    private Texture2D btnTransparentTex;
    private Texture2D toggleOffTex_Long;
    private Texture2D toggleOnTex_Long;
    private Texture2D toggleOffTex_Mid;
    private Texture2D toggleOnTex_Mid;
    private Texture2D toggleOffTex_Short;
    private Texture2D toggleOnTex_Short;
    private Texture2D logContentAreaTex_UnSelected;
    private Texture2D logContentAreaTex_Selected;
    private Texture2D textMiniTex_Error;
    private Texture2D textMiniTex_Warning;
    private Texture2D areaTex_Log;
    private Texture2D areaTex_Setting;
    private Texture2D scrollBarThumbTex;
    private Texture2D textFieldTex_Gray;
    private Texture2D toggleOnTex_Setting;
    private Texture2D toggleOffTex_Setting;
    private Texture2D toggleOffTex_Square;
    private Texture2D toggleOnTex_Square;

    // 通用字体大小
    private int FontSize_Common => (int)(19 * pxRatio);

    private void InitStyle(bool forceRefresh = false)
    {
        if (btnStyle_Transparent == null || forceRefresh)
        {
            btnTransparentTex = CreateTexture(Color.clear);
            btnStyle_Transparent = new GUIStyle(GUI.skin.button);
            btnStyle_Transparent.fixedWidth = (int)(111 * pxRatio);
            btnStyle_Transparent.fixedHeight = (int)(111 * pxRatio);
            btnStyle_Transparent.normal.background = btnTransparentTex;
            btnStyle_Transparent.hover.background = btnTransparentTex;
            btnStyle_Transparent.active.background = btnTransparentTex;
        }
        if (textStyle_StackContent == null || forceRefresh)
        {
            textStyle_StackContent = new GUIStyle();
            textStyle_StackContent.fontSize = FontSize_Log;
            textStyle_StackContent.normal.textColor = Color.white;
        }
        if (textStyle_StackContent_Bold == null || forceRefresh)
        {
            textStyle_StackContent_Bold = new GUIStyle();
            textStyle_StackContent_Bold.fontSize = FontSize_Log;
            textStyle_StackContent_Bold.normal.textColor = Color.white;
            textStyle_StackContent_Bold.fontStyle = FontStyle.Bold;
        }
        if (textStyle_Mini_Error == null || forceRefresh)
        {
            textMiniTex_Error = CreateTexture(Color.red);
            textStyle_Mini_Error = new GUIStyle();
            textStyle_Mini_Error.fontSize = (int)(40 * pxRatio);
            textStyle_Mini_Error.fixedWidth = (int)(70 * pxRatio);
            textStyle_Mini_Error.fixedHeight = (int)(70 * pxRatio);
            textStyle_Mini_Error.normal.textColor = Color.black;
            textStyle_Mini_Error.alignment = TextAnchor.MiddleCenter;
            textStyle_Mini_Error.fontStyle = FontStyle.Bold;
            textStyle_Mini_Error.normal.background = textMiniTex_Error;
        }
        if (textStyle_Mini_Warning == null || forceRefresh)
        {
            textMiniTex_Warning = CreateTexture(Color.yellow);
            textStyle_Mini_Warning = new GUIStyle();
            textStyle_Mini_Warning.fontSize = (int)(40 * pxRatio);
            textStyle_Mini_Warning.fixedWidth = (int)(70 * pxRatio);
            textStyle_Mini_Warning.fixedHeight = (int)(70 * pxRatio);
            textStyle_Mini_Warning.normal.textColor = Color.black;
            textStyle_Mini_Warning.alignment = TextAnchor.MiddleCenter;
            textStyle_Mini_Warning.fontStyle = FontStyle.Bold;
            textStyle_Mini_Warning.normal.background = textMiniTex_Warning;
        }
        if (labelStyle_Common == null || forceRefresh)
        {
            labelStyle_Common = new GUIStyle();
            labelStyle_Common.fontSize = FontSize_Common;
            labelStyle_Common.normal.textColor = Color.white;
            labelStyle_Common.alignment = TextAnchor.MiddleCenter;
            labelStyle_Common.fixedHeight = Height_Btn_Common;
        }
        if (textFieldStyle_Gray_Common == null || forceRefresh)
        {
            textFieldTex_Gray = CreateTexture(new Color(0.2f, 0.2f, 0.2f, 1f));
            textFieldStyle_Gray_Common = new GUIStyle();
            textFieldStyle_Gray_Common.fontSize = FontSize_Common;
            textFieldStyle_Gray_Common.fixedHeight = Height_Btn_Common;
            textFieldStyle_Gray_Common.fixedWidth = 100 * pxRatio;
            textFieldStyle_Gray_Common.normal.textColor = Color.white;
            textFieldStyle_Gray_Common.normal.background = textFieldTex_Gray;
        }
        if (toggleStyle_Short_Common == null || forceRefresh)
        {
            toggleStyle_Short_Common = new GUIStyle(GUI.skin.toggle);
            toggleOffTex_Short = CreateTexture(Color.black);
            toggleOnTex_Short = CreateTexture(Color.gray);
            CreateToggle(toggleStyle_Short_Common, 100 * pxRatio, Height_Btn_Common, toggleOnTex_Short, toggleOffTex_Short);
        }
        if (toggleStyle_Mid_Common == null || forceRefresh)
        {
            toggleStyle_Mid_Common = new GUIStyle(GUI.skin.toggle);
            toggleOffTex_Mid = CreateTexture(Color.black);
            toggleOnTex_Mid = CreateTexture(Color.gray);
            CreateToggle(toggleStyle_Mid_Common, 140 * pxRatio, Height_Btn_Common, toggleOnTex_Mid, toggleOffTex_Mid);
        }
        if (toggleStyle_Long_Common == null || forceRefresh)
        {
            toggleStyle_Long_Common = new GUIStyle(GUI.skin.toggle);
            toggleOffTex_Long = CreateTexture(Color.black);
            toggleOnTex_Long = CreateTexture(Color.gray);
            CreateToggle(toggleStyle_Long_Common, 180 * pxRatio, Height_Btn_Common, toggleOnTex_Long, toggleOffTex_Long);
        }
        if (btnStyle_Common == null || forceRefresh)
        {
            btnStyle_Common = new GUIStyle(GUI.skin.button);
            btnStyle_Common.fontSize = FontSize_Common;
            btnStyle_Common.richText = true;
            btnStyle_Common.fixedHeight = Height_Btn_Common;
            btnStyle_Common.fixedWidth = 120 * pxRatio;
        }
        if (btnStyle_Common_Square == null || forceRefresh)
        {
            btnStyle_Common_Square = new GUIStyle(GUI.skin.button);
            btnStyle_Common_Square.fontSize = FontSize_Common;
            btnStyle_Common_Square.richText = true;
            btnStyle_Common_Square.fixedHeight = Height_Btn_Common;
            btnStyle_Common_Square.fixedWidth = Height_Btn_Common;
        }
        if (textStyle_LogContentArea_UnSelected == null || forceRefresh)
        {
            logContentAreaTex_UnSelected = CreateTexture(new Color(0, 0, 0, 0f));
            textStyle_LogContentArea_UnSelected = new GUIStyle();
            textStyle_LogContentArea_UnSelected.fontSize = FontSize_Log;
            textStyle_LogContentArea_UnSelected.richText = true;
            textStyle_LogContentArea_UnSelected.normal.textColor = Color.white;
            textStyle_LogContentArea_UnSelected.normal.background = logContentAreaTex_UnSelected;
            CalcLogContentBtnHeight();
        }
        if (textStyle_LogContentArea_Selected == null || forceRefresh)
        {
            logContentAreaTex_Selected = CreateTexture(new Color(0.17f, 0.5f, 0.77f, 1f));
            textStyle_LogContentArea_Selected = new GUIStyle();
            textStyle_LogContentArea_Selected.fontSize = FontSize_Log;
            textStyle_LogContentArea_Selected.richText = true;
            textStyle_LogContentArea_Selected.fontStyle = FontStyle.Bold;
            textStyle_LogContentArea_Selected.normal.textColor = Color.white;
            textStyle_LogContentArea_Selected.normal.background = logContentAreaTex_Selected;
        }
        if (areaStyle_LogAndStackContent == null || forceRefresh)
        {
            areaTex_Log = CreateTexture(new Color(0, 0, 0, 0.7f));
            areaStyle_LogAndStackContent = new GUIStyle();
            areaStyle_LogAndStackContent.normal.background = areaTex_Log;
        }
        if (areaStyle_Setting == null || forceRefresh)
        {
            areaTex_Setting = CreateTexture(new Color(0, 0, 0, 1));
            areaStyle_Setting = new GUIStyle();
            areaStyle_Setting.normal.background = areaTex_Setting;
        }
        if (scrollBarThumbTex == null || forceRefresh)
        {
            scrollBarThumbTex = CreateTexture(new Color(0.5f, 0.5f, 0.5f));
        }
        if (scrollBarStyle_Vertical_Common == null || forceRefresh)
        {
            scrollBarStyle_Vertical_Common = new GUIStyle(GUI.skin.verticalScrollbar);
            scrollBarStyle_Vertical_Common.fixedWidth = ScrollBarSize;
        }
        if (scrollBarThumbStyle_Vertical_Common == null || forceRefresh)
        {
            scrollBarThumbStyle_Vertical_Common = new GUIStyle(GUI.skin.verticalScrollbarThumb);
            scrollBarThumbStyle_Vertical_Common.fixedWidth = 35 * pxRatio;
            scrollBarThumbStyle_Vertical_Common.normal.background = scrollBarThumbTex;
        }
        if (scrollBarStyle_Horizontal_Common == null || forceRefresh)
        {
            scrollBarStyle_Horizontal_Common = new GUIStyle(GUI.skin.horizontalScrollbar);
            scrollBarStyle_Horizontal_Common.fixedHeight = ScrollBarSize;
        }
        if (scrollBarThumbStyle_Horizontal_Common == null || forceRefresh)
        {
            scrollBarThumbStyle_Horizontal_Common = new GUIStyle(GUI.skin.horizontalScrollbarThumb);
            scrollBarThumbStyle_Horizontal_Common.fixedHeight = 35 * pxRatio;
            scrollBarThumbStyle_Horizontal_Common.normal.background = scrollBarThumbTex;
        }
        if (textStyle_ClearFilter == null || forceRefresh)
        {
            textStyle_ClearFilter = new GUIStyle();
            textStyle_ClearFilter.fontSize = (int)(35 * pxRatio);
            textStyle_ClearFilter.fixedWidth = Height_Btn_Common;
            textStyle_ClearFilter.fixedHeight = Height_Btn_Common;
            textStyle_ClearFilter.normal.textColor = Color.white;
            textStyle_ClearFilter.alignment = TextAnchor.MiddleCenter;
            textStyle_ClearFilter.fontStyle = FontStyle.Bold;
        }
        if (toggleStyle_Setting == null || forceRefresh)
        {
            toggleStyle_Setting = new GUIStyle(GUI.skin.toggle);
            toggleOffTex_Setting = CreateTexture(Color.gray);
            toggleOnTex_Setting = CreateTexture(Color.green);
            CreateToggle(toggleStyle_Setting, Height_Btn_Common, Height_Btn_Common, toggleOnTex_Setting, toggleOffTex_Setting);
        }
        if (toggleStyle_Square_Common == null || forceRefresh)
        {
            toggleStyle_Square_Common = new GUIStyle(GUI.skin.toggle);
            toggleOffTex_Square = CreateTexture(Color.black);
            toggleOnTex_Square = CreateTexture(Color.gray);
            CreateToggle(toggleStyle_Square_Common, Height_Btn_Common, Height_Btn_Common, toggleOnTex_Square, toggleOffTex_Square);
        }
    }

    private void CreateToggle(GUIStyle guiStyle, float fixedWidth, float fixedHeight, Texture2D onTex, Texture2D offTex)
    {
        guiStyle.fixedWidth = fixedWidth;
        guiStyle.fixedHeight = fixedHeight;
        guiStyle.fontSize = FontSize_Common;
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.normal.textColor = Color.white;
        guiStyle.normal.background = offTex;
        guiStyle.onNormal.background = onTex;
        guiStyle.active.background = offTex;
        guiStyle.onActive.background = onTex;
        guiStyle.focused.background = offTex;
        guiStyle.onFocused.background = onTex;
        guiStyle.hover.background = offTex;
        guiStyle.onHover.background = onTex;
    }

    private Texture2D CreateTexture(Color color)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        tex.Apply();
        return tex;
    }

    /// <summary>
    /// 绘制设置界面的Toggle
    /// </summary>
    private bool DrawSettingToggle(bool b, string content)
    {
        GUILayout.BeginHorizontal();
        b = GUILayout.Toggle(b, "", toggleStyle_Setting);
        GUILayout.Label(content, labelStyle_Common);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        return b;
    }

    #endregion GUI Style

    private void OnGUI()
    {
        InitStyle();

        if (enableDebugger)
        {
            if (showDebugger || (neverShow && minimizeModeFirstEnterGame))
            {
                if (lastBlockClick != blockClick)
                {
                    OnBlockClickToggleChanged();
                    lastBlockClick = blockClick;
                }

                DrawDebugger();
            }
            else
            {
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", btnStyle_Transparent))
                {
                    if (lastClickTime > 0)
                    {
                        if (Time.realtimeSinceStartup - lastClickTime < 0.5f)
                        {
                            OnBlockClickToggleChanged();
                            showDebugger = true;
                        }
                        else
                        {
                            lastClickTime = 0;
                        }
                    }
                    else
                    {
                        lastClickTime = Time.realtimeSinceStartup;
                    }
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }
    }

    private bool showDebugger;
    private bool neverShow = true; //没有展示过
    private float lastClickTime;

    /// <summary>
    /// 绘制调试器
    /// </summary>
    private void DrawDebugger()
    {
        if (isMinimize || (neverShow && minimizeModeFirstEnterGame))
        {
            if (neverShow && minimizeModeFirstEnterGame)
            {
                showDebugger = true;
            }
            DrawLogWindowMini();
        }
        else
        {
            neverShow = false;
            GUILayout.Window(0,
                new Rect(0, 0,
                    Screen.width, Height_LogTopButtonArea + Height_LogContentArea + Height_LogStackArea),
                DrawLogWindow, "", areaStyle_LogAndStackContent);

            if (showSettingWindow)
            {
                GUILayout.Window(1,
                    new Rect(0, 0,
                        Screen.width, Screen.height),
                    DrawSettingWindow, "", areaStyle_Setting);
            }
        }
    }

    #region Log窗口

    private static List<LogInfo> logCache = new List<LogInfo>(); //所有log信息

    /// <summary>
    /// 清空所有log
    /// </summary>
    public void ClearLog()
    {
        logCache.Clear();
        selectLog = null;
        infoCounter = 0;
        warningCounter = 0;
        errorCounter_Real = 0;
        errorCounter_Log = 0;
    }

    private LogInfo selectLog; //当前选择的log
    private string filterStr = ""; //过滤的字符串
    private string lastFilterStr;
    private string lastFilterLowerStr;

    private static int infoCounter; //log计数器
    private static int warningCounter; //warning计数器
    private static int errorCounter_Real; //error计数器（真实错误）
    private static int errorCounter_Log; //error计数器（Error级别的log）

    private bool showInfo = true; //是否显示log
    private bool showWarning = true; //是否显示warning
    private bool showError = true; //是否显示error
    private bool showRealErrorOnly; //是否只显示真实错误
    private bool unfold = true; //是否展开
    private bool blockClick = true; //是否阻挡点击
    private bool lastBlockClick; //记录是否阻挡点击状态
    private bool isMinimize; //是否最小化
    private bool showWarning_Minimize; //迷你模式是否显示warning
    private bool showLineLog = true; //log区域是否显示单行log
    private bool minimizeModeFirstEnterGame; //每次首次进入游戏时是否为迷你模式

    private Vector2 logScrollPos; //log区域滑动条位置
    private Vector2 stackScrollPos; //堆栈区域滑动条位置
    private Rect LogContentSliderRect => new Rect(0, Height_LogTopButtonArea + Height_LogTopButtonArea / 2,
        Screen.width - ScrollBarSize, Height_LogContentArea - ScrollBarSize);
    private Rect StackContentSliderRect => new Rect(0, Height_LogTopButtonArea + Height_LogTopButtonArea / 2 + Height_LogContentArea,
        Screen.width - ScrollBarSize, Height_LogStackArea - ScrollBarSize);

    //
    private const int DefaultFontSize_Log = 20; // Log和Stack区域字体大小
    private int fontSize_Log;
    private int FontSize_Log => (int)(fontSize_Log * pxRatio); // Log和Stack区域字体大小
    //
    private float ScrollBarSize => 40 * pxRatio;
    private float Height_LogTopButtonArea => 60 * pxRatio_V;
    private float Height_LogContentArea => (unfold ? 800 : 150) * pxRatio_V;
    private float Height_LogStackArea => (selectLog != null ? 250 : 0) * pxRatio_V;
    private float Height_Btn_Common => 35 * pxRatio;

    private const int DefaultLineMaxShowChar = 500; //一行显示的默认最大字符数
    private static int lineMaxShowChar; //一行显示的最大字符数

    //
    private bool clickLogBtn;
    private List<LogInfo> visibleLogInfoList = new List<LogInfo>();
    private const float LogContentBtnSpace = 5; // Log内容按钮间距
    private int showLogApproxLineCount; // 区域内可显示的近似log行数
    private float LogContentBtnHeight; // Log内容按钮高度
    private int showMinIndex = 0;
    private int showMaxIndex = 0;

    /// <summary>
    /// Log信息
    /// </summary>
    public class LogInfo
    {
        private string Prefix;

        public string OriLog { get; private set; } //原始log内容
        public string OriLogLower { get; private set; } //原始log内容（小写）
        public string OriLogLowerWithPrefix { get; private set; } // 原始log内容（小写带前缀）
        public string SplitLog { get; private set; } //分割多行后的内容（多行模式显示用）
        public int SplitLineCount { get; private set; } //分割后的行数
        public string LineLog { get; private set; } //一行的内容（单行模式显示用）
        public string StackTrace { get; private set; }
        public ELogType LogType { get; private set; }
        private DateTime logTime;

        public LogInfo(string oriLog, string splitLog, int splitLineCount, string lineLog, string stackTrace, ELogType logType)
        {
            logTime = DateTime.Now;
            Prefix = $"<color={GetLogColor(logType)}>\u25cf</color> [{logTime.ToString("HH:mm:ss")}] ";
            OriLog = oriLog;
            OriLogLower = oriLog.ToLower();
            OriLogLowerWithPrefix = $"{Prefix}{OriLogLower}";
            LineLog = $" {Prefix} {lineLog}";
            SplitLog = $" {Prefix} {splitLog}";
            SplitLineCount = splitLineCount;
            LogType = logType;
            StackTrace = stackTrace;
        }

        public void SetSplitLog(string splitLog, int splitLineCount)
        {
            SplitLog = splitLog;
            SplitLineCount = splitLineCount;
        }
    }

    private static void LogCallBack(string condition, string stackTrace, LogType logType)
    {
        string oriLog = condition;
        string lineLog = string.Empty;
        string splitLog = string.Empty;
        string tempLineLogWithoutLinefeed = condition.Replace("\n", " ");
        // lineLog（处理富文本显示问题）
        int subLen = 111;
        string headMatchStr = "<color=";
        string tailMatchStr = "</color>";
        int lastTailMatchIndex = tempLineLogWithoutLinefeed.LastIndexOf(tailMatchStr);
        if (lastTailMatchIndex == -1 || lastTailMatchIndex + tailMatchStr.Length <= subLen)
        {
            lineLog = tempLineLogWithoutLinefeed.Substring(0, Mathf.Min(subLen, tempLineLogWithoutLinefeed.Length));
        }
        else
        {
            string tempLineLog = string.Empty;
            int lastLeftAngleBracket = tempLineLogWithoutLinefeed.LastIndexOf("<");
            tempLineLog = lastLeftAngleBracket >= subLen - tailMatchStr.Length
                          && lastLeftAngleBracket <= subLen + tailMatchStr.Length
                ? tempLineLogWithoutLinefeed.Substring(0, Mathf.Min(subLen + tailMatchStr.Length * 2, tempLineLogWithoutLinefeed.Length))
                : tempLineLogWithoutLinefeed.Substring(0, Mathf.Min(subLen, tempLineLogWithoutLinefeed.Length));
            lineLog = tempLineLog;
            int headMatchCount = 0;
            int tailMatchCount = 0;
            int tempIndex = 0;
            while ((tempIndex = tempLineLog.IndexOf(headMatchStr, tempIndex)) != -1)
            {
                headMatchCount++;
                tempIndex += headMatchStr.Length;
            }
            tempIndex = 0;
            while ((tempIndex = tempLineLog.IndexOf(tailMatchStr, tempIndex)) != -1)
            {
                tailMatchCount++;
                tempIndex += tailMatchStr.Length;
            }
            for (int i = 0; i < headMatchCount - tailMatchCount; i++)
            {
                lineLog += tailMatchStr;
            }
        }
        // splitLog
        int splitLineCount = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < condition.Length; i += lineMaxShowChar)
        {
            sb.AppendLine(condition.Substring(i, Math.Min(lineMaxShowChar, condition.Length - i)));
            splitLineCount++;
        }
        splitLog = sb.ToString().Substring(0, sb.Length - "\n".Length); //去掉最后一个\n
        ELogType tempLogType = ELogType.Log;
        switch (logType)
        {
            case LogType.Error:
                if (IsRealError(condition))
                    tempLogType = ELogType.Error_Real;
                else
                    tempLogType = ELogType.Error_Log;
                break;

            case LogType.Assert:
                tempLogType = ELogType.Assert;
                break;

            case LogType.Warning:
                tempLogType = ELogType.Warning;
                break;

            case LogType.Log:
                tempLogType = ELogType.Log;
                break;

            case LogType.Exception:
                tempLogType = ELogType.Exception;
                break;
        }
        if (tempLogType == ELogType.Error_Real
            || tempLogType == ELogType.Exception
            || tempLogType == ELogType.Assert)
        {
            errorCounter_Real++;
        }
        else if (tempLogType == ELogType.Error_Log)
        {
            errorCounter_Log++;
        }
        else if (tempLogType == ELogType.Warning)
        {
            warningCounter++;
        }
        else
        {
            infoCounter++;
        }
        LogInfo logInfo = new LogInfo(oriLog, splitLog, splitLineCount, lineLog, stackTrace, tempLogType);
        logCache.Add(logInfo);
    }

    /// <summary>
    /// 刷新Log缓存
    /// </summary>
    private void RefreshLogCache()
    {
        int splitLineCount = 0;
        foreach (var logInfo in logCache)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < logInfo.OriLog.Length; i += lineMaxShowChar)
            {
                sb.AppendLine(logInfo.OriLog.Substring(i, Math.Min(lineMaxShowChar, logInfo.OriLog.Length - i)));
                splitLineCount++;
            }
            string splitLog = sb.ToString().Substring(0, sb.Length - "\n".Length); //去掉最后一个\n
            logInfo.SetSplitLog(splitLog, splitLineCount);
        }
    }

    private void DrawLogWindowMini()
    {
        GUILayout.BeginHorizontal();
        SetSpace(20);
        if (showWarning_Minimize)
        {
            if (GUILayout.Button(warningCounter.ToString(), textStyle_Mini_Warning))
            {
                isMinimize = false;
                neverShow = false;
                OnBlockClickToggleChanged();
            }
            SetSpace(5);
        }
        if (GUILayout.Button(errorCounter_Real.ToString(), textStyle_Mini_Error))
        {
            isMinimize = false;
            neverShow = false;
            OnBlockClickToggleChanged();
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制Log窗口
    /// </summary>
    private void DrawLogWindow(int windowId)
    {
        HandleScroll_Pre();

        DrawLogWindow_TopButtonArea();
        GUI.skin.horizontalScrollbarThumb = scrollBarThumbStyle_Horizontal_Common;
        GUI.skin.verticalScrollbarThumb = scrollBarThumbStyle_Vertical_Common;
        DrawLogWindow_LogContentArea();
        DrawLogWindow_LogStackArea();

        HandleScroll_Post();
    }

    private void DrawLogWindow_TopButtonArea()
    {
        // 第一行
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("设置", btnStyle_Common))
        {
            showSettingWindow = true;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("迷你模式", btnStyle_Common))
        {
            isMinimize = true;
            SetBlockClickState(true);
        }
        if (GUILayout.Button("关闭", btnStyle_Common))
        {
            showDebugger = false;
            lastClickTime = 0;
            SetBlockClickState(true);
        }
        GUILayout.EndHorizontal();

        // 第二行
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("查找", labelStyle_Common);
        filterStr = GUILayout.TextField(filterStr, textFieldStyle_Gray_Common);
        if (GUILayout.Button("X", textStyle_ClearFilter))
        {
            filterStr = string.Empty;
        }
        GUILayout.FlexibleSpace();
        showInfo = GUILayout.Toggle(showInfo, $"<color={GetLogColor(ELogType.Log)}>Info [{infoCounter}]</color>", toggleStyle_Mid_Common);
        SetSpace(5);
        showWarning = GUILayout.Toggle(showWarning, $"<color={GetLogColor(ELogType.Warning)}>Warning [{warningCounter}]</color>", toggleStyle_Mid_Common);
        SetSpace(5);
        showError = GUILayout.Toggle(showError, $"<color={GetLogColor(ELogType.Error_Real)}>Error [{errorCounter_Real + errorCounter_Log}]</color>", toggleStyle_Mid_Common);
        if (showError)
        {
            showRealErrorOnly = GUILayout.Toggle(showRealErrorOnly, $"<color={GetLogColor(ELogType.Error_Real)}>{errorCounter_Real}</color>", toggleStyle_Square_Common);
        }
        GUILayout.EndHorizontal();

        // 第三行
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("复制", btnStyle_Common))
        {
            if (selectLog != null)
            {
                GUIUtility.systemCopyBuffer = selectLog.SplitLog + "\n\n" + selectLog.StackTrace;
            }
        }
        if (GUILayout.Button("清空", btnStyle_Common))
        {
            ClearLog();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("↓", btnStyle_Common_Square))
        {
            float logContentScrollBarBottomY = 0;
            for (int i = 0; i < logCache.Count; i++)
            {
                logContentScrollBarBottomY += showLineLog
                    ? LogContentBtnHeight + LogContentBtnSpace * pxRatio
                    : logCache[i].SplitLineCount * LogContentBtnHeight + LogContentBtnSpace * pxRatio;
            }
            logScrollPos.y = logContentScrollBarBottomY;
        }
        GUILayout.FlexibleSpace();
        showLineLog = GUILayout.Toggle(showLineLog, showLineLog ? "多行" : "单行", toggleStyle_Short_Common);
        unfold = GUILayout.Toggle(unfold, unfold ? "折叠" : "展开", toggleStyle_Short_Common);
        SetSpace(5);
        blockClick = GUILayout.Toggle(blockClick, blockClick ? "关闭阻挡点击" : "开启阻挡点击", toggleStyle_Mid_Common);
        GUILayout.EndHorizontal();
    }

    private void DrawLogWindow_LogContentArea()
    {
        clickLogBtn = false;

        logScrollPos = GUILayout.BeginScrollView(logScrollPos,
            true, true, scrollBarStyle_Horizontal_Common, scrollBarStyle_Vertical_Common,
            GUILayout.Height(Height_LogContentArea));
        // 计算数据
        // 只在Layout阶段计算数据，因为OnGUI每帧会执行多次，分别为几个主要阶段Layout-Mouse事件-Button事件-Repaint，要求Layout和Repaint阶段控件数量必须一致
        if (Event.current.type == EventType.Layout)
        {
            // 处理过滤字符串
            if (filterStr != lastFilterStr)
            {
                lastFilterStr = filterStr;
                lastFilterLowerStr = string.IsNullOrEmpty(filterStr)
                    ? string.Empty
                    : filterStr.ToLower();
            }
            // 计算可显示的log
            visibleLogInfoList.Clear();
            foreach (var logInfo in logCache)
            {
                ELogType logType = logInfo.LogType;
                if (!IsShowLog(logType))
                    continue;
                if (!logInfo.OriLogLowerWithPrefix.Contains(lastFilterLowerStr))
                    continue;
                visibleLogInfoList.Add(logInfo);
            }
            // 计算
            // content总高度
            // 当前显示的log近似下标
            // 区域内可显示的近似log行数
            float tempHeight = 0;
            int showLogApprowIndex = 0;
            for (int i = 0; i < visibleLogInfoList.Count; i++)
            {
                tempHeight += showLineLog
                    ? LogContentBtnHeight + LogContentBtnSpace * pxRatio
                    : visibleLogInfoList[i].SplitLineCount * LogContentBtnHeight + LogContentBtnSpace * pxRatio;
                if (tempHeight < logScrollPos.y)
                    continue;
                showLogApprowIndex = i;
                break;
            }
            showLogApproxLineCount = (int)(Height_LogContentArea / (LogContentBtnHeight + LogContentBtnSpace * pxRatio));
            // 计算显示log的最小和最大下标
            int tempLineCount = 0;
            for (int i = showLogApprowIndex; i >= 0; i--)
            {
                tempLineCount++;
                if (tempLineCount >= showLogApproxLineCount || i == 0)
                {
                    showMinIndex = i;
                    break;
                }
            }
            tempLineCount = 0;
            for (int i = showLogApprowIndex; i < visibleLogInfoList.Count; i++)
            {
                tempLineCount++;
                if (tempLineCount >= showLogApproxLineCount || i == visibleLogInfoList.Count - 1)
                {
                    showMaxIndex = i;
                    break;
                }
            }
        }
        // 绘制
        // 绘制可见区域的log，不可绘制区域的用空格占位
        for (int i = 0; i < visibleLogInfoList.Count; i++)
        {
            var logInfo = visibleLogInfoList[i];
            if (i < showMinIndex || i > showMaxIndex)
            {
                SetSpace(LogContentBtnSpace, showLineLog
                    ? LogContentBtnHeight
                    : LogContentBtnHeight * logInfo.SplitLineCount);
                continue;
            }
            string text = showLineLog
                ? logInfo.LineLog
                : logInfo.SplitLog;
            if (GUILayout.Button(text, selectLog == logInfo
                    ? textStyle_LogContentArea_Selected
                    : textStyle_LogContentArea_UnSelected))
            {
                clickLogBtn = true;
                if (isDrag)
                    continue;

                if (selectLog == logInfo)
                {
                    selectLog = null;
                }
                else
                {
                    selectLog = logInfo;
                }
            }
            SetSpace(LogContentBtnSpace);
        }
        GUILayout.EndScrollView();
    }

    private void DrawLogWindow_LogStackArea()
    {
        if (selectLog == null) return;

        string text = selectLog.SplitLog;
        if (!string.IsNullOrEmpty(filterStr))
        {
            string pattern = Regex.Escape(filterStr);
            text = Regex.Replace(text, pattern,
                match => $"<color=red><b>{match.Value}</b></color>",
                RegexOptions.IgnoreCase);
        }
        stackScrollPos = GUILayout.BeginScrollView(stackScrollPos,
            true, true, scrollBarStyle_Horizontal_Common, scrollBarStyle_Vertical_Common,
            GUILayout.Height(Height_LogStackArea));
        GUILayout.Label(text, textStyle_StackContent_Bold);
        SetSpace(50);
        GUILayout.Label(selectLog.StackTrace, textStyle_StackContent);
        GUILayout.EndScrollView();
    }

    /// <summary>
    /// 是否显示Log
    /// </summary>
    private bool IsShowLog(ELogType logType)
    {
        switch (logType)
        {
            case ELogType.Error_Real:
            case ELogType.Exception:
            case ELogType.Assert:
                return showError;

            case ELogType.Error_Log:
                return showError && !showRealErrorOnly;

            case ELogType.Warning:
                return showWarning;

            case ELogType.Log:
                return showInfo;

            default:
                return showInfo;
        }
    }

    /// <summary>
    /// 获取Log颜色
    /// </summary>
    private static string GetLogColor(ELogType logType)
    {
        switch (logType)
        {
            case ELogType.Error_Real:
            case ELogType.Error_Log:
            case ELogType.Exception:
            case ELogType.Assert:
                return "#FF0000";

            case ELogType.Warning:
                return "#FFFF00";

            case ELogType.Log:
                return "#007FFF";

            default:
                return "#007FFF";
        }
    }

    /// <summary>
    /// 是否为真实的Error
    /// </summary>
    private static bool IsRealError(string condition)
    {
        bool isRealError = !condition.ToLower().Contains("error");
        return isRealError;
    }

    private void OnBlockClickToggleChanged()
    {
        SetBlockClickState();
    }

    private void CalcLogContentBtnHeight()
    {
        LogContentBtnHeight = textStyle_LogContentArea_UnSelected.CalcHeight(new GUIContent(""), Screen.width);
    }

    #endregion

    #region 设置窗口

    private static string Key_FontSize = "Debugger_FontSize";
    private static string Key_LineMaxShowChar = "Debugger_LineMaxShowChar";
    private static string Key_ShowWarning_Minimize = "Debugger_ShowWarning_Minimize";
    private static string Key_DragRatio = "Debugger_DragRatio";
    private static string Key_MinimizeModeFirstEnterGameBool = "Debugger_MinimizeModeFirstEnterGame";

    private Vector2 settingScrollPos;
    private bool showSettingWindow;

    private string fontSizeStr; //字体大小
    private string lineMaxShowCharStr; //一行最多显示字符数
    private bool showWarning_MinimizeBool; //迷你模式下是否显示warning
    private bool minimizeModeFirstEnterGameBool; //初始进入游戏时是否为迷你模式
    private string dragRatioStr; //拖拽系数
    private const float SettingItemLeftBorder = 50f;
    private const float SettingItemSpaceY = 5f;

    private void InitSetting()
    {
        fontSizeStr = PlayerPrefs.GetInt(Key_FontSize, DefaultFontSize_Log).ToString();
        int.TryParse(fontSizeStr, out fontSize_Log);

        lineMaxShowCharStr = PlayerPrefs.GetInt(Key_LineMaxShowChar, DefaultLineMaxShowChar).ToString();
        int.TryParse(lineMaxShowCharStr, out lineMaxShowChar);

        showWarning_MinimizeBool = PlayerPrefs.GetInt(Key_ShowWarning_Minimize, 0) == 1;
        showWarning_Minimize = showWarning_MinimizeBool;

        minimizeModeFirstEnterGameBool = PlayerPrefs.GetInt(Key_MinimizeModeFirstEnterGameBool, 0) == 1;
        minimizeModeFirstEnterGame = minimizeModeFirstEnterGameBool;

        dragRatioStr = PlayerPrefs.GetFloat(Key_DragRatio, DefaultDragRatio).ToString();
        float.TryParse(dragRatioStr, out dragRatio);
    }

    /// <summary>
    /// 绘制设置窗口
    /// </summary>
    private void DrawSettingWindow(int windowId)
    {
        GUILayout.BeginVertical();

        // 顶部栏
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("关闭", btnStyle_Common))
        {
            showSettingWindow = false;
        }
        GUILayout.EndHorizontal();
        SetSpace(50);

        // 设置项
        settingScrollPos = GUILayout.BeginScrollView(settingScrollPos);

        GUILayout.BeginHorizontal();
        SetSpace(SettingItemLeftBorder);
        GUILayout.Label("字体大小：", labelStyle_Common);
        if (int.TryParse(fontSizeStr = GUILayout.TextField(fontSizeStr, textFieldStyle_Gray_Common), out int _fontSize))
        {
            if (_fontSize > 0 && _fontSize != fontSize_Log)
            {
                fontSize_Log = _fontSize;
                PlayerPrefs.SetInt(Key_FontSize, fontSize_Log);
                InitStyle(true);
            }
        }
        GUILayout.Label($"默认：{DefaultFontSize_Log}", labelStyle_Common);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        SetSpace(SettingItemSpaceY);

        GUILayout.BeginHorizontal();
        SetSpace(SettingItemLeftBorder);
        GUILayout.Label("一行最多显示的字符数：", labelStyle_Common);
        if (int.TryParse(lineMaxShowCharStr = GUILayout.TextField(lineMaxShowCharStr, textFieldStyle_Gray_Common), out int _lineMaxShowChar))
        {
            if (_lineMaxShowChar > 0 && _lineMaxShowChar != lineMaxShowChar)
            {
                lineMaxShowChar = _lineMaxShowChar;
                PlayerPrefs.SetInt(Key_LineMaxShowChar, lineMaxShowChar);
                RefreshLogCache();
            }
        }
        GUILayout.Label($"默认：{DefaultLineMaxShowChar}", labelStyle_Common);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        SetSpace(SettingItemSpaceY);

        GUILayout.BeginHorizontal();
        SetSpace(SettingItemLeftBorder);
        showWarning_MinimizeBool = DrawSettingToggle(showWarning_MinimizeBool, "迷你模式下是否显示Warning");
        if (showWarning_Minimize != showWarning_MinimizeBool)
        {
            showWarning_Minimize = showWarning_MinimizeBool;
            PlayerPrefs.SetInt(Key_ShowWarning_Minimize, showWarning_Minimize ? 1 : 0);
        }
        GUILayout.EndHorizontal();

        SetSpace(SettingItemSpaceY);

        GUILayout.BeginHorizontal();
        SetSpace(SettingItemLeftBorder);
        minimizeModeFirstEnterGameBool = DrawSettingToggle(minimizeModeFirstEnterGameBool, "每次首次进入游戏时是否为迷你模式");
        if (minimizeModeFirstEnterGame != minimizeModeFirstEnterGameBool)
        {
            PlayerPrefs.SetInt(Key_MinimizeModeFirstEnterGameBool, minimizeModeFirstEnterGameBool ? 1 : 0);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        SetSpace(SettingItemLeftBorder);
        GUILayout.Label("拖拽滑动系数：", labelStyle_Common);
        if (float.TryParse(dragRatioStr = GUILayout.TextField(dragRatioStr, textFieldStyle_Gray_Common), out float _dragRatio))
        {
            if (_dragRatio > 0 && !Mathf.Approximately(_dragRatio, dragRatio))
            {
                dragRatio = _dragRatio;
                PlayerPrefs.SetFloat(Key_DragRatio, dragRatio);
            }
        }
        GUILayout.Label($"默认：{DefaultDragRatio}", labelStyle_Common);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();

        GUILayout.EndVertical();
    }

    #endregion 设置窗口

    private Vector2 lastMouseDownPos;
    private bool isMouseDown_LogContent; //是否鼠标按下在Log内容区域
    private bool isMouseDown_StackContent; //是否鼠标按下在堆栈内容区域
    private bool isDrag; //是否为拖拽
    private bool isDrag_Vertical; //是否为竖直拖拽
    private const float dragThreshold = 11; //视为拖拽的阈值
    private const float DefaultDragRatio = 1.5f; //默认拖拽系数
    private float dragRatio;
    private int DragRatio_Use => (int)(dragRatio * pxRatio);

    private void HandleScroll_Pre()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            if (LogContentSliderRect.Contains(e.mousePosition))
            {
                isMouseDown_LogContent = true;
            }
            else if (StackContentSliderRect.Contains(e.mousePosition))
            {
                isMouseDown_StackContent = true;
            }
            lastMouseDownPos = e.mousePosition;
        }
        if (e.type == EventType.MouseDrag && (isMouseDown_LogContent || isMouseDown_StackContent))
        {
            if ((e.mousePosition - lastMouseDownPos).sqrMagnitude > dragThreshold)
            {
                isDrag = true;
                if (Mathf.Abs(e.mousePosition.y - lastMouseDownPos.y) >= Mathf.Abs(e.mousePosition.x - lastMouseDownPos.x))
                {
                    isDrag_Vertical = true;
                }
                else
                {
                    isDrag_Vertical = false;
                }
            }
        }
        if (e.type == EventType.MouseDrag && isDrag)
        {
            Vector2 delta = e.mousePosition - lastMouseDownPos;
            lastMouseDownPos = e.mousePosition;
            if (isMouseDown_LogContent)
            {
                if (isDrag_Vertical)
                {
                    logScrollPos.y -= delta.y * DragRatio_Use;
                }
                else
                {
                    logScrollPos.x -= delta.x * DragRatio_Use;
                }
            }
            else if (isMouseDown_StackContent)
            {
                if (isDrag_Vertical)
                {
                    stackScrollPos.y -= delta.y * DragRatio_Use;
                }
                else
                {
                    stackScrollPos.x -= delta.x * DragRatio_Use;
                }
            }
        }
    }

    private void HandleScroll_Post()
    {
        Event e = Event.current;
        if ((e.type == EventType.MouseUp || clickLogBtn) && (isDrag || isMouseDown_LogContent || isMouseDown_StackContent))
        {
            isDrag = false;
            isMouseDown_LogContent = false;
            isMouseDown_StackContent = false;
        }
    }

    private void SetSpace(float space, float extraSpace = 0)
    {
        GUILayout.Space(extraSpace + space * pxRatio);
    }

    /// <summary>
    /// 计算像素比例
    /// </summary>
    private void CalcPxRatio()
    {
        float widthRatio = Screen.width * 1f / RefResolution.x;
        float heightRatio = Screen.height * 1f / RefResolution.y;
        pxRatio = Mathf.Min(widthRatio, heightRatio);
        pxRatio_V = heightRatio;
    }

    private void SetBlockClickState(bool forceDisable = false)
    {
        if (forceDisable)
        {
            //FindObjectOfType<EventSystem>(true)?.gameObject.SetActive(true);
            debuggerCanvas?.gameObject?.SetActive(false);
        }
        else
        {
            //FindObjectOfType<EventSystem>(true)?.gameObject.SetActive(!blockClick);
            if (blockClick)
            {
                CreateDebuggerTopCanvas();
            }
            debuggerCanvas?.gameObject?.SetActive(blockClick);
        }
    }

    private Canvas debuggerCanvas;
    private void CreateDebuggerTopCanvas()
    {
        if (debuggerCanvas != null)
            return;
        GameObject debuggerCanvasGo = new GameObject("DebuggerCanvas");
        DontDestroyOnLoad(debuggerCanvasGo);
        debuggerCanvas = debuggerCanvasGo.AddComponent<Canvas>();
        debuggerCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        debuggerCanvas.sortingOrder = 11111;
        debuggerCanvasGo.AddComponent<GraphicRaycaster>();
        debuggerCanvasGo.layer = LayerMask.NameToLayer("UI");
        GameObject imgGo = new GameObject();
        imgGo.transform.SetParent(debuggerCanvasGo.transform, false);
        var img = imgGo.AddComponent<Image>();
        img.color = Color.clear;
        RectTransform rect = imgGo.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        imgGo.layer = LayerMask.NameToLayer("UI");
        DontDestroyOnLoad(debuggerCanvas.gameObject);
    }

    public enum ELogType
    {
        Error_Real, //真实的error
        Error_Log, //error级别的log
        Assert,
        Warning,
        Log,
        Exception,
    }
}