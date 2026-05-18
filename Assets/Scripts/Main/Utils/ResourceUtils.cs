using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public static class ResourceUtils
{
    public static async UniTask<T> LoadResorceAsync<T>(string filePath)
        where T : Object
    {
        var req = Resources.LoadAsync<TextAsset>(filePath);
        await req;
        if (req == null || req.asset == null)
        {
            CLog.Error($"Resource文件夹中的资源加载失败：{filePath}");
            return null;
        }
        else
        {
            return req.asset as T;
        }
    }
}