/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-9 15:55:37*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_AchievementTipBase : UIWidgetBase
{
	protected Image UIImg_AchievementIcon;
	protected TextMeshProUGUI UITxt_AchievementName;

    protected override void BindComponent()
    {
		UIImg_AchievementIcon = GO.transform.Find("Root/UIImg_AchievementIcon").GetComponent<Image>();
		UITxt_AchievementName = GO.transform.Find("Root/UITxt_AchievementName").GetComponent<TextMeshProUGUI>();

    }
}
