/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-24 23:59:47*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_LoadingBase : UIViewBase
{
	protected Image UIImg_LoginBg;
	protected Image UIImg_Logo;
	protected Slider UISlider_Progress;
	protected TextMeshProUGUI UITxt_Tips;
	protected TextMeshProUGUI UITxt_Progress;
	protected RectTransform UINode_Progress;
	protected RectTransform UINode_Login;

    protected override void BindComponent()
    {
		UIImg_LoginBg = GO.transform.Find("UINode_Login/UIImg_LoginBg").GetComponent<Image>();
		UIImg_Logo = GO.transform.Find("UINode_Login/UIImg_Logo").GetComponent<Image>();
		UISlider_Progress = GO.transform.Find("UINode_Login/UINode_Progress/UISlider_Progress").GetComponent<Slider>();
		UITxt_Tips = GO.transform.Find("UINode_Login/UINode_Progress/UITxt_Tips").GetComponent<TextMeshProUGUI>();
		UITxt_Progress = GO.transform.Find("UINode_Login/UINode_Progress/UITxt_Progress").GetComponent<TextMeshProUGUI>();
		UINode_Progress = GO.transform.Find("UINode_Login/UINode_Progress").GetComponent<RectTransform>();
		UINode_Login = GO.transform.Find("UINode_Login").GetComponent<RectTransform>();

    }
}
