using UnityEngine;

public struct SimpleVector3
{
    public float x;
    public float y;
    public float z;

    public static SimpleVector3 Zero => new SimpleVector3(0, 0, 0);
    public static SimpleVector3 One => new SimpleVector3(1, 1, 1);

    public SimpleVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public SimpleVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator Vector3(SimpleVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public static implicit operator SimpleVector3(Vector3 v)
    {
        return new SimpleVector3(v);
    }

    public static SimpleVector3 operator +(SimpleVector3 v1, SimpleVector3 v2)
    {
        float x = v1.x + v2.x;
        float y = v1.y + v2.y;
        float z = v1.z + v2.z;
        var ret = new SimpleVector3(x, y, z);
        return ret;
    }

    public static SimpleVector3 operator -(SimpleVector3 v1, SimpleVector3 v2)
    {
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;
        float z = v1.z - v2.z;
        var ret = new SimpleVector3(x, y, z);
        return ret;
    }

    public override string ToString()
    {
        var ret = $"({x},{y},{z})";
        return ret;
    }
}