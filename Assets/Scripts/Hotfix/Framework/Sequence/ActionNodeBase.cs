using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 行为节点基类
/// </summary>
public abstract class ActionNodeBase : IBehaviourNode
{
    public string Tag { get; protected set; }
    public ENodeState State { get; protected set; } = ENodeState.Pending;

    private bool ignoreFail; //是否忽略异常，继续执行下一个节点
    private bool autoNextNode; //自动跳转下一个节点
    private UniTaskCompletionSource nodeTCS; //节点自身的tcs
    private CancellationTokenSource nodeCTS; //节点自身的cts

    public ActionNodeBase(string tag, bool ignoreFail = false, bool autoNextNode = false)
    {
        Tag = tag;
        this.ignoreFail = ignoreFail;
        this.autoNextNode = autoNextNode;

        nodeTCS = new UniTaskCompletionSource();
        nodeCTS = new CancellationTokenSource();
    }

    /// <summary>
    /// 
    /// </summary>
    /// cts：序列全局的cts
    public async UniTask<ENodeState> Execute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        if (State == ENodeState.Running)
            return State;
        if (cts.IsCancellationRequested)
            return ENodeState.Cancelled;

        var combinedCTS = CancellationTokenSource.CreateLinkedTokenSource(
            cts.Token,
            nodeCTS.Token
        );
        State = ENodeState.Running;
        try
        {
            await OnExecute(param, combinedCTS);
            if (autoNextNode)
                nodeTCS.TrySetResult();
            await nodeTCS.Task;
            State = ENodeState.Success;
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
            State = ENodeState.Cancelled;
            nodeTCS.TrySetCanceled(combinedCTS.Token);
        }
        catch (OperationCanceledException) when (nodeCTS.IsCancellationRequested)
        {
            State = ENodeState.Cancelled;
            nodeTCS.TrySetCanceled(combinedCTS.Token);
        }
        catch (Exception e)
        {
            CLog.Error($"ActionNode：{Tag}执行异常：{e}");
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
        }
        finally
        {
            combinedCTS.Dispose();
        }
        return State;
    }

    /// <summary>
    /// 具体执行的逻辑
    /// </summary>
    public virtual async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
    }

    public virtual void Finish()
    {
        if (!nodeCTS.IsCancellationRequested)
        {
            nodeCTS.Cancel();
        }
        nodeTCS?.TrySetResult();
        nodeCTS?.Dispose();
        State = ENodeState.Success;
    }

    public virtual void Abort()
    {
        if (!nodeCTS.IsCancellationRequested)
        {
            nodeCTS.Cancel();
        }
        nodeTCS?.TrySetResult();
        nodeCTS?.Dispose();
        State = ENodeState.Aborted;
    }

    public virtual void Reset()
    {
        nodeCTS?.Dispose();
 
        nodeCTS = new CancellationTokenSource();
        nodeTCS = new UniTaskCompletionSource();
        State = ENodeState.Pending;
    }

    public virtual void Dispose()
    {
        nodeCTS?.Cancel();
        nodeCTS?.Dispose();
        nodeTCS?.TrySetCanceled();
    }
}