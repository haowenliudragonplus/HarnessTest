/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-1-7 10:58:28*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIWidget_LevelItemBase : UIWidgetBase
{
	protected Image UIImg_Progress;
	protected RectTransform UINode_Progress;
	protected Image UIImg_LockBg;
	protected Image UIImg_NormalBg;
	protected Image UIImg_HardBg;
	protected Image UIImg_SuperHardBg;
	protected RectTransform UINode_LevelBgGroup;
	protected RectTransform UINode_Finished;

    protected override void BindComponent()
    {
		UIImg_Progress = GO.transform.Find("UINode_Progress/UIImg_Progress").GetComponent<Image>();
		UINode_Progress = GO.transform.Find("UINode_Progress").GetComponent<RectTransform>();
		UIImg_LockBg = GO.transform.Find("UINode_LevelBgGroup/UIImg_LockBg").GetComponent<Image>();
		UIImg_NormalBg = GO.transform.Find("UINode_LevelBgGroup/UIImg_NormalBg").GetComponent<Image>();
		UIImg_HardBg = GO.transform.Find("UINode_LevelBgGroup/UIImg_HardBg").GetComponent<Image>();
		UIImg_SuperHardBg = GO.transform.Find("UINode_LevelBgGroup/UIImg_SuperHardBg").GetComponent<Image>();
		UINode_LevelBgGroup = GO.transform.Find("UINode_LevelBgGroup").GetComponent<RectTransform>();
		UINode_Finished = GO.transform.Find("UINode_Finished").GetComponent<RectTransform>();

    }
}
