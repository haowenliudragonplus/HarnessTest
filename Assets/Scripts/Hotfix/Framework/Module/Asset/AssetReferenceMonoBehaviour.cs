using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源自动引用计数清理组件
/// </summary>
public class AssetReferenceMonoBehaviour : MonoBehaviour
{
    private HashSet<AssetReference> referenceList = new HashSet<AssetReference>();

    private void OnDestroy()
    {
        foreach (var reference in referenceList)
        {
            reference.RemoveReference(gameObject);
        }
        referenceList.Clear();
    }

    public void AddReference(AssetReference reference)
    {
        referenceList.Add(reference);
    }
}