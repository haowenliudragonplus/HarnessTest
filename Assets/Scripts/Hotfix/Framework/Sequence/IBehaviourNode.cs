using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>
/// 行为节点接口
/// </summary>
public interface IBehaviourNode
{
    string Tag { get; } //标签
    ENodeState State { get; } //节点当前状态

    /// <summary>
    /// 执行操作
    /// </summary>
    UniTask<ENodeState> Execute(BehaviourSequenceParam param, CancellationTokenSource cts);

    /// <summary>
    /// 完成
    /// </summary>
    void Finish();

    /// <summary>
    /// 中断
    /// </summary>
    void Abort();

    /// <summary>
    /// 重置
    /// </summary>
    void Reset();

    /// <summary>
    /// 释放
    /// </summary>
    void Dispose();
}

/// <summary>
/// 节点执行状态枚举
/// </summary>
public enum ENodeState
{
    Pending = 0, //准备就绪
    Running, //执行中
    Success, //执行成功
    Failure, //执行失败
    Aborted, //被中断
    Cancelled, //被取消
    Waiting, //等待中
}