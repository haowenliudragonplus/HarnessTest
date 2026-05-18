using DragonPlus.Config.Common;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_GuideMain : UIView_GuideMainBase
{
    private Transform transRoot;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        transRoot = GO.transform.Find("Root");
        OnInit();
    }
    private void OnInit()
    {
        var _tableGameGuide = ViewData as Table_Common_Guide;
        OpenUISubView<UISubView_Guide>(transRoot,_tableGameGuide);

    }
}
