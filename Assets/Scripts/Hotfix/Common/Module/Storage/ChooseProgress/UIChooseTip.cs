// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/18/17:06
// Ver : 1.0.0
// Description : UIChooseTip.cs
// ChangeLog :
// **********************************************

using System.Collections.Generic;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using UnityEngine.UI;
using TMGame;
using UnityEngine;

public class UIChooseTip : UIViewBase
{
    private InputField _inputField;
    private Button _confirmButton;
    private Button _cancelButton;
    private Button _grayConfirmButton;
    private Button _closeButton;
    private const string ConfirmString = "111";

    public EventProfileConflict.ProfileInfo profileInfo;
    public bool _useServer;

    protected override void BindComponent()
    {
        base.BindComponent();
        _inputField = GO.transform.Find("Root/ContentGroup/InputField").GetComponent<InputField>();
        _confirmButton = GO.transform.Find("Root/ContentGroup/ButtonGroup/ConfirmButton").GetComponent<Button>();
        _cancelButton = GO.transform.Find("Root/ContentGroup/ButtonGroup/CancelButton").GetComponent<Button>();
        _grayConfirmButton = GO.transform.Find("Root/ContentGroup/ButtonGroup/ConfirmGreyButton").GetComponent<Button>();
        _closeButton = GO.transform.Find("Root/CloseButton").GetComponent<Button>();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        List<object> param = (List<object>)ViewData;
        profileInfo = param[0] as EventProfileConflict.ProfileInfo;
        _useServer = (bool)param[1];
        SetConfirmButtonEnable(false);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        _closeButton.gameObject.SetActive(false);
        _grayConfirmButton.interactable = false;
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        _cancelButton.onClick.AddListener(OnCancelButtonClick);
        _inputField.onValueChanged.AddListener(OnTextFieldTextChanged);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        _confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        _cancelButton.onClick.RemoveListener(OnCancelButtonClick);
        _inputField.onValueChanged.RemoveListener(OnTextFieldTextChanged);
    }

    private void OnTextFieldTextChanged(string text)
    {
        SetConfirmButtonEnable(text.Equals(ConfirmString));
    }

    private void SetConfirmButtonEnable(bool enable)
    {
        _confirmButton.gameObject.SetActive(enable);
        _grayConfirmButton.gameObject.SetActive(!enable);
    }

    private void OnCancelButtonClick()
    {
        Close();
    }

    private void OnConfirmButtonClick()
    {
        if (_inputField.text.Equals(ConfirmString))
        {
            Confirm();
        }
    }

    private void Confirm()
    {
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventChooseProfileSelect,
            _useServer ? "server" : "client");
        SDK<IUserProfile>.Instance.ResolveProfile(profileInfo.serverProfile, profileInfo.serverVersion, _useServer);
        Game.GetMod<ModUI>().Close(UIViewName.UIChooseTip);
        Game.GetMod<ModUI>().Close(UIViewName.UIChooseProgress);
    }
}