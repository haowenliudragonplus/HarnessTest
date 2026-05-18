/************************************************
 * Config class : Table_Common_Icon
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Icon:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// ICON图标PREFAB
        /// </summary>
        public string IconId { get; set; }
        
        /// <summary>
        /// 控制ICON的; 所属区域; 0、没有入口; 1、左; 2、右
        /// </summary>
        public int Area { get; set; }
        
        /// <summary>
        /// 活动ICON的排序; ; 从上到下 越小排在最上面
        /// </summary>
        public int Order { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}