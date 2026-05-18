/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-8-16 17:49:17*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_GMOptionBase : UIWidgetBase
{
    protected Text UIOldTxt_Name;
    protected Image UIImg_Selected;
    protected Button UIBtn_Entrance;

    protected override void BindComponent()
    {
        UIOldTxt_Name = GO.transform.Find("UIBtn_Entrance/UIOldTxt_Name").GetComponent<Text>();
        UIImg_Selected = GO.transform.Find("UIBtn_Entrance/UIImg_Selected").GetComponent<Image>();
        UIBtn_Entrance = GO.transform.Find("UIBtn_Entrance").GetComponent<Button>();
    }
}