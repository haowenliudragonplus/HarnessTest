/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-17 21:17:57*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIPopupDownLoadBase : UIViewBase
{
	protected Image UIImg_BG;
	protected Image UIImg_Icon;
	protected Button UIBtn_DownLoad;
	protected TextMeshProUGUI UITxt_ProgressNormalLabel;
	protected Slider UISlider_DownLoadSlider;
	protected RectTransform UINode_DownLoadGroup;
	protected TextMeshProUGUI UITxt_TitleText;
	protected RectTransform UINode_NameGroup;
	protected Button UIBtn_Close;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("UIImg_BG").GetComponent<Image>();
		UIImg_Icon = GO.transform.Find("UIImg_Icon").GetComponent<Image>();
		UIBtn_DownLoad = GO.transform.Find("UINode_DownLoadGroup/UIBtn_DownLoad").GetComponent<Button>();
		UITxt_ProgressNormalLabel = GO.transform.Find("UINode_DownLoadGroup/UISlider_DownLoadSlider/UITxt_ProgressNormalLabel").GetComponent<TextMeshProUGUI>();
		UISlider_DownLoadSlider = GO.transform.Find("UINode_DownLoadGroup/UISlider_DownLoadSlider").GetComponent<Slider>();
		UINode_DownLoadGroup = GO.transform.Find("UINode_DownLoadGroup").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("UINode_NameGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UINode_NameGroup = GO.transform.Find("UINode_NameGroup").GetComponent<RectTransform>();
		UIBtn_Close = GO.transform.Find("UIBtn_Close").GetComponent<Button>();

    }
}
