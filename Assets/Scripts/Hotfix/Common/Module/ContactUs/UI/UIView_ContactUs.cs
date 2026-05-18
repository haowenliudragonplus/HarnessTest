using System.Globalization;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using DragonPlus;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Google.Protobuf.Collections;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_ContactUs : UIView_ContactUsBase
{
    RepeatedField<UserComplainMessage> MessageList { get; set; }
    GameObject MyCellUI { get; set; }
    GameObject OtherCellUI { get; set; }
    private ScrollRect ScrollView;
    private Text InputTextLengthText;
    private Text inputMailPlaceholder;

    // 需要等待执行的计数器
    int UpdateWaitAction { get; set; }

    // 输入最长文本
    private const int MaxShowLength = 200;

    private bool _isInitedFAQCell;

    protected override void OnCreate()
    {
        base.OnCreate();
        //UIIF_InputField_input_info.text = "0/" + MaxInputLength.ToString();
        InputTextLengthText = UIIF_InputField_input_info.transform.Find("TextDefault").GetComponent<Text>();
        UINode_Input.gameObject.SetActive(false);
        ScrollView = GO.transform.Find("Root/UINode_MidImage/CoinsEnough/Scroll View").GetComponent<ScrollRect>();
        inputMailPlaceholder = UIIF_InputField_Email.transform.Find("Placeholder").GetComponent<Text>();
        UIIF_InputField_input_info.characterLimit = MaxShowLength;
        InputTextLengthText.text = "0/" + MaxShowLength.ToString();
        ReloadUI();
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        base.UIIF_InputField_input_info.onValidateInput += OnValidateInput;
        UIBtn_CloseButton.onClick.AddListener(OnClickCloseButton);
        UIBtn_UpgradeButton.onClick.AddListener(OnClickSendButton);
        base.UIIF_InputField_input_info.onValueChanged.AddListener((newValue) => { OnChangeInputField(); });
        Game.GetMod<ModEvent>().Register<EvtFaqSelectQuestion>(ChangeSelectQuestion);
        Game.GetMod<ModEvent>().Register<EvtFaqQuestionServerBack>(ServerBackQuestion);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        base.UIIF_InputField_input_info.onValidateInput -= OnValidateInput;
        UIBtn_CloseButton.onClick.RemoveListener(OnClickCloseButton);
        UIBtn_UpgradeButton.onClick.RemoveListener(OnClickSendButton);
        base.UIIF_InputField_input_info.onValueChanged.RemoveListener((newValue) => { OnChangeInputField(); });
        Game.GetMod<ModEvent>().UnRegister<EvtFaqSelectQuestion>(ChangeSelectQuestion);
        Game.GetMod<ModEvent>().UnRegister<EvtFaqQuestionServerBack>(ServerBackQuestion);
    }

    public char OnValidateInput(string input, int charIndex, char addedChar)
    {
        if (char.GetUnicodeCategory(addedChar) == UnicodeCategory.Surrogate)
        {
            return '\0';
        }
        return addedChar;
    }

    public void ChangeSelectQuestion(EvtFaqSelectQuestion evt)
    {
        CreateMyMessage(evt.id);
        CreateOtherMessage(evt.id);
    }

    public void ServerBackQuestion(EvtFaqQuestionServerBack evt)
    {
        CreateOtherMessage(evt.id);
    }

    private void CreateOtherMessage(int id)
    {
        var message = new UserComplainMessage();
        message.Message = Game.GetMod<FaqSys>().GetAnswer(id);
        message.CreatedAt = (ulong)SDKUtil.TimeDate.CurrentTimeInMilliseconds();
        var widget = OpenUIWidget<UIView_ContactUsItem>(UINode_Content, false);
        widget.SetData(this, message);
        widget.AddIOSQuestions(id);
    }

    void CreateMyMessage(int id)
    {
        var message = new UserComplainMessage();
        message.Message = Game.GetMod<FaqSys>().GetQuestion(id);
        message.CreatedAt = (ulong)SDKUtil.TimeDate.CurrentTimeInMilliseconds();
        var widget = OpenUIWidget<UIView_ContactUsItem>(UINode_Content, false);
        widget.SetData(this, message);
    }

    private char MyValidate(char charToValidate)
    {
        // Emoji表情
        if (char.GetUnicodeCategory(charToValidate) == UnicodeCategory.Surrogate)
        {
            return '\0';
        }

        return charToValidate;
    }

    void OnChangeInputField()
    {
        string showStr = UIIF_InputField_input_info.text;
        if (UIIF_InputField_input_info.text.Length > MaxShowLength)
        {
            showStr = showStr.Substring(0, MaxShowLength);
            showStr += ".....";
        }
        UIOldTxt_TipText.text = showStr;
        UINode_Input.gameObject.SetActive(!string.IsNullOrEmpty(UIOldTxt_TipText.text));

        var textLength = UIOldTxt_TipText.text.Length.ToString();
        if (UIOldTxt_TipText.text.Length > 0)
        {
            InputTextLengthText.text = "";
            return;
        }

        InputTextLengthText.text = textLength + "/" + MaxShowLength.ToString();
    }

    void ReloadUI()
    {
        Game.GetMod<ModUI>().ShowWaiting();
        Game.GetMod<ContactUsLogic>().OnRequestMessageList(OnGetMessageListResult, OnGetMessageListError);
        inputMailPlaceholder.text = CoreUtils.GetLocalization("UI_ support_2");
        var email = Game.GetMod<ContactUsLogic>().GetSubscribeEmailAddress();
        if (!string.IsNullOrEmpty(email))
            UIIF_InputField_Email.text = email;
    }

    void OnGetMessageListResult()
    {
        UpdateWaitAction = 2;
        var messagesList = Game.GetMod<ContactUsLogic>().MessageList.Messages;

        for (int index = 0; index < messagesList.Count; index++)
        {
            UserComplainMessage itemData = messagesList[index];

            AddCell(itemData);
        }

        // RedPointCenter.Instance.Set(RedPointCenter.ContactUsRedPointKey, 0);
        //CreatFAQCell();
        DelayMove().Forget();
    }

    private async UniTask DelayMove()
    {
        await GotoScrollViewEnd(0.5f);
        await UniTask.WaitForSeconds(0.2f);
        Game.GetMod<ModUI>().CloseWaiting();
    }

    void AddCell(UserComplainMessage itemData)
    {
        if (itemData.MessageType == UserComplainMessage.Types.MessageType.Complain)
        {
            var widget = OpenUIWidget<UIView_ContactUsItem2>(UINode_Content, false);
            widget.SetData(this, itemData);
        }
        else if (itemData.MessageType == UserComplainMessage.Types.MessageType.Reply)
        {
            var widget = OpenUIWidget<UIView_ContactUsItem>(UINode_Content, false);
            widget.SetData(this, itemData);
        }
    }

    void OnGetMessageListError()
    {
        Game.GetMod<ModUI>().CloseWaiting();
    }

    public async UniTask GotoScrollViewEnd(float delayTime)
    {
        await UniTask.WaitForSeconds(delayTime);
        var endY = UINode_Content.GetComponent<RectTransform>().sizeDelta.y -
                   ScrollView.GetComponent<RectTransform>().sizeDelta.y;
        UINode_Content.localPosition = new Vector3(0, endY, 1);
        //ScrollView.StopMovement();
    }

    void OnClickSendButton()
    {
        if (string.IsNullOrEmpty(base.UIIF_InputField_input_info.text))
        {
            Log.Info("消息不能为空");

            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                showMidBtn = true,
                content = CoreUtils.GetLocalization("&key.UI_contactus_panel_input_popup_text"),
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            return;
        }

        if (string.IsNullOrEmpty(UIIF_InputField_Email.text) || (!string.IsNullOrEmpty(UIIF_InputField_Email.text) && !IsEmail(UIIF_InputField_Email.text)))
        {
            Log.Info("错误的邮箱");

            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                showMidBtn = true,
                content = CoreUtils.GetLocalization("&key.UI_contactus_panel_email_err_text")
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            return;
        }

        var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
        if (string.IsNullOrEmpty(storageCommon.Email) && !string.IsNullOrEmpty(UIIF_InputField_Email.text))
        {
            storageCommon.Email = UIIF_InputField_Email.text;
        }

        Game.GetMod<ModUI>().ShowWaiting();

        string email = string.IsNullOrEmpty(UIIF_InputField_Email.text) ? "" : UIIF_InputField_Email.text;

        Game.GetMod<ContactUsLogic>()
            .SenMyMessage(email, base.UIIF_InputField_input_info.text, OnSendMessageResult, OnSendMessageError);
    }

    void OnSendMessageResult()
    {
        Game.GetMod<ModUI>().CloseWaiting();

        if (this == null)
            return;

        base.UIIF_InputField_input_info.text = "";
        OnChangeInputField();
        UpdateWaitAction = 2;
        AddCell(Game.GetMod<ContactUsLogic>().TempSendMessage);
    }

    private void CreatFAQCell()
    {
        if (!_isInitedFAQCell)
        {
            _isInitedFAQCell = true;
            CreateOtherMessage(0);
        }
    }

    void OnSendMessageError()
    {
        Game.GetMod<ModUI>().CloseWaiting();

        if (this == null)
            return;
        UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
        {
            showMidBtn = true,
            content = CoreUtils.GetLocalization("&key.UI_contactus_panel_send_err_text")
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
    }

    void OnClickCloseButton()
    {
        Close();
    }

    public void OnUpdate(float deltaTime)
    {
        UpdateWaitAction--;
        if (UpdateWaitAction == 0)
        {
            GotoScrollViewEnd(0).Forget();
        }
    }

    private bool IsEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        if (email.Length > 320) return false;
        var atIndex = email.IndexOf("@");
        if (atIndex < 1) return false;
        var frontChars = email.Substring(0, atIndex);
        var endChars = email.Substring(atIndex + 1, email.Length - atIndex - 1);
        if (frontChars.Length > 64 || endChars.Length > 255) return false;

        email = email.Trim();
        string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        return Regex.IsMatch(email, pattern);
    }
}