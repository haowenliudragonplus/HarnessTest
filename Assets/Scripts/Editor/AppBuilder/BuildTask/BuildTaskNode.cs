using System;
using Cysharp.Threading.Tasks;
using Framework;

/// <summary>
/// 打包节点基类
/// </summary>
public abstract class BuildTaskNode : ActionNodeBase
{
    public BuildTaskNode(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    protected void Log(string content)
    {
        CLog.Info($"[{Tag}] {content}\n{DateTime.Now}", ELogTag.AppBuild);
    }
}