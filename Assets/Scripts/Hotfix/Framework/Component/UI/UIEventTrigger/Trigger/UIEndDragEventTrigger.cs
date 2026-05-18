using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIEndDragEventTrigger : MonoBehaviour, IEndDragHandler
    {
        private Action<GameObject, PointerEventData> onEndDrag;

        public static UIEndDragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIEndDragEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIEndDragEventTrigger>();
            return component;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onEndDrag)
        {
            this.onEndDrag += onEndDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onEndDrag)
        {
            this.onEndDrag -= onEndDrag;
            if (this.onEndDrag == null || this.onEndDrag.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIEndDragEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onEndDrag = null;
            Destroy(gameObject.GetComponent<UIEndDragEventTrigger>());
        }
    }
}