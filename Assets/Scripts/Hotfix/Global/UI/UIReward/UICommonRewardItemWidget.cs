// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/10/16:29
// Ver : 1.0.0
// Description : UIItemView.cs
// ChangeLog :
// **********************************************

using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DragonPlus;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UICommonRewardItemWidget : UIWidgetBase
{
    public Image icon;
    public LocalizeTextMeshProUGUI text;
    public LocalizeTextMeshProUGUI infiniteText;
    public Transform tagImage;
    public Transform countLess;

    public Transform doubleTrans;
    //public Transform effect;
    public ItemData TableGameItemData;

    protected override void OnCreate()
    {
        base.OnCreate();
        if (ViewData != null)
        {
            ItemData data = ViewData as ItemData;
            if (data != null)
            {
                Refresh(data);
            }
        }
    }

    protected override void BindComponent()
    {
        base.BindComponent();
        icon = GO.transform.Find("Icon").GetComponent<Image>();
        text = GO.transform.Find("NumberText").GetComponent<LocalizeTextMeshProUGUI>();
        infiniteText = GO.transform.Find("TagImage/NumberText").GetComponent<LocalizeTextMeshProUGUI>();

        tagImage = GO.transform.Find("TagImage");
        countLess = GO.transform.Find("Countless");
        doubleTrans = GO.transform.Find("Double");
        // effect = go.transform.Find("Icon");
    }

    public virtual void Refresh(ItemData data)
    {
        TableGameItemData = data;
        CoreUtils.SetImg(icon, data.itemCfg.Icon);
        text.gameObject.SetActive(false);
        tagImage.gameObject.SetActive(false);
        countLess.gameObject.SetActive(false);
        doubleTrans.gameObject.SetActive(false);

        //显示隐藏
        // if (data.GetItemType() == ItemType.AvatarFrame && effect != null)
        // {
        //     effect.gameObject.SetActive(false);
        // }
        //
        // if (data.GetItemInfinityIconType() == ItemInfinityIconType.None ||
        //     data.GetItemInfinityIconType() == ItemInfinityIconType.NoTag)
        // {
        //     text.gameObject.SetActive(true);
        // }
        //
        // if (data.GetItemInfinityIconType() == ItemInfinityIconType.TagAndInfinity)
        // {
        //     tagImage.gameObject.SetActive(true);
        //     countLess.gameObject.SetActive(true);
        // }

        //数据
        var amount = Mathf.Max(1, data.amount);
        string textValue = data.itemCfg.IsTimeLimitItem
            ? TimeUtils.FormatTime((long)(data.amount ))
            : data.amount.ToString();
        text.SetText(textValue);
        infiniteText.SetText(textValue);
    }

    public void Fly(Action action = null)
    {
        GO.SetActive(false);
        var transform = FlyTarget.GetTarget(TableGameItemData.id);
        if (null == transform)
        {
            action?.Invoke();
            return;
        }
        Game.GetMod<FlySys>().FlyItem(TableGameItemData.id, TableGameItemData.amount,
            GO.transform.position, transform.position
            , action);
    }

    public async UniTask FlyItemToTarget()
    {
        var taskCompleteSource = new UniTaskCompletionSource();
        var startPos = GO.transform.position;
        var localPos = GO.transform.localPosition;
        var destPos = FlyTarget.GetTarget(TableGameItemData.id).position;

        Vector3 control = Vector3.zero;
        control.x = startPos.x + 0.3f;
        control.y = startPos.y - 0.3f;
        control.z = startPos.z;

        var efPrefab = GameObjectPool.Get("VFX_Trail_0");
        if (efPrefab != null)
        {
            efPrefab.gameObject.SetActive(false);
            efPrefab.transform.SetParent(GO.transform);
            efPrefab.transform.Reset();
            efPrefab.gameObject.SetActive(true);
        }

        Vector3 control1 = Vector3.MoveTowards(control, destPos, 1);

        Sequence s = DOTween.Sequence();

        var scale = GO.transform.localScale;
        s.Append(GO.transform.DOScale(new Vector3(scale.x + 0.1f, scale.y + 0.1f, scale.z + 0.1f), 0.3f));
        s.Append(GO.transform
            .DOPath(new[] { startPos, control, control1, destPos }, 0.8f, PathType.CatmullRom)
            .SetEase(Ease.InQuart));
        s.OnComplete(() =>
        {
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(TableGameItemData.id);
            Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd((EItemType)itemCfg.Id));
            GameObjectPool.Put(efPrefab);
            taskCompleteSource.TrySetResult();
            GO.transform.localPosition = localPos;
            GO.transform.localScale = scale;
        });

        s.Play();

        await taskCompleteSource.Task;
    }
}