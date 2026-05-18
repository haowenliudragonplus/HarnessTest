using System;
using System.Collections.Generic;
using System.Text;
using Framework;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 游戏物体对象池类型
/// </summary>
public enum EGameObjectPoolType
{
    Global = 1, //全局
    InBattle, //战斗内
}

/// <summary>
/// 游戏物体对象池管理器
/// </summary>
public class GameObjectPool
{
    private static Transform gameObjectPoolRoot;
    public static Transform GameObjectPoolRoot
    {
        get
        {
            if (gameObjectPoolRoot == null)
            {
                GameObject rootGo = new GameObject("GameObjectPoolRoot");
                Object.DontDestroyOnLoad(rootGo);
                gameObjectPoolRoot = rootGo.transform;
            }
            return gameObjectPoolRoot;
        }
    }

    private static Dictionary<EGameObjectPoolType, Dictionary<string, GameObjectCollection>> gameObjectCollectionDict = new Dictionary<EGameObjectPoolType, Dictionary<string, GameObjectCollection>>(); //所有游戏物体池子
    private static Dictionary<EGameObjectPoolType, Transform> gameObjectPoolType2Root = new Dictionary<EGameObjectPoolType, Transform>(); //游戏物体池子类型对应的节点

    private static readonly object _lockObj = new object();

    public static void PreLoad(string poolKey, int count, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global,
        int capacity = -1,
        Func<GameObject> customInstantiateFun = null,
        bool enableCheckIdle = true,
        int maxIdleTime = -1)
    {
        if (count <= 0)
            return;
        var pool = GetOrCreatePool(poolKey, gameObjectPoolType, true);
        if (pool == null)
            return;

        if (customInstantiateFun != null)
        {
            pool.SetCustomInstantiateFun(customInstantiateFun);
        }
        if (capacity > 0)
        {
            pool.SetCapacity(capacity);
        }
        pool.SetEnableCheckIdle(enableCheckIdle);
        if (maxIdleTime > 0)
        {
            pool.SetMaxIdleTime(maxIdleTime);
        }
        pool.Add(count);
    }

    public static GameObject Get(string poolKey, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool active = true)
    {
        var pool = GetOrCreatePool(poolKey, gameObjectPoolType, forceGet: true);
        if (pool == null)
            return null;

        var ret = pool.Get(active);
        return ret;
    }

    public static bool Put(GameObject go, string forcePoolKey = "", EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        if (go == null)
        {
            CLog.Error($"GameObject不能为null");
            return false;
        }

        string poolKey = string.IsNullOrEmpty(forcePoolKey)
            ? go.name
            : forcePoolKey;
        var pool = GetOrCreatePool(poolKey, gameObjectPoolType);
        if (pool == null)
        {
            CLog.Error($"找不到此物体对应的池子，GameObject：{go}，池子key：{poolKey}");
            // Object.Destroy(go);
            return false;
        }

        var ret = pool.Put(go, forcePoolKey);
        return ret;
    }

    public static bool Add(string poolKey, int count, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetOrCreatePool(poolKey, gameObjectPoolType);
        if (pool == null)
            return false;

        var ret = pool.Add(count);
        return ret;
    }

    public static void Remove(string poolKey, int count, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetOrCreatePool(poolKey, gameObjectPoolType);
        if (pool == null)
            return;

        pool.Remove(count);
    }

    public static void DisposeFree(EGameObjectPoolType gameObjectPoolType)
    {
        if (ReferencePoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalDisposeFree(gameObjectPoolType);
        InternalDisposeFree(gameObjectPoolType);
    }

    public static void DisposeFreeAll()
    {
        if (ReferencePoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                InternalDisposeFreeAll();
        InternalDisposeFreeAll();
    }

    public static GameObjectCollection GetOrCreatePool(string poolKey,
        EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool forceGet = false)
    {
        if (string.IsNullOrEmpty(poolKey))
        {
            CLog.Error("poolKey不能为空");
            return null;
        }

        if (ReferencePoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                return InternalGetOrCreatePool(poolKey, gameObjectPoolType, forceGet);
        return InternalGetOrCreatePool(poolKey, gameObjectPoolType, forceGet);
    }

    private static Dictionary<string, GameObjectCollection> GetOrCreatePoolDict(EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool forceGet = false)
    {
        if (ReferencePoolConfiguration.EnableThreadSafe)
            lock (_lockObj)
                return InternalGetOrCreatePoolDict(gameObjectPoolType, forceGet);
        return InternalGetOrCreatePoolDict(gameObjectPoolType, forceGet);
    }

    public static void LogAllInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var poolDict in gameObjectCollectionDict.Values)
        {
            foreach (var pool in poolDict.Values)
            {
                sb.AppendLine(pool.GetInfo());
            }
        }
        CLog.Info(sb.ToString());
    }

    private static List<GameObjectCollection> toRemoveList = new List<GameObjectCollection>();
    public static void OnUpdate()
    {
        if (!GameObjectPoolConfiguration.EnableCheckIdle || gameObjectCollectionDict.Count <= 0)
            return;

        toRemoveList.Clear();
        foreach (var poolDict in gameObjectCollectionDict.Values)
        {
            foreach (var pool in poolDict.Values)
            {
                if (!pool.CheckIdle)
                    continue;

                var sec = (DateTime.Now - pool.LastActiveTime).TotalSeconds;
                if (sec > pool.MaxIdleTime)
                {
                    pool.DisposeFree();
                    toRemoveList.Add(pool);
                }
            }
        }
        if (toRemoveList.Count > 0)
        {
            foreach (var pool in toRemoveList)
            {
                if (pool.InUseCount > 0)
                    continue;
                gameObjectCollectionDict[pool.GameObjectPoolType].Remove(pool.PoolKey);
            }
        }
    }

    #region 内部方法

    private static void InternalDisposeFree(EGameObjectPoolType gameObjectPoolType)
    {
        var poolDict = GetOrCreatePoolDict(gameObjectPoolType);
        if (poolDict == null)
            return;
        bool b = true;
        foreach (var pool in poolDict.Values)
        {
            if (pool.InUseCount > 0)
                b = false;
            pool.DisposeFree();
        }
        if (b)
        {
            gameObjectCollectionDict.Remove(gameObjectPoolType);
            Object.Destroy(gameObjectPoolType2Root[gameObjectPoolType].gameObject);
            gameObjectPoolType2Root.Remove(gameObjectPoolType);
        }
    }

    private static void InternalDisposeFreeAll()
    {
        var temp = new Dictionary<EGameObjectPoolType, Dictionary<string, GameObjectCollection>>(gameObjectCollectionDict);
        foreach (var kvp in temp)
        {
            DisposeFree(kvp.Key);
        }
    }

    private static GameObjectCollection InternalGetOrCreatePool(string poolKey,
        EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool forceGet = false)
    {
        // 获取游戏物体池子集合字典
        var gameObjectCollectionDict = GetOrCreatePoolDict(gameObjectPoolType, forceGet);
        if (gameObjectCollectionDict == null)
        {
            CLog.Error($"找不到游戏物体池子集合字典，poolKey：{poolKey}");
            return null;
        }

        // 获取类型节点
        if (!gameObjectPoolType2Root.TryGetValue(gameObjectPoolType, out var _rootTrans))
        {
            _rootTrans = new GameObject().transform;
            _rootTrans.name = gameObjectPoolType.ToString();
            _rootTrans.transform.SetParent(GameObjectPoolRoot, false);
            _rootTrans.transform.localPosition = Vector3.zero;
            gameObjectPoolType2Root[gameObjectPoolType] = _rootTrans;
        }

        //
        if (!gameObjectCollectionDict.TryGetValue(poolKey, out var _collection))
        {
            if (forceGet)
            {
                _collection = new GameObjectCollection(poolKey, gameObjectPoolType, _rootTrans);
                gameObjectCollectionDict.Add(poolKey, _collection);
            }
            else
            {
                return null;
            }
        }
        return _collection;
    }

    private static Dictionary<string, GameObjectCollection> InternalGetOrCreatePoolDict(EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool forceGet = false)
    {
        if (!gameObjectCollectionDict.TryGetValue(gameObjectPoolType, out var _collectionDict))
        {
            if (forceGet)
            {
                _collectionDict = new Dictionary<string, GameObjectCollection>();
                gameObjectCollectionDict.Add(gameObjectPoolType, _collectionDict);
            }
            else
            {
                return null;
            }
        }
        return _collectionDict;
    }

    #endregion 内部方法
}