using System.Collections.Generic;
using DG.Tweening;
using DragonPlus.Config.Common;
using Framework;
using UnityEngine;

public class UIView_FlyObj : UIView_FlyObjBase
{
    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        RegisterEvent<EvtPlayFlyObj>(OnPlayFlyObj);
    }

    private void OnPlayFlyObj(EvtPlayFlyObj evt)
    {
        var flyObjData = evt.flyObjData;
        var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(flyObjData.itemId);
        // todo：在此扩展飞物体的表现
        switch ((EItemType)itemCfg.Id)
        {
            case EItemType.Energy:
            case EItemType.EnergyInfinity:
                break;

            default:
                Performance_CommonStraightSequence(flyObjData, evt.isLastInGroup,
                    0.2f, 0.1f, 0.5f);
                ;
                break;
        }
    }

    #region 飞道具表现

    /// <summary>
    /// 通用直线序列
    /// </summary>
    private void Performance_CommonStraightSequence(FlyObjData flyObjData, bool isLastInGroup,
        float spawnTime, float sequenceTimeInterval, float moveAniTime)
    {
        List<UIWidget_FlyObj> flyObjWidgetList = new();
        for (int i = 0; i < flyObjData.showCount; i++)
        {
            UIWidget_FlyObj.OpenData openData = new UIWidget_FlyObj.OpenData()
            {
                flyObjData = flyObjData,
                spawnTime = spawnTime,
            };
            var widget = OpenUIWidget<UIWidget_FlyObj>(RectTransform, false, openData);
            flyObjWidgetList.Add(widget);
        }
        Game.GetMod<ModTimer>().Register(spawnTime, true, onComplete: (v) =>
        {
            float tempTimer = 0;
            for (int i = 0; i < flyObjWidgetList.Count; i++)
            {
                int tempI = i;
                Game.GetMod<ModTimer>().Register(tempTimer, true, onComplete: (v) =>
                {
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(flyObjWidgetList[tempI].RectTransform.DOLocalMove(flyObjData.toUIPos, moveAniTime)
                        .SetEase(Ease.InSine));
                    sequence.SetUpdate(true);
                    sequence.OnComplete(() =>
                    {
                        EvtFlyObjSingleComplete evtFlyObjSingleComplete = new EvtFlyObjSingleComplete()
                        {
                            itemId = flyObjData.itemId,
                            addRealCount = flyObjData.realCount / (float)flyObjData.showCount,
                            flyObjTag = flyObjData.flyObjTag,
                        };
                        Game.GetMod<ModEvent>().Dispatch(evtFlyObjSingleComplete);

                        if (tempI == flyObjWidgetList.Count - 1 && isLastInGroup)
                        {
                            EvtFlyObjGroupComplete evtFlyObjGroupComplete = new EvtFlyObjGroupComplete()
                            {
                                itemId = flyObjData.itemId,
                                flyObjTag = flyObjData.flyObjTag,
                            };
                            Game.GetMod<ModEvent>().Dispatch(evtFlyObjGroupComplete);
                        }

                        CloseUIWidget(flyObjWidgetList[tempI], true);
                    });
                });
                tempTimer += sequenceTimeInterval;
            }
        });
    }

    #endregion 飞道具表现
}