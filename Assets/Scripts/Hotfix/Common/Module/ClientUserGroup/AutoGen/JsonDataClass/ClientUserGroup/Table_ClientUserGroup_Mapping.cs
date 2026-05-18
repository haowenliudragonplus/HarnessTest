/************************************************
 * Config class : Table_ClientUserGroup_Mapping
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.ClientUserGroup
{
    public partial class Table_ClientUserGroup_Mapping:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 用户条件列表
        /// </summary>
        public List<int> PlayerRuleList { get; set; }
        
        /// <summary>
        /// 难度阶数
        /// </summary>
        public int DifficultyStage { get; set; }
        
        /// <summary>
        /// 难度灵敏度
        /// </summary>
        public int DifficultySensitivity { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}