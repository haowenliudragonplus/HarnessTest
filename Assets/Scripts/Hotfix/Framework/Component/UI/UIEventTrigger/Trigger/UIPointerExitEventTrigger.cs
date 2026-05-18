using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIPointerExitEventTrigger : MonoBehaviour, IPointerExitHandler
    {
        private Action<GameObject, PointerEventData> onPointerExit;

        public static UIPointerExitEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIPointerExitEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIPointerExitEventTrigger>();
            return component;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerExit)
        {
            this.onPointerExit += onPointerExit;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerExit)
        {
            this.onPointerExit -= onPointerExit;
            if (this.onPointerExit == null || this.onPointerExit.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIPointerExitEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onPointerExit = null;
            Destroy(gameObject.GetComponent<UIPointerExitEventTrigger>());
        }
    }
}