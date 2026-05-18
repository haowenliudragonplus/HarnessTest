/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-5-22 18:47:31*****/
/*****************************/

using Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIWidget_DayCellBase : UIWidgetBase
{
	protected RectTransform UINode_Empty;
	protected TextMeshProUGUI UITxt_Day_Future;
	protected TextMeshProUGUI UITxt_Day;
	protected Image UIImg_Finished;
	protected Image UIImg_SelectedFrame;
	protected Button UIBtn_DayItem_Cuurent;
	protected RectTransform UINode_DayCell;

    protected override void BindComponent()
    {
		UINode_Empty = GO.transform.Find("UINode_Empty").GetComponent<RectTransform>();
		UITxt_Day_Future = GO.transform.Find("UINode_DayCell/UITxt_Day_Future").GetComponent<TextMeshProUGUI>();
		UITxt_Day = GO.transform.Find("UINode_DayCell/UIBtn_DayItem_Cuurent/UITxt_Day").GetComponent<TextMeshProUGUI>();
		UIImg_Finished = GO.transform.Find("UINode_DayCell/UIBtn_DayItem_Cuurent/UIImg_Finished").GetComponent<Image>();
		UIImg_SelectedFrame = GO.transform.Find("UINode_DayCell/UIBtn_DayItem_Cuurent/UIImg_SelectedFrame").GetComponent<Image>();
		UIBtn_DayItem_Cuurent = GO.transform.Find("UINode_DayCell/UIBtn_DayItem_Cuurent").GetComponent<Button>();
		UINode_DayCell = GO.transform.Find("UINode_DayCell").GetComponent<RectTransform>();

    }
}
