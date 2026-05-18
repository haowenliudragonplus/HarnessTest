using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#if UNITY_ANDROID
using UnityEditor.Android;
#endif
using System.Text.RegularExpressions;
using UnityEditor;
using System.Xml.Linq;

public class PostBuildProcess_Android_GenerateGradleAndroidProject : IPostprocessBuildWithReport
#if UNITY_ANDROID
    , IPostGenerateGradleAndroidProject
#endif
{
    public int callbackOrder => int.MaxValue-1;

    private static readonly Regex TokenDistributionUrl = new Regex(".*distributionUrl.*");
    private static readonly Regex TokenEnableUnCom = new Regex(".*enableUncompressedNativeLibs.*");

    private static bool ReplaceStringInFile(string path, Regex regexToMatch, string replacement)
    {
        if (!File.Exists(path)) return false;

        var lines = File.ReadAllLines(path);
        for (var i = 0; i < lines.Length; i++)
        {
            if (regexToMatch.IsMatch(lines[i]))
            {
                lines[i] = replacement;
                File.WriteAllLines(path, lines);
                return true;
            }
        }

        return false;
    }

    public void OnPostprocessBuild(BuildReport report)
    {
#if UNITY_ANDROID
        var gradleWrapperPropertiesPath = Path.Combine(report.summary.outputPath, "gradle/wrapper/gradle-wrapper.properties");
        Debug.Log($"GlobalPostBuildHandle.OnPostprocessBuild--{gradleWrapperPropertiesPath}");
        if (File.Exists(gradleWrapperPropertiesPath))
        {
            var newDistributionUrl = "distributionUrl=https\\://services.gradle.org/distributions/gradle-8.7-all.zip";
            Debug.Log($"GlobalPostBuildHandle.OnPostprocessBuild--gradleWrapperPropertiesPath is Exist");
            if (ReplaceStringInFile(gradleWrapperPropertiesPath, TokenDistributionUrl, newDistributionUrl))
            {
                Debug.Log("GlobalPostBuildHandle.OnPostprocessBuild,Distribution url set to " + newDistributionUrl);
            }
            else
            {
                Debug.LogError("GlobalPostBuildHandle.OnPostprocessBuild,Failed to set distribution URL");
            }

        }
#endif
    }

#if UNITY_ANDROID
    public void OnPostGenerateGradleAndroidProject(string path)
    {
        var gradleWrapperPropertiesPath = Path.Combine(path, "../gradle/wrapper/gradle-wrapper.properties");
        Debug.Log($"GlobalPostBuildHandle.OnPostGenerateGradleAndroidProject--{gradleWrapperPropertiesPath}");
        if (File.Exists(gradleWrapperPropertiesPath))
        {
            var newDistributionUrl = "distributionUrl=https\\://services.gradle.org/distributions/gradle-8.7-all.zip";
            Debug.Log($"GlobalPostBuildHandle.OnPostGenerateGradleAndroidProject--gradleWrapperPropertiesPath is Exist");
            if (ReplaceStringInFile(gradleWrapperPropertiesPath, TokenDistributionUrl, newDistributionUrl))
            {
                Debug.Log("GlobalPostBuildHandle.OnPostGenerateGradleAndroidProject,Distribution url set to " + newDistributionUrl);
            }
            else
            {
                Debug.LogError("GlobalPostBuildHandle.OnPostGenerateGradleAndroidProject,Failed to set distribution URL");
            }
        }

        var gradlePropertiesPath = Path.Combine(path, "../gradle.properties");
        Debug.Log($"GlobalPostBuildHandle.OnPostprocessBuild--{gradlePropertiesPath}");
        if (File.Exists(gradlePropertiesPath))
        {
            Debug.Log($"GlobalPostBuildHandle.OnPostprocessBuild--gradlePropertiesPath is Exist");
            if (ReplaceStringInFile(gradlePropertiesPath, TokenEnableUnCom, ""))
            {
                Debug.Log("GlobalPostBuildHandle.OnPostprocessBuild, enableUncompressedNativeLibs set to empty ");
            }
            else
            {
                Debug.LogError("GlobalPostBuildHandle.OnPostprocessBuild,Failed to set enableUncompressedNativeLibs");
            }
            
            List<string> lines = new List<string>(File.ReadAllLines(gradlePropertiesPath));
            bool isModified = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Trim().StartsWith("android.useFullClasspathForDexingTransform"))
                {
                    lines[i] = "android.useFullClasspathForDexingTransform=true"; // 强制设为 true
                    isModified = true;
                }
                else if (lines[i].Trim().StartsWith("android.enableDexingArtifactTransform"))
                {
                    lines.RemoveAt(i);
                    Debug.Log("✅ 已成功删除 android.enableDexingArtifactTransform");
                }
            }
            
            if (!isModified)
            {
                lines.Add("android.useFullClasspathForDexingTransform=true");
                isModified = true;
            }
            
            if (isModified)
            {
                File.WriteAllLines(gradlePropertiesPath, lines);
                AssetDatabase.Refresh(); // 让 Unity 识别文件变化
                Debug.Log("✅ 已成功启用 android.useFullClasspathForDexingTransform");
            }
            else
            {
                Debug.Log("ℹ️ 该属性已存在，无需修改");
            }
            for (int i = 0; i < lines.Count; i++)
            {
                Debug.Log($"✅gradle.properties[{i}]  :{lines[i]}");
            }
        }

        var theCurPath = path.Replace("\\", "/");
        var unityLibraryPath = $"{theCurPath}/../";
        if (Directory.Exists(unityLibraryPath))
        {//处理 AndroidManifest 的 package 属性
            var theFiles = Directory.GetFiles(unityLibraryPath, "AndroidManifest.xml", SearchOption.AllDirectories);
            XDocument manifest = new XDocument();
            for (int i = 0; i < theFiles.Length; i++)
            {
                var manifestPath = theFiles[i];

                try
                {
                    manifest = XDocument.Load(manifestPath);
                }
                catch (IOException e)
                {
                    Debug.LogWarning($"AndroidManifest.xml is missing. Try re-importing the plugin. error is {e}");
                    continue;
                }

                XElement elemManifest = manifest.Element("manifest");
                if (elemManifest != null)
                {
                    var attPackage = elemManifest.Attribute("package");
                    if (attPackage != null)
                    {
                        attPackage.Remove();
                        elemManifest.Save(manifestPath);
                    }
                }
            }
        }
    }
#endif
}
