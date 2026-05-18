using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIDoubleClickEventTrigger : MonoBehaviour, IPointerClickHandler
    {
        private Action<GameObject, int> onDoubleClick;

        private float doubleClickTime; //视为双击的时间
        private float lastClickTime;
        private int doubleClickCount;

        public static UIDoubleClickEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIDoubleClickEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIDoubleClickEventTrigger>();
            return component;
        }

        public void SetDoubleClickTime(float doubleClickTime = 0)
        {
            this.doubleClickTime = doubleClickTime;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (lastClickTime == 0 || Time.unscaledTime - lastClickTime > doubleClickTime)
            {
                doubleClickCount = 0;
                doubleClickCount++;
                lastClickTime = Time.unscaledTime;
                return;
            }

            doubleClickCount++;
            onDoubleClick?.Invoke(gameObject, doubleClickCount);
            lastClickTime = Time.unscaledTime;
        }

        public void AddListener(Action<GameObject, int> onDoubleClick)
        {
            this.onDoubleClick += onDoubleClick;
        }

        public void RemoveListener(Action<GameObject, int> onDoubleClick)
        {
            this.onDoubleClick -= onDoubleClick;
            if (this.onDoubleClick == null || this.onDoubleClick.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIDoubleClickEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onDoubleClick = null;
            Destroy(gameObject.GetComponent<UIDoubleClickEventTrigger>());
        }
    }
}