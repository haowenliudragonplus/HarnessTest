/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-5 15:9:58*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class TaskRewardBase : UIWidgetBase
{
	protected Image UIImg_Image;
	protected TextMeshProUGUI UITxt_NumText;

    protected override void BindComponent()
    {
		UIImg_Image = GO.transform.Find("UIImg_Image").GetComponent<Image>();
		UITxt_NumText = GO.transform.Find("UITxt_NumText").GetComponent<TextMeshProUGUI>();

    }
}
