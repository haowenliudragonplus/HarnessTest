using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMGame
{

    public static class AnimatorUtils
    {
        public static AnimationClip GetClipInfo(Animator animator, string clipName)
        {
            clipName = clipName.ToLower();
            var clips = animator?.runtimeAnimatorController?.animationClips;
            if (clips != null)
            {
                for (int i = 0; i < clips.Length; i++)
                {
                    if (clips[i].name.ToLower().Equals(clipName))
                    {
                        return clips[i];
                    }
                }
            }

            return default(AnimationClip);
        }

        public static AnimationClip GetClipInfo(Animator animator, int index)
        {
            var clips = animator?.runtimeAnimatorController?.animationClips;
            if (clips == null || clips.Length <= index)
                return null;
            return clips[index];
        }

        /// <summary>
        /// 得到动画片段的长度
        /// </summary>
        public static float GetAnimationClipLength(Animator animator, string animationName, bool needAllMatch = true)
        {
            animationName = animationName.ToLower();
            if (animator == null)
            {
                Debug.LogError("动画状态机组件为null");
                return 0;
            }

            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("动画状态机为null");
                return 0;
            }

            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            if (clips == null)
            {
                Debug.LogError("动画状态机中动画片段数组为null");
                return 0;
            }

            foreach (AnimationClip clip in clips)
            {
                if (needAllMatch ? clip.name.ToLower().Equals(animationName) : clip.name.Contains(animationName))
                {
                    return clip.length;
                }
            }

            Debug.LogError($"找不到此动画片段，animator挂载的物体：{animator.name}，animationName：{animationName}");
            return 0;
        }

        /// <summary>
        /// 得到动画片段的长度
        /// </summary>
        public static float GetAnimationClipLength(Animator animator, int index)
        {
            if (animator == null)
            {
                Debug.LogError("动画状态机为null");
                return 0;
            }

            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("动画状态机为null");
                return 0;
            }

            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            if (clips == null)
            {
                Debug.LogError("动画状态机中动画片段数组为null");
                return 0;
            }

            if (clips == null
                || clips.Length <= index)
            {
                Debug.LogError($"动画片段下标超出数组索引，animator挂载的物体：{animator.name}，index：{index}");
                return 0;
            }

            return clips[index].length;
        }

        public static float GetAnimationClipEventTime(Animator animator, string animationClipNameMatch)
        {
            if (animator == null)
            {
                Debug.LogError("动画状态机组件为null");
                return 0;
            }

            var clip = animator.runtimeAnimatorController.animationClips.First(x =>
                x.name.Contains(animationClipNameMatch));
            if (clip != null)
            {
                float evtTime = clip.events.Count() > 0 ? clip.events[0].time : clip.length;
                return evtTime;
            }

            return 0f;
        }

        public static float GetAnimationClipEventTime(Animator animator, int index)
        {
            var clip = GetClipInfo(animator, index);
            if (clip == null)
                return 0;
            if (clip.events.Count() <= index)
                return 0;
            float eventTime = clip.events[index].time;
            return eventTime;
        }

        /// <summary>
        /// 获取当前StateInfo
        /// </summary>
        /// 播放动画后一定要等待一帧后再获取
        public static IEnumerator GetCurStateInfo(this Animator animator, string stateName,
            Action<AnimatorStateInfo> onGetCurStateInfo, int layer = 0)
        {
            if (animator == null)
            {
                Debug.LogError("动画状态机组件为null");
                yield break;
            }

            if (string.IsNullOrEmpty(stateName))
            {
                Debug.LogError($"{animator.name}动画机中的状态名不能为空");
                yield break;
            }

            yield return new WaitForEndOfFrame();
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
            if (stateInfo.IsName(stateName))
            {
                onGetCurStateInfo?.Invoke(stateInfo);
            }
        }
    }
}