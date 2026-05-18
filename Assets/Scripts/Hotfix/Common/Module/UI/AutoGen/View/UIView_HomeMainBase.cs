/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2026-5-13 13:47:22*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIView_HomeMainBase : UIViewBase
{
	protected Image UIImg_HeadIcon;
	protected Image UIImg_Icon1;
	protected Button UIBtn_ButtonAvatar;
	protected Button UIBtn_Set;
	protected UISubView_ResourceBar UISubView_ResourceBar;
	protected RectTransform UINode_Entrance_Right;
	protected RectTransform UINode_Entrance_Left;
	protected RectTransform UINode_Entrance;
	protected RectTransform UINode_LevelContent;
	protected TextMeshProUGUI UITxt_Asmr;
	protected Image UIImg_Asmr;
	protected RectTransform UINode_Asmr;
	protected RectTransform UINode_WinStreak3;
	protected RectTransform UINode_WinStreak2;
	protected RectTransform UINode_WinStreak1;
	protected TextMeshProUGUI UITxt_Play;
	protected TextMeshProUGUI UITxt_Hard;
	protected RectTransform UINode_Hard;
	protected TextMeshProUGUI UITxt_SuperHard;
	protected RectTransform UINode_SuperHard;
	protected RectTransform UINode_Arrow;
	protected RectTransform UINode_Hand;
	protected Button UIBtn_ButtonPlay;
	protected RectTransform UINode_System;
	protected Image UIImg_RedDot;
	protected Button UIBtn_Achievement;
	protected Button UIBtn_Home;
	protected Button UIBtn_Lock;
	protected RectTransform UINode_Bubble;
	protected RectTransform UINode_Lock;
	protected RectTransform UINode_Bottom;
	protected RectTransform UINode_Achievement;

    protected override void BindComponent()
    {
		UIImg_HeadIcon = GO.transform.Find("Root/TopHome/Root/UIBtn_ButtonAvatar/UIImg_HeadIcon").GetComponent<Image>();
		UIImg_Icon1 = GO.transform.Find("Root/TopHome/Root/UIBtn_ButtonAvatar/UIImg_Icon1").GetComponent<Image>();
		UIBtn_ButtonAvatar = GO.transform.Find("Root/TopHome/Root/UIBtn_ButtonAvatar").GetComponent<Button>();
		UIBtn_Set = GO.transform.Find("Root/TopHome/Root/UIBtn_Set").GetComponent<Button>();
		UISubView_ResourceBar =new UISubView_ResourceBar();
		UISubView_ResourceBar.InternalInit(this, "UISubView_ResourceBar");
		UISubView_ResourceBar.InternalCreateWithoutInstantiate(GO.transform.Find("Root/TopHome/Root/UISubView_ResourceBar").gameObject);
		UINode_Entrance_Right = GO.transform.Find("Root/UINode_Entrance/Root/RightView/Viewport/UINode_Entrance_Right").GetComponent<RectTransform>();
		UINode_Entrance_Left = GO.transform.Find("Root/UINode_Entrance/Root/LeftView/Viewport/UINode_Entrance_Left").GetComponent<RectTransform>();
		UINode_Entrance = GO.transform.Find("Root/UINode_Entrance").GetComponent<RectTransform>();
		UINode_LevelContent = GO.transform.Find("Root/LevelGroup/Scroll View/Viewport/UINode_LevelContent").GetComponent<RectTransform>();
		UITxt_Asmr = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_Asmr/UITxt_Asmr").GetComponent<TextMeshProUGUI>();
		UIImg_Asmr = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_Asmr/UIImg_Asmr").GetComponent<Image>();
		UINode_Asmr = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_Asmr").GetComponent<RectTransform>();
		UINode_WinStreak3 = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_WinStreak3").GetComponent<RectTransform>();
		UINode_WinStreak2 = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_WinStreak2").GetComponent<RectTransform>();
		UINode_WinStreak1 = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_WinStreak1").GetComponent<RectTransform>();
		UITxt_Play = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UITxt_Play").GetComponent<TextMeshProUGUI>();
		UITxt_Hard = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_Hard/UITxt_Hard").GetComponent<TextMeshProUGUI>();
		UINode_Hard = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_Hard").GetComponent<RectTransform>();
		UITxt_SuperHard = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_SuperHard/UITxt_SuperHard").GetComponent<TextMeshProUGUI>();
		UINode_SuperHard = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/Root/UINode_SuperHard").GetComponent<RectTransform>();
		UINode_Arrow = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/UINode_Arrow").GetComponent<RectTransform>();
		UINode_Hand = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay/UINode_Hand").GetComponent<RectTransform>();
		UIBtn_ButtonPlay = GO.transform.Find("Root/UINode_System/UIBtn_ButtonPlay").GetComponent<Button>();
		UINode_System = GO.transform.Find("Root/UINode_System").GetComponent<RectTransform>();
		UIImg_RedDot = GO.transform.Find("Root/UINode_Bottom/UIBtn_Achievement/UIImg_RedDot").GetComponent<Image>();
		UIBtn_Achievement = GO.transform.Find("Root/UINode_Bottom/UIBtn_Achievement").GetComponent<Button>();
		UIBtn_Home = GO.transform.Find("Root/UINode_Bottom/UIBtn_Home").GetComponent<Button>();
		UIBtn_Lock = GO.transform.Find("Root/UINode_Bottom/UINode_Lock/UIBtn_Lock").GetComponent<Button>();
		UINode_Bubble = GO.transform.Find("Root/UINode_Bottom/UINode_Lock/UINode_Bubble").GetComponent<RectTransform>();
		UINode_Lock = GO.transform.Find("Root/UINode_Bottom/UINode_Lock").GetComponent<RectTransform>();
		UINode_Bottom = GO.transform.Find("Root/UINode_Bottom").GetComponent<RectTransform>();
		UINode_Achievement = GO.transform.Find("Root/UINode_Achievement").GetComponent<RectTransform>();

    }
}
