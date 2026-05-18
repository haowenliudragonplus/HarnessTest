#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using Framework;

/// <summary>
/// 每个GM工具的基类
/// </summary>
public abstract class GMBase
{
    public string OptionName { get; private set; } // 选项名（根节点名）

    public Dictionary<string, GMCommandData> CommandDict { get; set; } = new Dictionary<string, GMCommandData>(); //所有的命令数据
    public GMNode GMRootNode { get; private set; } // GM命令树根节点

    public void Init(string option)
    {
        OptionName = option;
        GMRootNode = new GMNode(OptionName, null);
        RegisterAllCommand();
    }

    protected abstract void RegisterAllCommand();

    /// <summary>
    /// 获取GMBuilder实例
    /// </summary>
    protected GMBuilder GetGMBuilder(string commandPath, EGMCommandType commandType = EGMCommandType.Button)
    {
        GMBuilder builder = new GMBuilder(this, OptionName, commandPath, commandType);
        return builder;
    }

    internal void RegisterCommand(GMCommandData commandData)
    {
        if (string.IsNullOrEmpty(commandData.CommandName))
        {
            CLog.Error("GM命令名称不能为空");
            return;
        }
        if (CommandDict.ContainsKey(commandData.CommandPath))
        {
            CLog.Error($"GM命令不能重复：{commandData.CommandPath}");
            return;
        }

        var curNode = GMRootNode;
        for (int i = 1; i < commandData.PathSegments.Length; i++)
        {
            string segment = commandData.PathSegments[i];
            curNode = curNode.AddChild(segment);
            if (i == commandData.PathSegments.Length - 1)
            {
                curNode.SetCommandData(commandData);
            }
        }
        CommandDict.Add(commandData.CommandPath, commandData);
    }
}

/// <summary>
/// GM命令数据
/// </summary>
public class GMCommandData
{
    public string OptionName { get; private set; } // 选项名（根节点名）
    public string CommandPath { get; private set; } //命令全路径
    public string[] PathSegments { get; private set; } //命令路径片段数组
    public string CommandName; //命令名称
    public EGMCommandType CommandType { get; private set; } //命令类型
    public string TipStr { get; private set; } //提示信息
    public bool CloseViewAfterExecute { get; private set; } //执行后是否关闭界面

    // EGMCommandType为Button时有效
    public Action<string, string, string> OnButtonClick { get; private set; } //按钮点击事件回调

    // EGMCommandTypeToggle时有效
    public Action<bool> OnToggleChanged { get; private set; } //复选框改变事件回调
    public Func<bool> GetToggleInitStateEvent { get; private set; } //获取复选框初始状态的事件

    public GMCommandData()
    {
    }

    public GMCommandData(string optionName, string commandPath)
    {
        OptionName = optionName;
        SetCommandPath(commandPath);
    }

    public void SetOptionName(string optionName)
    {
        OptionName = optionName;
    }

    public void SetCommandPath(string commandPath)
    {
        CommandPath = OptionName + "/" + commandPath;
        PathSegments = CommandPath.Split("/");
        CommandName = PathSegments.Length <= 1 ? string.Empty : PathSegments[^1];
    }

    public void SetCommandType(EGMCommandType commandType)
    {
        CommandType = commandType;
    }

    public void SetTipStr(string tipStr)
    {
        TipStr = tipStr;
    }

    public void SetOnButtonClick(Action<string, string, string> onButtonClick)
    {
        OnButtonClick = onButtonClick;
    }

    public void SetOnToggleChanged(Action<bool> onToggleChanged)
    {
        OnToggleChanged = onToggleChanged;
    }

    public void SetGetToggleInitStateEvent(Func<bool> getToggleInitStateEvent)
    {
        GetToggleInitStateEvent = getToggleInitStateEvent;
    }

    public void SetCloseViewAfterExecute()
    {
        CloseViewAfterExecute = true;
    }
}

/// <summary>
/// GM节点
/// </summary>
public class GMNode
{
    public string Name { get; private set; } //节点名称
    public Dictionary<string, GMNode> Children { get; private set; } = new Dictionary<string, GMNode>(); //子节点
    public GMNode Parent { get; private set; } //父节点
    public GMCommandData CommandData { get; private set; } //条目数据

    public GMNode(string name, GMNode parent)
    {
        Name = name;
        Parent = parent;
    }

    public void SetCommandData(GMCommandData commandData)
    {
        CommandData = commandData;
    }

    /// <summary>
    /// 添加子节点
    /// </summary>
    public GMNode AddChild(string name)
    {
        if (!Children.ContainsKey(name))
        {
            Children[name] = new GMNode(name, this);
        }
        return Children[name];
    }
}

/// <summary>
/// GM命令类型
/// </summary>
public enum EGMCommandType
{
    Button = 1,
    Toggle,
}

#endif