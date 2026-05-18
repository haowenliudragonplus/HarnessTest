using Framework;
using UnityEngine.UI;

public class UIWidget_Entrance_RemoveAd : UIWidget_Entrance_RemoveAdBase
{
    override protected void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        GO.GetComponent<Button>().onClick.AddListener(OnEntranceBtn);
    }

    private void OnEntranceBtn()
    {
        var openData = new UIView_RemoveAd.OpenData
        {
            removeAdCfg = Game.GetMod<ModActivity_RemoveAd>().GetRemoveAdCfg()
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_RemoveAd, openData);
    }
}
