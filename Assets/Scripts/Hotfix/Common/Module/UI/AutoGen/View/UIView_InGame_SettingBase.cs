/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:53:36*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_InGame_SettingBase : UIViewBase
{
	protected Button UIBtn_Return;
	protected Button UIBtn_Quit;
	protected Image UIImg_SoundOn;
	protected Image UIImg_SoundOff;
	protected Button UIBtn_Sound;
	protected Image UIImg_MusicOn;
	protected Image UIImg_MusicOff;
	protected Button UIBtn_Music;
	protected Image UIImg_VibrateOn;
	protected Image UIImg_VibrateOff;
	protected Button UIBtn_Vibrate;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;

    protected override void BindComponent()
    {
		UIBtn_Return = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_Return").GetComponent<Button>();
		UIBtn_Quit = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_Quit").GetComponent<Button>();
		UIImg_SoundOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound/UIImg_SoundOn").GetComponent<Image>();
		UIImg_SoundOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound/UIImg_SoundOff").GetComponent<Image>();
		UIBtn_Sound = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound").GetComponent<Button>();
		UIImg_MusicOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music/UIImg_MusicOn").GetComponent<Image>();
		UIImg_MusicOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music/UIImg_MusicOff").GetComponent<Image>();
		UIBtn_Music = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music").GetComponent<Button>();
		UIImg_VibrateOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate/UIImg_VibrateOn").GetComponent<Image>();
		UIImg_VibrateOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate/UIImg_VibrateOff").GetComponent<Image>();
		UIBtn_Vibrate = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate").GetComponent<Button>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();

    }
}
