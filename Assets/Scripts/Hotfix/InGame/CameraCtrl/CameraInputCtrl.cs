using System;
using System.Collections;
using DG.Tweening;
using Framework;
using UnityEngine;

public class CameraInputCtrl
{
    public class InputCameraSettings
    {
        //拖拽设置
        public float dragSpeed = 1.5f;
        public float dragEffectDistance = 5f; //屏幕坐标最小拖拽距离
        public float dragEffectTime = 0.06f;
        public float dragSmoothTime = 0.03f; //平滑时间，值越小越跟手
        public float dragInertiaDuration = 0.5f;
        public float dragInertiaSpeed = 0.8f;
        public float doubleTapTimeThreshold = 0.3f;
        // public float minDragDistance = 3f;

        //缩放设置
        public float zoomSpeed = 0.1f;
        public float zoomSmoothTime = 0.08f;
        public float minSize = 10f;
        public float maxSize = 16f;

        //边界设置
        public Vector2 minBounds = new Vector2(-15, -30);
        public Vector2 maxBounds = new Vector2(15, 30);

        //输入设置
        public bool enablePCSimulation = true;
    }

    private Camera targetCamera;
    private InputCameraSettings settings;

    // 拖拽相关
    private Vector3 dragStartWorldPos;
    private Vector2 dragStartScreenPos;
    private bool canDrag, isDragging = false;
    private float dragMoveTimer;
    private Vector3 inertiaVelocity;
    private float inertiaTimer;
    private Vector3 lastFramePosition;
    private Vector3 dragVelocitySmooth;

    // 缩放相关
    private float targetSize;
    private float zoomVelocity;
    private Vector2 zoomCenterPos;
    private bool isZooming { get; set; } = false;

    // PC模拟双指相关
    private bool isSimulatingPinch = false;
    private Vector2 simulatedPoint1;
    private Vector2 simulatedPoint2;
    private float simulatedPrevDistance;

    // 双击检测相关
    private float lastTapTime;
    private Vector2 lastTapPosition;
    private bool isProcessingDoubleTap = false;

    public bool CanInput { get; set; }
    private bool _isGuidingZoom;
    private Vector3 _centerPosition;

    private Touch touch0, touch1;
    private Vector2 prevPos0, prevPos1;
    private float verticalSize, horizontalSize;

    public CameraInputCtrl(Camera camera, InGameDataBase Data)
    {
        targetCamera = camera;
        if (!targetCamera.orthographic)
            targetCamera.orthographic = true;

        var view = Game.GetMod<ModUI>().FindView(UIViewName.UIView_InGame_Main) as UIView_InGame_Main;
        (Vector3 screenUiMax, Vector3 screenUiMin) = view.GetUISafeBoardBounds();

        var cameraSettings = new InputCameraSettings();
        var camWidth = (Data.LevelData.wide - 1 + 2) * InGameConst.PointSpacing;
        var camHeight = camWidth * ((float)Screen.height / Screen.width);

        var screenSafeHeight = (float)Screen.height;
        if (Data.LevelData.high > 26)
        {
            targetCamera.orthographicSize = camHeight * 0.5f;

            var uiSafeMax1 = CTUtils.Screen2World(screenUiMax, targetCamera);
            var uiSafeMin1 = CTUtils.Screen2World(screenUiMin, targetCamera);
            var uiSafeHeightCount = Mathf.CeilToInt((uiSafeMax1.y - uiSafeMin1.y) / InGameConst.PointSpacing);
            if (uiSafeHeightCount < Data.LevelData.high)
            {
                camHeight /= (float)uiSafeHeightCount / Data.LevelData.high;
            }
        }

        camHeight = Mathf.Max(camHeight, (Data.LevelData.high - 1) * InGameConst.PointSpacing);

        cameraSettings.maxSize = Mathf.Max(InGameConst.CameraSizeMin, camHeight * 0.5f);
        cameraSettings.minSize = Mathf.Min(InGameConst.CameraSizeMax, cameraSettings.maxSize - 8);

        settings = cameraSettings;

        ValidateAndApplyDefaults();
        ApplySettings();

        var uiSafeMax = CTUtils.Screen2World(screenUiMax, targetCamera);
        var uiSafeMin = CTUtils.Screen2World(screenUiMin, targetCamera);
        // y轴偏移量:相对于UI上下居中的偏移量
        var midY = (uiSafeMax + uiSafeMin) * -0.5f;
        _centerPosition = new Vector3(0, midY.y, -111);
        targetCamera.transform.position = _centerPosition;

        verticalSize = targetCamera.orthographicSize;
        horizontalSize = verticalSize * targetCamera.aspect;
        var boundWidth = Data.LevelData.wide * InGameConst.PointSpacing * 0.5f;
        var boundHigh = Data.LevelData.high * InGameConst.PointSpacing * 0.5f - midY.y;
        cameraSettings.maxBounds = new Vector2(horizontalSize + boundWidth, cameraSettings.maxSize + boundHigh);
        cameraSettings.minBounds = new Vector2(-cameraSettings.maxBounds.x, -cameraSettings.maxBounds.y + midY.y);
        // CLog.Info($" cameraSettings ------- [{camWidth}/{camHeight}], size:[{cameraSettings.maxSize}/{cameraSettings.minSize}], bounds:[{cameraSettings.maxBounds}/{cameraSettings.minBounds}], [{horizontalSize}/{verticalSize}]");

        StopAllMovement();

#if UNITY_EDITOR || UNITY_STANDALONE
        settings.enablePCSimulation = true;
#else
        settings.enablePCSimulation = false;
#endif

        CanInput = true;
        lastFramePosition = targetCamera.transform.position;
    }

    private void ApplySettings()
    {
        targetCamera.orthographicSize = settings.maxSize;
        targetSize = targetCamera != null ? targetCamera.orthographicSize : settings.maxSize;
    }

    private void ValidateAndApplyDefaults()
    {
        if (settings.minSize < 0)
        {
            CLog.Warning($"最小尺寸不能为负值，已修正为0");
            settings.minSize = 0;
        }

        if (settings.maxSize < settings.minSize)
        {
            CLog.Warning($"最大尺寸({settings.maxSize})不能小于最小尺寸({settings.minSize})，已交换");
            float temp = settings.maxSize;
            settings.maxSize = settings.minSize;
            settings.minSize = temp;
        }

        if (settings.minBounds.x >= settings.maxBounds.x)
        {
            CLog.Warning($"X轴边界无效(min:{settings.minBounds.x}, max:{settings.maxBounds.x})，已调整");
            float midX = (settings.minBounds.x + settings.maxBounds.x) * 0.5f;
            settings.minBounds.x = midX - 1;
            settings.maxBounds.x = midX + 1;
        }

        if (settings.minBounds.y >= settings.maxBounds.y)
        {
            CLog.Warning($"Y轴边界无效(min:{settings.minBounds.y}, max:{settings.maxBounds.y})，已调整");
            float midY = (settings.minBounds.y + settings.maxBounds.y) * 0.5f;
            settings.minBounds.y = midY - 1;
            settings.maxBounds.y = midY + 1;
        }
    }

    /// <summary>
    /// 更新 LateUpdate
    /// </summary>
    public void LateUpdate()
    {
        if (!CanInput || targetCamera == null || UIUtils.IsPointerOverUI())
            return;

        verticalSize = targetCamera.orthographicSize;
        horizontalSize = verticalSize * targetCamera.aspect;

        // 处理输入
        if (settings.enablePCSimulation && HandlePCSimulatedPinch())
        {
            // 模拟双指已处理
        }
        else
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_IOS || UNITY_ANDROID
            HandleTouchInput();
#endif
        }

        ApplyInertia();
        SmoothZoom();
        ClampCameraPosition();

        // 更新位置
        lastFramePosition = targetCamera.transform.position;
    }

    /// <summary>
    /// 获取相机是否正在被用户操作
    /// </summary>
    public bool IsUserControlling()
    {
        bool hasCurrentInput = isDragging || isSimulatingPinch;
        bool hasInertia = inertiaTimer > 0;
        CLog.Info($" InputCamera isDragging:{isDragging}, isSim:{isSimulatingPinch}, hasInertia:{hasInertia} isZooming:{isZooming}");
        return hasCurrentInput || hasInertia || isZooming;
    }

    /// <summary>
    /// PC端模拟移动端双指缩放
    /// </summary>
    private bool HandlePCSimulatedPinch()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSimulatingPinch = true;
                simulatedPoint1 = Input.mousePosition;
                isZooming = true;

                Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                Vector2 fromCenter = simulatedPoint1 - screenCenter;
                simulatedPoint2 = screenCenter - fromCenter;

                simulatedPrevDistance = Vector2.Distance(simulatedPoint1, simulatedPoint2);
                ClearDragInfo();
                return true;
            }

            if (Input.GetMouseButton(0) && isSimulatingPinch)
            {
                simulatedPoint1 = Input.mousePosition;

                Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                Vector2 fromCenter = simulatedPoint1 - screenCenter;
                simulatedPoint2 = screenCenter - fromCenter;

                float currentDistance = Vector2.Distance(simulatedPoint1, simulatedPoint2);
                float distanceDelta = currentDistance - simulatedPrevDistance;

                RecordZoomSizeCenter(simulatedPoint1, simulatedPoint2, distanceDelta);
                simulatedPrevDistance = currentDistance;
                TryFinishGuideZoom();
                return true;
            }

            if (Input.GetMouseButtonUp(0) && isSimulatingPinch)
            {
                isSimulatingPinch = false;
                isZooming = false;
                return true;
            }
        }
        else if (isSimulatingPinch)
        {
            isSimulatingPinch = false;
            ClearDragInfo();
        }

        return false;
    }

    /// <summary>
    /// 处理鼠标输入
    /// </summary>
    private void HandleMouseInput()
    {
        if (isSimulatingPinch) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 currentPos = Input.mousePosition;
            float timeSinceLastTap = Time.time - lastTapTime;

            // 双击检测
            if (timeSinceLastTap < settings.doubleTapTimeThreshold &&
                Vector2.Distance(currentPos, lastTapPosition) < settings.dragEffectDistance)
            {
                isProcessingDoubleTap = true;
                lastTapTime = 0;
                ClearDragInfo();
                return;
            }

            lastTapTime = Time.time;
            lastTapPosition = currentPos;

            // 按下开始拖拽
            ClearDragInfo();
            dragStartScreenPos = currentPos;
            dragStartWorldPos = GetWorldPoint(currentPos);
            canDrag = true;
            dragMoveTimer = 0;
            isProcessingDoubleTap = false;
            dragVelocitySmooth = Vector3.zero;
        }

        if (canDrag && Input.GetMouseButton(0) && !isProcessingDoubleTap)
        {
            Vector2 currentScreenPos = Input.mousePosition;

            float dragDistance = Vector2.Distance(currentScreenPos, dragStartScreenPos);
            if (dragDistance < settings.dragEffectDistance)
            {
                inertiaVelocity = Vector3.zero;
                return;
            }

            dragMoveTimer += Time.deltaTime;
            if (dragMoveTimer > settings.dragEffectTime)
            {
                isDragging = true;

                Vector3 currentWorldPos = GetWorldPoint(currentScreenPos);
                Vector3 delta = dragStartWorldPos - currentWorldPos;
                Vector3 targetPos = targetCamera.transform.position + delta * settings.dragSpeed;
                targetCamera.transform.position = Vector3.SmoothDamp(
                    targetCamera.transform.position,
                    targetPos,
                    ref dragVelocitySmooth,
                    settings.dragSmoothTime, // 平滑时间，值越小越跟手
                    Mathf.Infinity, // 最大速度
                    Time.deltaTime
                );

                dragStartWorldPos = GetWorldPoint(currentScreenPos);

                // 惯性速度计算：使用实际位移而不是delta 计算真实的速度（当前帧位置 - 上一帧位置）
                if (Time.deltaTime > 0)
                {
                    Vector3 actualVelocity = (targetCamera.transform.position - lastFramePosition) / Time.deltaTime;
                    inertiaVelocity = settings.dragInertiaSpeed * actualVelocity;
                }
            }
            else
            {
                inertiaVelocity = Vector3.zero;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isProcessingDoubleTap)
            {
                isProcessingDoubleTap = false;
                return;
            }

            isDragging = false;
            canDrag = false;
            isZooming = false;
            dragMoveTimer = 0;

            if (inertiaVelocity.magnitude > 0.05f)
            {
                inertiaTimer = settings.dragInertiaDuration;
                inertiaVelocity = Vector3.ClampMagnitude(inertiaVelocity, 10f); // 限制最大速度
            }
            else
            {
                inertiaVelocity = Vector3.zero;
                inertiaTimer = 0f;
            }
        }

        // // 鼠标滚轮缩放
        // if (!isSimulatingPinch)
        // {
        //     float scroll = Input.GetAxis("Mouse ScrollWheel");
        //     if (Mathf.Abs(scroll) > 0.01f)
        //     {
        //         targetSize -= scroll * settings.zoomSpeed * 10f;
        //         targetSize = Mathf.Clamp(targetSize, settings.minSize, settings.maxSize);
        //         isZooming = true;
        //     }
        // }
    }

    /// <summary>
    /// 处理触摸输入
    /// </summary>
    private void HandleTouchInput()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1)
        {
            touch0 = Input.GetTouch(0);

            switch (touch0.phase)
            {
                case TouchPhase.Began:
                    // 双击检测
                    float timeSinceLastTap = Time.time - lastTapTime;
                    if (timeSinceLastTap < settings.doubleTapTimeThreshold &&
                        Vector2.Distance(touch0.position, lastTapPosition) < settings.dragEffectDistance)
                    {
                        isProcessingDoubleTap = true;
                        lastTapTime = 0;
                        ClearDragInfo();
                        return;
                    }

                    lastTapTime = Time.time;
                    lastTapPosition = touch0.position;

                    ClearDragInfo();
                    dragStartScreenPos = touch0.position;
                    dragStartWorldPos = GetWorldPoint(touch0.position);
                    canDrag = true;
                    dragMoveTimer = 0;
                    isProcessingDoubleTap = false;
                    dragVelocitySmooth = Vector3.zero;
                    break;

                case TouchPhase.Moved:
                    if (canDrag && !isProcessingDoubleTap)
                    {
                        float dragDistance = Vector2.Distance(touch0.position, dragStartScreenPos);
                        if (dragDistance < settings.dragEffectDistance)
                        {
                            inertiaVelocity = Vector3.zero;
                            return;
                        }

                        dragMoveTimer += Time.deltaTime;
                        if (dragMoveTimer > settings.dragEffectTime)
                        {
                            isDragging = true;

                            Vector3 currentWorldPos = GetWorldPoint(touch0.position);
                            Vector3 delta = dragStartWorldPos - currentWorldPos;
                            Vector3 targetPos = targetCamera.transform.position + delta * settings.dragSpeed;
                            targetCamera.transform.position = Vector3.SmoothDamp(
                                targetCamera.transform.position,
                                targetPos,
                                ref dragVelocitySmooth,
                                settings.dragSmoothTime,
                                Mathf.Infinity,
                                Time.deltaTime
                            );

                            dragStartWorldPos = GetWorldPoint(touch0.position);

                            if (Time.deltaTime > 0)
                            {
                                Vector3 actualVelocity = (targetCamera.transform.position - lastFramePosition) / Time.deltaTime;
                                inertiaVelocity = settings.dragInertiaSpeed * actualVelocity;
                            }
                        }
                        else
                        {
                            inertiaVelocity = Vector3.zero;
                        }
                    }

                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isProcessingDoubleTap)
                    {
                        isProcessingDoubleTap = false;
                        return;
                    }

                    isDragging = false;
                    canDrag = false;
                    isZooming = false;
                    dragMoveTimer = 0;

                    if (inertiaVelocity.magnitude > 0.3f)
                    {
                        inertiaTimer = settings.dragInertiaDuration;
                        inertiaVelocity = Vector3.ClampMagnitude(inertiaVelocity, 10f);
                    }
                    else
                    {
                        inertiaVelocity = Vector3.zero;
                        inertiaTimer = 0f;
                    }

                    break;
            }
        }
        else if (touchCount == 2)
        {
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);

            prevPos0 = touch0.position - touch0.deltaPosition;
            prevPos1 = touch1.position - touch1.deltaPosition;

            float prevDistance = (prevPos0 - prevPos1).magnitude;
            float currentDistance = (touch0.position - touch1.position).magnitude;
            float distanceDelta = currentDistance - prevDistance;

            // CLog.Info($" InputCamera -------- Touch touch0:{touch0.position}, touch1:{touch1.position}, zoomDelta=={distanceDelta}");
            RecordZoomSizeCenter(touch0.position, touch1.position, distanceDelta);

            if ((touch0.phase is TouchPhase.Ended or TouchPhase.Canceled)
                && (touch1.phase is TouchPhase.Ended or TouchPhase.Canceled))
            {
                isZooming = false;
            }
            else
            {
                isZooming = true;
                ClearDragInfo();
                TryFinishGuideZoom();
            }
        }
    }

    /// <summary>
    /// 记录缩放中心点调整相机位置
    /// </summary>
    private void RecordZoomSizeCenter(Vector2 touchPos1, Vector2 touchPos2, float distanceDelta)
    {
        if (targetCamera == null)
            return;

        zoomCenterPos = (touchPos1 + touchPos2) * 0.5f;

        float zoomDelta = distanceDelta * settings.zoomSpeed;
        targetSize -= zoomDelta;
        targetSize = Mathf.Clamp(targetSize, settings.minSize, settings.maxSize);
    }

    /// <summary>
    /// 平滑缩放
    /// </summary>
    private void SmoothZoom()
    {
        if (targetCamera == null)
            return;

        if (NeedZoom() == false)
        {
            return;
        }

        Vector3 worldCenterBefore = GetWorldPoint(zoomCenterPos);

        targetCamera.orthographicSize = Mathf.SmoothDamp(
            targetCamera.orthographicSize,
            targetSize,
            ref zoomVelocity,
            settings.zoomSmoothTime
        );

        Vector3 worldCenterAfter = GetWorldPoint(zoomCenterPos);
        targetCamera.transform.position += (worldCenterBefore - worldCenterAfter);
    }

    private bool NeedZoom()
    {
        return Mathf.Abs(targetCamera.orthographicSize - targetSize) > 0.01f;
    }

    private void ClearDragInfo()
    {
        canDrag = false;
        isDragging = false;
        inertiaTimer = 0f;
        inertiaVelocity = Vector3.zero;
        dragStartWorldPos = Vector3.zero;
        dragStartScreenPos = Vector2.zero;
        dragMoveTimer = 0;
        isProcessingDoubleTap = false;
    }

    /// <summary>
    /// 应用惯性效果
    /// </summary>
    private void ApplyInertia()
    {
        if (isZooming || isDragging || isSimulatingPinch)
        {
            return;
        }

        if (inertiaTimer > 0 && inertiaVelocity.magnitude > 0.01f)
        {
            targetCamera.transform.position += inertiaVelocity * Time.deltaTime;

            float decayFactor = Mathf.Exp(-Time.deltaTime * 5f);
            // CLog.Info($" cameraSettings ------- ApplyInertia {inertiaVelocity}*{Time.deltaTime}, decay={decayFactor}");
            inertiaVelocity *= decayFactor;

            inertiaTimer -= Time.deltaTime;

            if (inertiaTimer <= 0 || inertiaVelocity.magnitude < 0.05f)
            {
                inertiaTimer = 0f;
                inertiaVelocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// 屏幕坐标转世界坐标
    /// </summary>
    private Vector3 GetWorldPoint(Vector2 screenPoint)
    {
        if (targetCamera == null)
            return Vector3.zero;

        Vector3 worldPoint = targetCamera.ScreenToWorldPoint(screenPoint);
        worldPoint.z = targetCamera.transform.position.z;
        return worldPoint;
    }

    /// <summary>
    /// 限制相机位置在边界内
    /// </summary>
    private void ClampCameraPosition()
    {
        if (targetCamera == null)
            return;

        // 计算有效边界
        float leftBound = settings.minBounds.x + horizontalSize;
        float rightBound = settings.maxBounds.x - horizontalSize;
        float bottomBound = settings.minBounds.y + verticalSize;
        float topBound = settings.maxBounds.y - verticalSize;

        Vector3 pos = targetCamera.transform.position;
        if (leftBound >= rightBound)
        {
            pos.x = (settings.minBounds.x + settings.maxBounds.x) * 0.5f;
        }
        else
        {
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        }

        if (bottomBound >= topBound)
        {
            pos.y = (settings.minBounds.y + settings.maxBounds.y) * 0.5f;
        }
        else
        {
            pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        }

        // 如果位置被边界限制，停止惯性
        if (Vector3.Distance(targetCamera.transform.position, pos) > 0.01f)
        {
            inertiaTimer = 0f;
            inertiaVelocity = Vector3.zero;
        }

        float precision = 1000f; // 防抖动 保留3位小数精度
        pos.x = Mathf.Round(pos.x * precision) / precision;
        pos.y = Mathf.Round(pos.y * precision) / precision;
        pos.z = Mathf.Round(pos.z * precision) / precision;

        targetCamera.transform.position = pos;
    }

    #region 公共API

    /// <summary>
    /// 停止相机移动
    /// </summary>
    public void StopAllMovement()
    {
        ClearDragInfo();
        isSimulatingPinch = false;
        isZooming = false;
        dragVelocitySmooth = Vector3.zero;
        inertiaVelocity = Vector3.zero;
        inertiaTimer = 0f;

        if (targetCamera != null)
        {
            targetSize = targetCamera.orthographicSize;
            lastFramePosition = targetCamera.transform.position;
        }
    }

    /// <summary>
    /// 获取控制器设置
    /// </summary>
    public InputCameraSettings GetSettings()
    {
        return settings;
    }

    public void FocusToCenter(Action onComplete = null)
    {
        Game.GetMod<ModCoroutine>().StartCoroutine(CoroutineFocus(_centerPosition, settings.maxSize, onComplete), ECoroutineBelongType.InGame);
    }

    /// <summary>
    /// 聚焦到某点
    /// </summary>
    public void Focus(Vector3 worldPos, Action onComplete = null)
    {
        var midSize = (settings.maxSize + settings.minSize) * 0.3f;
        Game.GetMod<ModCoroutine>().StartCoroutine(CoroutineFocus(worldPos, midSize, onComplete), ECoroutineBelongType.InGame);
    }

    private IEnumerator CoroutineFocus(Vector3 worldPos, float size, Action onComplete = null)
    {
        CanInput = false;

        float leftBound = settings.minBounds.x + horizontalSize;
        float rightBound = settings.maxBounds.x - horizontalSize;
        float bottomBound = settings.minBounds.y + verticalSize;
        float topBound = settings.maxBounds.y - verticalSize;

        worldPos.x = Mathf.Clamp(worldPos.x, leftBound, rightBound);
        worldPos.y = Mathf.Clamp(worldPos.y, bottomBound, topBound);
        worldPos.z = targetCamera.transform.position.z;

        Vector3 startPos = targetCamera.transform.position;
        float startSize = targetCamera.orthographicSize;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            t = Mathf.SmoothStep(0, 1, t);

            targetCamera.transform.position = Vector3.Lerp(startPos, worldPos, t);
            targetCamera.orthographicSize = Mathf.Lerp(startSize, size, t);

            yield return null;
        }

        targetCamera.transform.position = worldPos;
        targetCamera.orthographicSize = size;
        targetSize = size;

        StopAllMovement();
        CanInput = true;
        onComplete?.Invoke();
    }

    #endregion

    #region 引导

    public void DisplayGuideZoomAnimation(Action endCallback)
    {
        CanInput = false;
        _isGuidingZoom = true;
        var targetSize = (settings.maxSize + settings.minSize) * 0.5f;
        targetCamera.DOOrthoSize(targetSize, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            this.targetSize = targetCamera.orthographicSize;
            endCallback?.Invoke();
            InGameUtils.RegisterTimer(0.1f, onComplete: (v) =>
            {
                CanInput = true;
            }, ignoreTimeScale: false);
        });
    }

    private void TryFinishGuideZoom()
    {
        if (!_isGuidingZoom)
            return;

        if (NeedZoom())
        {
            var Mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame)?.Mode;
            if (Mode != null && Mode.Data != null)
                Mode.Data.TryFinishZoomGuide();
            _isGuidingZoom = false;
        }
    }

    #endregion

    public void Shake(float duration = 0.2f, float strength = 0.35f,
        int vibrato = 30, float randomness = 90f, bool fadeOut = true)
    {
        if (targetCamera == null)
            return;

        CanInput = false;
        var camTransform = targetCamera.transform;
        Vector3 originalPos = camTransform.localPosition;

        var sequence = DOTween.Sequence();
        sequence.Append(camTransform.DOShakePosition(duration, strength, vibrato, randomness, false, fadeOut)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                camTransform.localPosition = originalPos;
            }));
        // sequence.Join(camTransform.DOShakeRotation(duration * 0.8f, new Vector3(0, 0, 2f), vibrato / 2, randomness, fadeOut)
        //     .SetUpdate(true)
        //     .OnComplete(() =>
        //     {
        //         camTransform.localRotation = Quaternion.identity;
        //     }));
        sequence.SetUpdate(true);
        sequence.OnComplete(() =>
        {
            camTransform.localPosition = originalPos;
            camTransform.localRotation = Quaternion.identity;
            CanInput = true;
        });
    }
}