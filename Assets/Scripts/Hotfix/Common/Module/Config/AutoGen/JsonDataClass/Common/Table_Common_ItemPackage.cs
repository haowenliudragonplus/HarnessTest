/************************************************
 * Config class : Table_Common_ItemPackage
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_ItemPackage:ConfigBase
    {   
        /// <summary>
        /// 道具组类型
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 物品ID列表
        /// </summary>
        public List<int> ItemIdList { get; set; }
        
        /// <summary>
        /// 物品数量列表
        /// </summary>
        public List<int> ItemCountList { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}