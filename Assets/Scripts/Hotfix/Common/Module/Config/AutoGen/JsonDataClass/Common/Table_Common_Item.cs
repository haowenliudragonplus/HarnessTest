/************************************************
 * Config class : Table_Common_Item
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Item:ConfigBase
    {   
        /// <summary>
        /// 道具类型
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 图标
        /// </summary>
        public int Icon { get; set; }
        
        /// <summary>
        /// 是否是限时道具
        /// </summary>
        public bool IsTimeLimitItem { get; set; }
        
        /// <summary>
        /// 对应的限时道具ID
        /// </summary>
        public int TimeLimitItemId { get; set; }
        
        /// <summary>
        /// 数量上限
        /// </summary>
        public long NumLimit { get; set; }
        
        /// <summary>
        /// 自动获得的间隔
        /// </summary>
        public long AutoAddInterval { get; set; }
        
        /// <summary>
        /// 自动获得的数量
        /// </summary>
        public int AutoAddCount { get; set; }
        
        /// <summary>
        /// 功能跳转ID
        /// </summary>
        public int Jump { get; set; }
        
        /// <summary>
        /// 飞物体时的排序
        /// </summary>
        public int FlyOrder { get; set; }
        
        /// <summary>
        /// 道具价格
        /// </summary>
        public int Price { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}