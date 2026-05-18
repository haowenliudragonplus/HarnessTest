using System.Collections.Generic;
using System.Threading;
using AppLovinMax.Scripts.IntegrationManager.Editor;
using Cysharp.Threading.Tasks;
using DragonPlus.Core;
using DragonPlus.Core.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 设置DragonSDK
/// </summary>
public class BuildTask_SetUpDragonSDK : BuildTaskNode
{
    public BuildTask_SetUpDragonSDK(string tag, bool ignoreFail = false, bool autoNextNode = true) : base(tag, ignoreFail, autoNextNode)
    {
    }

    public override async UniTask OnExecute(BehaviourSequenceParam param, CancellationTokenSource cts)
    {
        var buildParam = param as AppBuilderParam;

        // 等待原生库生成成功
        await DragonPlus.Account.Editor.AccountEditorUtility.SetupThirdPartyLibraries();

        // 写入SDK的项目配置
        ThirdPartySetUpTools.SetUpThirdPartySetting();
        // 设置AppLovin
        {
            AppLovinSettings.Instance.QualityServiceEnabled = true;
            Dictionary<string, IThirdPartyConfigInfo> dictionary = ThirdPartyConfigProvider.Load();
            if (dictionary.ContainsKey(ThirdParty.MAX.ToString()) && dictionary[ThirdParty.MAX.ToString()] != null)
            {
                MaxConfigInfo maxConfigInfo = (MaxConfigInfo)dictionary[ThirdParty.MAX.ToString()];
                AppLovinSettings.Instance.SdkKey = maxConfigInfo.sdkKey;
                if (maxConfigInfo.ConsentFlowEnabled)
                {
                    AppLovinInternalSettings.Instance.ConsentFlowEnabled = true;
                    AppLovinInternalSettings.Instance.ShouldShowTermsAndPrivacyPolicyAlertInGDPR = true;
                    AppLovinInternalSettings.Instance.ConsentFlowPrivacyPolicyUrl = ConfigurationController.Instance.PrivacyPolicyURL;
                    AppLovinInternalSettings.Instance.ConsentFlowTermsOfServiceUrl = ConfigurationController.Instance.TermsOfServiceURL;
                    AppLovinInternalSettings.Instance.OverrideDefaultUserTrackingUsageDescriptions = false;
                    AppLovinInternalSettings.Instance.UserTrackingUsageLocalizationEnabled = false;
                    if (!buildParam.isRelease)
                    {
                        AppLovinInternalSettings.Instance.DebugUserGeography = MaxSdkBase.ConsentFlowUserGeography.Gdpr;
                    }
                    AppLovinInternalSettings.Instance.Save();
                }
            }
            AppLovinSettings.Instance.SaveAsync();
        }
        //
        {
            var configurationController = Resources.Load<ConfigurationController>("Settings/ConfigurationController");
            if (buildParam.isRelease)
            {
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                {
                    // 将有可能带dragon的字符串删掉
                    configurationController.API_Server_URL_Beta = "";
                    configurationController.AndroidKeyStorePath = "";
                    configurationController.AndroidKeyStorePass = "";
                    configurationController.AndroidKeyStoreAlias = "";
                    configurationController.AndroidKeyStoreAliasPass = "";
                }
                configurationController.version = VersionStatus.RELEASE;
            }
            else
            {
                configurationController.version = VersionStatus.DEBUG;
            }
            EditorUtility.SetDirty(configurationController);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        Log("DragonSDK设置成功");
    }
}