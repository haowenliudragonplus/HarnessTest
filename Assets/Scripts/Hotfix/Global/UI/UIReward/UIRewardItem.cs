using System.Collections;
using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Config.Common;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardItem : MonoBehaviour
{
    private Image _icon;
    private LocalizeTextMeshProUGUI _num;
    private Transform UINode_Countless;
    private Image _tagIcon;
    public void Init(ItemData data)
    {
        if (this == null || gameObject == null)
            return;
        _icon = transform.Find("UIImg_Icon").GetComponent<Image>();
        _num = transform.Find("UITxt_Num").GetComponent<LocalizeTextMeshProUGUI>();
        var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(data.id);
        CoreUtils.SetImg(_icon, itemCfg.Icon);
        if (itemCfg.IsTimeLimitItem)
        {
            _num.SetText(TimeUtils.FormatTime(data.amount));
        }
        else
        {
            _num.SetText(data.amount.ToString());
        }
    }
}