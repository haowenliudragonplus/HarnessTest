using Framework;
using UnityEngine;

/// <summary>
/// 加载非GameObject资源中间器
/// </summary>
public class ResProvider<T>
    where T : class
{
    AssetReference reference;

    public ResProvider(AssetReference reference)
    {
        this.reference = reference;
    }

    /// <summary>
    /// 获取资源实例
    /// </summary>
    public T GetInstance(GameObject goHolder)
    {
        if (reference == null)
            return null;

        if (goHolder == null)
        {
            CLog.Error($"获取资源实例不能没有实体GameObject持有者，资源：{reference.location}");
            return null;
        }

        var res = reference.asset.AssetObject as T;
#if UNITY_EDITOR
        if (typeof(T) == typeof(TextAsset))
        {
            res = GameSafeCenter.GetRuntimeTextAsset(reference.asset.GetAssetInfo().AssetPath, res as TextAsset) as T;
        }
#endif
        reference.AddReference(goHolder);
        AddAssetReferenceMonoBehaviour(goHolder);
        return res;
    }

    /// <summary>
    /// 删除实例化的GameObject
    /// </summary>
    public void DeleteInstance(GameObject go)
    {
        if (go == null)
            return;

        reference.RemoveReference(go);
    }

    private void AddAssetReferenceMonoBehaviour(GameObject goHolder)
    {
        AssetReferenceMonoBehaviour mono = goHolder.GetComponent<AssetReferenceMonoBehaviour>();
        if (mono == null)
        {
            mono = goHolder.AddComponent<AssetReferenceMonoBehaviour>();
        }
        mono.AddReference(reference);
    }
}
