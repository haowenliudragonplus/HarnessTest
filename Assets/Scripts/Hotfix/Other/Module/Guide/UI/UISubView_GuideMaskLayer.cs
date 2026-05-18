using System;
using DG.Tweening;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISubView_GuideMaskLayer : UISubView_GuideMaskLayerBase
{
    private Table_Common_Guide _config;

    private RectTransform _mask;
    private Image _screenMask;
    private RectTransform _focusShapeCircle;
    private RectTransform _focusShapeSquare;
    private RectTransform _squareShapeEdge;
    private RectTransform _circleShapeEdge;

    private RectTransform _focusShapeGrid1;
    private RectTransform _focusShapeGrid2;
    private RectTransform _focusShapeGrid3;

    private Transform[] _targets;
    private RectTransform[] _focusShapeGrids;
    private Vector2 _sizeDelta;
    private PointerEventCustomHandler _pointerEventCustomHandler;

    private Image maskImg;

    private bool isComplete;

    protected override void OnCreate()
    {
        base.OnCreate();
        _mask = GO.transform.Find("Mask").GetComponent<RectTransform>();
        _screenMask = GO.transform.Find("Mask/Screen").GetComponent<Image>();
        _focusShapeCircle = GO.transform.Find("Mask/ShapeCircle").GetComponent<RectTransform>();
        _focusShapeSquare = GO.transform.Find("Mask/ShapeSquare").GetComponent<RectTransform>();
        _squareShapeEdge = GO.transform.Find("SquareShapeEdge").GetComponent<RectTransform>();
        _circleShapeEdge = GO.transform.Find("CircleShapeEdge").GetComponent<RectTransform>();
        _focusShapeGrid1 = GO.transform.Find("Mask/Grid1").GetComponent<RectTransform>();
        _focusShapeGrid2 = GO.transform.Find("Mask/Grid2").GetComponent<RectTransform>();
        _focusShapeGrid3 = GO.transform.Find("Mask/Grid3").GetComponent<RectTransform>();
        maskImg = GO.transform.Find("Mask").GetComponent<Image>();
    }

    public void OnInit(Table_Common_Guide config)
    {
        _config = config;
        if (_config == null)
            return;
        GO.SetActive(true);
        _focusShapeGrids = new[] { _focusShapeGrid1, _focusShapeGrid2, _focusShapeGrid3 };
        InitializeMask(_config);
        isComplete = false;
        // _pointerEventCustomHandler = _mask.Bind<PointerEventCustomHandler>(true);
        //     
        // _pointerEventCustomHandler.BindingPointerDown(OnPointerDown);
        // _pointerEventCustomHandler.BindingPointerClick(OnPointerClick);


        DoGuideMaskFadeIn();
        //enable
        if (_config.ArrowEnable == null) return;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void OnShowCompleteTip()
    {
        isComplete = true;
        maskImg.DOFade(0, 0.1f);
        for (int i = 0; i < _focusShapeGrids.Length; i++)
        {
            _focusShapeGrids[i].gameObject.SetActive(false);
        }
    }

    protected override void OnUpdate()
    {
        if (_config == null) return;
        if (isComplete) return;
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerClick(Input.mousePosition);
        }

        UpdateShapePos();
    }

    public void DoGuideMaskFadeIn()
    {
        _screenMask.color = new Color(0, 0, 0, 0);
        _screenMask.DOFade(_config.MaskColor, 0.2f);

        if (_circleShapeEdge.gameObject.activeSelf)
        {
            var image = _circleShapeEdge.GetComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
            image.DOFade(0.8f, 0.2f);
        }

        if (_squareShapeEdge.gameObject.activeSelf)
        {
            var image = _squareShapeEdge.GetComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
            image.DOFade(0.8f, 0.2f);
        }
    }

    public void InitializeMask(Table_Common_Guide guideInfo)
    {
        _focusShapeGrid1.gameObject.SetActive(false);
        _focusShapeGrid2.gameObject.SetActive(false);
        _focusShapeGrid3.gameObject.SetActive(false);

        _focusShapeSquare.gameObject.SetActive(false);
        _focusShapeCircle.gameObject.SetActive(false);

        _squareShapeEdge.gameObject.SetActive(false);
        _circleShapeEdge.gameObject.SetActive(false);

        _mask.GetComponent<Image>().raycastTarget=guideInfo.MaskRaycastEnable;

        if (!guideInfo.MaskEnable)
        {
            GO.SetActive(false);
        }
        else if (guideInfo.MaskShape == 1)
        {
            _focusShapeCircle.gameObject.SetActive(true);
            _circleShapeEdge.gameObject.SetActive(true);
        }
        else if (guideInfo.MaskShape == 3)
        {
            _focusShapeSquare.gameObject.SetActive(true);
            _squareShapeEdge.gameObject.SetActive(true);
        }

        if (guideInfo.TargetTypes != null)
        {
            if (guideInfo.TargetParams != null)
            {
                _targets = new Transform[guideInfo.TargetParams.Count];
            }
            else
            {
                _targets = new Transform[1];
            }

            for (int i = 0; i < _targets.Length; i++)
            {
                var param = guideInfo.TargetParams != null ? guideInfo.TargetParams[i] : string.Empty;
                _targets[i] = Game.GetMod<GuideSys>()
                    .GetTarget((GuideTargetType)guideInfo.TargetTypes[i], param);
            }
        }
        else
        {
            _targets = null;
        }

        UpdateShapeSizeAndPos();
    }

    private Vector2 GetTargetLocalPos(Transform target)
    {
        Vector3 screenPoint = Vector3.zero;
        if (target is RectTransform)
            screenPoint = Game.GetMod<ModUI>().UICamera.WorldToScreenPoint(target.position);
        else
        {
            // if (Game.GetMod<ModFsm>().CurState is FsmState_FindTM fsm)
            // {
            //     screenPoint = fsm.findTMMode.LevelElementCamera.WorldToScreenPoint(target.position);
            // }
            // else
            {
                if (Camera.main != null)
                {
                    screenPoint = Camera.main.WorldToScreenPoint(target.position);
                }
            }
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_mask, screenPoint, Game.GetMod<ModUI>().UICamera,
            out var targetPoint);
        return targetPoint;
    }

    private void OnPointerDown()
    {
        if ((ClickToFinishType)_config.ClickToFinishType == ClickToFinishType.AnyWhere)
        {
            Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.None);
        }
    }

    public void OnPointerClick(Vector3 pos)
    {
        if (_targets == null)
            return;
        if ((ClickToFinishType)_config.ClickToFinishType == ClickToFinishType.Mask ||
            (ClickToFinishType)_config.ClickToFinishType == ClickToFinishType.Click3DTargetFinish)
        {
            if (_targets != null)
            {
                foreach (var target in _targets)
                {
                    if (target == null)
                    {
                        Game.GetMod<GuideSys>().FinishCurrent((GuideTargetType)_config.TargetTypes[0]);
                        return;
                    }

                    var clickInCircle = false;
                    if (target is RectTransform)
                    {
                        clickInCircle = RectTransformUtility.RectangleContainsScreenPoint((target as RectTransform),
                            pos, Game.GetMod<ModUI>().UICamera);
                    }
                    else
                    {
                        Vector2 centerScreenPos;
                        // if (Game.GetMod<ModFsm>().CurState is FsmState_FindTM fsm)
                        // {
                        //     centerScreenPos = fsm.findTMMode.LevelElementCamera.WorldToScreenPoint(target.position);
                        // }
                        // else
                        {
                            centerScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
                        }

                        if ((ClickToFinishType)_config.ClickToFinishType == ClickToFinishType.Mask)
                        {
                            clickInCircle = Vector2.Distance(pos, centerScreenPos) < _sizeDelta.x / 2;
                        }
                        else
                        {
                            var value = Vector2.Distance(pos, centerScreenPos);
                            clickInCircle = value < 80;
                        }
                    }

                    if (clickInCircle)
                    {
                        Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.None);
                        break;
                    }
                }
            }
        }
    }

    private void UpdateShapePos()
    {
        if (_targets == null)
            return;

        if (_config.MaskShape == 4)
            return;

        if (_config.MaskShape == 1)
        {
            if (_targets[0] == null)
                return;
            Vector2 targetPoint = GetTargetLocalPos(_targets[0]);
            _circleShapeEdge.anchoredPosition = targetPoint;
            _focusShapeCircle.anchoredPosition = targetPoint;
        }
        else if (_config.MaskShape == 3)
        {
            if (_targets[0] == null)
                return;
            Vector2 targetPoint = GetTargetLocalPos(_targets[0]);
            _squareShapeEdge.localPosition = targetPoint;
            _focusShapeSquare.localPosition = targetPoint;
        }
        else if (_config.MaskShape == 5)
        {
            var gridSize = Math.Min(3, _targets.Length);

            for (var i = 0; i < gridSize; i++)
            {
                _focusShapeGrids[i].localPosition = GetTargetLocalPos(_targets[i]);
                _focusShapeGrids[i].gameObject.SetActive(true);
            }
        }
    }

    private void UpdateShapeSizeAndPos()
    {
        if (_targets == null)
            return;

        if (_config.MaskShape == 4)
            return;

        var listShapeSize = _config.ShapeSize;

        if (_config.MaskShape == 1)
        {
            Vector2 sizeDelta;
            if (listShapeSize != null)
            {
                sizeDelta = new Vector2(listShapeSize[0], listShapeSize[1]);
            }
            else if (_targets[0] is RectTransform target)
            {
                sizeDelta = target.sizeDelta;
            }
            else
            {
                sizeDelta = new Vector2(326, 326);
            }

            _sizeDelta = sizeDelta;
            _circleShapeEdge.sizeDelta = sizeDelta;
            _focusShapeCircle.sizeDelta = sizeDelta;


            Vector2 targetPoint = GetTargetLocalPos(_targets[0]);
            _circleShapeEdge.anchoredPosition = targetPoint;
            _focusShapeCircle.anchoredPosition = targetPoint;
        }
        else if (_config.MaskShape == 3)
        {
            Vector2 sizeDelta;
            if (listShapeSize != null)
            {
                sizeDelta = new Vector2(listShapeSize[0], listShapeSize[1]);
            }
            else if (_targets[0] is RectTransform target)
            {
                sizeDelta = target.sizeDelta;
            }
            else
            {
                sizeDelta = new Vector2(178, 178);
            }

            _sizeDelta = sizeDelta;
            _squareShapeEdge.sizeDelta = sizeDelta;
            _focusShapeSquare.sizeDelta = sizeDelta;

            Vector2 targetPoint = GetTargetLocalPos(_targets[0]);
            _squareShapeEdge.localPosition = targetPoint;
            _focusShapeSquare.localPosition = targetPoint;
        }
        else if (_config.MaskShape == 5)
        {
            var gridSize = Math.Min(3, _targets.Length);

            for (var i = 0; i < gridSize; i++)
            {
                _focusShapeGrids[i].localPosition = GetTargetLocalPos(_targets[i]);
                _focusShapeGrids[i].gameObject.SetActive(true);
            }

            _sizeDelta = new Vector2(100, 100);
        }
    }
}