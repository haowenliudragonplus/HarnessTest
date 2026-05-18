using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DragonPlus.Core;
using DragonPlus.Haptics;
using DragonPlus.Network;
using Framework;
using Newtonsoft.Json;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public static class GameUtils
{
    public static void SetEventSystemEnable(bool enable)
    {
        Game.GetMod<ModUI>().EventSystem.enabled = enable;
    }

    /// <summary>
    /// while循环安全版
    /// </summary>
    public static void SafeWhile(Func<bool> loopCondition, Action loopBody, bool logError = true, int countLimit = 11111)
    {
        if (loopCondition == null)
        {
            Debug.LogError("循环条件不能为null");
            return;
        }

        int count = 0;
        while (loopCondition())
        {
            loopBody?.Invoke();
            count++;
            if (count == countLimit)
            {
                if (logError)
                {
                    Debug.LogError("出现死循环！！！！！！！！！！");
                }
                break;
            }
        }
    }

    private const float DefaultGameTimeScale = 1f; //游戏内默认TimeScale
    /// <summary>
    /// 进入子弹时间
    /// </summary>
    public static IEnumerator EnterBulletTime(float toTimeScale, float duration)
    {
        float curTime = 0;
        var scaleDis = Time.timeScale - toTimeScale;
        while (curTime < duration)
        {
            curTime += Time.unscaledDeltaTime;
            if (Time.timeScale <= toTimeScale)
                Time.timeScale = toTimeScale;
            else
                Time.timeScale -= Time.timeScale * 0.5f;
            yield return null;
        }
        Time.timeScale = DefaultGameTimeScale;
    }

    /// <summary>
    /// 设置时间缩放
    /// </summary>
    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    /// <summary>
    /// 创建GameObject
    /// </summary>
    public static GameObject CreateGameObject(string name, Transform parent, bool canRepeat = false, params Type[] components)
    {
        GameObject go = new GameObject();
        go.name = name;
        go.transform.SetParent(parent, false);
        go.ResetLocal();
        if (!canRepeat)
        {
            HashSet<Type> hashtable = new HashSet<Type>();
            foreach (var temp in components)
            {
                hashtable.Add(temp);
            }
            foreach (var component in hashtable)
            {
                go.AddComponent(component);
            }
        }
        else
        {
            foreach (var component in components)
            {
                go.AddComponent(component);
            }
        }
        return go;
    }

    /// <summary>
    /// 计算从rootTrans节点到targetTrans节点的路径
    /// </summary>
    /// 默认不带rootTrans
    public static string CalculateTransPath(Transform targetTrans, Transform rootTrans, bool startWithRootTrans = false)
    {
        if (!targetTrans.IsChildOf(rootTrans))
        {
            Debug.LogError($"{targetTrans.name}不是{rootTrans.name}的子物体");
            return string.Empty;
        }

        StringBuilder transPath = new StringBuilder(targetTrans.name);
        Transform parent = targetTrans.parent;
        SafeWhile(() =>
        {
            return parent != null
                   && parent != rootTrans;
        }, () =>
        {
            transPath.Insert(0, parent.name + "/");
            parent = parent.parent;
        });
        if (startWithRootTrans)
        {
            transPath.Insert(0, rootTrans.name + "/");
        }
        return transPath.ToString();
    }

    /// <summary>
    /// 查找某一个节点
    /// </summary>
    public static Transform FindTrans(Transform rootTrans, string findTransName)
    {
        if (string.IsNullOrEmpty(findTransName))
            return null;
        for (int i = 0; i < rootTrans.childCount; i++)
        {
            var trans = rootTrans.GetChild(i);
            if (trans.name == findTransName)
                return trans;
            FindTrans(trans, findTransName);
        }
        return null;
    }

    /// <summary>
    /// 获取Banner高度
    /// </summary>
    /// 获取到的是屏幕坐标height，也就是分辨率
    public static float GetBannerHeight()
    {
        float ret = 0;
        //MaxSdkUtils.GetAdaptiveBannerHeight返回值是dp（在安卓中称为dp）
        //dp表示像素无关密度，在任何设备上都是相同的，不同设备上的dp值相同，但是实际的像素值不同
        //规定了160dpi（密度）为标准，在标准密度下，1dp等于1px，比如屏幕密度为240的设备则1dp=1.5px
        var sdkBannerHeight = MaxSdkUtils.GetAdaptiveBannerHeight(); //返回的是dp
        if (sdkBannerHeight > 0)
        {
            var density = MaxSdkUtils.GetScreenDensity();
            var heightPx = sdkBannerHeight * density;
            ret = heightPx;
        }
#if UNITY_IOS
        float bottomPadding = Screen.safeArea.yMin;
        ret += bottomPadding;
#endif
        return ret;
    }

    /// <summary>
    /// 某个位置是否在相机视野范围内（2D）
    /// </summary>
    /// 不够精确，取的是物体的position进行判断
    public static bool IsInSight2D(Vector2 pos, Camera camera,
        float offsetMinX = 0, float offsetMinY = 0, float offsetMaxX = 0, float offsetMaxY = 0)
    {
        Vector2 minPos_World = CTUtils.Viewport2World(Vector3.zero, camera);
        Vector2 maxPos_World = CTUtils.Viewport2World(Vector3.one, camera);
        if (pos.x > maxPos_World.x + offsetMaxX
            || pos.x < minPos_World.x - offsetMinX
            || pos.y > maxPos_World.y + offsetMaxY
            || pos.y < minPos_World.y - offsetMinY)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 某个位置是否在相机视野范围内（3D透视相机）
    /// </summary>
    /// 不够精确，取的是物体的position进行判断
    public static bool IsInSight3D(Vector3 pos, Camera camera,
        float offsetMinX = 0, float offsetMinY = 0, float offsetMaxX = 0, float offsetMaxY = 0)
    {
        Vector2 minPos_World = CTUtils.Viewport2World(new Vector3(0, 0, -camera.transform.position.z), camera);
        Vector2 maxPos_World = CTUtils.Viewport2World(new Vector3(1, 1, -camera.transform.position.z), camera);
        if (pos.x > maxPos_World.x + offsetMaxX
            || pos.x < minPos_World.x - offsetMinX
            || pos.y > maxPos_World.y + offsetMaxY
            || pos.y < minPos_World.y - offsetMinY)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 某个位置是否在相机视野范围内
    /// </summary>
    public static bool IsInSight3D(Renderer renderer, Camera camera)
    {
        Bounds bounds;
        if (renderer == null)
        {
            bounds = new Bounds(renderer.transform.position, Vector3.one * 0.1f);
        }
        else
        {
            bounds = renderer.bounds;
        }
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        var ret = GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);
        return ret;
    }

    /// <summary>
    /// 获取Banner高度（世界坐标下）
    /// </summary>
    public static float GetBannerHeightY_World(Camera camera)
    {
        if (!Game.GetMod<AdSys>().ShowingBanner)
            return 0;
        float bannerHeight = GetBannerHeight();
        Vector2 screenTempPos_World = CTUtils.Screen2World(new Vector3(0, bannerHeight, 0), camera);
        Vector2 screenMinPos_World = CTUtils.Viewport2World(Vector3.zero, camera);
        float yOffset = screenTempPos_World.y - screenMinPos_World.y;
        return yOffset;
    }

    /// <summary>
    /// 获取刘海屏适配高度（世界坐标下）
    /// </summary>
    public static float GetBangHeightY_World(Camera camera)
    {
        Vector2 screenMaxPos_World = CTUtils.Viewport2World(Vector3.one, camera);
        Vector2 sceenSafePos_World = CTUtils.Screen2World(new Vector3(0, Screen.safeArea.yMax), camera);
        float safeAreaYOffset = screenMaxPos_World.y - sceenSafePos_World.y; // 计算安全区额外的y偏移
        return safeAreaYOffset;
    }

    /// <summary>
    /// 是否为pad
    /// </summary>
    public static bool IsPad()
    {
        float maxR = Mathf.Max(Screen.width, Screen.height);
        float minR = Mathf.Min(Screen.width, Screen.height);
        var ratio = (maxR / minR) <= 1.605f;
        return ratio;
    }

    private static readonly int Grayscale = Shader.PropertyToID("_Grayscale");
    public static void SetImgGray(GameObject go, bool isGray)
    {
        var images = go.GetComponentsInChildren<Image>(true);
        Material mat = Game.GetMod<ModAsset>().GetRes<Material>("UI-Default-Gray").GetInstance(go);
        foreach (var img in images)
        {
            img.material = new Material(mat);
            img.material.SetFloat(Grayscale, isGray ? 1.0f : 0.0f);
        }
        if (images.Length > 0)
        {
            if (go.gameObject.activeSelf)
            {
                go.SetActive(false);
                go.SetActive(true);
            }
        }
    }

    public static int GetEnumValue(Type type, string enumStr)
    {
        int index = -1;
        string[] nameArray = Enum.GetNames(type);
        Array valueArray = Enum.GetValues(type);
        for (int i = 0; i < nameArray.Length; i++)
        {
            if (nameArray[i] == enumStr)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
            return -1;
        int value = (int)valueArray.GetValue(index);
        return value;
    }

    /// <summary>
    /// 触发震动
    /// </summary>
    public static void Virbrate(HapticTypes type)
    {
        SDK<IHaptics>.Instance.Haptics(type);
    }

    public static void SetVibrateEnable(bool unEnabled)
    {
        SDK<IHaptics>.Instance.Active = !unEnabled;
    }

    /// <summary>
    /// 是否为相同的数量
    /// </summary>
    public static bool IsSameCount(params int[] countArray)
    {
        if (countArray == null)
            return true;
        int compareCount = countArray[0];
        foreach (var count in countArray)
        {
            if (compareCount == count)
                continue;
            return false;
        }
        return true;
    }

    /// <summary>
    /// 深拷贝一个对象
    /// </summary>
    public static T DeepCopy<T>(T obj)
    {
        // 序列化
        string json = JsonConvert.SerializeObject(obj);
        // 反序列化
        return JsonConvert.DeserializeObject<T>(json);
    }
}