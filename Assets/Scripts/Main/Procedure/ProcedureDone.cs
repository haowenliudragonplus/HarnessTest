using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

public class ProcedureDone : Procedure
{
    public ProcedureDone(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        // 删除EventSystem
        Object.Destroy(GameObject.Find(Const_Boot.ScenePath_EventSystem));

        CLog.Info("----------ProcedureDone Success，主工程加载完成，开始初始化热更工程", logColor: ELogColor.Cyan);
        // 开启热更模块
        ReflectUtils.InvokeStaticMethod("Hotfix", "", "Game", "Entrance");

        CLog.Info("----------等待IsBootSuccess == true", logColor: ELogColor.Cyan);
        while (!UIView_Boot.IsBootSuccess)
        {
            await UniTask.Yield();
        }

        CLog.Info("----------等待进度条完成", logColor: ELogColor.Cyan);
        // 检查进度条到100%
        await UIView_Boot.Ins.CheckBootSuccess();

        // 销毁启动场景UI
        Object.Destroy(GameObject.Find(Const_Boot.ScenePath_UIRoot));
        CLog.Info("----------打开主界面成功，销毁启动场景UI", logColor: ELogColor.Cyan);
    }
}