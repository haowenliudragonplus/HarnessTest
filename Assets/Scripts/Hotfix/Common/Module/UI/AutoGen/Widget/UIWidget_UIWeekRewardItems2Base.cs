/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-16 20:47:33*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIWidget_UIWeekRewardItems2Base : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_SerialNoText;
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_NumberText;
	protected RectTransform UINode_RewardGroup;
	protected Image UIImg_Lock;
	protected Image UIImg_ReceiveIcon;

    protected override void BindComponent()
    {
		UITxt_SerialNoText = GO.transform.Find("UITxt_SerialNoText").GetComponent<TextMeshProUGUI>();
		UIImg_Icon = GO.transform.Find("UINode_RewardGroup/UIImg_Icon").GetComponent<Image>();
		UITxt_NumberText = GO.transform.Find("UINode_RewardGroup/UITxt_NumberText").GetComponent<TextMeshProUGUI>();
		UINode_RewardGroup = GO.transform.Find("UINode_RewardGroup").GetComponent<RectTransform>();
		UIImg_Lock = GO.transform.Find("UIImg_Lock").GetComponent<Image>();
		UIImg_ReceiveIcon = GO.transform.Find("UIImg_ReceiveIcon").GetComponent<Image>();

    }
}
