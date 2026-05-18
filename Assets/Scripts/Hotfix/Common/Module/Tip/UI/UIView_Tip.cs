using Framework;
using TMGame;
using UnityEngine;

public class UIView_Tip : UIView_TipBase
{
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.NoAni;
        playSound = false;
        enableVirbrate = false;
    }

    protected override void RegisterGameEvent()
    {
        Game.GetMod<ModEvent>().Register<EvtShowTip>(OnShowTip);
        Game.GetMod<ModEvent>().Register<EvtCloseTip>(OnCloseTip);
    }

    private void OnShowTip(EvtShowTip evt)
    {
        Transform trans = GetTrans(evt.openData.posType);
        if (evt.openData.tipType == ETipType.Achievement)
        {
            OpenUIWidget<UIWidget_AchievementTip>(trans, false, evt.openData);
        }
        else
        {
            OpenUIWidget<UIWidget_Tip>(trans, false, evt.openData);
        }
       
    }

    private void OnCloseTip(EvtCloseTip evt)
    {
        CloseUIWidget(evt.tipWidget, true);
    }

    private Transform GetTrans(ETipPosType posType)
    {
        switch (posType)
        {
            case ETipPosType.Top:
                return UINode_Top;

            case ETipPosType.Mid:
                return UINode_Mid;

            case ETipPosType.Bottom:
                return UINode_Bottom;
        }
        return null;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Game.GetMod<ModEvent>().UnRegister<EvtShowTip>(OnShowTip);
        Game.GetMod<ModEvent>().UnRegister<EvtCloseTip>(OnCloseTip);
    }
}