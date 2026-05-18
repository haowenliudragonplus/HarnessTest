using UnityEditor;
using UnityEngine;

/// <summary>
/// 生成UI结果编辑器窗口
/// </summary>
public class GenerateUIResultWindow : EditorWindow
{
    private Vector2 scrollPosition = Vector2.zero;

    public string content;

    public static void CreateWindow(string title, string content)
    {
        GenerateUIResultWindow window = GetWindow<GenerateUIResultWindow>(true, title, true);
        window.content = content;
        window.Show();
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        string[] contentArray = content.Split('\n');
        foreach (var content in contentArray)
        {
            EditorGUILayout.SelectableLabel(content, GUILayout.Height(15));
        }
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("确定"))
        {
            Close();
        }
        if (GUILayout.Button("打开生成UI信息文件夹"))
        {
            UIEditorTool.OpenGenUIInfoDir();
        }
    }
}