// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * ConfigHub class : Mapping
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System.Collections.Generic;

namespace DragonPlus.ConfigHub.IAP
{
    public class Mapping
    {   
        
        /// <summary>
        /// #分组映射表
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 分组GROUPID; 市场分层的组
        /// </summary>
        public int UserGroup { get; set; }
        
        /// <summary>
        /// 内购分组; 5=大额; 4=中大额; 3=中额; 2=中小额; 1=小额
        /// </summary>
        public int IapGroup { get; set; }
        
    }
}