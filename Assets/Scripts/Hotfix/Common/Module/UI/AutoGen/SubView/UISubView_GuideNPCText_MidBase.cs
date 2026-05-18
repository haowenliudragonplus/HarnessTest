/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-16 16:59:48*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine;

public class UISubView_GuideNPCText_MidBase : UISubViewBase
{
	protected TextMeshProUGUI UITxt_NormalText;
	protected TextMeshProUGUI UITxt_TextWithAvatar;
	protected RectTransform UINode_AvatarIcon;
	protected RectTransform UINode_AvatarGuideText;

    protected override void BindComponent()
    {
		UITxt_NormalText = GO.transform.Find("NormalGuideText/BG/UITxt_NormalText").GetComponent<TextMeshProUGUI>();
		UITxt_TextWithAvatar = GO.transform.Find("UINode_AvatarGuideText/BG/UITxt_TextWithAvatar").GetComponent<TextMeshProUGUI>();
		UINode_AvatarIcon = GO.transform.Find("UINode_AvatarGuideText/UINode_AvatarIcon").GetComponent<RectTransform>();
		UINode_AvatarGuideText = GO.transform.Find("UINode_AvatarGuideText").GetComponent<RectTransform>();

    }
}
