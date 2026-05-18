using System;
using System.Collections.Generic;
using TMGame;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 所有UI基类
    /// </summary>
    public abstract class UIBase : EventListenerHolder
    {
        public UIViewBase UIViewHolder; //属于哪个界面

        protected UIBase Parent; //父对象

        public GameObject GO { protected set; get; } //自身GameObject
        public RectTransform RectTransform => GO.GetComponent<RectTransform>();

        protected object ViewData;

        public bool Visible //是否可见
        {
            get
            {
                return GO.activeSelf;
            }
            set
            {
                GO.SetActive(value);
            }
        }

        private List<UIWidgetBase> WidgetList = new List<UIWidgetBase>(); //所有控件
        private List<UIWidgetBase> WidgetList_Temp = new List<UIWidgetBase>();

        #region 控件

        public void InternalAddToWidgetList(UIWidgetBase widget)
        {
            WidgetList.Add(widget);
        }

        /// <summary>
        /// 打开一个控件
        /// </summary>
        /// reusable：为true则通过对象池管理
        public T OpenUIWidget<T>(Transform trans, bool reusable, object viewData = null)
            where T : UIWidgetBase
        {
            Type type = typeof(T);
            string widgetName = type.Name;
            var classType = Type.GetType(widgetName);
            T widget = Activator.CreateInstance(classType) as T;
            if (widget == null)
                return null;

            widget.InternalInit(this, widgetName, reusable, viewData);
            bool createRet = widget.InternalCreate(trans);
            if (!createRet)
            {
                return null;
            }
            widget.InternalOpen();
            return widget;
        }

        public void ShowUIWidget(UIWidgetBase widget)
        {
            if (widget == null)
                return;

            widget.InternalShow();
        }

        /// <summary>
        /// 关闭一个控件
        /// </summary>
        public bool CloseUIWidget(UIWidgetBase widget, bool isDestroy)
        {
            foreach (var temp in WidgetList)
            {
                if (temp == widget)
                {
                    widget.InternalClose();
                    if (isDestroy)
                    {
                        widget.InternalDestroy();
                        WidgetList.Remove(widget);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭所有控件
        /// </summary>
        public void CloseAllUIWidget(bool isDestroy)
        {
            foreach (var widget in WidgetList)
            {
                widget.InternalClose();
                if (isDestroy)
                {
                    widget.InternalDestroy();
                }
            }
            if (isDestroy)
            {
                WidgetList.Clear();
            }
        }

        #endregion 控件

        #region 生命周期

        protected virtual void OnInit(object viewData)
        {
            ViewData = viewData;
        }

        protected virtual void OnCreate()
        {
            BindComponent();
            RegisterUIEvent();
            RegisterGameEvent();
        }

        protected virtual void OnOpen()
        {
            foreach (var widget in WidgetList)
            {
                if (!widget.Visible)
                    continue;
                widget.OnOpen();
            }
        }

        protected virtual void OnShow()
        {
            foreach (var widget in WidgetList)
            {
                if (!widget.Visible)
                    continue;
                widget.OnShow();
            }
        }

        protected virtual void OnUpdate()
        {
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                if (!widget.Visible)
                    continue;
                widget.OnUpdate();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否销毁）
        /// </summary>
        protected virtual void OnClose()
        {
            foreach (var widget in WidgetList)
            {
                if (!widget.Visible)
                    continue;
                widget.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var widget in WidgetList)
            {
                widget.InternalDestroy();
            }
            WidgetList.Clear();

            RemoveGameEvent();
        }

        protected virtual void BindComponent()
        {
        }

        protected virtual void RegisterUIEvent()
        {
        }

        protected virtual void RegisterGameEvent()
        {
        }

        protected virtual void RemoveGameEvent()
        {
            UnRegisterAllEvent();
        }

        #endregion 生命周期
    }
}