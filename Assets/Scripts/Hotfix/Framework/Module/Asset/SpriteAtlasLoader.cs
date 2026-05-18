using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.U2D;
using YooAsset;

public class SpriteAtlasLoader : MonoBehaviour
{
    private static SpriteAtlasLoader _instance;

    private Dictionary<string, SpriteAtlas> _loadedAtlas = new Dictionary<string, SpriteAtlas>(1000);
    private List<AssetHandle> _loadHandles = new List<AssetHandle>(1000);
    private ResourcePackage package;

    public static SpriteAtlasLoader Instance => _instance;

    public void Awake()
    {
        _instance = this;
        package = YooAssets.GetPackage(GlobalSetting.Ins.DefaultPackageName);
        SpriteAtlasManager.atlasRequested += RequestAtlas;

        // 预热某些图集
        PreloadAtlas("Atlas_InGame_UI");
    }

    public void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        SpriteAtlasManager.atlasRequested -= RequestAtlas;

        foreach (var handle in _loadHandles)
        {
            handle.Release();
        }
    }

    public bool IsAtlasLoaded(string atlasName)
    {
        return !string.IsNullOrEmpty(atlasName) && _loadedAtlas.ContainsKey(atlasName);
    }

    public SpriteAtlas GetLoadedAtlas(string atlasName)
    {
        if (string.IsNullOrEmpty(atlasName))
            return null;

        _loadedAtlas.TryGetValue(atlasName, out var atlas);
        return atlas;
    }

    public bool PreloadAtlas(string atlasName, Action<SpriteAtlas> onCompleted = null)
    {
        if (string.IsNullOrEmpty(atlasName))
        {
            onCompleted?.Invoke(null);
            return false;
        }

        var atlas = LoadAtlasInternal(atlasName);
        onCompleted?.Invoke(atlas);
        return atlas != null;
    }

    public bool PreloadAtlas(IList<string> atlasNames, Action onCompleted = null)
    {
        if (atlasNames == null || atlasNames.Count == 0)
        {
            onCompleted?.Invoke();
            return true;
        }

        bool allSucceed = true;
        for (int i = 0; i < atlasNames.Count; i++)
        {
            if (!PreloadAtlas(atlasNames[i]))
            {
                allSucceed = false;
            }
        }

        onCompleted?.Invoke();
        return allSucceed;
    }

    private void RequestAtlas(string atlasName, Action<SpriteAtlas> callback)
    {
        var atlas = LoadAtlasInternal(atlasName);
        if (atlas == null)
            return;

        callback?.Invoke(atlas);
    }

    private SpriteAtlas LoadAtlasInternal(string atlasName)
    {
        if (string.IsNullOrEmpty(atlasName))
            return null;

        if (_loadedAtlas.TryGetValue(atlasName, out var value))
        {
            return value;
        }

        var loadHandle = package.LoadAssetSync<SpriteAtlas>(atlasName);
        if (loadHandle.Status != EOperationStatus.Succeed)
        {
            CLog.Error($"Failed to load sprite atlas : {atlasName} ! {loadHandle.LastError}");
            return null;
        }

        var atlas = loadHandle.AssetObject as SpriteAtlas;
        _loadedAtlas.Add(atlasName, atlas);
        _loadHandles.Add(loadHandle);
        return atlas;
    }
}
