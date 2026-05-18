using TMGame;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI控件基类
    /// </summary>
    public class UIWidgetBase : UIBase
    {
        public string WidgetName { get; private set; } //控件名字

        private bool reusable;

        public void SetViewData(object viewData = null)
        {
            ViewData = viewData;
        }

        public void InternalInit(UIBase parent, string widgetName, bool reusable, object viewData = null)
        {
            ViewData = viewData;
            WidgetName = widgetName;
            this.reusable = reusable;
            Parent = parent;
            UIViewHolder = parent.UIViewHolder;
            parent.InternalAddToWidgetList(this);
            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject widgetGo = null;
            if (reusable)
            {
                //todo 通过对象池管理
            }
            else
            {
                widgetGo = Game.GetMod<ModAsset>().GetGameObject(WidgetName).GetInstance();
            }
            if (widgetGo == null)
            {
                Debug.LogError($"{WidgetName}控件资源实例化失败");
                return false;
            }
            widgetGo.transform.SetParent(trans, false);
            GO = widgetGo;
            OnCreate();
            return true;
        }

        public void InternalOpen()
        {
            Visible = true;
            OnOpen();
        }

        public void InternalShow()
        {
            Visible = true;
            OnShow();
        }

        public void InternalClose()
        {
            Visible = false;
            OnClose();
        }

        public void InternalDestroy()
        {
            if (reusable)
            {
                //todo 通过对象池管理
            }
            else
            {
                Object.Destroy(GO);
                OnDestroy();
            }
        }
    }
}