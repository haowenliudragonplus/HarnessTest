using System;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Launcher;
using Main;
using UnityEngine;
using YooAsset;

public class ProcedureDownloadFiles : Procedure
{
    public enum EProcedureDownloadFilesState
    {
        None,
        NoNeed = 1, //不需要更新
        DownloadFinish, //下载完成
        Retry, //重试下载
        Skip, //跳过更新
    }

    private ResourcePackage package;

    private const int DownloadingMaxNum = 3;
    private const int FailedTryAgain = 3;

    private EProcedureDownloadFilesState state;

    public ProcedureDownloadFiles(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        package = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);

        while (state == EProcedureDownloadFilesState.None
               || state == EProcedureDownloadFilesState.Retry)
        {
            await ExecuteDownload();
        }

        if (state == EProcedureDownloadFilesState.NoNeed
            || state == EProcedureDownloadFilesState.DownloadFinish
            || state == EProcedureDownloadFilesState.Skip)
        {
            await ExecuteNextProcedure();
        }
    }

    private async UniTask ExecuteDownload()
    {
        // 创建下载器
        var downloader = YooAssets.CreateResourceDownloader(GlobalSetting.Ins.BuildInResTag, DownloadingMaxNum, FailedTryAgain);
        if (downloader.TotalDownloadCount == 0)
        {
            state = EProcedureDownloadFilesState.NoNeed;
        }
        else
        {
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventDownloadStart);
            UIView_Boot.Ins.SetViewState(EBootViewState.Download);
            // todo lhw 下载前检测磁盘空间不足
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            float totalDownloadMB = totalDownloadBytes * 1f / (1024 * 1024);
            CLog.Info($"有需要更新下载的资源，总数量：{totalDownloadCount}，总大小（MB）：{totalDownloadMB}");
            downloader.DownloadFileBeginCallback = OnDownloadFileBegin;
            downloader.DownloadErrorCallback = OnDownloadErrorCallback;
            downloader.DownloadUpdateCallback = OnDownloadProgressCallback;
            downloader.BeginDownload();
            await downloader.Task;
            UIView_Boot.Ins.SetViewState(EBootViewState.Common);
            if (downloader.Status != EOperationStatus.Succeed)
            {
                BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventDownloadFailure);
                CLog.Info($"ProcedureDownloadFiles failed");

                // 显示下载失败提示框
                await ShowDownloadFailNotice();
            }
            else
            {
                state = EProcedureDownloadFilesState.DownloadFinish;
            }
        }
    }

    /// <summary>
    /// 显示下载失败提示框
    /// </summary>
    private async UniTask ShowDownloadFailNotice()
    {
        var tcs = new UniTaskCompletionSource();
        UIView_MessageBox.Ins.ShowMessageBox(GameConfig.GetLocaleStr("UI_download_resources_error"),
            showRightBtn: true, rightBtnName: GameConfig.GetLocaleStr("UI_button_retry_text"),
            leftBtnName: GameConfig.GetLocaleStr("UI_button_ok"), closeSelf: true,
            onRightBtn: () =>
            {
                state = EProcedureDownloadFilesState.Retry;
                tcs.TrySetResult();
            },
            onLeftBtn: () =>
            {
                state = EProcedureDownloadFilesState.Skip;
                tcs.TrySetResult();
            });
        await tcs.Task;
    }

    /// <summary>
    /// 执行下一步
    /// </summary>
    private async UniTask ExecuteNextProcedure()
    {
        if (state == EProcedureDownloadFilesState.NoNeed)
        {
            CLog.Info($"----------ProcedureDownloadFiles Success 没有要更新的资源", logColor: ELogColor.Cyan);
        }
        else if (state == EProcedureDownloadFilesState.Skip)
        {
            string resVersion = string.Empty;
            // 跳过更新，如果是覆盖安装首次登录则强制使用底包版本，否则尝试使用上一次的资源版本或底包版本进入游戏
            if (ProcedureYooAssetInit.IsCoverInstall)
            {
                await GetBuildinPackageVersion(
                    onSuccess: () =>
                    {
                        CLog.Info($"本次为覆盖安装首次进游戏，强制使用当前底包资源版本重新获取清单：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
                        resVersion = GameConfig.CurResVersion;
                    });
            }
            else
            {
                if (string.IsNullOrEmpty(GameConfig.LastResVersion))
                {
                    if (string.IsNullOrEmpty(GameConfig.BuildInResVersion))
                    {
                        await GetBuildinPackageVersion(
                            onSuccess: () =>
                            {
                                resVersion = GameConfig.BuildInResVersion;
                                CLog.Error($"使用底包资源版本重新获取清单，底包资源版本号：{resVersion}");
                            });
                    }
                    else
                    {
                        resVersion = GameConfig.BuildInResVersion;
                        CLog.Error($"使用底包资源版本重新获取清单，底包资源版本号：{resVersion}");
                    }
                }
                else
                {
                    resVersion = GameConfig.LastResVersion;
                    CLog.Error($"使用上一次资源版本重新获取清单，上一次资源版本号：{resVersion}");
                }
            }
            GameConfig.CurResVersion = resVersion;
            // 重新获取清单
            var op = package.UpdatePackageManifestAsync(GameConfig.CurResVersion);
            await op.Task;
            if (op.Status == EOperationStatus.Succeed)
            {
                CLog.Info($"重新获取清单文件成功，清单文件资源版本号：{GameConfig.CurResVersion}");
            }
            else
            {
                CLog.Error($"重新获取清单文件失败，清单文件资源版本号：{GameConfig.CurResVersion}");
                await ShowRestartGameNotice();
            }
            if (string.IsNullOrEmpty(GameConfig.LastResVersion))
            {
                CLog.Info($"----------ProcedureDownloadFiles Success 跳过资源更新，并使用底包的资源版本进入游戏，资源版本号：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
            }
            else
            {
                CLog.Info($"----------ProcedureDownloadFiles Success 跳过资源更新，并使用上一次的资源版本进入游戏，资源版本号：{GameConfig.CurResVersion}", logColor: ELogColor.Cyan);
            }
        }
        else if (state == EProcedureDownloadFilesState.DownloadFinish)
        {
            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventDownloadFinish);
            CLog.Info($"----------ProcedureDownloadFiles Success 需要更新的资源全部下载成功", logColor: ELogColor.Cyan);
        }

        GameConfig.LastResVersion = GameConfig.CurResVersion;
        CLog.Info("设置上一次的资源版本号：" + GameConfig.LastResVersion);

        await SDK.Instance.OnGameAssetReady();
    }

    private void OnDownloadFileBegin(DownloadFileData data)
    {
        CLog.Info($"开始下载，文件名称：{data.FileName}，文件大小：{data.FileSize * 1f / (1024 * 1024)}MB");
    }

    private void OnDownloadProgressCallback(DownloadUpdateData data)
    {
        float v = 1024 * 1024;
        double v1 = Math.Round(data.CurrentDownloadBytes / v, 1);
        double v2 = Math.Round(data.TotalDownloadBytes / v, 1);
        UIView_Boot.Ins.UpdateDownloadProgress(data.CurrentDownloadBytes * 1f / data.TotalDownloadBytes,
            GameConfig.GetLocaleStr("UI_loading_upgrade",
                $"{data.CurrentDownloadCount}/{data.TotalDownloadCount}", $"{v1}MB/{v2}MB"));
    }

    private void OnDownloadErrorCallback(DownloadErrorData data)
    {
        CLog.Error($"下载文件失败\n" +
                   $"file：{data.FileName}\n" +
                   $"error：{data.ErrorInfo}");
    }
}