using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Config;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class CheckTool
{
    /// <summary>
    /// 检查配表能否正常解析
    /// </summary>
    [MenuItem(EditorConst.CheckConfigCanBeParsed, false, EditorConst.Priority_CheckConfigCanBeParsed)]
    private static void CheckConfigCanBeParsed()
    {
        // 获取所有ConfigBase的子类
        var assembly = Assembly.GetAssembly(typeof(ConfigBase));
        var classList = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ConfigBase)))
            .ToList();
        Dictionary<string, Type> classDict = new Dictionary<string, Type>();
        foreach (var c in classList)
        {
            var key = c.Name.Replace("Table_", "").ToLower();
            classDict.TryAdd(key, c);
        }
        // 获取选择路径下的所有配置的json文件路径
        List<string> configJsonFileList = new List<string>();
        var jsonFilePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.JsonResSuffix);
        foreach (var jsonFilePath in jsonFilePathList)
        {
            var jsonFileName = Path.GetFileNameWithoutExtension(jsonFilePath);
            if (!classDict.ContainsKey(jsonFileName))
                continue;
            configJsonFileList.Add(jsonFilePath);
        }
        // 开始检查
        bool pass = true;
        int tempNum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < configJsonFileList.Count; i++)
        {
            try
            {
                tempNum = i;
                var filePath = configJsonFileList[i];
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (EditorUtility.DisplayCancelableProgressBar("正在检查",
                        $"正在检查{fileName} {i + 1}/{configJsonFileList.Count}",
                        (i + 1) * 1f / configJsonFileList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                var ta = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
                if (ta == null)
                {
                    sb.AppendLine($"配置文件{fileName}加载失败，请检查路径是否正确");
                    pass = false;
                    continue;
                }
                var type = classDict[fileName];
                var listType = typeof(List<>).MakeGenericType(type);
                var obj = JsonConvert.DeserializeObject(ta.text, listType);
                if (obj == null)
                {
                    sb.AppendLine($"配置文件{fileName}解析失败，请检查json格式是否正确");
                    pass = false;
                    continue;
                }
            }
            catch (Exception e)
            {
                sb.AppendLine($"配置文件{Path.GetFileNameWithoutExtension(configJsonFileList[tempNum])}解析失败，请检查json格式是否正确");
                pass = false;
                EditorUtility.ClearProgressBar();
            }
        }

        string retStr = $"共查找到 {configJsonFileList.Count} 个配置文件\n\n" +
                        (pass ? "所有配置都可正常解析" : $"检查到不可被解析的配置：\n{sb}");
        EditorUtility.ClearProgressBar();
        EditorUtils.ShowDialogWindow("检查配表能否正常解析 完成",
            retStr);
    }

    /// <summary>
    /// 检查Missing脚本
    /// </summary>
    [MenuItem(EditorConst.CheckMissingScripts, false, EditorConst.Priority_CheckMissingScripts)]
    private static void CheckMissingScripts()
    {
        List<string> missingScriptInfo = new List<string>();
        StringBuilder sb = new StringBuilder();

        // 获取所有预制体文件
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int totalCount = prefabGuids.Length;
        int currentIndex = 0;

        try
        {
            // 检查预制体中的missing脚本
            foreach (string guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                currentIndex++;

                if (EditorUtility.DisplayCancelableProgressBar("检查Missing脚本",
                    $"正在检查预制体: {Path.GetFileName(path)} ({currentIndex}/{totalCount})",
                    (float)currentIndex / totalCount))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    CheckMissingScriptsInGameObject(prefab, path, missingScriptInfo);
                }
            }

            EditorUtility.ClearProgressBar();

            // 生成报告
            if (missingScriptInfo.Count == 0)
            {
                sb.AppendLine("恭喜！没有发现任何Missing脚本。");
            }
            else
            {
                sb.AppendLine($"发现 {missingScriptInfo.Count} 个Missing脚本：\n");
                foreach (string info in missingScriptInfo)
                {
                    sb.AppendLine(info);
                }
            }

            EditorUtils.ShowDialogWindow("检查Missing脚本完成", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow("检查Missing脚本失败", $"检查过程中出现错误：{e.Message}");
        }
    }

    /// <summary>
    /// 检查GameObject及其子对象中的missing脚本
    /// </summary>
    private static void CheckMissingScriptsInGameObject(GameObject go, string assetPath, List<string> missingScriptInfo)
    {
        // 检查当前对象的组件
        Component[] components = go.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component == null)
            {
                // 检测到完全丢失的组件
                string objectPath = GetGameObjectPath(go);
                missingScriptInfo.Add($"{assetPath} -> {objectPath} (完全丢失的组件)");
                continue;
            }

            // 使用SerializedObject检测脚本引用是否丢失
            SerializedObject serializedObject = new SerializedObject(component);
            SerializedProperty scriptProperty = serializedObject.FindProperty("m_Script");

            if (scriptProperty != null && scriptProperty.objectReferenceValue == null)
            {
                // 脚本引用丢失
                string objectPath = GetGameObjectPath(go);
                string componentType = component.GetType().Name;
                missingScriptInfo.Add($"{assetPath} -> {objectPath} -> {componentType} (脚本引用丢失)");
            }
        }

        // 递归检查子对象
        for (int i = 0; i < go.transform.childCount; i++)
        {
            CheckMissingScriptsInGameObject(go.transform.GetChild(i).gameObject, assetPath, missingScriptInfo);
        }
    }

    /// <summary>
    /// 获取GameObject的层级路径
    /// </summary>
    private static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        Transform current = go.transform;

        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }

        return path;
    }

    /// <summary>
    /// 计算C#代码行数
    /// </summary>
    [MenuItem(EditorConst.CalcCodeLineCount, false, EditorConst.Priority_CalcCodeLineCount)]
    private static void CalcCodeLineCount()
    {
        int lineCount = 0;
        var csFilePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.CsResSuffix);
        for (int i = 0; i < csFilePathList.Count; i++)
        {
            try
            {
                var filePath = csFilePathList[i];
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (EditorUtility.DisplayCancelableProgressBar("正在计算",
                        $"正在计算{fileName} {i + 1}/{csFilePathList.Count}",
                        (i + 1) * 1f / csFilePathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                var lineArray = File.ReadAllLines(filePath);
                lineCount += lineArray.Length;
            }
            catch (Exception e)
            {
                EditorUtility.ClearProgressBar();
            }
        }
        EditorUtility.ClearProgressBar();
        EditorUtils.ShowDialogWindow("计算C#代码行数 完成",
            $"共查找到 {csFilePathList.Count} 个C#文件，代码总行数：{lineCount} ");
    }

    private static string[] accessSymbolArray = new string[]
    {
        "public", "private", "protected", "internal"
    };
    /// <summary>
    /// 计算C#代码方法数量
    /// </summary>
    [MenuItem(EditorConst.CalcMethodCount, false, EditorConst.Priority_CalcMethodCount)]
    private static void CalcMethodCount()
    {
        int methodCount = 0;
        var csFilePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.CsResSuffix);
        for (int i = 0; i < csFilePathList.Count; i++)
        {
            try
            {
                var filePath = csFilePathList[i];
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (EditorUtility.DisplayCancelableProgressBar("正在计算",
                        $"正在计算{fileName} {i + 1}/{csFilePathList.Count}",
                        (i + 1) * 1f / csFilePathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                var lineArray = File.ReadAllLines(filePath);
                foreach (var line in lineArray)
                {
                    if (accessSymbolArray.Any(s => line.Contains(s))
                        && line.Contains("("))
                    {
                        methodCount++;
                    }
                }
            }
            catch (Exception e)
            {
                EditorUtility.ClearProgressBar();
            }
        }
        EditorUtility.ClearProgressBar();
        EditorUtils.ShowDialogWindow("计算C#代码方法数量 完成",
            $"共查找到 {csFilePathList.Count} 个C#文件，方法总数：{methodCount} ");
    }

    /// <summary>
    /// 计算C#代码类数量
    /// </summary>
    [MenuItem(EditorConst.CalcClassCount, false, EditorConst.Priority_CalcClassCount)]
    private static void CalcClassCount()
    {
        int classCount = 0;
        var csFilePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.CsResSuffix);
        for (int i = 0; i < csFilePathList.Count; i++)
        {
            try
            {
                var filePath = csFilePathList[i];
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (EditorUtility.DisplayCancelableProgressBar("正在计算",
                        $"正在计算{fileName} {i + 1}/{csFilePathList.Count}",
                        (i + 1) * 1f / csFilePathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                var lineArray = File.ReadAllLines(filePath);
                foreach (var line in lineArray)
                {
                    if (line.Contains("class ")
                        || line.Contains("interface "))
                    {
                        classCount++;
                    }
                }
            }
            catch (Exception e)
            {
                EditorUtility.ClearProgressBar();
            }
        }
        EditorUtility.ClearProgressBar();
        EditorUtils.ShowDialogWindow("计算C#代码类数量 完成",
            $"共查找到 {csFilePathList.Count} 个C#文件，类总数：{classCount} ");
    }

    /// <summary>
    /// 计算C#文件数量
    /// </summary>
    [MenuItem(EditorConst.CalcFileCount, false, EditorConst.Priority_CalcFileCount)]
    private static void CalcFileCount()
    {
        var csFilePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.CsResSuffix);
        EditorUtils.ShowDialogWindow("计算C#代码文件数量 完成",
            $"文件总数：{csFilePathList.Count} ");
    }
}