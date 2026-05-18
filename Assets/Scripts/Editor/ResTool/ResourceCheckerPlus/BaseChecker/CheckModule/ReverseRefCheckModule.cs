using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    /// <summary>
    /// 资源在全工程反向引用查找
    /// </summary>
    public class ReverseRefCheckModule : ResCheckModuleBase
    {
        public override void SetCheckerConfig(CheckModuleCfg cfg)
        {
            moduleType = CheckModule.ReverseRefRes;
            checkModeName = new GUIContent("反向", "全工程范围内检查资源被哪些Prefab，Material或Scene引用了");
            CreateChecker(cfg);
            ShowRefCheckItem(true, false, false);
        }

        public override void CheckResource(Object[] resources)
        {
            Clear();
            Object[] selection = resources == null ? GetAllObjectInSelection() : resources;
            foreach (var v in activeCheckerList)
            {
                v.ReverseRefResCheck(selection);
            }
            Refresh();
        }

        public void DoCheckRes(List<Object> objectList)
        {
            Clear();
            foreach (var v in activeCheckerList)
            {
                v.ReverseRefResCheck(objectList.ToArray());
            }
            Refresh();
        }

    }
}
