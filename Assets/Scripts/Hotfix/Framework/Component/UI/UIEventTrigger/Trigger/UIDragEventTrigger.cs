using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIDragEventTrigger : MonoBehaviour, IDragHandler
    {
        private Action<GameObject, PointerEventData> onDrag;

        public static UIDragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIDragEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIDragEventTrigger>();
            return component;
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onDrag)
        {
            this.onDrag += onDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onDrag)
        {
            this.onDrag -= onDrag;
            if (this.onDrag == null || this.onDrag.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIDragEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onDrag = null;
            Destroy(gameObject.GetComponent<UIDragEventTrigger>());
        }
    }
}