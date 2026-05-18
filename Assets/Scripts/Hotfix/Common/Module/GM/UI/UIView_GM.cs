#if DEVELOPMENT_BUILD || UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine.UI;

/// <summary>
/// GM界面
/// </summary>
public class UIView_GM : UIView_GMBase
{
    private List<GMBase> gmList = new List<GMBase>(); //所有GM数据

    public GMBase CurSelectOption { get; private set; } //当前选择的入口选项
    public GMNode CurSelectCommandNode { get; private set; } //当前选择的命令节点

    private List<UIWidget_GMOption> optionWidgetCache = new List<UIWidget_GMOption>(); //选项控件缓存
    private List<UIWidget_GMCommandButton> commandWidgetCache_Button = new List<UIWidget_GMCommandButton>(); //按钮命令控件缓存
    private List<UIWidget_GMCommandToggle> commandWidgetCache_Toggle = new List<UIWidget_GMCommandToggle>(); //复选框命令控件缓存

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Close.onClick.AddListener(() => Close());
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        InitData();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView_Option();
        RefreshView_Command();
    }

    /// <summary>
    /// 刷新选项界面
    /// </summary>
    private void RefreshView_Option()
    {
        for (int i = 0; i < gmList.Count; i++)
        {
            int tempI = i;
            var tempEntrance = gmList[tempI];
            UIWidget_GMOption.OpenData viewData = new UIWidget_GMOption.OpenData()
            {
                gm = tempEntrance,
                onSelect = () =>
                {
                    if (CurSelectOption == tempEntrance)
                        return;
                    CurSelectOption = tempEntrance;
                    CurSelectCommandNode = CurSelectOption.GMRootNode;
                    // 刷新选项
                    foreach (var widget in optionWidgetCache)
                    {
                        widget.RefreshView_SelectedImg();
                    }
                    // 刷新命令
                    ClearCommandWidget();
                    RefreshView_Command();
                }
            };
            var widget = OpenUIWidget<UIWidget_GMOption>(UISR_Option.content, false, viewData);
            optionWidgetCache.Add(widget);
        }
    }

    /// <summary>
    /// 刷新命令界面
    /// </summary>
    private void RefreshView_Command()
    {
        foreach (var kvp in CurSelectCommandNode.Children)
        {
            // 如果是叶子节点，则根据数据绘制
            if (kvp.Value.CommandData != null)
            {
                switch (kvp.Value.CommandData.CommandType)
                {
                    // 按钮命令
                    case EGMCommandType.Button:
                    {
                        UIWidget_GMCommandButton.OpenData viewData = new UIWidget_GMCommandButton.OpenData()
                        {
                            data = kvp.Value.CommandData,
                            onClick = () =>
                            {
                                //
                                kvp.Value.CommandData.OnButtonClick?.Invoke(UIIF_ParamInputField1.text, UIIF_ParamInputField2.text, UIIF_ParamInputField3.text);
                            }
                        };
                        var widget = OpenUIWidget<UIWidget_GMCommandButton>(UISR_Command.content, false, viewData);
                        commandWidgetCache_Button.Add(widget);
                        break;
                    }

                    // 复选框命令
                    case EGMCommandType.Toggle:
                    {
                        UIWidget_GMCommandToggle.OpenData viewData = new UIWidget_GMCommandToggle.OpenData()
                        {
                            data = kvp.Value.CommandData,
                            onValueChanged = (b) =>
                            {
                                //
                                kvp.Value.CommandData.OnToggleChanged?.Invoke(b);
                            }
                        };
                        var widget = OpenUIWidget<UIWidget_GMCommandToggle>(UISR_Command.content, false, viewData);
                        commandWidgetCache_Toggle.Add(widget);
                        break;
                    }
                }
            }
            // 如果不是叶子节点，则直接创建一个按钮，点击后进入子节点
            else
            {
                GMCommandData commandData = new GMCommandData(CurSelectOption.OptionName, kvp.Key);
                UIWidget_GMCommandButton.OpenData viewData = new UIWidget_GMCommandButton.OpenData()
                {
                    data = commandData,
                    onClick = () =>
                    {
                        CurSelectCommandNode = kvp.Value;
                        ClearCommandWidget();
                        RefreshView_Command();
                    }
                };
                var widget = OpenUIWidget<UIWidget_GMCommandButton>(UISR_Command.content, false, viewData);
                commandWidgetCache_Button.Add(widget);
            }
        }

        if (CurSelectCommandNode.Parent != null)
        {
            GMCommandData commandData = new GMCommandData(CurSelectOption.OptionName, "返回上一级");
            UIWidget_GMCommandButton.OpenData viewData = new UIWidget_GMCommandButton.OpenData()
            {
                data = commandData,
                onClick = () =>
                {
                    CurSelectCommandNode = CurSelectCommandNode.Parent;
                    ClearCommandWidget();
                    RefreshView_Command();
                }
            };
            var widget = OpenUIWidget<UIWidget_GMCommandButton>(UISR_Command.content, false, viewData);
            commandWidgetCache_Button.Add(widget);
        }
    }

    private void ClearCommandWidget()
    {
        foreach (var widget in commandWidgetCache_Button)
        {
            CloseUIWidget(widget, true);
        }
        commandWidgetCache_Button.Clear();
        foreach (var widget in commandWidgetCache_Toggle)
        {
            CloseUIWidget(widget, true);
        }
        commandWidgetCache_Toggle.Clear();
    }

    private void InitData()
    {
        foreach (var entryData in Game.GetMod<ModGM>().gmDataDict.Values)
        {
            gmList.Add(entryData);
        }
        if (gmList == null || gmList.Count == 0)
        {
            CLog.Error("至少注册一个GM命令");
            return;
        }
        CurSelectOption = gmList[0];
        CurSelectCommandNode = CurSelectOption.GMRootNode;
    }
}

#endif