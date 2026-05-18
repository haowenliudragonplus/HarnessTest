using System;
using DragonPlus;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine.UI;

public class UIWidget_SetLanguageBtn : UIWidget_SetLanguageBtnBase
{
    private Text UITxt_LanguageName;

    private SetLanguageBtnData data;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        data = viewData as SetLanguageBtnData;
    }

    protected override void BindComponent()
    {
        base.BindComponent();
        UITxt_LanguageName = GO.transform.Find("UITxt_LanguageName").GetComponent<Text>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        bool isSelect = Game.GetMod<ModLanguage>().CurLanguage == data.languageType.ToDes();
        UIImg_SelectedBG.gameObject.SetActive(isSelect);
        Shadow com_Shadow = UITxt_LanguageName.GetComponent<Shadow>();
        Outline com_Outline = UITxt_LanguageName.GetComponent<Outline>();
        com_Shadow.effectColor = isSelect ? ColorUtils.Hex2Color("#186D08") : ColorUtils.Hex2Color("#1045A5");
        com_Outline.effectColor = isSelect ? ColorUtils.Hex2Color("#186D08") : ColorUtils.Hex2Color("#1045A5");
        UIBtn_Select.onClick.AddListener(OnSelectBtn);
        UITxt_LanguageName.text = data.languageType.ToLanguageName();
    }

    private void OnSelectBtn()
    {
        var curLocale = Game.GetMod<ModLanguage>().CurLanguage;
        if (curLocale == data.languageType.ToDes())
            return;

        Game.GetMod<ModLanguage>().SetCurLanguage(data.languageType.ToDes());
        Game.GetMod<ModEvent>().Dispatch(new EvtLanguageChange());
    }
}