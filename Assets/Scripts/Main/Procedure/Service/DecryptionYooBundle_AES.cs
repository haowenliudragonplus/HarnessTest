using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using YooAsset;

public class DecryptionYooBundle_AES : IDecryptionServices
{
    public DecryptResult LoadAssetBundle(DecryptFileInfo fileInfo)
    {
        var outputStream = CreateDecryptedStream(fileInfo);
        DecryptResult result = new DecryptResult();
        result.Result = AssetBundle.LoadFromStream(outputStream, fileInfo.FileLoadCRC, GetManagedReadBufferSize());
        result.ManagedStream = outputStream;
        return result;
    }
    public DecryptResult LoadAssetBundleAsync(DecryptFileInfo fileInfo)
    {
        var outputStream = CreateDecryptedStream(fileInfo);
        DecryptResult result = new DecryptResult();
        result.CreateRequest = AssetBundle.LoadFromStreamAsync(outputStream, fileInfo.FileLoadCRC, GetManagedReadBufferSize());
        result.ManagedStream = outputStream;
        return result;
    }
    public DecryptResult LoadAssetBundleFallback(DecryptFileInfo fileInfo)
    {
        var outputStream = CreateDecryptedStream(fileInfo);
        DecryptResult result = new DecryptResult();
        result.Result = AssetBundle.LoadFromStream(outputStream, fileInfo.FileLoadCRC, GetManagedReadBufferSize());
        result.ManagedStream = outputStream;
        return result;
    }
    public byte[] ReadFileData(DecryptFileInfo fileInfo)
    {
        return DecryptFileData(fileInfo);
    }
    public string ReadFileText(DecryptFileInfo fileInfo)
    {
        var plaintextByteArray = DecryptFileData(fileInfo);
        return Encoding.UTF8.GetString(plaintextByteArray);
    }

    private static MemoryStream CreateDecryptedStream(DecryptFileInfo fileInfo)
    {
        return new MemoryStream(DecryptFileData(fileInfo));
    }

    private static byte[] DecryptFileData(DecryptFileInfo fileInfo)
    {
        var byteArray = File.ReadAllBytes(fileInfo.FileLoadPath);
        return AESUtils.DecryptFromBase64(byteArray, GlobalSetting.Ins.Res_AESKey, CipherMode.CBC);
    }

    private static uint GetManagedReadBufferSize()
    {
        return 1024;
    }
}
