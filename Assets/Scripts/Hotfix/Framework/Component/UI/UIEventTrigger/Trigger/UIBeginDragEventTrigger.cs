using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIBeginDragEventTrigger : MonoBehaviour, IBeginDragHandler
    {
        private Action<GameObject, PointerEventData> onBeginDrag;

        public static UIBeginDragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIBeginDragEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIBeginDragEventTrigger>();
            return component;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onBeginDrag)
        {
            this.onBeginDrag += onBeginDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onBeginDrag)
        {
            this.onBeginDrag -= onBeginDrag;
            if (this.onBeginDrag == null || this.onBeginDrag.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIBeginDragEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onBeginDrag = null;
            Destroy(gameObject.GetComponent<UIBeginDragEventTrigger>());
        }
    }
}