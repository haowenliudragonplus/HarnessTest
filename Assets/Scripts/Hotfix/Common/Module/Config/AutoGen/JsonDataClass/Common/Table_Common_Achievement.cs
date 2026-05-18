/************************************************
 * Config class : Table_Common_Achievement
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Achievement:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// 图集
        /// </summary>
        public string Atlas { get; set; }
        
        /// <summary>
        /// ICON图
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// 成就名字
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 任务类型
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        
        /// <summary>
        /// 阶段
        /// </summary>
        public int Stage { get; set; }
        
        /// <summary>
        /// 开启关卡; 大于等于多少关时 开启这个成就
        /// </summary>
        public int UnlockLevel { get; set; }
        
        /// <summary>
        /// 进度
        /// </summary>
        public int Count { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}