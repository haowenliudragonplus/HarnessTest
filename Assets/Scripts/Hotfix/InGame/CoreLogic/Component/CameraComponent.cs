using TMGame;
using UnityEngine;

public class CameraComponent
{
    public float cameraZoomDefault;
    public float cameraZoomSpeed;
    private float cameraZoomMax;
    private float cameraZoomMin;

    private InGameModeBase mode;
    private Camera curCamera;

    private Vector2 cameraMoveMin = new Vector2(-15, -30);
    private Vector2 cameraMoveMax = new Vector2(15, 30);
    public float cameraSmoothSpeed;
    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 targetPos;
    private bool setCameraPos;
    private bool canDrag;

    public bool IsPauseMove;

    public CameraComponent(InGameModeBase mode)
    {
        this.mode = mode;
        curCamera = mode.ElementCamera;
        cameraZoomDefault = InGameConst.CameraSizeMax;
        cameraZoomSpeed = 1;
        cameraZoomMax = InGameConst.CameraSizeMin;
        cameraZoomMin = InGameConst.CameraSizeMax;
        cameraSmoothSpeed = 0.01f;
        curCamera.orthographicSize = cameraZoomDefault;

        targetPos = curCamera.transform.position;
    }

    public void OnLateUpdate(float deltaTime)
    {
        if (IsPauseMove)
        {
            return;
        }

#if UNITY_EDITOR
        HandleCamera_Editor();
#else
        HandleCamera_Mobile();
#endif
        // 限制位置
        ClampPos();

        // 缓动移动到指定位置
        if (setCameraPos && Vector2.Distance(curCamera.transform.position, targetPos) >= 0.01f)
        {
            Vector3 tempTargetPos = Vector3.SmoothDamp(curCamera.transform.position, targetPos,
                ref smoothVelocity, cameraSmoothSpeed);
            // Vector3 tempTargetPos = Vector3.Lerp(curCamera.transform.position, targetPos, cameraSmoothSpeed);
            curCamera.transform.position = tempTargetPos;
        }
        else
        {
            curCamera.transform.position = targetPos;
            setCameraPos = false;
        }
    }

    private Vector2 lastMousePos;
    private void HandleCamera_Editor()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Vector3 prevPos = CTUtils.Screen2World(Input.mousePosition, curCamera);
            prevPos.z = curCamera.transform.position.z;
            float targetSize = curCamera.orthographicSize - scrollDelta * 5; //编辑器下与真机上参数无法统一，编辑器下直接定好数值
            targetSize = Mathf.Clamp(targetSize, cameraZoomMin, cameraZoomMax);
            curCamera.orthographicSize = targetSize;
            Vector3 curPos = CTUtils.Screen2World(Input.mousePosition, curCamera);
            curPos.z = curCamera.transform.position.z;
            curCamera.transform.position += (prevPos - curPos);
            targetPos = curCamera.transform.position;
        }
        if (Input.GetMouseButtonDown(0))
        {
            canDrag = true;
            lastMousePos = Input.mousePosition;
        }
        if (canDrag && Input.GetMouseButton(0))
        {
            Vector3 diff_WorldPos = CTUtils.Screen2World(Input.mousePosition, curCamera) - CTUtils.Screen2World(lastMousePos, curCamera);
            targetPos -= diff_WorldPos;
            lastMousePos = Input.mousePosition;
            setCameraPos = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canDrag = false;
        }
    }

    private bool inPinch;
    private void HandleCamera_Mobile()
    {
        if (Input.touchCount == 2)
        {
            inPinch = true;
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                Vector3 prevPos = CTUtils.Screen2World((touch0.position + touch1.position) / 2, curCamera);
                prevPos.z = curCamera.transform.position.z;
                var curTouchDelta_World = CTUtils.Screen2World(touch0.position, curCamera) - CTUtils.Screen2World(touch1.position, curCamera);
                var lastTouchDelta_World = CTUtils.Screen2World(touch0.position - touch0.deltaPosition, curCamera) - CTUtils.Screen2World(touch1.position - touch1.deltaPosition, curCamera);
                float diff_World = curTouchDelta_World.magnitude - lastTouchDelta_World.magnitude;
                float targetSize = curCamera.orthographicSize - diff_World * cameraZoomSpeed;
                targetSize = Mathf.Clamp(targetSize, cameraZoomMin, cameraZoomMax);
                curCamera.orthographicSize = targetSize;
                Vector3 curPos = CTUtils.Screen2World((touch0.position + touch1.position) / 2, curCamera);
                curPos.z = curCamera.transform.position.z;
                curCamera.transform.position += (prevPos - curPos);
                targetPos = curCamera.transform.position;
            }
        }
        else if (Input.touchCount == 1)
        {
            if (inPinch)
                return;
            var touch0 = Input.GetTouch(0);
            if (touch0.phase == TouchPhase.Began)
            {
                canDrag = true;
            }
            if (canDrag && touch0.phase == TouchPhase.Moved)
            {
                Vector2 curTouchPos = touch0.position;
                Vector2 lastTouchPos = touch0.position - touch0.deltaPosition;
                Vector3 diffPos_World = CTUtils.Screen2World(curTouchPos, curCamera) - CTUtils.Screen2World(lastTouchPos, curCamera);
                targetPos -= diffPos_World;
                setCameraPos = true;
            }
        }
        else
        {
            inPinch = false;
        }
    }

    /// <summary>
    /// 限制位置
    /// </summary>
    private void ClampPos()
    {
        float value_Y = curCamera.orthographicSize;
        float value_X = Screen.width * 1f / Screen.height * value_Y;
        float clamp_X = Mathf.Clamp(targetPos.x, cameraMoveMin.x + value_X, cameraMoveMax.x - value_X);
        float clamp_Y = Mathf.Clamp(targetPos.y, cameraMoveMin.y + value_Y, cameraMoveMax.y - value_Y);
        targetPos = new Vector3(clamp_X, clamp_Y, curCamera.transform.position.z);
    }
}