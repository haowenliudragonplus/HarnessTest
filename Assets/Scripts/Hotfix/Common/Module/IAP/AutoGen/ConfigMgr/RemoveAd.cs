// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * ConfigHub class : RemoveAd
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System.Collections.Generic;

namespace DragonPlus.ConfigHub.IAP
{
    public class RemoveAd
    {   
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 分组
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 解锁关卡
        /// </summary>
        public int UnlockLevel { get; set; }
        
        /// <summary>
        /// SHOP表内对应的礼包
        /// </summary>
        public List<int> ShopId { get; set; }
        
    }
}