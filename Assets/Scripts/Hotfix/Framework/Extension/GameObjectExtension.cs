using Framework;
using UnityEngine;
using UnityEngine.Rendering;

public static class GameObjectExtension
{
    /// <summary>
    /// 设置X坐标
    /// </summary>
    public static void SetPosX(this GameObject go, float x)
    {
        var tempPos = go.transform.position;
        tempPos.x = x;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置Y坐标
    /// </summary>
    public static void SetPosY(this GameObject go, float y)
    {
        var tempPos = go.transform.position;
        tempPos.y = y;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置Z坐标
    /// </summary>
    public static void SetPosZ(this GameObject go, float z)
    {
        var tempPos = go.transform.position;
        tempPos.z = z;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置局部X坐标
    /// </summary>
    public static void SetLocalPosX(this GameObject go, float x)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.x = x;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置局部Y坐标
    /// </summary>
    public static void SetLocalPosY(this GameObject go, float y)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.y = y;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置局部Z坐标
    /// </summary>
    public static void SetLocalPosZ(this GameObject go, float z)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.z = z;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置X旋转
    /// </summary>
    public static void SetRotX(this GameObject go, float x)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.x = x;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置Y旋转
    /// </summary>
    public static void SetRotY(this GameObject go, float y)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.y = y;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置Z旋转
    /// </summary>
    public static void SetRotZ(this GameObject go, float z)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.z = z;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置局部X旋转
    /// </summary>
    public static void SetLocalRotX(this GameObject go, float x)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.x = x;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置局部Y旋转
    /// </summary>
    public static void SetLocalRotY(this GameObject go, float y)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.y = y;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置局部Z旋转
    /// </summary>
    public static void SetLocalRotZ(this GameObject go, float z)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.z = z;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置X缩放
    /// </summary>
    public static void SetScaleX(this GameObject go, float x)
    {
        var tempScale = go.transform.localScale;
        tempScale.x = x;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置Y缩放
    /// </summary>
    public static void SetScaleY(this GameObject go, float y)
    {
        var tempScale = go.transform.localScale;
        tempScale.y = y;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置Z缩放
    /// </summary>
    public static void SetScaleZ(this GameObject go, float z)
    {
        var tempScale = go.transform.localScale;
        tempScale.z = z;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置父物体
    /// </summary>
    public static void SetParent(this GameObject go, GameObject parentGo, bool worldPositionStays = true)
    {
        go.transform.SetParent(parentGo.transform, worldPositionStays);
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    public static T GetComponent<T>(this GameObject go, bool forceGet = false)
        where T : Component
    {
        if (go == null)
            return null;
        T component = go.GetComponent<T>();
        if (component == null && forceGet)
            component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T AddComponent<T>(this GameObject go, bool canRepeat = false)
        where T : Component
    {
        if (go == null)
            return null;
        T component = go.GetComponent<T>();
        if (component != null && !canRepeat)
            return null;
        component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 销毁游戏物体
    /// </summary>
    public static bool Destroy(this GameObject go)
    {
        if (go == null)
            return false;
        Object.Destroy(go);
        return true;
    }

    public static void Reset(this GameObject go)
    {
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

    public static void ResetLocal(this GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

    public static void SetActive(this GameObject go, bool active, bool includeChild = false)
    {
        go.SetActive(active);
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                childGo.SetActive(active, true);
            }
        }
    }

    public static void SetLayer(this GameObject go, string layerName, bool includeChild = false, string excludeLayer = "")
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex == -1)
            return;
        if (!string.IsNullOrEmpty(excludeLayer))
        {
            string curLayerName = LayerMask.LayerToName(go.layer);
            if (curLayerName != excludeLayer)
            {
                go.layer = layerIndex;
            }
        }
        else
        {
            go.layer = layerIndex;
        }

        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                childGo.SetLayer(layerName, true, excludeLayer);
            }
        }
    }

    public static void SetSortingGroup(this GameObject go, string sortingLayerName, int orderInLayer, bool sortAtRoot = true)
    {
        var com = go.GetComponent<SortingGroup>(true);
        com.sortingLayerName = sortingLayerName;
        com.sortingOrder = orderInLayer;
        com.sortAtRoot = sortAtRoot;
    }

    public static void SetComponentEnable<T>(this GameObject go, bool enable, bool includeChild = false)
        where T : Behaviour
    {
        var com = go.GetComponent<T>();
        if (com != null)
        {
            com.enabled = enable;
        }
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                childGo.SetComponentEnable<T>(enable, true);
            }
        }
    }

    public static void SetStatic(this GameObject go, bool isStatic, bool includeChild = false)
    {
        go.isStatic = isStatic;
        if (includeChild)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                childGo.SetStatic(isStatic, true);
            }
        }
    }
}