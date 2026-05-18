/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-6 17:0:8*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class UIWidget_ResourceBarItemBase : UIWidgetBase
{
	protected Image UIImg_Bg;
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_IconNum;
	protected Image UIImg_TimeLimit;
	protected TextMeshProUGUI UITxt_Num;
	protected Button UIBtn_Add;

    protected override void BindComponent()
    {
		UIImg_Bg = GO.transform.Find("UIImg_Bg").GetComponent<Image>();
		UIImg_Icon = GO.transform.Find("UIImg_Icon").GetComponent<Image>();
		UITxt_IconNum = GO.transform.Find("UITxt_IconNum").GetComponent<TextMeshProUGUI>();
		UIImg_TimeLimit = GO.transform.Find("UIImg_TimeLimit").GetComponent<Image>();
		UITxt_Num = GO.transform.Find("UITxt_Num").GetComponent<TextMeshProUGUI>();
		UIBtn_Add = GO.transform.Find("UIBtn_Add").GetComponent<Button>();

    }
}
