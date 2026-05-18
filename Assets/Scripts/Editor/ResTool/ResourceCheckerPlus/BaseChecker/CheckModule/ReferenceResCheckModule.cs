using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    /// <summary>
    /// 引用资源检查功能
    /// </summary>
    public class ReferenceResCheckModule : ResCheckModuleBase
    {
        private GUIContent checkPrefabDetailRefContent = new GUIContent("检查资源被Prefab下子节点的引用", "开启该选项后，在检查Prefab引用的资源时，会将资源具体被哪些子物体引用了也统计出来（切换该属性需要重新检查）");
        private bool showRefCheckInputConfig = true;
        public bool refCheckPrefab = true;
        public bool refCheckScene = false;
        public bool refCheckMaterial = false;
        public bool checkPrefabDetailRef = false;

        public override void SetCheckerConfig(CheckModuleCfg cfg)
        {
            moduleType = CheckModule.RefRes;
            checkModeName = new GUIContent("引用", "检查目录下所有Prefab,Material或者Scene引用的资源");
            CreateChecker(cfg);
            ShowRefCheckItem(true, checkPrefabDetailRef, checkPrefabDetailRef);
        }

        public override void ShowCommonSideBarContent()
        {
            if (ResourceCheckerPlus.instance.checkerConfig.inputType == CheckInputMode.DragMode)
            {
                ShowObjectDragSlot();
            }
            if (GUILayout.Button("检查资源", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth)))
            {
                CheckResource(null);
            }
            if (refCheckPrefab)
            {
                EditorGUI.BeginChangeCheck();
                checkPrefabDetailRef = GUILayout.Toggle(checkPrefabDetailRef, checkPrefabDetailRefContent, GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth));
                if (EditorGUI.EndChangeCheck())
                {
                    ShowRefCheckItem(true, checkPrefabDetailRef, checkPrefabDetailRef);
                }
            }
            showRefCheckInputConfig = EditorGUILayout.Foldout(showRefCheckInputConfig, new GUIContent("     引用资源检查范围    "));
            if (showRefCheckInputConfig)
            {
                refCheckPrefab = GUILayout.Toggle(refCheckPrefab, "Prefab", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth));
                refCheckScene = GUILayout.Toggle(refCheckScene, "Scene", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth));
                refCheckMaterial = GUILayout.Toggle(refCheckMaterial, "Material", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth));
            }
        }

        public override void CheckResource(Object[] resources)
        {
            Clear();
            Object[] selection = GetAllObjectInSelection();
            string filter = refCheckPrefab ? "t:Prefab" : "";
            filter += refCheckScene ? " t:Scene" : "";
            filter += refCheckMaterial ? " t:Material" : "";
            activeCheckerList.ForEach(x => x.ReferenceResCheck(selection, filter, checkPrefabDetailRef));
            Refresh();
        }
    }
}
