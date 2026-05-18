using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using YooAsset;

public static class GameSafeCenter
{
    private const string ArtLibraryKey = "wodemimahenchang";
    private const string ImportantFileKeyFallback = "123456";
    public const string EnhancementEncryptKey = "elephant";

    private const string WindowsEncryptKeyFilePath = "C:/CustomKey/ArrowPuzzle1.txt";
    private const string MacOSEncryptKeyFilePath = "/Users/CustomKey/ArrowPuzzle1.txt";

    private const string ArtLibraryEncryptPrefix = "ENC:";
    public const string ImportantFileEncryptPrefix = "ENC1:";

    /// <summary>
    /// 获取运行时TextAsset实例（编辑器下会对一些资源加密）
    /// </summary>
    public static TextAsset GetRuntimeTextAsset(string assetPath, TextAsset textAsset)
    {
        if (textAsset == null)
            return null;

        if (textAsset.text.StartsWith(ImportantFileEncryptPrefix))
        {
            if (TryDecryptImportantFile(assetPath, textAsset.text, out var _plaintext1))
            {
                return CreateRuntimeTextAsset(textAsset.name, _plaintext1);
            }
        }
        else if (textAsset.text.StartsWith(ArtLibraryEncryptPrefix))
        {
            if (TryDecryptArtLibraryFile(textAsset.text, out var _plaintext2))
            {
                return CreateRuntimeTextAsset(textAsset.name, _plaintext2);
            }
        }

        return textAsset;
    }

    public static bool TryDecryptImportantFile(string assetPath, string encryptedText, out string plaintext)
    {
        plaintext = null;

        //
        var localEncryptKey = LoadLocalEncryptKey();
        if (!string.IsNullOrEmpty(localEncryptKey)
            && TryDecryptImportantFileWithoutFallback(encryptedText, localEncryptKey, out plaintext))
            return true;

        //
        if (!TryGetLevelConfigOverride(assetPath, out var _overrideTextAsset))
            return false;
        if (!TryGetCiphertext(_overrideTextAsset.text, ImportantFileEncryptPrefix, out var _ciphertext2))
            return false;

        return TryDecryptWithKey(_ciphertext2, ImportantFileKeyFallback, out plaintext, out _);
    }

    public static bool TryDecryptImportantFileWithoutFallback(string encryptedText, string encryptKey, out string plaintext)
    {
        plaintext = null;

        if (!TryGetCiphertext(encryptedText, ImportantFileEncryptPrefix, out var _ciphertext))
            return false;

        bool ret = TryDecryptWithKey(_ciphertext, encryptKey, out plaintext, out _);
        return ret;
    }

    public static bool TryDecryptArtLibraryFile(string encryptedText, out string plaintext)
    {
        plaintext = null;
        if (!TryGetCiphertext(encryptedText, ArtLibraryEncryptPrefix, out var _ciphertext))
            return false;

        return TryDecryptWithKey(_ciphertext, ArtLibraryKey, out plaintext, out _);
    }

    private static TextAsset CreateRuntimeTextAsset(string assetName, string plaintext)
    {
        var runtimeTextAsset = new TextAsset(plaintext)
        {
            name = assetName
        };
        return runtimeTextAsset;
    }

    private static bool TryGetLevelConfigOverride(string assetPath, out TextAsset textAsset)
    {
        textAsset = null;
        if (string.IsNullOrWhiteSpace(assetPath))
            return false;

        if (!assetPath.Contains("LevelConfigs", StringComparison.Ordinal))
            return false;

        string overrideLocation = $"level_{UnityEngine.Random.Range(100, 2563)}";
        AssetHandle handle = null;
        try
        {
            handle = YooAssets.LoadAssetSync<TextAsset>(overrideLocation);
            textAsset = handle?.AssetObject as TextAsset;
            return textAsset != null;
        }
        finally
        {
            handle?.Dispose();
        }
    }

    public static bool TryGetCiphertext(string encryptedText, string encryptPrefix, out string ciphertext)
    {
        ciphertext = null;
        if (string.IsNullOrWhiteSpace(encryptedText))
            return false;

        string encryptedContent = encryptedText.Trim();
        if (!encryptedContent.StartsWith(encryptPrefix, StringComparison.Ordinal))
            return false;

        ciphertext = encryptedContent.Substring(encryptPrefix.Length);
        return true;
    }

    public static string LoadLocalEncryptKey()
    {
        string keyFilePath;
#if UNITY_EDITOR_WIN
        keyFilePath = WindowsEncryptKeyFilePath;
#elif UNITY_EDITOR_OSX
        keyFilePath = MacOSEncryptKeyFilePath;
#else
        return null;
#endif

        try
        {
            if (!File.Exists(keyFilePath))
            {
                return null;
            }

            string key = File.ReadAllText(keyFilePath).Trim();
            return key;
        }
        catch
        {
            return null;
        }
    }

    private static bool TryDecryptWithKey(string ciphertext, string key, out string plaintext, out string error)
    {
        plaintext = null;
        error = null;

        byte[] plainBytes;
        try
        {
            plainBytes = AESUtils.DecryptFromBase64(ciphertext, key, CipherMode.CBC);
        }
        catch (Exception e)
        {
            error = e.ToString();
            return false;
        }

        try
        {
            plaintext = Encoding.UTF8.GetString(plainBytes);
            return true;
        }
        catch (Exception e)
        {
            error = e.ToString();
            return false;
        }
    }
}
