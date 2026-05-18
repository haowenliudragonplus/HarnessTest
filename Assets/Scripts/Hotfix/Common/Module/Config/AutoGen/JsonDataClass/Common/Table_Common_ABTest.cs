/************************************************
 * Config class : Table_Common_ABTest
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_ABTest:ConfigBase
    {   
        /// <summary>
        /// ABTEST类型
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// ABTEST的KEY
        /// </summary>
        public string AbTestKey { get; set; }
        
        /// <summary>
        /// 默认分组
        /// </summary>
        public int DefaultGroup { get; set; }
        
        /// <summary>
        /// 是否固化
        /// </summary>
        public bool EnableFixed { get; set; }
        
        /// <summary>
        /// 客户端分组池子
        /// </summary>
        public List<int> ClientGroupPool { get; set; }
        
        /// <summary>
        /// 参与分组的最小首次登录APP版本号
        /// </summary>
        public int MinFirstAppVersion { get; set; }
        
        /// <summary>
        /// 参与分组的最大首次登录APP版本号
        /// </summary>
        public int MaxFirstAppVersion { get; set; }
        
        /// <summary>
        /// 参与过的用户是否强制使用分组
        /// </summary>
        public bool ForceUseGroupForJoin { get; set; }
        
        /// <summary>
        /// 强制使用的分组
        /// </summary>
        public int ForceUseGroup { get; set; }
        
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool Finish { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}