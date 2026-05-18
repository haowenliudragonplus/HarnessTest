using DG.Tweening;
using DragonPlus.Config.Common;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_GuideNPCText_Top : UISubView_GuideNPCText_TopBase
{
    private Tween _tween;
    public void OnInit(Table_Common_Guide guideData)
    {
        if (guideData == null)
            return;
        //enable
        GO.SetActive(guideData.NPCTextEnable&&guideData.TextType == "1");
        if (!guideData.NPCTextEnable||
            guideData.TextType != "1") return;
        //text
        if (!string.IsNullOrEmpty(guideData.TextGuide))
        {
            UITxt_NormalText.SetText(CoreUtils.GetLocalization(guideData.TextGuide));
            UITxt_TextWithAvatar.SetText(CoreUtils.GetLocalization(guideData.TextGuide));
            UINode_AvatarGuideText.gameObject.SetActive(true);
        }

        // if (!string.IsNullOrEmpty(guideData.NPCTextAvatar))
        // {
        //     UITxt_NormalText.SetText(CoreUtils.GetLocalization(guideData.NPCTextAvatar));
        //     UITxt_TextWithAvatar.SetText(CoreUtils.GetLocalization(guideData.NPCTextAvatar));
        //     UINode_AvatarIcon.gameObject.SetActive(true);
        //     UINode_AvatarGuideText.gameObject.SetActive(true);
        // }

        LayoutRebuilder.ForceRebuildLayoutImmediate(UINode_AvatarGuideText.transform.Find("BG")
            .GetComponent<RectTransform>());
        //pos
        float height = Game.GetMod<ModUI>().UICanvas.transform.GetComponent<RectTransform>().sizeDelta.y;
        float maxDis = height * 0.5f - GO.GetComponent<RectTransform>().sizeDelta.y * 1.5f;
        var npcPos = (GuideNpcPos)guideData.NPCTextPos;
        var vec1 = Vector3.zero;
        switch (npcPos)
        {
            case GuideNpcPos.Top:
                vec1 = new Vector3(0.0f, maxDis*0.8f, 0.0f);
                break;
            case GuideNpcPos.TopDownLittle:
                vec1 = new Vector3(0.0f, maxDis * 0.8f, 0.0f);
                break;
            case GuideNpcPos.TopMiddle:
                vec1 = new Vector3(0.0f, maxDis * 0.5f, 0.0f);
                break;
            case GuideNpcPos.Middle:
                vec1 = new Vector3(0.0f, 0.0f, 0.0f);
                break;

            case GuideNpcPos.MiddleButtom:
                vec1 = new Vector3(0.0f, -maxDis * 0.5f, 0.0f);
                break;
            case GuideNpcPos.Buttom:
                vec1 = new Vector3(0.0f, -maxDis, 0.0f);
                break;
            case GuideNpcPos.MiddleButtomButtom:
                vec1 = new Vector3(0.0f, -maxDis * 0.75f, 0.0f);
                break;
        }
        GO.transform.localPosition = vec1+new Vector3(0.0f,guideData.TextMove,0);
        if (guideData.TextMove>0)
        {
            _tween?.Kill();
            _tween = GO.transform.DOLocalMoveY(vec1.y, guideData.TextMoveTime/1000f);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _tween?.Kill();
    }


    public void OnShowCompleteTip(Table_Common_Guide guideData)
    {
        if (guideData == null) return;
        GO.SetActive(guideData.NPCTextEnable&&guideData.TextType == "1");
        if (!guideData.NPCTextEnable||
            guideData.TextType != "1") return;
        if (!string.IsNullOrEmpty(guideData.GuideCompleteTipDes))
        {
            UITxt_NormalText.SetText(CoreUtils.GetLocalization(guideData.GuideCompleteTipDes));
            UITxt_TextWithAvatar.SetText(CoreUtils.GetLocalization(guideData.GuideCompleteTipDes));
        }
        //pos
        float height = Game.GetMod<ModUI>().UICanvas.transform.GetComponent<RectTransform>().sizeDelta.y;
        float maxDis = height * 0.5f - GO.GetComponent<RectTransform>().sizeDelta.y * 1.5f;
        var npcPos = (GuideNpcPos)guideData.GuideCompleteTipPos;
        switch (npcPos)
        {
            case GuideNpcPos.Top:
                GO.transform.localPosition = new Vector3(0.0f, maxDis, 0.0f);
                break;
            case GuideNpcPos.TopDownLittle:
                GO.transform.localPosition = new Vector3(0.0f, maxDis * 0.8f, 0.0f);
                break;
            case GuideNpcPos.TopMiddle:
                GO.transform.localPosition = new Vector3(0.0f, maxDis * 0.5f, 0.0f);
                break;
            case GuideNpcPos.Middle:
                GO.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;

            case GuideNpcPos.MiddleButtom:
                GO.transform.localPosition = new Vector3(0.0f, -maxDis * 0.5f, 0.0f);
                break;
            case GuideNpcPos.Buttom:
                GO.transform.localPosition = new Vector3(0.0f, -maxDis, 0.0f);
                break;
            case GuideNpcPos.MiddleButtomButtom:
                GO.transform.localPosition = new Vector3(0.0f, -maxDis * 0.75f, 0.0f);
                break;
        }
    }
}
