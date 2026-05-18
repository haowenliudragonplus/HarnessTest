using System.Collections.Generic;
using Framework;
using TMGame;

public enum ETipPosType
{
    Top = 1,
    Mid,
    Bottom,
}

public enum ETipType
{
    Common = 1,
    Achievement,
}

public class OpenTipData
{
    public string content;
    public ETipPosType posType;
    public bool sequenceShow;
    public ETipType tipType;
}

public class ModTip : ModuleBase
{
    private Queue<OpenTipData> tipDataQueue = new Queue<OpenTipData>();
    private bool inSequenceTipShow;

    public override void OnInit()
    {
        base.OnInit();
        Game.GetMod<ModEvent>().Register<EvtCloseTip>(OnCloseTip);
    }

    public override void OnStart()
    {
        base.OnStart();
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Tip);
    }

    public void ShowTip(string content, ETipType tipType = ETipType.Common,  ETipPosType posType = ETipPosType.Mid, bool sequenceShow = false)
    {
        OpenTipData openData = new OpenTipData()
        {
            content = content,
            posType = posType,
            sequenceShow = sequenceShow,
            tipType = tipType
        };

        if (sequenceShow)
        {
            tipDataQueue.Enqueue(openData);
            CheckTipSequence();
        }
        else
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtShowTip(openData));
        }
    }
    private void OnCloseTip(EvtCloseTip evt)
    {
        if (!evt.isSequenceShow)
            return;
        inSequenceTipShow = false;
        CheckTipSequence();
    }

    private void CheckTipSequence()
    {
        if (tipDataQueue.Count <= 0)
            return;
        if (inSequenceTipShow)
            return;
        inSequenceTipShow = true;
        var data = tipDataQueue.Dequeue();
        Game.GetMod<ModEvent>().Dispatch(new EvtShowTip(data));
    }

    public override void OnDispose()
    {
        base.OnDispose();
        tipDataQueue?.Clear();
        inSequenceTipShow = false;
        Game.GetMod<ModUI>().Close(UIViewName.UIView_Tip);
        Game.GetMod<ModEvent>().UnRegister<EvtCloseTip>(OnCloseTip);
    }
}