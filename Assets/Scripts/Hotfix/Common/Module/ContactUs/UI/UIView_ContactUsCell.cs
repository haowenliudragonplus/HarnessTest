using DragonPlus;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine.UI;

public class UIView_ContactUsCell : UIView_ContactUsCellBase
{
    private ContactUsFaqConfig config;
    private string localeKey;

    protected override void OnCreate()
    {
        base.OnCreate();
        localeKey = Game.GetMod<ModLanguage>().CurLanguage;
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        GO.GetComponent<Button>().onClick.AddListener(OnClickQuestion);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        GO.GetComponent<Button>().onClick.RemoveListener(OnClickQuestion);
    }

    public void InitData(ContactUsFaqConfig data)
    {
        config = data;

        ContactUsI18nConfig i18nConfig;
        if (Game.GetMod<FaqSys>().I18nDic.TryGetValue(config.Question, out i18nConfig))
        {
            string strValue = i18nConfig.I18nDic[localeKey];
            UIOldTxt_Text.text = strValue;
        }
    }

    public void OnClickQuestion()
    {
        Game.GetMod<ModEvent>().Dispatch(new EvtFaqSelectQuestion(config.Id));
           
        var cMessage = new CAutoSendUserComplainMessage
        {
            Id = (uint) config.Id,
            Locale = localeKey
        };

        SDK<INetwork>.Instance.HandleRequest(cMessage, (SAutoSendUserComplainMessage sGetConfig) =>
            {
                uint succId = sGetConfig.Id;
                ContactUsFaqConfig succConfig = Game.GetMod<FaqSys>().FaqDic[(int) succId];
                if (succConfig.Successor != 0)
                {
                    Game.GetMod<ModEvent>().Dispatch(new EvtFaqQuestionServerBack(succConfig.Successor));
                }
            },
            (errno, errmsg, resp) =>
            {
                Log.Error("CAutoSendUserComplainMessage erro code = {0} message = {1}", errno, errmsg);
            });
    }
}
