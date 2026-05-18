/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-19 19:8:30*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIView_ASMR_LevelBase : UIViewBase
{
	protected Image UIImg_BGBlack;
	protected Scrollbar UIBar_Scrollbar;
	protected Button UIBtn_CloseButton;
	protected TextMeshProUGUI UITxt_Title;
	protected RectTransform UINode_Content;
	protected Image UIImg_BtDebug;
	protected Button UIBtn_VibratteOn;
	protected Button UIBtn_VibratteOff;

    protected override void BindComponent()
    {
		UIImg_BGBlack = GO.transform.Find("Root/UIImg_BGBlack").GetComponent<Image>();
		UIBar_Scrollbar = GO.transform.Find("Root/UIBar_Scrollbar").GetComponent<Scrollbar>();
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UITxt_Title = GO.transform.Find("Root/ScheduleBG/UITxt_Title").GetComponent<TextMeshProUGUI>();
		UINode_Content = GO.transform.Find("Root/Scroll View/Viewport/UINode_Content").GetComponent<RectTransform>();
		UIImg_BtDebug = GO.transform.Find("Root/UIImg_BtDebug").GetComponent<Image>();
		UIBtn_VibratteOn = GO.transform.Find("Root/UIBtn_VibratteOn").GetComponent<Button>();
		UIBtn_VibratteOff = GO.transform.Find("Root/UIBtn_VibratteOff").GetComponent<Button>();

    }
}
