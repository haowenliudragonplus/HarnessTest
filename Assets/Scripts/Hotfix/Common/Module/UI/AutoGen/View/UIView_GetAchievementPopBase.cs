/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-8 20:8:43*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIView_GetAchievementPopBase : UIViewBase
{
	protected Button UIBtn_CloseButton;
	protected Image UIImg_AchievementMedalIcon;
	protected TextMeshProUGUI UITxt_AchievementName;
	protected TextMeshProUGUI UITxt_Date;
	protected Button UIBtn_Continue;

    protected override void BindComponent()
    {
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIImg_AchievementMedalIcon = GO.transform.Find("Root/InsideGroup/UIImg_AchievementMedalIcon").GetComponent<Image>();
		UITxt_AchievementName = GO.transform.Find("Root/InsideGroup/UITxt_AchievementName").GetComponent<TextMeshProUGUI>();
		UITxt_Date = GO.transform.Find("Root/InsideGroup/UITxt_Date").GetComponent<TextMeshProUGUI>();
		UIBtn_Continue = GO.transform.Find("Root/UIBtn_Continue").GetComponent<Button>();

    }
}
