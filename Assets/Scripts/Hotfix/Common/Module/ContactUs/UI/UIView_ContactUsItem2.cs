using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UIView_ContactUsItem2 : UIView_ContactUsItem2Base
{
    private UIView_ContactUs uiContactUs;
    UserComplainMessage ItemData { get; set; }
    private RectTransform tailImageTSF;
    private RectTransform messageBGTSF;
    //[UIBinder("Image/Image/Text")] private RectTransform messageTSF;
    // [UIBinder("Image/Image/Text")] private ContentSizeFitter messageSizeFitter;

    protected override void BindComponent()
    {
        base.BindComponent();
        tailImageTSF = GO.transform.Find("HeadImage/Image").GetComponent<RectTransform>();
        messageBGTSF = GO.transform.Find("UILayooutV_Image/UILayooutV_Image").GetComponent<RectTransform>();
        // messageTSF = go.transform.Find("UILayooutV_Image/UILayooutV_Image").GetComponent<RectTransform>();
    }

    public void SetData(UIView_ContactUs inUIContactUs, UserComplainMessage itemData)
    {
        uiContactUs = inUIContactUs;
        ItemData = itemData;
        ReloadFromData();
    }

    public void ReloadFromData()
    {
        UIOldTxt_Text.text = ItemData.Message;
        UIOldTxt_TimeText.text = SDKUtil.TimeDate.GetTimeStampDateString(ItemData.CreatedAt);
        UpdateMessageView();
        uiContactUs.GotoScrollViewEnd(0.5f).Forget();
    }

    private void UpdateMessageView()
    {
        // LayoutRebuilder.ForceRebuildLayoutImmediate(messageTSF);
        // LayoutRebuilder.ForceRebuildLayoutImmediate(messageBGTSF);
        // if (messageTSF.sizeDelta.x > 410f)
        // {
        //     messageSizeFitter.enabled = false;
        //     messageTSF.sizeDelta = new Vector2(410f, messageTSF.sizeDelta.y);
        // }
        // else
        // {
        //     messageSizeFitter.enabled = true;
        // }

        LayoutRebuilder.ForceRebuildLayoutImmediate(UIOldTxt_Text.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(tailImageTSF);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GO.GetComponent<RectTransform>());
    }
}