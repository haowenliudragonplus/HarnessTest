using DG.Tweening;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_GuideArrow : UISubView_GuideArrowBase
{
    private const string TARGET_ARROW_ROOT = "TARGET_ARROW_ROOT";

    private Table_Common_Guide _config;

    private Vector3 _targetInitPos;
    private Tween[] _tweens;

    protected override void OnCreate()
    {
        base.OnCreate();
       

      //  EnableUpdate(2);
    }

    public void OnInit(Table_Common_Guide config)
    {
        _config = config;
        if (_config == null)
            return;
        GO.SetActive(false);
        //enable
        if (_config.ArrowEnable == null) return;
        SetTarget();
    }
    
    public void OnShowCompleteTip()
    {
        if (_tweens != null)
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }
        }
        GO.SetActive(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_tweens != null)
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }
        }
    }

    protected override void OnUpdate()
    {
        if (_config == null || _config.ArrowEnable == null) return;
        if (_config.TargetTypes == null || _config.TargetTypes.Count == 0) return;
        var targetParam = _config.TargetParams == null
            ? string.Empty
            : _config.TargetParams[GetFirstEnableValidIndex()];
        var target = Game.GetMod<GuideSys>().GetTarget(
            (GuideTargetType)_config.TargetTypes[GetFirstEnableValidIndex()],
            targetParam);

        if (target == null)
            return;

        var targetMoved = _targetInitPos != target.position;

        if (targetMoved)
        {
            SetTarget();
        }
    }

    private void SetTarget()
    {
        var arrowRoot = MakeTargetRootAndClear();

        if (_config.TargetTypes == null) return;

        var targetCount = _config.TargetParams == null ? 1 : _config.TargetParams.Count;
        _tweens = new Tween[targetCount];
        for (int i = 0; i < targetCount; i++)
        {
            if (_config.ArrowEnable[i] != 1) continue;
            var targetParam = _config.TargetParams == null ? string.Empty : _config.TargetParams[i];
            var target = Game.GetMod<GuideSys>()
                .GetTarget((GuideTargetType)_config.TargetTypes[i], targetParam);
            if (i == GetFirstEnableValidIndex()) _targetInitPos = target.position;

            // 箭头设置
            if (target)
            {
                var tempArrow = GameObject.Instantiate(GO);
                tempArrow.transform.SetParent(arrowRoot.transform);
                tempArrow.transform.Reset();
                tempArrow.SetActive(true);

                switch ((GuideArrowPosition)_config.ArrowPos[i])
                {
                    case GuideArrowPosition.Left:
                    {
                        var delta = -1f;
                        tempArrow.transform.position = target.position + new Vector3(delta, 0, 0);
                        tempArrow.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                        HorizontalArrowAnimation(delta, tempArrow.transform, target, i);
                    }
                        break;
                    case GuideArrowPosition.Right:
                    {
                        var delta = 1f;
                        tempArrow.transform.position = target.position + new Vector3(delta, 0, 0f);
                        tempArrow.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
                        HorizontalArrowAnimation(delta, tempArrow.transform, target, i);
                    }
                        break;
                    case GuideArrowPosition.Top:
                    {
                        var delta = 1.5f;
                        tempArrow.transform.position = target.position + new Vector3(0f, delta, 0f);
                        tempArrow.transform.localRotation = Quaternion.identity;
                        VerticalAnimation(delta, tempArrow.transform, target, i);
                    }
                        break;
                    case GuideArrowPosition.Bottom:
                    {
                        var delta = -0.6f;
                        tempArrow.transform.position = target.position + new Vector3(0f, delta, 0f);
                        tempArrow.transform.localRotation = Quaternion.Euler(0f, 0f, 180);
                        VerticalAnimation(delta, tempArrow.transform, target, i);
                    }
                        break;
                }
            }
        }
    }

    private GameObject MakeTargetRootAndClear()
    {
        var arrowRoot = GO.transform.parent.Find(TARGET_ARROW_ROOT)?.gameObject;
        if (!arrowRoot)
        {
            arrowRoot = new GameObject("TARGET_ARROW_ROOT", typeof(RectTransform));
            arrowRoot.transform.SetParent(GO.transform.parent);
            arrowRoot.transform.Reset();
        }

        arrowRoot.RemoveAllChildren();

        return arrowRoot;
    }

    private int GetFirstEnableValidIndex()
    {
        int firstValidIndex = -1;
        for (int i = 0; i < _config.ArrowEnable.Count; i++)
        {
            if (_config.ArrowEnable[i] == 1)
            {
                firstValidIndex = i;
                break;
            }
        }

        return firstValidIndex;
    }

    private void VerticalAnimation(float delta, Transform arrow, Transform target, int index)
    {
        _tweens[index]?.Kill();
        _tweens[index] = DOTween.To(
            () => target.position.y + delta,
            y => { arrow.position = new Vector3(arrow.position.x, y, arrow.transform.position.z); },
            target.position.y + delta - 0.5f,
            0.5f).SetLoops(int.MaxValue, LoopType.Yoyo).SetEase(Ease.OutQuad);

        var fourCornersArray = new Vector3[4];
        _tweens[index].onUpdate = () =>
        {
            if (target is RectTransform)
            {
                (target as RectTransform).GetWorldCorners(fourCornersArray);
            }

            arrow.position = new Vector3((fourCornersArray[0] + fourCornersArray[2]).x / 2f,
                arrow.transform.position.y, arrow.position.z);
        };
    }

    private void HorizontalArrowAnimation(float delta, Transform arrow, Transform target, int index)
    {
        _tweens[index]?.Kill();
        _tweens[index] = DOTween.To(
            () => target.position.x + delta,
            x => { arrow.position = new Vector3(x, arrow.transform.position.y, arrow.position.z); },
            target.position.x + delta - 0.5f,
            0.5f).SetLoops(int.MaxValue, LoopType.Yoyo).SetEase(Ease.OutQuad);

        var fourCornersArray = new Vector3[4];
        _tweens[index].onUpdate = () =>
        {
            if (target is RectTransform)
            {
                (target as RectTransform).GetWorldCorners(fourCornersArray);
            }

            arrow.position = new Vector3(arrow.transform.position.x,
                (fourCornersArray[0] + fourCornersArray[2]).y / 2f, arrow.position.z);
        };
    }
}