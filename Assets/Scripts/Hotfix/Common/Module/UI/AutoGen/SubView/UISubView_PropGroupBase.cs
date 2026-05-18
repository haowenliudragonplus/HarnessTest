/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-11 11:24:37*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UISubView_PropGroupBase : UISubViewBase
{
	protected Button UIBtn_Adsorb;
	protected Button UIBtn_Broom;
	protected Button UIBtn_Fan;
	protected Button UIBtn_Frozen;
	protected Button UIBtn_Halt;

    protected override void BindComponent()
    {
		UIBtn_Adsorb = GO.transform.Find("Prop/Viewport/Content/UIBtn_Adsorb").GetComponent<Button>();
		UIBtn_Broom = GO.transform.Find("Prop/Viewport/Content/UIBtn_Broom").GetComponent<Button>();
		UIBtn_Fan = GO.transform.Find("Prop/Viewport/Content/UIBtn_Fan").GetComponent<Button>();
		UIBtn_Frozen = GO.transform.Find("Prop/Viewport/Content/UIBtn_Frozen").GetComponent<Button>();
		UIBtn_Halt = GO.transform.Find("UIBtn_Halt").GetComponent<Button>();

    }
}
