/************************************************
 * Config class : Table_Common_NotificationInfo
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_NotificationInfo:ConfigBase
    {   
        /// <summary>
        /// #ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// #推送ID
        /// </summary>
        public int ScheduleId { get; set; }
        
        /// <summary>
        /// #CD时间
        /// </summary>
        public int CdTimeInSecond { get; set; }
        
        /// <summary>
        /// #推送TITLE多语言KEY
        /// </summary>
        public string NotificationLocaleTitleKey { get; set; }
        
        /// <summary>
        /// #推送内容KEY
        /// </summary>
        public string NotificationLocaleContentKey { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}