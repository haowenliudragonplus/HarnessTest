using System;
using UnityEngine.EventSystems;
using DG.Tweening;
using DragonPlus.Haptics;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EmptyGraphic))]
public class GameButton : Button
{
    [SerializeField]
    private bool enableClickSound;

    [SerializeField]
    private int clickSoundId; //点击按钮时的音效

    [SerializeField]
    private bool enableClickAni = true; //开启点击动画

    [SerializeField]
    private bool enableVirbrate = true; //开启点击震动

    private Sequence clickAniSeq;

    protected override void OnEnable()
    {
        base.OnEnable();
        clickAniSeq?.Kill(true);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable)
            return;
        base.OnPointerClick(eventData);
        if (enableClickAni)
        {
            PlayAni();
        }
        if (enableClickSound)
        {
            PlayClickSound();
        }
        if (enableVirbrate)
        {
            GameUtils.Virbrate(HapticTypes.Selection);
        }
    }

    private void PlayClickSound()
    {
        //Game.GetMod<ModAudio>().PlaySound( AudioName.sfx_bu,"sfx_button_m_1");
    }

    private void PlayAni()
    {
        clickAniSeq?.Kill(true);
        float originScale = transform.localScale.x;
        clickAniSeq = DOTween.Sequence();
        clickAniSeq.Append(transform.DOScale(Vector3.one * originScale * 0.85f, 0.1f));
        clickAniSeq.Append(transform.DOScale(Vector3.one * originScale, 0.1f));
        clickAniSeq.SetUpdate(true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        clickAniSeq?.Kill(true);
    }
}