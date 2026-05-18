/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:50:11*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_SettingBase : UIViewBase
{
	protected Button UIBtn_LanguageButton;
	protected Button UIBtn_SupportButton;
	protected Button UIBtn_FileButton;
	protected Button UIBtn_Deletion;
	protected Image UIImg_MusicOn;
	protected Image UIImg_MusicOff;
	protected Button UIBtn_Music;
	protected Image UIImg_SoundOn;
	protected Image UIImg_SoundOff;
	protected Button UIBtn_Sound;
	protected Image UIImg_VibrateOn;
	protected Image UIImg_VibrateOff;
	protected Button UIBtn_Vibrate;
	protected TextMeshProUGUI UITxt_VersionText;
	protected TextMeshProUGUI UITxt_PrivacyText;
	protected Button UIBtn_Privacy;
	protected TextMeshProUGUI UITxt_UserText;
	protected TextMeshProUGUI UITxt_ServiceText;
	protected Button UIBtn_Service;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_CloseButton;

    protected override void BindComponent()
    {
		UIBtn_LanguageButton = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_LanguageButton").GetComponent<Button>();
		UIBtn_SupportButton = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_SupportButton").GetComponent<Button>();
		UIBtn_FileButton = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_FileButton").GetComponent<Button>();
		UIBtn_Deletion = GO.transform.Find("Root/BG_Mask/Group/BG_ReplaceDiff/UIBtn_Deletion").GetComponent<Button>();
		UIImg_MusicOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music/UIImg_MusicOn").GetComponent<Image>();
		UIImg_MusicOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music/UIImg_MusicOff").GetComponent<Image>();
		UIBtn_Music = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Music").GetComponent<Button>();
		UIImg_SoundOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound/UIImg_SoundOn").GetComponent<Image>();
		UIImg_SoundOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound/UIImg_SoundOff").GetComponent<Image>();
		UIBtn_Sound = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Sound").GetComponent<Button>();
		UIImg_VibrateOn = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate/UIImg_VibrateOn").GetComponent<Image>();
		UIImg_VibrateOff = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate/UIImg_VibrateOff").GetComponent<Image>();
		UIBtn_Vibrate = GO.transform.Find("Root/BG_Mask/Group/ToggleGroup/UIBtn_Vibrate").GetComponent<Button>();
		UITxt_VersionText = GO.transform.Find("Root/BG_Mask/Group/UITxt_VersionText").GetComponent<TextMeshProUGUI>();
		UITxt_PrivacyText = GO.transform.Find("Root/BG_Mask/Group/UIBtn_Privacy/UITxt_PrivacyText").GetComponent<TextMeshProUGUI>();
		UIBtn_Privacy = GO.transform.Find("Root/BG_Mask/Group/UIBtn_Privacy").GetComponent<Button>();
		UITxt_UserText = GO.transform.Find("Root/BG_Mask/Group/UITxt_UserText").GetComponent<TextMeshProUGUI>();
		UITxt_ServiceText = GO.transform.Find("Root/BG_Mask/Group/UIBtn_Service/UITxt_ServiceText").GetComponent<TextMeshProUGUI>();
		UIBtn_Service = GO.transform.Find("Root/BG_Mask/Group/UIBtn_Service").GetComponent<Button>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();

    }
}
