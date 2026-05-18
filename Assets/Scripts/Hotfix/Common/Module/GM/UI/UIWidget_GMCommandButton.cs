#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using Framework;
using UnityEngine.UI;

public class UIWidget_GMCommandButton : UIWidget_GMCommandButtonBase
{
    public class OpenData
    {
        public GMCommandData data;
        public Action onClick;
    }

    private OpenData openData;

    protected override void OnCreate()
    {
        base.OnCreate();
        openData = ViewData as OpenData;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Command.onClick.AddListener(OnClickOption);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        UIOldTxt_Name.text = openData.data.CommandName;
        UIBtn_Tip.gameObject.SetActive(!string.IsNullOrEmpty(openData.data.TipStr));
        UIBtn_Tip.onClick.AddListener(OnTipBtn);
    }

    private void OnClickOption()
    {
        openData.onClick?.Invoke();
        if (openData.data.CloseViewAfterExecute)
        {
            Game.GetMod<ModUI>().Close(UIViewName.UIView_GM);
        }
    }

    private void OnTipBtn()
    {
        UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
        {
            showMidBtn = true,
            content = this.openData.data.TipStr,
            enableScroll = true,
            useText = true,
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
    }
}

#endif