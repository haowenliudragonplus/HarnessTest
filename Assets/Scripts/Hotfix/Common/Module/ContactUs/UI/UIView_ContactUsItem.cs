using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using DragonPlus;
using DragonPlus.Config;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_ContactUsItem : UIView_ContactUsItemBase
{
    private UIView_ContactUs uiContactUs;
    UserComplainMessage ItemData { get; set; }
    private RectTransform timeTSF;
    private RectTransform messageTSF;
    // [UIBinder("Image/Image/Text")] private ContentSizeFitter messageSizeFitter;
    protected override void BindComponent()
    {
        base.BindComponent();
        messageTSF = UIOldTxt_Text.GetComponent<RectTransform>();
    }
    
    public void SetData(UIView_ContactUs inUIContactUs, UserComplainMessage itemData)
    {
        uiContactUs = inUIContactUs;
        ItemData = itemData;
        
        UIOldTxt_Text.text =itemData.Message.ToString(); //CoreUtils.GetLocalization("UI_ support_2");
        ReloadFromData();
    }

    public async UniTaskVoid ReloadFromData()
    {
        string message = ItemData.Message;

        Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?)");
        MatchCollection mc = re.Matches(message);
        foreach (Match m in mc)
        {
            message = message.Replace(m.Result("${url}"), string.Format("<a href={0}>{0}</a>", m.Result("${url}")));
        }

        // MessageText.text = message;
        UIOldTxt_TimeText.text = SDKUtil.TimeDate.GetTimeStampDateString(ItemData.CreatedAt);

        uiContactUs.GotoScrollViewEnd(0.5f).Forget();

        await UniTask.DelayFrame(1,cancellationToken: GO.GetCancellationTokenOnDestroy());
        UpdateMessageView();
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        Game.GetMod<ModEvent>().Register<EvtFaqSelectQuestion>(ChangeSelectQuestion);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        Game.GetMod<ModEvent>().UnRegister<EvtFaqSelectQuestion>(ChangeSelectQuestion);
    }


    private void UpdateMessageView()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageTSF);
        if (messageTSF.sizeDelta.x > 410f)
        {
            //  messageSizeFitter.enabled = false;
            messageTSF.sizeDelta = new Vector2(410f, messageTSF.sizeDelta.y);
        }
        else
        {
            // messageSizeFitter.enabled = true;
        }

        // LayoutRebuilder.ForceRebuildLayoutImmediate(MessageText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(UINode_vImage.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(UINode__ContactGroup.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(GO.GetComponent<RectTransform>());
    }

    public void AddIOSQuestions(int id)
    {
        var qsList = Game.GetMod<FaqSys>().GetQuestions(id);

        for (int i = 0; i < qsList.Count; i++)
        {
            ContactUsFaqConfig config = qsList[i];
            var widget = OpenUIWidget<UIView_ContactUsCell>(UINode__ContactGroup, false);
            widget.InitData(config);
        }
    }

    public void ChangeSelectQuestion(EvtFaqSelectQuestion evt)
    {
        UINode__ContactGroup.gameObject.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GO.GetComponent<RectTransform>());
        uiContactUs.GotoScrollViewEnd(0.5f).Forget();
    }
}