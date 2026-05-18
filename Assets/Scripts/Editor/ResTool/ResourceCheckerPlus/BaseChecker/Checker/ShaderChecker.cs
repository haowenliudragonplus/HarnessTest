using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    /// <summary>
    /// Shader类型检查
    /// </summary>
    public class ShaderChecker : ObjectChecker
    {
        public class ShaderDetail : ObjectDetail
        {
            public ShaderDetail(Object obj, ShaderChecker checker) : base(obj, checker)
            {
                Shader shader = obj as Shader;
                checkMap.Add(checker.shaderMaxLod, shader.maximumLOD);
                checkMap.Add(checker.shaderRenderQueue, shader.renderQueue);
            }
        }

        CheckItem shaderMaxLod;
        CheckItem shaderRenderQueue;

        public override void InitCheckItem()
        {
            checkerName = "Shader";
            checkerFilter = "t:Shader";
            shaderMaxLod = new CheckItem(this, "MaximumLOD", 100, CheckType.Int);
            shaderRenderQueue = new CheckItem(this, "RenderQueue", 100, CheckType.Int);
            nameItem.width = 350;
        }

        public override ObjectDetail AddObjectDetail(Object obj, Object refObj, Object detailRefObj)
        {
            Shader shader = obj as Shader;
            if (shader == null)
                return null;
            ObjectDetail detail = null;
            foreach (var v in CheckList)
            {
                if (v.checkObject == obj)
                    detail= v;
            }
            if (detail == null)
            {
                detail = new ShaderDetail(obj, this);
            }
            detail.AddObjectReference(refObj, detailRefObj);
            return detail;
        }

        public override void AddObjectDetailRef(GameObject rootObj)
        {
            Renderer[] renderers = rootObj.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer r in renderers)
            {
                foreach (Material mat in r.sharedMaterials)
                {
                    if (mat != null && mat.shader != null)
                    {
                        if (checkModule is SceneResCheckModule)
                            AddObjectDetail(mat.shader, r.gameObject, null);
                        else
                            AddObjectDetail(mat.shader, rootObj, r.gameObject);
                    }
                }
            }
        }
    }
}
