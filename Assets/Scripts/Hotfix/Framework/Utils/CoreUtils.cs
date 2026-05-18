using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DragonPlus;
using DragonPlus.Config;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMGame;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public static class CoreUtils
{
    public static string GetResKey(int resourceId)
    {
        var resourceCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Resource>(resourceId);
        if (resourceCfg == null)
            return string.Empty;
        return resourceCfg.ResourceKey;
    }

    /// <summary>
    /// 获取Sprite
    /// </summary>
    public static Sprite GetSprite(int resourceId, GameObject holder)
    {
        var resourceCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Resource>(resourceId);
        if (resourceCfg == null)
            return null;
        if (resourceCfg.ResourceType != (int)EResourceType.Texture)
        {
            CLog.Error($"{resourceId}不是图片类型");
            return null;
        }
        int leftBracketIndex = resourceCfg.ResourceKey.IndexOf("[");
        int rightBracketIndex = resourceCfg.ResourceKey.IndexOf("]");
        if (leftBracketIndex != -1 && rightBracketIndex != -1)
        {
            string atlasName = resourceCfg.ResourceKey.Substring(0, leftBracketIndex);
            string spriteName = resourceCfg.ResourceKey.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);
            SpriteAtlas altas = Game.GetMod<ModAsset>().GetRes<SpriteAtlas>(atlasName).GetInstance(holder);
            if (altas == null)
            {
                CLog.Error($"找不到图集{atlasName}，resourceId：{resourceId}");
                return null;
            }
            Sprite sprite = altas.GetSprite(spriteName);
            if (sprite == null)
            {
                CLog.Error($"图集{atlasName}中找不到图片{spriteName}");
                return null;
            }
            return sprite;
        }
        else
        {
            string spriteName = resourceCfg.ResourceKey;
            var sprite = Game.GetMod<ModAsset>().GetRes<Sprite>(spriteName).GetInstance(holder);
            return sprite;
        }
    }

    /// <summary>
    /// 设置Image
    /// </summary>
    public static void SetImg(Image img, int resourceName, float scale = 1, bool setNativeSize = false)
    {
        if (img == null)
            return;
        Sprite sprite = GetSprite(resourceName, img.gameObject);
        SetImg(img, sprite, scale, setNativeSize);
    }

    /// <summary>
    /// 设置Image
    /// </summary>
    public static void SetImg(Image img, Sprite sprite, float scale = 1, bool setNativeSize = false)
    {
        if (sprite == null)
        {
            CLog.Error($"设置图片失败，Sprite不能为null");
            return;
        }
        img.sprite = sprite;
        img.transform.localScale = Vector3.one * scale;
        if (setNativeSize)
        {
            img.SetNativeSize();
        }
    }

    public static void SetRawImg(RawImage rawImg, Texture2D texture2D, float scale = 1, bool setNativeSize = false)
    {
        if (rawImg == null)
            return;
        rawImg.texture = texture2D;
        rawImg.transform.localScale = Vector3.one * scale;
        if (setNativeSize)
        {
            rawImg.SetNativeSize();
        }
    }

    /// <summary>
    /// 获取语言本地化值
    /// </summary>
    public static string GetLocalization(string key, params object[] args)
    {
        var languageValue = Game.GetMod<ModLanguage>().GetLocalization(Game.GetMod<ModLanguage>().CurLanguage, key, args);
        return languageValue;
    }

    /// <summary>
    /// 通过图集和图片名获取Sprite
    /// </summary>
    public static Sprite GetSprite(string atlasName, string spriteName, GameObject holder)
    {
        if (string.IsNullOrEmpty(atlasName) || string.IsNullOrEmpty(spriteName))
            return null;
        SpriteAtlas altas = Game.GetMod<ModAsset>().GetRes<SpriteAtlas>(atlasName).GetInstance(holder);
        if (altas == null)
        {
            CLog.Error($"找不到图集{atlasName}");
            return null;
        }
        Sprite sprite = altas.GetSprite(spriteName);
        if (sprite == null)
        {
            CLog.Error($"图集{atlasName}中找不到图片{spriteName}");
            return null;
        }
        return sprite;
    }

    /// <summary>
    /// 设置Image
    /// </summary>
    public static void SetImg(Image img, string atlasName, string spriteName, float scale = 1, bool setNativeSize = false)
    {
        if (img == null)
            return;
        Sprite sprite = GetSprite(atlasName, spriteName, img.gameObject);
        SetImg(img, sprite, scale, setNativeSize);
    }

    /// <summary>
    /// 方便异步调用的等几秒
    /// </summary>
    public static async UniTask WaitSeconds(float seconds, bool ignoreTimeScale = false,
        CancellationToken cancellationToken = default)
    {
        if (seconds <= 0)
            return;

        await UniTask.Delay((int)(seconds * 1000), ignoreTimeScale, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// 方便异步调用的等几秒
    /// </summary>
    public static async UniTaskVoid WaitSeconds(float seconds, Action finishCallback = null,
        CancellationToken cancellationToken = default, bool ignoreTimeScale = false)
    {
        if (seconds <= 0)
        {
            finishCallback?.Invoke();
            return;
        }

        await UniTask.Delay((int)(seconds * 1000), ignoreTimeScale, cancellationToken: cancellationToken);

        if (!cancellationToken.IsCancellationRequested)
            finishCallback?.Invoke();
    }
}