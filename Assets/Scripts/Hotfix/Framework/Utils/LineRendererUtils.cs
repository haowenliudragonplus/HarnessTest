using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Framework;
using UnityEngine;
using Color = UnityEngine.Color;

public static class LineRendererUtils
{
    /// <summary>
    /// 创建LineRenderer
    /// </summary>
    public static LineRenderer CreateLineRenderer(Transform parent, float width, Color color, List<Vector3> pointList,
        string sortingLayerName, string layerName, int sortingOrder = 0, bool useWorldSpace = true, string lineRendererGoName = "LineRenderer",
        Vector3 pointOffset = default, int cornerVetices = 0, int endCapVertices = 0)
    {
        if (parent == null)
        {
            CLog.Error("创建LineRenderer失败，parent不能为null");
            return null;
        }
        if (pointList == null)
        {
            CLog.Error("创建LineRenderer失败，pointList不能为null");
            return null;
        }
        if (pointList.Count <= 0)
        {
            CLog.Error("创建LineRenderer失败，pointList个数不能为0");
            return null;
        }
        Shader shader = Shader.Find("Sprites/Default");
        // Shader shader = Shader.Find("Unlit/Color");
        Material mat = new Material(shader);
        GameObject lineRenderGo = GameUtils.CreateGameObject(lineRendererGoName, parent, false, typeof(LineRenderer));
        var com_LineRenderer = lineRenderGo.GetComponent<LineRenderer>();
        lineRenderGo.layer = LayerMask.NameToLayer(layerName);
        com_LineRenderer.material = mat;
        com_LineRenderer.useWorldSpace = useWorldSpace;
        com_LineRenderer.startWidth = width;
        com_LineRenderer.endWidth = width;
        com_LineRenderer.startColor = color;
        com_LineRenderer.endColor = color;
        com_LineRenderer.positionCount = 0;
        com_LineRenderer.sortingLayerName = sortingLayerName;
        com_LineRenderer.sortingOrder = sortingOrder;
        com_LineRenderer.numCornerVertices = cornerVetices;
        com_LineRenderer.numCapVertices = endCapVertices;
        foreach (var point in pointList)
        {
            Vector3 v = point + pointOffset;
            com_LineRenderer.SetPosition(com_LineRenderer.positionCount++, v);
        }
        return com_LineRenderer;
    }

    public static void SetLineRenderer(LineRenderer lineRenderer, List<Vector3> pointList)
    {
        if (lineRenderer == null)
        {
            CLog.Error("LineRenderer不能为null");
            return;
        }
        if (pointList == null)
        {
            CLog.Error("设置LineRenderer失败，pointList不能为null");
            return;
        }

        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }

    public static void SetColor(LineRenderer lineRenderer, Color color)
    {
        if (lineRenderer == null)
        {
            CLog.Error("LineRenderer不能为null");
            return;
        }
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    public static void SetWidth(LineRenderer lineRenderer, float width)
    {
        if (lineRenderer == null)
        {
            CLog.Error("LineRenderer不能为null");
            return;
        }
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    public static float LineAnimSpeed { get; set; } = 150f; // 线条入场速度
    public static int PointsPerSegment { get; set; } = 2; // 相邻2点间插值个数

    public static Tweener PlayAnimateLine(LineRenderer line, float drawTime, Action onComplete = null)
    {
        if (line == null)
        {
            CLog.Error("LineRenderer不能为null");
            return null;
        }

        // 获取原始点
        Vector3[] originalPoints = new Vector3[line.positionCount];
        line.GetPositions(originalPoints);
        // CLog.Info($"原始点数量: {originalPoints.Length}");
        
        var originalNumCornerVertices = line.numCornerVertices;
        var originalNumCapVertices = line.numCapVertices;
        line.numCornerVertices = 12;
        line.numCapVertices = 10;

        // 插值生成平滑的点 每段插值5个点
        Vector3[] smoothPoints = GenerateSmoothPoints(originalPoints, PointsPerSegment);
        // CLog.Info($"平滑后点数量: {smoothPoints.Length}");

        // 先隐藏线条 - 只显示最后一个点
        line.positionCount = 1;
        line.SetPosition(0, smoothPoints[smoothPoints.Length - 1]);

        float progress = 0.5f;

        var tween = DOTween.To(() => progress, x =>
            {
                if (!line)
                    return;
                progress = x;
                int totalPoints = smoothPoints.Length;
                int visiblePoints = Mathf.CeilToInt(progress * totalPoints);
                // 限制显示范围
                visiblePoints = Mathf.Clamp(visiblePoints, 2, totalPoints);
                line.positionCount = visiblePoints;
                // 从后往前填充点（尾部到头部动画）
                for (int i = 0; i < visiblePoints; i++)
                {
                    int sourceIndex = totalPoints - visiblePoints + i;
                    line.SetPosition(i, smoothPoints[sourceIndex]);
                }
            }, 1f, drawTime)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (line)
                {
                    // 动画完成后恢复原始点
                    line.numCornerVertices = originalNumCornerVertices;
                    line.numCapVertices = originalNumCapVertices;
                    line.positionCount = originalPoints.Length;
                    for (int i = 0; i < originalPoints.Length; i++)
                    {
                        line.SetPosition(i, originalPoints[i]);
                    }
                }

                onComplete?.Invoke();
            });
        return tween;
    }

    /// <summary>
    /// 生成平滑的点序列（线性插值）
    /// </summary>
    /// <param name="originalPoints">原始点</param>
    /// <param name="pointsPerSegment">每段之间的插值点数</param>
    /// <returns>平滑后的点数组</returns>
    public static Vector3[] GenerateSmoothPoints(Vector3[] originalPoints, int pointsPerSegment)
    {
        if (originalPoints.Length < 2 || pointsPerSegment < 1)
            return originalPoints;

        // 计算总点数
        int totalSegments = originalPoints.Length - 1;
        int totalPoints = totalSegments * pointsPerSegment + 1;

        Vector3[] smoothPoints = new Vector3[totalPoints];

        // 生成平滑点
        int pointIndex = 0;

        for (int segment = 0; segment < totalSegments; segment++)
        {
            Vector3 startPoint = originalPoints[segment];
            Vector3 endPoint = originalPoints[segment + 1];

            // 在每一段之间生成插值点
            for (int i = 0; i < pointsPerSegment; i++)
            {
                float t = i / (float)pointsPerSegment;
                smoothPoints[pointIndex] = Vector3.Lerp(startPoint, endPoint, t);
                pointIndex++;
            }
        }

        // 添加最后一个点
        smoothPoints[totalPoints - 1] = originalPoints[originalPoints.Length - 1];

        return smoothPoints;
    }

}