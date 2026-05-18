using DragonPlus;
using DragonPlus.Account;
using DragonPlus.Core;
using DragonPlus.InAppPurchasing;
using DragonPlus.Native.Bridge;
using DragonPlus.Save;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;
using YooAsset;

/// <summary>
/// 设置界面
/// </summary>
public class UIView_Setting : UIView_SettingBase
{
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        playSound = true;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.onClick.AddListener(OnCloseBtn);
        UIBtn_Music.onClick.AddListener(OnMusicBtn);
        UIBtn_Sound.onClick.AddListener(OnSoundBtn);
        UIBtn_Vibrate.onClick.AddListener(OnVibrateBtn);
        //UIBtn_Notice.onClick.AddListener(OnNoticeBtn);
        UIBtn_LanguageButton.onClick.AddListener(OnLanguageBtn);
        UIBtn_SupportButton.onClick.AddListener(OnContactUsBtn);
        //UIBtn_RestoreBuyButton.onClick.AddListener(OnRestoreBtn);
        UIBtn_FileButton.onClick.AddListener(OnSaveProgressBtn);
        UIBtn_Privacy.onClick.AddListener(OnPrivacyBtn);
        UIBtn_Service.onClick.AddListener(OnServerBtn);
        UIBtn_Deletion.onClick.AddListener(OnDeleteAccountBtn);
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        RegisterEvent<EvtOnApplicationPause>(OnApplicationPause);
        RegisterEvent<EvtLanguageChange>(OnLanguageChange);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        string playerIdStr = SDKUtil.Misc.PlayerIdToString(SDK<IStorage>.Instance.Get<StorageCommon>().PlayerId);
        UITxt_UserText.SetText(CoreUtils.GetLocalization("UI_setting_user_id") + playerIdStr);
        UITxt_VersionText.SetText($"v{GameConfig.Version}");
        RefreshView_MusicBtn();
        RefreshView_SoundBtn();
        RefreshView_VibrateBtn();
        RefreshView_NoticeBtn();
#if UNITY_IOS
        UIBtn_FileButton.gameObject.SetActive(false);
#else
        UIBtn_FileButton.gameObject.SetActive(false);
#endif
        //UIBtn_Notice.gameObject.SetActive(false);
    }

    private void RefreshView_MusicBtn()
    {
        var isOff = !Game.GetMod<ModAudio>().EnableBGM;
        UIImg_MusicOff.gameObject.SetActive(isOff);
        UIImg_MusicOn.gameObject.SetActive(!isOff);
    }

    private void RefreshView_SoundBtn()
    {
        var isOff = !Game.GetMod<ModAudio>().EnableSound;
        UIImg_SoundOff.gameObject.SetActive(isOff);
        UIImg_SoundOn.gameObject.SetActive(!isOff);
    }

    private void RefreshView_VibrateBtn()
    {
        var isOff = Game.GetMod<ModAudio>().VibrateClose;
        UIImg_VibrateOff.gameObject.SetActive(isOff);
        UIImg_VibrateOn.gameObject.SetActive(!isOff);
    }

    private void RefreshView_NoticeBtn()
    {
        // var isOn = Game.GetMod<NotificationSys>().IsNotificationOn();
        // UIImg_NoticeOn.gameObject.SetActive(isOn);
        // UIImg_NoticeOff.gameObject.SetActive(!isOn);
    }

    private void OnLanguageBtn()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_SetLanguage);
    }

    private void OnCloseBtn()
    {
        Close();
    }

    private void OnMusicBtn()
    {
        Game.GetMod<ModAudio>().EnableBGM = !Game.GetMod<ModAudio>().EnableBGM;
        RefreshView_MusicBtn();
    }

    private void OnDeleteAccountBtn()
    {
        if (SDK<IStorage>.Instance.Get<StorageClientCommon>().UserData.IsSubmitDeleteAccount)
        {
            ShowSubmitDeleteAccountView();
        }
        else
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                title = CoreUtils.GetLocalization("&key.UI_login_button_delete_account"),
                content = CoreUtils.GetLocalization("&key.UI_delete_txt1"),
                showCloseBtn = false,
                onRightBtn = () =>
                {
                    SDK<IStorage>.Instance.Get<StorageClientCommon>().UserData.IsSubmitDeleteAccount = true;
                    ShowSubmitDeleteAccountView();
                },
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
        }
    }

    private void ShowSubmitDeleteAccountView()
    {
        UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
        {
            title = CoreUtils.GetLocalization("&key.UI_login_button_delete_account"),
            content = CoreUtils.GetLocalization("&key.UI_login_tip_delete_account_7days"),
            showMidBtn = true,
            showCloseBtn = false,
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
    }

    private void OnSoundBtn()
    {
        Game.GetMod<ModAudio>().EnableSound = !Game.GetMod<ModAudio>().EnableSound;
        RefreshView_SoundBtn();
    }

    private void OnVibrateBtn()
    {
        Game.GetMod<ModAudio>().VibrateClose = !Game.GetMod<ModAudio>().VibrateClose;
        RefreshView_VibrateBtn();
    }

    private void OnNoticeBtn()
    {
        SDK<INative>.Instance.OpenNotificationSetting();
    }

    private void OnContactUsBtn()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_ContactUs);
    }

    private void OnSaveProgressBtn()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_SaveYourProgress);
    }

    private void OnPrivacyBtn()
    {
        Main.GameUtils.OpenURL(ConfigurationController.Instance.PrivacyPolicyURL);
    }

    private void OnServerBtn()
    {
        Main.GameUtils.OpenURL(ConfigurationController.Instance.TermsOfServiceURL);
    }

    private void OnRestoreBtn()
    {
        if (!SDK<IAP>.Instance.IsInitialized())
        {
            UIView_Notice.OpenData openData1 = new UIView_Notice.OpenData()
            {
                showMidBtn = true,
#if UNITY_ANDROID
                content = CoreUtils.GetLocalization("&key.UI_cannot_connect_to_google_play")
#elif UNITY_IOS
                content = CoreUtils.GetLocalization("&key.UI_cannot_connect_to_itunes_store")
#else
                content = CoreUtils.GetLocalization("&key.UI_purchase_failed")
#endif
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData1);
            return;
        }
        //
        if (Game.GetMod<AdSys>().IsRemoveAd())
        {
            UIView_Notice.OpenData openData2 = new UIView_Notice.OpenData()
            {
                showMidBtn = true,
                content = CoreUtils.GetLocalization("UI_common_no_ad_buy"),
                title = CoreUtils.GetLocalization("&key.UI_common_box_tittle"),
                rightBtnTitle = CoreUtils.GetLocalization("&key.UI_button_continue")
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData2);
            return;
        }
        //             
        Game.GetMod<ModUI>().ShowWaiting();
        SDK<IAP>.Instance.RestorePurchases(productIds =>
        {
            Game.GetMod<ModUI>().CloseWaiting();
            Debug.Log($"RestorePurchases count {productIds.Count}");
            foreach (var p in productIds) Debug.Log($"RestorePurchases product {p}");

            if (productIds.Count == 0)
            {
                UIView_Notice.OpenData openData3 = new UIView_Notice.OpenData()
                {
                    showMidBtn = true,
                    content = CoreUtils.GetLocalization("&key.UI_setting_common_3"),
                    midBtnTitle = CoreUtils.GetLocalization("&key.UI_button_continue")
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData3);
                return;
            }

            foreach (var productId in productIds)
            {
                var shopCfg = Game.GetMod<ModIAP>().GetShopCfgByProductId(productId);
                if ((EShopType)shopCfg.ShopType == EShopType.RemoveAd)
                {
                    //Game.GetMod<AdSys>().SetRemoveAd();

                    UIView_Notice.OpenData openData4 = new UIView_Notice.OpenData()
                    {
                        showMidBtn = true,
                        content = CoreUtils.GetLocalization("&key.UI_setting_common_2"),
                        midBtnTitle = CoreUtils.GetLocalization("&key.UI_button_continue")
                    };
                    Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData4);
                    return;
                }
            }

            UIView_Notice.OpenData openData5 = new UIView_Notice.OpenData()
            {
                showMidBtn = true,
                content = CoreUtils.GetLocalization("&key.UI_setting_common_3"),
                midBtnTitle = CoreUtils.GetLocalization("&key.UI_button_continue")
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData5);
        });
    }

    public void OnApplicationPause(EvtOnApplicationPause evt)
    {
        if (!evt.pause)
        {
            //RefreshView_NoticeBtn();
        }
    }

    private void OnLanguageChange(EvtLanguageChange evt)
    {
        Close();
    }
}