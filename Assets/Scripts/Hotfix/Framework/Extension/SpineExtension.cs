using Framework;
using Spine;
using Spine.Unity;
using UnityEngine;
using Animation = Spine.Animation;

public static class SpineExtension
{
    #region SpriteRenderer

    public static void CalcMaxAndMinWorldPos(this SkeletonAnimation skeletonAnimation,
        out float minX, out float minY, out float maxX, out float maxY)
    {
        minX = float.MaxValue;
        minY = float.MaxValue;
        maxX = float.MinValue;
        maxY = float.MinValue;

        var skeleton = skeletonAnimation.Skeleton;
        foreach (var slot in skeleton.Slots)
        {
            var attachment = slot.Attachment;
            if (attachment is RegionAttachment regionAttachment)
            {
                int vertexCount = 8;
                float[] worldVertices = new float[vertexCount];
                regionAttachment.ComputeWorldVertices(slot, worldVertices, 0);
                for (int i = 0; i < vertexCount; i += 2)
                {
                    Vector2 tempPos = new Vector2(worldVertices[i], worldVertices[i + 1]);
                    Vector3 worldPos = skeletonAnimation.transform.TransformPoint(tempPos);
                    float x = worldPos.x;
                    float y = worldPos.y;

                    if (x < minX) minX = x;
                    if (y < minY) minY = y;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }
            }
            else if (attachment is MeshAttachment meshAttachment)
            {
                var vertexCount = meshAttachment.WorldVerticesLength;
                float[] worldVertices = new float[vertexCount];
                meshAttachment.ComputeWorldVertices(slot, 0, vertexCount, worldVertices, 0);
                for (int i = 0; i < worldVertices.Length; i += 2)
                {
                    Vector2 tempPos = new Vector2(worldVertices[i], worldVertices[i + 1]);
                    Vector3 worldPos = skeletonAnimation.transform.TransformPoint(tempPos);
                    float x = worldPos.x;
                    float y = worldPos.y;

                    if (x < minX) minX = x;
                    if (y < minY) minY = y;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }
            }
        }
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// 之前项目的家装修改了spine源码，所以这里也使用修改后的代码
    public static void PlayAni(this SkeletonAnimation skeletonAnimation, string aniName, bool loop = true, int trackIndex = 0)
    {
        if (skeletonAnimation == null)
        {
            CLog.Error("SkeletonAnimation组件为null");
            return;
        }
        if (string.IsNullOrEmpty(aniName))
        {
            CLog.Error($"不能播放游戏物体 [{skeletonAnimation.name}] 的SkeletonAnimation组件中的空动画");
            return;
        }
        if (!skeletonAnimation.HaveAni(aniName))
        {
            CLog.Error($"游戏物体 [{skeletonAnimation.name}] 的SkeletonAnimation组件中找不到 [{aniName}] 动画");
            return;
        }

        skeletonAnimation.state.SetAnimation(trackIndex, aniName, loop); //正常源码的调用方式

        // 之前项目的家装修改了spine源码，所以这里也使用修改后的代码
        // skeletonAnimation.autoUpdate2 = true;
        // skeletonAnimation.ClearState();
        // skeletonAnimation.skeleton.SetSlotsToSetupPose();
        // skeletonAnimation.AnimationName = null;
        // skeletonAnimation.AnimationName = aniName;
        // skeletonAnimation.loop = loop;
        // skeletonAnimation.Update(0);
    }

    /// <summary>
    /// 是否有某个动画
    /// </summary>
    public static bool HaveAni(this SkeletonAnimation skeletonAnimation, string aniName)
    {
        if (skeletonAnimation == null)
            return false;
        if (string.IsNullOrEmpty(aniName))
            return false;
        var ani = skeletonAnimation.skeleton.Data.FindAnimation(aniName);
        if (ani == null)
            return false;
        return true;
    }

    /// <summary>
    /// 是否有某个骨骼
    /// </summary>
    public static bool HaveBone(this SkeletonAnimation skeletonAnimation, string boneName)
    {
        if (string.IsNullOrEmpty(boneName))
            return false;
        var bone = skeletonAnimation.skeleton.Data.FindBone(boneName);
        if (bone == null)
            return false;
        return true;
    }

    /// <summary>
    /// 获取动画时间
    /// </summary>
    public static float GetAniTime(this SkeletonAnimation skeletonAnimation, string aniName)
    {
        if (!skeletonAnimation.HaveAni(aniName))
        {
            CLog.Error($"游戏物体 [{skeletonAnimation.name}] 的SkeletonAnimation组件中找不到 [{aniName}] 动画");
            return 0;
        }
        Animation ani = skeletonAnimation.Skeleton.Data.FindAnimation(aniName);
        var time = ani.Duration;
        return time;
    }

    #endregion SpriteRenderer

    #region UGUI

    /// <summary>
    /// 播放动画
    /// </summary>
    public static void PlayAni(this SkeletonGraphic skeletonGraphic, string aniName, bool loop = true, int trackIndex = 0)
    {
        if (skeletonGraphic == null)
        {
            CLog.Error("SkeletonGraphic组件为null");
            return;
        }
        if (string.IsNullOrEmpty(aniName))
        {
            CLog.Error($"不能播放游戏物体 [{skeletonGraphic.name}] 的SkeletonGraphic组件中的空动画");
            return;
        }
        if (!skeletonGraphic.HaveAni(aniName))
        {
            CLog.Error($"游戏物体 [{skeletonGraphic.name}] 的SkeletonGraphic组件中找不到 [{aniName}] 动画");
            return;
        }

        skeletonGraphic.AnimationState.SetAnimation(0, aniName, loop);
    }

    /// <summary>
    /// 是否有某个动画
    /// </summary>
    public static bool HaveAni(this SkeletonGraphic skeletonGraphic, string aniName)
    {
        if (skeletonGraphic == null)
            return false;
        if (string.IsNullOrEmpty(aniName))
            return false;
        if (skeletonGraphic.Skeleton.Data.FindAnimation(aniName) == null)
            return false;
        return true;
    }

    /// <summary>
    /// 获取动画时间
    /// </summary>
    public static float GetAniTime(this SkeletonGraphic skeletonGraphic, string aniName, bool logError = true)
    {
        if (!skeletonGraphic.HaveAni(aniName))
        {
            CLog.Error($"游戏物体 [{skeletonGraphic.name}] 的SkeletonGraphic组件中找不到 [{aniName}] 动画");
            return 0;
        }
        Animation ani = skeletonGraphic.Skeleton.Data.FindAnimation(aniName);
        var time = ani.Duration;
        return time;
    }

    #endregion UGUI
}