/************************************************
 * Config class : Table_InGame_Global
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.InGame
{
    public partial class Table_InGame_Global:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 结算后获得金币奖励数量-简单
        /// </summary>
        public int WinRewardCoinNumberSimple { get; set; }
        
        /// <summary>
        /// 结算后获得金币奖励数量-困难
        /// </summary>
        public int WinRewardCoinNumberHard { get; set; }
        
        /// <summary>
        /// 结算后获得金币奖励数量-超级困难
        /// </summary>
        public int WinRewardCoinNumberSuperHard { get; set; }
        
        /// <summary>
        /// 提示道具在多少秒后没操作出现
        /// </summary>
        public int HintCd { get; set; }
        
        /// <summary>
        /// 多少关卡都是免费复活
        /// </summary>
        public int FreeRevive { get; set; }
        
        /// <summary>
        /// 复活后增加步数
        /// </summary>
        public int MoveRevive { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}