/************************************************
 * Config class : Table_Common_UIView
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_UIView:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// RESOURCE表ID
        /// </summary>
        public int ResourceId { get; set; }
        
        /// <summary>
        /// UI所属层级
        /// </summary>
        public int LayerType { get; set; }
        
        /// <summary>
        /// 界面类型
        /// </summary>
        public int Type { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}