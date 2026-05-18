using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UILongPressEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Action<GameObject> onLongPress;

        private bool isPress;
        private float longPressTime;
        private float longPressTimer;
        private bool isContinueCheck;
        private bool isReleaseOnLongPress;

        public static UILongPressEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<UILongPressEventTrigger>();
            if (component == null)
                component = go.AddComponent<UILongPressEventTrigger>();
            return component;
        }

        public void SetLongPressTime(float longPressTime = 0)
        {
            this.longPressTime = longPressTime;
        }

        public void SetIsContinueCheck(bool isContinueCheck)
        {
            this.isContinueCheck = isContinueCheck;
        }

        public void AddListener(Action<GameObject> onLongPress)
        {
            this.onLongPress += onLongPress;
        }

        public void RemoveListener(Action<GameObject> onLongPress)
        {
            this.onLongPress -= onLongPress;
            if (this.onLongPress == null || this.onLongPress.GetInvocationList().Length <= 0)
                Destroy(gameObject.GetComponent<UILongPressEventTrigger>());
        }

        public void RemoveAllListener()
        {
            this.onLongPress = null;
            Destroy(gameObject.GetComponent<UILongPressEventTrigger>());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPress = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPress = false;
            longPressTimer = 0;
            isReleaseOnLongPress = false;
        }

        private void Update()
        {
            if (isPress &&
                (!isReleaseOnLongPress || isContinueCheck))
            {
                longPressTimer += Time.unscaledDeltaTime;
                if (longPressTimer >= longPressTime)
                {
                    onLongPress?.Invoke(gameObject);
                    isReleaseOnLongPress = true;
                    longPressTimer = 0;
                }
            }
        }
    }
}