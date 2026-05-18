/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-9 17:47:42*****/
/*****************************/

using Framework;
using TMPro;

public class UIChooseProgressBase : UIViewBase
{
	protected TextMeshProUGUI UITxt_LevelText1;
	protected TextMeshProUGUI UITxt_LevelText2;

    protected override void BindComponent()
    {
		UITxt_LevelText1 = GO.transform.Find("Root/Progress1/UITxt_LevelText1").GetComponent<TextMeshProUGUI>();
		UITxt_LevelText2 = GO.transform.Find("Root/Progress2/UITxt_LevelText2").GetComponent<TextMeshProUGUI>();

    }
}
