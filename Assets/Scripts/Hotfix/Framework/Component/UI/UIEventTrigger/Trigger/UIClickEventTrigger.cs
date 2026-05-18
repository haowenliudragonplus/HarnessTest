using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIClickEventTrigger : MonoBehaviour, IPointerClickHandler
    {
        private Action<GameObject> onClick;

        private float invalidTime;
        private float lastClickTime;

        public static UIClickEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UIClickEventTrigger>();
            if (component == null)
                component = go.AddComponent<UIClickEventTrigger>();
            return component;
        }

        public void SetInvalidTime(float invalidTime = 0)
        {
            this.invalidTime = invalidTime;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (lastClickTime != 0 && Time.unscaledTime - lastClickTime < invalidTime)
                return;
            onClick?.Invoke(gameObject);
            lastClickTime = Time.unscaledTime;
        }

        public void AddListener(Action<GameObject> onClick)
        {
            this.onClick += onClick;
        }

        public void RemoveListener(Action<GameObject> onClick)
        {
            this.onClick -= onClick;
            if (this.onClick == null || this.onClick.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UIClickEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onClick = null;
            Destroy(gameObject.GetComponent<UIClickEventTrigger>());
        }
    }
}