/************************************************
 * Config class : Table_InGame_Level
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.InGame
{
    public partial class Table_InGame_Level:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 关卡文件名
        /// </summary>
        public string Json { get; set; }
        
        /// <summary>
        /// 关卡步数
        /// </summary>
        public int Move { get; set; }
        
        /// <summary>
        /// 关卡模式类型
        /// </summary>
        public int ModeType { get; set; }
        
        /// <summary>
        /// 关卡难度类型
        /// </summary>
        public int DifficultyType { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}