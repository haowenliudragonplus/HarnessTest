// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
/************************************************
 * ConfigHub class : AdReward
 * This file is can not be modify !!!
 * If there is some problem, ask yunhan.zeng@dragonplus.com
 ************************************************/

using System.Collections.Generic;

namespace DragonPlus.ConfigHub.Ad
{
    public class AdReward
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
        /// 分组GROUPID; 100=多; 200=中; 300=少; 1000=无
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 解锁条件（单位：到达关卡）
        /// </summary>
        public int UnlockLevel { get; set; }
        
        /// <summary>
        /// 间隔时间(秒
        /// </summary>
        public int ShowInterval { get; set; }
        
        /// <summary>
        /// 每日次数限制
        /// </summary>
        public int LimitPerDay { get; set; }
        
        /// <summary>
        /// 奖励ID
        /// </summary>
        public List<int> RewardId { get; set; }
        
        /// <summary>
        /// 奖励数量
        /// </summary>
        public List<int> RewardCnt { get; set; }
        
        /// <summary>
        /// 配置转盘的奖励组ID; （每次抽奖随机一个组的奖励）
        /// </summary>
        public List<int> WheelGroupId { get; set; }
        
    }
}