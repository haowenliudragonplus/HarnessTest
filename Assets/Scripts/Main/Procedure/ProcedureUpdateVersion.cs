using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using YooAsset;

public class ProcedureUpdateVersion : Procedure
{
    public ProcedureUpdateVersion(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        var defaultPackage = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);
        var requestPackageVersionOp = defaultPackage.RequestPackageVersionAsync();
        await requestPackageVersionOp.Task;
        if (requestPackageVersionOp.Status == EOperationStatus.Succeed)
        {
            CLog.Info($"获取远端的资源版本成功：{requestPackageVersionOp.PackageVersion}");
            if (ProcedureYooAssetInit.PlayMode == EPlayMode.EditorSimulateMode)
            {
                GameConfig.CurResVersion = "Simulate";
                CLog.Info($"编辑器模式，使用的资源版本：{GameConfig.CurResVersion}");
            }
            else if (!string.IsNullOrEmpty(requestPackageVersionOp.PackageVersion))
            {
                GameConfig.CurResVersion = requestPackageVersionOp.PackageVersion;
                CLog.Info($"获取到远端最新的资源版本，使用远端最新的资源版本：{GameConfig.CurResVersion}");
            }
            CLog.Info($"----------ProcedureUpdateVersion Success\n使用远端最新资源版本：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
        }
        else
        {
            CLog.Info($"----------获取远端资源版本失败，可能是无网状态进入游戏，尝试使用当前底包资源版本或上一次资源版本", logColor: ELogColor.Cyan);
            // 如果是覆盖安装，强制使用当前底包的版本
            if (ProcedureYooAssetInit.IsCoverInstall)
            {
                await GetBuildinPackageVersion(
                    onSuccess: () => { CLog.Info($"----------ProcedureUpdateVersion Success\n本次为覆盖安装首次进游戏，强制使用当前底包资源版本：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan); },
                    onFailed: () => { CLog.Error($"----------ProcedureUpdateVersion failed\n本次为覆盖安装首次进游戏，获取底包资源版本失败"); });
                return;
            }

            if (!string.IsNullOrEmpty(GameConfig.LastResVersion))
            {
                GameConfig.CurResVersion = GameConfig.LastResVersion;
                CLog.Info($"----------ProcedureUpdateVersion Success\n使用上一次资源版本：{GameConfig.LastResVersion}", logColor: ELogColor.Cyan);
            }
            else
            {
                if (string.IsNullOrEmpty(GameConfig.BuildInResVersion))
                {
                    await GetBuildinPackageVersion(
                        onSuccess: () => { CLog.Info($"----------ProcedureUpdateVersion Success\n使用底包资源版本：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan); },
                        onFailed: () => { CLog.Error($"----------ProcedureUpdateVersion failed\n获取底包资源版本失败"); });
                }
                else
                {
                    GameConfig.CurResVersion = GameConfig.BuildInResVersion;
                    CLog.Info($"----------ProcedureUpdateVersion Success\n使用底包资源版本：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
                }
            }
        }
    }
}