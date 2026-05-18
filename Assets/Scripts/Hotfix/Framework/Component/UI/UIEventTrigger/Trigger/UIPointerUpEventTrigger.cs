using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIPointerUpEventTrigger : MonoBehaviour, IPointerUpHandler
    {
        private Action<GameObject, PointerEventData> onPointerUp;

        public static UIPointerUpEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIPointerUpEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIPointerUpEventTrigger>();
            return component;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerUp)
        {
            this.onPointerUp += onPointerUp;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerUp)
        {
            this.onPointerUp -= onPointerUp;
            if (this.onPointerUp == null || this.onPointerUp.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIPointerUpEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onPointerUp = null;
            Destroy(gameObject.GetComponent<UIPointerUpEventTrigger>());
        }
    }
}