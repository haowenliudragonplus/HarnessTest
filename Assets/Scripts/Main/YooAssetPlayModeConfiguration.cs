#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using YooAsset;

/// <summary>
/// Yoo运行模式配置
/// </summary>
[CreateAssetMenu(fileName = "YooAssetPlayModeConfiguration", menuName = "YooAssetPlayModeConfiguration", order = 0)]
public class YooAssetPlayModeConfiguration : ScriptableObject
{
    private static YooAssetPlayModeConfiguration ins;
    public static YooAssetPlayModeConfiguration Ins
    {
        get
        {
            if (ins == null)
            {
                ins = AssetDatabase.LoadAssetAtPath<YooAssetPlayModeConfiguration>("Assets/Settings/YooAssetPlayModeConfiguration.asset");
            }
            return ins;
        }
    }

    [Header("**********配置只在Unity编辑器下生效")]
    [Space(10)]
    [Header("运行模式")]
    public EPlayMode PlayMode;
    [Header("应用版本（不填则取GameConfig_Version的配置）")]
    public string AppVersion;
    [Header("资源版本（不填则取远端当前应用版本内最新的）")]
    public string ResVersion;
}

#endif