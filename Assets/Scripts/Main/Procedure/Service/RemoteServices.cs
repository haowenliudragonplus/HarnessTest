using YooAsset;

public class RemoteServices : IRemoteServices
{
    private readonly string _defaultHostServer;
    private readonly string _fallbackHostServer;

    public RemoteServices(string defaultHostServer, string fallbackHostServer)
    {
        this._defaultHostServer = defaultHostServer;
        this._fallbackHostServer = fallbackHostServer;
    }

    string IRemoteServices.GetRemoteMainURL(string fileName) => this._defaultHostServer + "/" + fileName;

    string IRemoteServices.GetRemoteFallbackURL(string fileName) => this._fallbackHostServer + "/" + fileName;
}