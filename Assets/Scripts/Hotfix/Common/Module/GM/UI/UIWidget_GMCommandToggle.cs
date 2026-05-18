#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using Framework;
using UnityEngine.UI;

public class UIWidget_GMCommandToggle : UIWidget_GMCommandToggleBase
{
    public class OpenData
    {
        public GMCommandData data;
        public Action<bool> onValueChanged;
    }

    private OpenData openData;

    private bool sign;

    protected override void OnCreate()
    {
        base.OnCreate();
        openData = ViewData as OpenData;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIToggle_Command.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        UIOldTxt_Name.text = openData.data.CommandName;
        UIToggle_Command.isOn = openData.data.GetToggleInitStateEvent == null ? false : openData.data.GetToggleInitStateEvent.Invoke();
        UIBtn_Tip.gameObject.SetActive(!string.IsNullOrEmpty(openData.data.TipStr));
        UIBtn_Tip.onClick.AddListener(OnTipBtn);
        if (UIToggle_Command.isOn)
        {
            sign = true;
        }
    }

    private void OnValueChanged(bool b)
    {
        openData.onValueChanged?.Invoke(b);
        if (sign && openData.data.CloseViewAfterExecute)
        {
            Game.GetMod<ModUI>().Close(UIViewName.UIView_GM);
        }
        if (!sign && !b)
            sign = true;
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