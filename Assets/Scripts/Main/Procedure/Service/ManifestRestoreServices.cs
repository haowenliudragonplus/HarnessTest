using YooAsset;

public class ManifestRestoreServices : IManifestRestoreServices
{
    public byte[] RestoreManifest(byte[] fileData)
    {
        return XORUtils.Crypto(fileData, GlobalSetting.Ins.Res_AESKey);
    }
}