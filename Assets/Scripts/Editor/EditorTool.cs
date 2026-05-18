using System;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class EditorTool
{
    [MenuItem(EditorConst.ChangeToBootScene + " %#&p", priority = EditorConst.Priority_ChangeToBootScene)]
    private static void ChangeToBootScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/BootScene.unity");
    }

    [MenuItem(EditorConst.ClearProgressBar, priority = EditorConst.Priority_ClearProgressBar)]
    private static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
}