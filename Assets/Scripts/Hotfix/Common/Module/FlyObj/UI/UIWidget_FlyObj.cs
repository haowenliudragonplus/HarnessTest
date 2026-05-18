using DG.Tweening;
using UnityEngine;

public class UIWidget_FlyObj : UIWidget_FlyObjBase
{
    public class OpenData
    {
        public FlyObjData flyObjData;
        public float spawnTime;
    }

    private OpenData openData;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        RectTransform.localPosition = openData.flyObjData.fromUIPos;
        RectTransform.DOLocalMove(GetRandomPos(openData.flyObjData.fromUIPos), openData.spawnTime);
    }

    private Vector3 GetRandomPos(Vector3 uiPos)
    {
        int offset = 100;
        float randomX = Random.Range(-offset, offset);
        float randomY = Random.Range(-offset, offset);
        float targetPosX = uiPos.x + randomX;
        float targetPosY = uiPos.y + randomY;
        Vector3 pos = new Vector3(targetPosX, targetPosY, 0);
        return pos;
    }
}