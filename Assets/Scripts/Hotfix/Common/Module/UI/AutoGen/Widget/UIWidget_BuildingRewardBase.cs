/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-19 21:17:20*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_BuildingRewardBase : UIWidgetBase
{
	protected Image UIImg_BgCommon;
	protected Image UIImg_BgSuper;
	protected Image UIImg_UnReach;
	protected Image UIImg_Cur;
	protected Image UIImg_Reach;
	protected Image UIImg_Lock;
	protected TextMeshProUGUI UITxt_Level;
	protected Image UIImg_Reward;
	protected Image UIImg_Claimed;
	protected TextMeshProUGUI UITxt_Name;
	protected Button UIBtn_Claim;

    protected override void BindComponent()
    {
		UIImg_BgCommon = GO.transform.Find("Root/UIImg_BgCommon").GetComponent<Image>();
		UIImg_BgSuper = GO.transform.Find("Root/UIImg_BgSuper").GetComponent<Image>();
		UIImg_UnReach = GO.transform.Find("Root/UIImg_UnReach").GetComponent<Image>();
		UIImg_Cur = GO.transform.Find("Root/UIImg_Cur").GetComponent<Image>();
		UIImg_Reach = GO.transform.Find("Root/UIImg_Reach").GetComponent<Image>();
		UIImg_Lock = GO.transform.Find("Root/UIImg_Lock").GetComponent<Image>();
		UITxt_Level = GO.transform.Find("Root/UITxt_Level").GetComponent<TextMeshProUGUI>();
		UIImg_Reward = GO.transform.Find("Root/RewardGroup/UIImg_Reward").GetComponent<Image>();
		UIImg_Claimed = GO.transform.Find("Root/RewardGroup/UIImg_Claimed").GetComponent<Image>();
		UITxt_Name = GO.transform.Find("Root/RewardGroup/UITxt_Name").GetComponent<TextMeshProUGUI>();
		UIBtn_Claim = GO.transform.Find("Root/RewardGroup/UIBtn_Claim").GetComponent<Button>();

    }
}
