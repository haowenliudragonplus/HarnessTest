using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

/// <summary>
/// 反射工具类
/// </summary>
public static class ReflectUtils
{
    /// <summary>
    /// 获取一个程序集
    /// </summary>
    public static Assembly LoadAssembly(string assemblyName)
    {
        Assembly assembly = Assembly.Load(assemblyName);
        if (assembly == null)
        {
            CLog.Error($"程序集获取失败，assemblyName：{assemblyName}");
            return null;
        }
        return assembly;
    }

    /// <summary>
    /// 创建一个类
    /// </summary>
    public static T Create<T>()
    {
        Type type = typeof(T);
        var obj = Create(type);
        return (T)obj;
    }

    /// <summary>
    /// 创建一个类
    /// </summary>
    public static object Create(Type type)
    {
        if (type == null)
        {
            CLog.Error($"创建Type失败，Type不能为空");
            return null;
        }
        object obj = Activator.CreateInstance(type);
        return obj;
    }

    /// <summary>
    /// 创建一个类
    /// </summary>
    public static object Create(string assemblyName, string nameSpace, string typeName)
    {
        Type type = GetType(assemblyName, nameSpace, typeName);
        object obj = Create(type);
        return obj;
    }

    /// <summary>
    /// 获取某个程序集下的所有类
    /// </summary>
    public static List<Type> GetTypes(string assemblyName)
    {
        Assembly assembly = LoadAssembly(assemblyName);
        var typeArray = assembly?.GetTypes();
        return typeArray.ToList();
    }

    public static List<Type> GetTypes(List<string> assemblyNameList)
    {
        List<Type> ret = new List<Type>();
        foreach (var assemblyName in assemblyNameList)
        {
            var typeList = GetTypes(assemblyName);
            ret.AddRange(typeList);
        }
        return ret;
    }

    /// <summary>
    /// 获取某个程序集下的某个类
    /// </summary>
    public static Type GetType(string assemblyName, string nameSpace, string typeName)
    {
        Assembly assembly = LoadAssembly(assemblyName);
        if (assembly == null)
            return null;
        Type type = assembly.GetType($"{nameSpace}.{typeName}");
        if (type == null)
        {
            CLog.Error($"类获取失败，assemblyName：{assemblyName}，nameSpace：{nameSpace}，typeName：{typeName}");
            return null;
        }
        return type;
    }

    /// <summary>
    /// 调用静态方法
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <param name="nameSpace"></param>
    /// <param name="typeName"></param>
    /// <param name="methodName"></param>
    /// <param name="genericTypeArray">泛型参数类型数组</param>
    /// <param name="argsTypeArray">方法参数类型数组</param>
    /// <param name="argsArray">方法参数数组</param>
    public static object InvokeStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName,
        object[] argsArray = null, Type[] genericTypeArray = null, Type[] argsTypeArray = null)
    {
        var methodInfo = GetMethodInfo(assemblyName, nameSpace, typeName, methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
        if (methodInfo == null)
            return null;
        var ret = methodInfo?.Invoke(null, argsArray);
        return ret;
    }

    /// <summary>
    /// 调用非静态方法
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <param name="nameSpace"></param>
    /// <param name="typeName"></param>
    /// <param name="methodName"></param>
    /// <param name="instance"></param>
    /// <param name="genericTypeArray">泛型参数类型数组</param>
    /// <param name="argsTypeArray">方法参数类型数组</param>
    /// <param name="argsArray">方法参数数组</param>
    public static object InvokeNoneStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName,
        object instance, object[] argsArray = null, Type[] genericTypeArray = null, Type[] argsTypeArray = null)
    {
        var methodInfo = GetMethodInfo(assemblyName, nameSpace, typeName, methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
        if (methodInfo == null)
            return null;
        var ret = methodInfo.Invoke(instance, argsArray);
        return ret;
    }

    public static MethodInfo GetMethodInfo(string assemblyName, string nameSpace, string typeName, string methodName, BindingFlags bindingFlags,
        Type[] genericTypeArray = null, Type[] argsTypeArray = null)
    {
        var type = GetType(assemblyName, nameSpace, typeName);
        if (type == null)
            return null;
        MethodInfo methodInfo;
        if (argsTypeArray == null)
        {
            methodInfo = type.GetMethod(methodName, bindingFlags);
        }
        else
        {
            methodInfo = type.GetMethod(methodName, bindingFlags, null, argsTypeArray, null);
        }
        // 如果是泛型方法
        if (genericTypeArray != null)
        {
            methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);
        }
        if (methodInfo == null)
        {
            CLog.Error($"方法获取失败，type：{type.FullName}，methodName：{methodName}，bindingFlags：{bindingFlags}");
            return null;
        }
        return methodInfo;
    }
}