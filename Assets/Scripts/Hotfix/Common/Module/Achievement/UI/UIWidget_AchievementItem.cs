using DragonPlus.Config.Common;
using Framework;
using UnityEngine.UI;

public class UIWidget_AchievementItem : UIWidget_AchievementItemBase
{
    private Table_Common_Achievement _info;
    public void RefreshWight(int id)
    {
        _info = Game.GetMod<ModConfig>().GetConfig<Table_Common_Achievement>(id);
        UITxt_AchievementName.SetText(CoreUtils.GetLocalization(_info.Name));
        UITxt_Date.SetText(Game.GetMod<ModAchievement>().GetCollectedAchievementTime(id));
        UIImg_AchievementIcon.sprite = CoreUtils.GetSprite(ModAchievement.Atlas_Achievement, _info.Icon, UIImg_AchievementIcon.gameObject);
    }
}
