using DragonPlus.Config.Common;
using Framework;
using TMGame;

/// <summary>
/// 功能跳转类型
/// </summary>
public enum EFunctionJumpType
{
    None = 0,
    UI = 1, //跳转UI
}

public class ModFunctionJump : ModuleBase
{
    public void Jump(int functionJumpId)
    {
        var jumpCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_FunctionJump>(functionJumpId);
        if (jumpCfg == null)
        {
            CLog.Error($"FunctionJump表里找不到functionJumpId：{functionJumpId}");
            return;
        }

        var handler = GetFunctionJumpHandler((EFunctionJumpType)jumpCfg.JumpType);
        handler.Jump(functionJumpId, jumpCfg.ParamArray);
    }

    /// <summary>
    /// 获取功能跳转处理器类型
    /// </summary>
    ///----------在此扩展
    private IFunctionJumpHandler GetFunctionJumpHandler(EFunctionJumpType jumpType)
    {
        IFunctionJumpHandler handler = null;
        switch (jumpType)
        {
            case EFunctionJumpType.UI:
                handler = new FunctionJumpHandler_UI();
                break;
        }
        return handler;
    }
}