using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 资源设置
/// </summary>
[CreateAssetMenu(fileName = "资源配置", menuName = "ResourceSetting", order = 0)]
public class ResourceSetting : ScriptableObject
{
}

/// <summary>
/// 图片设置
/// </summary>
public class TextureSetting
{
    [Header("通用设置")]
    public TextureImporterType textureType = TextureImporterType.Sprite;
    public SpriteImportMode spriteImportMode = SpriteImportMode.Single;
    public float pixelsPreUnit = 100f;
    public bool alphaIsTransparency = true;
    public bool readable = false;
    public bool minimipEnabled = false;
    public TextureWrapMode wrapMode = TextureWrapMode.Clamp;
    public FilterMode filterMode = FilterMode.Bilinear;
    [Header("Android设置")]
    public TextureImporterFormat textureFormat_Android = TextureImporterFormat.ASTC_6x6;
    public int maxSize_Android = 1024;
    [Header("IOS设置")]
    public TextureImporterFormat textureFormat_IOS = TextureImporterFormat.ASTC_6x6;
    public int maxSize_IOS = 1024;
}

public class AtlasSetting
{
    [Header("通用设置")]
    // unity2022打包测试
    // 例如一个图集aa，预制体bb里引用了图集aa中部分图片，分别打两个ab包，解包后查看
    // 不勾选includeInBuild，图集aa的占用内存会变小（只是少了一些依赖关系的数据信息），但是预制体bb中会单独存放一份引用的图片资源（造成冗余）
    // 勾选includeInBuild后，图集aa的占用内存会变大（内部会多记录一些依赖关系的数据信息）
    // 先说结论：勾选includeInBuild
    // 勾选后，虽然图集包体的内存占用会变大，但是这个增大量可忽略不计，几千个图集也就几mb
    // 但是不勾选后，预制体中会单独存放一份引用的图片资源，造成冗余，包体的增加量会更显著，并且逻辑使用时还需要late binding
    public bool includeInBuild = true;
    public bool enableRotation = false;
    public bool enableTightPacking = false;
    public int padding = 4;
    public bool readable = false;
    public bool minimipEnabled = false;
    public FilterMode filterMode = FilterMode.Bilinear;
    [Header("Android设置")]
    public TextureImporterFormat textureFormat_Android = TextureImporterFormat.ASTC_6x6;
    public int maxSize_Android = 2048;
    [Header("IOS设置")]
    public TextureImporterFormat textureFormat_IOS = TextureImporterFormat.ASTC_6x6;
    public int maxSize_IOS = 2048;
}