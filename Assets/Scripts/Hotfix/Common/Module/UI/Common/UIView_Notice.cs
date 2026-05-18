using System;
using DragonPlus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通用通知界面
/// </summary>
public class UIView_Notice : UIView_NoticeBase
{
    public class OpenData
    {
        public bool showMidBtn = false;
        public string title = CoreUtils.GetLocalization("&key.UI_common_box_tittle");
        public string rightBtnTitle = CoreUtils.GetLocalization("&key.UI_button_ok");
        public string midBtnTitle = CoreUtils.GetLocalization("&key.UI_button_ok");
        public string leftBtnTitle = CoreUtils.GetLocalization("&key.UI_button_no");
        public Action onRightBtn;
        public Action onLeftBtn;
        public Action onMidBtn;
        public bool showCloseBtn = true;
        public string content;
        public bool enableScroll;
        public int fontSize = -1;
        public TextAlignmentOptions alignmentType = TextAlignmentOptions.Center;
        public bool useText = false;
    }

    private OpenData openData;

    private const int DefaultFontSize = 35;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenData;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Close.onClick.AddListener(OnCloseBtn);
        UIBtn_Left.onClick.AddListener(OnLeftBtn);
        UIBtn_Right.onClick.AddListener(OnRightBtn);
        UIBtn_Mid.onClick.AddListener(OnMidBtn);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        // 标题
        if (openData.useText)
        {
            UITxt_Title.gameObject.SetActive(false);
            UIOldTxt_Title.gameObject.SetActive(true);
            UIOldTxt_Title.text = openData.title;
        }
        else
        {
            UITxt_Title.gameObject.SetActive(true);
            UIOldTxt_Title.gameObject.SetActive(false);
            UITxt_Title.text = openData.title;
        }
        // 内容
        RefreshView_Content();
        // 按钮
        UIBtn_Close.gameObject.SetActive(false);
        UIBtn_Left.gameObject.SetActive(false);
        UIBtn_Right.gameObject.SetActive(false);
        UIBtn_Mid.gameObject.SetActive(false);
        bool showNodeBtn1 = openData.showMidBtn;
        if (showNodeBtn1)
        {
            UIBtn_Mid.gameObject.SetActive(true);
            UITxt_MidBtn.text = openData.midBtnTitle;
        }
        else
        {
            UITxt_LeftBtn.text = openData.leftBtnTitle;
            UIBtn_Left.gameObject.SetActive(true);
            UITxt_RightBtn.text = openData.rightBtnTitle;
            UIBtn_Right.gameObject.SetActive(true);
        }
        if (openData.showCloseBtn)
        {
            UIBtn_Close.gameObject.SetActive(true);
        }
    }

    private void RefreshView_Content()
    {
        if (openData.useText)
        {
            UISR_Content.content = UIOldTxt_Content.rectTransform;
            UIOldTxt_Content.gameObject.SetActive(true);
            UITxt_Content.gameObject.SetActive(false);
            UIOldTxt_Content.fontSize = DefaultFontSize;
            UIOldTxt_Content.resizeTextForBestFit = openData.fontSize == -1;
            if (openData.fontSize != -1)
            {
                UIOldTxt_Content.fontSize = openData.fontSize;
            }
            UIOldTxt_Content.SetTextSafely(openData.content);
            LayoutRebuilder.ForceRebuildLayoutImmediate(UIOldTxt_Content.rectTransform);
        }
        else
        {
            UISR_Content.content = UITxt_Content.rectTransform;
            UIOldTxt_Content.gameObject.SetActive(false);
            UITxt_Content.gameObject.SetActive(true);
            UITxt_Content.fontSize = DefaultFontSize;
            UITxt_Content.enableAutoSizing = openData.fontSize == -1;
            UITxt_Content.alignment = openData.alignmentType;
            if (openData.fontSize != -1)
            {
                UITxt_Content.fontSize = openData.fontSize;
            }
            UITxt_Content.SetTMPSafely(openData.content);
            LayoutRebuilder.ForceRebuildLayoutImmediate(UITxt_Content.rectTransform);
        }
        if (openData.enableScroll)
        {
            UISR_Content.vertical = (openData.useText
                ? UIOldTxt_Content.preferredHeight
                : UITxt_Content.preferredHeight) > UISR_Content.viewport.rect.height;
            UISR_Content.verticalNormalizedPosition = 1;
        }
        else
        {
            UISR_Content.vertical = false;
        }
    }

    private void OnRightBtn()
    {
        Close(true, () => { openData.onRightBtn?.Invoke(); });
    }

    private void OnLeftBtn()
    {
        Close(true, () => { openData.onLeftBtn?.Invoke(); });
    }

    private void OnMidBtn()
    {
        Close(true, () => { openData.onMidBtn?.Invoke(); });
    }

    private void OnCloseBtn()
    {
        Close();
    }
}