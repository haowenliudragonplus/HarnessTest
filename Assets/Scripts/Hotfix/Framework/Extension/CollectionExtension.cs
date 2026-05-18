using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public static class CollectionExtension
{
    /// <summary>
    /// 打乱顺序
    /// </summary>
    public static void Shuffle<T>(this List<T> list)
    {
        int index;
        T temp;
        for (int i = list.Count - 1; i > 0; i--)
        {
            index = UnityEngine.Random.Range(0, i + 1);
            temp = list[i];
            list[i] = list[index];
            list[index] = temp;
        }
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this T[] list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Length);
        T randomValue = list[randomNum];
        GameUtils.SafeWhile(
            () => { return ignoreList != null && ignoreList.Contains(randomValue); },
            () =>
            {
                int randomNum = UnityEngine.Random.Range(0, list.Length);
                randomValue = list[randomNum];
            });
        return randomValue;
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this List<T> list, List<T> ignoreList = null)
    {
        var randomValue = GetRandomValue(list.ToArray(), ignoreList);
        return randomValue;
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this Array list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Length);
        T randomValue = (T)list.GetValue(randomNum);
        GameUtils.SafeWhile(
            () => { return ignoreList != null && ignoreList.Contains(randomValue); },
            () =>
            {
                int randomNum = UnityEngine.Random.Range(0, list.Length);
                randomValue = (T)list.GetValue(randomNum);
            });
        return randomValue;
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this T[] list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            CLog.Error($"可随机的数量 [{canRandomCount}] 小于要获取的数量 [{getCount}]");
            return null;
        }
        List<T> randomList = new List<T>();
        GameUtils.SafeWhile(
            () => { return randomList.Count < getCount; },
            () =>
            {
                var value = GetRandomValue<T>(list, ignoreList);
                if (excludeSame || !randomList.Contains(value))
                {
                    randomList.Add(value);
                }
            });
        return randomList;
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this List<T> list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        var randomList = GetRandomValueList(list.ToArray(), getCount, excludeSame, ignoreList);
        return randomList;
    }

    /// <summary>
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this Array list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            Debug.LogError("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new List<T>();
        GameUtils.SafeWhile(
            () => { return randomList.Count < getCount; },
            () =>
            {
                var value = GetRandomValue<T>(list, ignoreList);
                if (excludeSame || !randomList.Contains(value))
                {
                    randomList.Add(value);
                }
            });
        return randomList;
    }

    /// <summary>
    /// 拷贝列表（无申请内存消耗）
    /// </summary>
    public static void CopyListNonAlloc<T>(this List<T> originList, List<T> copyToList)
    {
        copyToList.Clear();
        foreach (var temp in originList)
        {
            copyToList.Add(temp);
        }
    }

    /// <summary>
    /// 派生类列表转换成基类列表
    /// </summary>
    public static List<T2> ToList_BaseType<T1, T2>(this List<T1> deriveTypeList)
        where T1 : T2
    {
        var ret = new List<T2>();
        foreach (var temp in deriveTypeList)
        {
            ret.Add(temp);
        }
        return ret;
    }

    /// <summary>
    /// 基类列表转换成派生类列表
    /// </summary>
    public static List<T1> ToList_DeriveType<T1, T2>(this List<T2> baseTypeList)
        where T1 : T2
    {
        var ret = new List<T1>();
        foreach (var temp in baseTypeList)
        {
            ret.Add((T1)temp);
        }
        return ret;
    }

    /// <summary>
    /// Vector2列表转换成Vector3列表
    /// </summary>
    public static List<Vector3> ToList_Vector3(this List<Vector2> vector2List)
    {
        var ret = new List<Vector3>();
        foreach (var temp in vector2List)
        {
            ret.Add((Vector3)temp);
        }
        return ret;
    }

    /// <summary>
    /// Vector3列表转换成Vector2列表
    /// </summary>
    public static List<Vector2> ToList_Vector2(this List<Vector3> vector3List)
    {
        Vector3 v1 = new Vector3();
        Vector2 v2 = v1;
        var ret = new List<Vector2>();
        foreach (var temp in vector3List)
        {
            ret.Add((Vector2)temp);
        }
        return ret;
    }
}