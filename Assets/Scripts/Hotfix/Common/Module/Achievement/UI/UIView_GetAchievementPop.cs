using DragonPlus.Config.Common;
using Framework;
using UnityEngine.UI;

public class UIView_GetAchievementPop : UIView_GetAchievementPopBase
{
    private Table_Common_Achievement _info;
    protected override void BindComponent()
    {
        base.BindComponent();
        UIBtn_Continue.onClick.AddListener(OnClickContinue);  
    }

    private void OnClickContinue()
    {
        Game.GetMod<ModUI>().Close(this);
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        if (viewData is int id) 
            _info = Game.GetMod<ModConfig>().GetConfig<Table_Common_Achievement>(id);
    }
    
    protected override void OnOpen()
    {
        base.OnOpen();
        var str =   TimeUtils.FormatDateTime(Game.GetMod<ModAchievement>().GetAchievementClaimableTime(_info.Id),"yyyy/MM/dd");
        UITxt_Date.SetText(str);
        UITxt_AchievementName.SetText(CoreUtils.GetLocalization(_info.Name));
        UIImg_AchievementMedalIcon.sprite = CoreUtils.GetSprite(ModAchievement.Atlas_Achievement, _info.Icon, UIImg_AchievementMedalIcon.gameObject);
    }
}
