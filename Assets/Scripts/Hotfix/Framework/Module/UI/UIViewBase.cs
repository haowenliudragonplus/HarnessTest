using System;
using DG.Tweening;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Haptics;
using TMGame;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// 界面动画类型
    /// </summary>
    public enum EViewAniType
    {
        NoAni = 1, //不播放动画
        Tween, //通过Tween控制
        Animator, //通过Animator控制
    }

    /// <summary>
    /// UI界面基类
    /// </summary>
    public abstract class UIViewBase : UIViewOrUISubViewBase
    {
        public Table_Common_UIView UIViewCfg { get; private set; } //UIView表
        public string ViewName { get; private set; } //界面名字
        public UILayer UILayer { get; private set; } //UILayer
        public int ViewId => UIViewCfg.Id; //界面id
        public EUILayerType LayerType => (EUILayerType)UIViewCfg.LayerType; //层级类型
        public EUIType Type => (EUIType)UIViewCfg.Type; //界面类型

        private Canvas canvas; //当前界面的Canvas
        private Canvas[] childCanvas; //当前界面下的所有子Canvas
        private int[] childCanvasOriginSortingOrder;

        public RectTransform ViewRootRect { get; private set; } //界面Root根节点，可以没有（控制动画，适配）

        protected EViewAniType viewAniType = EViewAniType.Tween; //界面动画类型（之后可以改成读表配置）
        protected bool playSound = true;
        protected bool enableVirbrate = true;

        public int OrderInLayer //层级排序
        {
            set
            {
                if (canvas != null)
                {
                    if (canvas.sortingOrder == value)
                        return;

                    canvas.sortingOrder = value;
                    int v = value;
                    for (int i = 0; i < childCanvas.Length; i++)
                    {
                        var canvas = childCanvas[i];
                        if (canvas != this.canvas)
                        {
                            canvas.sortingOrder = childCanvasOriginSortingOrder[i] + v;
                        }
                    }
                }
            }
        }

        #region Callback

        protected virtual void OnPreOpenAniComplete()
        {
        }

        protected virtual void OnOpenAniComplete()
        {
        }

        protected virtual void OnPreCloseAniComplete()
        {
        }

        protected virtual void OnCloseAniComplete()
        {
        }

        #endregion Callback

        public void Close(bool isDestroy = true, Action onComplete = null)
        {
            Game.GetMod<ModUI>().Close(this, isDestroy, onComplete);
        }

        private Sequence openAniSeq;
        private Sequence closeAniSeq;
        private void PlayAni(bool isOpen, Action onComplete = null)
        {
            if (viewAniType == EViewAniType.NoAni
                || (viewAniType == EViewAniType.Animator && ViewRootRect.GetComponent<Animator>() == null)
                || ViewRootRect == null)
            {
                onComplete?.Invoke();
                return;
            }

            if (GO.GetComponent<CanvasGroup>() == null)
            {
                GO.AddComponent<CanvasGroup>();
            }

            switch (viewAniType)
            {
                case EViewAniType.Tween:
                    if (isOpen)
                    {
                        openAniSeq = DOTween.Sequence();
                        ViewRootRect.localScale = Vector3.one * 0.6f;
                        GO.GetComponent<CanvasGroup>().alpha = 0;
                        // Tween tween1 = viewRootRect.DOScale(Vector3.one * 1.1f, 0.16f);
                        Tween tween2 = ViewRootRect.DOScale(Vector3.one * 1f, 0.25f).SetEase(Ease.OutBack);
                        Tween tween3 = GO.GetComponent<CanvasGroup>().DOFade(1f, 0.25f).SetEase(Ease.OutBack);
                        // openAniSeq.Append(tween1);
                        openAniSeq.Append(tween2);
                        openAniSeq.Join(tween3);
                        openAniSeq.SetUpdate(true);
                        openAniSeq.OnComplete(() => { onComplete?.Invoke(); });
                    }
                    else
                    {
                        GameObject emptyImgGo = CreateEmptyImg();
                        closeAniSeq = DOTween.Sequence();
                        // Tween tween1 = viewRootRect.DOScale(Vector3.one * 1.1f, 0.08f);
                        Tween tween1 = GO.GetComponent<CanvasGroup>().DOFade(0f, 0.15f);
                        Tween tween2 = ViewRootRect.DOScale(Vector3.one * 0.75f, 0.15f);
                        closeAniSeq.Join(tween1);
                        closeAniSeq.Append(tween2);
                        closeAniSeq.SetUpdate(true);
                        closeAniSeq.OnComplete(() =>
                        {
                            Object.Destroy(emptyImgGo);
                            onComplete?.Invoke();
                        });
                    }
                    break;

                case EViewAniType.Animator:
                    var ani = ViewRootRect.GetComponent<Animator>();
                    //todo 
                    // ani.Play(isOpen ? OPEN_ANI_NAME : CLOSE_ANI_NAME);
                    break;
            }
        }

        private void PlaySound(bool isOpen)
        {
            if (!playSound)
                return;
            Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_show);
        }

        private void PlayVirbrate(bool isOpen)
        {
            if (!enableVirbrate)
                return;
            if(!isOpen) return;
            GameUtils.Virbrate(HapticTypes.Medium);
        }

        public void InternalInit(string viewName, Table_Common_UIView uiViewCfg, UILayer uiLayer, object viewData = null)
        {
            ViewData = viewData;
            UIViewCfg = uiViewCfg;
            UILayer = uiLayer;
            ViewName = viewName;
            UIViewHolder = this;
            Parent = null;

            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject viewGo = null;
            if (GameUtils.IsPad())
            {
                string padName = ViewName + "_Pad";
                viewGo = Game.GetMod<ModAsset>().GetGameObject(padName).GetInstance();
            }
            if (viewGo == null)
            {
                viewGo = Game.GetMod<ModAsset>().GetGameObject(ViewName).GetInstance();
            }
            if (viewGo == null)
            {
                Debug.LogError($"{ViewName}界面资源实例化失败");
                return false;
            }
            viewGo.transform.SetParent(trans, false);

            GO = viewGo;
            ViewRootRect = GO.transform.Find("Root")?.GetComponent<RectTransform>();
            canvas = GO.GetComponent<Canvas>(true);
            canvas.overrideSorting = true;
            childCanvas = GO.GetComponentsInChildren<Canvas>(true);
            childCanvasOriginSortingOrder = new int[childCanvas.Length];
            for (int i = 0; i < childCanvasOriginSortingOrder.Length; i++)
            {
                childCanvasOriginSortingOrder[i] = childCanvas[i].sortingOrder;
            }
            GO.GetComponent<GraphicRaycaster>(true);
            viewGo.SetLayer("UI", true);

            OnCreate();
            return true;
        }

        public void InternalOpen()
        {
            Visible = true;

            openAniSeq?.Kill(true);
            closeAniSeq?.Kill(true);

            Game.GetMod<ModEvent>().Dispatch(new WindowOpenEvent());

            PlaySound(true);
            PlayVirbrate(true);
            OnPreOpenAniComplete();
            PlayAni(true, () =>
            {
                OnOpenAniComplete();

                Game.GetMod<ModEvent>().Dispatch(new WindowOpenCompleteEvent());
            });

            OnOpen();
        }

        public void InternalShow()
        {
            Visible = true;
            OnShow();
        }

        public void InternalUpdate()
        {
            OnUpdate();
        }

        public void InternalClose(bool isDestroy = true, Action onComplete = null)
        {
            Game.GetMod<ModEvent>().Dispatch(new WindowCloseEvent());

            PlaySound(false);
            PlayVirbrate(false);
            OnPreCloseAniComplete();
            PlayAni(false, () =>
            {
                openAniSeq?.Kill(true);
                closeAniSeq?.Kill(true);

                Visible = false;
                OnCloseAniComplete();

                if (isDestroy)
                {
                    Object.Destroy(GO);
                    OnDestroy();
                }

                Game.GetMod<ModEvent>().Dispatch(new WindowCloseCompleteEvent());
                onComplete?.Invoke();

                if (LayerType == EUILayerType.Window
                    || LayerType == EUILayerType.Pop
                    || LayerType == EUILayerType.Guide)
                {
                    //Game.GetMod<ModPopup>().SetState(false);
                }
            });
            OnClose();
        }

        private GameObject CreateEmptyImg()
        {
            GameObject go = new GameObject("EmptyImage");
            Image img = go.AddComponent<Image>();
            img.color = ColorUtils.SetColorAlpha(img.color, 0);
            go.transform.SetParent(RectTransform, false);
            go.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            go.GetComponent<RectTransform>().anchorMax = Vector2.one;
            go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            return go;
        }
    }
}