using DragonPlus.Ad;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine.UI;

/// <summary>
/// 购买体力界面
/// </summary>
public class UIView_BuyEnergy : UIView_BuyEnergyBase
{
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Close.onClick.AddListener(OnCloseBtn);
        UIBtn_Buy.onClick.AddListener(OnBuyBtn);
        UIBtn_Rv.onClick.AddListener(OnAdButtonClicked);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        UIBtn_Close.onClick.RemoveListener(OnCloseBtn);
        UIBtn_Buy.onClick.RemoveListener(OnBuyBtn);
        UIBtn_Rv.onClick.RemoveListener(OnAdButtonClicked);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        InitAdBtn();
        RefreshView();
    }

    private void InitAdBtn()
    {
        // bool canShow = Game.GetMod<AdSys>().ShouldShowRV(eAdReward.BuyEnergy, false);
        // UIBtn_Rv.gameObject.gameObject.SetActive(canShow);
        // if (canShow)
        // {
        //     BIHelper.SendGameEvent_ShowRvBtn(eAdReward.BuyEnergy);
        // }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        //var newText = SDKUtil.TimeDate.GetTimeString("%mm:%ss", (int)(energySys.LeftAutoAddEnergyTime() * 0.001));
        //UITxt_Time.SetText(newText);
    }

    private void RefreshView()
    {
        //UITxt_Count.SetText(energySys.GetEnergy().ToString());
        //UITxt_NeedCount.SetText(Game.GetMod<ConfigMgr>().GetConstConfig<Table_Common_Global, int>("EnergyGemPrice").ToString());
    }

    private void OnBuyBtn()
    {
        Game.GetMod<ModBag>().ConsumeItem(EItemType.Coin, 1, new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.None),
            onSuccess: () =>
            {
                Game.GetMod<ModBag>().AddItem(EItemType.Energy, 1, new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.None));
                Close();
            }, onFailed: () =>
            {
                UIView_UIShopMain.OpenData openData = new UIView_UIShopMain.OpenData();
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_UIShopMain, openData);
            });
    }

    private void OnCloseBtn()
    {
        Close();
    }

    private void OnAdButtonClicked()
    {
        // if (energySys.IsEnergyFull()) return;
        // var adSys = Game.GetMod<AdSys>();
        // BIHelper.SendGameEvent_ClickRvBtn(eAdReward.BuyEnergy);
        // adSys.TryShowRewardedVideo(eAdReward.BuyEnergy, (result, str) =>
        // {
        //     if (result == AdPlayResult.Success)
        //     {
        //         energySys.AddEnergy(1, new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.Ads)
        //         {
        //             data1 = ((int)eAdReward.BuyEnergy).ToString(),
        //         });
        //         Close();
        //     }
        // });
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}