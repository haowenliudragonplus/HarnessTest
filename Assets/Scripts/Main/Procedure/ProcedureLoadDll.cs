using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Framework;
using HybridCLR;
using UnityEngine;
using YooAsset;

public class ProcedureLoadDll : Procedure
{
    public ProcedureLoadDll(ProcedureContext context) : base(context)
    {
    }

    public override async UniTask ExecuteProcedure()
    {
        base.ExecuteProcedure();

        if (ProcedureYooAssetInit.PlayMode == EPlayMode.EditorSimulateMode)
            return;

        foreach (string aotDllName in GlobalSetting.Ins.PatchedAOTAssemblyList)
        {
            var textAsset = Resources.Load<TextAsset>("PatchedAOT/" + aotDllName);
            if (textAsset != null)
            {
                var err = RuntimeApi.LoadMetadataForAOTAssembly(textAsset.bytes, HomologousImageMode.Consistent);
                CLog.Info($"LoadMetadata:{aotDllName}， result：{err}");
            }
        }

        bool loadAllComplete = true;
        foreach (var hotfixDllName in GlobalSetting.Ins.HotfixAssemblyList)
        {
            var operation = YooAssets.LoadAssetAsync<TextAsset>(hotfixDllName);
            await operation;
            var ta = operation.AssetObject as TextAsset;
            if (operation.Status != EOperationStatus.Succeed || ta == null)
            {
                CLog.Error($"加载热更dll失败：{hotfixDllName}");
                loadAllComplete = false;
                continue;
            }
            else
            {
                var assembly = Assembly.Load(ta.bytes);
                CLog.Info($"加载热更dll成功：{hotfixDllName}");
            }
        }

        if (loadAllComplete)
        {
            CLog.Info($"----------ProcedureLoadDll Success", logColor: ELogColor.Cyan);
        }
        else
        {
            CLog.Error($"----------ProcedureLoadDll failed");
        }
    }
}