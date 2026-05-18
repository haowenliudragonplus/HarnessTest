/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-1-16 12:8:40*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_ShopPurchaseBase : UIViewBase
{
	protected Button UIBtn_CloseButton;
	protected Button UIBtn_PlayButton;

    protected override void BindComponent()
    {
		UIBtn_CloseButton = GO.transform.Find("Root/UIBtn_CloseButton").GetComponent<Button>();
		UIBtn_PlayButton = GO.transform.Find("Root/UIBtn_PlayButton").GetComponent<Button>();

    }
}
