using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIPointerDownEventTrigger : MonoBehaviour, IPointerDownHandler
    {
        private Action<GameObject, PointerEventData> onPointerDown;

        public static UIPointerDownEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIPointerDownEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIPointerDownEventTrigger>();
            return component;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerDown)
        {
            this.onPointerDown += onPointerDown;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerDown)
        {
            this.onPointerDown -= onPointerDown;
            if (this.onPointerDown == null || this.onPointerDown.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIPointerDownEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onPointerDown = null;
            Destroy(gameObject.GetComponent<UIPointerDownEventTrigger>());
        }
    }
}