using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class FacebookPodFixPostprocess
{
    [PostProcessBuild(45)]
    public static void OnPostprocessBuild(BuildTarget target, string buildPath)
    {
        if (target == BuildTarget.iOS)
        {
            var podfilePath = Path.Combine(buildPath, "Podfile");
            if (!File.Exists(podfilePath)) return;

            var lines         = File.ReadAllLines(podfilePath);
            var modifiedLines = new List<string>();
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("pod 'FBSDKCoreKit'"))
                {
                    modifiedLines.Add("  pod 'FBSDKCoreKit', '18.0.1'");
                }
                else if (trimmedLine.StartsWith("pod 'FBSDKCoreKit_Basics'"))
                {
                    modifiedLines.Add("  pod 'FBSDKCoreKit_Basics', '18.0.1'");
                }
                else if (trimmedLine.StartsWith("pod 'FBSDKGamingServicesKit'"))
                {
                    modifiedLines.Add("  pod 'FBSDKGamingServicesKit', '18.0.1'");
                }
                else if (trimmedLine.StartsWith("pod 'FBSDKLoginKit'"))
                {
                    modifiedLines.Add("  pod 'FBSDKLoginKit', '18.0.1'");
                }
                else if (trimmedLine.StartsWith("pod 'FBSDKShareKit'"))
                {
                    modifiedLines.Add("  pod 'FBSDKShareKit', '18.0.1'");
                }
                else
                {
                    modifiedLines.Add(line);
                }
            }

            File.WriteAllLines(podfilePath, modifiedLines);
        }
    }
}