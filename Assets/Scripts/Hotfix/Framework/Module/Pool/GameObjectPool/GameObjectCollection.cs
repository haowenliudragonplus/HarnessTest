using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 游戏物体对象池
/// </summary>
public class GameObjectCollection
{
    private Queue<GameObject> freeQueue = new Queue<GameObject>(); //空闲物体
    private HashSet<GameObject> inUseQueue = new HashSet<GameObject>(); //使用中的物体
    private Func<GameObject> customInstantiateFun; //自定义实例化对象的方法

    private Transform parentTrans; //父节点
    private GameObject templatePrefab; //模板预制体

    public EGameObjectPoolType GameObjectPoolType { get; private set; } //池子归属类型
    public string PoolKey { get; private set; } //池子key（物体加载路径）
    public int Capacity { get; private set; } //-1表示无限容量
    public bool CheckIdle { get; private set; } //是否检测空闲
    public int MaxIdleTime { get; private set; } //最长的空闲时间（默认1小时）
    public DateTime LastActiveTime { get; private set; } //最后一次活动时间

    public int FreeCount => freeQueue.Count; //空闲的数量
    public int InUseCount => inUseQueue.Count; //使用中的数量
    public int TotalInstantiateCount { get; private set; } //总实例化的数量

    private readonly object _lockObj = new object();

    public GameObjectCollection(string poolKey, EGameObjectPoolType gameObjectPoolType, Transform typeRootTrans)
    {
        this.GameObjectPoolType = gameObjectPoolType;
        this.PoolKey = poolKey;

        this.Capacity = GameObjectPoolConfiguration.DefaultCapacity;
        CheckIdle = true;
        MaxIdleTime = GameObjectPoolConfiguration.MaxIdleTime;

        parentTrans = new GameObject().transform;
        parentTrans.name = "Root_" + poolKey;
        parentTrans.transform.SetParent(typeRootTrans, false);
        parentTrans.transform.localPosition = Vector3.zero;

        TotalInstantiateCount = 0;

        RecordActiveTime();
    }

    #region 获取

    public GameObject Get(bool active = true)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                return InternalGet(active);
        return InternalGet(active);
    }

    #endregion 获取

    #region 放回

    public bool Put(GameObject go, string forcePoolKey = "")
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                return InternalPut(go, forcePoolKey);
        return InternalPut(go, forcePoolKey);
    }

    #endregion 放回

    #region 添加

    public bool Add(int count)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                return InternalAdd(count);
        return InternalAdd(count);
    }

    #endregion 添加

    #region 移除

    public void Remove(int count)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalRemove(count);
        else
            InternalRemove(count);
    }

    #endregion 移除

    #region 销毁

    public void DisposeFree()
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalDisposeFree();
        else
            InternalDisposeFree();
    }

    #endregion 销毁

    #region 外部工具

    public void SetCapacity(int capacity)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalSetCapacity(capacity);
        else
            InternalSetCapacity(capacity);
    }

    public void SetCustomInstantiateFun(Func<GameObject> customInstantiateFun)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalSetCustomInstantiateFun(customInstantiateFun);
        else
            InternalSetCustomInstantiateFun(customInstantiateFun);
    }

    public void SetEnableCheckIdle(bool checkIdle)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalSetCheckIdle(checkIdle);
        else
            InternalSetCheckIdle(checkIdle);
    }

    public void SetMaxIdleTime(int maxIdleTime)
    {
        if (GameObjectPoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalSetMaxIdleTime(maxIdleTime);
        else
            InternalSetMaxIdleTime(maxIdleTime);
    }

    #endregion 外部工具

    private GameObject Instantiate(bool active)
    {
        GameObject newGo = null;
        if (customInstantiateFun == null)
        {
            if (templatePrefab == null)
            {
                templatePrefab = Game.GetMod<ModAsset>().GetRes<GameObject>(PoolKey).GetInstance(parentTrans.gameObject); //TODO logic 根据项目的资源加载接口修改
                if (templatePrefab == null)
                {
                    CLog.Error($"资源加载失败，池子key：{PoolKey}");
                    return null;
                }
            }
            newGo = Object.Instantiate(templatePrefab);
        }
        else
        {
            newGo = customInstantiateFun?.Invoke();
        }
        if (newGo == null)
            return null;

        newGo.name = PoolKey;
        newGo.transform.SetParent(parentTrans, false);
        newGo.SetActive(active);
        return newGo;
    }

    private void DestorySingle(GameObject go)
    {
        Object.Destroy(go);
    }

    private bool IsFull()
    {
        bool isFull = Capacity >= 0 && freeQueue.Count >= Capacity;
        return isFull;
    }

    private void RecordActiveTime()
    {
        LastActiveTime = DateTime.Now;
    }

    public string GetInfo()
    {
        string infoStr = $"池子key：{PoolKey}，池子类型；{GameObjectPoolType}，容量：{Capacity}，" +
                         $"空闲的数量：{FreeCount}，使用中的数量：{InUseCount}，总创建的数量：{TotalInstantiateCount}，" +
                         $"最后活动时间：{LastActiveTime}";
        return infoStr;
    }

    #region 内部实现

    private GameObject InternalGet(bool active = true)
    {
        if (freeQueue.Count <= 0)
        {
            bool ret = Add(1);
            if (ret == false)
            {
                CLog.Error($"获取失败，池子中没有可用的游戏物体并且池子容量已满，无法添加，池子key：{PoolKey}");
                return null;
            }
        }

        var go = freeQueue.Dequeue();
        inUseQueue.Add(go);
        go.SetActive(active);
        go.transform.SetParent(null, false);

        RecordActiveTime();

        return go;
    }

    private bool InternalPut(GameObject go, string forcePoolKey = "")
    {
        if (go == null)
            return false;
        if (string.IsNullOrEmpty(forcePoolKey) && go.name != PoolKey)
        {
            DestorySingle(go);
            CLog.Error($"放回失败，不属于此池子，直接销毁，传入的游戏物体名：{go.name}，池子key：{PoolKey}");
            return false;
        }
        if (freeQueue.Contains(go))
        {
            CLog.Error($"放回失败，已经被回收了，池子key：{PoolKey}");
            return false;
        }
        if (!inUseQueue.Contains(go))
        {
            DestorySingle(go);
            CLog.Error($"放回失败，不是此池子创建的游戏物体，直接销毁，池子key：{PoolKey}");
            return false;
        }
        if (!string.IsNullOrEmpty(forcePoolKey) && forcePoolKey != PoolKey)
        {
            inUseQueue.Remove(go);
            DestorySingle(go);
            CLog.Error($"放回失败，不属于此池子，直接销毁，传入的池子key：{go.name}，池子key：{PoolKey}");
            return false;
        }
        if (IsFull())
        {
            inUseQueue.Remove(go);
            DestorySingle(go);
            CLog.Error($"放回失败，池子容量已满，直接销毁，池子key：{PoolKey}");
            return false;
        }

        inUseQueue.Remove(go);
        freeQueue.Enqueue(go);
        go.transform.SetParent(parentTrans, false);
        go.transform.localPosition = Vector3.zero;
        go.name = string.IsNullOrEmpty(forcePoolKey)
            ? PoolKey
            : forcePoolKey;
        go.SetActive(false);

        RecordActiveTime();

        return true;
    }

    private bool InternalAdd(int count)
    {
        if (count <= 0)
            return false;
        if (IsFull())
        {
            CLog.Error($"池子容量已满，无法添加，池子key：{PoolKey}");
            return false;
        }

        var leftCount = Capacity < 0 ? count : Capacity - freeQueue.Count;
        count = Mathf.Min(count, leftCount);
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(false);
            freeQueue.Enqueue(obj);
        }
        if (count > leftCount)
        {
            CLog.Error($"池子容量已满，其中{count - leftCount}个游戏物体无法创建，池子key：{PoolKey}");
        }
        return true;
    }

    private void InternalRemove(int count)
    {
        if (count <= 0)
            return;

        int removeCount = Mathf.Min(count, freeQueue.Count);
        for (int i = 0; i < removeCount; i++)
        {
            var go = freeQueue.Dequeue();
            DestorySingle(go);
        }

        if (freeQueue.Count == 0 && parentTrans != null)
        {
            DestorySingle(parentTrans.gameObject);
            parentTrans = null;
        }

        RecordActiveTime();
    }

    private void InternalDisposeFree()
    {
        while (freeQueue.Count > 0)
        {
            var go = freeQueue.Dequeue();
            DestorySingle(go);
        }
        freeQueue.Clear();

        if (inUseQueue.Count <= 0)
            Object.Destroy(parentTrans.gameObject);

        RecordActiveTime();
    }

    private void InternalSetCapacity(int capacity)
    {
        this.Capacity = capacity;
        if (freeQueue.Count > capacity)
        {
            int excessCount = freeQueue.Count - capacity;
            Remove(excessCount);
        }
    }

    private void InternalSetCustomInstantiateFun(Func<GameObject> customInstantiateFun)
    {
        this.customInstantiateFun = customInstantiateFun;
        CheckIdle = false; // 使用自定义实例化方法时，不检测空闲，以防止自定义方法销毁导致异常
    }

    private void InternalSetCheckIdle(bool checkIdle)
    {
        this.CheckIdle = checkIdle;
    }

    private void InternalSetMaxIdleTime(int maxIdleTime)
    {
        this.MaxIdleTime = maxIdleTime;
    }

    #endregion 内部实现
}