/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-26 13:46:12*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class CommonGetRewardBase : UIViewBase
{
	protected Image UIImg_BoxOpen;
	protected Image UIImg_BoxBottomIcon;
	protected Image UIImg_BoxTopIcon;
	protected RectTransform UINode_BoxGroup;

    protected override void BindComponent()
    {
		UIImg_BoxOpen = GO.transform.Find("UINode_BoxGroup/UIImg_BoxOpen").GetComponent<Image>();
		UIImg_BoxBottomIcon = GO.transform.Find("UINode_BoxGroup/UIImg_BoxBottomIcon").GetComponent<Image>();
		UIImg_BoxTopIcon = GO.transform.Find("UINode_BoxGroup/UIImg_BoxTopIcon").GetComponent<Image>();
		UINode_BoxGroup = GO.transform.Find("UINode_BoxGroup").GetComponent<RectTransform>();

    }
}
