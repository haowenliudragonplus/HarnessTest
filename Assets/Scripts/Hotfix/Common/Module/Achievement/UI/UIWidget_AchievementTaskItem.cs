using DragonPlus.Config.Common;
using Framework;
using UnityEngine.UI;

public class UIWidget_AchievementTaskItem : UIWidget_AchievementTaskItemBase
{
    private Table_Common_Achievement _info;
    private ModAchievement _modAchievement;
    protected override void BindComponent()
    {
        base.BindComponent();
        UIBtn_Receive.onClick.AddListener(OnClickReceive);
    }

    private void OnClickReceive()
    {
        if (_info==null)
            return;
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_GetAchievementPop, _info.Id);
        _modAchievement.ClaimAchievement(_info.Id);
    }
    
    public void RefreshWight(int id)
    {
        _modAchievement = Game.GetMod<ModAchievement>();
        _info = _modAchievement.GetAchievementById(id);
        UITxt_AchievementName.SetText(CoreUtils.GetLocalization(_info.Name));
        UITxt_AchievementName_Completed.SetText(CoreUtils.GetLocalization(_info.Name));
        UITxt_TaskDesc.SetText(CoreUtils.GetLocalization(_info.Desc,_info.Count));
        UITxt_TaskDesc_Completed.SetText(CoreUtils.GetLocalization(_info.Desc,_info.Count));
        UIImg_AchievementMedalIcon.sprite = CoreUtils.GetSprite(ModAchievement.Atlas_Achievement, _info.Icon, UIImg_AchievementMedalIcon.gameObject);
        if (_modAchievement.GetAchievementClaimableTime(id)!=0)
        {
            UINode_InProgress.gameObject.SetActive(false);
            UIBtn_Receive.gameObject.SetActive(true);
            UINode_Completed.gameObject.SetActive(false);
            UISlider_Progress.value = 1;
            UITxt_Progress.SetText($"{_info.Count}/{_info.Count}");
        }
        else if (_modAchievement.GetCollectedAchievementTime(id)!="")
        {
            UINode_InProgress.gameObject.SetActive(false);
            UIBtn_Receive.gameObject.SetActive(false);
            UINode_Completed.gameObject.SetActive(true);
            UISlider_Progress.value = 1;
            UITxt_Progress.SetText($"{_info.Count}/{_info.Count}");
        }
        else
        {
            UINode_InProgress.gameObject.SetActive(true);
            UIBtn_Receive.gameObject.SetActive(false);
            UINode_Completed.gameObject.SetActive(false);
            var cur = _modAchievement.GetCurAchievementCount(_info.Type);
            UISlider_Progress.value = (float)cur/_info.Count;
            UITxt_Progress.SetText($"{cur}/{_info.Count}");
        }
        
    }
    
}
