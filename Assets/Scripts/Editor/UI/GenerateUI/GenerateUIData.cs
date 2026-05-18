using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GenUIData
{
    public GameObject prefab;

    public ClassData uiViewData;
    public Dictionary<string, ClassData> uiSubViewDataDict = new Dictionary<string, ClassData>();
    public ClassData uiWidgetData;

    public StringBuilder errorStr = new StringBuilder();
    public List<string> genSuccessClassNameList = new List<string>();
    public List<string> genFailClassNameList = new List<string>();
}

public class ClassData
{
    public string className;
    public Dictionary<string, FieldData> fieldDataDict = new Dictionary<string, FieldData>();
    public List<string> namespaceList = new List<string>();
}

public class FieldData
{
    public EGenUIFieldType GenUIFieldType;
    public string fieldName;
    public string fieldTypeName;
    public string fieldPath;
}

/// <summary>
/// 生成UI的结构类型
/// </summary>
public enum EGenUIType
{
    View = 1, //界面
    SubView, //子界面
    Widget, //控件
}

/// <summary>
/// 生成UI的字段类型
/// </summary>
public enum EGenUIFieldType
{
    Common = 1, //通用的（UGUI组件等）
    SubView, //子界面
    Container, //容器
}

/// <summary>
/// 生成UI的存档，防止逻辑界面覆盖生成
/// </summary>
public struct GenUIArchive
{
    public string className;
    public string genUITypeStr;
    public string filePath;
}