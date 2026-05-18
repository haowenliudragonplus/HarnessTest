using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    public class AnimClipChecker : ObjectChecker
    {
        public class AnimClipDetail : ObjectDetail
        {
            public AnimClipDetail(Object obj, AnimClipChecker checker) : base(obj, checker)
            {
                AnimationClip clip = obj as AnimationClip;
                checkMap.Add(checker.animLength, clip.length);
                checkMap.Add(checker.animWrapMode, clip.wrapMode.ToString());
                checkMap.Add(checker.animLoop, clip.isLooping.ToString());
                checkMap.Add(checker.animFrameRate, clip.frameRate);
                checkMap.Add(checker.animLegacy, clip.legacy.ToString());
            }
        }

        CheckItem animLength;
        CheckItem animWrapMode;
        CheckItem animLoop;
        CheckItem animFrameRate;
        CheckItem animLegacy;

        public override void InitCheckItem()
        {
            checkerName = "AnimClip";
            checkerFilter = "t:AnimationClip";
            animLength = new CheckItem(this, "时间", 80, CheckType.Float);
            animWrapMode = new CheckItem(this, "WrapMode");
            animLoop = new CheckItem(this, "Looping");
            animFrameRate = new CheckItem(this, "FrameRate", 80, CheckType.Float);
            animLegacy = new CheckItem(this, "IsLegacy");
        }

        public override ObjectDetail AddObjectDetail(Object obj, Object refObj, Object detailRefObj)
        {
            AnimationClip animClip = obj as AnimationClip;
            if (animClip == null)
                return null;
            ObjectDetail detail = null;
            foreach (var v in CheckList)
            {
                if (v.checkObject == obj)
                    detail = v;
            }
            if (detail == null)
            {
                detail = new AnimClipDetail(obj, this);
            }
            detail.AddObjectReference(refObj, detailRefObj);
            return detail;
        }

        public override List<Object> GetAllDirectCheckObjectFromInput(Object[] selection, string filter)
        {
            return GetAllObjectFromInput<AnimationClip>(selection, filter);
        }

        public override void AddObjectDetailRef(GameObject rootObj)
        {
            AddAnimDetail<Animation>(rootObj);
            AddAnimDetail<Animator>(rootObj);
        }

        private void AddAnimDetail<T>(GameObject rootObj) where T : Component
        {
            Component[] coms = rootObj.GetComponentsInChildren<T>(true);
            foreach (var anim in coms)
            {
                Object[] dependency = EditorUtility.CollectDependencies(new Object[] { anim.gameObject });
                foreach (var clip in dependency)
                {
                    if (checkModule is SceneResCheckModule)
                        AddObjectDetail(clip, anim.gameObject, null);
                    else
                        AddObjectDetail(clip, rootObj, anim.gameObject);
                }
            }
        }
    }
}
