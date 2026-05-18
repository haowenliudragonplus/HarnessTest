/************************************************
 * Config class : Table_Common_Guide
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Guide:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 教程ID
        /// </summary>
        public string GuideId { get; set; }
        
        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 加载关卡; 进入关卡后加载的第几个ID; 链接到  MAHJONG-GUIDELEVEL表
        /// </summary>
        public int GuideLevel { get; set; }
        
        /// <summary>
        /// 触发位置; 1.触发关卡ID -进入触发; 2、上一步引导结束; 3、消除完成
        /// </summary>
        public int TriggerPosition { get; set; }
        
        /// <summary>
        /// 触发参数
        /// </summary>
        public string TriggerParam { get; set; }
        
        /// <summary>
        /// 触发条件
        /// </summary>
        public string TriggerCondition { get; set; }
        
        /// <summary>
        /// 前置引导
        /// </summary>
        public string PreGuideId { get; set; }
        
        /// <summary>
        /// 引导延迟出现时间（单位毫秒）
        /// </summary>
        public int DelayShowGuide { get; set; }
        
        /// <summary>
        /// ; 11.点击箭头; 12.点击UI道具; 13.手指滑动
        /// </summary>
        public List<int> TargetTypes { get; set; }
        
        /// <summary>
        /// 目标参数; TARGETTYPES=11：箭头ID; TARGETTYPES=12：道具ID
        /// </summary>
        public List<string> TargetParams { get; set; }
        
        /// <summary>
        /// TARGET是否提高到最上层
        /// </summary>
        public bool TargetToTop { get; set; }
        
        /// <summary>
        /// 当引导出现的时候; 就标记引导完成，; 不论玩家操作与否
        /// </summary>
        public bool SaveFlagWhenTrigger { get; set; }
        
        /// <summary>
        /// 是否忽略缓存引导完成状态; (可以重复完成的引导为TRUE)
        /// </summary>
        public bool IgnoreCache { get; set; }
        
        /// <summary>
        /// 是否存盘; (连续步骤中，; 最后一步存盘)
        /// </summary>
        public bool SaveFlag { get; set; }
        
        /// <summary>
        /// 存储系列教程ID组
        /// </summary>
        public string SaveGroup { get; set; }
        
        /// <summary>
        /// 是否有引导完成提示展示
        /// </summary>
        public bool HasGuideCompleteDisplayTip { get; set; }
        
        /// <summary>
        /// 引导完成提示展示时间
        /// </summary>
        public float GuideCompleteDisplayTipTime { get; set; }
        
        /// <summary>
        /// 引导完成提示展示描述文字KEY
        /// </summary>
        public string GuideCompleteTipDes { get; set; }
        
        /// <summary>
        /// 带头像类文本的位置; 1.上; 2.上中; 3.中; 4.中下; 5.下; 41.中下下; 42.上上中
        /// </summary>
        public int GuideCompleteTipPos { get; set; }
        
        /// <summary>
        /// 是否需要相机聚焦
        /// </summary>
        public bool CameraFocus { get; set; }
        
        /// <summary>
        /// 相机聚焦位置
        /// </summary>
        public string CameraFocusPos { get; set; }
        
        /// <summary>
        /// 文字指引(KEY)
        /// </summary>
        public string TextGuide { get; set; }
        
        /// <summary>
        /// 文本是否使用
        /// </summary>
        public bool NPCTextEnable { get; set; }
        
        /// <summary>
        /// 文本类型
        /// </summary>
        public string TextType { get; set; }
        
        /// <summary>
        /// 位移距离
        /// </summary>
        public int TextMove { get; set; }
        
        /// <summary>
        /// 毫秒
        /// </summary>
        public int TextMoveTime { get; set; }
        
        /// <summary>
        /// 头像类文本的位置; 1.上; 2.上中; 3.中; 4.中下; 5.下; 41.中下下; 42.上上中
        /// </summary>
        public int NPCTextPos { get; set; }
        
        /// <summary>
        /// 箭头是否使用
        /// </summary>
        public List<int> ArrowEnable { get; set; }
        
        /// <summary>
        /// 箭头位置; 1.左; 2.右; 3.上; 4.下
        /// </summary>
        public List<int> ArrowPos { get; set; }
        
        /// <summary>
        /// 手指是否使用; 不填写=不使用; 1=手指; 2=大箭头; 3=手放大; 4=缩放小手
        /// </summary>
        public int FingerEnable { get; set; }
        
        /// <summary>
        /// 手指点击位置偏移; X,Y
        /// </summary>
        public List<string> FingerPos { get; set; }
        
        /// <summary>
        /// FINGER操作的物体信息
        /// </summary>
        public string FingerDragInfo { get; set; }
        
        /// <summary>
        /// 0:不移动; 1:从TARGET0向TARGET1移动
        /// </summary>
        public int FingerMoveType { get; set; }
        
        /// <summary>
        /// 重置手指移动的时间间隔
        /// </summary>
        public float FingerMoveCD { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool FingerRotate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float FingerRotateValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float FingerMoveX { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float FingerMoveY { get; set; }
        
        /// <summary>
        /// MASK射线是否开启; UI就打开射线检测
        /// </summary>
        public bool MaskRaycastEnable { get; set; }
        
        /// <summary>
        /// MASK是否使用
        /// </summary>
        public bool MaskEnable { get; set; }
        
        /// <summary>
        /// MASK是否使用遮罩色
        /// </summary>
        public float MaskColor { get; set; }
        
        /// <summary>
        /// MASK的形状; 1.圆形; 2.椭圆; 3.矩形; 4.无; 5.地格; 10.高亮
        /// </summary>
        public int MaskShape { get; set; }
        
        /// <summary>
        /// 形状大小
        /// </summary>
        public List<int> ShapeSize { get; set; }
        
        /// <summary>
        /// 点击完成的类型; 1.由逻辑的控制的点击目标完成; 2.点击遮罩的MASK区域完成; 3.点击遮罩即结束本条; 4.点击到3D目标物结束
        /// </summary>
        public int ClickToFinishType { get; set; }
        
        /// <summary>
        /// BI; 通过START和END事件查询，DATA1记录的是ID
        /// </summary>
        public string Bi { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}