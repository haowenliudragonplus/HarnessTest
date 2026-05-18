using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    public class CustomCheckModule : ResCheckModuleBase
    {
        protected bool checkPrefabDetailReference = false;
        protected GUIContent checkPrefabDetailRefContent = new GUIContent("检查资源被Prefab下子节点的引用", "开启该选项后，在检查Prefab引用的资源时，会将资源具体被哪些子物体引用了也统计出来（可以检查空脚本以及Unity内置资源，切换该属性需要重新检查）");

        public override void SetCheckerConfig(CheckModuleCfg cfg)
        {
            moduleType = CheckModule.CustomRes;
            checkModeName = new GUIContent("自定义", "接受输入Object数组的自定义检查方式，会检查输入列表中所有裸资源与引用到的资源");
            CreateChecker(cfg);
        }

        public override void ShowCommonSideBarContent()
        {
            checkPrefabDetailReference = GUILayout.Toggle(checkPrefabDetailReference, checkPrefabDetailRefContent);

            if (GUILayout.Button("检查资源", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth)))
            {
                CheckerInterface.CheckResource(Selection.objects);
                CheckerInterface.ApplyCheckFilter();
                CheckerInterface.ExportCheckResult();
            }
        }

        public override void CheckResource(Object[] objects)
        {
            Clear();
            for(int i = 0; i < objects.Length; i++)
            {
                EditorUtility.DisplayProgressBar("正在检查资源", "已完成：" + i + "/" + objects.Length, (float)i / objects.Length);
                Object root = objects[i];
                if (root == null)
                    continue;
                activeCheckerList.ForEach(x => x.MixCheckDirectAndRefRes(root, checkPrefabDetailReference));
            }
            EditorUtility.ClearProgressBar();
            Refresh();
        }
    }
}
