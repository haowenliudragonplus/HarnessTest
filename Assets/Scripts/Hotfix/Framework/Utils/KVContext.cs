using System.Collections.Generic;
using UnityEngine;

public class KVContext
{
    protected Dictionary<string, int> _intValues = new Dictionary<string, int>();
    protected Dictionary<string, float> _floatValues = new Dictionary<string, float>();
    protected Dictionary<string, string> _stringValues = new Dictionary<string, string>();
    protected Dictionary<string, bool> _boolValues = new Dictionary<string, bool>();
    protected Dictionary<string, object> _objectValues = new Dictionary<string, object>();
    protected Dictionary<string, Vector3> _vectorValues = new Dictionary<string, Vector3>();

    public void Reset()
    {
        _intValues.Clear();
        _floatValues.Clear();
        _stringValues.Clear();
        _boolValues.Clear();
        _objectValues.Clear();
        _vectorValues.Clear();
    }

    public int Set(string key, int value)
    {
        _intValues[key] = value;
        return value;
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        int value = defaultValue;
        _intValues.TryGetValue(key, out value);
        return value;
    }

    public float Set(string key, float value)
    {
        _floatValues[key] = value;
        return value;
    }

    public float GetFloat(string key, float defaultValue = 0)
    {
        float value = defaultValue;
        _floatValues.TryGetValue(key, out value);
        return value;
    }

    public string Set(string key, string value)
    {
        _stringValues[key] = value;
        return value;
    }

    public string GetString(string key, string defaultValue = "")
    {
        string value = defaultValue;
        _stringValues.TryGetValue(key, out value);
        return value;
    }

    public bool Set(string key, bool value)
    {
        _boolValues[key] = value;
        return value;
    }

    public bool GetBool(string key, bool defaultValue = false)
    {
        bool value = defaultValue;
        _boolValues.TryGetValue(key, out value);
        return value;
    }

    public object Set(string key, object value)
    {
        _objectValues[key] = value;
        return value;
    }

    public object GetObject(string key)
    {
        object value = null;
        _objectValues.TryGetValue(key, out value);
        return value;
    }

    public T GetObject<T>(string key)
    {
        object value = null;
        _objectValues.TryGetValue(key, out value);
        return (T)value;
    }

    public Vector3 Set(string key, Vector3 value)
    {
        _vectorValues[key] = value;
        return value;
    }

    public Vector3 GetVector(string key, Vector3 defaultValue)
    {
        Vector3 value = defaultValue;
        _vectorValues.TryGetValue(key, out value);
        return value;
    }

    public bool HasKey(string key)
    {
        if (_intValues.ContainsKey(key))
            return true;
        if (_floatValues.ContainsKey(key))
            return true;
        if (_stringValues.ContainsKey(key))
            return true;
        if (_boolValues.ContainsKey(key))
            return true;
        if (_objectValues.ContainsKey(key))
            return true;
        if (_vectorValues.ContainsKey(key))
            return true;
        return false;
    }
}

public class KVContentKey
{
    
}