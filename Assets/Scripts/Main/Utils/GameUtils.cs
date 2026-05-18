using DragonPlus.Core;
using DragonPlus.Native.Bridge;
using UnityEngine;

namespace Main
{
    public static class GameUtils
    {
        /// <summary>
        /// 是否在Unity编辑器环境下
        /// </summary>
        public static bool IsInEditorEnv()
        {
            return Application.isEditor;
        }

        /// <summary>
        /// 是否为开发环境
        /// </summary>
        /// 编辑器下始终为true
        public static bool IsDevelopmentEnv()
        {
            bool isDevelopmentEnv = Debug.isDebugBuild;
            return isDevelopmentEnv;
        }

        /// <summary>
        /// 是否在运行中
        /// </summary>
        public static bool IsInApplicationPlaying()
        {
            return Application.isPlaying;
        }

        public static bool HasNetwork()
        {
            bool hasNetwork = Application.internetReachability != NetworkReachability.NotReachable;
            return hasNetwork;
        }
        
        /// <summary>
        /// 打开URL
        /// </summary>
        public static void OpenURL(string url)
        {
#if UNITY_IOS&&!UNITY_EDITOR
            SDK<INative>.Instance.OpenAppStoreRate(url);
#else
            Application.OpenURL(url);
#endif
        }
        
        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }   
}
