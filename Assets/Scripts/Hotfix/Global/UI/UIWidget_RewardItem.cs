using TMGame;

public enum EUIRewardItemType
{
    Element,
    Currency,
}

public class UIRewardItemData
{
    public EUIRewardItemType rewardItemShowType;
    public int logicId;
    public int count;
}

/// <summary>
/// 通用奖励item
/// </summary>
public class UIWidget_RewardItem : UIWidget_RewardItemBase
{
    protected override void OnOpen()
    {
        base.OnOpen();
        UIRewardItemData data = ViewData as UIRewardItemData;
        switch (data.rewardItemShowType)
        {
            // case EUIRewardItemType.Element:
            //     var elementCfg = Game.GetMod<ConfigMgr>().GetConfigs<Table_Merge_ElementConfig>().Find(cfg => cfg.ElementID == data.logicId);
            //     if (elementCfg != null)
            //     {
            //         CoreUtils.SetImg(UIImg_Icon, MergeUtils.GetElementSprite(data.logicId, UIImg_Icon.gameObject), elementCfg.SizeIcon);
            //     }
            //     break;

            case EUIRewardItemType.Currency:
                break;
        }
        UITxt_Text.text = $"X{data.count}";
    }
}