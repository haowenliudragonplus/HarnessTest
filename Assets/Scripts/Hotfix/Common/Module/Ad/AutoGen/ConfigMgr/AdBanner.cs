// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * ConfigHub class : AdBanner
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System.Collections.Generic;

namespace DragonPlus.ConfigHub.Ad
{
    public class AdBanner
    {   
        
        /// <summary>
        /// #
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 广告位
        /// </summary>
        public int PlaceId { get; set; }
        
        /// <summary>
        /// BANNER广告组; 100=多; 200=少
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 到达XX关后解锁广告; ; （新增）
        /// </summary>
        public int UnlockLevel { get; set; }
        
    }
}