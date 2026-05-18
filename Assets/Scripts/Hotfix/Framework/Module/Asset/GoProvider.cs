using UnityEngine;

/// <summary>
/// 加载GameObject资源中间器
/// </summary>
public class GoProvider
{
    private AssetReference reference;

    public GoProvider(AssetReference reference)
    {
        this.reference = reference;
    }

    /// <summary>
    /// 获取实例化后的GameObject
    /// </summary>
    public GameObject GetInstance(Transform parent = null)
    {
        if (reference == null)
            return null;

        GameObject go = reference.asset.InstantiateSync(parent);
        if (go == null)
            return null;

        reference.AddReference(go);
        AddAssetReferenceMonoBehaviour(go);
        return go;
    }

    /// <summary>
    /// 删除实例化的GameObject
    /// </summary>
    public void DeleteInstance(GameObject go)
    {
        if (go == null)
            return;

        reference.RemoveReference(go);
        Object.Destroy(go);
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