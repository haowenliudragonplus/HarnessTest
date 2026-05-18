using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public static class SpineUtils
{
    public static void GoAttachToBone(GameObject go, SkeletonAnimation skeletonAnimation, string boneName,
        bool followBoneRotation = true, bool followLocalScale = true, bool followSkeletonFlip = true)
    {
        var com_BoneFollower = go.AddComponent<BoneFollower>();
        com_BoneFollower.skeletonRenderer = skeletonAnimation;
        com_BoneFollower.boneName = boneName;
        com_BoneFollower.followBoneRotation = true; // 可选，根据需要设置
        com_BoneFollower.followLocalScale = true; // 可选，根据需要设置
        com_BoneFollower.followSkeletonFlip = true; // 可选，根据需要设置
    }
}