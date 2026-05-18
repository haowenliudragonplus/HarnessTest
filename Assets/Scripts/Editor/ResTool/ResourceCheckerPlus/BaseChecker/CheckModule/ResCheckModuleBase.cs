using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

namespace ResourceCheckerPlus
{
    [System.Serializable]
    public class CheckModuleCfg
    {
        public string checkModuleName;
        public CheckerCfg[] checkerCfgs;
    }

    /// <summary>
    /// 检查模块基类
    /// </summary>
    public class ResCheckModuleBase
    {
        public GUIContent checkModeName = null;
        //用于检查某个文件夹下所有的功能
        public Object objInputSlot = null;
        public Queue<Object> checkRecord = new Queue<Object>();
        public static int checkRecordCount = 10;
        public List<ObjectChecker> checkerList = new List<ObjectChecker>();
        public List<ObjectChecker> activeCheckerList = null;
        public string currentModuleTip = "";
        public CheckModule moduleType = CheckModule.Unknown;
        public SelfObjectChecker sideBarObjList = new SelfObjectChecker();
        public int[] activeCheckerConfig = null;
        public Rect SideBarRect;
        public Rect MainRect;

        public int currentActiveChecker = 0;
        public string[] checkerListNames = null;

        public void CreateChecker(CheckModuleCfg cfg)
        {
            foreach (var v in cfg.checkerCfgs)
            {
                ObjectChecker.CreateChecker(this, v);
            }
        }

        public void ShowRefCheckItem(bool refObj, bool detailRef, bool activeInRef)
        {
            foreach( var v in checkerList)
            {
                v.refItem.show = refObj;
                v.totalRefItem.show = detailRef;
                v.activeItem.show = activeInRef;
            }
        }

        public ObjectChecker CurrentActiveChecker()
        {
            if (activeCheckerList != null && currentActiveChecker < activeCheckerList.Count)
            {
                return activeCheckerList[currentActiveChecker];
            }
            return null;
        }

        public void ShowCurrentCheckDetail()
        {
            var checker = CurrentActiveChecker();
            if (checker != null)
            {
                checker.ShowCheckerTitle();
            }
            else
            {
                GUILayout.Label("当前无可用检查类别，请从右侧下拉列表中选择需要检查的资源类型");
            }
            ShowCheckerSelecter();
            if (checker != null)
            {
                checker.ShowCheckResult();
                if (Event.current.button == 1 && MainRect.Contains(Event.current.mousePosition))
                {
                    checker.OnContexMenu();
                }
            }
            CurrentActiveChecker();
        }

        public void ShowCheckerSelecter()
        {
            if (checkerListNames == null || activeCheckerList == null)
                return;
            GUILayout.BeginHorizontal();
            currentActiveChecker = GUILayout.Toolbar(currentActiveChecker, checkerListNames, GUILayout.Width(MainRect.width - 80));
            if (GUILayout.Button("检查类型", GUILayout.Width(80)))
            {
                GenericMenu menu = new GenericMenu();
                foreach (var c in checkerList)
                {
                    //这个地方不能用delegate，Unity5.5版本没有问题，但在Unity5.3版本测试时下拉菜单传入的checker一直是for循环的最后一个
                    menu.AddItem(new GUIContent(c.checkerName), c.checkerEnabled, new GenericMenu.MenuFunction2(OnCheckerItemSelected), c);
                }
                menu.AddSeparator(string.Empty);
                menu.AddItem(new GUIContent("全部开启"), false, new GenericMenu.MenuFunction2(SetAllCheckerEnable), true);
                menu.AddItem(new GUIContent("全部关闭"), false, new GenericMenu.MenuFunction2(SetAllCheckerEnable), false);
                menu.ShowAsContext();
            }

            GUILayout.EndHorizontal();
        }

        private void OnCheckerItemSelected(object obj)
        {
            ObjectChecker checker = obj as ObjectChecker;
            if (checker == null)
                return;
            checker.checkerEnabled = !checker.checkerEnabled;
            RefreshCheckerConfig(checker);
        }

        private void SetAllCheckerEnable(object enable)
        {
            bool enabled = (bool)enable;
            foreach(var v in checkerList)
            {
                v.checkerEnabled = enabled;
                RefreshCheckerConfig(v);
            }
        }

        public virtual void ShowCurrentSideBar()
        {
            ShowCommonSideBarContent();
            if (GUILayout.Button("导出全部激活列表内容"))
            {
                ExportAllActiveCheckerResult();
            }
            sideBarObjList.ShowCheckResult();
        }

        public virtual void ShowCommonSideBarContent()
        {
            if (ResourceCheckerPlus.instance.checkerConfig.inputType == CheckInputMode.DragMode)
            {
                ShowObjectDragSlot();
            }
            if (GUILayout.Button("检查资源", GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth)))
            {
                CheckResource(null);
            }
        }

        public void InitCheckModule(CheckModuleCfg cfg)
        {
            checkerList.Clear();
            sideBarObjList.Clear();
            //刷新Active的Checker一次
            SetCheckerConfig(cfg);
            RefreshCheckerConfig();
        }

        public virtual void SetCheckerConfig(CheckModuleCfg cfg) { }

        public void RefreshCheckerConfig(ObjectChecker checker = null)
        {
            if (checkerList != null)
            {
                activeCheckerList = checkerList.Where(x => x.checkerEnabled).ToList();
                checkerListNames = activeCheckerList.Select(x => x.checkerName).ToArray();
                if (checker != null && checker.checkerEnabled)
                {
                    currentActiveChecker = activeCheckerList.IndexOf(checker);
                    checker.Clear();
                }
                else
                {
                    currentActiveChecker = 0;
                }
            }
        }

        public void Clear()
        {
            //全清
            checkerList.ForEach(x => x.Clear());
            ClearSideBarList();
        }

        public void Refresh()
        {
            checkerList.ForEach(x => x.RefreshCheckResult());
        }

        public virtual void CheckResource(Object[] resources) { }

        public Object[] GetAllObjectInSelection()
        {
            if (ResourceCheckerPlus.instance.checkerConfig.inputType == CheckInputMode.SelectMode)
                return Selection.objects;
            else
                return new Object[] { objInputSlot };
        }

        protected void ShowObjectDragSlot()
        {
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            objInputSlot = EditorGUILayout.ObjectField(objInputSlot, typeof(UnityEngine.Object), true, GUILayout.Width(ResourceCheckerPlus.instance.checkerConfig.sideBarWidth - 60));
            if (EditorGUI.EndChangeCheck())
            {
                RecordCheckPath(objInputSlot);
            }
            ShowCheckRecord();
            GUILayout.EndHorizontal();
        }

        protected void ShowCheckRecord()
        {
            if (checkRecord.Count == 0)
                return;
            if (GUILayout.Button("最近查询", GUILayout.Width(60)))
            {
                GenericMenu genericMenu = new GenericMenu();
                foreach (var v in checkRecord)
                {
                    if (v == null)
                        continue;
                    string path = AssetDatabase.GetAssetPath(v).Replace('/', '.');
                    genericMenu.AddItem(new GUIContent(path), false, new GenericMenu.MenuFunction(delegate { objInputSlot = v; }));
                }
                genericMenu.ShowAsContext();
            }
        }

        protected void RecordCheckPath(Object obj)
        {
            if (obj != null)
            {
                checkRecord.Enqueue(obj);
            }
            if (checkRecord.Count > checkRecordCount)
            {
                checkRecord.Dequeue();
            }
        }

        public void SelectAll()
        {
            sideBarObjList.SelectAll();
        }

        public void AddObjectToSideBarList(List<Object> objects, bool clear = true)
        {
            sideBarObjList.AddObjectToSelfObjectChecker(objects, clear);
        }

        public void ClearSideBarList()
        {
            sideBarObjList.ClearSelfObjectList();
        }

        public void OnSideBarMouseMenu()
        {
            sideBarObjList.OnContexMenu();
        }

        public void ExportAllActiveCheckerResult()
        {
            string path = ResourceCheckerHelper.GenericExportFolderName();
            Directory.CreateDirectory(path);
            activeCheckerList.ForEach(x => x.ExportCheckResult(path));
            AssetDatabase.Refresh();
        }

        public void LoadPredefineCheckFilterCfg()
        {
            activeCheckerList.ForEach(x => x.LoadCheckFilter());
        }

        public static void CreateCheckModule(ResourceCheckerPlus root, CheckModuleCfg cfg)
        {
            System.Type type = System.Type.GetType("ResourceCheckerPlus." + cfg.checkModuleName);
            if (type == null)
                return;
            ResCheckModuleBase checkModule = System.Activator.CreateInstance(type) as ResCheckModuleBase;
            checkModule.InitCheckModule(cfg);
            root.resCheckModeList.Add(checkModule);
        }
    }
}
