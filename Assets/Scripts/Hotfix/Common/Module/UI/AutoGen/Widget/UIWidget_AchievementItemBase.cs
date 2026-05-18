/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-8 16:30:4*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_AchievementItemBase : UIWidgetBase
{
	protected Image UIImg_AchievementIcon;
	protected TextMeshProUGUI UITxt_AchievementName;
	protected TextMeshProUGUI UITxt_Date;

    protected override void BindComponent()
    {
		UIImg_AchievementIcon = GO.transform.Find("UIImg_AchievementIcon").GetComponent<Image>();
		UITxt_AchievementName = GO.transform.Find("UITxt_AchievementName").GetComponent<TextMeshProUGUI>();
		UITxt_Date = GO.transform.Find("UITxt_Date").GetComponent<TextMeshProUGUI>();

    }
}
