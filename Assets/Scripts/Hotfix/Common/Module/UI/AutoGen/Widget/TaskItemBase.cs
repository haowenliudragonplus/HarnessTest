/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-12-12 16:7:38*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using TMPro;

public class TaskItemBase : UIWidgetBase
{
	protected Image UIImg_Icon;
	protected TextMeshProUGUI UITxt_TaskName;
	protected Image UIImg_FinishIcon;
	protected Image UIImg_StarIcon;
	protected TextMeshProUGUI UITxt_NumText;
	protected Button UIBtn_ClaimButton;

    protected override void BindComponent()
    {
		UIImg_Icon = GO.transform.Find("TaskIcon/UIImg_Icon").GetComponent<Image>();
		UITxt_TaskName = GO.transform.Find("UITxt_TaskName").GetComponent<TextMeshProUGUI>();
		UIImg_FinishIcon = GO.transform.Find("UIImg_FinishIcon").GetComponent<Image>();
		UIImg_StarIcon = GO.transform.Find("UIBtn_ClaimButton/UIImg_StarIcon").GetComponent<Image>();
		UITxt_NumText = GO.transform.Find("UIBtn_ClaimButton/UITxt_NumText").GetComponent<TextMeshProUGUI>();
		UIBtn_ClaimButton = GO.transform.Find("UIBtn_ClaimButton").GetComponent<Button>();

    }
}
