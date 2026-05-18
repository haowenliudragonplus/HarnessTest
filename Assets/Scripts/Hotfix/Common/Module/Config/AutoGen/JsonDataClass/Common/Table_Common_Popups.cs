/************************************************
 * Config class : Table_Common_Popups
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Popups:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// UI界面ID
        /// </summary>
        public int UiViewId { get; set; }
        
        /// <summary>
        /// 优先级(数值越大越靠前); 100-199  付费类; 200-299 活动类; 300-399 高优付费类; 400-499 广告类; 900+结算/奖励/反馈类
        /// </summary>
        public int Priority { get; set; }
        
        /// <summary>
        /// 触发循环弹出的关卡数; -进入关卡次数
        /// </summary>
        public int LevelFinishRequired { get; set; }
        
        /// <summary>
        /// 是否首次登陆添加弹出
        /// </summary>
        public int Showscene1 { get; set; }
        
        /// <summary>
        /// 每日弹出上限
        /// </summary>
        public int Showscene4 { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}