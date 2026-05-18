/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-12-18 17:50:9*****/
/*****************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIView_CommonRewardBase : UIViewBase
{
	protected RectTransform UINode_Reward;
	protected RectTransform UINode_RewardGroup;
	protected Button UIBtn_ContinueButton;

    protected override void BindComponent()
    {
		UINode_Reward = GO.transform.Find("Root/UINode_RewardGroup/UINode_Reward").GetComponent<RectTransform>();
		UINode_RewardGroup = GO.transform.Find("Root/UINode_RewardGroup").GetComponent<RectTransform>();
		UIBtn_ContinueButton = GO.transform.Find("Root/UIBtn_ContinueButton").GetComponent<Button>();

    }
}
