using System;
using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>
/// 具体行为节点（通过传入函数的形式来执行）
/// </summary>
public class ActionNode : ActionNodeBase
{
    private Func<BehaviourSequenceParam, CancellationTokenSource, UniTask> onExecute;

    public ActionNode(Func<BehaviourSequenceParam, CancellationTokenSource, UniTask> onExecute, string tag, bool ignoreFail = false, bool autoNextNode = false) : base(tag, ignoreFail, autoNextNode)
    {
        this.onExecute = onExecute;
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        await onExecute.Invoke(param, cts);
    }
}