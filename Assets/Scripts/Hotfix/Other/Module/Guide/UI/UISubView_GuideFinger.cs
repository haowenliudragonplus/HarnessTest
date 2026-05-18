using DG.Tweening;
using DragonPlus.Config.Common;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_GuideFinger : UISubView_GuideFingerBase
{
    private Table_Common_Guide _config;

    private Vector3 _targetInitPos;
    private Sequence _tween;
    private Transform finger;
    private Transform fingerBody;

    private Vector3[] _tempWorldCorners = new Vector3[4];

    private bool isInFingerMoveCD;
    private float fignerMoveCDTimer;
    private float fignerMoveCD = 2f;
    private bool isComplete;
    protected override void OnCreate()
    {
        _tween?.Kill();
        _tween = null;
        finger = GO.transform.Find("UINode_DragHandNode");
        fingerBody = this.finger.Find("Hand");
        UINode_DragHandNode = GO.transform.Find("UINode_DragHandNode").GetComponent<RectTransform>();
        UINode_DragArrowNode = GO.transform.Find("UINode_DragArrowNode").GetComponent<RectTransform>();
        UINode_DragHandNode1 = GO.transform.Find("UINode_DragHandNode1").GetComponent<RectTransform>();
        UINode_DragDoubleHandNode = GO.transform.Find("UINode_DragDoubleHandNode").GetComponent<RectTransform>();
        isInFingerMoveCD = false;
        fignerMoveCDTimer = 0;
        //EnableUpdate(2);
        GO.SetActive(false);
    }

    public void OnInit(Table_Common_Guide config)
    {
        _config = config;

        if (_config == null)
            return;
        //enable
        GO.SetActive(_config.FingerEnable != 0);
        if (_config.FingerEnable == 0) return;
        fignerMoveCD = _config.FingerMoveCD;
        isComplete = false;
        fingerBody.localScale = _config.FingerRotate ? new Vector3(-1, 1, 1) : Vector3.one;
        UINode_DragHandNode.gameObject.SetActive(_config.FingerEnable == 1);
        UINode_DragArrowNode.gameObject.SetActive(_config.FingerEnable == 2);
        UINode_DragHandNode1.gameObject.SetActive(_config.FingerEnable == 3);
        UINode_DragDoubleHandNode.gameObject.SetActive(_config.FingerEnable == 4);
        SetTarget();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _tween?.Kill();
    }

    public void OnShowCompleteTip()
    {
        _tween?.Kill();
        isComplete = true;
        GO.SetActive(false);
    }

    private bool IsTouch()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0);
#else
            return Input.touchCount > 0;
#endif
    }

    private bool IsDragTarget()
    {
        // bool touchGuideTarget = MergeWorld.Instance.MergeControllerIns.IsTouchGuideTarget();
        // return IsTouch()&&touchGuideTarget;
        return false;
    }

    protected override void OnUpdate()
    {
        if (isComplete) return;
        if (_config == null) return;
        if (_config.FingerEnable == 0) return;
        if (_config.FingerMoveType == 1)
        {
            var isTouch = IsDragTarget();
            if (isTouch)
            {
                if (finger.gameObject.activeSelf)
                {
                    finger.gameObject.SetActive(false);
                    if (_tween != null)
                    {
                        _tween.Kill();
                        _tween = null;
                        isInFingerMoveCD = true;
                    }
                }
                return;
            }


            // if (finger.gameObject.activeSelf != !isTouch)
            // {
            //     finger.gameObject.SetActive(!isTouch);
            //     if (_tween != null)
            //     {
            //         _tween.Kill();
            //         _tween = null;
            //         isInFingerMoveCD = true;
            //     }
            // }

            if (isInFingerMoveCD)
            {
                fignerMoveCDTimer += Time.deltaTime;
                if (fignerMoveCDTimer >= fignerMoveCD)
                {
                    fignerMoveCDTimer = 0;
                    isInFingerMoveCD = false;
                }
            }
            if (isInFingerMoveCD) return;

            if (_config.TargetTypes.Count > 1 && _tween == null)
            {
                var startPos = GetTargetPos(0);
                var endPos = GetTargetPos(1);
                if (_tween == null)
                {
                    GO.transform.GetComponent<CanvasGroup>().alpha = 0;
                    GO.transform.position = startPos;
                    finger.gameObject.SetActive(true);
                    // var skeletonGraphic = finger.GetComponentInChildren<SkeletonGraphic>();
                    // skeletonGraphic.AnimationState.SetAnimation(0, "animation2", true);
                    // _tween?.Kill();
                    // _tween = DOTween.Sequence();
                    // _tween.AppendInterval(0.5f);
                    // _tween.AppendCallback(() =>
                    // {
                    //     GO.transform.position = startPos;
                    //     GO.transform.GetComponent<CanvasGroup>().alpha = 0;
                    // });
                    // _tween.Append(GO.transform.GetComponent<CanvasGroup>().DOFade(1,0.2f));
                    // _tween.Append(GO.transform.DOMove(endPos, 0.667f));
                    // _tween.Append(GO.transform.GetComponent<CanvasGroup>().DOFade(0,0.2f));
                    // _tween.AppendInterval(0.3333f);
                    // _tween.SetLoops(int.MaxValue);
                }
            }
        }
        else
        {
            var target = GetTarget();
            if (null == target) return;
            var targetMoved = _targetInitPos != GetTargetPos(0);
            if (targetMoved)
                SetTarget();
            // if (_tween == null)
            // {
            //     GO.transform.localScale = Vector3.one;
            //     _tween = DOTween.Sequence();
            //     _tween.Append(GO.transform.DOScale(0.8f, 0.5f));
            //     _tween.SetLoops(int.MaxValue,LoopType.Yoyo);
            // }

        }
    }

    private Vector3 GetTargetPos(int index)
    {
        var target = GetTarget(index);
        var targetWorldPos = Vector3.zero;

        if (target is RectTransform)
        {
            var targetRect = target as RectTransform;
            targetRect.GetWorldCorners(_tempWorldCorners);
            targetWorldPos = (_tempWorldCorners[0] + _tempWorldCorners[2]) / 2f;
        }
        else
        {
            targetWorldPos = target.transform.position;
            // RectTransformUtility.WorldToScreenPoint(UIModule.Instance.WorldCamera, worldCenter);
            // if (Game.GetMod<ModFsm>().CurState is FsmState_FindTM fsm)
            // {
            //     
            //     targetWorldPos = fsm.findTMMode.GuideCamera.WorldToScreenPoint(target.position);
            // }
            // else
            {
                if (Camera.main != null) { targetWorldPos = Camera.main.WorldToScreenPoint(target.position); }
            }
            targetWorldPos = Game.GetMod<ModUI>().UICamera.ScreenToWorldPoint(targetWorldPos);
            targetWorldPos.z = 0;
        }

        return targetWorldPos;
    }

    private void SetTarget()
    {
        var target = GetTarget();
        _targetInitPos = GetTargetPos(0);

        var worldCenter = Vector3.zero;

        if (target is RectTransform)
        {
            var targetRect = target as RectTransform;
            targetRect.GetWorldCorners(_tempWorldCorners);
            worldCenter = (_tempWorldCorners[0] + _tempWorldCorners[2]) / 2f;
        }
        else
        {
            worldCenter = target.transform.position;
            Camera camera = null;
            if (Game.GetMod<ModFsm>().CurState is FsmState_InGame)
            {
                var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
                switch ((GuideTargetType)_config.TargetTypes[0])
                {
                    case GuideTargetType.ClickArrowEntity:
                    case GuideTargetType.ZoomCamera:
                        camera = mode.ElementCamera;
                        break;

                    case GuideTargetType.ClickBox:
                        camera = mode.TopAreaCamera;
                        break;

                    case GuideTargetType.ClickItem:
                        camera = Game.GetMod<ModUI>().UICamera;
                        break;
                }
                if (camera != null)
                {
                    worldCenter = camera.WorldToScreenPoint(target.position);
                }
            }
            else
            {
                //if (Camera.main != null) {worldCenter = Camera.main.WorldToScreenPoint(target.position);}
            }
            worldCenter = Game.GetMod<ModUI>().UICamera.ScreenToWorldPoint(worldCenter);
            worldCenter.z = 0;
        }

        GO.transform.position = worldCenter;

        if (_config.Id <= 17 && _config.Id != 11)
        {
            var localPosition = GO.transform.localPosition;
            GO.transform.localPosition = new Vector3(localPosition.x, localPosition.y + 30, localPosition.z);
        }

        if (_config.FingerPos != null)
        {
            var localPosition = GO.transform.localPosition;
            Vector2 pos = Vector2.zero;
            if (_config.FingerPos.Count > 0)
            {
                int index1 = int.Parse(_config.FingerPos[0]);
                pos.x += index1;
            }
            if (_config.FingerPos.Count > 1)
            {
                int index2 = int.Parse(_config.FingerPos[1]);
                pos.y += index2;
            }
            GO.transform.localPosition = new Vector3(localPosition.x + pos.x, localPosition.y + pos.y, localPosition.z);
        }


        GO.transform.localScale = Vector3.one;

        // if (_tween == null && _config.FingerMoveType == 0)
        //     _tween = rectTransform.DOScale(0.8f, 0.5f).SetLoops(int.MaxValue, LoopType.Yoyo);
    }

    private void setTarget()
    {
        var target = GetTarget();
        _targetInitPos = target.position;

        var worldCenter = Vector3.zero;

        if (target is RectTransform)
        {
            var targetRect = target as RectTransform;
            targetRect.GetWorldCorners(_tempWorldCorners);
            worldCenter = (_tempWorldCorners[0] + _tempWorldCorners[2]) / 2f;
        }
        else
        {
            worldCenter = target.transform.position;
            RectTransformUtility.WorldToScreenPoint(Camera.main, worldCenter);
            worldCenter = Camera.main.WorldToScreenPoint(worldCenter);
            worldCenter = Game.GetMod<ModUI>().UICamera.ScreenToWorldPoint(worldCenter);
            worldCenter.z = 0;
        }

        GO.transform.position = worldCenter;

        if (_config.Id <= 17 && _config.Id != 11)
        {
            var localPosition = GO.transform.localPosition;
            GO.transform.localPosition = new Vector3(localPosition.x, localPosition.y + 30, localPosition.z);
        }

        GO.transform.localScale = Vector3.one;

    }

    private Transform GetTarget(int index = 0)
    {
        if (_config.TargetTypes == null || _config.TargetTypes.Count < index + 1) return null;
        var targetParam = _config.TargetParams == null ? string.Empty : _config.TargetParams[index];
        var target = Game.GetMod<GuideSys>()
            .GetTarget((GuideTargetType)_config.TargetTypes[index], targetParam);
        return target;
    }
}