/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-12 11:0:47*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;

public class UISubView_TaskInfoBase : UISubViewBase
{
	protected TextMeshProUGUI UITxt_TaskTitle;
	protected Image UIImg_BG;
	protected Image UIImg_BG2;
	protected TextMeshProUGUI UITxt_TaskDesc;
	protected Image UIImg_TaskIcon;
	protected TextMeshProUGUI UITxt_TaskProgress;
	protected TextMeshProUGUI UITxt_Text;
	protected Image UIImg_RewardIcon;
	protected TextMeshProUGUI UITxt_RewardAmount;

    protected override void BindComponent()
    {
		UITxt_TaskTitle = GO.transform.Find("Root/TitleGroup/UITxt_TaskTitle").GetComponent<TextMeshProUGUI>();
		UIImg_BG = GO.transform.Find("Root/HeadGroup/UIImg_BG").GetComponent<Image>();
		UIImg_BG2 = GO.transform.Find("Root/HeadGroup/UIImg_BG2").GetComponent<Image>();
		UITxt_TaskDesc = GO.transform.Find("Root/TaskGroup/UITxt_TaskDesc").GetComponent<TextMeshProUGUI>();
		UIImg_TaskIcon = GO.transform.Find("Root/TaskGroup/IconBG/UIImg_TaskIcon").GetComponent<Image>();
		UITxt_TaskProgress = GO.transform.Find("Root/TaskGroup/UITxt_TaskProgress").GetComponent<TextMeshProUGUI>();
		UITxt_Text = GO.transform.Find("Root/RewardGroup/UITxt_Text").GetComponent<TextMeshProUGUI>();
		UIImg_RewardIcon = GO.transform.Find("Root/RewardGroup/IconBG/UIImg_RewardIcon").GetComponent<Image>();
		UITxt_RewardAmount = GO.transform.Find("Root/RewardGroup/UITxt_RewardAmount").GetComponent<TextMeshProUGUI>();

    }
}
