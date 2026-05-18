using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIPointerEnterEventTrigger : MonoBehaviour, IPointerEnterHandler
    {
        private Action<GameObject, PointerEventData> onPointerEnter;

        public static UIPointerEnterEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIPointerEnterEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIPointerEnterEventTrigger>();
            return component;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerEnter)
        {
            this.onPointerEnter += onPointerEnter;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerEnter)
        {
            this.onPointerEnter -= onPointerEnter;
            if (this.onPointerEnter == null || this.onPointerEnter.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIPointerEnterEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onPointerEnter = null;
            Destroy(gameObject.GetComponent<UIPointerEnterEventTrigger>());
        }
    }
}