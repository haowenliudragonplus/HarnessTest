/************************************************
 * Config class : Table_Common_FunctionJump
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_FunctionJump:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 跳转类型
        /// </summary>
        public int JumpType { get; set; }
        
        /// <summary>
        /// 参数数组
        /// </summary>
        public List<string> ParamArray { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}