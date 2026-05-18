using System;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Launcher;
using Main;
using UnityEngine.UI;
using UnityEngine;

public class UIView_PrivacyPolicy : MonoBehaviour
{
    public Button UIBtn_PrivacyPolicy;
    public Button UIBtn_ServicePolicy;
    public Text UITxt_PrivacyPolicy;
    public Text UITxt_ServicePolicy;
    public Text UITxt_Title;
    public Button UIBtn_Agree;
    public Text UITxt_AgreeBtnName;
    public Text UITxt_Des;

    private static bool showPrivacyPolicy;

    public static async UniTask CheckShow()
    {
        bool isAgreedGDPR = PlayerPrefs.HasKey("GDPR");
        if (!isAgreedGDPR)
        {
            showPrivacyPolicy = true;
            Show();
            while (showPrivacyPolicy)
            {
                await UniTask.Yield();
            }
        }
    }

    public static void Show()
    {
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFtePrivacyPop);
        GameObject viewGo = Instantiate(Resources.Load<GameObject>(Const_Boot.PrefabPath_PrivacyPolicyView));
        viewGo.transform.SetParent(GameObject.Find(Const_Boot.ScenePath_UICanvas).transform, false);
        var view = viewGo.GetComponent<UIView_PrivacyPolicy>();
        view.UITxt_Title.text = GameConfig.GetLocaleStr("UI_PrivacyPolicy_3");
        view.UITxt_PrivacyPolicy.text = GameConfig.GetLocaleStr("UI_PrivacyPolicy_3");
        view.UITxt_ServicePolicy.text = GameConfig.GetLocaleStr("UI_PrivacyPolicy_5");
        view.UITxt_AgreeBtnName.text = GameConfig.GetLocaleStr("UI_privacy_btn_agree_title_native");
        view.UITxt_Des.text = GameConfig.GetLocaleStr("UI_PrivacyPolicy_2");
        view.UIBtn_PrivacyPolicy.onClick.AddListener(() => { GameUtils.OpenURL(ConfigurationController.Instance.PrivacyPolicyURL); });
        view.UIBtn_ServicePolicy.onClick.AddListener(() => { GameUtils.OpenURL(ConfigurationController.Instance.TermsOfServiceURL); });
        view.UIBtn_Agree.onClick.AddListener(() =>
        {
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFtePrivacyAccept);
            PlayerPrefs.SetInt("GDPR", 1);
            showPrivacyPolicy = false;
            Destroy(viewGo);
        });
    }
}