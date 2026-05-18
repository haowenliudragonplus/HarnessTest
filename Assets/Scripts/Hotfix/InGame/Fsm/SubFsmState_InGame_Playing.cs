using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class SubFsmState_InGame_Playing : FsmStateBase
{
    private InGameModeBase Mode;

    private CameraInputCtrl _cameraInput;

    private Collider2D[] hitColliderArray = new Collider2D[111];
    
    private float _mouseDownTime = 0f;
    private bool _isMouseDown = false;
    private const float LONG_PRESS_DURATION = 0.5f; // 长按时间阈值（秒）
    private bool _longPressTriggered = false;

    public override void OnEnter(FsmStateEnterParam enterParam = null)
    {
        base.OnEnter(enterParam);
        Mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;

        _cameraInput = Mode.CameraInput;
    }

    public override void OnFixedUpdate(float deltaTime)
    {
        base.OnFixedUpdate(deltaTime);
        Mode.Data.Update(deltaTime);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        HandleClick();
    }

    public override void OnLateUpdate(float deltaTime)
    {
        base.OnLateUpdate(deltaTime);
        _cameraInput.LateUpdate();
        // Mode.CameraComponent.OnLateUpdate(deltaTime);
    }

    private void HandleClick()
    {
        if (!Mode.Data.canOperate)
            return;

        if (Game.GetMod<ModABTest>().GetABTestGroup(EABTestType.LongPress) == EABTestGroup.Group2)
        {
            // 优先处理移动端触摸输入
            if (Application.isMobilePlatform && Input.touchCount > 0)
            {
                HandleTouchInput();
            }
            else
            {
                HandleMouseInput();
            }
        }
        else
        {
            CommonClick();
        }
        
    }
    
    private void CommonClick()
    { 
        if (!Input.GetMouseButtonUp(0))
            return;
        if (UIUtils.IsPointerOverUI())
            return;
        if (_cameraInput.IsUserControlling())
            return;
        if (Mode.Data.IsGuidingCameraZoom)
        {
            CLog.Info(" 关卡正在引导 guide_102, 屏蔽点击");
            return;
        }

        var mouseWorldPos = CTUtils.Screen2World(Input.mousePosition, Mode.ElementCamera);
        // 点击特效
        GameObject effectGo = InGameUtils.PlayEffect(InGameConst.Prefab_Click, new Vector2(mouseWorldPos.x, mouseWorldPos.y),
            layerName: InGameConst.LayerName_InGame_Element, autoDestoryTime: 5f);
        effectGo.transform.localScale = Vector3.one * 5;
        SpriteRendererUtils.SetSoringLayer(effectGo, CommonConst.SortingLayer_Layer5);
        Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_click);
        //
        int hitCount = Physics2D.OverlapCircleNonAlloc(mouseWorldPos, InGameConst.PointSpacing - 0.1f, hitColliderArray
            , LayerMask.GetMask(InGameConst.LayerName_InGame_Element));
        var bestArrowEntity = FindBestArrowEntity(mouseWorldPos, hitCount);
        if (bestArrowEntity == null)
            return;

        bestArrowEntity?.OnSelected();
    }
    
    private void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _mouseDownTime = Time.time;
                _isMouseDown = true;
                _longPressTriggered = false;
                
                if (hintLineRenderer != null)
                {
                    Object.Destroy(hintLineRenderer.gameObject);
                    hintLineRenderer = null;
                }
                break;
                
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                // 检查是否处于长按状态且已达到阈值
                if (_isMouseDown && !_longPressTriggered && 
                    (Time.time - _mouseDownTime >= LONG_PRESS_DURATION))
                {
                    // 只在长按触发时检查是否为多指触摸
                    if (Input.touchCount >= 2)
                    {
                        // 双指或多指，不触发长按
                        _isMouseDown = false;
                        _longPressTriggered = false;
                        if (hintLineRenderer != null)
                        {
                            Object.Destroy(hintLineRenderer.gameObject);
                            hintLineRenderer = null;
                        }
                    }
                    else
                    {
                        // 单指，触发长按
                        ProcessLongPress();
                        _longPressTriggered = true;
                    }
                }
                break;
                
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _isMouseDown = false;
                
                // 如果按下时间短于长按阈值，则视为普通点击
                if (!_longPressTriggered && 
                    (Time.time - _mouseDownTime < LONG_PRESS_DURATION))
                {
                    ProcessNormalClick();
                }
                
                _longPressTriggered = false;
                
                if (hintLineRenderer != null)
                {
                    Object.Destroy(hintLineRenderer.gameObject);
                    hintLineRenderer = null;
                }
                break;
        }
    }
   private void HandleMouseInput()
    {
        if (!Mode.Data.canOperate)
            return;
            
        // 检测鼠标按下
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDownTime = Time.time;
            _isMouseDown = true;
            if (hintLineRenderer!=null)
            {
                Object.Destroy(hintLineRenderer.gameObject);
                hintLineRenderer = null;
            }
        }
        
        // 检测鼠标释放
        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
            
            // 如果按下时间短于长按阈值，则视为普通点击
            if (Time.time - _mouseDownTime < LONG_PRESS_DURATION)
            {
                ProcessNormalClick();
            }
            _longPressTriggered = false;
            if (hintLineRenderer!=null)
            {
                Object.Destroy(hintLineRenderer.gameObject);
                hintLineRenderer = null;
            }
        }
        // 检查是否处于长按状态且已达到阈值
        if (_isMouseDown && !_longPressTriggered && (Time.time - _mouseDownTime >= LONG_PRESS_DURATION))
        {
            ProcessLongPress();
            _longPressTriggered = true; // 标记长按已触发，防止重复执行
        }
    }
   
    private LineRenderer hintLineRenderer;
   
    private void ProcessNormalClick()
    {
        if (UIUtils.IsPointerOverUI())
            return;
        if (_cameraInput.IsUserControlling())
            return;
        if (Mode.Data.IsGuidingCameraZoom)
        {
            CLog.Info(" 关卡正在引导 guide_102, 屏蔽点击");
            return;
        }
        var mouseWorldPos = CTUtils.Screen2World(Input.mousePosition, Mode.ElementCamera);
        // 点击特效
        GameObject effectGo = InGameUtils.PlayEffect(InGameConst.Prefab_Click, new Vector2(mouseWorldPos.x, mouseWorldPos.y),
            layerName: InGameConst.LayerName_InGame_Element, autoDestoryTime: 5f);
        effectGo.transform.localScale = Vector3.one * 5;
        SpriteRendererUtils.SetSoringLayer(effectGo, CommonConst.SortingLayer_Layer5);
        Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_click);
        //
        int hitCount = Physics2D.OverlapCircleNonAlloc(mouseWorldPos, InGameConst.PointSpacing - 0.1f, hitColliderArray
            , LayerMask.GetMask(InGameConst.LayerName_InGame_Element));
        var bestArrowEntity = FindBestArrowEntity(mouseWorldPos, hitCount);
        if (bestArrowEntity == null)
            return;
        bestArrowEntity?.OnSelected();
    }
    
    private void ProcessLongPress()
    {
        var mouseWorldPos = CTUtils.Screen2World(Input.mousePosition, Mode.ElementCamera);
        CLog.Info("检测到长按操作！");
        int hitCount = Physics2D.OverlapCircleNonAlloc(mouseWorldPos, InGameConst.PointSpacing - 0.1f, hitColliderArray
            , LayerMask.GetMask(InGameConst.LayerName_InGame_Element));
        var bestArrowEntity = FindBestArrowEntity(mouseWorldPos, hitCount);
        if (bestArrowEntity == null)
            return;
        hintLineRenderer = bestArrowEntity.RefreshMoveHintLine();
    }

    private ArrowEntity FindBestArrowEntity(Vector2 clickPos, int hitCount)
    {
        ArrowEntity bestArrow = null;
        float bestScore = float.MaxValue;

        if (hitCount == 0
            || hitColliderArray == null
            || hitColliderArray.Length <= 0)
            return bestArrow;


        for (int i = 0; i < hitCount; i++)
        {
            var hitCol = hitColliderArray[i];
            if (hitCol == null)
                continue;

            var arrowEntity = InGameRayUtils.GetEntity(hitCol);
            if (arrowEntity == null)
                continue;

            var colWorldCenter = hitCol is BoxCollider2D
                ? hitCol.transform.position + (Vector3)(hitCol as BoxCollider2D).offset
                : hitCol.transform.position;
            float distance = Vector2.Distance(colWorldCenter, clickPos);
            bool canCompleteMove = arrowEntity.ArrowData.CheckCanCompleteMove();

            float score = distance;
            if (!canCompleteMove)
            {
                score += 11111;
            }

            if (bestArrow == null
                || score < bestScore)
            {
                bestArrow = arrowEntity;
                bestScore = score;
            }
        }
        return bestArrow;
    }

    public override void OnExit()
    {
        base.OnExit();
        _cameraInput.StopAllMovement();
        if (hintLineRenderer != null)
        {
            Object.Destroy(hintLineRenderer.gameObject);
            hintLineRenderer = null;
        }
    }
}
