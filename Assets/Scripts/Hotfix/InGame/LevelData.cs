using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int id;              // 关卡ID
    public int desc;            // 描述ID（废弃）
    public int hp;              // 生命值
    public int time;            // 时间限制
    public int high;            // 关卡高度
    public int wide;            // 关卡宽度
    public List<List<int>> level;  // 每个元素 - 元素拐点坐标列表
    public int type;            // 关卡类型（废弃）
    public int showImage;       // 显示图片ID（废弃）
    public int guide;           // 引导ID（废弃）
    public int soundMapping;    // （废弃）
    public List<string> color;  // 关卡线段颜色列表（十六进制字符串）
}