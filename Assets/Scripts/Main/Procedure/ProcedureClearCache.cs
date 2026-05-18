using Cysharp.Threading.Tasks;
using Framework;
using YooAsset;

public class ProcedureClearCache : Procedure
{
    public ProcedureClearCache(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();
        var defaultPackage = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);

        // 清理不再使用的bundle
        var ClearUnusedBundleFilesOp = defaultPackage.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
        await ClearUnusedBundleFilesOp;
        if (ClearUnusedBundleFilesOp.Status == EOperationStatus.Succeed)
        {
            CLog.Info($"----------ProcedureClearCache Success：ClearUnusedBundleFiles", logColor: ELogColor.Cyan);
        }
        else
        {
            CLog.Error($"----------ProcedureClearCache failed：ClearUnusedBundleFiles");
        }

        // 清理不再使用的清单文件
        var clearUnusedManifestFilesOp = defaultPackage.ClearCacheFilesAsync(EFileClearMode.ClearUnusedManifestFiles);
        await clearUnusedManifestFilesOp;
        if (clearUnusedManifestFilesOp.Status == EOperationStatus.Succeed)
        {
            CLog.Info($"----------ProcedureClearCache Success：ClearUnusedManifestFiles", logColor: ELogColor.Cyan);
        }
        else
        {
            CLog.Error($"----------ProcedureClearCache failed：ClearUnusedManifestFiles");
        }
    }
}