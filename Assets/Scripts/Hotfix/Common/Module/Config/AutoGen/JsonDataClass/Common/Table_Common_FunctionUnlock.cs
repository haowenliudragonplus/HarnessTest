/************************************************
 * Config class : Table_Common_FunctionUnlock
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_FunctionUnlock:ConfigBase
    {   
        /// <summary>
        /// 功能解锁类型
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 解锁条件类型列表
        /// </summary>
        public List<int> FunctionUnlockConditionTypeList { get; set; }
        
        /// <summary>
        /// 解锁条件参数列表
        /// </summary>
        public List<int> FunctionUnlockConditionParamList { get; set; }
        
        /// <summary>
        /// 解锁条件额外参数列表
        /// </summary>
        public List<string> FunctionUnlockConditionExtraParamList { get; set; }
        
        /// <summary>
        /// 是否展示功能解锁弹窗
        /// </summary>
        public bool IsShowPopups { get; set; }
        
        /// <summary>
        /// 功能开启弹窗排序
        /// </summary>
        public int PopupsOrder { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}