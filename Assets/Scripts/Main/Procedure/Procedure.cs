using System;
using Cysharp.Threading.Tasks;
using Framework;
using Main;
using YooAsset;

public class Procedure
{
    public ProcedureContext context;

    public Procedure(ProcedureContext context)
    {
        this.context = context;
    }

    public virtual async UniTask ExecuteProcedure()
    {
        UIView_Boot.Ins.SetTargetValue(context.targetValue);
    }

    /// <summary>
    /// 显示重启游戏提示框
    /// </summary>
    protected async UniTask ShowRestartGameNotice()
    {
        var tcs = new UniTaskCompletionSource();
        UIView_MessageBox.Ins.ShowMessageBox(GameConfig.GetLocaleStr("UI_no_internet_tips"),
            leftBtnName: GameConfig.GetLocaleStr("UI_button_ok"), closeSelf: true,
            onLeftBtn: () =>
            {
                if (string.IsNullOrEmpty(GameConfig.LastResVersion))
                {
                    GameConfig.CurResVersion = string.Empty;
                }
                else
                {
                    GameConfig.CurResVersion = GameConfig.LastResVersion;
                }
                GameUtils.Quit();
            }, showRightBtn: false);
        await tcs.Task;
    }

    /// <summary>
    /// 获取底包资源版本
    /// </summary>
    protected async UniTask GetBuildinPackageVersion(Action onSuccess = null, Action onFailed = null)
    {
        var copyBuildinManifestOp = new GetBuildinPackageVersionOperation(GlobalSetting.Ins.DefaultPackageName);
        YooAssets.StartOperation(copyBuildinManifestOp);
        await copyBuildinManifestOp.Task;
        if (copyBuildinManifestOp.Status == EOperationStatus.Succeed)
        {
            GameConfig.BuildInResVersion = copyBuildinManifestOp.PackageVersion;
            GameConfig.CurResVersion = GameConfig.BuildInResVersion;
            onSuccess?.Invoke();
        }
        else
        {
            onFailed?.Invoke();
            await ShowRestartGameNotice();
        }
    }
}