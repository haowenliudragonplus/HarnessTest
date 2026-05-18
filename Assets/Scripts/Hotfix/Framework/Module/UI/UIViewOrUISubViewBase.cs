using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIViewOrUISubViewBase : UIBase
{
    private List<UISubViewBase> SubViewList = new List<UISubViewBase>(); //所有子界面
    private List<UISubViewBase> SubViewList_Temp = new List<UISubViewBase>();

    #region 子界面

    public void InternalAddToSubViewList(UISubViewBase subView)
    {
        SubViewList.Add(subView);
    }

    /// <summary>
    /// 打开一个子界面
    /// </summary>
    public T OpenUISubView<T>(Transform trans, object viewData = null)
        where T : UISubViewBase
    {
        Type classType = typeof(T);
        string subViewName = classType.Name;
        T subView = Activator.CreateInstance(classType) as T;
        if (subView == null)
            return null;

        subView.InternalInit(this, subViewName, viewData);
        bool createRet = subView.InternalCreate(trans);
        if (!createRet)
            return null;
        subView.InternalOpen();
        return subView;
    }

    /// <summary>
    /// 显示一个子界面
    /// </summary>
    public void ShowUISubView(UISubViewBase subView)
    {
        if (subView == null)
            return;

        subView.InternalShow();
    }

    /// <summary>
    /// 关闭一个子界面
    /// </summary>
    public bool CloseUISubView(UISubViewBase subView, bool isDestroy)
    {
        foreach (var temp in SubViewList)
        {
            if (temp == subView)
            {
                subView.InternalClose(isDestroy);
                if (isDestroy)
                {
                    SubViewList.Remove(subView);
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 关闭所有子界面
    /// </summary>
    public void CloseAllUISubView(bool isDestroy)
    {
        foreach (var subView in SubViewList)
        {
            subView.InternalClose(true);
        }
        if (isDestroy)
        {
            SubViewList.Clear();
        }
    }

    #endregion 子界面

    protected override void OnOpen()
    {
        base.OnOpen();
        foreach (var subView in SubViewList)
        {
            if (!subView.Visible)
                continue;
            subView.OnOpen();
        }
    }

    protected override void OnShow()
    {
        base.OnShow();
        foreach (var subView in SubViewList)
        {
            if (!subView.Visible)
                continue;
            subView.OnShow();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            if (!subView.Visible)
                continue;
            subView.OnUpdate();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        foreach (var subView in SubViewList)
        {
            if (!subView.Visible)
                continue;
            subView.OnClose();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var subView in SubViewList)
        {
            subView.OnDestroy();
        }
        SubViewList.Clear();
    }
}