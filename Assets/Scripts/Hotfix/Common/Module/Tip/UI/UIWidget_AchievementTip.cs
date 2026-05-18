using DG.Tweening;
using DragonPlus.Config.Common;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIWidget_AchievementTip : UIWidget_AchievementTipBase
{
    private OpenTipData openData;
    private Table_Common_Achievement _info;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenTipData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        GO.transform.localPosition = new Vector3(0,185f,0);
        _info = Game.GetMod<ModAchievement>().GetAchievementById(int.Parse(openData.content));
        UITxt_AchievementName.SetText(CoreUtils.GetLocalization(_info.Name));
        UIImg_AchievementIcon.sprite = CoreUtils.GetSprite(ModAchievement.Atlas_Achievement, _info.Icon, UIImg_AchievementIcon.gameObject);
        DOVirtual.DelayedCall(3, () =>
        {
            Game.GetMod<ModEvent>().Dispatch(new EvtCloseTip(openData.sequenceShow, this));
        });
    }
}
