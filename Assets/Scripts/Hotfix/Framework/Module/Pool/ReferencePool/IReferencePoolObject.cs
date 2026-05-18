namespace Framework
{
    public interface IReferencePoolObject
    {
        /// <summary>
        /// 对象创建时调用（仅一次）
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 对象被获取时调用
        /// </summary>
        void OnAllocate();

        /// <summary>
        /// 对象被回收时调用
        /// </summary>
        void OnRecycle();

        /// <summary>
        /// 对象被永久销毁时调用
        /// </summary>
        void OnDispose();
    }
}