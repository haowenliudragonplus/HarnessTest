/************************************************
 * Config class : Table_RemoveAd_RemoveAd
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.RemoveAd
{
    public partial class Table_RemoveAd_RemoveAd:ConfigBase
    {   
        /// <summary>
        /// #
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 广告礼包显示组; 100=低价; 200=高价
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 去广告礼包推送解锁条件（单位：关卡）
        /// </summary>
        public int UnlockLevel { get; set; }
        
        /// <summary>
        /// SHOP表对应ID
        /// </summary>
        public List<int> RemoveAdsGiftId { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}