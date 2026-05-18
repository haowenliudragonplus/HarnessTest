using UnityEngine;

public struct SimpleVector2
{
    public float x;
    public float y;

    public static SimpleVector2 Zero => new SimpleVector2(0, 0);
    public static SimpleVector2 One => new SimpleVector2(1, 1);

    public SimpleVector2(Vector2 v)
    {
        x = v.x;
        y = v.y;
    }

    public SimpleVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(SimpleVector2 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static implicit operator Vector3(SimpleVector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static implicit operator SimpleVector2(Vector2 v)
    {
        return new SimpleVector2(v);
    }

    public static SimpleVector2 operator +(SimpleVector2 v1, SimpleVector2 v2)
    {
        float x = v1.x + v2.x;
        float y = v1.y + v2.y;
        var ret = new SimpleVector2(x, y);
        return ret;
    }

    public static SimpleVector2 operator -(SimpleVector2 v1, SimpleVector2 v2)
    {
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;
        var ret = new SimpleVector2(x, y);
        return ret;
    }

    public override string ToString()
    {
        var ret = $"({x},{y})";
        return ret;
    }
}