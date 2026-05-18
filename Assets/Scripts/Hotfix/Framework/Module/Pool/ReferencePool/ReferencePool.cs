using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    /// <summary>
    /// 引用池管理器
    /// </summary>
    public class ReferencePool
    {
        private static Dictionary<Type, ReferenceCollection> referencelCollectionDict = new Dictionary<Type, ReferenceCollection>(); //所有引用池子

        private static readonly object _lockObj = new object();

        public static void PreLoad<T>(int count,
            int capacity = -1,
            Func<T> customCreateFun = null,
            bool enableCheckIdle = true,
            int maxIdleTime = -1)
            where T : class, IReferencePoolObject
        {
            if (count <= 0)
                return;
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return;

            if (customCreateFun != null)
            {
                pool.SetCustomCreateFun(customCreateFun);
            }
            if (capacity > 0)
            {
                pool.SetCapacity(capacity < 0 ? ReferencePoolConfiguration.DefaultCapacity : capacity);
            }
            pool.SetnableCheckIdle(enableCheckIdle);
            if (maxIdleTime > 0)
            {
                pool.SetMaxIdleTime(maxIdleTime);
            }
            pool.Add(count);
        }

        public static T Allocate<T>()
            where T : class, IReferencePoolObject
        {
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return null;

            var obj = pool.Allocate<T>();
            return obj;
        }

        public static bool Recycle<T>(T obj)
            where T : class, IReferencePoolObject
        {
            if (obj == null)
            {
                CLog.Error($"回收失败，对象不能为空");
                return false;
            }
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return false;

            var ret = pool.Recycle<T>(obj);
            return ret;
        }

        public static bool Add<T>(int count)
            where T : class, IReferencePoolObject
        {
            if (count <= 0)
                return false;
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return false;

            var ret = pool.Add(count);
            return ret;
        }

        public static void Remove<T>(int count)
            where T : class, IReferencePoolObject
        {
            if (count <= 0)
                return;
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return;

            pool.Remove(count);
        }

        public static void DisposeFree<T>()
            where T : class, IReferencePoolObject
        {
            var pool = GetOrCreatePool<T>();
            if (pool == null)
                return;

            pool?.DisposeFree();
        }

        public static void DisposeAll()
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalDisposeFreeAll();
            else
                InternalDisposeFreeAll();
        }

        public static ReferenceCollection GetOrCreatePool<T>()
            where T : class, IReferencePoolObject
        {
            var referenceType = typeof(T);
            if (referenceType.IsValueType)
            {
                CLog.Error($"类型错误，不能为值类型，类型：{referenceType}");
                return null;
            }
            if (referenceType.IsAbstract)
            {
                CLog.Error($"类型错误，不能为抽象类，类型：{referenceType}");
                return null;
            }

            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    return InternalGetOrCreatePool<T>();
            return InternalGetOrCreatePool<T>();
        }

        public static void OnUpdate()
        {
            if (!ReferencePoolConfiguration.EnableCheckIdle || referencelCollectionDict.Count <= 0)
                return;

            foreach (var pool in referencelCollectionDict.Values)
            {
                if (!pool.CheckIdle)
                    continue;

                var sec = (DateTime.Now - pool.LastActiveTime).TotalSeconds;
                if (sec > pool.MaxIdleTime)
                {
                    pool.DisposeFree();
                }
            }
        }

        public static void LogAllInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pool in referencelCollectionDict.Values)
            {
                sb.AppendLine(pool.GetInfo());
            }
            CLog.Info(sb.ToString());
        }

        #region 内部方法

        private static ReferenceCollection InternalGetOrCreatePool<T>()
            where T : class, IReferencePoolObject
        {
            var type = typeof(T);
            if (referencelCollectionDict.TryGetValue(type, out var _pool))
                return _pool;

            // 创建新的引用池
            _pool = new ReferenceCollection(type);
            referencelCollectionDict.Add(type, _pool);
            return _pool;
        }

        private static void InternalDisposeFreeAll()
        {
            foreach (var pool in referencelCollectionDict.Values)
            {
                pool?.DisposeFree();
            }
            referencelCollectionDict?.Clear();
        }

        #endregion
    }
}