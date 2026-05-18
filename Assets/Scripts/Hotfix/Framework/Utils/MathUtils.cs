using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

public static class MathUtils
{
    /// <summary>
    /// 四舍五入
    /// </summary>
    /// digits：保留几位小数
    public static float Round(float value, int digits = 1)
    {
        if (value == 0)
            return 0;
        float sign = Mathf.Sign(value);
        value = Mathf.Abs(value);
        float multiple = Mathf.Pow(10, digits);
        float tempValue = value * multiple + 0.5f;
        tempValue = Mathf.FloorToInt(tempValue);
        return tempValue / multiple * sign;
    }

    /// <summary>
    /// 获取一个随机的枚举值
    /// </summary>
    public static T GetRandomEmum<T>(List<T> ignoreList = null)
        where T : Enum
    {
        Array valueArray = Enum.GetValues(typeof(T));
        T randomEnum = valueArray.GetRandomValue(ignoreList);
        return randomEnum;
    }

    /// <summary>
    /// 获取一个随机的枚举值列表
    /// </summary>
    public static List<T> GetRandomEnumList<T>(int getCount, bool excludeSame = false, List<T> ignoreList = null)
        where T : Enum
    {
        Array valueArray = Enum.GetValues(typeof(T));
        List<T> randomEnumList = valueArray.GetRandomValueList<T>(getCount, excludeSame, ignoreList);
        return randomEnumList;
    }

    /// <summary>
    /// 根据权重获取值
    /// </summary>
    public static T GetValueByWeight<T>(List<T> selectList, List<int> weightList)
    {
        if (selectList == null || weightList == null)
        {
            CLog.Error("选择列表和权重列表不能为null");
            return default(T);
        }

        // 去除检测权重是0的
        List<T> tempSelectList = new List<T>(selectList);
        List<int> tempWeightList = new List<int>(weightList);
        List<int> invalidIndexList = new List<int>();
        for (int i = weightList.Count - 1; i >= 0; i--)
        {
            if (tempWeightList[i] != 0)
                continue;
            invalidIndexList.Add(i);
        }
        foreach (var index in invalidIndexList)
        {
            tempSelectList.RemoveAt(index);
            tempWeightList.RemoveAt(index);
        }

        if (tempSelectList.Count != tempWeightList.Count)
        {
            CLog.Error("选择列表和权重列表数量不一致");
            return default(T);
        }

        var totalWight = tempWeightList.Sum();
        var randomValue = UnityEngine.Random.Range(0, totalWight + 1);
        int tempValue = 0;
        for (int i = 0; i < tempSelectList.Count; i++)
        {
            tempValue += tempWeightList[i];
            if (randomValue <= tempValue)
                return tempSelectList[i];
        }
        return tempSelectList[^1];
    }

    /// <summary>
    /// 根据权重获取值的下标
    /// </summary>
    public static int GetValueIndexByWeight<T>(List<T> selectList, List<int> weightList)
    {
        T value = GetValueByWeight(selectList, weightList);
        int index = selectList.IndexOf(value);
        return index;
    }

    /// <summary>
    /// 判断距离（不开根号）
    /// </summary>
    public static float SqrtDistance(Vector3 a, Vector3 b)
    {
        float num1 = a.x - b.x;
        float num2 = a.y - b.y;
        float num3 = a.z - b.z;
        return num1 * num1 + num2 * num2 + num3 * num3;
    }
}