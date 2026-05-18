using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public static class HttpUtils
{
    public static async UniTask<UnityWebRequest> Get(string url, string jsonData = "", int timeOut = 5)
    {
        var request = new UnityWebRequest(url);
        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        //request.certificateHandler = new WebReqSkipCert();//跳过ssl验证
        request.downloadHandler = new DownloadHandlerBuffer();
        request.timeout = timeOut;
        if (!string.IsNullOrEmpty(jsonData))
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(byteArray);
        }
        await request.SendWebRequest();
        return request;
    }

    public class WebReqSkipCert : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}