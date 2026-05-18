/************************************************
 * Config class : Table_ClientUserGroup_Rules
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.ClientUserGroup
{
    public partial class Table_ClientUserGroup_Rules:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 判断条件类型
        /// </summary>
        public int RuleType { get; set; }
        
        /// <summary>
        /// 操作符类型
        /// </summary>
        public int RuleOp { get; set; }
        
        /// <summary>
        /// 数值参数
        /// </summary>
        public int RulePara { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}