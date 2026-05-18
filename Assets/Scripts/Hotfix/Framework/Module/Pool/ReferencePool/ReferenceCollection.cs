using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 引用池
    /// </summary>
    public class ReferenceCollection
    {
        private Queue<object> freeQueue = new Queue<object>(); //空闲对象
        private HashSet<object> inUseQueue = new HashSet<object>(); //使用中的对象
        private Func<object> customCreateFun; //自定义创建对象的方法

        public Type ReferenceType { get; private set; } //对象类型
        public int Capacity { get; private set; } //-1表示无限容量
        public bool CheckIdle { get; private set; } //是否检测空闲
        public int MaxIdleTime { get; private set; } //最长的空闲时间（默认1小时）
        public DateTime LastActiveTime { get; private set; } //最后一次活动时间

        public int FreeCount => freeQueue.Count; //空闲的数量
        public int InUseCount => inUseQueue.Count; //使用中的数量
        public int TotalCreateCount { get; private set; } //总创建的数量

        private readonly object _lockObj = new object();

        public ReferenceCollection(Type type)
        {
            ReferenceType = type;

            this.Capacity = ReferencePoolConfiguration.DefaultCapacity;
            CheckIdle = true;
            MaxIdleTime = ReferencePoolConfiguration.MaxIdleTime;

            TotalCreateCount = 0;

            RecordActiveTime();
        }

        #region 获取

        public T Allocate<T>()
            where T : class, IReferencePoolObject
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    return InternalAllocate<T>();
            return InternalAllocate<T>();
        }

        #endregion 获取

        #region 回收

        public bool Recycle<T>(T obj)
            where T : class, IReferencePoolObject
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    return InternalRecycle(obj);
            return InternalRecycle(obj);
        }

        #endregion 回收

        #region 添加

        public bool Add(int count)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    return InternalAdd(count);
            return InternalAdd(count);
        }

        #endregion 创建

        #region 移除

        public void Remove(int count)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalRemove(count);
            else
                InternalRemove(count);
        }

        #endregion 移除

        #region 释放空闲的对象

        public void DisposeFree()
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalDisposeFree();
            else
                InternalDisposeFree();
        }

        #endregion 释放空闲的对象

        #region 外部工具

        public void SetCapacity(int capacity)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalSetCapacity(capacity);
            else
                InternalSetCapacity(capacity);
        }

        public void SetCustomCreateFun(Func<object> customCreateFun)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalSetOnCreate(customCreateFun);
            else
                InternalSetOnCreate(customCreateFun);
        }

        public void SetnableCheckIdle(bool checkIdle)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalSetCheckIdle(checkIdle);
            else
                InternalSetCheckIdle(checkIdle);
        }

        public void SetMaxIdleTime(int maxIdleTime)
        {
            if (ReferencePoolConfiguration.EnableThreadSafe)
                lock (_lockObj)
                    InternalSetMaxIdleTime(maxIdleTime);
            else
                InternalSetMaxIdleTime(maxIdleTime);
        }

        #endregion 外部工具

        private object Create()
        {
            try
            {
                var newObj = customCreateFun == null
                    ? Activator.CreateInstance(ReferenceType)
                    : customCreateFun?.Invoke();
                if (newObj == null)
                    return null;

                if (newObj is IReferencePoolObject poolObj)
                    poolObj.OnCreate();
                TotalCreateCount++;
                RecordActiveTime();
                return newObj;
            }
            catch (Exception e)
            {
                CLog.Error($"创建对象失败，池子类型：{ReferenceType}，异常信息：{e.Message}");
                return null;
            }
        }

        private void DisposeSingleObj(object obj)
        {
            if (obj is IReferencePoolObject poolObj)
                poolObj.OnDispose();

            // 若对象实现了IDisposable，触发资源释放
            if (obj is IDisposable disposable)
                disposable.Dispose();
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
            string infoStr = $"类型：{ReferenceType.Name}，容量：{Capacity}，" +
                             $"空闲的数量：{FreeCount}，使用中的数量：{InUseCount}，总创建的数量：{TotalCreateCount}，" +
                             $"最后活动时间：{LastActiveTime}";
            return infoStr;
        }

        #region 内部实现

        private T InternalAllocate<T>()
            where T : class, IReferencePoolObject
        {
            if (freeQueue.Count <= 0)
            {
                bool ret = Add(1);
                if (ret == false)
                {
                    CLog.Error($"获取失败，池子中没有可用的对象并且池子容量已满，无法添加，池子类型：{ReferenceType}");
                    return null;
                }
            }

            var obj = freeQueue.Dequeue();
            inUseQueue.Add(obj);

            (obj as IReferencePoolObject)?.OnAllocate();

            RecordActiveTime();

            return obj as T;
        }

        private bool InternalRecycle<T>(T obj)
            where T : class, IReferencePoolObject
        {
            if (obj == null)
            {
                CLog.Error("回收失败，对象不能为空");
                return false;
            }
            if (obj.GetType() != ReferenceType)
            {
                CLog.Error($"回收失败，类型不一致，对象类型：{obj.GetType().Name}，池子类型：{ReferenceType}");
                return false;
            }
            if (freeQueue.Contains(obj))
            {
                CLog.Error($"回收失败，已经被回收了，池子类型：{ReferenceType}");
                return false;
            }
            if (!inUseQueue.Contains(obj))
            {
                DisposeSingleObj(obj);
                CLog.Error($"回收失败，不是此池子创建的对象，直接释放，池子类型：{ReferenceType}");
                return false;
            }
            if (IsFull())
            {
                DisposeSingleObj(obj);
                CLog.Error($"回收失败，池子容量已满，直接释放，池子类型：{ReferenceType}");
                return false;
            }

            inUseQueue.Remove(obj);
            freeQueue.Enqueue(obj);

            if (obj is IReferencePoolObject poolObject)
                poolObject.OnRecycle();

            RecordActiveTime();

            return true;
        }

        private bool InternalAdd(int count)
        {
            if (count <= 0)
                return false;
            if (IsFull())
            {
                CLog.Error($"池子容量已满，无法创建，池子类型：{ReferenceType}");
                return false;
            }

            var leftCount = Capacity < 0 ? count : Capacity - freeQueue.Count;
            count = Mathf.Min(count, leftCount);
            for (int i = 0; i < count; i++)
            {
                var obj = Create();
                freeQueue.Enqueue(obj);
            }
            if (count > leftCount)
            {
                CLog.Error($"池子容量已满，其中{count - leftCount}个对象无法创建，池子类型：{ReferenceType}");
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
                var obj = freeQueue.Dequeue();
                DisposeSingleObj(obj);
            }

            RecordActiveTime();
        }

        private void InternalDisposeFree()
        {
            while (freeQueue.Count > 0)
            {
                var obj = freeQueue.Dequeue();
                DisposeSingleObj(obj);
            }
            freeQueue.Clear();

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

        private void InternalSetOnCreate(Func<object> customCreateFun)
        {
            this.customCreateFun = customCreateFun;
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
}