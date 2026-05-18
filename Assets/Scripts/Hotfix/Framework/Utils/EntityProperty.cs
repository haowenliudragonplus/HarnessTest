using System.Collections.Generic;

public class EntityProperty<T>
{
    protected T sum = default;
    protected Dictionary<int, T> multiValue = new Dictionary<int, T>();

    public void Add(int key, T value)
    {
        OnAdd(key, value);
    }

    public void Minus(int key, T value)
    {
        OnMinus(key, value);
    }

    public void Set(int key, T value)
    {
        OnSet(key, value);
    }

    public void Set(T value)
    {
        multiValue[0] = value;
        sum = value;
    }

    public T Get(int key)
    {
        var value = default(T);
        multiValue.TryGetValue(key, out value);
        return value;
    }

    public T Get()
    {
        return sum;
    }

    public void Remove(int key)
    {
        OnRemove(key);
    }

    protected virtual void OnSet(int key, T value)
    {
    }

    protected virtual void OnRemove(int key)
    {
    }

    protected virtual void OnAdd(int key, T value)
    {
    }

    protected virtual void OnMinus(int key, T value)
    {
    }
}

public class IntProperty : EntityProperty<int>
{
    protected override void OnSet(int key, int value)
    {
        Remove(key);
        Add(key, value);
    }

    protected override void OnRemove(int key)
    {
        var value = Get(key);
        multiValue.Remove(key);
        sum -= value;
    }

    protected override void OnAdd(int key, int value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] += value;
        sum += value;
    }

    protected override void OnMinus(int key, int value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] -= value;
        sum -= value;
    }
}

public class FloatProperty : EntityProperty<float>
{
    protected override void OnSet(int key, float value)
    {
        Remove(key);
        Add(key, value);
    }

    protected override void OnRemove(int key)
    {
        var value = Get(key);
        multiValue.Remove(key);
        sum -= value;
    }

    protected override void OnAdd(int key, float value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] += value;
        sum += value;
    }

    protected override void OnMinus(int key, float value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] -= value;
        sum -= value;
    }
}

public class DoubleProperty : EntityProperty<double>
{
    protected override void OnSet(int key, double value)
    {
        Remove(key);
        Add(key, value);
    }

    protected override void OnRemove(int key)
    {
        var value = Get(key);
        multiValue.Remove(key);
        sum -= value;
    }

    protected override void OnAdd(int key, double value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] += value;
        sum += value;
    }

    protected override void OnMinus(int key, double value)
    {
        if (!multiValue.TryGetValue(key, out var v))
            multiValue[key] = 0;

        multiValue[key] -= value;
        sum -= value;
    }
}

public class BoolProperty : EntityProperty<bool>
{
    protected override void OnSet(int key, bool value)
    {
        multiValue[key] = value;
        bool ret = true;
        foreach (var v in multiValue)
        {
            ret &= v.Value;
        }
        sum = ret;
    }

    protected override void OnRemove(int key)
    {
        multiValue.Remove(key);
        if (multiValue.Count <= 0)
        {
            sum = false;
        }
        else
        {
            bool ret = true;
            foreach (var v in multiValue)
            {
                ret &= v.Value;
            }
            sum = ret;
        }
    }
}