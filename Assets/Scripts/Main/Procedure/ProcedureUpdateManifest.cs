using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using YooAsset;

public class ProcedureUpdateManifest : Procedure
{
    public ProcedureUpdateManifest(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        var defaultPackage = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);
        var op = defaultPackage.UpdatePackageManifestAsync(GameConfig.CurResVersion);
        await op.Task;
        if (op.Status == EOperationStatus.Succeed)
        {
            CLog.Info($"----------ProcedurepUpdateManifest Success\n清单资源版本号：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
        }
        else
        {
            CLog.Error($"----------ProcedurepUpdateManifest failed");
            await ShowRestartGameNotice();
        }
    }
}