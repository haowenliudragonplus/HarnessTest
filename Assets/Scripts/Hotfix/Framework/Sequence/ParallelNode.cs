using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 并行节点
/// </summary>
public class ParallelNode : IBehaviourNode
{
    public string Tag { get; protected set; }
    public ENodeState State { get; protected set; } = ENodeState.Pending;
    public bool IsAbort { get; protected set; }

    private List<IBehaviourNode> childNodeList;

    private bool ignoreFail; //是否忽略异常，继续执行下一个节点
    private bool autoNextNode; //自动跳转下一个节点

    private UniTaskCompletionSource nodeTCS; //节点自身的tcs

    public ParallelNode(List<IBehaviourNode> childNodeList, string tag,
        bool ignoreFail = false, bool autoNextNode = false)
    {
        if (childNodeList == null)
            throw new ArgumentNullException(nameof(childNodeList), "childNodeList不能为null");
        if (childNodeList.Count == 0)
            throw new ArgumentException("childNodeList数量不能为0", nameof(childNodeList));

        this.childNodeList = childNodeList;
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
            var taskList = new List<UniTask<ENodeState>>();
            foreach (var node in childNodeList)
            {
                if (node == null)
                    continue;
                var state = node.Execute(param, cancellationTokenSource);
                taskList.Add(state);
            }
            
            if (taskList.Count == 0)
            {
                State = ENodeState.Success;
                return State;
            }
            
            var stateList = await UniTask.WhenAll(taskList);
            
            if (autoNextNode)
                nodeTCS?.TrySetResult();
            await nodeTCS.Task;
            
            // 检查所有子节点的执行结果
            State = ENodeState.Success;
            foreach (var state in stateList)
            {
                if (state == ENodeState.Success
                    || state == ENodeState.Cancelled
                    || state == ENodeState.Aborted)
                    continue;
                State = ENodeState.Failure;
                break;
            }
        }
        catch (OperationCanceledException) when (cancellationTokenSource.IsCancellationRequested)
        {
            State = ENodeState.Cancelled;
            nodeTCS?.TrySetCanceled();
        }
        catch (Exception e)
        {
            CLog.Error($"ParallelNode：{Tag}执行异常：{e}");
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
        foreach (var node in childNodeList)
        {
            node?.Finish();
        }
        State = ENodeState.Success;
    }

    public void Abort()
    {
        nodeTCS.TrySetResult();
        foreach (var node in childNodeList)
        {
            node?.Abort();
        }
        State = ENodeState.Aborted;
    }

    public void Reset()
    {
        // 释放旧资源
        nodeTCS?.TrySetCanceled();
        
        // 创建新资源
        nodeTCS = new UniTaskCompletionSource();
        foreach (var node in childNodeList)
        {
            node?.Reset();
        }
        State = ENodeState.Pending;
    }

    public void Dispose()
    {
        nodeTCS?.TrySetCanceled();
        foreach (var node in childNodeList)
        {
            node?.Dispose();
        }
    }
}