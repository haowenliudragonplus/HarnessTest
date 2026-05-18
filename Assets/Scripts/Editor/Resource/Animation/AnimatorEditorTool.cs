using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 动画编辑器工具
/// </summary>
public class AnimatorEditorTool
{
    /// <summary>
    /// 将动画片段内嵌至指定状态机
    /// </summary>
    [MenuItem(EditorConst.AddAnimationClipToNest, priority = EditorConst.Priority_AddAnimationClipToNest)]
    public static void AddAnimationClipsToNest()
    {
        try
        {
            List<string> modifyAnimatorControllerList = new List<string>();
            var filePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.AnimatorControllerResSuffix);
            for (int i = 0; i < filePathList.Count; i++)
            {
                var filePath = filePathList[i];
                if (EditorUtility.DisplayCancelableProgressBar("将动画片段内嵌至指定状态机", $"正在将动画片段内嵌至指定状态机 {Path.GetFileNameWithoutExtension(filePath)} {i + 1}/{filePathList.Count}", (i + 1) * 1f / filePathList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(filePath);
                var clipsArray = animatorController.animationClips;
                if (animatorController == null || clipsArray.Length <= 0)
                    return;

                modifyAnimatorControllerList.Add(animatorController.name);
                var instantiateMotionDict = new Dictionary<string, Motion>();
                var toDeleteClipPathList = new List<string>();
                // 内嵌动画片段
                foreach (AnimationClip clip in clipsArray)
                {
                    var clipCellAssetPath = AssetDatabase.GetAssetPath(clip);
                    if (clipCellAssetPath.EndsWith(".anim"))
                    {
                        var newClip = Object.Instantiate(clip);
                        newClip.name = clip.name;
                        instantiateMotionDict.Add(newClip.name, newClip);
                        AssetDatabase.AddObjectToAsset(newClip, animatorController);
                        toDeleteClipPathList.Add(AssetDatabase.GetAssetPath(clip));
                    }
                }

                // 绑定动画片段
                var layer = animatorController.layers[0];
                var sm = layer.stateMachine;
                var childAnimatorStateArray = sm.states;
                foreach (var childAnimatorState in childAnimatorStateArray)
                {
                    if (instantiateMotionDict.ContainsKey(childAnimatorState.state.motion.name))
                    {
                        childAnimatorState.state.motion = instantiateMotionDict[childAnimatorState.state.motion.name];
                    }
                }
                instantiateMotionDict.Clear();

                // 删除原有动画片段
                foreach (var clipPath in toDeleteClipPathList)
                {
                    AssetDatabase.DeleteAsset(clipPath);
                }
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"将动画片段内嵌至指定状态机成功，共操作" + modifyAnimatorControllerList.Count + "个动画状态机：");
            foreach (var modifyAnimatorController in modifyAnimatorControllerList)
            {
                sb.AppendLine(modifyAnimatorController);
            }
            EditorUtils.ShowDialogWindow($"将动画片段内嵌至指定状态机成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow($"将动画片段内嵌至指定状态机失败", e.ToString());
        }
    }

    /// <summary>
    /// 删除状态机内嵌动画片段
    /// </summary>
    [MenuItem(EditorConst.DeleteNestAnimationClip, priority = EditorConst.Priority_DeleteNestAnimationClip)]
    public static void DeleteNestAnimationClip()
    {
        try
        {
            List<string> deleteAnimationClipList = new List<string>();
            Object[] selectedAsstes = Selection.objects;
            foreach (Object asset in selectedAsstes)
            {
                if (AssetDatabase.IsSubAsset(asset))
                {
                    string path = AssetDatabase.GetAssetPath(asset);
                    Object.DestroyImmediate(asset, true);
                    AssetDatabase.ImportAsset(path);
                    deleteAnimationClipList.Add(Path.GetFileName(path));
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"删除状态机内嵌动画片段成功，共删除" + deleteAnimationClipList.Count + "个动画片段：");
            foreach (var deleteAnimationClip in deleteAnimationClipList)
            {
                sb.AppendLine(deleteAnimationClip);
            }
            EditorUtils.ShowDialogWindow($"删除状态机内嵌动画片段成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow($"删除状态机内嵌动画片段失败", e.ToString());
        }
    }

    /// <summary>
    /// 优化动画片段
    /// </summary>
    [MenuItem(EditorConst.OptimizeAnimationClip, priority = EditorConst.Priority_OptimizeAnimationClip)]
    public static void OptimizeAnimationClip()
    {
        try
        {
            List<string> modifyAnimationClipList = new List<string>();
            // 收集所有动画片段
            List<AnimationClip> collectClipList = new List<AnimationClip>();
            foreach (var filePath in EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.AnimatorControllerResSuffix))
            {
                var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(filePath);
                foreach (var animationClip in animatorController.animationClips)
                {
                    if (animationClip == null)
                        continue;
                    collectClipList.Add(animationClip);
                }
            }
            var filePathList = EditorUtils.GetSelectedFileList(SearchOption.AllDirectories, EditorConst.AnimationClipResSuffix);
            foreach (var filePath in filePathList)
            {
                var animationClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(filePath);
                if (animationClip == null)
                    continue;
                collectClipList.Add(animationClip);
            }

            // 处理动画片段
            for (int i = 0; i < collectClipList.Count; i++)
            {
                bool dirty = false;
                var clip = collectClipList[i];
                if (EditorUtility.DisplayCancelableProgressBar("优化动画片段", $"正在优化动画片段 {clip.name} {i + 1}/{collectClipList.Count}", (i + 1) * 1f / collectClipList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                dirty |= Optimize_RemvoveRedundantKey(clip);
                dirty |= Optimize_CompressCurvePrecision(clip);
                if (dirty)
                {
                    modifyAnimationClipList.Add(clip.name);
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"优化动画片段成功，共优化" + modifyAnimationClipList.Count + "个动画片段：");
            foreach (var animationClip in modifyAnimationClipList)
            {
                sb.AppendLine(animationClip);
            }
            EditorUtils.ShowDialogWindow($"优化动画片段成功", sb.ToString());
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
            EditorUtils.ShowDialogWindow($"优化动画片段失败", e.ToString());
        }
    }

    /// <summary>
    /// 移除冗余的关键帧（只保留第一帧和最后一帧）
    /// </summary>
    private static bool Optimize_RemvoveRedundantKey(AnimationClip clip)
    {
        bool dirty = false;
        var bindingArray = AnimationUtility.GetCurveBindings(clip);
        foreach (var binding in bindingArray)
        {
            var curve = AnimationUtility.GetEditorCurve(clip, binding);
            if (curve == null || curve.length <= 2)
                continue;

            float v = curve.keys[0].value;
            bool b = false;
            for (int i = 1; i < curve.length; i++)
            {
                if (!Mathf.Approximately(curve.keys[i].value, v))
                {
                    b = true;
                    break;
                }
            }
            if (!b)
            {
                for (int i = curve.length - 2; i >= 1; i--)
                {
                    curve.RemoveKey(i);
                }
                AnimationUtility.SetEditorCurve(clip, binding, curve);
                dirty = true;
            }
        }
        return dirty;
    }

    /// <summary>
    /// 压缩精度
    /// </summary>
    private static bool Optimize_CompressCurvePrecision(AnimationClip clip)
    {
        bool dirty = false;
        var bindingArray = AnimationUtility.GetCurveBindings(clip);
        foreach (var binding in bindingArray)
        {
            var curve = AnimationUtility.GetEditorCurve(clip, binding);
            if (curve == null)
                continue;
            for (int i = 0; i < curve.keys.Length; i++)
            {
                Keyframe key = curve.keys[i];
                float newValue = MathUtils.Round(key.value, 2);
                float newIn = MathUtils.Round(key.inTangent, 2);
                float newOut = MathUtils.Round(key.outTangent, 2);
                if (!Mathf.Approximately(key.value, newValue)
                    || !Mathf.Approximately(key.inTangent, newIn)
                    || !Mathf.Approximately(key.outTangent, newOut))
                {
                    key.value = newValue;
                    key.inTangent = newIn;
                    key.outTangent = newOut;
                    dirty = true;
                    curve.MoveKey(i, key);
                }
            }
            AnimationUtility.SetEditorCurve(clip, binding, curve);
        }
        return dirty;
    }
}