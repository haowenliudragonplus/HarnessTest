using System.Collections.Generic;
using System.Linq;
using DragonPlus.Core;
using TMGame;

public class SetLanguageBtnData
{
    public ELanguageType languageType;
}

/// <summary>
/// 设置语言界面
/// </summary>
public class UIView_SetLanguage : UIView_SetLanguageBase
{
    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        Game.GetMod<ModEvent>().Register<EvtLanguageChange>(OnEventLanguageChange);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Close.onClick.AddListener(OnCloseBtn);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        var supportLanguageList = Game.GetMod<ModLanguage>().GetSupportLanguageTypeList();
        List<SetLanguageBtnData> setLanguageBtnDatas = new List<SetLanguageBtnData>();
        for (int i = 0; i < supportLanguageList.Count; i++)
        {
            SetLanguageBtnData data = new SetLanguageBtnData();
            data.languageType = supportLanguageList[i];
            setLanguageBtnDatas.Add(data);
        }
        UIContainer_SetLanguageBtn.Refresh<UIWidget_SetLanguageBtn>(setLanguageBtnDatas.ToArray(), false);
    }

    private void OnCloseBtn()
    {
        Close();
    }

    private void OnEventLanguageChange(EvtLanguageChange data)
    {
        Close();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Game.GetMod<ModEvent>().UnRegister<EvtLanguageChange>(OnEventLanguageChange);
    }
}