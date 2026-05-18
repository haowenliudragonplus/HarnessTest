using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// 生成UI工具
/// </summary>
/*****
 生成规则
 1.UIView下可以嵌套UISubView
 2.UISubView下可以嵌套UISubView
 3.UIWidget只能作为单独预制体生成自身
 4.UIContainer只能作为UIView或UISubView下的空节点，它下面的一切节点都不会生成
 *****/
public class GenerateUIEditor
{
    //**********只有按照以下规范命名的节点才会生成，可扩展
    private static Dictionary<string, Type> Name2ComponentType = new Dictionary<string, Type>()
    {
        { "UIBtn", typeof(Button) },
        { "UIOldTxt", typeof(Text) },
        { "UITxt", typeof(TextMeshProUGUI) },
        { "UIImg", typeof(Image) },
        { "UIRawImg", typeof(RawImage) },
        { "UIToggle", typeof(Toggle) },
        { "UITG", typeof(ToggleGroup) },
        { "UILayoutH", typeof(HorizontalLayoutGroup) },
        { "UILayoutV", typeof(VerticalLayoutGroup) },
        { "UILayoutHV", typeof(GridLayoutGroup) },
        { "UIIF", typeof(InputField) },
        { "UISlider", typeof(Slider) },
        { "UINode", typeof(RectTransform) },
        { "UISR", typeof(ScrollRect) },
        { "UITmpIF", typeof(TMP_InputField) },
        { "UIBar", typeof(Scrollbar) },
    };

    public static string UIGENINFOARCHIVEPATH = Application.dataPath + "/../UIGenInfo.json"; //已经生成过的逻辑代码序列化成json存项目根目录下，防止覆盖生成逻辑代码

    //模板路径
    private const string UIVIEW_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewLogicTemplate.txt"; //UIView逻辑模板路径
    private const string UIVIEW_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewViewTemplate.txt"; //UIView界面模板路径
    private const string UISUBVIEW_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewLogicTemplate.txt"; //UISubView逻辑模板路径
    private const string UISUBVIEW_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewViewTemplate.txt"; //UISubView界面模板路径
    private const string UIWIDGET_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIWidgetLogicTemplate.txt"; //UIWidget逻辑模板路径
    private const string UIWIDGET_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIWidgetViewTemplate.txt"; //UIWidget界面模板路径

    //生成代码文件夹
    private const string UIVIEW_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/Common/Module/UI/AutoGen/View/"; //自动生成UIView界面代码的文件夹
    private const string UISUBVIEW_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/Common/Module/UI/AutoGen/SubView/"; //自动生成UISubView界面代码的文件夹
    private const string UIWIDGET_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/Common/Module/UI/AutoGen/Widget/"; //自动生成UIWidget界面代码的文件夹

    private const string EXTRANAME_AUTOGEN = "Base";
    private const string PREFIX_UISUBVIEW = "UISubView";
    private const string PREFIX_UICONTAINER = "UIContainer";
    private const string NAMESPACE_DEFINE_TEMPLATE = "using #Namespace#;"; //命名空间定义模板
    private const string COMMON_FIELD_DEFINE_TEMPLATE = "\tprotected #FieldType# #FieldName#;"; //通用字段定义模板
    private const string SUBVIEW_FIELD_DEFINE_TEMPLATE = "\tprotected #FieldType# #FieldName#;"; //子界面字段定义模板
    private const string CONTAINER_FIELD_DEFINE_TEMPLATE = "\tprotected UIContainer #FieldName#;"; //容器字段定义模板
    private const string COMMON_FIELD_BIND_TEMPLATE = "\t\t#FieldName# = GO.transform.Find(\"#FieldPath#\").GetComponent<#ComponentType#>();"; //通用字段绑定模板
    private const string SUBVIEW_FIELD_BIND_TEMPLATE = "\t\t#FieldName# =new #FieldName#();\n" +
                                                       "\t\t#FieldName#.InternalInit(this, \"#FieldName#\");\n" +
                                                       "\t\t#FieldName#.InternalCreateWithoutInstantiate(GO.transform.Find(\"#FieldPath#\").gameObject);"; //子界面字段绑定模板
    private const string CONTAINER_FIELD_BIND_TEMPLATE = "\t\t#FieldName# =new UIContainer();\n" +
                                                         "\t\t#FieldName#.InternalInit(GO.transform.Find(\"#FieldPath#\").gameObject , this);"; //容器字段绑定模版

    private static GenUIData genUIData;

    [MenuItem(EditorConst.GenUIView, false, EditorConst.Priority_GenUIView)]
    private static void GenUIViewCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUIViewCode(selectedGo);
    }

    [MenuItem(EditorConst.GenUIView, true, EditorConst.Priority_GenUIView)]
    private static bool ValidateGenUIViewCode()
    {
        return EditorUtils.ValidateSelectObject<GameObject>();
    }

    [MenuItem(EditorConst.GenUISubView, false, EditorConst.Priority_GenUISubView)]
    private static void GenUISubViewCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUISubViewCode(selectedGo);
    }

    [MenuItem(EditorConst.GenUISubView, true, EditorConst.Priority_GenUISubView)]
    private static bool ValidateGenUISubViewCode()
    {
        return EditorUtils.ValidateSelectObject<GameObject>();
    }

    [MenuItem(EditorConst.GenUIWidget, false, EditorConst.Priority_GenUIWidget)]
    private static void GenUIWidgetCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUIWidget(selectedGo);
    }

    [MenuItem(EditorConst.GenUIWidget, true, EditorConst.Priority_GenUIWidget)]
    private static bool ValidateGenUIWidgetCode()
    {
        return EditorUtils.ValidateSelectObject<GameObject>();
    }

    /// <summary>
    /// 生成UIView
    /// </summary>
    private static void GenUIViewCode(GameObject go)
    {
        genUIData = new GenUIData();
        genUIData.prefab = go;
        ClassData uiViewData = new ClassData();
        uiViewData.className = go.name;
        genUIData.uiViewData = uiViewData;

        // 收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(genUIData, root, root, EGenUIType.View);
        // 生成UIView代码
        if (!RegexUtils.IsCSValidName(genUIData.uiViewData.className))
        {
            genUIData.errorStr.AppendLine($"生成UIView失败，预制体{genUIData.uiViewData.className}命名不符合C#规则");
            genUIData.genFailClassNameList.Add(genUIData.uiViewData.className);
        }
        else
        {
            if (!GenUIViewCode_Logic(genUIData.uiViewData))
                return;
            GenUIViewCode_View(genUIData.uiViewData);
        }

        // 生成UISubView代码
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            if (!RegexUtils.IsCSValidName(uiSubViewData.className))
            {
                genUIData.errorStr.AppendLine($"生成UISubView失败，预制体{genUIData.prefab.name}下的SubView节点{uiSubViewData.className}命名不符合C#规则");
                genUIData.genFailClassNameList.Add(uiSubViewData.className);
            }
            else
            {
                if (GenUISubViewCode_Logic(uiSubViewData))
                {
                    GenUISubViewCode_View(uiSubViewData);
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GenerateUIResultWindow.CreateWindow("生成UIView结果", GenResultStr());
    }

    /// <summary>
    /// 生成UISubView
    /// </summary>
    private static void GenUISubViewCode(GameObject go)
    {
        genUIData = new GenUIData();
        genUIData.prefab = go;
        genUIData.uiViewData = null;

        // 收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(genUIData, root, root, EGenUIType.SubView);
        // 生成UISubView代码
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            if (!RegexUtils.IsCSValidName(uiSubViewData.className))
            {
                if (uiSubViewData.className == genUIData.prefab.name)
                {
                    genUIData.errorStr.AppendLine($"生成UISubView失败，预制体{genUIData.prefab.name}命名不符合C#规则");
                }
                else
                {
                    genUIData.errorStr.AppendLine($"生成UISubView失败，预制体{genUIData.prefab.name}下的SubView节点{uiSubViewData.className}命名不符合C#规则");
                }
                genUIData.genFailClassNameList.Add(uiSubViewData.className);
            }
            else
            {
                if (GenUISubViewCode_Logic(uiSubViewData))
                {
                    GenUISubViewCode_View(uiSubViewData);
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GenerateUIResultWindow.CreateWindow("生成UISubView结果", GenResultStr());
    }

    /// <summary>
    /// 生成UIWidget
    /// </summary>
    private static void GenUIWidget(GameObject go)
    {
        genUIData = new GenUIData();
        genUIData.prefab = go;
        genUIData.uiViewData = null;
        ClassData uiWidgetData = new ClassData();
        uiWidgetData.className = go.name;
        genUIData.uiWidgetData = uiWidgetData;

        // 收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(genUIData, root, root, EGenUIType.Widget);
        // 生成UIWidget代码
        if (!RegexUtils.IsCSValidName(genUIData.uiWidgetData.className))
        {
            genUIData.errorStr.AppendLine($"生成UIWidget失败，预制体{genUIData.prefab.name}命名不符合C#规则");
            genUIData.genFailClassNameList.Add(genUIData.uiWidgetData.className);
        }
        else
        {
            if (GenUIWidgetCode_Logic(genUIData.uiWidgetData))
            {
                GenUIWidgetCode_View(genUIData.uiWidgetData);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GenerateUIResultWindow.CreateWindow("生成UIWidget结果", GenResultStr());
    }

    private static void CollectGenUIData(GenUIData genUIData, Transform targetTrans, Transform rootTrans, EGenUIType genUIType)
    {
        for (int i = 0; i < targetTrans.childCount; i++)
        {
            Transform trans = targetTrans.GetChild(i);
            string transName = trans.name;
            string namePrefix = transName.Split('_')[0];
            // 不是所有的类型都可以递归查找
            bool canRecursiveFind = genUIType == EGenUIType.View || genUIType == EGenUIType.SubView;
            if (namePrefix == PREFIX_UISUBVIEW && canRecursiveFind)
            {
                AddGenUIData(genUIData, genUIType, EGenUIFieldType.SubView, trans, null, trans.name, rootTrans);
                CollectGenUIData(genUIData, trans, trans, EGenUIType.SubView);
                continue;
            }
            if (namePrefix == PREFIX_UICONTAINER)
            {
                AddGenUIData(genUIData, genUIType, EGenUIFieldType.Container, trans, null, trans.name, rootTrans);
                continue;
            }

            CollectGenUIData(genUIData, trans, rootTrans, genUIType);
            if (!Name2ComponentType.TryGetValue(namePrefix, out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
            {
                genUIData.errorStr.AppendLine($"预制体{genUIData.prefab.name}下的{transName}节点找不到{type.Name}组件");
                continue;
            }
            AddGenUIData(genUIData, genUIType, EGenUIFieldType.Common, trans, type, type.Name, rootTrans);
        }
    }

    /// <summary>
    /// 添加生成UI数据
    /// </summary>
    private static void AddGenUIData(GenUIData genUIData, EGenUIType genUIType, EGenUIFieldType genUIFieldType, Transform trans, Type type, string typeName, Transform rootTrans)
    {
        if (genUIType == EGenUIType.View)
        {
            // 先创建UISubView，防止UISubView下面没有可生成的节点时不会生成此UISubView
            if (genUIFieldType == EGenUIFieldType.SubView)
            {
                string uiSubViewName = trans.name;
                ClassData uiSubViewData = new ClassData();
                uiSubViewData.className = uiSubViewName;
                genUIData.uiSubViewDataDict.Add(uiSubViewName, uiSubViewData);
            }
            // 给UIView中添加数据
            AddFieldData(genUIData.uiViewData, genUIFieldType, trans, type, typeName, rootTrans);
        }
        else if (genUIType == EGenUIType.SubView)
        {
            // 先创建UISubView，防止UISubView下面没有可生成的节点时不会生成此UISubView
            if (genUIFieldType == EGenUIFieldType.SubView)
            {
                string uiSubViewName = trans.name;
                ClassData uiSubViewData = new ClassData();
                uiSubViewData.className = uiSubViewName;
                genUIData.uiSubViewDataDict.Add(uiSubViewName, uiSubViewData);
            }
            // 给UISubView中添加数据
            string belongUISubViewName = rootTrans.name;
            if (!genUIData.uiSubViewDataDict.TryGetValue(belongUISubViewName, out var subViewData))
            {
                subViewData = new ClassData();
                subViewData.className = belongUISubViewName;
                genUIData.uiSubViewDataDict.Add(belongUISubViewName, subViewData);
            }
            AddFieldData(subViewData, genUIFieldType, trans, type, typeName, rootTrans);
        }
        else if (genUIType == EGenUIType.Widget)
        {
            AddFieldData(genUIData.uiWidgetData, genUIFieldType, trans, type, typeName, rootTrans);
        }
    }

    /// <summary>
    /// 添加字段数据
    /// </summary>
    private static void AddFieldData(ClassData classData, EGenUIFieldType genUIFieldType, Transform trans, Type type, string typeName, Transform rootTrans)
    {
        string transName = trans.name;
        if (!RegexUtils.IsCSValidName(transName))
        {
            if (classData.className == genUIData.prefab.name)
            {
                genUIData.errorStr.AppendLine($"预制体{genUIData.prefab.name}下的{transName}节点命名不符合C#规则");
            }
            else
            {
                genUIData.errorStr.AppendLine($"预制体{genUIData.prefab.name}下的{classData.className}下的{transName}节点命名不符合C#规则");
            }
            return;
        }
        if (classData.fieldDataDict.ContainsKey(transName))
        {
            if (classData.className == genUIData.prefab.name)
            {
                genUIData.errorStr.AppendLine($"预制体{genUIData.prefab.name}下存在相同{transName}名字的{typeName}组件");
            }
            else
            {
                genUIData.errorStr.AppendLine($"预制体{genUIData.prefab.name}下的{classData.className}下存在相同{transName}名字的{typeName}组件");
            }
            return;
        }
        if (type != null)
        {
            classData.namespaceList.Add(type.Namespace);
        }
        FieldData fieldData = new FieldData();
        fieldData.GenUIFieldType = genUIFieldType;
        fieldData.fieldName = transName;
        fieldData.fieldTypeName = typeName;
        fieldData.fieldPath = GameUtils.CalculateTransPath(trans, rootTrans);
        classData.fieldDataDict.Add(transName, fieldData);
    }

    /// <summary>
    /// 生成UIView逻辑代码
    /// </summary>
    private static bool GenUIViewCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(UIVIEW_LOGIC_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UIVIEW_LOGIC_TEMPLATE_PATH}中不存在UIView逻辑模板");
            genUIData.genFailClassNameList.Add(classData.className);
            return false;
        }
        if (HasGenLogicCode(classData.className))
            return true;
        string logicCode = File.ReadAllText(UIVIEW_LOGIC_TEMPLATE_PATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string logicFileDir = EditorUtility.OpenFolderPanel($"选择UIView：{classData.className} 逻辑脚本路径", Application.dataPath + "/Scripts", "");
        if (string.IsNullOrEmpty(logicFileDir))
            return false;
        string filePath = $"{logicFileDir}/{classData.className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath, EGenUIType.View);
        genUIData.genSuccessClassNameList.Add(classData.className);
        return true;
    }

    /// <summary>
    /// 生成UIView界面代码
    /// </summary>
    private static void GenUIViewCode_View(ClassData classData)
    {
        string className = $"{classData.className}{EXTRANAME_AUTOGEN}";
        if (!IOUtils.FileExist(UIVIEW_VIEW_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UIVIEW_VIEW_TEMPLATE_PATH}中不存在UIView界面模板");
            genUIData.genFailClassNameList.Add(className);
            return;
        }
        string viewCode = File.ReadAllText(UIVIEW_VIEW_TEMPLATE_PATH);
        viewCode = viewCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{UIVIEW_VIEW_GENCODE_DIR}{className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
        genUIData.genSuccessClassNameList.Add(className);
    }

    /// <summary>
    /// 生成UISubView逻辑代码
    /// </summary>
    private static bool GenUISubViewCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(UISUBVIEW_LOGIC_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UISUBVIEW_LOGIC_TEMPLATE_PATH}中不存在UISubView逻辑模板");
            genUIData.genFailClassNameList.Add(classData.className);
            return false;
        }
        if (HasGenLogicCode(classData.className))
            return true;
        string logicCode = File.ReadAllText(UISUBVIEW_LOGIC_TEMPLATE_PATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string logicFileDir = EditorUtility.OpenFolderPanel($"选择UISubView：{classData.className} 逻辑脚本路径", Application.dataPath + "/Scripts", "");
        if (string.IsNullOrEmpty(logicFileDir))
            return false;
        string filePath = $"{logicFileDir}/{classData.className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath, EGenUIType.SubView);
        genUIData.genSuccessClassNameList.Add(classData.className);
        return true;
    }

    /// <summary>
    /// 生成UISubView界面代码
    /// </summary>
    private static void GenUISubViewCode_View(ClassData classData)
    {
        string className = $"{classData.className}{EXTRANAME_AUTOGEN}";
        if (!IOUtils.FileExist(UISUBVIEW_VIEW_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UISUBVIEW_VIEW_TEMPLATE_PATH}中不存在UISubView界面模板");
            genUIData.genFailClassNameList.Add(className);
            return;
        }
        string viewCode = File.ReadAllText(UISUBVIEW_VIEW_TEMPLATE_PATH);
        viewCode = viewCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{UISUBVIEW_VIEW_GENCODE_DIR}{className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
        genUIData.genSuccessClassNameList.Add(className);
    }

    /// <summary>
    /// 生成UIWidget逻辑代码
    /// </summary>
    private static bool GenUIWidgetCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(UIWIDGET_LOGIC_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UIWIDGET_LOGIC_TEMPLATE_PATH}中不存在UIWidget逻辑模板");
            genUIData.genFailClassNameList.Add(classData.className);
            return false;
        }
        if (HasGenLogicCode(classData.className))
            return true;
        string logicCode = File.ReadAllText(UIWIDGET_LOGIC_TEMPLATE_PATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string logicFileDir = EditorUtility.OpenFolderPanel($"选择UIWidget：{classData.className} 逻辑脚本路径", Application.dataPath + "/Scripts", "");
        if (string.IsNullOrEmpty(logicFileDir))
            return false;
        string filePath = $"{logicFileDir}/{classData.className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath, EGenUIType.Widget);
        genUIData.genSuccessClassNameList.Add(classData.className);
        return true;
    }

    /// <summary>
    /// 生成UIWidget界面代码
    /// </summary>
    private static void GenUIWidgetCode_View(ClassData classData)
    {
        string className = $"{classData.className}{EXTRANAME_AUTOGEN}";
        if (!IOUtils.FileExist(UIWIDGET_VIEW_TEMPLATE_PATH))
        {
            genUIData.errorStr.AppendLine($"{UIWIDGET_VIEW_TEMPLATE_PATH}中不存在UIWidget界面模板");
            genUIData.genFailClassNameList.Add(className);
            return;
        }
        string viewCode = File.ReadAllText(UIWIDGET_VIEW_TEMPLATE_PATH);
        viewCode = viewCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{UIWIDGET_VIEW_GENCODE_DIR}{className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
        genUIData.genSuccessClassNameList.Add(className);
    }

    /// <summary>
    /// 生成命名空间定义代码
    /// </summary>
    private static string GenNamespaceCode(ClassData classData)
    {
        StringBuilder str = new StringBuilder();
        HashSet<string> hashSet = new HashSet<string>(classData.namespaceList);
        foreach (var nameSpace in hashSet)
        {
            string namespaceDefineStr = NAMESPACE_DEFINE_TEMPLATE;
            namespaceDefineStr = namespaceDefineStr.Replace("#Namespace#", nameSpace);
            str.AppendLine(namespaceDefineStr + "");
        }
        return str.ToString();
    }

    /// <summary>
    /// 生成字段定义代码
    /// </summary>
    private static string GenFieldDefineCode(List<FieldData> fieldDatas)
    {
        StringBuilder str = new StringBuilder();
        foreach (var fieldData in fieldDatas)
        {
            string fieldDefineStr = string.Empty;
            switch (fieldData.GenUIFieldType)
            {
                case EGenUIFieldType.Common:
                    fieldDefineStr = COMMON_FIELD_DEFINE_TEMPLATE;
                    fieldDefineStr = fieldDefineStr.Replace("#FieldType#", fieldData.fieldTypeName);
                    fieldDefineStr = fieldDefineStr.Replace("#FieldName#", fieldData.fieldName);
                    break;

                case EGenUIFieldType.SubView:
                    fieldDefineStr = SUBVIEW_FIELD_DEFINE_TEMPLATE;
                    fieldDefineStr = fieldDefineStr.Replace("#FieldType#", fieldData.fieldTypeName);
                    fieldDefineStr = fieldDefineStr.Replace("#FieldName#", fieldData.fieldName);
                    break;

                case EGenUIFieldType.Container:
                    fieldDefineStr = CONTAINER_FIELD_DEFINE_TEMPLATE;
                    fieldDefineStr = fieldDefineStr.Replace("#FieldName#", fieldData.fieldName);
                    break;
            }
            str.AppendLine(fieldDefineStr + "");
        }
        return str.ToString();
    }

    /// <summary>
    /// 生成字段绑定代码
    /// </summary>
    private static string GenFieldBindCode(List<FieldData> fieldDatas)
    {
        StringBuilder str = new StringBuilder();
        foreach (var fieldData in fieldDatas)
        {
            string fieldBindStr = string.Empty;
            switch (fieldData.GenUIFieldType)
            {
                case EGenUIFieldType.Common:
                    fieldBindStr = COMMON_FIELD_BIND_TEMPLATE;
                    fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.fieldName);
                    fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.fieldPath);
                    fieldBindStr = fieldBindStr.Replace("#ComponentType#", fieldData.fieldTypeName);
                    str.AppendLine(fieldBindStr + "");
                    break;

                case EGenUIFieldType.SubView:
                    fieldBindStr = SUBVIEW_FIELD_BIND_TEMPLATE;
                    fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.fieldName);
                    fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.fieldPath);
                    str.AppendLine(fieldBindStr + "");
                    break;

                case EGenUIFieldType.Container:
                    fieldBindStr = CONTAINER_FIELD_BIND_TEMPLATE;
                    fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.fieldName);
                    fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.fieldPath);
                    str.AppendLine(fieldBindStr + "");
                    break;
            }
        }
        return str.ToString();
    }

    private static string GenResultStr()
    {
        string genResultStr = string.Empty;
        genResultStr += "生成成功的类：\n";
        foreach (var className in genUIData.genSuccessClassNameList)
        {
            genResultStr += className + "\n";
        }
        genResultStr += "\n生成失败的类：\n";
        foreach (var className in genUIData.genFailClassNameList)
        {
            genResultStr += className + "\n";
        }
        if (!string.IsNullOrEmpty(genUIData.errorStr.ToString()))
        {
            genResultStr += "\n错误信息：\n";
            genResultStr += genUIData.errorStr;
        }
        return genResultStr;
    }

    private static bool HasGenLogicCode(string className)
    {
        if (!IOUtils.FileExist(UIGENINFOARCHIVEPATH))
            return false;
        string content = File.ReadAllText(UIGENINFOARCHIVEPATH);
        List<GenUIArchive> genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(content);
        if (genUIArchives == null)
            return false;
        var hasGen = genUIArchives.Exists(v => v.className == className);
        return hasGen;
    }

    private static void SaveToGenUIInfoArchive(ClassData classData, string logicCodePath, EGenUIType genUIType)
    {
        List<GenUIArchive> genUIArchives;
        if (!IOUtils.FileExist(UIGENINFOARCHIVEPATH))
        {
            genUIArchives = new List<GenUIArchive>();
        }
        else
        {
            string str = File.ReadAllText(UIGENINFOARCHIVEPATH);
            genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(str);
        }
        if (genUIArchives == null)
            return;
        GenUIArchive genUIArchive = new GenUIArchive();
        genUIArchive.className = classData.className;
        genUIArchive.genUITypeStr = genUIType.ToString();
        genUIArchive.filePath = logicCodePath;
        genUIArchives.Add(genUIArchive);
        string content = JsonConvert.SerializeObject(genUIArchives, Formatting.Indented);
        IOUtils.WirteToFile(UIGENINFOARCHIVEPATH, content);
    }
}