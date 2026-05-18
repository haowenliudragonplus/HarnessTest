using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[InitializeOnLoad]
public static class SyncToolbarButtons
{
    private const string MainToArtScriptName = "rsync_Main2Art.sh";
    private const string CodeToArtScriptName = "rsync_Code2Art.sh";
    private const string ArtToMainScriptName = "rsync_Art2Main.sh";
    private const string SyncToolRelativePath = @"Toolchain\SyncTool";
    private const string ConfigToolRelativePath = @"Toolchain\ConfigTool";
    private const string ToolbarGroupContainerName = "SyncToolbarButtons_Group";

    private const float ToolbarGroupHeight = 44f;
    private const float ToggleColumnWidth = 90f;
    private const float MainButtonWidth = 146f;
    private const float ArtButtonWidth = 146f;
    private const float ExportButtonWidth = 124f;
    private const float ToolbarButtonHeight = 22f;
    private const float ToolbarLeftInset = 8f;
    private const float ToolbarRightInset = 8f;
    private const float ToggleToMainButtonGap = 2f;
    private const float MainToArtButtonGap = 16f;
    private const float ExportButtonGap = MainToArtButtonGap;
    private const float ToolbarGroupWidth = ToolbarLeftInset
        + ToggleColumnWidth
        + ToggleToMainButtonGap
        + MainButtonWidth
        + MainToArtButtonGap
        + ArtButtonWidth
        + ExportButtonGap
        + ExportButtonWidth
        + ToolbarRightInset;

    private static readonly Color ToolbarTextColorPro = Color.white;
    private static readonly Color ToolbarTextColorLight = Color.black;

    private static readonly Type ToolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
    private static readonly FieldInfo ToolbarRootField = ToolbarType?.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly StringBuilder ProcessOutput = new StringBuilder(4096);

    private static ScriptableObject currentToolbar;
    private static bool toolbarInstalled;
    private static bool missingToolbarLogged;
    private static Process runningProcess;
    private static string runningActionName;
    private static string runningWorkingDirectory;
    private static ToolbarCommand[] runningCommands;
    private static int runningCommandIndex;

    private static bool syncCodeInMainToArt;
    private static bool syncAllCodeInMainToArt;

    private static GUIStyle toolbarButtonStyle;
    private static GUIStyle toolbarToggleStyle;

    private readonly struct ToolbarCommand
    {
        public ToolbarCommand(string scriptName, string arguments = null)
        {
            ScriptName = scriptName;
            Arguments = arguments;
        }

        public string ScriptName { get; }
        public string Arguments { get; }

        public string DisplayName => string.IsNullOrWhiteSpace(Arguments) ? ScriptName : $"{ScriptName} {Arguments}";
    }

    static SyncToolbarButtons()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    private static GUIStyle ToolbarButtonStyle
    {
        get
        {
            if (toolbarButtonStyle != null)
            {
                return toolbarButtonStyle;
            }

            GUIStyle baseStyle = null;
            try
            {
                baseStyle = EditorStyles.toolbarButton;
            }
            catch (NullReferenceException)
            {
                // Unity may construct InitializeOnLoad classes before editor styles are ready.
            }

            baseStyle ??= GUI.skin?.button;
            toolbarButtonStyle = new GUIStyle(baseStyle ?? GUIStyle.none)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedHeight = ToolbarButtonHeight,
                padding = new RectOffset(10, 10, 0, 0),
                clipping = TextClipping.Clip,
                fontStyle = FontStyle.Bold,
            };
            ApplyToolbarTextColor(toolbarButtonStyle);
            return toolbarButtonStyle;
        }
    }

    private static GUIStyle ToolbarToggleStyle
    {
        get
        {
            if (toolbarToggleStyle != null)
            {
                return toolbarToggleStyle;
            }

            GUIStyle baseStyle = null;
            try
            {
                baseStyle = EditorStyles.miniLabel;
            }
            catch (NullReferenceException)
            {
                // Unity may construct InitializeOnLoad classes before editor styles are ready.
            }

            baseStyle ??= GUI.skin?.label;
            toolbarToggleStyle = new GUIStyle(baseStyle ?? GUIStyle.none)
            {
                alignment = TextAnchor.UpperLeft,
                clipping = TextClipping.Clip,
                fontStyle = FontStyle.Bold,
                fontSize = 10,
            };
            ApplyToolbarTextColor(toolbarToggleStyle);
            return toolbarToggleStyle;
        }
    }

    private static void OnEditorUpdate()
    {
        TryInstallToolbarButtons();
        UpdateRunningProcess();
    }

    private static void TryInstallToolbarButtons()
    {
        if (ToolbarType == null || ToolbarRootField == null)
        {
            return;
        }

        Object[] toolbars = Resources.FindObjectsOfTypeAll(ToolbarType);
        if (toolbars == null || toolbars.Length == 0)
        {
            return;
        }

        ScriptableObject toolbar = toolbars[0] as ScriptableObject;
        if (toolbar == null)
        {
            return;
        }

        if (currentToolbar != toolbar)
        {
            currentToolbar = toolbar;
            toolbarInstalled = false;
        }

        VisualElement root = ToolbarRootField.GetValue(toolbar) as VisualElement;
        if (root == null)
        {
            return;
        }

        if (toolbarInstalled && root.Q(ToolbarGroupContainerName) != null)
        {
            return;
        }

        bool installed = TryInstallOnRightToolbar(root);
        toolbarInstalled = installed && root.Q(ToolbarGroupContainerName) != null;
        if (toolbarInstalled)
        {
            missingToolbarLogged = false;
        }
        else if (missingToolbarLogged == false)
        {
            Debug.LogWarning($"[{nameof(SyncToolbarButtons)}] Failed to locate the left toolbar insertion point.");
            missingToolbarLogged = true;
        }
    }

    private static bool TryInstallOnRightToolbar(VisualElement root)
    {
        VisualElement rightZone = root.Q("ToolbarZoneRightAlign");
        if (rightZone == null)
        {
            return false;
        }

        if (rightZone.Q(ToolbarGroupContainerName) == null)
        {
            rightZone.Add(CreateToolbarContainer(ToolbarGroupContainerName, DrawToolbarGroupGUI, ToolbarGroupWidth, ToolbarGroupHeight));
        }

        return rightZone.Q(ToolbarGroupContainerName) != null;
    }

    private static VisualElement CreateToolbarContainer(string containerName, Action onGUI, float containerWidth, float containerHeight)
    {
        VisualElement container = new VisualElement
        {
            name = containerName,
        };
        container.style.flexDirection = FlexDirection.Row;
        container.style.flexShrink = 0;
        container.style.alignSelf = Align.Center;
        container.style.width = containerWidth;
        container.style.minWidth = containerWidth;
        container.style.maxWidth = containerWidth;
        container.style.marginLeft = 0;
        container.style.marginRight = 2;
        container.style.marginTop = -6f;
        container.style.justifyContent = Justify.FlexStart;
        container.style.alignItems = Align.Center;
        container.style.overflow = Overflow.Visible;

        IMGUIContainer imguiContainer = new IMGUIContainer(() =>
        {
            onGUI?.Invoke();
        });
        imguiContainer.style.flexGrow = 0;
        imguiContainer.style.alignSelf = Align.Center;
        imguiContainer.style.width = containerWidth;
        imguiContainer.style.height = containerHeight;
        imguiContainer.style.overflow = Overflow.Visible;

        container.Add(imguiContainer);
        return container;
    }

    private static void ApplyToolbarTextColor(GUIStyle style)
    {
        Color textColor = EditorGUIUtility.isProSkin ? ToolbarTextColorPro : ToolbarTextColorLight;
        style.normal.textColor = textColor;
        style.hover.textColor = textColor;
        style.active.textColor = textColor;
        style.focused.textColor = textColor;
        style.onNormal.textColor = textColor;
        style.onHover.textColor = textColor;
        style.onActive.textColor = textColor;
        style.onFocused.textColor = textColor;
    }

    private static void DrawToolbarGroupGUI()
    {
        Rect groupRect = GUILayoutUtility.GetRect(
            ToolbarGroupWidth - 2f,
            ToolbarGroupHeight - 2f,
            GUILayout.Width(ToolbarGroupWidth - 2f),
            GUILayout.Height(ToolbarGroupHeight - 2f));

        const float toolbarLayoutUpNudge = 3f;
        float buttonY = groupRect.y + (groupRect.height - ToolbarButtonHeight) * 0.5f - toolbarLayoutUpNudge;
        float mainButtonX = groupRect.x + ToolbarLeftInset + ToggleColumnWidth + ToggleToMainButtonGap;
        float artButtonX = mainButtonX + MainButtonWidth + MainToArtButtonGap;
        float exportButtonX = artButtonX + ArtButtonWidth + ExportButtonGap;

        Rect syncCodeToggleRect = new Rect(groupRect.x + ToolbarLeftInset, groupRect.y + 3f, ToggleColumnWidth - 6f, 14f);
        Rect syncAllCodeToggleRect = new Rect(groupRect.x + ToolbarLeftInset, groupRect.y + 19f, ToggleColumnWidth - 6f, 14f);
        Rect mainButtonRect = new Rect(mainButtonX, buttonY, MainButtonWidth, ToolbarButtonHeight);
        Rect artButtonRect = new Rect(artButtonX, buttonY, ArtButtonWidth, ToolbarButtonHeight);
        Rect exportButtonRect = new Rect(exportButtonX, buttonY, ExportButtonWidth, ToolbarButtonHeight);

        bool disabled = IsToolbarInteractionDisabled();
        using (new EditorGUI.DisabledScope(disabled))
        {
            syncCodeInMainToArt = EditorGUI.ToggleLeft(
                syncCodeToggleRect,
                new GUIContent("同步代码", "勾上后执行 rsync_Code2Art"),
                syncCodeInMainToArt,
                ToolbarToggleStyle);

            syncAllCodeInMainToArt = EditorGUI.ToggleLeft(
                syncAllCodeToggleRect,
                new GUIContent("全量同步代码", "勾上后执行 rsync_Code2Art all"),
                syncAllCodeInMainToArt,
                ToolbarToggleStyle);

            if (GUI.Button(mainButtonRect, new GUIContent("程序库同步美术库", BuildMainToArtTooltip()), ToolbarButtonStyle))
            {
                StartScriptProcess("程序库同步美术库", GetSyncToolDirectory(), BuildMainToArtCommands());
            }

            if (GUI.Button(artButtonRect, new GUIContent("美术库同步程序库", "执行 rsync_Art2Main"), ToolbarButtonStyle))
            {
                StartScriptProcess("美术库同步程序库", GetSyncToolDirectory(), new ToolbarCommand(ArtToMainScriptName));
            }

            if (EditorGUI.DropdownButton(exportButtonRect, new GUIContent("导出配置表"), FocusType.Passive, ToolbarButtonStyle))
            {
                ShowConfigToolMenu(exportButtonRect);
            }
        }
    }

    private static string BuildMainToArtTooltip()
    {
        if (syncAllCodeInMainToArt)
        {
            return "执行 rsync_Main2Art，然后执行 rsync_Code2Art all";
        }

        if (syncCodeInMainToArt)
        {
            return "执行 rsync_Main2Art，然后执行 rsync_Code2Art";
        }

        return "仅执行 rsync_Main2Art";
    }

    private static ToolbarCommand[] BuildMainToArtCommands()
    {
        List<ToolbarCommand> commands = new List<ToolbarCommand>
        {
            new ToolbarCommand(MainToArtScriptName),
        };

        if (syncAllCodeInMainToArt)
        {
            commands.Add(new ToolbarCommand(CodeToArtScriptName, "all"));
        }
        else if (syncCodeInMainToArt)
        {
            commands.Add(new ToolbarCommand(CodeToArtScriptName));
        }

        return commands.ToArray();
    }

    private static void ShowConfigToolMenu(Rect buttonRect)
    {
        string configToolDirectory = GetConfigToolDirectory();
        string[] scriptPaths = GetConfigToolScriptPaths(configToolDirectory);
        GenericMenu menu = new GenericMenu();

        if (scriptPaths.Length == 0)
        {
            menu.AddDisabledItem(new GUIContent("未找到 .sh 脚本"));
        }
        else
        {
            foreach (string scriptPath in scriptPaths)
            {
                string scriptName = Path.GetFileName(scriptPath);
                menu.AddItem(new GUIContent(scriptName), false, () =>
                {
                    StartScriptProcess($"导出配置表 - {scriptName}", configToolDirectory, new ToolbarCommand(scriptName));
                });
            }
        }

        menu.DropDown(buttonRect);
    }

    private static string[] GetConfigToolScriptPaths(string configToolDirectory)
    {
        if (Directory.Exists(configToolDirectory) == false)
        {
            return Array.Empty<string>();
        }

        string[] scriptPaths = Directory.GetFiles(configToolDirectory, "*.sh", SearchOption.TopDirectoryOnly);
        Array.Sort(scriptPaths, StringComparer.OrdinalIgnoreCase);
        return scriptPaths;
    }

    private static bool IsToolbarInteractionDisabled()
    {
        return runningProcess != null
            || runningCommands != null
            || EditorApplication.isCompiling
            || EditorApplication.isPlayingOrWillChangePlaymode;
    }

    private static void StartScriptProcess(string actionName, string workingDirectory, params ToolbarCommand[] commands)
    {
        if (commands == null || commands.Length == 0)
        {
            EditorUtility.DisplayDialog("同步失败", "未配置要执行的脚本。", "确定");
            return;
        }

        foreach (ToolbarCommand command in commands)
        {
            string scriptPath = Path.Combine(workingDirectory, command.ScriptName);
            if (File.Exists(scriptPath) == false)
            {
                EditorUtility.DisplayDialog("同步失败", $"未找到脚本：\n{scriptPath}", "确定");
                return;
            }
        }

        if (IsToolbarInteractionDisabled())
        {
            EditorUtility.DisplayDialog("同步进行中", $"{runningActionName} 正在执行，请稍后再试。", "确定");
            return;
        }

        try
        {
            lock (ProcessOutput)
            {
                ProcessOutput.Length = 0;
            }

            runningActionName = actionName;
            runningWorkingDirectory = workingDirectory;
            runningCommands = (ToolbarCommand[])commands.Clone();
            runningCommandIndex = 0;
            StartCurrentCommandProcess();
        }
        catch (Exception ex)
        {
            HandleStartProcessException(ex);
        }
    }

    private static void StartCurrentCommandProcess()
    {
        if (runningCommands == null || runningCommandIndex < 0 || runningCommandIndex >= runningCommands.Length)
        {
            throw new InvalidOperationException("No toolbar command is ready to run.");
        }

        ToolbarCommand command = runningCommands[runningCommandIndex];
        string scriptPath = Path.Combine(runningWorkingDirectory, command.ScriptName);
        Process process = CreateBashProcess(runningWorkingDirectory, command);
        process.OutputDataReceived += OnProcessOutput;
        process.ErrorDataReceived += OnProcessOutput;
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.StandardInput.Close();
        runningProcess = process;

        Debug.Log(
            $"[{nameof(SyncToolbarButtons)}] 开始执行：{runningActionName} ({runningCommandIndex + 1}/{runningCommands.Length})\n脚本：{scriptPath}\n参数：{(string.IsNullOrWhiteSpace(command.Arguments) ? "<none>" : command.Arguments)}");
    }

    private static Process CreateBashProcess(string workingDirectory, ToolbarCommand command)
    {
        string bashExecutable = FindBashExecutable();
        if (string.IsNullOrEmpty(bashExecutable))
        {
            throw new FileNotFoundException("Unable to find bash.exe. Please install Git Bash or add bash to PATH.");
        }

        string arguments = $"\"{command.ScriptName}\"";
        if (string.IsNullOrWhiteSpace(command.Arguments) == false)
        {
            arguments += $" {command.Arguments}";
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = bashExecutable,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
        };

        return new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = false,
        };
    }

    private static string FindBashExecutable()
    {
        string[] candidates =
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Git", "bin", "bash.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Git", "usr", "bin", "bash.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Git", "bin", "bash.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Git", "usr", "bin", "bash.exe"),
            "bash",
        };

        foreach (string candidate in candidates)
        {
            if (candidate == "bash" || File.Exists(candidate))
            {
                return candidate;
            }
        }

        return null;
    }

    private static string GetSyncToolDirectory()
    {
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
        return Path.Combine(projectRoot, SyncToolRelativePath);
    }

    private static string GetConfigToolDirectory()
    {
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
        return Path.Combine(projectRoot, ConfigToolRelativePath);
    }

    private static void UpdateRunningProcess()
    {
        if (runningProcess == null)
        {
            return;
        }

        float progress = Mathf.Repeat((float)(EditorApplication.timeSinceStartup * 0.35d), 1f);
        EditorUtility.DisplayProgressBar("执行中", GetRunningProgressMessage(), progress);

        if (runningProcess.HasExited == false)
        {
            return;
        }

        EditorUtility.ClearProgressBar();

        int exitCode = runningProcess.ExitCode;
        string finishedCommand = GetCurrentCommandDisplayName();
        runningProcess.Dispose();
        runningProcess = null;

        if (exitCode == 0 && runningCommands != null)
        {
            runningCommandIndex++;
            if (runningCommandIndex < runningCommands.Length)
            {
                try
                {
                    StartCurrentCommandProcess();
                    return;
                }
                catch (Exception ex)
                {
                    HandleStartProcessException(ex);
                    return;
                }
            }
        }

        string output;
        lock (ProcessOutput)
        {
            output = ProcessOutput.ToString().Trim();
            ProcessOutput.Length = 0;
        }

        if (string.IsNullOrEmpty(output) == false)
        {
            Debug.Log($"[{nameof(SyncToolbarButtons)}] {runningActionName} 输出：\n{output}");
        }

        if (exitCode == 0)
        {
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("执行完成", $"{runningActionName} 执行完成。", "确定");
        }
        else
        {
            Debug.LogError($"[{nameof(SyncToolbarButtons)}] {runningActionName} 执行失败，命令：{finishedCommand}，ExitCode={exitCode}\n{output}");
            EditorUtility.DisplayDialog(
                "执行失败",
                $"{runningActionName} 执行失败，命令：{finishedCommand}，ExitCode={exitCode}。\n详情请查看 Console。",
                "确定");
        }

        CleanupRunningState();
    }

    private static string GetRunningProgressMessage()
    {
        if (runningCommands == null || runningCommands.Length == 0)
        {
            return $"{runningActionName}...";
        }

        return $"{runningActionName} ({runningCommandIndex + 1}/{runningCommands.Length})...";
    }

    private static string GetCurrentCommandDisplayName()
    {
        if (runningCommands == null || runningCommandIndex < 0 || runningCommandIndex >= runningCommands.Length)
        {
            return string.Empty;
        }

        return runningCommands[runningCommandIndex].DisplayName;
    }

    private static void HandleStartProcessException(Exception ex)
    {
        string commandDisplayName = GetCurrentCommandDisplayName();
        string actionName = runningActionName;
        Debug.LogException(ex);
        CleanupRunningState();
        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog(
            "执行失败",
            $"无法启动脚本。\n命令：{commandDisplayName}\n操作：{actionName}\n请确认本机可用 bash/Git Bash。\n\n{ex.Message}",
            "确定");
    }

    private static void CleanupRunningState()
    {
        runningProcess = null;
        runningActionName = null;
        runningWorkingDirectory = null;
        runningCommands = null;
        runningCommandIndex = 0;
    }

    private static void OnProcessOutput(object sender, DataReceivedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.Data))
        {
            return;
        }

        lock (ProcessOutput)
        {
            ProcessOutput.AppendLine(e.Data);
        }
    }
}
