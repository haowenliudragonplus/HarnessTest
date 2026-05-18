// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/18/14:58
// Ver : 1.0.0
// Description : TutorialMask.cs
// ChangeLog :
// **********************************************

using System;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonU3DSDK;
using Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TMGame
{
    public class TutorialMask : MonoBehaviour, ICanvasRaycastFilter, IPointerDownHandler, IPointerClickHandler
    {
        public Transform[] _targets;

        private Material _material;
        private float _current = 0f;
        private float _yVelocity = 0f;
        private Image _circleImage;
        private Image _rectangleImage;

        private float _width;
        private float _height;
        private Vector2 _center;
        private Vector2 _centerScreenPos;
        private bool _targetMoving;


        private Vector3[] _tempWorldCorners = new Vector3[4];

        private Canvas _canvas;
        private Camera _camera;
        private Table_Common_Guide _config;

        public Action<PointerEventData> OnMaskClick;


        private double _updateTargetDelta;

        void Awake()
        {
            _canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
            _camera = Game.GetMod<ModUI>().UICamera;
            _material = GetComponent<Image>().material;
            _circleImage = transform.Find("CircleShapeEdge").GetComponent<Image>();
            _rectangleImage = transform.Find("SquareShapeEdge").GetComponent<Image>();
        }

        public void SetConfig(Table_Common_Guide config)
        {
            _config = config;
            _updateTargetDelta = 1f;

            if (_config == null)
            {
                Log.Error("tutorial Mask setConfig config is null");
                return;
            }

            _targetMoving = true;

            CoreUtils.WaitSeconds(5, () => { _targetMoving = false; }).Forget();


            if (_config.TargetTypes != null)
            {
                if (_config.TargetParams != null)
                {
                    _targets = new Transform[_config.TargetParams.Count];
                }
                else
                {
                    _targets = new Transform[1];
                }

                for (int i = 0; i < _targets.Length; i++)
                {
                    var param = config.TargetParams != null ? config.TargetParams[i] : string.Empty;
                    _targets[i] = Game.GetMod<GuideSys>()
                        .GetTarget((GuideTargetType) config.TargetTypes[i], param);
                }
            }
            else
            {
                _targets = new Transform[0];
            }

            if (_config.MaskColor==0)
            {
                _material.color = Color.clear;
                _circleImage.gameObject.SetActive(false);
                _rectangleImage.gameObject.SetActive(false);
            }
            else
            {
                _material.color = Color.white;
                _circleImage.gameObject.SetActive((GuideMaskShape)_config.MaskShape != GuideMaskShape.Rectangle);
                _rectangleImage.gameObject.SetActive((GuideMaskShape)_config.MaskShape == GuideMaskShape.Rectangle);
            }

            updateMaterialSettings();
            UpdateHighShow();
        }

        private void updateTargetSizeAndPos()
        {
            //if (!_targetMoving) return;

            var worldCenter = Vector3.zero;
            if (_targets.Length > 0 && _targets[0] != null)
            {
                if (_targets[0] is RectTransform)
                {
                    var target = _targets[0] as RectTransform;
                    // var scale = Screen.width / 1334f;
                    _width = target.sizeDelta.x / 2f;
                    _height = target.sizeDelta.y / 2f;
                    if ((GuideMaskShape) _config.MaskShape == GuideMaskShape.Circle)
                    {
                        _width = Mathf.Max(_width, _height);
                        _height = _width;
                    }

                    target.GetWorldCorners(_tempWorldCorners);
                    worldCenter = (_tempWorldCorners[0] + _tempWorldCorners[2]) / 2f;
                    _centerScreenPos = _camera.WorldToScreenPoint(new Vector3(worldCenter.x, worldCenter.y, 0));
                }
                else
                {
                    _width = 100f;
                    _height = 100f;
                    worldCenter = _targets[0].transform.position;
                    _centerScreenPos =
                        RectTransformUtility.WorldToScreenPoint(Camera.main, worldCenter);
                    worldCenter = Camera.main.WorldToScreenPoint(worldCenter);
                    worldCenter = Game.GetMod<ModUI>().UICamera.ScreenToWorldPoint(worldCenter);
                    worldCenter.z = 0;
                }

                RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                    _centerScreenPos, _camera, out var v3);
                _center = v3;

                _current = 1.2f;
            }

            _circleImage.rectTransform.sizeDelta = new Vector2(_width * 2, _height * 2);
            _circleImage.transform.position = worldCenter;
            _circleImage.transform.localScale = Vector3.one * _current;

            _rectangleImage.rectTransform.sizeDelta = new Vector2(_width * 2, _height * 2);
            _rectangleImage.transform.position = worldCenter;
            _rectangleImage.transform.localScale = Vector3.one * _current;
        }

        private void updateMaterialSettings()
        {
            updateTargetSizeAndPos();

            // _material.SetVector("_Center", _center);
            // _material.SetFloat("_width", _current * _width);
            // _material.SetFloat("_height", _current * _height);
            if ((GuideMaskShape)_config.MaskShape == GuideMaskShape.HighLight)
            {
                _material.SetVector("_Center", _center);
                _material.SetFloat("_width", _current * 0);
                _material.SetFloat("_height", _current * 0);
            }
            else
            {
                _material.SetVector("_Center", _center);
                _material.SetFloat("_width", _current * _width);
                _material.SetFloat("_height", _current * _height);
            }
        }
        
        private void UpdateHighShow()
        {
            if ((GuideMaskShape)_config.MaskShape == GuideMaskShape.HighLight)
            {
                if (_targets.Length > 0 && _targets[0] != null)
                {
                    if (_targets[0] is RectTransform)
                    {
                        var target = _targets[0] as RectTransform;
                        var canvas = target.GetComponent<Canvas>();
                        if (null == canvas)
                        {
                            canvas = target.gameObject.AddComponent<Canvas>();
                            // 启用顶点颜色和法线信息
                            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
                            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Normal;
                            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent;
                            target.gameObject.AddComponent<GraphicRaycaster>();

                            canvas.overrideSorting = true;
                            Game.GetMod<GuideSys>().PreTMatchGuideItemOrderName2D = canvas.sortingLayerName;
                            canvas.sortingLayerName = "Top";
                        }
                        else
                        {
                            //暂时不考虑已经有 canvas 的情况
                           // DebugUtil.LogError("zxy : guide , high show : has canvas : guide config id : " + _config.Id);
                        }
                    }
                    else
                    {
                        var target = _targets[0].transform;
                        Game.GetMod<GuideSys>().PreTMatchGuideItemLayer = target.gameObject.layer;
                        target.gameObject.layer = LayerMask.NameToLayer("TMatchGuideItem");
                        //var itemComp = Game.GetMod<TMatchLogic>().FindItem(target.gameObject);
                        // if (itemComp != null)
                        // {
                        //     itemComp.Pick(false);
                        // }
                        // Game.GetMod<TMatchLogic>().SetGuideCameraShow(true);   
                    }
                }
                
                _circleImage.gameObject.SetActive(false);
                _rectangleImage.gameObject.SetActive(false);
            }
        }

        void FixedUpdate()
        {
            updateMaterialSettings();
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (_config == null) return true;
            if ((ClickToFinishType) _config.ClickToFinishType != ClickToFinishType.LogicTarget) return true;

            if (_targets != null)
            {
                foreach (var target in _targets)
                {
                    var clickInCircle = false;
                    if (target is RectTransform)
                    {
                        clickInCircle =
                            RectTransformUtility.RectangleContainsScreenPoint((target as RectTransform), sp, _camera);
                    }
                    else
                    {
                        clickInCircle = Vector2.Distance(sp, _centerScreenPos) < GetRadius() / 2f;
                    }

                    if (clickInCircle)
                    {
                        //false表示没有点中遮罩，不响应点击，透过
                        return false;
                    }
                }
            }

            return true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if ((ClickToFinishType) _config.ClickToFinishType == ClickToFinishType.AnyWhere)
            {
                Game.GetMod<GuideSys>().FinishCurrent(GuideTargetType.None);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if ((ClickToFinishType) _config.ClickToFinishType == ClickToFinishType.Mask)
            {
                if (_targets != null)
                {
                    foreach (var target in _targets)
                    {
                        var clickInCircle = false;
                        if (target is RectTransform)
                        {
                            clickInCircle = RectTransformUtility.RectangleContainsScreenPoint((target as RectTransform),
                                eventData.position, _camera);
                        }
                        else
                        {
                            clickInCircle = Vector2.Distance(eventData.position, _centerScreenPos) < GetRadius();
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

        public float GetRadius()
        {
            return _height * 2f;
        }
        
        private void ResetGuideHighShow()
        {
            if ((GuideMaskShape)_config.MaskShape == GuideMaskShape.HighLight)
            {
                if (_targets.Length > 0 && _targets[0] != null)
                {
                    if (_targets[0] is RectTransform)
                    {
                        var target = _targets[0] as RectTransform;
                        var canvas = target.gameObject.GetComponent<Canvas>();
                        if (canvas != null)
                        { 
                            if (!String.IsNullOrEmpty(Game.GetMod<GuideSys>().PreTMatchGuideItemOrderName2D))
                            {
                                var graphicRaycaster = target.gameObject.GetComponent<GraphicRaycaster>();
                                if(graphicRaycaster!=null)Destroy(graphicRaycaster);
                                var mCanvas = target.gameObject.GetComponent<Canvas>();
                                if(mCanvas!=null)Destroy(mCanvas);
                            }
                        }
                    }
                    else
                    {
                        var target = _targets[0].transform;
                        var preLayer = Game.GetMod<GuideSys>().PreTMatchGuideItemLayer;
                        target.gameObject.layer = preLayer;
                        //Game.GetMod<TMatchLogic>().SetGuideCameraShow(false);   
                        
                    }
                }
            }
        }

        public void OnClose()
        {
            ResetGuideHighShow();
        }
    }
}