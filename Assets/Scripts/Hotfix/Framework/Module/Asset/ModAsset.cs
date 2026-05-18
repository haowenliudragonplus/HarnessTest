using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using TMGame;
using UnityEngine;
using YooAsset;
using UEObject = UnityEngine.Object;

/// <summary>
/// 资源类型
/// </summary>
public enum EResourceType
{
    Prefab = 1,
    Texture,
    Audio,
    Spine,
}

public class ModAsset : ModuleBase
{
    private const int downloadingMaxNum = 3;
    private const int failedTryAgain = 3;

    private ResourcePackage package;

    private Dictionary<string, AssetReference> assetReferenceCache = new Dictionary<string, AssetReference>();

    private Dictionary<string, ResourceDownloaderOperation> downloadOperationDict = new Dictionary<string, ResourceDownloaderOperation>(); //下载任务缓存

    #region 下载资源

    /// <summary>
    /// 下载资源
    /// </summary>
    public async UniTask Download(string tag,
        Action<string, long> onStartDownloadFile = null, Action<int, int, long, long> onDownloadProgress = null,
        Action<bool, bool> onDownloadOver = null, Action<string, string> onDownloadError = null)
    {
        if (downloadOperationDict.ContainsKey(tag))
            return;

        EHasAssetResult result = CheckTagNeedDownload(tag);
        if (result != EHasAssetResult.AssetOnline)
        {
            CLog.Info($"不需要下载资源，tag：[{tag}]");
            onDownloadOver?.Invoke(false, true);
            var evt = new EvtDownloadOver();
            evt.isByDownload = false;
            evt.isSucceed = true;
            evt.tag = tag;
            Game.GetMod<ModEvent>().Dispatch(evt);
            return;
        }

        var downloader = package.CreateResourceDownloader(tag, downloadingMaxNum, failedTryAgain);
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;
        float totalDownloadMB = totalDownloadBytes * 1f / (1024 * 1024);
        CLog.Info($"开始下载资源，tag：[{tag}]，资源总数量：{totalDownloadCount}，总大小（MB）：{totalDownloadMB}");
        downloader.DownloadFileBeginCallback = (data) =>
        {
            var evt = new EvtStartDownload();
            evt.fileName = data.FileName;
            evt.sizeBytes = data.FileSize;
            Game.GetMod<ModEvent>().Dispatch(evt);
            onStartDownloadFile?.Invoke(data.FileName, data.FileSize);
        };
        downloader.DownloadUpdateCallback = (data) =>
        {
            var evt = new EvtDownloadProgress();
            evt.tag = tag;
            evt.totalDownloadCount = totalDownloadCount;
            evt.currentDownloadCount = data.CurrentDownloadCount;
            evt.totalDownloadBytes = totalDownloadBytes;
            evt.currentDownloadBytes = data.CurrentDownloadBytes;
            Game.GetMod<ModEvent>().Dispatch(evt);
            onDownloadProgress?.Invoke(totalDownloadCount, data.CurrentDownloadCount, totalDownloadBytes, data.CurrentDownloadBytes);
        };
        downloader.DownloadFinishCallback = (data) =>
        {
            CLog.Info($"下载资源结束，tag：[{tag}]，下载结果：[{data.Succeed}]");
            downloadOperationDict.Remove(tag);
            onDownloadOver?.Invoke(true, data.Succeed);
            var evt = new EvtDownloadOver();
            evt.isByDownload = true;
            evt.isSucceed = data.Succeed;
            evt.tag = tag;
            Game.GetMod<ModEvent>().Dispatch(evt);
        };
        downloader.DownloadErrorCallback = (data) => { onDownloadError?.Invoke(data.FileName, data.ErrorInfo); };
        downloader.BeginDownload();
        downloadOperationDict.Add(tag, downloader);
        await downloader.Task;
    }

    /// <summary>
    /// 检查某个标签下的资源是否有需要从远端下载的
    /// </summary>
    public EHasAssetResult CheckTagNeedDownload(string tag)
    {
        var assetInfos = YooAssets.GetAssetInfos(tag);
        if (assetInfos != null && assetInfos.Length > 0)
        {
            for (int i = 0; i < assetInfos.Length; i++)
            {
                if (IsNeedDownloadFromRemote(assetInfos[i]))
                    return EHasAssetResult.AssetOnline;
            }
            return EHasAssetResult.AssetOnDisk;
        }
        return EHasAssetResult.Invalid;
    }

    /// <summary>
    /// 是否需要从远端更新下载。
    /// </summary>
    public bool IsNeedDownloadFromRemote(string location)
    {
        return YooAssets.IsNeedDownloadFromRemote(location);
    }

    /// <summary>
    /// 是否需要从远端更新下载
    /// </summary>
    public bool IsNeedDownloadFromRemote(AssetInfo assetInfo)
    {
        return YooAssets.IsNeedDownloadFromRemote(assetInfo);
    }

    #endregion 下载资源

    #region 加载资源

    /// <summary>
    /// 同步获取不需要实例化的资源
    /// </summary>
    public ResProvider<T> GetRes<T>(string location)
        where T : UEObject
    {
        assetReferenceCache.TryGetValue(location, out var _reference);
        if (_reference == null)
        {
            AssetHandle handle = YooAssets.LoadAssetSync<T>(location);
            _reference = new AssetReference(location, handle);
            assetReferenceCache.Add(location, _reference);
        }
        return new ResProvider<T>(_reference);
    }

    /// <summary>
    /// 异步获取不需要实例化的资源
    /// </summary>
    public void GetResAsync<T>(string location, Action<ResProvider<T>> callback)
        where T : UEObject
    {
        ResProvider<T> resProvider;

        assetReferenceCache.TryGetValue(location, out var _reference);
        if (_reference == null)
        {
            YooAssets.LoadAssetAsync<T>(location).Completed += handle =>
            {
                _reference = new AssetReference(location, handle);
                assetReferenceCache.Add(location, _reference);

                resProvider = new ResProvider<T>(_reference);
                callback(resProvider);
            };
        }
        else
        {
            resProvider = new ResProvider<T>(_reference);
            callback(resProvider);
        }
    }

    /// <summary>
    /// 同步获取GameObject
    /// </summary>
    public GoProvider GetGameObject(string location)
    {
        assetReferenceCache.TryGetValue(location, out var _reference);
        if (_reference == null)
        {
            _reference = new AssetReference(location, YooAssets.LoadAssetSync<GameObject>(location));
            assetReferenceCache.Add(location, _reference);
        }
        return new GoProvider(_reference);
    }

    /// <summary>
    /// 异步获取GameObject
    /// </summary>
    public void GetGameObjectAsync(string location, Action<GoProvider> callback)
    {
        GoProvider goProvider;

        assetReferenceCache.TryGetValue(location, out var _reference);
        if (_reference == null)
        {
            YooAssets.LoadAssetAsync<GameObject>(location).Completed += handle =>
            {
                _reference = new AssetReference(location, handle);
                assetReferenceCache.Add(location, _reference);

                goProvider = new GoProvider(_reference);
                callback(goProvider);
            };
        }
        else
        {
            goProvider = new GoProvider(_reference);
            callback(goProvider);
        }
    }

    #endregion 加载资源

    #region 卸载资源

    /// <summary>
    /// 卸载不再使用的资源
    /// </summary>
    public void UnloadUnusedAssets()
    {
        package.UnloadUnusedAssetsAsync();
        GC.Collect();
    }

    /// <summary>
    /// 卸载某一个资源
    /// </summary>
    public void TryUnloadUnusedAsset(string location)
    {
        package.TryUnloadUnusedAsset(location);
    }

    /// <summary>
    /// 卸载全部资源
    /// </summary>
    public void ForceUnloadAllAssets()
    {
        package.UnloadAllAssetsAsync();
        GC.Collect();
    }

    #endregion 卸载资源

    #region 对外接口

    public EHasAssetResult HasAsset(string location)
    {
        if (string.IsNullOrEmpty(location))
            return EHasAssetResult.Invalid;
        if (!YooAssets.CheckLocationValid(location))
            return EHasAssetResult.Invalid;

        AssetInfo assetInfo = YooAssets.GetAssetInfo(location);
        if (assetInfo == null)
            return EHasAssetResult.Invalid;

        if (IsNeedDownloadFromRemote(assetInfo))
            return EHasAssetResult.AssetOnline;
        return EHasAssetResult.AssetOnDisk;
    }

    /// <summary>
    /// 检查资源是否可以清理并清理
    /// </summary>
    public void CheckAssetReferenceAndDispose()
    {
        List<string> toReleaseRefreneceList = new List<string>();
        foreach (var asset in assetReferenceCache)
        {
            if (asset.Value.CanRelease())
            {
                toReleaseRefreneceList.Add(asset.Key);
            }
        }

        foreach (var reference in toReleaseRefreneceList)
        {
            assetReferenceCache[reference].Dispose();
            assetReferenceCache.Remove(reference);
        }
    }

    public void RemoveReference(AssetReference reference)
    {
        assetReferenceCache.Remove(reference.location);
    }

    #endregion 对外接口

    #region 生命周期

    public void Init()
    {
        package = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);
    }
    public void Start()
    {
    }
    public void Update()
    {
    }

    public void Dispose()
    {
    }

    #endregion 生命周期
}

/// <summary>
/// 检查资源是否存在的结果
/// </summary>
public enum EHasAssetResult : byte
{
    /// <summary>
    /// 资源定位地址无效。
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// 资源需要从远端更新下载。
    /// </summary>
    AssetOnline,

    /// <summary>
    /// 存在资源且存储在磁盘上。
    /// </summary>
    AssetOnDisk,
}