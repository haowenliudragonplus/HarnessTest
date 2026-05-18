using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

/// <summary>
/// 不收集贴图目录
/// </summary>
public class CollectExcludeTexture : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        string extensionName = Path.GetExtension(data.AssetPath);
        bool valid = !extensionName.Contains("png")
                     && !extensionName.Contains("jpg");
        return valid;
    }
}

/// <summary>
/// 不收集贴图目录和动画目录
/// </summary>
public class CollectExcludeTextureAndAnimation : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        string extensionName = Path.GetExtension(data.AssetPath);
        bool valid = !extensionName.Contains("png")
                     && !extensionName.Contains("jpg")
                     && !extensionName.Contains("anim")
                     && !extensionName.Contains("controller");
        return valid;
    }
}

#region 自动化刷新YooAsset配置

public class YooAssetCollectorEditor
{
    private static AssetBundleCollectorPackage GetOrCreateCollectorPackage(string packageName)
    {
        AssetBundleCollectorPackage ret = null;
        var setting = AssetBundleCollectorSettingData.Setting;
        foreach (var package in setting.Packages)
        {
            if (package.PackageName != packageName)
                continue;
            ret = package;
            break;
        }
        if (ret == null)
        {
            ret = AssetBundleCollectorSettingData.CreatePackage(GlobalSetting.Ins.DefaultPackageName);
        }
        return ret;
    }

    private static AssetBundleCollectorGroup GetOrCreateCollectorGroup(string packageName, string groupName)
    {
        AssetBundleCollectorGroup ret = null;
        var setting = AssetBundleCollectorSettingData.Setting;
        foreach (var package in setting.Packages)
        {
            if (package.PackageName != packageName)
                continue;
            foreach (var group in package.Groups)
            {
                if (group.GroupName != groupName)
                    continue;
                ret = group;
                break;
            }
            if (ret != null)
                break;
        }
        if (ret == null)
        {
            var collectorPackage = GetOrCreateCollectorPackage(packageName);
            ret = AssetBundleCollectorSettingData.CreateGroup(collectorPackage, groupName);
        }
        return ret;
    }

    private static AssetBundleCollector GetOrCreateCollector(string packageName, string groupName, string collectPath)
    {
        AssetBundleCollector ret = null;
        var collectorGroup = GetOrCreateCollectorGroup(packageName, groupName);
        foreach (var groupCollector in collectorGroup.Collectors)
        {
            if (groupCollector.CollectPath != collectPath)
                continue;
            ret = groupCollector;
            break;
        }
        if (ret == null)
        {
            ret = new AssetBundleCollector();
            ret.CollectorType = ECollectorType.MainAssetCollector;
            ret.CollectPath = collectPath;
            ret.AddressRuleName = nameof(AddressByFileName);
            ret.PackRuleName = nameof(PackDirectory);
            ret.FilterRuleName = nameof(CollectAll);
            AssetBundleCollectorSettingData.CreateCollector(collectorGroup, ret);
        }
        return ret;
    }
}

#endregion 自动化刷新YooAsset配置