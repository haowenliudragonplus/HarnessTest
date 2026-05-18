// Resource Checker原始版本，貌似...除了名字（额，的前半部分）...其他都不一样了....
// https://github.com/handcircus/Unity-Resource-Checker
// Resource Checker Plus
// author: 引擎部 zhangjian_dev，有改进建议欢迎rtx联系

using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace ResourceCheckerPlus
{
    public enum CheckModule
    {
        SceneRes = 0, //当前场景资源检查
        RefRes = 1, //（prefab，scene，material）引用资源检查
        DirectRes = 2, //Textrure，Mesh等裸资源直接检查
        ReverseRefRes = 3, //全工程范围内对资源的引用进行查找
        CustomRes = 4, //自定义查找模式
        Unknown,
    }

    public enum CheckInputMode
    {
        DragMode,
        SelectMode,
    }

    public enum BatchOptionSelection
    {
        AllInFilterList,
        CurrentSelect,
    }

    public class ResourceCheckerPlus : EditorWindow
    {
        public static Color defaultTextColor;
        public static Color defaultBackgroundColor;
        public static int spriteBarWidth = 4;
        public CheckerCommonConfig checkerConfig = null;
        public CheckerInitConfig checkerInitConfig = null;
        public List<ResCheckModuleBase> resCheckModeList = new List<ResCheckModuleBase>();
        public int currentActiveCheckMode = 0;
        public GUIContent[] checkModeListNames = null;
        public static ResourceCheckerPlus instance = null;
        //配置文件路径
        public static string configRootPath = "Assets/Scripts/Editor/ResTool/ResourceCheckerPlus/CheckerConfig";
        public static string commonConfigName = "/CommonConfig.asset";
        public static string initConfigName = "/InitConfig.asset";
        public static string predefineFilterCfgPath = "/FilterCfg";
        public static string defaultExportResultPath = "Assets";

        [MenuItem("游戏工具/资源工具/Resource Checker Plus", priority = 2999)]
        public static void Init()
        {
            ResourceCheckerPlus window = (ResourceCheckerPlus)EditorWindow.GetWindow(typeof(ResourceCheckerPlus));
            window.minSize = new Vector2(800, 600);
        }

        void OnEnable()
        {
            instance = this;
            defaultTextColor = GUI.color;
            defaultBackgroundColor = GUI.backgroundColor;
            InitConfig();
            InitCheckerModule();
        }

        void OnDestroy()
        {
            instance = null;
            EditorUtility.SetDirty(checkerInitConfig);
            EditorUtility.SetDirty(checkerConfig);
            Resources.UnloadUnusedAssets();
        }

        void OnGUI()
        {
            Rect rect = this.position;
            Rect rectLeft = new Rect(0, 0, checkerConfig.sideBarWidth, rect.height);
            Rect rectMid = new Rect(checkerConfig.sideBarWidth, 0, spriteBarWidth, rect.height);
            Rect rectRight = new Rect(checkerConfig.sideBarWidth + spriteBarWidth, 0, rect.width - checkerConfig.sideBarWidth - spriteBarWidth, rect.height);

            SetRect(rectLeft, rectRight);

            //侧边栏
            GUILayout.BeginArea(rectLeft);
            ShowSideBar();
            GUILayout.EndArea();

            //分割线
            GUILayout.BeginArea(rectMid);
            GUI.backgroundColor = Color.black;
            GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(spriteBarWidth));
            GUI.backgroundColor = defaultBackgroundColor;
            GUILayout.EndArea();

            //主界面
            GUILayout.BeginArea(rectRight);
            ShowCheckDetail();
            GUILayout.EndArea();
        }

        void InitConfig()
        {
            if (checkerConfig == null)
            {
                string path = configRootPath + commonConfigName;
                checkerConfig = AssetDatabase.LoadAssetAtPath<CheckerCommonConfig>(path);
                if (checkerConfig == null)
                {
                    checkerConfig = ScriptableObject.CreateInstance<CheckerCommonConfig>();
                    AssetDatabase.CreateAsset(checkerConfig, path);
                }
            }

            if (checkerInitConfig == null)
            {
                string path = configRootPath + initConfigName;
                checkerInitConfig = AssetDatabase.LoadAssetAtPath<CheckerInitConfig>(path);
                if (checkerInitConfig == null)
                {
                    checkerInitConfig = ScriptableObject.CreateInstance<CheckerInitConfig>();
                    AssetDatabase.CreateAsset(checkerInitConfig, path);
                }
            }
        }

        public ResCheckModuleBase CurrentCheckModule()
        {
            if (currentActiveCheckMode < resCheckModeList.Count)
            {
                return resCheckModeList[currentActiveCheckMode];
            }
            return null;
        }

        public void SetRect(Rect sideBar, Rect mainWindow)
        {
            ResCheckModuleBase module = CurrentCheckModule();
            if (module != null)
            {
                module.SideBarRect = sideBar;
                module.MainRect = mainWindow;
            }
        }

        private void ShowSideBar()
        {
            if (checkModeListNames == null)
                return;
            currentActiveCheckMode = GUILayout.Toolbar(currentActiveCheckMode, checkModeListNames);
            GUILayout.BeginVertical();
            ResCheckModuleBase module = CurrentCheckModule();
            if (module != null)
            {
                module.ShowCurrentSideBar();
            }
            GUILayout.EndVertical();
        }

        private void ShowCheckDetail()
        {
            if (checkModeListNames == null)
                return;
            if (currentActiveCheckMode < resCheckModeList.Count)
            {
                resCheckModeList[currentActiveCheckMode].ShowCurrentCheckDetail();
            }
        }

        public void InitCheckerModule()
        {
            //初始化检查模式
            resCheckModeList.Clear();
            foreach (var v in checkerInitConfig.moduleConfg)
            {
                ResCheckModuleBase.CreateCheckModule(this, v);
            }
            checkModeListNames = resCheckModeList.Select(x => x.checkModeName).ToArray();
        }

        public void ClearCheckModule()
        {
            resCheckModeList.ForEach(x => x.Clear());
        }

        public void SetCurrentActiveCheckModule(CheckModule checkmodule)
        {
            currentActiveCheckMode = (int)checkmodule;
        }
    }
}