// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/18/14:40
// Ver : 1.0.0
// Description : GuideType.cs
// ChangeLog :
// **********************************************

namespace TMGame
{
    public enum GuideTrigger
    {
        LevelStart = 1, //关卡开始
        GuideEnd = 2, //引导之后 参数:guideId
    }

    // 对应表里的 targetTypes
    public enum GuideTargetType
    {
        None = 0,

        ClickBox = 10,
        ClickArrowEntity = 11,
        ClickItem = 12,
        ZoomCamera = 13,

    }

    enum GuideActionTrigger
    {
        TMatchDirectly = 1,
    }

    public enum GuideNpcPos
    {
        None = 0,
        Top = 1, //上
        TopMiddle = 2, //上中
        Middle = 3, //中
        MiddleButtom = 4, //中下
        Buttom = 5, //下

        MiddleButtomButtom = 41, //中下下
        TopDownLittle = 42, // 上的下面一点点
    }

    enum GuideArrowPosition
    {
        None = 0,
        Left,
        Right,
        Top,
        Bottom
    }

    enum GuideMaskShape
    {
        None = 0,
        Circle = 1, //圆
        Oval = 2, //椭圆
        Rectangle = 3, //矩形
        HighLight = 10,   //高亮显示
    }

    enum ClickToFinishType
    {
        LogicTarget = 1, //由逻辑的控制的点击目标完成
        Mask = 2, //点击遮罩的mask区域完成
        AnyWhere = 3, //点击遮罩即结束本条
        Click3DTargetFinish = 4, //点击任何地方即结束本条引导
    }
}