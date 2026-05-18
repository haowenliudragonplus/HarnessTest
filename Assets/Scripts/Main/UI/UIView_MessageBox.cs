using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView_MessageBox : MonoBehaviour
{
    private static UIView_MessageBox ins;
    public static UIView_MessageBox Ins
    {
        get
        {
            if (ins == null)
            {
                GameObject viewGo = Instantiate(Resources.Load<GameObject>(Const_Boot.PrefabPath_MessageBox));
                viewGo.transform.SetParent(GameObject.Find(Const_Boot.ScenePath_UICanvas).transform, false);
                ins = viewGo.GetComponent<UIView_MessageBox>();
            }
            return ins;
        }
    }

    public Text UITxt_Title;
    public Text UITxt_Content;
    public Text UITxt_LeftBtn;
    public Text UITxt_RightBtn;
    public Button UIBtn_LeftBtn;
    public Button UIBtn_RightBtn;
    public Button UIBtn_Close;
    private Action onLeftBtn;
    private Action onRightBtn;
    private bool closeSelf;

    public void ShowMessageBox(string content, string title = "", string leftBtnName = "", bool showRightBtn = true, bool showCloseBtn = false, string rightBtnName = "",
        Action onLeftBtn = null, Action onRightBtn = null, bool closeSelf = true)
    {
        UITxt_Title.text = string.IsNullOrEmpty(title) ? GameConfig.GetLocaleStr("UI_common_box_tittle") : title;
        UITxt_Content.text = content;
        UITxt_LeftBtn.text = string.IsNullOrEmpty(leftBtnName) ? GameConfig.GetLocaleStr("UI_button_ok") : leftBtnName;
        this.closeSelf = closeSelf;
        this.onLeftBtn = onLeftBtn;
        UIBtn_LeftBtn.gameObject.SetActive(true);
        UIBtn_LeftBtn.onClick.AddListener(OnLeftBtn);
        if (showRightBtn)
        {
            UIBtn_RightBtn.gameObject.SetActive(true);
            UITxt_RightBtn.gameObject.SetActive(true);
            UITxt_RightBtn.text = string.IsNullOrEmpty(rightBtnName) ? GameConfig.GetLocaleStr("UI_button_cancel") : rightBtnName;
            UIBtn_RightBtn.onClick.AddListener(OnRightBtn);
            this.onRightBtn = onRightBtn;
        }
        else
        {
            UIBtn_RightBtn.gameObject.SetActive(false);
            UITxt_RightBtn.gameObject.SetActive(false);
        }
        if (showCloseBtn)
        {
            UIBtn_Close.gameObject.SetActive(true);
        }
        else
        {
            UIBtn_Close.gameObject.SetActive(false);
            UIBtn_Close.onClick.AddListener(CloseSelf);
        }
    }

    private void OnLeftBtn()
    {
        CloseSelf();
        onLeftBtn?.Invoke();
    }

    private void OnRightBtn()
    {
        CloseSelf();
        onRightBtn?.Invoke();
    }

    private void CloseSelf()
    {
        if (!closeSelf)
            return;
        Destroy(gameObject);
        ins = null;
    }
}