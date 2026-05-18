using System;
using System.Collections;
using System.Collections.Generic;
using DragonPlus.Core;
using Framework;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 局内工具类
/// </summary>
public static class InGameUtils
{
    /// <summary>
    /// 获取坐标（通过点下标）
    /// </summary>
    public static Vector2 GetUnitVector2(int pointIndex)
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        int x = pointIndex % mode.Data.LevelData.wide;
        int y = pointIndex / mode.Data.LevelData.wide;
        var ret = new Vector2(x, y);
        return ret;
    }

    /// <summary>
    /// 获取点下标（通过单位坐标坐标）
    /// </summary>
    public static int GetPointIndex(Vector2 unitV2)
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        int pointIndex = (int)unitV2.y * mode.Data.LevelData.wide + (int)unitV2.x;
        return pointIndex;
    }

    /// <summary>
    /// 获取真实世界坐标（通过单位坐标）
    /// </summary>
    public static Vector2 GetRealVector2(Vector2 unitV2)
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        var realPos = unitV2 * InGameConst.PointSpacing + (Vector2)mode.Data.SpawnStartPos;
        return realPos;
    }

    /// <summary>
    /// 获取真实世界坐标（通过点下标）
    /// </summary>
    public static Vector2 GetRealVector2(int pointIndex)
    {
        var unitV2 = GetUnitVector2(pointIndex);
        var realV2 = GetRealVector2(unitV2);
        return realV2;
    }

    /// <summary>
    /// 根据方向获取旋转角度
    /// </summary>
    public static float GetRotationAngle(Vector2 v2)
    {
        if (v2.x == 0 && v2.y == 1)
        {
            return 0f;
        }
        else if (v2.x == 0 && v2.y == -1)
        {
            return 180f;
        }
        else if (v2.x == -1 && v2.y == 0)
        {
            return 90f;
        }
        else if (v2.x == 1 && v2.y == 0)
        {
            return -90f;
        }
        CLog.Error($"获取角度失败，v2：{v2}");
        return 0;
    }

    /// <summary>
    /// 判断是否胜利或失败（屏蔽点击ui按钮用）
    /// </summary>
    /// todo：加模式类型需要在此添加判断逻辑
    public static bool IsWinOrFail()
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        if (!mode.CurState.CheckSubState<SubFsmState_InGame_Playing>())
        {
            return true;
        }
        return false;
    }

    public static GameObject PlayEffect(string resKey, Vector3 pos, Action onComplete = null,
      string layerName = "", float autoDestoryTime = -1)
    {
        if (string.IsNullOrEmpty(resKey))
            return null;
        var go = Game.GetMod<ModAsset>().GetGameObject(resKey).GetInstance();
        if (go == null)
            return null;

        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        go.transform.SetParent(mode.GameRoot.transform, false);
        go.transform.position = pos;
        go.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(layerName))
        {
            go.SetLayer(layerName, true);
        }
        float time = autoDestoryTime == -1
            ? Mathf.Max(1, ParticleSystemUtils.GetDuration(go))
            : autoDestoryTime;
        RegisterTimer(time, onComplete: (v) =>
            {
                Object.Destroy(go);
                onComplete?.Invoke();
            }, ignoreTimeScale: true);
        return go;
    }

    #region 通用工具的封装

    public static TimerTask RegisterTimer(float duration, bool ignoreTimeScale = false, int loopCount = 1,
        Action onSet = null, Action<float> onUpdate = null, Action<int> onComplete = null,
        MonoBehaviour monoHolder = null)
    {
        var tiemrTask = Game.GetMod<ModTimer>().Register(duration, ignoreTimeScale, loopCount, onSet, onUpdate, onComplete,
            monoHolder, ETimerBelongType.InGame);
        return tiemrTask;
    }

    public static void Register<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        Game.GetMod<ModEvent>().Register<T>(callback, subId, EEventBelongType.InGame);
    }

    public static bool UnRegister<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        bool ret = Game.GetMod<ModEvent>().UnRegister<T>(callback, subId, EEventBelongType.InGame);
        return ret;
    }

    public static void Dispatch<T>(T evt, int subId = -1)
        where T : IEvent
    {
        Game.GetMod<ModEvent>().Dispatch(evt, subId, EEventBelongType.InGame);
    }

    #endregion 通用工具的封装
}
