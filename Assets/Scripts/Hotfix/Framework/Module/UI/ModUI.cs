using System;
using System.Collections.Generic;
using System.IO;
using DragonPlus.Config.Common;
using TMGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// UI层级类型
    /// </summary>
    public enum EUILayerType
    {
        Hud = 0,
        Window = 1,
        Pop = 2,
        Guide = 3,
        Top = 4,
        OverTop = 5,
    }

    /// <summary>
    /// UI类型
    /// </summary>
    public enum EUIType
    {
        Main = 1, //主界面
        FullScreen, //全屏界面
        NotFullScreen, //非全屏界面
        Popup, //弹窗
        System, //系统
    }

    public class ModUI : ModuleBase
    {
        //根据项目进行调整
        public static Vector2 ReferenceResolution = new Vector2(768, 1366);

        public const int LAYERSTEP_ORDERINLAYER = 2000;
        public const int VIEWSTEP_ORDERINLAYER = 100;

        private GameObject uiRootGo; //UIRoot物体
        public Canvas UICanvas { get; private set; } //UI画布
        public Camera UICamera { get; private set; } //UI相机
        public RectTransform UIRect => UICanvas.GetComponent<RectTransform>(); //UIRect
        public EventSystem EventSystem { get; private set; }

        private List<UIViewBase> viewStack = new List<UIViewBase>();
        private List<UIViewBase> viewStack_Temp = new List<UIViewBase>();
        private Dictionary<EUILayerType, UILayer> layerType2Layer = new Dictionary<EUILayerType, UILayer>();

        /// <summary>
        /// 同步打开界面
        /// </summary>
        public UIViewBase OpenSync(int viewId, object viewData = null)
        {
            var curView = FindView(viewId);
            if (curView != null)
            {
                Pop(curView);
                Push(curView);
                if (!curView.Visible)
                {
                    curView.InternalOpen();
                }
                else
                {
                    curView.InternalShow();
                }
                return curView;
            }
            else
            {
                Table_Common_UIView uiViewCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_UIView>(viewId);
                if (uiViewCfg == null)
                {
                    Debug.LogError($"UIView表里没有配置id为{viewId}的界面");
                    return null;
                }
                string resKey = Game.GetMod<ModConfig>().GetConfig<Table_Common_Resource>(uiViewCfg.ResourceId).ResourceKey;
                if (string.IsNullOrEmpty(resKey))
                    return null;

                string viewName = Path.GetFileName(resKey);
                var uiLayer = FindLayer((EUILayerType)uiViewCfg.LayerType);
                if (uiLayer == null)
                    return null;
                var classType = Type.GetType(viewName);
                if (classType == null)
                {
                    Debug.LogError($"没有界面类{viewName}，请先生成界面类");
                    return null;
                }
                UIViewBase view = Activator.CreateInstance(classType) as UIViewBase;
                if (view == null)
                {
                    Debug.LogError($"类{viewName}不是继承UIViewBase的界面类");
                    return null;
                }

                view.InternalInit(viewName, uiViewCfg, uiLayer, viewData);
                bool createRet = view.InternalCreate(uiLayer.LayerGo.transform);
                if (!createRet)
                    return null;
                Push(view);
                view.InternalOpen();
                return view;
            }
        }

        public void Close(int viewId, bool isDestroy = true, Action onComplete = null)
        {
            var view = FindView(viewId);
            if (view == null)
                return;
            Close(view, isDestroy, onComplete);
        }

        public bool Close(UIViewBase view, bool isDestroy = true, Action onComplete = null)
        {
            if (view == null)
                return false;
            view.InternalClose(isDestroy, onComplete);
            if (isDestroy)
            {
                Pop(view);
            }
            return true;
        }

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        public void CloseAll(bool isDestroy, List<int> ignoreViewList = null)
        {
            List<UIViewBase> copyList = new List<UIViewBase>();
            viewStack.CopyListNonAlloc(copyList);
            foreach (var view in copyList)
            {
                if (ignoreViewList != null && ignoreViewList.Contains(view.ViewId))
                    continue;
                Close(view, isDestroy);
            }
        }

        /// <summary>
        /// 查找界面
        /// </summary>
        public UIViewBase FindView(int viewId)
        {
            foreach (var uiView in viewStack)
            {
                if (uiView.ViewId == viewId)
                    return uiView;
            }
            return null;
        }

        /// <summary>
        /// 查找某一层级下的最顶部界面
        /// </summary>
        public UIViewBase FindTopView(EUILayerType layerType, List<int> ignoreViewList = null)
        {
            var layer = FindLayer(layerType);
            var view = layer.GetTopView(ignoreViewList);
            return view;
        }

        public UIViewBase FindTopView(List<int> ignoreViewList = null)
        {
            if (viewStack.Count <= 0)
                return null;

            for (int i = viewStack.Count - 1; i >= 0; i--)
            {
                var view = viewStack[i];
                if (!view.Visible ||
                    (ignoreViewList != null && ignoreViewList.Contains(view.ViewId)))
                    continue;
                return view;
            }
            return null;
        }

        #region 常用工具

        public void ShowWaiting(float closeTime = 30, float delayTime = 0)
        {
            UIView_Waiting.OpenData viewData = new UIView_Waiting.OpenData()
            {
                closeTime = closeTime,
                delayTime = delayTime,
            };
            OpenSync(UIViewName.UIView_Waiting, viewData);
        }

        public void CloseWaiting()
        {
            Close(UIViewName.UIView_Waiting, true);
        }

        #endregion 常用工具

        #region private

        /// <summary>
        /// 查找层级
        /// </summary>
        public UILayer FindLayer(EUILayerType layerType)
        {
            if (!layerType2Layer.TryGetValue(layerType, out var layer))
            {
                Debug.LogError($"没有找到{layerType}层");
                return null;
            }
            return layer;
        }

        private void Push(UIViewBase view)
        {
            var layer = FindLayer(view.LayerType);
            layer.AddView(view);
            viewStack.Add(view);
        }

        private void Pop(UIViewBase view)
        {
            var layer = FindLayer(view.LayerType);
            layer.RemoveView(view);
            viewStack.Remove(view);
        }

        #region 创建UI结构

        private void CreateUIStructure()
        {
            uiRootGo = GameUtils.CreateGameObject("UIRoot", null);
            Object.DontDestroyOnLoad(uiRootGo);
            uiRootGo.AddComponent<SpriteAtlasLoader>();
            CreateUICamera();
            CreateEventSystem();
            CreateUICanvas();
        }

        private void CreateUICamera()
        {
            GameObject uiCameraGo = GameUtils.CreateGameObject("UICamera", uiRootGo.transform, false, typeof(Camera));
            UICamera = uiCameraGo.GetComponent<Camera>();
            uiCameraGo.transform.position -= Vector3.forward * 5;
            UICamera.orthographic = true;
            UICamera.cullingMask = LayerMask.GetMask(CommonConst.Layer_UI);
            UICamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;
        }

        private void CreateUICanvas()
        {
            GameObject uiCanvasGo = GameUtils.CreateGameObject("UICanvas", uiRootGo.transform, false,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            uiCanvasGo.GetComponent<GraphicRaycaster>();
            uiCanvasGo.layer = LayerMask.NameToLayer(CommonConst.Layer_UI);
            UICanvas = uiCanvasGo.GetComponent<Canvas>();
            UICanvas.renderMode = RenderMode.ScreenSpaceCamera;
            UICanvas.worldCamera = UICamera;
            CanvasScaler canvasScaler = uiCanvasGo.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.referenceResolution = ReferenceResolution;
            //创建Canvas下的层级结构
            string[] uiLayerNameArray = Enum.GetNames(typeof(EUILayerType));
            foreach (var layerName in uiLayerNameArray)
            {
                EUILayerType uiLayerType = (EUILayerType)Enum.Parse(typeof(EUILayerType), layerName);
                GameObject layerRootGo = GameUtils.CreateGameObject(layerName, uiCanvasGo.transform, false, typeof(RectTransform));
                RectTransform rect = layerRootGo.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                UILayer layer = new UILayer();
                layer.Init(layerRootGo, uiLayerType);
                layerRootGo.layer = LayerMask.NameToLayer(CommonConst.Layer_UI);
                layerType2Layer.Add(uiLayerType, layer);
            }
        }

        private void CreateEventSystem()
        {
            var eventSystemGo = GameUtils.CreateGameObject("EventSystem", uiRootGo.transform, false,
                typeof(EventSystem), typeof(StandaloneInputModule));
            EventSystem = eventSystemGo.GetComponent<EventSystem>();
        }

        #endregion 创建UI结构

        #endregion Private

        #region 生命周期

        public override void OnInit()
        {
            //创建UI结构
            CreateUIStructure();
        }

        public override void Update(float deltaTime)
        {
            viewStack.CopyListNonAlloc(viewStack_Temp);
            foreach (var view in viewStack_Temp)
            {
                view.InternalUpdate();
            }
        }

        #endregion 生命周期
    }
}