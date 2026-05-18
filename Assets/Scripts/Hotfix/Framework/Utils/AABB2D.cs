using UnityEngine;

public class AABB2D
{
    public Vector2 minPoint;
    public Vector2 maxPoint;

    public AABB2D(Vector2 minPoint, Vector2 maxPoint)
    {
        this.minPoint = minPoint;
        this.maxPoint = maxPoint;
    }

    public AABB2D(Collider2D collider)
    {
        Bounds bounds = collider.bounds;
        minPoint = bounds.min;
        maxPoint = bounds.max;
    }

    public float Width()
    {
        return maxPoint.x - minPoint.x;
    }

    public float Height()
    {
        return maxPoint.y - minPoint.y;
    }

    /// <summary>
    /// 是否相交
    /// </summary>
    public bool IsIntersects(AABB2D other)
    {
        if (!IsValid() || !other.IsValid())
            return false;
        return maxPoint.x >= other.minPoint.x
               && maxPoint.y >= other.minPoint.y
               && other.maxPoint.x >= minPoint.x
               && other.maxPoint.y >= minPoint.y;
    }

    /// <summary>
    /// 检查是否合法
    /// </summary>
    public bool IsValid()
    {
        return maxPoint.x >= minPoint.x && maxPoint.y >= minPoint.y;
    }

    public static AABB2D operator +(AABB2D other, Vector2 point)
    {
        Vector2 maxPoint = other.maxPoint + point;
        Vector2 minPoint = other.minPoint + point;
        AABB2D aabb2D = new AABB2D(minPoint, maxPoint);
        return aabb2D;
    }

    public static AABB2D operator -(AABB2D other, Vector2 point)
    {
        Vector2 maxPoint = other.maxPoint - point;
        Vector2 minPoint = other.minPoint - point;
        AABB2D aabb2D = new AABB2D(minPoint, maxPoint);
        return aabb2D;
    }
}