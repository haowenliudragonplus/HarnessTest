using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceCheckerPlus
{
    /// <summary>
    /// 初始化，ScriptableObject与Mono脚本一样，要求类名与文件名一致，否则重启Unity后会丢失脚本
    /// </summary>
    public class CheckerInitConfig : ScriptableObject
    {
        public CheckModuleCfg[] moduleConfg = null;

        public CheckerInitConfig()
        {
            CheckModuleCfg sceneCheckerCfg = new CheckModuleCfg();
            sceneCheckerCfg.checkModuleName = "SceneResCheckModule";
            sceneCheckerCfg.checkerCfgs = new CheckerCfg[]
            {
                new CheckerCfg("TextureChecker", true),
                new CheckerCfg("MeshChecker", true),
                new CheckerCfg("MaterialChecker", true),
                new CheckerCfg("ComponentChecker", true),
                new CheckerCfg("ShaderChecker"),
                new CheckerCfg("AudioChecker"),
                new CheckerCfg("ParticleChecker"),
                new CheckerCfg("AnimClipChecker"),
                new CheckerCfg("GameObjectChecker"),
                new CheckerCfg("PrefabChecker"),
                new CheckerCfg("RefObjectChecker"),
                new CheckerCfg("NGUIChecker")
            };

            CheckModuleCfg referenceCheckerCfg = new CheckModuleCfg();
            referenceCheckerCfg.checkModuleName = "ReferenceResCheckModule";
            referenceCheckerCfg.checkerCfgs = new CheckerCfg[]
            {
                new CheckerCfg("TextureChecker", true),
                new CheckerCfg("MeshChecker", true),
                new CheckerCfg("MaterialChecker", true),
                new CheckerCfg("ComponentChecker", true),
                new CheckerCfg("ShaderChecker"),
                new CheckerCfg("AudioChecker"),
                new CheckerCfg("ParticleChecker"),
                new CheckerCfg("AnimClipChecker"),
                new CheckerCfg("GameObjectChecker"),
                new CheckerCfg("PrefabChecker"),
                new CheckerCfg("RefObjectChecker"),
                new CheckerCfg("NGUIChecker")
            };

            CheckModuleCfg directCheckCfg = new CheckModuleCfg();
            directCheckCfg.checkModuleName = "DirectResCheckModule";
            directCheckCfg.checkerCfgs = new CheckerCfg[]
            {
                new CheckerCfg("TextureChecker", true),
                new CheckerCfg("MeshChecker", true),
                new CheckerCfg("MaterialChecker", true),
                new CheckerCfg("ShaderChecker"),
                new CheckerCfg("AudioChecker", true),
                new CheckerCfg("ParticleChecker"),
                new CheckerCfg("AnimClipChecker"),
                new CheckerCfg("GameObjectChecker"),
                new CheckerCfg("PrefabChecker"),
                new CheckerCfg("SceneChecker")
            };

            CheckModuleCfg reverseCheckCfg = new CheckModuleCfg();
            reverseCheckCfg.checkModuleName = "ReverseRefCheckModule";
            reverseCheckCfg.checkerCfgs = new CheckerCfg[]
            {
                new CheckerCfg("PrefabChecker", true),
                new CheckerCfg("SceneChecker", true),
                new CheckerCfg("MaterialChecker", true),
            };

            CheckModuleCfg customCheckCfg = new CheckModuleCfg();
			customCheckCfg.checkModuleName = "ImportedListCheckModule";//"CustomCheckModule";

			customCheckCfg.checkerCfgs = new CheckerCfg[]
            {
                new CheckerCfg("TextureChecker", true),
                new CheckerCfg("MeshChecker", true),
                new CheckerCfg("MaterialChecker", true),
                new CheckerCfg("ComponentChecker", true),
                new CheckerCfg("ShaderChecker"),
                new CheckerCfg("AudioChecker"),
                new CheckerCfg("ParticleChecker"),
                new CheckerCfg("AnimClipChecker"),
                new CheckerCfg("GameObjectChecker"),
                new CheckerCfg("PrefabChecker"),
                new CheckerCfg("RefObjectChecker"),
                new CheckerCfg("NGUIChecker")
           };
            moduleConfg = new CheckModuleCfg[] { sceneCheckerCfg, referenceCheckerCfg, directCheckCfg, reverseCheckCfg, customCheckCfg };
        }
    }
}
