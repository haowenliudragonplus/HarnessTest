using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using TMGame;
using UnityEngine.UI;

public class UIView_InGame_Setting : UIView_InGame_SettingBase
{
    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }
    private void RefreshView()
    {
        RefreshView_MusicBtn();
        RefreshView_SoundBtn();
        RefreshView_VibrateBtn();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.onClick.AddListener(OnCloseBtn);
        UIBtn_Music.onClick.AddListener(OnMusicBtn);
        UIBtn_Sound.onClick.AddListener(OnSoundBtn);
        UIBtn_Vibrate.onClick.AddListener(OnVibrateBtn);
        UIBtn_Return.onClick.AddListener(() =>
        {
            Game.GetMod<AdSys>().TryShowInterstitial(eAdInterstitial.QuitLevel);
            var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
            Close();
            BIHelper.SendLevelInfo(mode.Data, BIHelper.ELevelInfoType.Restart);
            mode.RePlayGame();
        });
        UIBtn_Quit.onClick.AddListener(() =>
        {
            Close();
            var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
            BIHelper.SendLevelInfo(mode.Data, BIHelper.ELevelInfoType.Quit);
            mode.ExitGame();
        });
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
}
