using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class FunctionJumpHandler_UI : IFunctionJumpHandler
{
    public void Jump(int functionJumpId, List<string> paramArray)
    {
        if (paramArray == null || paramArray.Count <= 0)
        {
            CLog.Error("跳转UI至少需要一个UIView的ID");
            return;
        }
        if (!int.TryParse(paramArray[0], out var _uiViewId))
        {
            CLog.Error($"{paramArray[0]}解析成int的UIViewId失败");
            return;
        }
        Game.GetMod<ModUI>().OpenSync(_uiViewId);
    }
}