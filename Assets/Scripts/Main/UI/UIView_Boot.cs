using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EBootViewState
{
    None,
    Common,
    Download,
}

public class UIView_Boot : MonoBehaviour
{
    public static UIView_Boot Ins;

    public GameObject UINode_Splash;
    public GameObject UINode_Login;
    public GameObject UINode_Progress;
    public GameObject UINode_DownloadProgress;

    public Slider UISlider_Progress;
    public Text UITxt_Progress;
    public Text UITxt_ProgressTip;
    public Slider UISlider_DownloadProgress;
    public Text UITxt_DownloadProgress;
    public Text UITxt_DownloadProgressTip;

    private EBootViewState bootViewState;

    private int targetValue;
    private float deltaValue;

    public static bool IsBootSuccess { get; set; } = false; //打开主界面视为启动成功

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        UISlider_Progress.value = 0;
        UISlider_Progress.minValue = 0;
        UISlider_Progress.maxValue = 100;
        UISlider_DownloadProgress.value = 0;
        UISlider_DownloadProgress.minValue = 0;
        UISlider_DownloadProgress.maxValue = 100;
    }

    /// <summary>
    /// 显示闪屏
    /// </summary>
    public async UniTask ShowSplash(float time = 0.5f)
    {
        UINode_Splash.SetActive(true);
        UINode_Login.SetActive(false);
        int timeInt = (int)(time * 1000);
        await Task.Delay(timeInt);
        UINode_Splash.SetActive(false);
        UINode_Login.SetActive(true);
    }

    private void Update()
    {
        if (UISlider_Progress.value >= 100)
            return;

        if (UISlider_Progress.value < targetValue)
        {
            float showValue = UISlider_Progress.value + deltaValue;
            showValue = Mathf.Clamp(showValue, 0, targetValue);
            UISlider_Progress.value = showValue;
            UITxt_Progress.text = GameConfig.GetLocaleStr("UI_loading_load", (int)showValue);
        }
    }

    public async UniTask CheckBootSuccess()
    {
        while (UISlider_Progress.value < 100)
        {
            await UniTask.Yield();
        }
    }

    public void SetTargetValue(int targetValue, float deltaValue = 1f)
    {
        this.targetValue = targetValue;
        this.deltaValue = deltaValue;
    }

    /// <summary>
    /// 设置界面状态
    /// </summary>
    public void SetViewState(EBootViewState bootViewState)
    {
        UINode_Progress.gameObject.SetActive(false);
        UINode_DownloadProgress.gameObject.SetActive(false);
        switch (bootViewState)
        {
            case EBootViewState.Common:
                UINode_Progress.gameObject.SetActive(true);
                UINode_DownloadProgress.gameObject.SetActive(false);
                break;

            case EBootViewState.Download:
                UINode_Progress.gameObject.SetActive(false);
                UINode_DownloadProgress.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateDownloadProgress(float value, string progressStr = "")
    {
        UISlider_DownloadProgress.value = value * 100;
        UITxt_DownloadProgress.text = progressStr;
    }
}