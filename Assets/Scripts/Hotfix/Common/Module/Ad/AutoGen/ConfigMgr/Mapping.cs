// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * ConfigHub class : Mapping
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System.Collections.Generic;

namespace DragonPlus.ConfigHub.Ad
{
    public class Mapping
    {   
        
        /// <summary>
        /// #分组映射表
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 分组GROUPID
        /// </summary>
        public int UserGroup { get; set; }
        
        /// <summary>
        /// RV广告组; 100=多; 200=中; 300=少; 1000=无
        /// </summary>
        public int AdReward { get; set; }
        
        /// <summary>
        /// 插屏广告组; 100=多; 200=中; 300=少; 1000=无
        /// </summary>
        public int AdInterstitial { get; set; }
        
        /// <summary>
        /// BANNER广告组; 100=多; 200=少
        /// </summary>
        public int AdBanner { get; set; }
        
        /// <summary>
        /// 广告礼包显示组; 100=1.99元; 200=4.99元
        /// </summary>
        public int RemoveAdGroup { get; set; }
        
        /// <summary>
        /// 广告控制; ; 100=多; 200=中; 300=少; 1000=无
        /// </summary>
        public int AdTask { get; set; }
        
    }
}