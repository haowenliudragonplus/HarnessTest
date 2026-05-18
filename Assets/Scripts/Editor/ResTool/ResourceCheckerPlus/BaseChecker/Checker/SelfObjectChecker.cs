//侧边栏显示列表
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace ResourceCheckerPlus
{
    public class SelfObjectChecker : ObjectChecker
    {
        public class SelfObjectDetail : ObjectDetail
        {
            public SelfObjectDetail(Object obj, SelfObjectChecker checker) : base(obj, checker)
            {

            }
        }

        public bool isLocked = false;

        public override void InitCheckItem()
        {
            checkerName = "SelfObj";
            refItem.show = false;
            pathItem.show = false;
            nameItem.clickOption = OnRefButtonClick;
        }

        public override void AddObjectDetail(Object rootObj)
        {
            ObjectDetail detail = null;
            foreach (var v in CheckList)
            {
                if (v.checkObject == rootObj)
                    detail = v;
            }
            if (detail == null)
            {
                detail = new SelfObjectDetail(rootObj, this);
            }
        }

        public void OnRefButtonClick(ObjectDetail detail)
        {
            SelectObjectDetail(detail);
            CheckModule module = (CheckModule)ResourceCheckerPlus.instance.currentActiveCheckMode;
            if (module != CheckModule.DirectRes && ResourceCheckerPlus.instance.checkerConfig.autoFilterOnSideBarButtonClick)
            {
                ObjectChecker checker = ResourceCheckerPlus.instance.CurrentCheckModule().CurrentActiveChecker();
                if (checker is ParticleChecker || checker is GameObjectChecker)
                    return;
                RefFilterItem filter = new RefFilterItem(checker);
                checker.filterItem.Clear(true);
                filter.checkObjList = SelectList.Select(x => x.checkObject).ToList();
                checker.filterItem.AddFilterNode(filter);
                checker.RefreshCheckResult();
            }
        }

        public override void ShowCheckResult()
        {
            if (CheckList.Count > 0 )
            {
                base.ShowCheckResult();
            }
        }

        public override void ShowCheckerSort()
        {
            viewListScrollPos.x = EditorGUILayout.BeginScrollView(new Vector2(viewListScrollPos.x, 0), GUILayout.Height(40)).x;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(previewItem.title, GUILayout.Width(previewItem.width)))
            {
            }
            if (GUILayout.Button(nameItem.title, GUILayout.Width(180)))
            {
                CheckDetailSort(nameItem, nameItem.sortSymbol);
                nameItem.sortSymbol = !nameItem.sortSymbol;
            }
            isLocked = EditorGUILayout.Toggle(lockButtonContent, isLocked, new GUIStyle("IN LockButton"), GUILayout.Width(20));
            GUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }

        private GUIContent lockButtonContent = new GUIContent("", "锁定侧边栏时，新加入内容时不会清空原有内容");

        public void AddObjectToSelfObjectChecker(List<Object> objects, bool clear)
        {
            if (!isLocked && clear)
            {
                Clear();
            }
            AddObjectDetailBatch(objects);
            RefreshCheckResult();
        }

        public void ClearSelfObjectList()
        {
            if (!isLocked)
            {
                Clear();
            }
            RefreshCheckResult();
        }

        //public override void ShowCheckerFilterTitle()
        //{
        //    GUILayout.BeginHorizontal();
        //    string filter = SearchField(currentFilterStr, GUILayout.Width(100));
        //    if (filter != currentFilterStr)
        //    {
        //        currentFilterStr = filter;
        //        RefreshCheckResult();
        //    }
        //    if (GUILayout.Button("其他功能", GUILayout.Width(60)))
        //    {
        //        OnContexMenu();
        //    }
        //    GUILayout.Label(checkSummary);
        //    GUILayout.EndHorizontal();
        //}
    }
}