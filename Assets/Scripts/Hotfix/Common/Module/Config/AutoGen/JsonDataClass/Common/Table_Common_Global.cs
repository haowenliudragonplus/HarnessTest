/************************************************
 * Config class : Table_Common_Global
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Global:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 活动拉取CD（秒）
        /// </summary>
        public int SeverActivityDataFetchCD { get; set; }
        
        /// <summary>
        /// 买一个体力需要花费的金币
        /// </summary>
        public int AddEnergyPrice { get; set; }
        
        /// <summary>
        /// 商店看广告得金币数量
        /// </summary>
        public int ShopRVRewardCoins { get; set; }
        
        /// <summary>
        /// 插屏广告在注册N秒后开启
        /// </summary>
        public int LimitDuractionCgetInterstitia { get; set; }
        
        /// <summary>
        /// 好评弹出上限次数(包含功能开启)
        /// </summary>
        public int RateUsLimitNumber { get; set; }
        
        /// <summary>
        /// RATEUS功能开放的累积关卡数(通过此关)
        /// </summary>
        public int RateUsUnlockLevel { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}