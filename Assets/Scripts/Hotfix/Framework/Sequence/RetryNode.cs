using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 重试节点
/// </summary>
/// 在失败时按设定次数循环执行
public class RetryNode : IBehaviourNode
{
    public string Tag { get; protected set; }
    public ENodeState State { get; protected set; } = ENodeState.Pending;
    public bool IsAbort { get; protected set; }

    private IBehaviourNode innerNode; //内部节点
    private int maxRetryCount; //最大重试次数
    private bool ignoreFail; //是否忽略异常，继续执行下一个节点
    private bool autoNextNode; //自动跳转下一个节点

    private UniTaskCompletionSource nodeTCS; //节点自身的tcs

    public RetryNode(IBehaviourNode innerNode, string tag, int maxRetryCount,
        bool ignoreFail = false, bool autoNextNode = false)
    {
        if (innerNode == null)
            throw new ArgumentNullException(nameof(innerNode), "innerNode不能为null");
        if (maxRetryCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxRetryCount), "maxRetryCount必须大于0");

        this.innerNode = innerNode;
        Tag = tag;
        this.maxRetryCount = maxRetryCount;
        this.ignoreFail = ignoreFail;
        this.autoNextNode = autoNextNode;

        nodeTCS = new UniTaskCompletionSource();
    }

    public async UniTask<ENodeState> Execute(BehaviourSequenceParam param, CancellationTokenSource cancellationTokenSource)
    {
        if (State == ENodeState.Running)
            return State;
        if (cancellationTokenSource.IsCancellationRequested)
            return ENodeState.Cancelled;

        State = ENodeState.Running;
        int attemptCount = 0;

        while (attemptCount <= maxRetryCount)
        {
            try
            {
                ENodeState state = await innerNode.Execute(param, cancellationTokenSource);

                if (autoNextNode)
                    nodeTCS?.TrySetResult();
                await nodeTCS.Task;

                if (state == ENodeState.Success
                    || state == ENodeState.Cancelled
                    || state == ENodeState.Aborted)
                {
                    State = ENodeState.Success;
                    break;
                }
                else
                {
                    attemptCount++;
                    if (attemptCount <= maxRetryCount)
                    {
                        innerNode?.Reset();
                    }
                    else
                    {
                        State = state;
                    }
                }
            }
            catch (OperationCanceledException) when (cancellationTokenSource.IsCancellationRequested)
            {
                State = ENodeState.Cancelled;
                nodeTCS?.TrySetCanceled();
                break;
            }
            catch (Exception e)
            {
                attemptCount++;
                CLog.Error($"RetryNode：{Tag}第{attemptCount}次执行异常：{e}");

                if (attemptCount <= maxRetryCount)
                {
                    innerNode?.Reset();
                }
                else
                {
                    if (ignoreFail)
                    {
                        State = ENodeState.Success;
                        nodeTCS?.TrySetResult();
                    }
                    else
                    {
                        State = ENodeState.Failure;
                        nodeTCS?.TrySetException(e);
                    }
                    break;
                }
            }
        }

        if (State != ENodeState.Success 
            && State != ENodeState.Cancelled
            && State != ENodeState.Aborted)
        {
            CLog.Error($"RetryNode：{Tag}执行失败，已重试{attemptCount}次");
            if (ignoreFail)
            {
                State = ENodeState.Success;
                nodeTCS?.TrySetResult();
            }
            else
            {
                State = ENodeState.Failure;
            }
        }

        return State;
    }

    public void Finish()
    {
        nodeTCS.TrySetResult();
        innerNode?.Finish();
        State = ENodeState.Success;
    }

    public void Abort()
    {
        nodeTCS.TrySetResult();
        innerNode?.Abort();
    }

    public void Reset()
    {
        // 释放旧资源
        nodeTCS?.TrySetCanceled();

        // 创建新资源
        nodeTCS = new UniTaskCompletionSource();
        innerNode?.Reset();
        State = ENodeState.Pending;
    }

    public void Dispose()
    {
        nodeTCS?.TrySetCanceled();
        innerNode?.Dispose();
    }
}