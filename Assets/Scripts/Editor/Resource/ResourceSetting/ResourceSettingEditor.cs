using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 资源设置窗口
/// </summary>
public class ResourceSettingEditor : EditorWindow
{
    [MenuItem(EditorConst.OpenResourceSettingWindow, priority = EditorConst.Priority_OpenResourceSettingWindow)]
    public static void OpenWindow()
    {
        GetWindow<ResourceSettingEditor>(false, "资源设置窗口", true);
    }

    string[] windowTypeStr = new string[]
    {
        "贴图设置",
        "图集设置",
    };
    int windowTypeIndex;

    private void OnGUI()
    {
        windowTypeIndex = GUILayout.Toolbar(windowTypeIndex, windowTypeStr, GUILayout.Height(50));
        if (windowTypeIndex == 0)
        {
            DrawTextureSetting();
        }
        else if (windowTypeIndex == 1)
        {
            DrawAtlasSetting();
        }
    }

    #region 贴图设置

    private TextureSetting textureSetting = new TextureSetting();
    private List<Object> findDirList_Textute = new List<Object>();

    private void DrawTextureSetting()
    {
        GUILayout.Label("通用设置", EditorStyles.boldLabel);
        textureSetting.textureType = (TextureImporterType)EditorGUILayout.EnumPopup("Texture Type", textureSetting.textureType);
        textureSetting.spriteImportMode = (SpriteImportMode)EditorGUILayout.EnumPopup("Sprite Import Mode", textureSetting.spriteImportMode);
        textureSetting.pixelsPreUnit = EditorGUILayout.FloatField("Pixels Pre Unit", textureSetting.pixelsPreUnit);
        textureSetting.alphaIsTransparency = EditorGUILayout.Toggle("Alpha Is Transparency", textureSetting.alphaIsTransparency);
        textureSetting.readable = EditorGUILayout.Toggle("Readable", textureSetting.readable);
        textureSetting.minimipEnabled = EditorGUILayout.Toggle("Minimip Enabled", textureSetting.minimipEnabled);
        textureSetting.wrapMode = (TextureWrapMode)EditorGUILayout.EnumPopup("Wrap Mode", textureSetting.wrapMode);
        textureSetting.filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", textureSetting.filterMode);
        GUILayout.Label("平台特定设置", EditorStyles.boldLabel);
        textureSetting.textureFormat_Android = (TextureImporterFormat)EditorGUILayout.EnumPopup("Android Format", textureSetting.textureFormat_Android);
        textureSetting.maxSize_Android = EditorGUILayout.IntField("Android Max Size", textureSetting.maxSize_Android);
        textureSetting.textureFormat_IOS = (TextureImporterFormat)EditorGUILayout.EnumPopup("IOS Format", textureSetting.textureFormat_IOS);
        textureSetting.maxSize_IOS = EditorGUILayout.IntField("IOS Max Size", textureSetting.maxSize_IOS);

        GUILayout.Space(20);

        if (findDirList_Textute == null || findDirList_Textute.Count <= 0)
        {
            if (GUILayout.Button("添加路径"))
            {
                findDirList_Textute.Add(null);
            }
            return;
        }

        GUILayout.Space(20);
        for (int i = 0; i < findDirList_Textute.Count; i++)
        {
            GUILayout.BeginHorizontal();
            findDirList_Textute[i] = EditorGUILayout.ObjectField("选择文件夹", findDirList_Textute[i], typeof(Object), false) as Object;
            if (GUILayout.Button("删除", GUILayout.Width(50)))
            {
                findDirList_Textute.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("添加路径"))
        {
            findDirList_Textute.Add(null);
        }
        if (GUILayout.Button("清空所有路径"))
        {
            findDirList_Textute.Clear();
        }
        if (GUILayout.Button("应用"))
        {
            // 查找到所有TextureImporter
            List<TextureImporter> textureImporterList = new List<TextureImporter>();
            foreach (var dir in findDirList_Textute)
            {
                string dirPath = AssetDatabase.GetAssetPath(dir);
                if (string.IsNullOrEmpty(dirPath))
                    continue;
                var filePathList = EditorUtils.GetFileList(new List<string>() { dirPath }, SearchOption.AllDirectories, EditorConst.TextureResSuffix);
                foreach (var filePath in filePathList)
                {
                    TextureImporter importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
                    if (importer == null)
                        continue;
                    textureImporterList.Add(importer);
                }
            }
            // 设置贴图
            for (int i = 0; i < textureImporterList.Count; i++)
            {
                var importer = textureImporterList[i];
                if (EditorUtility.DisplayCancelableProgressBar("设置贴图", $"正在设置贴图：{Path.GetFileNameWithoutExtension(importer.assetPath)} {i + 1}/{textureImporterList.Count}", (i + 1) * 1f / textureImporterList.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                importer.textureType = textureSetting.textureType;
                importer.spriteImportMode = textureSetting.spriteImportMode;
                importer.spritePixelsPerUnit = textureSetting.pixelsPreUnit;
                importer.alphaIsTransparency = textureSetting.alphaIsTransparency;
                importer.isReadable = textureSetting.readable;
                importer.mipmapEnabled = textureSetting.minimipEnabled;
                importer.wrapMode = textureSetting.wrapMode;
                importer.filterMode = textureSetting.filterMode;
                importer.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    name = "Android",
                    format = textureSetting.textureFormat_Android,
                    maxTextureSize = textureSetting.maxSize_Android,
                    overridden = true
                });
                importer.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    name = "iPhone",
                    format = textureSetting.textureFormat_IOS,
                    maxTextureSize = textureSetting.maxSize_IOS,
                    overridden = true
                });
                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    #endregion 贴图设置

    #region 图集设置

    private AtlasSetting atlasSetting = new AtlasSetting();
    private List<Object> findDirList_Atlas = new List<Object>();

    private void DrawAtlasSetting()
    {
        GUILayout.Label("通用设置", EditorStyles.boldLabel);
        atlasSetting.includeInBuild = EditorGUILayout.Toggle("Include In Build", atlasSetting.includeInBuild);
        atlasSetting.enableRotation = EditorGUILayout.Toggle("Enable Rotation", atlasSetting.enableRotation);
        atlasSetting.enableTightPacking = EditorGUILayout.Toggle("Enable Tight Packing", atlasSetting.enableTightPacking);
        atlasSetting.padding = EditorGUILayout.IntField("Padding", atlasSetting.padding);
        atlasSetting.readable = EditorGUILayout.Toggle("Readable", atlasSetting.readable);
        atlasSetting.minimipEnabled = EditorGUILayout.Toggle("Minimip Enabled", atlasSetting.minimipEnabled);
        atlasSetting.filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", atlasSetting.filterMode);
        GUILayout.Label("平台特定设置", EditorStyles.boldLabel);
        atlasSetting.textureFormat_Android = (TextureImporterFormat)EditorGUILayout.EnumPopup("Android Format", atlasSetting.textureFormat_Android);
        atlasSetting.maxSize_Android = EditorGUILayout.IntField("Android Max Size", atlasSetting.maxSize_Android);
        atlasSetting.textureFormat_IOS = (TextureImporterFormat)EditorGUILayout.EnumPopup("IOS Format", atlasSetting.textureFormat_IOS);
        atlasSetting.maxSize_IOS = EditorGUILayout.IntField("IOS Max Size", atlasSetting.maxSize_IOS);

        GUILayout.Space(20);

        if (GUILayout.Button("应用项目中全部图集"))
        {
            if (EditorUtility.DisplayDialog("应用项目中全部图集", "应用项目中全部图集", "确定", "取消"))
            {
                var filePathList = EditorUtils.GetFileList(new List<string>() { EditorConst.ProjectRoot }, SearchOption.AllDirectories, EditorConst.SpriteAtlasResSuffix);
                SetSpriteAtlas(filePathList);

                EditorUtility.ClearProgressBar();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        GUILayout.Space(20);
        if (findDirList_Atlas == null || findDirList_Atlas.Count <= 0)
        {
            if (GUILayout.Button("添加路径"))
            {
                findDirList_Atlas.Add(null);
            }
            return;
        }

        GUILayout.Space(20);
        for (int i = 0; i < findDirList_Atlas.Count; i++)
        {
            GUILayout.BeginHorizontal();
            findDirList_Atlas[i] = EditorGUILayout.ObjectField("选择文件夹", findDirList_Atlas[i], typeof(Object), false) as Object;
            if (GUILayout.Button("删除", GUILayout.Width(50)))
            {
                findDirList_Atlas.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("添加路径"))
        {
            findDirList_Atlas.Add(null);
        }
        if (GUILayout.Button("清空所有路径"))
        {
            findDirList_Atlas.Clear();
        }
        if (GUILayout.Button("应用"))
        {
            List<string> dirList = new List<string>();
            foreach (var dir in findDirList_Atlas)
            {
                string dirPath = AssetDatabase.GetAssetPath(dir);
                if (string.IsNullOrEmpty(dirPath))
                    continue;
                dirList.Add(dirPath);
            }
            var filePathList = EditorUtils.GetFileList(dirList, SearchOption.AllDirectories, EditorConst.SpriteAtlasResSuffix);
            SetSpriteAtlas(filePathList);

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void SetSpriteAtlas(List<string> pathList)
    {
        // 获取所有SpriteAtlasImporter和图集引用的全部贴图的TextureImporter
        List<SpriteAtlasImporter> spriteAtlasImporterList = new List<SpriteAtlasImporter>();
        List<TextureImporter> textureImporterList = new List<TextureImporter>();
        foreach (var path in pathList)
        {
            SpriteAtlasImporter spriteAtlasImporter = AssetImporter.GetAtPath(path) as SpriteAtlasImporter;
            if (spriteAtlasImporter != null)
            {
                spriteAtlasImporterList.Add(spriteAtlasImporter);
            }

            SpriteAtlas spriteAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
            if (spriteAtlas != null)
            {
                var packObjList = spriteAtlas.GetPackables();
                foreach (var packObj in packObjList)
                {
                    var textureDir = AssetDatabase.GetAssetPath(packObj);
                    var filePathList = EditorUtils.GetFileList(new List<string>() { textureDir }, SearchOption.AllDirectories, EditorConst.TextureResSuffix);
                    foreach (var filePath in filePathList)
                    {
                        TextureImporter textureImporter = AssetImporter.GetAtPath(filePath) as TextureImporter;
                        if (textureImporter == null)
                            continue;
                        if (textureImporter.textureType != TextureImporterType.Sprite)
                            continue;
                        textureImporterList.Add(textureImporter);
                    }
                }
            }
        }

        // 设置图集
        for (int i = 0; i < spriteAtlasImporterList.Count; i++)
        {
            var importer = spriteAtlasImporterList[i];
            if (EditorUtility.DisplayCancelableProgressBar("设置图集", $"正在设置图集：{Path.GetFileNameWithoutExtension(importer.assetPath)} {i + 1}/{spriteAtlasImporterList.Count}", (i + 1) * 1f / spriteAtlasImporterList.Count))
            {
                EditorUtility.ClearProgressBar();
                return;
            }
            importer.includeInBuild = atlasSetting.includeInBuild;
            SpriteAtlasPackingSettings packingSettings = importer.packingSettings;
            packingSettings.enableRotation = atlasSetting.enableRotation;
            packingSettings.enableTightPacking = atlasSetting.enableTightPacking;
            packingSettings.padding = atlasSetting.padding;
            importer.packingSettings = packingSettings;
            SpriteAtlasTextureSettings textureSettings = importer.textureSettings;
            textureSettings.readable = atlasSetting.readable;
            textureSettings.generateMipMaps = atlasSetting.minimipEnabled;
            textureSettings.filterMode = atlasSetting.filterMode;
            importer.textureSettings = textureSettings;
            importer.SetPlatformSettings(new TextureImporterPlatformSettings
            {
                name = "Android",
                format = atlasSetting.textureFormat_Android,
                maxTextureSize = atlasSetting.maxSize_Android,
                overridden = true
            });
            importer.SetPlatformSettings(new TextureImporterPlatformSettings
            {
                name = "iPhone",
                format = atlasSetting.textureFormat_IOS,
                maxTextureSize = atlasSetting.maxSize_IOS,
                overridden = true
            });
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
        }

        // 将图集中引用的全部贴图设置为非override，防止图集设置被贴图设置覆盖
        for (int i = 0; i < textureImporterList.Count; i++)
        {
            var importer = textureImporterList[i];
            if (EditorUtility.DisplayCancelableProgressBar("设置贴图", $"正在设置贴图：{Path.GetFileNameWithoutExtension(importer.assetPath)} {i + 1}/{textureImporterList.Count}", (i + 1) * 1f / textureImporterList.Count))
            {
                EditorUtility.ClearProgressBar();
                return;
            }
            bool dirty = false;
            var platformSettings_Android = importer.GetPlatformTextureSettings("Android");
            if (platformSettings_Android.overridden)
            {
                platformSettings_Android.overridden = false;
                importer.SetPlatformTextureSettings(platformSettings_Android);
                dirty = true;
            }
            var platformSettings_IOS = importer.GetPlatformTextureSettings("iPhone");
            if (platformSettings_IOS.overridden)
            {
                platformSettings_IOS.overridden = false;
                importer.SetPlatformTextureSettings(platformSettings_IOS);
                dirty = true;
            }
            if (dirty)
            {
                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
            }
        }
    }

    #endregion 图集设置
}