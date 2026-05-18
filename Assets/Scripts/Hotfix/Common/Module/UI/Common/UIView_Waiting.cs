using Framework;
using UnityEngine;

/// <summary>
/// 转圈等待界面
/// </summary>
public class UIView_Waiting : UIView_WaitingBase
{
    public class OpenData
    {
        public float closeTime;
        public float delayTime;
    }

    private float timer_close;
    private float timer_delay;
    private bool isShow;
    private OpenData openData;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenData;
        isShow = openData.delayTime == 0;
        viewAniType = EViewAniType.NoAni;
        playSound = false;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        timer_close = 0;
        timer_delay = 0;
        isShow = openData.delayTime == 0;
        if (openData.delayTime > 0)
        {
            UIImg_Bg.gameObject.SetActive(false);
            UIImg_Icon.gameObject.SetActive(false);
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (isShow)
        {
            timer_close += Time.deltaTime;
            UIImg_Icon.gameObject.transform.Rotate(Vector3.forward, -Time.deltaTime * 200);
            if (timer_close >= openData.closeTime)
            {
                Close(false);
            }
        }
        else
        {
            timer_delay += Time.deltaTime;
            if (timer_delay >= openData.delayTime)
            {
                UIImg_Bg.gameObject.SetActive(true);
                UIImg_Icon.gameObject.SetActive(true);
                isShow = true;
            }
        }
    }
}