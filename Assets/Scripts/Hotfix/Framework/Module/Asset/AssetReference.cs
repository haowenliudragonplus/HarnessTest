using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class AssetReference
{
    private bool isProtected = false; //此资源是否被保护，不能被释放

    public readonly AssetHandle asset;
    public readonly string location;
    private string bundleName;

    private HashSet<GameObject> holderList = new HashSet<GameObject>(); //持有者列表

    public AssetReference(string location, AssetHandle ass)
    {
        this.location = location;
        asset = ass;
    }

    /// <summary>
    /// 增加一次引用
    /// </summary>
    public void AddReference(GameObject go)
    {
        holderList.Add(go);
    }

    /// <summary>
    /// 从引用列表中移除一次
    /// </summary>
    public void RemoveReference(GameObject holder)
    {
        holderList.Remove(holder);
        // if (CanRelease())
        // {
        //     Dispose();
        //     Game.GetMod<AssetMgr>().RemoveReference(this);
        // }
    }

    public bool CanRelease()
    {
        return holderList.Count <= 0 && !isProtected;
    }

    public void Dispose()
    {
        asset?.Dispose();
    }
}