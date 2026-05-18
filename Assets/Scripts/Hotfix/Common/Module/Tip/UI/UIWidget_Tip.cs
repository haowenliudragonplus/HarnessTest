using DG.Tweening;
using Framework;
using TMGame;

public class UIWidget_Tip : UIWidget_TipBase
{
    private OpenTipData openData;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenTipData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        UITxt_Text.text = openData.content;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(RectTransform.transform.DOLocalMoveY(70, 1f));
        sequence.SetUpdate(true);
        sequence.OnComplete(() =>
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtCloseTip(openData.sequenceShow, this));
        });
    }
}