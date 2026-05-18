/// <summary>
/// 编辑器常量
/// </summary>
public class EditorConst
{
    private const int BeginPriority_CommonTool = 0;
    private const int BeginPriority_ResTool = 1000;
    private const int BeginPriority_UITool = 2000;
    private const int BeginPriority_EditorTool = 3000;
    private const int BeginPriority_CheckTool = 4000;
    private const int PerMax = 900;

    private const string TOOLBAR_COMMONTOOL = "游戏工具/通用工具/";
    private const string TOOLBAR_RESTOOL = "游戏工具/资源工具/";
    private const string TOOLBAR_UITOOL = "游戏工具/UI工具/";
    private const string TOOLBAR_EDITORTOOL = "游戏工具/编辑器工具/";
    private const string TOOLBAR_CHECKTOOL = "游戏工具/检查工具/";

    // Priority
    public const int Priority_ChangeToBootScene = BeginPriority_CommonTool;

    public const int Priority_GenResourceName = BeginPriority_ResTool + 1;
    public const int Priority_GenAudioName = BeginPriority_ResTool + 2;
    public const int Priority_OpenResourceSettingWindow = BeginPriority_ResTool + 20;
    public const int Priority_RemoveMonoBehavioursWithMissingScript = BeginPriority_ResTool + 80;
    public const int Priority_DeleteAnimator = BeginPriority_ResTool + 100;
    public const int Priority_ReplaceButton2GameButton = BeginPriority_ResTool + 150;
    public const int Priority_AddAnimationClipToNest = BeginPriority_ResTool + 200;
    public const int Priority_DeleteNestAnimationClip = BeginPriority_ResTool + 201;
    public const int Priority_OptimizeAnimationClip = BeginPriority_ResTool + 202;
    public const int Priority_GenerateDiffFile = BeginPriority_ResTool + 500;

    public const int Priority_GenUIViewName = BeginPriority_UITool + 1;
    public const int Priority_GenUIView = BeginPriority_UITool + 20;
    public const int Priority_GenUISubView = BeginPriority_UITool + 21;
    public const int Priority_GenUIWidget = BeginPriority_UITool + 22;
    public const int Priority_OpenGenUIInfoDir = BeginPriority_UITool + PerMax;

    public const int Priority_ClearProgressBar = BeginPriority_EditorTool + PerMax;

    public const int Priority_CheckConfigCanBeParsed = BeginPriority_CheckTool + 1;
    public const int Priority_CheckMissingScripts = BeginPriority_CheckTool + 2;
    public const int Priority_CalcCodeLineCount = BeginPriority_CheckTool + PerMax - 3;
    public const int Priority_CalcMethodCount = BeginPriority_CheckTool + PerMax - 2;
    public const int Priority_CalcClassCount = BeginPriority_CheckTool + PerMax - 1;
    public const int Priority_CalcFileCount = BeginPriority_CheckTool + PerMax;

    //自定义工具栏
    public const string ChangeToBootScene = TOOLBAR_COMMONTOOL + "切换到启动场景";

    public const string GenResourceName = TOOLBAR_RESTOOL + "生成ResourceName";
    public const string GenAudioName = TOOLBAR_RESTOOL + "生成AudioName";
    public const string OpenResourceSettingWindow = TOOLBAR_RESTOOL + "打开资源设置窗口";
    public const string RemoveMonoBehavioursWithMissingScript = TOOLBAR_RESTOOL + "移除预制体上丢失的脚本";
    public const string DeleteAnimator = TOOLBAR_RESTOOL + "删除Animator组件";
    public const string ReplaceButton2GameButton = TOOLBAR_RESTOOL + "替换组件 Button->GameButton";
    public const string AddAnimationClipToNest = TOOLBAR_RESTOOL + "/动画工具/将动画片段内嵌至指定状态机";
    public const string DeleteNestAnimationClip = TOOLBAR_RESTOOL + "/动画工具/删除状态机内嵌动画片段";
    public const string OptimizeAnimationClip = TOOLBAR_RESTOOL + "/动画工具/优化动画片段";
    public const string GenerateDiffFile = TOOLBAR_RESTOOL + "生成差异文件";

    public const string GenUIViewName = TOOLBAR_UITOOL + "生成UIViewName";
    public const string GenUIView = TOOLBAR_UITOOL + "生成UIView";
    public const string GenUISubView = TOOLBAR_UITOOL + "生成UISubView";
    public const string GenUIWidget = TOOLBAR_UITOOL + "生成UIWidget";
    public const string OpenGenUIInfoDir = TOOLBAR_UITOOL + "打开生成UI信息文件夹";

    public const string ClearProgressBar = TOOLBAR_EDITORTOOL + "ClearProgressBar";

    public const string CheckConfigCanBeParsed = TOOLBAR_CHECKTOOL + "检查配表能否正常解析";
    public const string CheckMissingScripts = TOOLBAR_CHECKTOOL + "检查Missing脚本";
    public const string CalcCodeLineCount = TOOLBAR_CHECKTOOL + "计算C#代码行数";
    public const string CalcMethodCount = TOOLBAR_CHECKTOOL + "计算C#代码方法数量";
    public const string CalcClassCount = TOOLBAR_CHECKTOOL + "计算C#代码类数量";
    public const string CalcFileCount = TOOLBAR_CHECKTOOL + "计算C#代码文件数量";

    // 常量
    public const string SUFFIX_CS = ".cs";
    public const string ProjectRoot = "Assets";
    public static readonly string[] PrefabResSuffix = new string[]
    {
        "*.prefab"
    };
    public static readonly string[] TextureResSuffix = new string[]
    {
        "*.png", "*.jpg", "*.jpeg", "*.tga", "*.psd", "*.tiff", "*.bmp"
    };
    public static readonly string[] SpriteAtlasResSuffix = new string[]
    {
        "*.spriteAtlasv2"
    };
    public static readonly string[] JsonResSuffix = new string[]
    {
        "*.json"
    };
    public static readonly string[] CsResSuffix = new string[]
    {
        "*.cs"
    };
    public static readonly string[] AnimatorControllerResSuffix = new string[]
    {
        "*.controller"
    };
    public static readonly string[] AnimationClipResSuffix = new string[]
    {
        "*.anim"
    };
}