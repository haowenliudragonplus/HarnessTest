using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 行为序列
/// </summary>
public class BehaviourSequence
{
    private List<IBehaviourNode> nodeList;

    private IBehaviourNode curNode; //当前节点
    private Action onComplete; //完成本次全部序列节点的回调
    private CancellationTokenSource cts; //全局cts

    public BehaviourSequence()
    {
        nodeList = new List<IBehaviourNode>();
        curNode = null;
        cts = new CancellationTokenSource();
    }

    /// <summary>
    /// 添加节点
    /// </summary>
    public void AddNode(IBehaviourNode node)
    {
        nodeList.Add(node);
    }

    public void SetOnCompleteAction(Action onComplete)
    {
        this.onComplete = onComplete;
    }

    /// <summary>
    /// 启动序列
    /// </summary>
    public async UniTask Run(BehaviourSequenceParam param)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i] == null)
                continue;
            if (cts.IsCancellationRequested)
            {
                curNode = null;
                break;
            }

            curNode = nodeList[i];
            var state = await nodeList[i].Execute(param, cts);
            if (state != ENodeState.Success
                && state != ENodeState.Cancelled
                && state != ENodeState.Aborted)
                break;
        }

        // 完成序列所有节点（中途cancel或abort也会走到这里）
        onComplete?.Invoke();
    }

    public bool FinishCurNode()
    {
        if (curNode == null)
            return false;
        curNode?.Finish();
        return true;
    }

    public bool FinishNode(string tag)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].Tag != tag)
                continue;
            nodeList[i]?.Finish();
            return true;
        }
        return false;
    }

    public bool AbortCurNode()
    {
        if (curNode == null)
            return false;
        curNode?.Abort();
        return true;
    }

    public bool AbortNode(string tag)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].Tag != tag)
                continue;
            nodeList[i]?.Abort();
            return true;
        }
        return false;
    }

    public IBehaviourNode GetCurNode()
    {
        return curNode;
    }

    /// <summary>
    /// 取消整个序列
    /// </summary>
    public void Cancel()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }

    /// <summary>
    /// 重置序列
    /// </summary>
    public void Reset()
    {
        // 释放旧资源
        cts?.Cancel();
        cts?.Dispose();

        // 重置所有节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i]?.Reset();
        }
        curNode = null;

        // 创建新资源
        cts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        foreach (var node in nodeList)
        {
            node?.Dispose();
        }
        nodeList.Clear();
        curNode = null;
    }
}

public class BehaviourSequenceParam
{
}