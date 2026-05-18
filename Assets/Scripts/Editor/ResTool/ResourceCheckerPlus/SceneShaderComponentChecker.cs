using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneShaderComponentChecker : EditorWindow
{
    private List<GameObject> missingComponentObjList = new List<GameObject>();
    private List<GameObject> standardShaderObjList = new List<GameObject>();
    Vector2 scrollPos1 = Vector2.zero;
    Vector2 scrollPos2 = Vector2.zero;

    public static bool onlyCheckMapNode = true;

    [MenuItem("Custom/Check Component and Shader")]
    public static void Init()
    {
        var checker = GetWindow<SceneShaderComponentChecker>();
        checker.CheckSceneMissingComponentAndStandardShader();
    }

    public void CheckSceneMissingComponentAndStandardShader()
    {
        missingComponentObjList.Clear();
        standardShaderObjList.Clear();
        var scene = SceneManager.GetActiveScene();
        var rootObjects = scene.GetRootGameObjects();
        foreach(var go in rootObjects)
        {
            if (go == null)
                continue;
            if (onlyCheckMapNode && go.name.ToLower() != "map")
                continue;
            CheckSceneMissingComponent(go, go);
            CheckSceneStandardShader(go);
        }
    }

    private void CheckSceneMissingComponent(GameObject rootObj, GameObject checkObject)
    {
        var rootTran = checkObject.transform;
        var coms = rootTran.GetComponents<Component>();
        foreach (var com in coms)
        {
            if (com == null)
                missingComponentObjList.Add(checkObject);
        }
        for (int i = 0; i < rootTran.childCount; i++)
        {
            CheckSceneMissingComponent(rootObj, rootTran.GetChild(i).gameObject);
        }
    }

    public void CheckSceneStandardShader(GameObject rootObject)
    {
        var renderers = rootObject.GetComponentsInChildren<Renderer>(true);
        foreach(var r in renderers)
        {
            foreach(var m in r.sharedMaterials)
            {
                if (m != null && m.shader != null && m.shader.name == "Standard")
                    standardShaderObjList.Add(r.gameObject);
            }
        }
    }

    public void OnGUI()
    {
        onlyCheckMapNode = GUILayout.Toggle(onlyCheckMapNode, "仅检查Map节点下内容");
        var oriGUIColor = GUI.color;
        if (GUILayout.Button("重新检查"))
        {
            CheckSceneMissingComponentAndStandardShader();
        }
        ShowMissingComentResult();
        ShowStandardShaderResult();
        GUI.color = oriGUIColor;
    }

    private void ShowMissingComentResult()
    {
        var componentLabel = "无空脚本";
        GUI.color = Color.green;
        if (missingComponentObjList.Count > 0)
        {
            componentLabel = "空脚本个数：" + missingComponentObjList.Count;
            GUI.color = Color.red;
        }
        if (GUILayout.Button(componentLabel))
        {
            Selection.objects = missingComponentObjList.ToArray();
        }
        scrollPos1 = GUILayout.BeginScrollView(scrollPos1);
        foreach (var obj in missingComponentObjList)
        {
            if (obj == null)
                continue;
            GUILayout.BeginHorizontal();
            GUILayout.Space(40);
            if (GUILayout.Button(obj.name))
            {
                Selection.objects = new Object[] { obj };
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }

    private void ShowStandardShaderResult()
    {
        var standardShaderLabel = "无Standard Shader";
        GUI.color = Color.green;
        if (standardShaderObjList.Count > 0)
        {
            standardShaderLabel = "Standard Shader个数：" + standardShaderObjList.Count;
            GUI.color = Color.red;
        }
        if (GUILayout.Button(standardShaderLabel))
        {
            Selection.objects = standardShaderObjList.ToArray();
        }

        scrollPos2 = GUILayout.BeginScrollView(scrollPos2);
        foreach (var obj in standardShaderObjList)
        {
            if (obj == null)
                continue;
            GUILayout.BeginHorizontal();
            GUILayout.Space(40);
            if (GUILayout.Button(obj.name))
            {
                Selection.objects = new Object[] { obj };
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }

    
}
