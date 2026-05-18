/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2025-5-14 14:3:24*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIView_SelectThemeBase : UIViewBase
{
	protected Image UIImg_BG;
	protected Image UIImg_InsideBG;
	protected Image UIImg_TitleBG;
	protected RectTransform UINode_BGGroup;
	protected RectTransform UINode_Empty;
	protected TextMeshProUGUI UITxt_TitleText;
	protected Button UIBtn_Close;
	protected TextMeshProUGUI UITxt_Tiles;
	protected TextMeshProUGUI UITxt_TilesSelected;
	protected Image UIImg_TilesSelected;
	protected Button UIBtn_Tiles;
	protected TextMeshProUGUI UITxt_Bg;
	protected TextMeshProUGUI UITxt_BgSelected;
	protected Image UIImg_BgSelected;
	protected Button UIBtn_Background;
	protected RectTransform UINode_TilesContent;
	protected RectTransform UINode_TilesList;
	protected RectTransform UINode_BgContent;
	protected RectTransform UINode_BackgroundList;
	protected TextMeshProUGUI UITxt_Continue;
	protected Button UIBtn_ContinueButton;

    protected override void BindComponent()
    {
		UIImg_BG = GO.transform.Find("Root/UINode_BGGroup/UIImg_BG").GetComponent<Image>();
		UIImg_InsideBG = GO.transform.Find("Root/UINode_BGGroup/UIImg_InsideBG").GetComponent<Image>();
		UIImg_TitleBG = GO.transform.Find("Root/UINode_BGGroup/UIImg_TitleBG").GetComponent<Image>();
		UINode_BGGroup = GO.transform.Find("Root/UINode_BGGroup").GetComponent<RectTransform>();
		UINode_Empty = GO.transform.Find("Root/BG_ReplaceDiff/UINode_Empty").GetComponent<RectTransform>();
		UITxt_TitleText = GO.transform.Find("Root/TitleGroup/UITxt_TitleText").GetComponent<TextMeshProUGUI>();
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UITxt_Tiles = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Tiles/UITxt_Tiles").GetComponent<TextMeshProUGUI>();
		UITxt_TilesSelected = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Tiles/UIImg_TilesSelected/UITxt_TilesSelected").GetComponent<TextMeshProUGUI>();
		UIImg_TilesSelected = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Tiles/UIImg_TilesSelected").GetComponent<Image>();
		UIBtn_Tiles = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Tiles").GetComponent<Button>();
		UITxt_Bg = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Background/UITxt_Bg").GetComponent<TextMeshProUGUI>();
		UITxt_BgSelected = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Background/UIImg_BgSelected/UITxt_BgSelected").GetComponent<TextMeshProUGUI>();
		UIImg_BgSelected = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Background/UIImg_BgSelected").GetComponent<Image>();
		UIBtn_Background = GO.transform.Find("Root/InsideGroup/TabGroup/UIBtn_Background").GetComponent<Button>();
		UINode_TilesContent = GO.transform.Find("Root/InsideGroup/UINode_TilesList/Viewport/UINode_TilesContent").GetComponent<RectTransform>();
		UINode_TilesList = GO.transform.Find("Root/InsideGroup/UINode_TilesList").GetComponent<RectTransform>();
		UINode_BgContent = GO.transform.Find("Root/InsideGroup/UINode_BackgroundList/Viewport/UINode_BgContent").GetComponent<RectTransform>();
		UINode_BackgroundList = GO.transform.Find("Root/InsideGroup/UINode_BackgroundList").GetComponent<RectTransform>();
		UITxt_Continue = GO.transform.Find("Root/InsideGroup/UIBtn_ContinueButton/UITxt_Continue").GetComponent<TextMeshProUGUI>();
		UIBtn_ContinueButton = GO.transform.Find("Root/InsideGroup/UIBtn_ContinueButton").GetComponent<Button>();

    }
}
