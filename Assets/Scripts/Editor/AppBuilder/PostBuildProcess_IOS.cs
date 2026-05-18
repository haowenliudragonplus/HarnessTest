#if UNITY_IOS && !DEVELOPMENT_BUILD
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Framework;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public static class PostBuildProcess_IOS
{
    private const string KEY_SK_ADNETWORK_ITEMS = "SKAdNetworkItems";

    [PostProcessBuild(int.MaxValue)]
    public static void OnPostprocessBuild(BuildTarget target, string buildPath)
    {
        if (EditorUserBuildSettings.development)
            return;

        // 处理代码量太大打包失败
        string targetFilePath = Path.Combine(
            buildPath,
            "Il2CppOutputProject/IL2CPP/libil2cpp/os/c-api/il2cpp-config-platforms.h"
        );
        string[] lines = File.ReadAllLines(targetFilePath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("#define IL2CPP_LARGE_EXECUTABLE_ARM_WORKAROUND 0"))
            {
                lines[i] = "#define IL2CPP_LARGE_EXECUTABLE_ARM_WORKAROUND 1";
            }
        }
        File.WriteAllLines(targetFilePath, lines.ToArray());

        //
        string projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string targetGUID = "";
#if UNITY_2019_3_OR_NEWER
        targetGUID = proj.GetUnityMainTargetGuid();
#else
            targetGUID = proj.TargetGuidByName("Unity-iPhone");
#endif
        string unityFrameworkTargetGUID = "";
#if UNITY_2019_3_OR_NEWER
        unityFrameworkTargetGUID = proj.GetUnityFrameworkTargetGuid();
#endif

        string projectGUID = proj.ProjectGuid();
        proj.SetBuildProperty(projectGUID, "ENABLE_BITCODE", "NO");
        proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-lxml2");

        // proj.AddFrameworkToProject(targetGUID, "UserNotificationsUI.framework", true);
        proj.AddFrameworkToProject(targetGUID, "iAd.framework", true);
        proj.AddFrameworkToProject(targetGUID, "AdSupport.framework", true);
        proj.AddFrameworkToProject(targetGUID, "AppTrackingTransparency.framework", true);

        // adds the AuthenticationServices.framework as an Optional framework, preventing crashes in
        // iOS versions previous to 13.0
        proj.AddFrameworkToProject(targetGUID, "AuthenticationServices.framework", true);
        if (!string.IsNullOrEmpty(unityFrameworkTargetGUID))
        {
            proj.AddFrameworkToProject(unityFrameworkTargetGUID, "AuthenticationServices.framework", true);
            proj.SetBuildProperty(unityFrameworkTargetGUID, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO"); //ERROR ITMS-90206. UnityFramework contains disallowed FrameWorks
            proj.SetBuildProperty(unityFrameworkTargetGUID, "GCC_OPTIMIZATION_LEVEL", "s"); //GCC -Os
        }

        proj.SetBuildProperty(targetGUID, "USYM_UPLOAD_AUTH_TOKEN", "FakeToken");
        proj.SetBuildProperty(targetGUID, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
        proj.SetBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
        proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");

        //infoPlist.strings
        string localizationPath = $"{Application.dataPath}/AdLocalization~";
        if (Directory.Exists(localizationPath))
        {
            string[] localizationDirectories = Directory.GetDirectories(localizationPath);
            foreach (var p in localizationDirectories)
            {
                string tempGuid = proj.AddFolderReference(p, p.Substring(p.LastIndexOf("/") + 1));
                proj.AddFileToBuild(targetGUID, tempGuid);
            }
        }

        File.WriteAllText(projPath, proj.WriteToString());

        // Read plist
        var plistPath = Path.Combine(buildPath, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        // if (plist.root.values.ContainsKey(KEY_SK_ADNETWORK_ITEMS))
        // {
        //     plist.root.values.Remove(KEY_SK_ADNETWORK_ITEMS);
        // }

        // Update value
        PlistElementDict rootDict = plist.root;
        if (rootDict.values.ContainsKey("LSApplicationQueriesSchemes"))
        {
            rootDict.values["LSApplicationQueriesSchemes"].AsArray().AddString("fb-messenger-share-api");
        }

        // remove exit on suspend if it exists.
        string exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
        if (rootDict.values.ContainsKey(exitsOnSuspendKey))
        {
            rootDict.values.Remove(exitsOnSuspendKey);
        }

        // remove NSAllowsArbitraryLoadsInWebContent if it exists.
        string appTransportSecurityKey = "NSAppTransportSecurity";
        if (rootDict.values.ContainsKey(appTransportSecurityKey))
        {
            if (rootDict.values[appTransportSecurityKey].AsDict().values
                .ContainsKey("NSAllowsArbitraryLoadsInWebContent"))
            {
                rootDict.values[appTransportSecurityKey].AsDict().values
                    .Remove("NSAllowsArbitraryLoadsInWebContent");
            }
        }

        //if (!rootDict.values.ContainsKey("NSUserTrackingUsageDescription"))
        {
            rootDict.SetString("NSUserTrackingUsageDescription",
                "If you choose to enable tracking, we can show ads that better match your interests, which helps us improve the game experience. If you choose to decline, we won’t be able to provide personalized ads. Whatever you decide, we hope you enjoy the game!");
        }

        // Write plist
        File.WriteAllText(plistPath, plist.WriteToString());

        string entitlePath = "Unity-iPhone/" + Application.productName + ".entitlements";
#if UNITY_2019_3_OR_NEWER
        ProjectCapabilityManager projectCapabilityManager =
            new ProjectCapabilityManager(projPath, entitlePath, "Unity-iPhone", targetGUID);
#else
        ProjectCapabilityManager projectCapabilityManager =
 new ProjectCapabilityManager(projPath, entitlePath, "Unity-iPhone");
#endif

        try
        {
            projectCapabilityManager.AddInAppPurchase();
        }
        catch (Exception e)
        {
        }

        projectCapabilityManager.WriteToFile();
    }
}

#endif