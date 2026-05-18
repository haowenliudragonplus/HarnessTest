/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-4-9 16:6:0*****/
/*****************************/

using Framework;
using TMPro;
using UnityEngine.UI;

public class UIWidget_ScoreTipBase : UIWidgetBase
{
	protected TextMeshProUGUI UITxt_Score;
	protected Image UIImg_FlowerLight;

    protected override void BindComponent()
    {
		UITxt_Score = GO.transform.Find("Root/UIImg_FlowerLight/UITxt_Score").GetComponent<TextMeshProUGUI>();
		UIImg_FlowerLight = GO.transform.Find("Root/UIImg_FlowerLight").GetComponent<Image>();

    }
}
