using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 局内常量
/// </summary>
public class InGameConst
{
    public static float PointSpacing = 0.8f;//点之间的间距
    public const int CameraSizeMin = 16;//相机size最大值
    public const int CameraSizeMax = 8;//相机size最小值

    // prefab
    public const string Prefab_GameRoot = "Root_InGame"; //游戏节点
    public const string Prefab_Point = "Point"; //点
    public const string Prefab_Arrow = "Arrow"; //箭头
    public const string Prefab_Click = "Click"; //点击特效

    // LayerName
    public const string LayerName_InGame_Bg = "InGame_Bg";
    public const string LayerName_InGame_Element = "InGame_Element";
    public const string LayerName_InGame_TopArea = "InGame_TopArea";

    // Atlas
    public const string Atlas_Tile = "Atlas_Tile";
}
