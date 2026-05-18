using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 跨域调用器
/// </summary>
/// 不支持调用重载函数
public static class CrossDomainBridge
{
    public class TypeData
    {
        public Dictionary<string, MethodInfo> methodInfoDict = new Dictionary<string, MethodInfo>();
        // 如果需要还可以缓存属性、字段等.....

        public MethodInfo GetMethodInfo(string methodName)
        {
            if (!methodInfoDict.TryGetValue(methodName, out var _methodInfo))
                return null;
            return _methodInfo;
        }
    }

    private static Dictionary<string, TypeData> typeDataDict = new Dictionary<string, TypeData>();

    /// <summary>
    /// 调用静态方法
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <param name="nameSpace"></param>
    /// <param name="typeName"></param>
    /// <param name="methodName"></param>
    /// <param name="argsArray">方法参数数组</param>
    /// <param name="genericTypeArray">泛型参数类型数组</param>
    /// <param name="argsTypeArray">方法参数类型数组</param>
    /// <returns></returns>
    public static object InvokeStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName,
        object[] argsArray = null, Type[] genericTypeArray = null, Type[] argsTypeArray = null)
    {
        string typeStr = assemblyName + "." + nameSpace + "." + typeName;
        if (!typeDataDict.TryGetValue(typeStr, out var _typeData))
        {
            var methodInfo = ReflectUtils.GetMethodInfo(assemblyName, nameSpace, typeName, methodName,
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
            if (methodInfo == null)
                return null;

            _typeData = new TypeData();
            _typeData.methodInfoDict.Add(methodName, methodInfo);
            typeDataDict.Add(typeStr, _typeData);

            var ret = methodInfo.Invoke(null, argsArray);
            return ret;
        }
        else
        {
            var _methodInfo = _typeData.GetMethodInfo(methodName);
            if (_methodInfo != null)
            {
                var ret = _methodInfo.Invoke(null, argsArray);
                return ret;
            }
            else
            {
                var methodInfo = ReflectUtils.GetMethodInfo(assemblyName, nameSpace, typeName, methodName,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
                if (methodInfo == null)
                    return null;

                _typeData.methodInfoDict.Add(methodName, methodInfo);
                var ret = methodInfo.Invoke(null, argsArray);
                return ret;
            }
        }
    }

    /// <summary>
    /// 调用非静态方法
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <param name="nameSpace"></param>
    /// <param name="typeName"></param>
    /// <param name="methodName"></param>
    /// <param name="instance"></param>
    /// <param name="argsArray">方法参数数组</param>
    /// <param name="genericTypeArray">泛型参数类型数组</param>
    /// <param name="argsTypeArray">方法参数类型数组</param>
    /// <returns></returns>
    public static object InvokeNoneStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName,
        object instance, object[] argsArray = null, Type[] genericTypeArray = null, Type[] argsTypeArray = null)
    {
        string typeStr = assemblyName + "." + nameSpace + "." + typeName;
        if (!typeDataDict.TryGetValue(typeStr, out var _typeData))
        {
            var methodInfo = ReflectUtils.GetMethodInfo(assemblyName, nameSpace, typeName, methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
            if (methodInfo == null)
                return null;

            _typeData = new TypeData();
            _typeData.methodInfoDict.Add(methodName, methodInfo);
            typeDataDict.Add(typeStr, _typeData);

            var ret = methodInfo.Invoke(instance, argsArray);
            return ret;
        }
        else
        {
            var _methodInfo = _typeData.GetMethodInfo(methodName);
            if (_methodInfo != null)
            {
                var ret = _methodInfo.Invoke(instance, argsArray);
                return ret;
            }
            else
            {
                var methodInfo = ReflectUtils.GetMethodInfo(assemblyName, nameSpace, typeName, methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, genericTypeArray, argsTypeArray);
                if (methodInfo == null)
                    return null;

                _typeData.methodInfoDict.Add(methodName, methodInfo);
                var ret = methodInfo.Invoke(instance, argsArray);
                return ret;
            }
        }
    }
}