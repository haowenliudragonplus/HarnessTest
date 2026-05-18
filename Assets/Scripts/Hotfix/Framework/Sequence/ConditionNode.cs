using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 条件节点
/// </summary>
public class ConditionNode : IBehaviourNode
{
    public string Tag { get; protected set; }
    public ENodeState State { get; protected set; } = ENodeState.Pending;
    public bool IsAbort { get; protected set; }

    private Func<bool> condition;
    private IBehaviourNode trueNode;
    private IBehaviourNode falseNode;
    private bool ignoreFail; //是否忽略异常，继续执行下一个节点
    private bool autoNextNode; //自动跳转下一个节点

    private UniTaskCompletionSource nodeTCS; //节点自身的tcs

    public ConditionNode(Func<bool> condition, IBehaviourNode trueNode, IBehaviourNode falseNode, string tag,
        bool ignoreFail = false, bool autoNextNode = false)
    {
        if (condition == null)
            throw new ArgumentNullException(nameof(condition), "condition不能为null");
        if (trueNode == null)
            throw new ArgumentNullException(nameof(trueNode), "trueNode不能为null");
        if (falseNode == null)
            throw new ArgumentNullException(nameof(falseNode), "falseNode不能为null");

        this.condition = condition;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
        Tag = tag;
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
        try
        {
            bool b = condition();
            var nextNode = b ? trueNode : falseNode;
            ENodeState state = await nextNode.Execute(param, cancellationTokenSource);
            
            if (autoNextNode)
                nodeTCS?.TrySetResult();
            await nodeTCS.Task;
            
            if (state == ENodeState.Success
                || state == ENodeState.Cancelled
                || state == ENodeState.Aborted)
            {
                State = ENodeState.Success;
            }
            else
            {
                State = state;
            }
        }
        catch (OperationCanceledException) when (cancellationTokenSource.IsCancellationRequested)
        {
            State = ENodeState.Cancelled;
            nodeTCS?.TrySetCanceled();
        }
        catch (Exception e)
        {
            CLog.Error($"ConditionNode：{Tag}执行异常：{e}");
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
        return State;
    }

    public void Finish()
    {
        nodeTCS.TrySetResult();
        trueNode?.Finish();
        falseNode?.Finish();
        State = ENodeState.Success;
    }

    public void Abort()
    {
        nodeTCS.TrySetResult();
        trueNode?.Finish();
        falseNode?.Finish();
        State = ENodeState.Aborted;
    }

    public void Reset()
    {
        // 释放旧资源
        nodeTCS?.TrySetCanceled();
        
        // 创建新资源
        nodeTCS = new UniTaskCompletionSource();
        trueNode?.Reset();
        falseNode?.Reset();
        State = ENodeState.Pending;
    }

    public void Dispose()
    {
        nodeTCS?.TrySetCanceled();
        trueNode?.Dispose();
        falseNode?.Dispose();
    }
}