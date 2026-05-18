using Framework;
using UnityEngine.UI;

public class UIView_PurchaseFailure : UIView_PurchaseFailureBase
{
    private UIView_Notice.OpenData uiData;
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        uiData = viewData as UIView_Notice.OpenData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        UITxt_HelpText.text = uiData.content;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_CloseButton.onClick.AddListener(() =>
        {
            Close();
        });
        UIBtn_PlayButton.onClick.AddListener(() =>
        {
            Close();
        });
    }
}
