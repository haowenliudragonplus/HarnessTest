/************************************************
 * Config class : Table_Shop_Shop
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Shop
{
    public partial class Table_Shop_Shop:ConfigBase
    {   
        /// <summary>
        /// #
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// # 价格
        /// </summary>
        public string Price { get; set; }
        
        /// <summary>
        /// # 商品在GOOGLEPLAY中的ID
        /// </summary>
        public string Product_id { get; set; }
        
        /// <summary>
        /// # 商品在APPSTORE中的ID
        /// </summary>
        public string Product_id_ios { get; set; }
        
        /// <summary>
        /// # 0:普通商品,; 1:不可消耗商品（例如去广告）,; 2:订阅（例如视频VIP会员）
        /// </summary>
        public int PurchaseType { get; set; }
        
        /// <summary>
        /// # 商品类型; 1.去广告礼包
        /// </summary>
        public int ShopType { get; set; }
        
        /// <summary>
        /// #SCREW玩法的商品; 填写此列，此处填写; SCREWMATCH3-GLOBAL表中; 的SHOP页签中的ID; （用于表格映射使用）
        /// </summary>
        public int RelateScrewShopid { get; set; }
        
        /// <summary>
        /// # 物品ID（ITEM表）
        /// </summary>
        public List<int> ItemId { get; set; }
        
        /// <summary>
        /// # 物品数量
        /// </summary>
        public List<int> ItemCnt { get; set; }
        
        /// <summary>
        /// # 图集名称
        /// </summary>
        public string Atlas { get; set; }
        
        /// <summary>
        /// # 商品图标
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// # 商品名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// # 商品描述
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// # 背景板类型; 0.忽略; 1.黄色; 2.橘色; 
        /// </summary>
        public int BgType { get; set; }
        
        /// <summary>
        /// # 0-普通；1-限量优惠；2-最受欢迎；3-火爆最优；
        /// </summary>
        public int Market { get; set; }
        
        /// <summary>
        /// 折扣显示：转换成百分数显示
        /// </summary>
        public int ShowDiscount { get; set; }
        
        /// <summary>
        /// 限购次数：; 默认不限。
        /// </summary>
        public int LmtNum { get; set; }
        
        /// <summary>
        /// # 被购买后续; 触发的礼包ID
        /// </summary>
        public List<int> BundleList { get; set; }
        
        /// <summary>
        /// #商品展示顺序；值越大越靠后
        /// </summary>
        public string ShowOrder { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}