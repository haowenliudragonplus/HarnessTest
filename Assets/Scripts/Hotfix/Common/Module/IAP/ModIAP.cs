using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DragonPlus;
using DragonPlus.Config;
using DragonPlus.Config.Common;
using DragonPlus.Config.Shop;
using DragonPlus.ConfigHub.Ad;
using DragonPlus.ConfigHub.IAP;
using DragonPlus.Core;
using DragonPlus.InAppPurchasing;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using Newtonsoft.Json;
using TMGame;
using UnityEngine;
using UnityEngine.Purchasing;

/// <summary>
/// 商店类型
/// </summary>
public enum EShopType
{
    RemoveAd = 1,

    Coin = 2,
    Pack = 3,
    PiggyBank = 4,
    ReviveGift = 5,
    Newbie = 6,
    GoldenPass = 7,
    IceBreaking = 8,
    EndlessGiftLT = 9,
    TripleGift = 10,
    EndlessGift = 11,
}

/// <summary>
/// 支付模块
/// </summary>
public class ModIAP : ModuleBase
{
    private const int DefaultUserGroup = 0;//默认用户分组

    private StorageShop storageShop;//商店存档

    private bool inPaying;//是否正在支付中
    private bool inDebugPayMode;//是否在模拟支付模式
    public bool InDebugPayMode
    {
        get
        {
            if (Main.GameUtils.IsInEditorEnv())
                return true;
            else if (!Main.GameUtils.IsDevelopmentEnv())
                inDebugPayMode = false;
            return inDebugPayMode;
        }
        set
        {
            inDebugPayMode = value;
        }
    }

    private object userData;//调用购买时传入的用户自定义数据

    private Dictionary<EShopType, PaymentHandler> paymentHandlerDict;//购买不同类型的商品后的处理

    private CancellationTokenSource cts;

    public override void OnInit()
    {
        base.OnInit();

        storageShop = Game.GetMod<ModStorage>().GetStorage<StorageClientCommon>().Shop;

        EventBus.Subscribe<UnfulfilledPaymentPending>(OnFindPaddingPayment);
        EventBus.Subscribe<EventConfigHubUpdatedEvent>(OnEventConfigHubUpdated);
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();

        InitIAP();
        InitPaymentHandleDict();
    }

    public override void OnDispose()
    {
        base.OnDispose();

        cts?.Cancel();
        cts = null;

        inPaying = false;
        paymentHandlerDict.Clear();
    }

    /// <summary>
    /// 初始化购买不同商品后的操作
    /// </summary>
    /// **********需要在此扩展**********
    private void InitPaymentHandleDict()
    {
        paymentHandlerDict = new Dictionary<EShopType, PaymentHandler>();
        paymentHandlerDict.Add(EShopType.RemoveAd, new PaymentHandler_RemoveAd());
    }

    /// <summary>
    /// 购买
    /// </summary>
    public void Purchase(int shopId, object userData = null)
    {
        cts?.Cancel();

        var shopCfg = GetShopCfgById(shopId);
        if (shopCfg == null)
        {
            CLog.Error($"找不到商品配置：{shopId}");
            return;
        }

        this.userData = userData;
        string productId = GetProductId(shopCfg);

        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventIapShopIdClick,
            data1: shopCfg.Id.ToString(),
            data2: shopCfg.Price);

        var products = SDK<IAP>.Instance.GetAllProductInfo();

        // 模拟支付
        if (InDebugPayMode)
        {
            // 模拟真机支付
            Game.GetMod<ModUI>().ShowWaiting();
            CoreUtils.WaitSeconds(2, () =>
            {
                Game.GetMod<ModUI>().CloseWaiting();
                if (paymentHandlerDict.TryGetValue((EShopType)shopCfg.ShopType, out var _paymentHandler))
                {
                    _paymentHandler?.HandlePaymentSuccess();
                }

                // 存档中记录购买信息
                RecordShopPurchaseInfo(shopCfg);
                // 获取奖励
                GetIAPReward(shopCfg);
            }).Forget();
            return;
        }

        // 正式支付
        if (products == null
            || products.Length <= 0
            || Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
#if UNITY_ANDROID
                content = CoreUtils.GetLocalization("&key.UI_cannot_connect_to_google_play"),
#elif UNITY_IOS
                content = CoreUtils.GetLocalization("&key.UI_cannot_connect_to_itunes_store"),
#else
                content = CoreUtils.GetLocalization("&key.UI_purchase_failed"),
#endif
                showCloseBtn = true,
                showMidBtn = true,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_PurchaseFailure, openData);
            return;
        }

        CLog.Info($"准备购买商品：{productId}");
        Game.GetMod<ModUI>().ShowWaiting();
        inPaying = true;

#if UNITY_ANDROID
        if (SDK<IAP>.Instance.IsProductAlreadyOwned(productId))
        {
            // 该商品有未完成订单，尝试一次补单
            CLog.Info($"该商品为未完成状态，尝试补单，取消本次付费行为，productId：{productId}");
            RequestUnfulfilledPaymentsAndTryVerify(productId);
            return;
        }
#endif

        SDK<IAP>.Instance.PurchaseProduct(productId, OnPurchased);

        cts = new CancellationTokenSource();
        CoreUtils.WaitSeconds(120, () =>
        {
            cts = null;
            if (inPaying)
            {
                Game.GetMod<ModUI>().CloseWaiting();
            }
        }, cts.Token).Forget();
    }

    /// <summary>
    /// 购买回调
    /// </summary>
    void OnPurchased(bool success, string productId, Product product, PurchaseFailureReason failureReason, bool b)
    {
        cts?.Cancel();

        Game.GetMod<ModUI>().CloseWaiting();

        var shopCfg = GetShopCfgByProductId(productId);

        if (shopCfg == null)
        {
            CLog.Error($"找不到商品配置，productId：{productId}");
            return;
        }

        if (success && product != null)
        {
            CLog.Info($"购买成功，shopId：{shopCfg.Id}");

            if (paymentHandlerDict.TryGetValue((EShopType)shopCfg.ShopType, out var _paymentHandler))
            {
                _paymentHandler?.HandlePaymentSuccess();
            }

            // 存档中记录购买信息
            RecordShopPurchaseInfo(shopCfg);
            // 获取奖励
            GetIAPReward(shopCfg);
        }
        else
        {
            CLog.Warning($"购买失败，shopId：{shopCfg.Id}，失败原因：{failureReason}");

            if (paymentHandlerDict.TryGetValue((EShopType)shopCfg.ShopType, out var _paymentHandler))
            {
                _paymentHandler?.HandlePaymentFailed();
            }

            if (failureReason == PurchaseFailureReason.UserCancelled)
            {
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    showMidBtn = true,
                    content = CoreUtils.GetLocalization("&key.UI_common_iap"),
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_PurchaseFailure, openData);
            }
            else
            {
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    showMidBtn = true,
                    content = CoreUtils.GetLocalization(failureReason == PurchaseFailureReason.Unknown ? "&key.UI_shop_fail_explain" : "&key.UI_purchase_failed_title"),
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_PurchaseFailure, openData);
            }
        }

        inPaying = false;
    }

    /// <summary>
    /// 获取奖励
    /// </summary>
    private void GetIAPReward(Table_Shop_Shop shopCfg)
    {
        if ((EShopType)shopCfg.ShopType == EShopType.RemoveAd)
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_common_no_ad_buy"),
                showCloseBtn = false,
                showMidBtn = true
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
        }
        else
        {
            // 添加奖励
            Game.GetMod<ModBag>().AddItem(shopCfg.ItemId, shopCfg.ItemCnt, new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.Iap)
            {
                data1 = GetProductId(shopCfg),
                data2 = shopCfg.Price,
            });
            // 显示奖励展示界面
            List<ItemData> itemDataList = new List<ItemData>();
            for (int i = 0; i < shopCfg.ItemCnt.Count; i++)
            {
                itemDataList.Add(new ItemData { amount = shopCfg.ItemCnt[i], id = shopCfg.ItemId[i] });
            }
            UIGetRewardParam data = new UIGetRewardParam()
            {
                itemDatas = itemDataList,
                fly = true,
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_CommonReward, data);
        }

        RefreshUserGroup();

        Game.GetMod<ModEvent>().Dispatch(new EvtIAPSuccess(shopCfg, userData));
    }

    /// <summary>
    /// 刷新玩家分组
    /// </summary>
    private void RefreshUserGroup()
    {
        try
        {
            //强制刷新一次玩家用户分组
            CoreUtils.WaitSeconds(15, () => { SDK<ConfigHub>.Instance.FetchRemoteConfig(true); }).Forget();
        }
        catch (Exception e)
        {
            CLog.Error(e.Message);
        }
    }

    /// <summary>
    /// 获得展示价格
    /// </summary>
    public string GetDisplayPrice(int shopId)
    {
        var shopCfg = GetShopCfgById(shopId);
        if (shopCfg == null)
        {
            CLog.Error($"找不到商品配置，shopId：{shopId}");
            return "";
        }

        var products = SDK<IAP>.Instance.GetAllProductInfo();
        if (products != null && products.Length > 0)
        {
            var product = GetProductInfo(shopCfg.Id);
            if (product == null || product.metadata == null)
            {
                CLog.Error("product获取失败，shopId：" + shopId);
                return shopCfg.Price;
            }
            return product.metadata.localizedPriceString;
        }
        CLog.Info("价格从配置获取" + shopCfg.Price);
        return "$" + shopCfg.Price;
    }

    /// <summary>
    /// 获取商品信息
    /// </summary>
    public Product GetProductInfo(int shopId)
    {
        var productList = SDK<IAP>.Instance.GetAllProductInfo();
        if (productList == null)
            return null;

        var shopConfig = GetShopCfgById(shopId);
        if (shopConfig == null)
            return null;

        foreach (var p in productList)
        {
#if UNITY_IOS
                if (p.definition.storeSpecificId == shopConfig.Product_id_ios)
                {
                    return p;
                }
#elif UNITY_ANDROID
            if (p.definition.storeSpecificId == shopConfig.Product_id)
            {
                return p;
            }
#endif
        }

        return null;
    }

    private void OnEventConfigHubUpdated(EventConfigHubUpdatedEvent evt)
    {

    }

    /// <summary>
    /// 收到补单的回调
    /// </summary>
    private void OnFindPaddingPayment(UnfulfilledPaymentPending evt)
    {
        var productId = evt.data.ProductId;
        if (string.IsNullOrEmpty(productId))
        {
            CLog.Info("收到补单通知，productId为空，不用处理！");
            return;
        }
        if (!evt.data.pending)
        {
            CLog.Info("收到补单通知，订单状态为pending！不用处理！");
            return;
        }

        if (!Game.GetMod<GuideSys>().IsShowingGuide())
        {
            TryHandleUnfulfilledPayments();
        }
    }

    /// <summary>
    /// 获取商店展示的列表
    /// </summary>
    public List<Table_Shop_Shop> GetDisplayShopCfgList()
    {
        // todo：抽空改成从表里配置是否商店展示
        var shopCfgList = GetShopCfgListByShopType(new List<EShopType>() { EShopType.Coin, EShopType.Pack });

        List<Table_Shop_Shop> ret = new List<Table_Shop_Shop>();
        foreach (var shopCfg in shopCfgList)
        {
            ret.Add(shopCfg);
        }
        return ret;
    }

    private void InitIAP()
    {
        var configs = GetShopCfgList();
        var consumableProductIds = new List<string>();
        var nonconsumableProductIds = new List<string>();
        var subProductIds = new List<string>();
        foreach (var config in configs)
        {
            var productId = "";
#if UNITY_IOS
            productId = config.Product_id_ios;
#elif UNITY_ANDROID
            productId = config.Product_id;
#endif

            switch (config.PurchaseType)
            {
                case 0:
                    consumableProductIds.Add(productId);
                    break;

                case 1:
                    nonconsumableProductIds.Add(productId);
                    break;

                case 2:
                    subProductIds.Add(productId);
                    break;
            }
        }
#if !UNITY_STANDALONE
        SDK<IAP>.Instance.Init(consumableProductIds, nonconsumableProductIds, subProductIds);
#endif

        // 请求补单
        RequestUnfulfilledPaymentsAndTryVerify();
    }

    /// <summary>
    /// 获取当前分组
    /// </summary>
    public int GetCurGroup()
    {
        var mappingList = GetMappingList();
        if (mappingList != null && mappingList.Count > 0)
        {
            var group = mappingList[0].UserGroup;
            return group;
        }
        return DefaultUserGroup;
    }

    /// <summary>
    /// 获取当前mapping
    /// </summary>
    public DragonPlus.ConfigHub.IAP.Mapping GetCurMapping()
    {
        var groups = GetMappingList();
        if (groups != null && groups.Count > 0)
        {
            return groups[0];
        }
        return null;
    }

    private List<DragonPlus.ConfigHub.IAP.Mapping> GetMappingList()
    {
        return IAPConfigManager.Instance.GetConfig<DragonPlus.ConfigHub.IAP.Mapping>();
    }

    /// <summary>
    /// 获取所有商品配置
    /// </summary>
    public List<Table_Shop_Shop> GetShopCfgList()
    {
        var shopCfgList = Game.GetMod<ModConfig>().GetConfigs<Table_Shop_Shop>();
        return shopCfgList;
    }

    /// <summary>
    /// 根据商店类型获取商品配置列表
    /// </summary>
    public List<Table_Shop_Shop> GetShopCfgListByShopType(EShopType shopType)
    {
        var shopCfgList = GetShopCfgList();
        List<Table_Shop_Shop> ret = new List<Table_Shop_Shop>();
        foreach (var cfg in shopCfgList)
        {
            if (cfg.ShopType != (int)shopType)
                continue;
            ret.Add(cfg);
        }
        return ret;
    }

    /// <summary>
    /// 根据商店类型获取商品配置列表
    /// </summary>
    public List<Table_Shop_Shop> GetShopCfgListByShopType(List<EShopType> shopTypeList)
    {
        var shopCfgList = GetShopCfgList();
        List<Table_Shop_Shop> ret = new List<Table_Shop_Shop>();
        foreach (var cfg in shopCfgList)
        {
            if (!shopTypeList.Contains((EShopType)cfg.ShopType))
                continue;
            ret.Add(cfg);
        }
        return ret;
    }

    /// <summary>
    /// 根据shopId获取某个商品配置
    /// </summary>
    public Table_Shop_Shop GetShopCfgById(int shopId)
    {
        return Game.GetMod<ModConfig>().GetConfig<Table_Shop_Shop>(shopId);
    }

    /// <summary>
    /// 根据productId获取商品配置
    /// </summary>
    public Table_Shop_Shop GetShopCfgByProductId(string productId)
    {
        return GetShopCfgList().Find((cfg) =>
        {
#if UNITY_IOS
            return productId == cfg.Product_id_ios;
#else
            return productId == cfg.Product_id;
#endif
        });
    }

    /// <summary>
    /// 获取productId
    /// </summary>
    public string GetProductId(Table_Shop_Shop shopCfg)
    {
#if UNITY_IOS
            return shopCfg.Product_id_ios;
#else
        return shopCfg.Product_id;
#endif
    }

    /// <summary>
    /// 获取某个商品的购买次数
    /// </summary>
    public int GetPurchasedCount(int shopId)
    {
        if (storageShop.PurchaseCount.TryGetValue(shopId, out var _count))
            return _count;
        return 0;
    }

    /// <summary>
    /// 获取平均购买价格（美分）
    /// </summary>
    public int GetAvgCents()
    {
        int avgCents = 0;
        if (storageShop.TotalPayCount > 0)
        {
            avgCents = (storageShop.TotalPayCents / storageShop.TotalPayCount);
        }
        return avgCents;
    }

    /// <summary>
    /// 用户是否支付过
    /// </summary>
    public bool IsPlayerPurchased()
    {
        bool isPurchased = storageShop.LastBuyTime != 0;
        return isPurchased;
    }

    /// <summary>
    /// 获取玩家未付费天数
    /// </summary>
    public int GetNotPayDays()
    {
        long lastBuyTime = storageShop.LastBuyTime;
        var nowTime = (long)TimeUtils.GetServerTimeStamp();
        if (lastBuyTime == 0)
        {
            var installedAt = (long)Game.GetMod<ModStorage>().GetStorage<StorageCommon>().InstalledAt;
            lastBuyTime = installedAt;
        }

        if (nowTime <= lastBuyTime)
            return 0;

        return TimeUtils.GetDayInterval(nowTime, lastBuyTime);
    }

    /// <summary>
    /// 美元转换为美分
    /// </summary>
    private int ConvertToCents(string price)
    {
        var cents = float.Parse(price);
        return (int)(cents * 100);
    }

    /// <summary>
    /// 存档中记录购买的信息
    /// </summary>
    private void RecordShopPurchaseInfo(Table_Shop_Shop shopCfg, int count = 1)
    {
        // 记录每个商品id对应的购买数量
        if (!storageShop.PurchaseCount.ContainsKey(shopCfg.Id))
        {
            storageShop.PurchaseCount.Add(shopCfg.Id, count);
        }
        else
        {
            storageShop.PurchaseCount[shopCfg.Id] += count;
        }

        // 记录每个商品的购买信息
        var storageInfo = new StorageShopPurchaseInfo();
        storageInfo.Id = shopCfg.Id;
        storageInfo.Price = shopCfg.Price;
        storageInfo.ShopType = shopCfg.ShopType;
        storageInfo.PurchasedTimeStamp = (ulong)TimeUtils.GetServerTimeStamp();
        storageShop.ShopPurchaseInfoList.Add(storageInfo);

        // 记录购买信息
        var cents = ConvertToCents(shopCfg.Price);
        storageShop.LastPayCents = cents;
        storageShop.TotalPayCents += cents;
        storageShop.TotalPayCount += 1;
        storageShop.LastBuyTime = TimeUtils.GetServerTimeStamp();
    }

    public void TryHandleUnfulfilledPayments()
    {
        CLog.Info($"尝试补单");
        string productId = string.Empty;
        Product[] products = SDK<IAP>.Instance.GetAllProductInfo();
        if (products == null || products.Length <= 0)
            return;
        for (int i = 0; i < products.Length; i++)
        {
            var id = products[i].definition.id;
            if (SDK<IAP>.Instance.IsProductAlreadyOwned(id))
            {
                productId = id;
                break;
            }
        }
        if (string.IsNullOrEmpty(productId))
        {
            CLog.Info($"无需补单");
            return;
        }

        CLog.Info($"确认补单，productId：{productId}");
        RequestUnfulfilledPaymentsAndTryVerify(productId);
    }

    /// <summary>
    /// 请求补单并验证
    /// </summary>
    public void RequestUnfulfilledPaymentsAndTryVerify(string checkProductId = "")
    {
        CLog.Info("请求补单");
        SDK<INetwork>.Instance.Send(new CListUnfulfilledPayments(),
            (SListUnfulfilledPayments obj) =>
            {
                var payments = obj;
                CLog.Info($"请求补单成功，需要补单的数量：{payments.Payments.Count}");
                if (payments.Payments.Count > 0)
                {
                    foreach (var payment in payments.Payments)
                    {
                        SDK<IAP>.Instance.SetUnfulfilledPaymentId(payment.ProductId, payment.PaymentId);
                    }
                }
                SDK<IAP>.Instance.VerifyUnfulfilledPayment(OnPurchased, checkProductId);
            },
            (arg1, arg2, arg3) => { CLog.Info("补单失败，" + arg1 + "  " + arg2 + "  " + arg3); });
    }
}