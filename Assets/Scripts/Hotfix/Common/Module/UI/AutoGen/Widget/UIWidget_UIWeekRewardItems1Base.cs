/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-20 11:34:20*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIWidget_UIWeekRewardItems1Base : UIWidgetBase
{
	protected RectTransform UINode_YellowBG;
	protected RectTransform UINode_PurpleBG;
	protected TextMeshProUGUI UITxt_SerialNoText;
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_TagText;
	protected Image UIImg_BG;
	protected TextMeshProUGUI UITxt_NumberText;
	protected TextMeshProUGUI UITxt_DoubleText;
	protected Image UIImg_Double;
	protected Image UIImg_Countness1;
	protected Image UIImg_Countness;
	protected RectTransform UINode_RewardGroup;
	protected Image UIImg_Lock;
	protected Image UIImg_ReceiveIcon;

    protected override void BindComponent()
    {
		UINode_YellowBG = GO.transform.Find("UINode_YellowBG").GetComponent<RectTransform>();
		UINode_PurpleBG = GO.transform.Find("UINode_PurpleBG").GetComponent<RectTransform>();
		UITxt_SerialNoText = GO.transform.Find("UITxt_SerialNoText").GetComponent<TextMeshProUGUI>();
		UIImg_Icon = GO.transform.Find("UINode_RewardGroup/UIImg_Icon").GetComponent<Image>();
		UITxt_TagText = GO.transform.Find("UINode_RewardGroup/UIImg_BG/UITxt_TagText").GetComponent<TextMeshProUGUI>();
		UIImg_BG = GO.transform.Find("UINode_RewardGroup/UIImg_BG").GetComponent<Image>();
		UITxt_NumberText = GO.transform.Find("UINode_RewardGroup/UITxt_NumberText").GetComponent<TextMeshProUGUI>();
		UITxt_DoubleText = GO.transform.Find("UINode_RewardGroup/UIImg_Double/UITxt_DoubleText").GetComponent<TextMeshProUGUI>();
		UIImg_Double = GO.transform.Find("UINode_RewardGroup/UIImg_Double").GetComponent<Image>();
		UIImg_Countness1 = GO.transform.Find("UINode_RewardGroup/UIImg_Countness/UIImg_Countness1").GetComponent<Image>();
		UIImg_Countness = GO.transform.Find("UINode_RewardGroup/UIImg_Countness").GetComponent<Image>();
		UINode_RewardGroup = GO.transform.Find("UINode_RewardGroup").GetComponent<RectTransform>();
		UIImg_Lock = GO.transform.Find("UIImg_Lock").GetComponent<Image>();
		UIImg_ReceiveIcon = GO.transform.Find("UIImg_ReceiveIcon").GetComponent<Image>();

    }
}
