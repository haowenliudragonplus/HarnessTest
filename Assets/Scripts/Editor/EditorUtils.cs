using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    /// <summary>
    /// 显示对话框
    /// </summary>
    public static void ShowDialogWindow(string title, string content, string btn1Name = "确定")
    {
        CommonDialogWindow.CreateWindow(title, content, btn1Name);
    }

    /// <summary>
    /// 获取选择的所有路径
    /// </summary>
    public static string[] GetSelectPathArray()
    {
        string[] assetGUIDs = Selection.assetGUIDs;
        string[] assetPaths = new string[assetGUIDs.Length];
        for (int i = 0; i < assetGUIDs.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            assetPaths[i] = assetPath;
        }
        return assetPaths;
    }

    /// <summary>
    /// 获取所有选择的文件
    /// </summary>
    /// searchPatternArray：*.asset
    public static List<string> GetSelectedFileList(SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] searchPatternArray)
    {
        List<string> fileList = new List<string>();
        string[] assetGuidArray = Selection.assetGUIDs;
        foreach (var assetGuid in assetGuidArray)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            fileList.Add(assetPath);
        }
        var ret = GetFileList(fileList, searchOption, searchPatternArray);
        return ret;
    }

    /// <summary>
    /// 获取某个路径列表中的文件
    /// </summary>
    /// searchPatternArray：*.asset
    public static List<string> GetFileList(List<string> pathList, SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] searchPatternArray)
    {
        var ret = new HashSet<string>();
        foreach (var path in pathList)
        {
            if (IOUtils.IsFilePath(path))
            {
                if (searchPatternArray == null || searchPatternArray.Length == 0)
                {
                    ret.Add(path);
                }
                else
                {
                    if (searchPatternArray.Any(pattern => pattern.Contains(Path.GetExtension(path))))
                    {
                        ret.Add(path);
                    }
                }
            }
            else
            {
                if (searchPatternArray == null || searchPatternArray.Length == 0)
                {
                    string[] filePathArray = Directory.GetFiles(path, "*", searchOption);
                    foreach (var filePath in filePathArray)
                    {
                        ret.Add(filePath);
                    }
                }
                else
                {
                    foreach (var searchPattern in searchPatternArray)
                    {
                        string[] filePathArray = Directory.GetFiles(path, searchPattern, searchOption);
                        foreach (var filePath in filePathArray)
                        {
                            ret.Add(filePath);
                        }
                    }
                }
            }
        }
        return ret.ToList();
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
    public static string GenCurDateTimeStr()
    {
        string dateTimeStr = DateTime.Now.ToString("yyyy-M-d H:m:s");
        return dateTimeStr;
    }

    /// <summary>
    /// 编辑器下验证是否选择了一个物体
    /// </summary>
    public static bool ValidateSelectObject()
    {
        var selectedGUIDs = Selection.assetGUIDs;
        if (selectedGUIDs == null)
            return false;
        return true;
    }

    /// <summary>
    /// 编辑器下验证是否选择了某一个类型的物体
    /// </summary>
    public static bool ValidateSelectObject<T>()
        where T : UnityEngine.Object
    {
        var selectedObj = Selection.activeObject;
        if (selectedObj == null)
            return false;
        var ret = selectedObj as T;
        return ret != null;
    }

    /// <summary>
    /// 删除某个GameObject下所有子物体上missing的脚本
    /// </summary>
    public static int DeleteAllMissingMonoBehaviour(GameObject go, int removeCount)
    {
        removeCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            removeCount += DeleteAllMissingMonoBehaviour(go.transform.GetChild(i).gameObject, removeCount);
        }
        return removeCount;
    }

    /// <summary>
    /// 删除某个组件
    /// </summary>
    public static void DeleteComponent<T>(List<string> pathList)
        where T : Component
    {
        try
        {
            List<string> modifyPrefabList = new List<string>();
            for (int i = 0; i < pathList.Count; i++)
            {
                string prefabPath = pathList[i];
                if (EditorUtility.DisplayCancelableProgressBar($"删除{typeof(T).Name}", $"正在删除{Path.GetFileName(prefabPath)}中的{typeof(T).Name}组件", (i + 1) * 1f / pathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                GameObject instance = PrefabUtility.LoadPrefabContents(prefabPath);
                T[] comArray = instance.GetComponentsInChildren<T>(true);
                if (comArray == null || comArray.Length <= 0)
                    continue;
                bool hasModify = false;
                for (int k = 0; k < comArray.Length; k++)
                {
                    hasModify = true;
                    Transform componentTrans = comArray[k].transform;
                    UnityEngine.Object.DestroyImmediate(comArray[k]);
                }
                if (hasModify)
                {
                    PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    modifyPrefabList.Add(Path.GetFileName(prefabPath));
                }
                PrefabUtility.UnloadPrefabContents(instance);
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"删除组件{typeof(T).Name}成功，共操作" + modifyPrefabList.Count + "个预制体：");
            foreach (var modifyPrefab in modifyPrefabList)
            {
                sb.AppendLine(modifyPrefab);
            }
            EditorUtils.ShowDialogWindow($"删除组件{typeof(T).Name}成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtils.ShowDialogWindow($"删除组件{typeof(T).Name}失败", e.ToString());
            EditorUtility.ClearProgressBar();
        }
    }

    /// <summary>
    /// 替换某个组件
    /// </summary>
    public static void ReplaceComponent<T1, T2>(List<string> pathList)
        where T1 : Component
        where T2 : Component
    {
        try
        {
            List<string> modifyPrefabList = new List<string>();
            for (int i = 0; i < pathList.Count; i++)
            {
                string prefabPath = pathList[i];
                if (EditorUtility.DisplayCancelableProgressBar($"替换{typeof(T1).Name}", $"正在替换{Path.GetFileName(prefabPath)}中的{typeof(T1).Name}组件为{typeof(T2).Name}组件", (i + 1) * 1f / pathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                GameObject instance = PrefabUtility.LoadPrefabContents(prefabPath);
                T1[] comArray = instance.GetComponentsInChildren<T1>(true);
                if (comArray == null || comArray.Length <= 0)
                    continue;
                bool hasModify = false;
                for (int k = 0; k < comArray.Length; k++)
                {
                    if (comArray[k].GetType() != typeof(T1))
                        continue;
                    hasModify = true;
                    Transform componentTrans = comArray[k].transform;
                    UnityEngine.Object.DestroyImmediate(comArray[k]);
                    componentTrans.gameObject.AddComponent<T2>();
                }
                if (hasModify)
                {
                    PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    modifyPrefabList.Add(Path.GetFileName(prefabPath));
                }
                PrefabUtility.UnloadPrefabContents(instance);
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"替换组件 {typeof(T1).Name} -> {typeof(T2).Name}成功，共操作" + modifyPrefabList.Count + "个预制体：");
            foreach (var modifyPrefab in modifyPrefabList)
            {
                sb.AppendLine(modifyPrefab);
            }
            EditorUtils.ShowDialogWindow($"替换组件 {typeof(T1).Name} -> {typeof(T2).Name}成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtils.ShowDialogWindow($"替换组件 {typeof(T1).Name} -> {typeof(T2).Name}失败", e.ToString());
            EditorUtility.ClearProgressBar();
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    public static void DeleteFile(params string[] searchPatternArray)
    {
        try
        {
            var deleteFileList = new List<string>();
            var filePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, searchPatternArray);
            for (int i = 0; i < filePathList.Count; i++)
            {
                string filePath = filePathList[i];
                if (EditorUtility.DisplayCancelableProgressBar("正在删除文件", $"正在删除{Path.GetFileNameWithoutExtension(filePath)} {i + 1}/{filePathList.Count}", (i + 1) * 1f / filePathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                File.Delete(filePath);
                deleteFileList.Add(Path.GetFileNameWithoutExtension(filePath));
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"删除文件成功，共删除" + deleteFileList.Count + "个文件：");
            foreach (var file in deleteFileList)
            {
                sb.AppendLine(file);
            }
            EditorUtils.ShowDialogWindow("删除文件成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow("删除文件失败", e.ToString());
        }
    }
}