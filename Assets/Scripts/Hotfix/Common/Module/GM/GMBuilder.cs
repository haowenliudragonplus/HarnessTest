#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System;

/// <summary>
/// GM创造器
/// </summary>
public class GMBuilder
{
    private GMBase gm;
    private GMCommandData curData;

    public GMBuilder(GMBase gm, string optionName, string commandPath, EGMCommandType commandType = EGMCommandType.Button)
    {
        this.gm = gm;
        curData = new GMCommandData();
        curData.SetOptionName(optionName);
        curData.SetCommandPath(commandPath);
        curData.SetCommandType(commandType);
    }

    /// <summary>
    /// 设置提示字符串
    /// </summary>
    public GMBuilder SetTipStr(string tipStr)
    {
        curData.SetTipStr(tipStr);
        return this;
    }

    /// <summary>
    /// 设置按钮点击回调
    /// </summary>
    public GMBuilder SetOnButtonClick(Action<string, string, string> onButtonClick)
    {
        curData.SetOnButtonClick(onButtonClick);
        return this;
    }

    /// <summary>
    /// 设置复选框状态改变回调
    /// </summary>
    public GMBuilder SetOnToggleChanged(Action<bool> onToggleChanged)
    {
        curData.SetOnToggleChanged(onToggleChanged);
        return this;
    }

    /// <summary>
    /// 设置获取Toggle初始状态的事件
    /// </summary>
    public GMBuilder SetGetToggleInitStateEvent(Func<bool> getToggleInitStateEvent)
    {
        curData.SetGetToggleInitStateEvent(getToggleInitStateEvent);
        return this;
    }

    public GMBuilder SetCloseViewAfterExecute()
    {
        curData.SetCloseViewAfterExecute();
        return this;
    }

    /// <summary>
    /// 完成构建并注册命令
    /// </summary>
    public void Register()
    {
        gm.RegisterCommand(curData);
    }
}

#endif