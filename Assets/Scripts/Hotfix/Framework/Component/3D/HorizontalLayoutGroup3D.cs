using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public enum EChildAlignmentType
{
    MiddleLeft = 1,
    MiddleCenter,
    MiddleRight,
}

/// <summary>
/// 3D水平布局组件
/// </summary>
[ExecuteInEditMode]
public class HorizontalLayoutGroup3D : MonoBehaviour
{
    public EChildAlignmentType alignmentType;
    public float gridX;
    public float space;
    public float paddingLeft;
    public float paddingRight;
    public bool canScroll; //能否滑动
    public string cameraName; //相机名称

    private float startPosX;
    private float minX;
    private float maxX;

    private Camera camera;
    private float screenWidth;
    private Vector3 lastWorldPos;

    private void Start()
    {
        if (canScroll)
        {
            camera = GameObject.Find(cameraName)?.GetComponent<Camera>();
            if (camera == null)
            {
                CLog.Error($"当前场景中没有相机 [{cameraName}]");
            }
            Vector2 screenMaxPos_World = CTUtils.Viewport2World(Vector3.one, camera);
            Vector2 screenMinPos_World = CTUtils.Viewport2World(Vector3.zero, camera);
            screenWidth = screenMaxPos_World.x - screenMinPos_World.x;
        }
    }

    private void Update()
    {
        CalcStartPosX();
        UpdateChildPos();
    }

    private void CalcStartPosX()
    {
        var com_SpriteRenderer = GetComponent<SpriteRenderer>();
        switch (alignmentType)
        {
            case EChildAlignmentType.MiddleLeft:
                if (com_SpriteRenderer == null)
                {
                    startPosX = -transform.localScale.x / 2;
                }
                else
                {
                    startPosX = -Mathf.Max(transform.localScale.x / 2, com_SpriteRenderer.size.x / 2);
                }
                startPosX += gridX / 2;
                startPosX += paddingLeft;
                break;

            case EChildAlignmentType.MiddleRight:
                if (com_SpriteRenderer == null)
                {
                    startPosX = transform.localScale.x / 2;
                }
                else
                {
                    startPosX = Mathf.Max(transform.localScale.x / 2, com_SpriteRenderer.size.x / 2);
                }
                startPosX -= gridX / 2;
                startPosX -= paddingRight;
                break;
        }
    }

    private void UpdateChildPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
                continue;
            transform.GetChild(i).transform.localPosition = new Vector3(startPosX, 0); //y先固定是0，中间
            switch (alignmentType)
            {
                case EChildAlignmentType.MiddleLeft:
                    startPosX += gridX + space;
                    break;

                case EChildAlignmentType.MiddleRight:
                    startPosX -= (gridX + space);
                    break;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!canScroll)
            return;
        lastWorldPos = CTUtils.Screen2World(Input.mousePosition, camera);
    }

    private void OnMouseDrag()
    {
        if (!canScroll)
            return;
        CalcBorderX();
        var curWorldPos = CTUtils.Screen2World(Input.mousePosition, camera);
        Vector3 deltaPos = curWorldPos - lastWorldPos;
        deltaPos.y = 0;
        var targetPos = transform.position + deltaPos;
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        transform.position = targetPos;
        lastWorldPos = curWorldPos;
    }

    /// <summary>
    /// 计算X边界
    /// </summary>
    private void CalcBorderX()
    {
        switch (alignmentType)
        {
            case EChildAlignmentType.MiddleLeft:
                maxX = 0;
                float totolWidth = paddingLeft + transform.childCount * gridX + (transform.childCount - 1) * space + paddingRight;
                minX = totolWidth - screenWidth <= 0
                    ? 0
                    : -(totolWidth - screenWidth);
                break;
        }
    }
}