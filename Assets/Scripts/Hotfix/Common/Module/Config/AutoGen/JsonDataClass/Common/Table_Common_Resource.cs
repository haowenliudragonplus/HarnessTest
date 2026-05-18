/************************************************
 * Config class : Table_Common_Resource
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Resource:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 资源KEY（资源名称）
        /// </summary>
        public string ResourceKey { get; set; }
        
        /// <summary>
        /// 资源类型
        /// </summary>
        public int ResourceType { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}