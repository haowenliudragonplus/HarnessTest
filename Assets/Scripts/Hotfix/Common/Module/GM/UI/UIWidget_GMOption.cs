#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using Framework;
using UnityEngine.UI;

public class UIWidget_GMOption : UIWidget_GMOptionBase
{
    public class OpenData
    {
        public GMBase gm;
        public Action onSelect;
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
        UIBtn_Entrance.onClick.AddListener(OnClickEntrance);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        UIOldTxt_Name.text = openData.gm.OptionName;
        RefreshView_SelectedImg();
    }

    public void RefreshView_SelectedImg()
    {
        UIImg_Selected.gameObject.SetActive((Parent as UIView_GM).CurSelectOption == openData.gm);
    }

    private void OnClickEntrance()
    {
        openData.onSelect?.Invoke();
    }
}

#endif