using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"DOTween.dll",
		"DragonPlus.Config.Hub.dll",
		"DragonPlus.Core.dll",
		"DragonPlus.Network.dll",
		"DragonPlus.Save.dll",
		"Google.Protobuf.dll",
		"Newtonsoft.Json.dll",
		"StrompyRobot.dll",
		"System.Core.dll",
		"System.dll",
		"UniTask.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"UnityEngine.UI.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetGroup.<LoadAssetAsync>d__12<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetGroup.<LoadGameObjectAsync>d__13,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetGroup.<LoadGameObjectAsync>d__14,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetMgr.<LoadAssetAsync>d__28<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetMgr.<LoadGameObjectAsync>d__29,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetReference.<LoadAssetAsync>d__24<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.AssetReference.<LoadGameObjectAsync>d__25,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.ConfigSys.<LoadCensorWordConfig>d__2>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.ConfigSys.<LoadConfig>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.FlySys.<PlaySettleRewardAnimation>d__30>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeFairy.<DropElement>d__13,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeFairySys.<SimulateWork>d__2,float>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeGroundCell.<OpenElementBox>d__49>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<CheckMerge>d__26,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<CheckMultiToMulti>d__3,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<CheckMultiToSingle>d__2,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<CheckSingleToMulti>d__1,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<CheckSingleToSingle>d__0,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<DoMerge>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<GenerateNewElement>d__29>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.MergeOperation.<MergeOnce>d__25>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.PlotScene.<PlayAppearAnimation>d__48>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.PlotScene.<PlayHappyAnimation>d__47>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.SequenceNode.<Execute>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.TMUtility.<PlayAnimationAsync>d__16>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.TMUtility.<WaitNFrame>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.TMUtility.<WaitSeconds>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UIBase.<LoadAssetAsync>d__70<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UIBase.<LoadGameObjectAsync>d__71,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UIBase.<PlayAnimationAsync>d__73>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UIItemView.<FlyItemToTarget>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UILoading.<PreloadConfig>d__26>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UITripleMatchWinChest.<StartAnimation>d__16>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<TMGame.UIWindow.<ShowCloseAnimation>d__63>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetGroup.<LoadAssetAsync>d__12<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetGroup.<LoadGameObjectAsync>d__13,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetGroup.<LoadGameObjectAsync>d__14,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetMgr.<LoadAssetAsync>d__28<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetMgr.<LoadGameObjectAsync>d__29,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetReference.<LoadAssetAsync>d__24<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.AssetReference.<LoadGameObjectAsync>d__25,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.ConfigSys.<LoadCensorWordConfig>d__2>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.ConfigSys.<LoadConfig>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.FlySys.<PlaySettleRewardAnimation>d__30>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeFairy.<DropElement>d__13,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeFairySys.<SimulateWork>d__2,float>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeGroundCell.<OpenElementBox>d__49>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<CheckMerge>d__26,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<CheckMultiToMulti>d__3,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<CheckMultiToSingle>d__2,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<CheckSingleToMulti>d__1,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<CheckSingleToSingle>d__0,byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<DoMerge>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<GenerateNewElement>d__29>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.MergeOperation.<MergeOnce>d__25>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.PlotScene.<PlayAppearAnimation>d__48>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.PlotScene.<PlayHappyAnimation>d__47>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.SequenceNode.<Execute>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.TMUtility.<PlayAnimationAsync>d__16>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.TMUtility.<WaitNFrame>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.TMUtility.<WaitSeconds>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UIBase.<LoadAssetAsync>d__70<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UIBase.<LoadGameObjectAsync>d__71,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UIBase.<PlayAnimationAsync>d__73>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UIItemView.<FlyItemToTarget>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UILoading.<PreloadConfig>d__26>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UITripleMatchWinChest.<StartAnimation>d__16>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<TMGame.UIWindow.<ShowCloseAnimation>d__63>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<float>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<TMGame.TMUtility.<WaitSeconds>d__15>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<TMGame.UILoading.<BeginDownload>d__21>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<TMGame.UIWaiting.<ShowWaiting>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<TMGame.TMUtility.<WaitSeconds>d__15>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<TMGame.UILoading.<BeginDownload>d__21>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<TMGame.UIWaiting.<ShowWaiting>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.IStateMachineRunnerPromise<byte>
	// Cysharp.Threading.Tasks.CompilerServices.IStateMachineRunnerPromise<float>
	// Cysharp.Threading.Tasks.CompilerServices.IStateMachineRunnerPromise<object>
	// Cysharp.Threading.Tasks.ITaskPoolNode<object>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.UIntPtr>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.UIntPtr>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,byte>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,float>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.IUniTaskSource<byte>
	// Cysharp.Threading.Tasks.IUniTaskSource<float>
	// Cysharp.Threading.Tasks.IUniTaskSource<object>
	// Cysharp.Threading.Tasks.Internal.ArrayPool<Cysharp.Threading.Tasks.UniTask<byte>>
	// Cysharp.Threading.Tasks.Internal.ArrayPoolUtil.RentArray<Cysharp.Threading.Tasks.UniTask<byte>>
	// Cysharp.Threading.Tasks.Internal.MinimumQueue<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.UIntPtr>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,byte>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,float>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<byte>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<float>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<object>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.UIntPtr>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,byte>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,float>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<byte>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<float>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<object>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.UIntPtr>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,byte>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,float>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<byte>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<float>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<object>
	// Cysharp.Threading.Tasks.UniTask.WhenAllPromise.<>c<byte>
	// Cysharp.Threading.Tasks.UniTask.WhenAllPromise<byte>
	// Cysharp.Threading.Tasks.UniTask<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.UIntPtr>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,byte>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,float>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask<byte>
	// Cysharp.Threading.Tasks.UniTask<float>
	// Cysharp.Threading.Tasks.UniTask<object>
	// Cysharp.Threading.Tasks.UniTaskCompletionSource<byte>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<Cysharp.Threading.Tasks.AsyncUnit>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<System.UIntPtr>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<byte>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<float>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<object>
	// DG.Tweening.Core.DOGetter<UnityEngine.Vector3>
	// DG.Tweening.Core.DOGetter<float>
	// DG.Tweening.Core.DOGetter<int>
	// DG.Tweening.Core.DOGetter<long>
	// DG.Tweening.Core.DOSetter<UnityEngine.Vector3>
	// DG.Tweening.Core.DOSetter<float>
	// DG.Tweening.Core.DOSetter<int>
	// DG.Tweening.Core.DOSetter<long>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.ASMRResultExecuteEvent>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.ClickFBLikeUs>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventASMRLevelFinished>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventActivityBundleDownloadSuccess>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventActivityCreate>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventActivityExpire>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventActivityOnCreate>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventActivityUpdate>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventAvatarChange>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventBuyEnergyInOutLives>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventCheckHasTaskToBeDoneInHome>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventClaimExtraLevelReward>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventCollectActivitySpecialSettleTask>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventCurrencyChange>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventCurrencyFlyAniEnd>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventEgyptianHammerAdd>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventEnergyChange>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventEnterHome>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventFaqQuestionServerBack>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventFaqSelectQuestion>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventHideElementMergeTip>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventHideTapPlayTip>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventJumpToLobbyNavigationType>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventLanguageChange>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventLeaveHome>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventLevelEditorClearItemChose>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventLobbyNavigationActiveTypeChanged>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeBalloonTimeUpdate>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeCellUnlocked>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeCollectScrollToIndex>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeCollectSelectItem>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeCollectUpdateRedPoint>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeFairyStartWork>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeOperationFinish>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeTaskUpdated>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventMergeWorldLoaded>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventNewTaskGenerated>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventObtainNewElements>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventOnApplicationPause>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventPlayButtonCollectAnimation>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventPrepareClaimExtraLevelReward>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventRefreshMiniGameEntrance>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventResolveConflict>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShopBuyMergeItem>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMQuitPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMSpaceOutPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMTimeOutPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMViewQuitPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMViewSpaceOutPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTMViewTimeOutPopup>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventShowTapPlayTip>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventTaskClaim>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventToggleHomeUIState>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.EventUserTouchedMap>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.GameItemChangeEvent>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.IAPFailure>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.IAPSuccess>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.IAPSuccessPopupAfter>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.PrivacyAcceptedEvent>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameChooseDifficulty>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameCollectActivityItemWin>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameCreate>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameFail>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameItemChangeEvent>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameSpaceOut>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameStart>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTargetFinish>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTimeOut>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTimePause>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTriple>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTripleBoost>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTripleBuyBoost>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameTryAgain>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameUseBoost>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.TMatchGameWin>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.WeeklyChallengeAddCollectCnt>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.WeeklyChallengeStateReset>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.WindowCloseEvent>
	// DragonPlus.Core.EventBus.IEventHandler<TMGame.WindowOpenEvent>
	// DragonPlus.Core.SDK<object>
	// DragonPlus.Core.SDKRef<object>
	// DragonPlus.Core.Singleton<object>
	// DragonPlus.Save.StorageDictionary<int,byte>
	// DragonPlus.Save.StorageDictionary<int,int>
	// DragonPlus.Save.StorageDictionary<int,long>
	// DragonPlus.Save.StorageDictionary<int,object>
	// DragonPlus.Save.StorageDictionary<int,ulong>
	// DragonPlus.Save.StorageDictionary<object,byte>
	// DragonPlus.Save.StorageDictionary<object,int>
	// DragonPlus.Save.StorageDictionary<object,object>
	// DragonPlus.Save.StorageList<UnityEngine.Vector2Int>
	// DragonPlus.Save.StorageList<int>
	// DragonPlus.Save.StorageList<object>
	// Google.Protobuf.Collections.MapField.<>c<object,object>
	// Google.Protobuf.Collections.MapField.<>c__DisplayClass7_0<object,object>
	// Google.Protobuf.Collections.MapField.Codec.MessageAdapter<object,object>
	// Google.Protobuf.Collections.MapField.Codec<object,object>
	// Google.Protobuf.Collections.MapField.DictionaryEnumerator<object,object>
	// Google.Protobuf.Collections.MapField.MapView<object,object,object>
	// Google.Protobuf.Collections.MapField<object,object>
	// Google.Protobuf.Collections.RepeatedField.<GetEnumerator>d__22<int>
	// Google.Protobuf.Collections.RepeatedField.<GetEnumerator>d__22<object>
	// Google.Protobuf.Collections.RepeatedField.<GetEnumerator>d__22<ulong>
	// Google.Protobuf.Collections.RepeatedField<int>
	// Google.Protobuf.Collections.RepeatedField<object>
	// Google.Protobuf.Collections.RepeatedField<ulong>
	// Google.Protobuf.FieldCodec.<>c<int>
	// Google.Protobuf.FieldCodec.<>c<object>
	// Google.Protobuf.FieldCodec.<>c<ulong>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass38_0<int>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass38_0<object>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass38_0<ulong>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass39_0<int>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass39_0<object>
	// Google.Protobuf.FieldCodec.<>c__DisplayClass39_0<ulong>
	// Google.Protobuf.FieldCodec.InputMerger<int>
	// Google.Protobuf.FieldCodec.InputMerger<object>
	// Google.Protobuf.FieldCodec.InputMerger<ulong>
	// Google.Protobuf.FieldCodec.ValuesMerger<int>
	// Google.Protobuf.FieldCodec.ValuesMerger<object>
	// Google.Protobuf.FieldCodec.ValuesMerger<ulong>
	// Google.Protobuf.FieldCodec<int>
	// Google.Protobuf.FieldCodec<object>
	// Google.Protobuf.FieldCodec<ulong>
	// Google.Protobuf.IDeepCloneable<int>
	// Google.Protobuf.IDeepCloneable<object>
	// Google.Protobuf.IDeepCloneable<ulong>
	// Google.Protobuf.IMessage<object>
	// Google.Protobuf.MessageParser.<>c__DisplayClass2_0<object>
	// Google.Protobuf.MessageParser<object>
	// System.Action<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Action<Cysharp.Threading.Tasks.UniTask>
	// System.Action<DragonPlus.Core.EventProfileConflict>
	// System.Action<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Action<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Action<System.ValueTuple<object,object>>
	// System.Action<TMGame.ASMRResultExecuteEvent>
	// System.Action<TMGame.AreaRewardInfo>
	// System.Action<TMGame.ClickFBLikeUs>
	// System.Action<TMGame.EventASMRLevelFinished>
	// System.Action<TMGame.EventActivityBundleDownloadSuccess>
	// System.Action<TMGame.EventActivityCreate>
	// System.Action<TMGame.EventActivityExpire>
	// System.Action<TMGame.EventActivityOnCreate>
	// System.Action<TMGame.EventActivityUpdate>
	// System.Action<TMGame.EventAvatarChange>
	// System.Action<TMGame.EventBuyEnergyInOutLives>
	// System.Action<TMGame.EventBuyReviveGiftPackSuccess>
	// System.Action<TMGame.EventCheckHasTaskToBeDoneInHome>
	// System.Action<TMGame.EventClaimExtraLevelReward>
	// System.Action<TMGame.EventCollectActivitySpecialSettleTask>
	// System.Action<TMGame.EventConfigHubUpdated>
	// System.Action<TMGame.EventCurrencyChange>
	// System.Action<TMGame.EventCurrencyFlyAniEnd>
	// System.Action<TMGame.EventEgyptianHammerAdd>
	// System.Action<TMGame.EventEnergyChange>
	// System.Action<TMGame.EventEnterHome>
	// System.Action<TMGame.EventEnterMiniGameLevel>
	// System.Action<TMGame.EventExitMiniGameLevel>
	// System.Action<TMGame.EventFaqQuestionServerBack>
	// System.Action<TMGame.EventFaqSelectQuestion>
	// System.Action<TMGame.EventFocusWidgetGroup>
	// System.Action<TMGame.EventHideElementMergeTip>
	// System.Action<TMGame.EventHideTapPlayTip>
	// System.Action<TMGame.EventJumpToLobbyNavigationType>
	// System.Action<TMGame.EventLanguageChange>
	// System.Action<TMGame.EventLeaveHome>
	// System.Action<TMGame.EventLevelEditorClearItemChose>
	// System.Action<TMGame.EventLobbyNavigationActiveTypeChanged>
	// System.Action<TMGame.EventMergeAreaUnlocked>
	// System.Action<TMGame.EventMergeBalloonTimeUpdate>
	// System.Action<TMGame.EventMergeCellUnlocked>
	// System.Action<TMGame.EventMergeCollectScrollToIndex>
	// System.Action<TMGame.EventMergeCollectSelectItem>
	// System.Action<TMGame.EventMergeCollectUpdateRedPoint>
	// System.Action<TMGame.EventMergeFairyStartWork>
	// System.Action<TMGame.EventMergeOperationFinish>
	// System.Action<TMGame.EventMergeTaskUpdated>
	// System.Action<TMGame.EventMergeWorldLoaded>
	// System.Action<TMGame.EventNewTaskGenerated>
	// System.Action<TMGame.EventObtainNewElements>
	// System.Action<TMGame.EventOnApplicationPause>
	// System.Action<TMGame.EventPlayButtonCollectAnimation>
	// System.Action<TMGame.EventPrepareClaimExtraLevelReward>
	// System.Action<TMGame.EventRefreshMiniGameEntrance>
	// System.Action<TMGame.EventResolveConflict>
	// System.Action<TMGame.EventRestorePurchasesSuccess>
	// System.Action<TMGame.EventRewardMail>
	// System.Action<TMGame.EventShopBuyMergeItem>
	// System.Action<TMGame.EventShowTMQuitPopup>
	// System.Action<TMGame.EventShowTMSpaceOutPopup>
	// System.Action<TMGame.EventShowTMTimeOutPopup>
	// System.Action<TMGame.EventShowTMViewQuitPopup>
	// System.Action<TMGame.EventShowTMViewSpaceOutPopup>
	// System.Action<TMGame.EventShowTMViewTimeOutPopup>
	// System.Action<TMGame.EventShowTapPlayTip>
	// System.Action<TMGame.EventTMatchResultStarRewardExecute>
	// System.Action<TMGame.EventTaskClaim>
	// System.Action<TMGame.EventTeamModifyPlayNameSuccess>
	// System.Action<TMGame.EventToggleHomeUIState>
	// System.Action<TMGame.EventUserTouchedMap>
	// System.Action<TMGame.FairyMergeStep>
	// System.Action<TMGame.GameItemChangeEvent>
	// System.Action<TMGame.IAPFailure>
	// System.Action<TMGame.IAPSuccess>
	// System.Action<TMGame.IAPSuccessPopupAfter>
	// System.Action<TMGame.ItemMoveParam>
	// System.Action<TMGame.MergeGroundCellInfo>
	// System.Action<TMGame.PrivacyAcceptedEvent>
	// System.Action<TMGame.RedPointEvent>
	// System.Action<TMGame.TMatchGameChooseDifficulty>
	// System.Action<TMGame.TMatchGameCollectActivityItemWin>
	// System.Action<TMGame.TMatchGameCreate>
	// System.Action<TMGame.TMatchGameFail>
	// System.Action<TMGame.TMatchGameItemChangeEvent>
	// System.Action<TMGame.TMatchGameSpaceOut>
	// System.Action<TMGame.TMatchGameStart>
	// System.Action<TMGame.TMatchGameTargetFinish>
	// System.Action<TMGame.TMatchGameTimeOut>
	// System.Action<TMGame.TMatchGameTimePause>
	// System.Action<TMGame.TMatchGameTriple>
	// System.Action<TMGame.TMatchGameTripleBoost>
	// System.Action<TMGame.TMatchGameTripleBoostChange>
	// System.Action<TMGame.TMatchGameTripleBoostHandle>
	// System.Action<TMGame.TMatchGameTripleBuyBoost>
	// System.Action<TMGame.TMatchGameTryAgain>
	// System.Action<TMGame.TMatchGameUseBoost>
	// System.Action<TMGame.TMatchGameWin>
	// System.Action<TMGame.TMatchNeedCollectItemEvent>
	// System.Action<TMGame.TMatchNeedShowOutItemEvent>
	// System.Action<TMGame.TabInfo>
	// System.Action<TMGame.WeeklyChallengeAddCollectCnt>
	// System.Action<TMGame.WeeklyChallengeStateReset>
	// System.Action<TMGame.WindowCloseEvent>
	// System.Action<TMGame.WindowOpenEvent>
	// System.Action<UnityEngine.EventSystems.RaycastResult>
	// System.Action<UnityEngine.Vector2Int>
	// System.Action<UnityEngine.Vector3>
	// System.Action<byte,byte>
	// System.Action<byte,object,object,int>
	// System.Action<byte,object>
	// System.Action<byte>
	// System.Action<float>
	// System.Action<int,object,object>
	// System.Action<int,object>
	// System.Action<int>
	// System.Action<object,int>
	// System.Action<object,object,object>
	// System.Action<object,object>
	// System.Action<object,ulong>
	// System.Action<object>
	// System.ArraySegment.Enumerator<int>
	// System.ArraySegment<int>
	// System.Buffers.ArrayPool<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.LockedStack<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.PerCoreLockedStacks<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool<int>
	// System.ByReference<int>
	// System.Collections.Generic.ArraySortHelper<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.ArraySortHelper<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ArraySortHelper<System.ValueTuple<object,object>>
	// System.Collections.Generic.ArraySortHelper<TMGame.AreaRewardInfo>
	// System.Collections.Generic.ArraySortHelper<TMGame.FairyMergeStep>
	// System.Collections.Generic.ArraySortHelper<TMGame.ItemMoveParam>
	// System.Collections.Generic.ArraySortHelper<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.ArraySortHelper<TMGame.TabInfo>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2Int>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector3>
	// System.Collections.Generic.ArraySortHelper<byte>
	// System.Collections.Generic.ArraySortHelper<float>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.Comparer<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.Comparer<System.UIntPtr>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.UIntPtr>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,byte>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,float>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.Comparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.Comparer<TMGame.AreaRewardInfo>
	// System.Collections.Generic.Comparer<TMGame.FairyMergeStep>
	// System.Collections.Generic.Comparer<TMGame.ItemMoveParam>
	// System.Collections.Generic.Comparer<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.Comparer<TMGame.TabInfo>
	// System.Collections.Generic.Comparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.Comparer<UnityEngine.Vector3>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary.Enumerator<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,ulong>
	// System.Collections.Generic.Dictionary.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,ulong>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary.KeyCollection<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.KeyCollection<int,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,ulong>
	// System.Collections.Generic.Dictionary.KeyCollection<object,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<object,float>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,ulong>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary.ValueCollection<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.ValueCollection<int,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,ulong>
	// System.Collections.Generic.Dictionary.ValueCollection<object,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<object,float>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.Dictionary<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary<int,byte>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<int,ulong>
	// System.Collections.Generic.Dictionary<object,byte>
	// System.Collections.Generic.Dictionary<object,float>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.EqualityComparer<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.EqualityComparer<System.UIntPtr>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.UIntPtr>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,byte>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,float>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<float>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<ulong>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.ICollection<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.ICollection<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,Common.Debug.controller.AutoGridLayout.PosInfo>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,System.ValueTuple<object,object>>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,ulong>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.ValueTuple<object,object>>
	// System.Collections.Generic.ICollection<TMGame.AreaRewardInfo>
	// System.Collections.Generic.ICollection<TMGame.FairyMergeStep>
	// System.Collections.Generic.ICollection<TMGame.ItemMoveParam>
	// System.Collections.Generic.ICollection<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.ICollection<TMGame.TabInfo>
	// System.Collections.Generic.ICollection<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2Int>
	// System.Collections.Generic.ICollection<UnityEngine.Vector3>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<float>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.IComparer<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.IComparer<TMGame.AreaRewardInfo>
	// System.Collections.Generic.IComparer<TMGame.FairyMergeStep>
	// System.Collections.Generic.IComparer<TMGame.ItemMoveParam>
	// System.Collections.Generic.IComparer<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.IComparer<TMGame.TabInfo>
	// System.Collections.Generic.IComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IComparer<UnityEngine.Vector3>
	// System.Collections.Generic.IComparer<byte>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,Common.Debug.controller.AutoGridLayout.PosInfo>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,System.ValueTuple<object,object>>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,ulong>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.ValueTuple<object,object>>
	// System.Collections.Generic.IEnumerable<TMGame.AreaRewardInfo>
	// System.Collections.Generic.IEnumerable<TMGame.FairyMergeStep>
	// System.Collections.Generic.IEnumerable<TMGame.ItemMoveParam>
	// System.Collections.Generic.IEnumerable<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.IEnumerable<TMGame.TabInfo>
	// System.Collections.Generic.IEnumerable<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerable<byte>
	// System.Collections.Generic.IEnumerable<float>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerable<ulong>
	// System.Collections.Generic.IEnumerator<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.IEnumerator<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,Common.Debug.controller.AutoGridLayout.PosInfo>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,System.ValueTuple<object,object>>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,ulong>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.ValueTuple<object,object>>
	// System.Collections.Generic.IEnumerator<TMGame.AreaRewardInfo>
	// System.Collections.Generic.IEnumerator<TMGame.FairyMergeStep>
	// System.Collections.Generic.IEnumerator<TMGame.ItemMoveParam>
	// System.Collections.Generic.IEnumerator<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.IEnumerator<TMGame.TabInfo>
	// System.Collections.Generic.IEnumerator<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerator<byte>
	// System.Collections.Generic.IEnumerator<float>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEnumerator<ulong>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.IList<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IList<System.ValueTuple<object,object>>
	// System.Collections.Generic.IList<TMGame.AreaRewardInfo>
	// System.Collections.Generic.IList<TMGame.FairyMergeStep>
	// System.Collections.Generic.IList<TMGame.ItemMoveParam>
	// System.Collections.Generic.IList<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.IList<TMGame.TabInfo>
	// System.Collections.Generic.IList<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IList<UnityEngine.Vector2Int>
	// System.Collections.Generic.IList<UnityEngine.Vector3>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<float>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.IReadOnlyCollection<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.KeyValuePair<System.UIntPtr,object>
	// System.Collections.Generic.KeyValuePair<int,Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.KeyValuePair<int,System.ValueTuple<object,object>>
	// System.Collections.Generic.KeyValuePair<int,byte>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<int,ulong>
	// System.Collections.Generic.KeyValuePair<object,byte>
	// System.Collections.Generic.KeyValuePair<object,float>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.LinkedList.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.LinkedList<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.LinkedListNode<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List.Enumerator<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.List.Enumerator<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.List.Enumerator<System.ValueTuple<object,object>>
	// System.Collections.Generic.List.Enumerator<TMGame.AreaRewardInfo>
	// System.Collections.Generic.List.Enumerator<TMGame.FairyMergeStep>
	// System.Collections.Generic.List.Enumerator<TMGame.ItemMoveParam>
	// System.Collections.Generic.List.Enumerator<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.List.Enumerator<TMGame.TabInfo>
	// System.Collections.Generic.List.Enumerator<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector3>
	// System.Collections.Generic.List.Enumerator<byte>
	// System.Collections.Generic.List.Enumerator<float>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.List<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.List<System.ValueTuple<object,object>>
	// System.Collections.Generic.List<TMGame.AreaRewardInfo>
	// System.Collections.Generic.List<TMGame.FairyMergeStep>
	// System.Collections.Generic.List<TMGame.ItemMoveParam>
	// System.Collections.Generic.List<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.List<TMGame.TabInfo>
	// System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.List<UnityEngine.Vector2Int>
	// System.Collections.Generic.List<UnityEngine.Vector3>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.Generic.ObjectComparer<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectComparer<System.UIntPtr>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.UIntPtr>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,byte>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,float>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.ObjectComparer<TMGame.AreaRewardInfo>
	// System.Collections.Generic.ObjectComparer<TMGame.FairyMergeStep>
	// System.Collections.Generic.ObjectComparer<TMGame.ItemMoveParam>
	// System.Collections.Generic.ObjectComparer<TMGame.MergeGroundCellInfo>
	// System.Collections.Generic.ObjectComparer<TMGame.TabInfo>
	// System.Collections.Generic.ObjectComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector3>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<Common.Debug.controller.AutoGridLayout.PosInfo>
	// System.Collections.Generic.ObjectEqualityComparer<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.UIntPtr>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.UIntPtr>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,byte>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,float>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<float>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<ulong>
	// System.Collections.Generic.Queue.Enumerator<float>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<float>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.ValueListBuilder<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Collections.ObjectModel.ReadOnlyCollection<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.ValueTuple<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<TMGame.AreaRewardInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<TMGame.FairyMergeStep>
	// System.Collections.ObjectModel.ReadOnlyCollection<TMGame.ItemMoveParam>
	// System.Collections.ObjectModel.ReadOnlyCollection<TMGame.MergeGroundCellInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<TMGame.TabInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector3>
	// System.Collections.ObjectModel.ReadOnlyCollection<byte>
	// System.Collections.ObjectModel.ReadOnlyCollection<float>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Comparison<Cysharp.Threading.Tasks.UniTask>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Comparison<System.ValueTuple<object,object>>
	// System.Comparison<TMGame.AreaRewardInfo>
	// System.Comparison<TMGame.FairyMergeStep>
	// System.Comparison<TMGame.ItemMoveParam>
	// System.Comparison<TMGame.MergeGroundCellInfo>
	// System.Comparison<TMGame.TabInfo>
	// System.Comparison<UnityEngine.EventSystems.RaycastResult>
	// System.Comparison<UnityEngine.Vector2Int>
	// System.Comparison<UnityEngine.Vector3>
	// System.Comparison<byte>
	// System.Comparison<float>
	// System.Comparison<int>
	// System.Comparison<object>
	// System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Func<System.Collections.Generic.KeyValuePair<object,object>,System.Collections.DictionaryEntry>
	// System.Func<System.Collections.Generic.KeyValuePair<object,object>,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<object,object>,object>
	// System.Func<System.Threading.Tasks.VoidTaskResult>
	// System.Func<System.UIntPtr>
	// System.Func<System.ValueTuple<byte,System.UIntPtr>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Func<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Func<System.ValueTuple<byte,byte>>
	// System.Func<System.ValueTuple<byte,float>>
	// System.Func<System.ValueTuple<byte,object>>
	// System.Func<UnityEngine.Vector3,int,UnityEngine.Vector3>
	// System.Func<UnityEngine.Vector3,object,UnityEngine.Vector3>
	// System.Func<byte>
	// System.Func<float,byte>
	// System.Func<float>
	// System.Func<int,byte>
	// System.Func<int,int,object>
	// System.Func<int,int>
	// System.Func<int,object>
	// System.Func<int>
	// System.Func<object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,System.UIntPtr>
	// System.Func<object,System.ValueTuple<byte,System.UIntPtr>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Func<object,System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Func<object,System.ValueTuple<byte,byte>>
	// System.Func<object,System.ValueTuple<byte,float>>
	// System.Func<object,System.ValueTuple<byte,object>>
	// System.Func<object,byte>
	// System.Func<object,float>
	// System.Func<object,int,int,int,object>
	// System.Func<object,int,object>
	// System.Func<object,int>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object,ulong>
	// System.Func<object>
	// System.Func<ulong,int>
	// System.IEquatable<object>
	// System.Linq.Buffer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.<ExceptIterator>d__77<int>
	// System.Linq.Enumerable.<ExceptIterator>d__77<object>
	// System.Linq.Enumerable.Iterator<float>
	// System.Linq.Enumerable.Iterator<int>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<float>
	// System.Linq.Enumerable.WhereEnumerableIterator<int>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<int,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,float>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<int,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,float>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,int>
	// System.Linq.Enumerable.WhereSelectListIterator<int,object>
	// System.Linq.Enumerable.WhereSelectListIterator<object,float>
	// System.Linq.Enumerable.WhereSelectListIterator<object,int>
	// System.Linq.EnumerableSorter<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Linq.EnumerableSorter<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.EnumerableSorter<object,float>
	// System.Linq.EnumerableSorter<object>
	// System.Linq.IdentityFunction.<>c<object>
	// System.Linq.IdentityFunction<object>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<object>
	// System.Linq.OrderedEnumerable<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Linq.OrderedEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.OrderedEnumerable<object,float>
	// System.Linq.OrderedEnumerable<object>
	// System.Linq.Set<int>
	// System.Linq.Set<object>
	// System.Nullable<System.TimeSpan>
	// System.Nullable<UnityEngine.Color>
	// System.Predicate<Cysharp.Threading.Tasks.UniTask<byte>>
	// System.Predicate<Cysharp.Threading.Tasks.UniTask>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Predicate<System.ValueTuple<object,object>>
	// System.Predicate<TMGame.AreaRewardInfo>
	// System.Predicate<TMGame.FairyMergeStep>
	// System.Predicate<TMGame.ItemMoveParam>
	// System.Predicate<TMGame.MergeGroundCellInfo>
	// System.Predicate<TMGame.TabInfo>
	// System.Predicate<UnityEngine.EventSystems.RaycastResult>
	// System.Predicate<UnityEngine.Vector2Int>
	// System.Predicate<UnityEngine.Vector3>
	// System.Predicate<byte>
	// System.Predicate<float>
	// System.Predicate<int>
	// System.Predicate<object>
	// System.ReadOnlySpan.Enumerator<int>
	// System.ReadOnlySpan<int>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.UIntPtr>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<float>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.UIntPtr>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<byte>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<float>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.UIntPtr>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<byte>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<float>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.UIntPtr>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<byte>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<float>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.UIntPtr>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<byte>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<float>
	// System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.TaskAwaiter<System.UIntPtr>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.TaskAwaiter<byte>
	// System.Runtime.CompilerServices.TaskAwaiter<float>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.UIntPtr>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.UIntPtr>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,byte>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,float>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<System.ValueTuple<byte,object>>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<byte>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<float>
	// System.Runtime.CompilerServices.ValueTaskAwaiter<object>
	// System.Span.Enumerator<int>
	// System.Span<int>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.UIntPtr>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<byte>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<float>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.UIntPtr>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.Sources.IValueTaskSource<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.Sources.IValueTaskSource<byte>
	// System.Threading.Tasks.Sources.IValueTaskSource<float>
	// System.Threading.Tasks.Sources.IValueTaskSource<object>
	// System.Threading.Tasks.Task<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.Task<System.UIntPtr>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.Task<byte>
	// System.Threading.Tasks.Task<float>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskCompletionSource<byte>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.UIntPtr>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<byte>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<float>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory<System.UIntPtr>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.TaskFactory<byte>
	// System.Threading.Tasks.TaskFactory<float>
	// System.Threading.Tasks.TaskFactory<object>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.UIntPtr>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<byte>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<float>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask.<>c<object>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.UIntPtr>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<byte>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<float>
	// System.Threading.Tasks.ValueTask.ValueTaskSourceAsTask<object>
	// System.Threading.Tasks.ValueTask<System.UIntPtr>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.UIntPtr>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,byte>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,float>>
	// System.Threading.Tasks.ValueTask<System.ValueTuple<byte,object>>
	// System.Threading.Tasks.ValueTask<byte>
	// System.Threading.Tasks.ValueTask<float>
	// System.Threading.Tasks.ValueTask<object>
	// System.Tuple<object,object>
	// System.ValueTuple<byte,Cysharp.Threading.Tasks.UniTask>
	// System.ValueTuple<byte,System.UIntPtr>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.UIntPtr>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,byte>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,float>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,byte>>
	// System.ValueTuple<byte,System.ValueTuple<byte,float>>
	// System.ValueTuple<byte,System.ValueTuple<byte,object>>
	// System.ValueTuple<byte,byte>
	// System.ValueTuple<byte,float>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<int,int>
	// System.ValueTuple<object,object>
	// UnityEngine.EventSystems.ExecuteEvents.EventFunction<object>
	// UnityEngine.Events.InvokableCall<UnityEngine.Vector2>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.InvokableCall<int>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<UnityEngine.Vector2>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<UnityEngine.Vector2>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Events.UnityEvent<int>
	// UnityEngine.Events.UnityEvent<object>
	// UnityEngine.Pool.CollectionPool.<>c<object,object>
	// UnityEngine.Pool.CollectionPool<object,object>
	// }}

	public void RefMethods()
	{
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39>(Cysharp.Threading.Tasks.UniTask.Awaiter&,GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.ConfigSys.<LoadConfig>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.ConfigSys.<LoadConfig>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.FlySys.<PlaySettleRewardAnimation>d__30>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.FlySys.<PlaySettleRewardAnimation>d__30&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeGroundCell.<OpenElementBox>d__49>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeGroundCell.<OpenElementBox>d__49&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<DoMerge>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<DoMerge>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.PlotScene.<PlayAppearAnimation>d__48>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.PlotScene.<PlayAppearAnimation>d__48&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.PlotScene.<PlayHappyAnimation>d__47>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.PlotScene.<PlayHappyAnimation>d__47&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.SequenceNode.<Execute>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.SequenceNode.<Execute>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<WaitNFrame>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<WaitNFrame>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<WaitSeconds>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<WaitSeconds>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIBase.<PlayAnimationAsync>d__73>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIBase.<PlayAnimationAsync>d__73&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIItemView.<FlyItemToTarget>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIItemView.<FlyItemToTarget>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UILoading.<PreloadConfig>d__26>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UILoading.<PreloadConfig>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchWinChest.<StartAnimation>d__16>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchWinChest.<StartAnimation>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIWindow.<ShowCloseAnimation>d__63>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIWindow.<ShowCloseAnimation>d__63&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.TMUtility.<PlayAnimationAsync>d__16>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.TMUtility.<PlayAnimationAsync>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.ConfigSys.<LoadCensorWordConfig>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.ConfigSys.<LoadCensorWordConfig>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.MergeOperation.<GenerateNewElement>d__29>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.MergeOperation.<GenerateNewElement>d__29&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.MergeOperation.<MergeOnce>d__25>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.MergeOperation.<MergeOnce>d__25&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<CheckMerge>d__26>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<CheckMerge>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<CheckMultiToMulti>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<CheckMultiToMulti>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<CheckMultiToSingle>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<CheckMultiToSingle>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<CheckSingleToMulti>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<CheckSingleToMulti>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeOperation.<CheckSingleToSingle>d__0>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeOperation.<CheckSingleToSingle>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeFairy.<DropElement>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeFairy.<DropElement>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeOperation.<CheckMerge>d__26>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeOperation.<CheckMerge>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeOperation.<CheckMultiToSingle>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeOperation.<CheckMultiToSingle>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeOperation.<CheckSingleToMulti>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeOperation.<CheckSingleToMulti>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<float>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeFairySys.<SimulateWork>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeFairySys.<SimulateWork>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<float>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeFairySys.<SimulateWork>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeFairySys.<SimulateWork>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.AssetMgr.<LoadAssetAsync>d__28<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.AssetMgr.<LoadAssetAsync>d__28<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.AssetMgr.<LoadGameObjectAsync>d__29>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.AssetMgr.<LoadGameObjectAsync>d__29&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.AssetGroup.<LoadAssetAsync>d__12<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.AssetGroup.<LoadAssetAsync>d__12<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.AssetGroup.<LoadGameObjectAsync>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.AssetGroup.<LoadGameObjectAsync>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.AssetGroup.<LoadGameObjectAsync>d__14>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.AssetGroup.<LoadGameObjectAsync>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.AssetReference.<LoadAssetAsync>d__24<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.AssetReference.<LoadAssetAsync>d__24<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.AssetReference.<LoadGameObjectAsync>d__25>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.AssetReference.<LoadGameObjectAsync>d__25&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.UIBase.<LoadAssetAsync>d__70<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.UIBase.<LoadAssetAsync>d__70<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,TMGame.UIBase.<LoadGameObjectAsync>d__71>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,TMGame.UIBase.<LoadGameObjectAsync>d__71&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60>(DragonPlus.LocalizationManager.<LoadLocalFontAsync>d__60&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39>(GamePlay.MapBuildingGraphic.<PerformUpdateBuildingLevelAnimation>d__39&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.ConfigSys.<LoadCensorWordConfig>d__2>(TMGame.ConfigSys.<LoadCensorWordConfig>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.ConfigSys.<LoadConfig>d__1>(TMGame.ConfigSys.<LoadConfig>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.FlySys.<PlaySettleRewardAnimation>d__30>(TMGame.FlySys.<PlaySettleRewardAnimation>d__30&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.MergeGroundCell.<OpenElementBox>d__49>(TMGame.MergeGroundCell.<OpenElementBox>d__49&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.MergeOperation.<DoMerge>d__4>(TMGame.MergeOperation.<DoMerge>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.MergeOperation.<GenerateNewElement>d__29>(TMGame.MergeOperation.<GenerateNewElement>d__29&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.MergeOperation.<MergeOnce>d__25>(TMGame.MergeOperation.<MergeOnce>d__25&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7>(TMGame.PlotNodeUIView.<FlyFxToProgressBar>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.PlotScene.<PlayAppearAnimation>d__48>(TMGame.PlotScene.<PlayAppearAnimation>d__48&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.PlotScene.<PlayHappyAnimation>d__47>(TMGame.PlotScene.<PlayHappyAnimation>d__47&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.SequenceNode.<Execute>d__6>(TMGame.SequenceNode.<Execute>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.TMUtility.<PlayAnimationAsync>d__16>(TMGame.TMUtility.<PlayAnimationAsync>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.TMUtility.<WaitNFrame>d__4>(TMGame.TMUtility.<WaitNFrame>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.TMUtility.<WaitSeconds>d__12>(TMGame.TMUtility.<WaitSeconds>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UIBase.<PlayAnimationAsync>d__73>(TMGame.UIBase.<PlayAnimationAsync>d__73&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UIItemView.<FlyItemToTarget>d__10>(TMGame.UIItemView.<FlyItemToTarget>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UILoading.<PreloadConfig>d__26>(TMGame.UILoading.<PreloadConfig>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UITripleMatchWinChest.<StartAnimation>d__16>(TMGame.UITripleMatchWinChest.<StartAnimation>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UIWindow.<PreLoadAssets>d__50>(TMGame.UIWindow.<PreLoadAssets>d__50&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<TMGame.UIWindow.<ShowCloseAnimation>d__63>(TMGame.UIWindow.<ShowCloseAnimation>d__63&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeFairy.<DropElement>d__13>(TMGame.MergeFairy.<DropElement>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeOperation.<CheckMerge>d__26>(TMGame.MergeOperation.<CheckMerge>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeOperation.<CheckMultiToMulti>d__3>(TMGame.MergeOperation.<CheckMultiToMulti>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeOperation.<CheckMultiToSingle>d__2>(TMGame.MergeOperation.<CheckMultiToSingle>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeOperation.<CheckSingleToMulti>d__1>(TMGame.MergeOperation.<CheckSingleToMulti>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<byte>.Start<TMGame.MergeOperation.<CheckSingleToSingle>d__0>(TMGame.MergeOperation.<CheckSingleToSingle>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<float>.Start<TMGame.MergeFairySys.<SimulateWork>d__2>(TMGame.MergeFairySys.<SimulateWork>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetGroup.<LoadAssetAsync>d__12<object>>(TMGame.AssetGroup.<LoadAssetAsync>d__12<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetGroup.<LoadGameObjectAsync>d__13>(TMGame.AssetGroup.<LoadGameObjectAsync>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetGroup.<LoadGameObjectAsync>d__14>(TMGame.AssetGroup.<LoadGameObjectAsync>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetMgr.<LoadAssetAsync>d__28<object>>(TMGame.AssetMgr.<LoadAssetAsync>d__28<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetMgr.<LoadGameObjectAsync>d__29>(TMGame.AssetMgr.<LoadGameObjectAsync>d__29&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetReference.<LoadAssetAsync>d__24<object>>(TMGame.AssetReference.<LoadAssetAsync>d__24<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.AssetReference.<LoadGameObjectAsync>d__25>(TMGame.AssetReference.<LoadGameObjectAsync>d__25&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.UIBase.<LoadAssetAsync>d__70<object>>(TMGame.UIBase.<LoadAssetAsync>d__70<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<TMGame.UIBase.<LoadGameObjectAsync>d__71>(TMGame.UIBase.<LoadGameObjectAsync>d__71&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<WaitSeconds>d__15>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<WaitSeconds>d__15&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIWaiting.<ShowWaiting>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIWaiting.<ShowWaiting>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UILoading.<BeginDownload>d__21>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UILoading.<BeginDownload>d__21&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14>(TMGame.ContactUsOtherCellWidget.<ReloadFromData>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<TMGame.TMUtility.<WaitSeconds>d__15>(TMGame.TMUtility.<WaitSeconds>d__15&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>>(TMGame.UIBase.<AsyncAdjustIconNumInternal>d__58<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<TMGame.UILoading.<BeginDownload>d__21>(TMGame.UILoading.<BeginDownload>d__21&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<TMGame.UIWaiting.<ShowWaiting>d__12>(TMGame.UIWaiting.<ShowWaiting>d__12&)
		// System.Void Cysharp.Threading.Tasks.Internal.ArrayPoolUtil.EnsureCapacity<Cysharp.Threading.Tasks.UniTask<byte>>(Cysharp.Threading.Tasks.UniTask<byte>[]&,int,Cysharp.Threading.Tasks.Internal.ArrayPool<Cysharp.Threading.Tasks.UniTask<byte>>)
		// System.Void Cysharp.Threading.Tasks.Internal.ArrayPoolUtil.EnsureCapacityCore<Cysharp.Threading.Tasks.UniTask<byte>>(Cysharp.Threading.Tasks.UniTask<byte>[]&,int,Cysharp.Threading.Tasks.Internal.ArrayPool<Cysharp.Threading.Tasks.UniTask<byte>>)
		// Cysharp.Threading.Tasks.Internal.ArrayPoolUtil.RentArray<Cysharp.Threading.Tasks.UniTask<byte>> Cysharp.Threading.Tasks.Internal.ArrayPoolUtil.Materialize<Cysharp.Threading.Tasks.UniTask<byte>>(System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask<byte>>)
		// bool Cysharp.Threading.Tasks.Internal.RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<Cysharp.Threading.Tasks.UniTask<byte>>()
		// Cysharp.Threading.Tasks.UniTask<byte[]> Cysharp.Threading.Tasks.UniTask.WhenAll<byte>(System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask<byte>>)
		// object DG.Tweening.TweenExtensions.Play<object>(object)
		// object DG.Tweening.TweenSettingsExtensions.From<object>(object)
		// object DG.Tweening.TweenSettingsExtensions.OnComplete<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.OnStart<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.OnUpdate<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.SetDelay<object>(object,float)
		// object DG.Tweening.TweenSettingsExtensions.SetEase<object>(object,DG.Tweening.Ease)
		// object DG.Tweening.TweenSettingsExtensions.SetLoops<object>(object,int)
		// object DG.Tweening.TweenSettingsExtensions.SetLoops<object>(object,int,DG.Tweening.LoopType)
		// object DG.Tweening.TweenSettingsExtensions.SetRelative<object>(object)
		// object DG.Tweening.TweenSettingsExtensions.SetTarget<object>(object,object)
		// bool DragonPlus.Config.IConfigHub.RegisterConfig<object>()
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.ASMRResultExecuteEvent>(TMGame.ASMRResultExecuteEvent)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.ClickFBLikeUs>(TMGame.ClickFBLikeUs)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventASMRLevelFinished>(TMGame.EventASMRLevelFinished)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventActivityBundleDownloadSuccess>(TMGame.EventActivityBundleDownloadSuccess)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventActivityCreate>(TMGame.EventActivityCreate)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventActivityExpire>(TMGame.EventActivityExpire)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventActivityOnCreate>(TMGame.EventActivityOnCreate)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventActivityUpdate>(TMGame.EventActivityUpdate)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventAvatarChange>(TMGame.EventAvatarChange)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventBuyEnergyInOutLives>(TMGame.EventBuyEnergyInOutLives)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventCheckHasTaskToBeDoneInHome>(TMGame.EventCheckHasTaskToBeDoneInHome)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventClaimExtraLevelReward>(TMGame.EventClaimExtraLevelReward)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventCollectActivitySpecialSettleTask>(TMGame.EventCollectActivitySpecialSettleTask)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventCurrencyChange>(TMGame.EventCurrencyChange)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventCurrencyFlyAniEnd>(TMGame.EventCurrencyFlyAniEnd)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventEgyptianHammerAdd>(TMGame.EventEgyptianHammerAdd)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventEnergyChange>(TMGame.EventEnergyChange)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventEnterHome>(TMGame.EventEnterHome)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventFaqQuestionServerBack>(TMGame.EventFaqQuestionServerBack)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventFaqSelectQuestion>(TMGame.EventFaqSelectQuestion)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventHideElementMergeTip>(TMGame.EventHideElementMergeTip)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventHideTapPlayTip>(TMGame.EventHideTapPlayTip)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventJumpToLobbyNavigationType>(TMGame.EventJumpToLobbyNavigationType)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventLanguageChange>(TMGame.EventLanguageChange)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventLeaveHome>(TMGame.EventLeaveHome)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventLevelEditorClearItemChose>(TMGame.EventLevelEditorClearItemChose)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventLobbyNavigationActiveTypeChanged>(TMGame.EventLobbyNavigationActiveTypeChanged)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeBalloonTimeUpdate>(TMGame.EventMergeBalloonTimeUpdate)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeCellUnlocked>(TMGame.EventMergeCellUnlocked)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeCollectScrollToIndex>(TMGame.EventMergeCollectScrollToIndex)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeCollectSelectItem>(TMGame.EventMergeCollectSelectItem)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeCollectUpdateRedPoint>(TMGame.EventMergeCollectUpdateRedPoint)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeFairyStartWork>(TMGame.EventMergeFairyStartWork)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeOperationFinish>(TMGame.EventMergeOperationFinish)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeTaskUpdated>(TMGame.EventMergeTaskUpdated)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventMergeWorldLoaded>(TMGame.EventMergeWorldLoaded)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventNewTaskGenerated>(TMGame.EventNewTaskGenerated)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventObtainNewElements>(TMGame.EventObtainNewElements)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventOnApplicationPause>(TMGame.EventOnApplicationPause)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventPlayButtonCollectAnimation>(TMGame.EventPlayButtonCollectAnimation)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventPrepareClaimExtraLevelReward>(TMGame.EventPrepareClaimExtraLevelReward)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventRefreshMiniGameEntrance>(TMGame.EventRefreshMiniGameEntrance)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventResolveConflict>(TMGame.EventResolveConflict)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShopBuyMergeItem>(TMGame.EventShopBuyMergeItem)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMQuitPopup>(TMGame.EventShowTMQuitPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMSpaceOutPopup>(TMGame.EventShowTMSpaceOutPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMTimeOutPopup>(TMGame.EventShowTMTimeOutPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMViewQuitPopup>(TMGame.EventShowTMViewQuitPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMViewSpaceOutPopup>(TMGame.EventShowTMViewSpaceOutPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTMViewTimeOutPopup>(TMGame.EventShowTMViewTimeOutPopup)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventShowTapPlayTip>(TMGame.EventShowTapPlayTip)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventTaskClaim>(TMGame.EventTaskClaim)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventToggleHomeUIState>(TMGame.EventToggleHomeUIState)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.EventUserTouchedMap>(TMGame.EventUserTouchedMap)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.GameItemChangeEvent>(TMGame.GameItemChangeEvent)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.IAPFailure>(TMGame.IAPFailure)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.IAPSuccess>(TMGame.IAPSuccess)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.IAPSuccessPopupAfter>(TMGame.IAPSuccessPopupAfter)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.PrivacyAcceptedEvent>(TMGame.PrivacyAcceptedEvent)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameChooseDifficulty>(TMGame.TMatchGameChooseDifficulty)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameCollectActivityItemWin>(TMGame.TMatchGameCollectActivityItemWin)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameCreate>(TMGame.TMatchGameCreate)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameFail>(TMGame.TMatchGameFail)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameItemChangeEvent>(TMGame.TMatchGameItemChangeEvent)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameSpaceOut>(TMGame.TMatchGameSpaceOut)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameStart>(TMGame.TMatchGameStart)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTargetFinish>(TMGame.TMatchGameTargetFinish)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTimeOut>(TMGame.TMatchGameTimeOut)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTimePause>(TMGame.TMatchGameTimePause)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTriple>(TMGame.TMatchGameTriple)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTripleBoost>(TMGame.TMatchGameTripleBoost)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTripleBuyBoost>(TMGame.TMatchGameTripleBuyBoost)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameTryAgain>(TMGame.TMatchGameTryAgain)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameUseBoost>(TMGame.TMatchGameUseBoost)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.TMatchGameWin>(TMGame.TMatchGameWin)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.WeeklyChallengeAddCollectCnt>(TMGame.WeeklyChallengeAddCollectCnt)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.WeeklyChallengeStateReset>(TMGame.WeeklyChallengeStateReset)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.WindowCloseEvent>(TMGame.WindowCloseEvent)
		// System.Void DragonPlus.Core.Game.GetMod<ModEvent>().Dispatch<TMGame.WindowOpenEvent>(TMGame.WindowOpenEvent)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<DragonPlus.Core.EventProfileConflict>(System.Action<DragonPlus.Core.EventProfileConflict>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventActivityBundleDownloadSuccess>(System.Action<TMGame.EventActivityBundleDownloadSuccess>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventActivityExpire>(System.Action<TMGame.EventActivityExpire>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventActivityOnCreate>(System.Action<TMGame.EventActivityOnCreate>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventAvatarChange>(System.Action<TMGame.EventAvatarChange>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventBuyEnergyInOutLives>(System.Action<TMGame.EventBuyEnergyInOutLives>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventBuyReviveGiftPackSuccess>(System.Action<TMGame.EventBuyReviveGiftPackSuccess>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventConfigHubUpdated>(System.Action<TMGame.EventConfigHubUpdated>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventCurrencyChange>(System.Action<TMGame.EventCurrencyChange>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventCurrencyFlyAniEnd>(System.Action<TMGame.EventCurrencyFlyAniEnd>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventEnergyChange>(System.Action<TMGame.EventEnergyChange>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventEnterHome>(System.Action<TMGame.EventEnterHome>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventEnterMiniGameLevel>(System.Action<TMGame.EventEnterMiniGameLevel>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventExitMiniGameLevel>(System.Action<TMGame.EventExitMiniGameLevel>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventFaqQuestionServerBack>(System.Action<TMGame.EventFaqQuestionServerBack>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventFaqSelectQuestion>(System.Action<TMGame.EventFaqSelectQuestion>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventFocusWidgetGroup>(System.Action<TMGame.EventFocusWidgetGroup>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventHideElementMergeTip>(System.Action<TMGame.EventHideElementMergeTip>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventHideTapPlayTip>(System.Action<TMGame.EventHideTapPlayTip>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventJumpToLobbyNavigationType>(System.Action<TMGame.EventJumpToLobbyNavigationType>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventLanguageChange>(System.Action<TMGame.EventLanguageChange>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventLeaveHome>(System.Action<TMGame.EventLeaveHome>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventLevelEditorClearItemChose>(System.Action<TMGame.EventLevelEditorClearItemChose>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventLobbyNavigationActiveTypeChanged>(System.Action<TMGame.EventLobbyNavigationActiveTypeChanged>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeAreaUnlocked>(System.Action<TMGame.EventMergeAreaUnlocked>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeBalloonTimeUpdate>(System.Action<TMGame.EventMergeBalloonTimeUpdate>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeCellUnlocked>(System.Action<TMGame.EventMergeCellUnlocked>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeCollectScrollToIndex>(System.Action<TMGame.EventMergeCollectScrollToIndex>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeCollectSelectItem>(System.Action<TMGame.EventMergeCollectSelectItem>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeCollectUpdateRedPoint>(System.Action<TMGame.EventMergeCollectUpdateRedPoint>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeFairyStartWork>(System.Action<TMGame.EventMergeFairyStartWork>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeOperationFinish>(System.Action<TMGame.EventMergeOperationFinish>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeTaskUpdated>(System.Action<TMGame.EventMergeTaskUpdated>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventMergeWorldLoaded>(System.Action<TMGame.EventMergeWorldLoaded>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventNewTaskGenerated>(System.Action<TMGame.EventNewTaskGenerated>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventObtainNewElements>(System.Action<TMGame.EventObtainNewElements>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventOnApplicationPause>(System.Action<TMGame.EventOnApplicationPause>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventPlayButtonCollectAnimation>(System.Action<TMGame.EventPlayButtonCollectAnimation>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventRefreshMiniGameEntrance>(System.Action<TMGame.EventRefreshMiniGameEntrance>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventRestorePurchasesSuccess>(System.Action<TMGame.EventRestorePurchasesSuccess>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventRewardMail>(System.Action<TMGame.EventRewardMail>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventShopBuyMergeItem>(System.Action<TMGame.EventShopBuyMergeItem>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventShowTapPlayTip>(System.Action<TMGame.EventShowTapPlayTip>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventTMatchResultStarRewardExecute>(System.Action<TMGame.EventTMatchResultStarRewardExecute>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventTaskClaim>(System.Action<TMGame.EventTaskClaim>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventTeamModifyPlayNameSuccess>(System.Action<TMGame.EventTeamModifyPlayNameSuccess>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventToggleHomeUIState>(System.Action<TMGame.EventToggleHomeUIState>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.EventUserTouchedMap>(System.Action<TMGame.EventUserTouchedMap>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.IAPSuccess>(System.Action<TMGame.IAPSuccess>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.RedPointEvent>(System.Action<TMGame.RedPointEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameItemChangeEvent>(System.Action<TMGame.TMatchGameItemChangeEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameSpaceOut>(System.Action<TMGame.TMatchGameSpaceOut>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameStart>(System.Action<TMGame.TMatchGameStart>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTargetFinish>(System.Action<TMGame.TMatchGameTargetFinish>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTimeOut>(System.Action<TMGame.TMatchGameTimeOut>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTimePause>(System.Action<TMGame.TMatchGameTimePause>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTriple>(System.Action<TMGame.TMatchGameTriple>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTripleBoost>(System.Action<TMGame.TMatchGameTripleBoost>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTripleBoostChange>(System.Action<TMGame.TMatchGameTripleBoostChange>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTripleBoostHandle>(System.Action<TMGame.TMatchGameTripleBoostHandle>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTripleBuyBoost>(System.Action<TMGame.TMatchGameTripleBuyBoost>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameTryAgain>(System.Action<TMGame.TMatchGameTryAgain>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameUseBoost>(System.Action<TMGame.TMatchGameUseBoost>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchGameWin>(System.Action<TMGame.TMatchGameWin>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchNeedCollectItemEvent>(System.Action<TMGame.TMatchNeedCollectItemEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.TMatchNeedShowOutItemEvent>(System.Action<TMGame.TMatchNeedShowOutItemEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.WindowCloseEvent>(System.Action<TMGame.WindowCloseEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<TMGame.WindowOpenEvent>(System.Action<TMGame.WindowOpenEvent>)
		// DragonPlus.Core.EventBus.Listener DragonPlus.Core.Game.GetMod<ModEvent>().Register<object>(System.Action<object>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventAvatarChange>(System.Action<TMGame.EventAvatarChange>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventBuyEnergyInOutLives>(System.Action<TMGame.EventBuyEnergyInOutLives>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventBuyReviveGiftPackSuccess>(System.Action<TMGame.EventBuyReviveGiftPackSuccess>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventCurrencyChange>(System.Action<TMGame.EventCurrencyChange>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventCurrencyFlyAniEnd>(System.Action<TMGame.EventCurrencyFlyAniEnd>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventEnergyChange>(System.Action<TMGame.EventEnergyChange>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventEnterHome>(System.Action<TMGame.EventEnterHome>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventHideTapPlayTip>(System.Action<TMGame.EventHideTapPlayTip>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventLanguageChange>(System.Action<TMGame.EventLanguageChange>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventMergeBalloonTimeUpdate>(System.Action<TMGame.EventMergeBalloonTimeUpdate>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventMergeFairyStartWork>(System.Action<TMGame.EventMergeFairyStartWork>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventMergeTaskUpdated>(System.Action<TMGame.EventMergeTaskUpdated>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventNewTaskGenerated>(System.Action<TMGame.EventNewTaskGenerated>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventOnApplicationPause>(System.Action<TMGame.EventOnApplicationPause>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventShowTapPlayTip>(System.Action<TMGame.EventShowTapPlayTip>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.EventTaskClaim>(System.Action<TMGame.EventTaskClaim>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.IAPSuccess>(System.Action<TMGame.IAPSuccess>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameItemChangeEvent>(System.Action<TMGame.TMatchGameItemChangeEvent>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameStart>(System.Action<TMGame.TMatchGameStart>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTimePause>(System.Action<TMGame.TMatchGameTimePause>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTriple>(System.Action<TMGame.TMatchGameTriple>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTripleBoost>(System.Action<TMGame.TMatchGameTripleBoost>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTripleBoostChange>(System.Action<TMGame.TMatchGameTripleBoostChange>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTripleBoostHandle>(System.Action<TMGame.TMatchGameTripleBoostHandle>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchGameTripleBuyBoost>(System.Action<TMGame.TMatchGameTripleBuyBoost>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchNeedCollectItemEvent>(System.Action<TMGame.TMatchNeedCollectItemEvent>)
		// bool DragonPlus.Core.Game.GetMod<ModEvent>().UnRegister<TMGame.TMatchNeedShowOutItemEvent>(System.Action<TMGame.TMatchNeedShowOutItemEvent>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventEnterHome>(System.Action<TMGame.EventEnterHome>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventHideElementMergeTip>(System.Action<TMGame.EventHideElementMergeTip>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventLeaveHome>(System.Action<TMGame.EventLeaveHome>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventMergeAreaUnlocked>(System.Action<TMGame.EventMergeAreaUnlocked>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventMergeCellUnlocked>(System.Action<TMGame.EventMergeCellUnlocked>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventMergeOperationFinish>(System.Action<TMGame.EventMergeOperationFinish>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventMergeWorldLoaded>(System.Action<TMGame.EventMergeWorldLoaded>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventObtainNewElements>(System.Action<TMGame.EventObtainNewElements>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.EventUserTouchedMap>(System.Action<TMGame.EventUserTouchedMap>)
		// System.Void DragonPlus.Core.EventListenerHolder.SubscribeEvent<TMGame.TMatchGameWin>(System.Action<TMGame.TMatchGameWin>)
		// System.Void DragonPlus.Core.Log.Error<int,object>(string,int,object)
		// System.Void DragonPlus.Core.Log.Error<object,object>(string,object,object)
		// System.Void DragonPlus.Core.Log.Error<object>(string,object)
		// System.Void DragonPlus.Core.SDK.Install<object>()
		// System.Void DragonPlus.Core.SDKLog.Error<int,object>(string,int,object)
		// System.Void DragonPlus.Core.SDKLog.Error<object,object>(string,object,object)
		// System.Void DragonPlus.Core.SDKLog.Error<object>(string,object)
		// object DragonPlus.Core.SDKUtil.Bind<object>(UnityEngine.Transform,bool)
		// int DragonPlus.Core.SDKUtil.Dequeue<int>(System.Collections.Generic.List<int>)
		// object DragonPlus.Core.SDKUtil.Dequeue<object>(System.Collections.Generic.List<object>)
		// System.Void DragonPlus.Core.SDKUtil.Enqueue<object>(System.Collections.Generic.List<object>,object)
		// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,object>> DragonPlus.Core.SDKUtil.ToListEx<System.Collections.Generic.KeyValuePair<int,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>)
		// System.Void DragonPlus.Network.INetwork.Send<object,object>(object,System.Action<object>,System.Action<DragonU3DSDK.Network.API.Protocol.ErrorCode,string,object>)
		// System.Void DragonPlus.Network.IRemoteRequest.HandleRequest<object,object>(object,System.Action<object>,System.Action<string,string,object>)
		// object DragonPlus.Save.IStorage.Get<object>()
		// TMGame.BIHelper.ItemChangeReasonArgs Newtonsoft.Json.JsonConvert.DeserializeObject<TMGame.BIHelper.ItemChangeReasonArgs>(string)
		// TMGame.BIHelper.ItemChangeReasonArgs Newtonsoft.Json.JsonConvert.DeserializeObject<TMGame.BIHelper.ItemChangeReasonArgs>(string,Newtonsoft.Json.JsonSerializerSettings)
		// TMGame.MergeGroundCellInfoList Newtonsoft.Json.JsonConvert.DeserializeObject<TMGame.MergeGroundCellInfoList>(string)
		// TMGame.MergeGroundCellInfoList Newtonsoft.Json.JsonConvert.DeserializeObject<TMGame.MergeGroundCellInfoList>(string,Newtonsoft.Json.JsonSerializerSettings)
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string)
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string,Newtonsoft.Json.JsonSerializerSettings)
		// object SRF.SRFGameObjectExtensions.GetComponentOrAdd<object>(UnityEngine.GameObject)
		// object System.Activator.CreateInstance<object>()
		// Cysharp.Threading.Tasks.UniTask<byte>[] System.Array.Empty<Cysharp.Threading.Tasks.UniTask<byte>>()
		// object[] System.Array.Empty<object>()
		// bool System.Enum.TryParse<int>(string,bool,int&)
		// bool System.Enum.TryParse<int>(string,int&)
		// UnityEngine.Vector3 System.Linq.Enumerable.Aggregate<int,UnityEngine.Vector3>(System.Collections.Generic.IEnumerable<int>,UnityEngine.Vector3,System.Func<UnityEngine.Vector3,int,UnityEngine.Vector3>)
		// UnityEngine.Vector3 System.Linq.Enumerable.Aggregate<object,UnityEngine.Vector3>(System.Collections.Generic.IEnumerable<object>,UnityEngine.Vector3,System.Func<UnityEngine.Vector3,object,UnityEngine.Vector3>)
		// bool System.Linq.Enumerable.All<int>(System.Collections.Generic.IEnumerable<int>,System.Func<int,bool>)
		// bool System.Linq.Enumerable.Any<int>(System.Collections.Generic.IEnumerable<int>,System.Func<int,bool>)
		// bool System.Linq.Enumerable.Any<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// bool System.Linq.Enumerable.Contains<int>(System.Collections.Generic.IEnumerable<int>,int)
		// bool System.Linq.Enumerable.Contains<int>(System.Collections.Generic.IEnumerable<int>,int,System.Collections.Generic.IEqualityComparer<int>)
		// bool System.Linq.Enumerable.Contains<object>(System.Collections.Generic.IEnumerable<object>,object)
		// bool System.Linq.Enumerable.Contains<object>(System.Collections.Generic.IEnumerable<object>,object,System.Collections.Generic.IEqualityComparer<object>)
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Except<int>(System.Collections.Generic.IEnumerable<int>,System.Collections.Generic.IEnumerable<int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Except<object>(System.Collections.Generic.IEnumerable<object>,System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.ExceptIterator<int>(System.Collections.Generic.IEnumerable<int>,System.Collections.Generic.IEnumerable<int>,System.Collections.Generic.IEqualityComparer<int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.ExceptIterator<object>(System.Collections.Generic.IEnumerable<object>,System.Collections.Generic.IEnumerable<object>,System.Collections.Generic.IEqualityComparer<object>)
		// System.Collections.Generic.KeyValuePair<object,object> System.Linq.Enumerable.First<System.Collections.Generic.KeyValuePair<object,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>)
		// object System.Linq.Enumerable.First<object>(System.Collections.Generic.IEnumerable<object>)
		// int System.Linq.Enumerable.Last<int>(System.Collections.Generic.IEnumerable<int>)
		// object System.Linq.Enumerable.Last<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderBy<object,float>(System.Collections.Generic.IEnumerable<object>,System.Func<object,float>)
		// System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<int,int>> System.Linq.Enumerable.OrderByDescending<System.Collections.Generic.KeyValuePair<int,int>,int>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>,System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>)
		// System.Collections.Generic.IEnumerable<float> System.Linq.Enumerable.Select<object,float>(System.Collections.Generic.IEnumerable<object>,System.Func<object,float>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<int,object>(System.Collections.Generic.IEnumerable<int>,System.Func<int,object>)
		// int System.Linq.Enumerable.Sum<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.Dictionary<int,object> System.Linq.Enumerable.ToDictionary<object,int,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>,System.Func<object,object>,System.Collections.Generic.IEqualityComparer<int>)
		// System.Collections.Generic.Dictionary<int,object> System.Linq.Enumerable.ToDictionary<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,int>> System.Linq.Enumerable.ToList<System.Collections.Generic.KeyValuePair<int,int>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>)
		// System.Collections.Generic.List<float> System.Linq.Enumerable.ToList<float>(System.Collections.Generic.IEnumerable<float>)
		// System.Collections.Generic.List<int> System.Linq.Enumerable.ToList<int>(System.Collections.Generic.IEnumerable<int>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<float> System.Linq.Enumerable.Iterator<object>.Select<float>(System.Func<object,float>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<object>.Select<int>(System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<int>.Select<object>(System.Func<int,object>)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.PlotNodeContentView.<DoEnvDecoration>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.PlotNodeContentView.<DoEnvDecoration>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,UITripleMatchTaskItemView.<BeforeDestroy>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,UITripleMatchTaskItemView.<BeforeDestroy>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.GFsmStateGame.<CreateGame>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.GFsmStateGame.<CreateGame>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,TMGame.GFsmStateGame.<CorrectItemPosition>d__6>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,TMGame.GFsmStateGame.<CorrectItemPosition>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.PlotNodeContentView.<DoEnvDecoration>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.PlotNodeContentView.<DoEnvDecoration>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,UITripleMatchTaskItemView.<BeforeDestroy>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,UITripleMatchTaskItemView.<BeforeDestroy>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.GFsmStateGame.<CreateGame>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.GFsmStateGame.<CreateGame>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,TMGame.GFsmStateGame.<CorrectItemPosition>d__6>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,TMGame.GFsmStateGame.<CorrectItemPosition>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.GFsmStateHome.<PreOnEnter>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.GFsmStateHome.<PreOnEnter>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.GFsmStateGame.<PreOnEnter>d__2>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.GFsmStateGame.<PreOnEnter>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.GameStateBase.<PreOnLeave>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.GameStateBase.<PreOnLeave>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,TMGame.GFsmStateHome.<PreOnEnter>d__2>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,TMGame.GFsmStateHome.<PreOnEnter>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<TMGame.GFsmStateGame.<CorrectItemPosition>d__6>(TMGame.GFsmStateGame.<CorrectItemPosition>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<TMGame.GFsmStateGame.<CreateGame>d__5>(TMGame.GFsmStateGame.<CreateGame>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<TMGame.PlotNodeContentView.<DoEnvDecoration>d__4>(TMGame.PlotNodeContentView.<DoEnvDecoration>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3>(TMGame.PlotNodeUIView.<PlayCostCloverAnimation>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2>(TMGame.UITripleMatchTaskItemView.<BeforeDestroy>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<UITripleMatchTaskItemView.<BeforeDestroy>d__3>(UITripleMatchTaskItemView.<BeforeDestroy>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<TMGame.GFsmStateGame.<PreOnEnter>d__2>(TMGame.GFsmStateGame.<PreOnEnter>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<TMGame.GFsmStateHome.<PreOnEnter>d__2>(TMGame.GFsmStateHome.<PreOnEnter>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<TMGame.GameStateBase.<PreOnEnter>d__4>(TMGame.GameStateBase.<PreOnEnter>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<TMGame.GameStateBase.<PreOnLeave>d__5>(TMGame.GameStateBase.<PreOnLeave>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,GamePlay.MagicPowerOperation.<FlyMagicPowerToBuilding>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,GamePlay.MagicPowerOperation.<FlyMagicPowerToBuilding>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,GamePlay.MapBuilding.<OnBuildingClicked>d__8>(Cysharp.Threading.Tasks.UniTask.Awaiter&,GamePlay.MapBuilding.<OnBuildingClicked>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.BehaviorSequence.<Start>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.BehaviorSequence.<Start>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.IapPurchaseSuccessPopup.<OnContinueButtonClicked>d__9>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.IapPurchaseSuccessPopup.<OnContinueButtonClicked>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeEffectOperation.<GenerateEffect>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeEffectOperation.<GenerateEffect>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeEffectOperation.<GenerateFlyBoxEffect>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeEffectOperation.<GenerateFlyBoxEffect>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeElementBubbleGraphic.<DoIdleAnimation>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeElementBubbleGraphic.<DoIdleAnimation>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeEnergyOperation.<EnergyMoveToTarget>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeEnergyOperation.<EnergyMoveToTarget>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeFairy.<DoWork>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeFairy.<DoWork>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeFairyGraphic.<Talk>d__15>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeFairyGraphic.<Talk>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.MergeTipsOperation.<GenerateTip>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.MergeTipsOperation.<GenerateTip>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.PlotNodeUIView.<OnNodeFinished>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.PlotNodeUIView.<OnNodeFinished>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.StarChestUIWidget.<ClaimStarChestRewards>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.StarChestUIWidget.<ClaimStarChestRewards>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.StarChestUIWidget.<OnEventRewardMail>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.StarChestUIWidget.<OnEventRewardMail>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<>c__DisplayClass49_0.<<OnDeselectEvent>b__0>d>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<>c__DisplayClass49_0.<<OnDeselectEvent>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<>c__DisplayClass50_0.<<ShowTipAndAutoHide>b__3>d>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<>c__DisplayClass50_0.<<ShowTipAndAutoHide>b__3>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<PlayAnimation>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<PlayAnimation>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMUtility.<WaitNFrame>d__5>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMUtility.<WaitNFrame>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<OnTMatchGameStartGoldenHatter>d__176>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<OnTMatchGameStartGoldenHatter>d__176&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<OnWin>d__206>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<OnWin>d__206&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseBroom>d__47>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseBroom>d__47&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseFrozen>d__53>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseFrozen>d__53&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseLevelBoostClock>d__35>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseLevelBoostClock>d__35&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseLevelBoostLighting>d__36>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseLevelBoostLighting>d__36&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseLighting>d__54>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseLighting>d__54&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseMagnet>d__41>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseMagnet>d__41&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.TMatchLogic.<UseWindmill>d__50>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.TMatchLogic.<UseWindmill>d__50&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIChapterItemView.<PlayFinishAnimation>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIChapterItemView.<PlayFinishAnimation>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIChapterItemView.<PlayUnlockAnimation>d__18>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIChapterItemView.<PlayUnlockAnimation>d__18&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UICommonClaimBoxReward.<ClaimOnClick>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UICommonClaimBoxReward.<ClaimOnClick>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UICommonClaimBoxReward.<ContinueOnClick>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UICommonClaimBoxReward.<ContinueOnClick>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIElementMenu.<ShowElementMenu>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIElementMenu.<ShowElementMenu>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIElementMenu.<WaitForHide>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIElementMenu.<WaitForHide>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIEntranceGroup.<UpdateWidgetGroupState>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIEntranceGroup.<UpdateWidgetGroupState>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UILikeUs.<PopCommonReward>d__9>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UILikeUs.<PopCommonReward>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UILoading.<Init>d__15>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UILoading.<Init>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UILoading.<OnAssetsLoadCompleted>d__25>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UILoading.<OnAssetsLoadCompleted>d__25&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIManager.<CloseWindow>d__39>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIManager.<CloseWindow>d__39&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIManager.<OnWindowPrepare>d__41>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIManager.<OnWindowPrepare>d__41&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITransition.<OnUpdate>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITransition.<OnUpdate>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchBottomView.<FlyBoosterItemToTarget>d__21>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchBottomView.<FlyBoosterItemToTarget>d__21&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchBuyBoost.<BuyOnClick>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchBuyBoost.<BuyOnClick>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchLevelRewards.<SettleActivityReward>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchLevelRewards.<SettleActivityReward>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchWinChest.<>c__DisplayClass17_0.<<PlayCollectAnim>b__0>d>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchWinChest.<>c__DisplayClass17_0.<<PlayCollectAnim>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UITripleMatchWinChest.<PlayCollectAnim>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UITripleMatchWinChest.<PlayCollectAnim>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,TMGame.UIWindow.<PlayOpenAni>d__54>(Cysharp.Threading.Tasks.UniTask.Awaiter&,TMGame.UIWindow.<PlayOpenAni>d__54&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,UISubView_PropGroup.<FlyBoosterItemToTarget>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter&,UISubView_PropGroup.<FlyBoosterItemToTarget>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,UIView_UIBuyProps.<BuyOnClick>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,UIView_UIBuyProps.<BuyOnClick>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<byte>,TMGame.MergeFairy.<DoWork>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter<byte>&,TMGame.MergeFairy.<DoWork>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<float>,TMGame.MergeFairySys.<SimulateFairyWork>d__0>(Cysharp.Threading.Tasks.UniTask.Awaiter<float>&,TMGame.MergeFairySys.<SimulateFairyWork>d__0&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,TMGame.UILoading.<OnAssetsLoadCompleted>d__25>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,TMGame.UILoading.<OnAssetsLoadCompleted>d__25&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,TMGame.UITripleMatchCollectItemView.<OnTMatchGameTripleBoost>d__9>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,TMGame.UITripleMatchCollectItemView.<OnTMatchGameTripleBoost>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,UISubView_CollectViewContent.<OnTMatchGameTripleBoost>d__11>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,UISubView_CollectViewContent.<OnTMatchGameTripleBoost>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.ActivityBase.<CheckAndDownloadAssets>d__31>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.ActivityBase.<CheckAndDownloadAssets>d__31&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.PlotNodeUIView.<OnNodeUIClicked>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.PlotNodeUIView.<OnNodeUIClicked>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.StarChestUIWidget.<OnEventRewardMail>d__17>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.StarChestUIWidget.<OnEventRewardMail>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.TMatchItem.<Retract>d__44>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.TMatchItem.<Retract>d__44&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UIGetReward2.<OnContinueButtonClick>d__9>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UIGetReward2.<OnContinueButtonClick>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UIGetReward2.<ShowItemAppear>d__8>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UIGetReward2.<ShowItemAppear>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UIMiniGameSuccess.<FlyCallback>d__6>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UIMiniGameSuccess.<FlyCallback>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UIRemoveAd.<DelayRefreshBuyText>d__16>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UIRemoveAd.<DelayRefreshBuyText>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UITripleMatchLevelChoose.<OnPlayBtnClicked>d__23>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UITripleMatchLevelChoose.<OnPlayBtnClicked>d__23&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UITripleMatchTaskView.<OnTMatchGameTriple>d__7>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UITripleMatchTaskView.<OnTMatchGameTriple>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,TMGame.UserSys.<OnLoginSuccess>d__4>(System.Runtime.CompilerServices.TaskAwaiter&,TMGame.UserSys.<OnLoginSuccess>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,UISubView_TaskGroup.<OnTMatchGameTriple>d__6>(System.Runtime.CompilerServices.TaskAwaiter&,UISubView_TaskGroup.<OnTMatchGameTriple>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,TMGame.GameFsm.<ToState>d__8<object>>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,TMGame.GameFsm.<ToState>d__8<object>&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,TMGame.GFsmStateGame.<BoostGuide>d__7>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,TMGame.GFsmStateGame.<BoostGuide>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,TMGame.UITripleMatchTimeView.<FrozenTime>d__15>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,TMGame.UITripleMatchTimeView.<FrozenTime>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,UISubView_TopGroup.<FrozenTime>d__16>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,UISubView_TopGroup.<FrozenTime>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<GamePlay.MagicPowerOperation.<FlyMagicPowerToBuilding>d__1>(GamePlay.MagicPowerOperation.<FlyMagicPowerToBuilding>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<GamePlay.MapBuilding.<OnBuildingClicked>d__8>(GamePlay.MapBuilding.<OnBuildingClicked>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.ActivityBase.<CheckAndDownloadAssets>d__31>(TMGame.ActivityBase.<CheckAndDownloadAssets>d__31&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.ActivitySys.<<SetLeftTimeFinishCallback>b__20_0>d>(TMGame.ActivitySys.<<SetLeftTimeFinishCallback>b__20_0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.BehaviorSequence.<Start>d__4>(TMGame.BehaviorSequence.<Start>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.FbSys.<>c.<<TryAutoOpenFbLikeReward>b__8_0>d>(TMGame.FbSys.<>c.<<TryAutoOpenFbLikeReward>b__8_0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.GFsmStateGame.<BoostGuide>d__7>(TMGame.GFsmStateGame.<BoostGuide>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.GameFsm.<ToState>d__8<object>>(TMGame.GameFsm.<ToState>d__8<object>&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.IapPurchaseSuccessPopup.<OnContinueButtonClicked>d__9>(TMGame.IapPurchaseSuccessPopup.<OnContinueButtonClicked>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.LobbySequenceSys.<CheckMatchWin>d__13>(TMGame.LobbySequenceSys.<CheckMatchWin>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeEffectOperation.<GenerateEffect>d__12>(TMGame.MergeEffectOperation.<GenerateEffect>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeEffectOperation.<GenerateFlyBoxEffect>d__11>(TMGame.MergeEffectOperation.<GenerateFlyBoxEffect>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeElementBubbleGraphic.<DoIdleAnimation>d__13>(TMGame.MergeElementBubbleGraphic.<DoIdleAnimation>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeEnergyOperation.<EnergyMoveToTarget>d__3>(TMGame.MergeEnergyOperation.<EnergyMoveToTarget>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeFairy.<DoWork>d__11>(TMGame.MergeFairy.<DoWork>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeFairyGraphic.<Talk>d__15>(TMGame.MergeFairyGraphic.<Talk>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeFairySys.<SimulateFairyWork>d__0>(TMGame.MergeFairySys.<SimulateFairyWork>d__0&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.MergeTipsOperation.<GenerateTip>d__4>(TMGame.MergeTipsOperation.<GenerateTip>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.NodeUIItem.<OnButtonClicked>d__17>(TMGame.NodeUIItem.<OnButtonClicked>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.PlotNodeUIView.<OnNodeFinished>d__10>(TMGame.PlotNodeUIView.<OnNodeFinished>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.PlotNodeUIView.<OnNodeUIClicked>d__5>(TMGame.PlotNodeUIView.<OnNodeUIClicked>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.StarChestUIWidget.<ClaimStarChestRewards>d__13>(TMGame.StarChestUIWidget.<ClaimStarChestRewards>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.StarChestUIWidget.<OnEventRewardMail>d__17>(TMGame.StarChestUIWidget.<OnEventRewardMail>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMUtility.<>c__DisplayClass49_0.<<OnDeselectEvent>b__0>d>(TMGame.TMUtility.<>c__DisplayClass49_0.<<OnDeselectEvent>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMUtility.<>c__DisplayClass50_0.<<ShowTipAndAutoHide>b__3>d>(TMGame.TMUtility.<>c__DisplayClass50_0.<<ShowTipAndAutoHide>b__3>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMUtility.<PlayAnimation>d__11>(TMGame.TMUtility.<PlayAnimation>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMUtility.<WaitNFrame>d__5>(TMGame.TMUtility.<WaitNFrame>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchItem.<Retract>d__44>(TMGame.TMatchItem.<Retract>d__44&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<OnFail>d__202>(TMGame.TMatchLogic.<OnFail>d__202&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<OnTMatchGameStartGoldenHatter>d__176>(TMGame.TMatchLogic.<OnTMatchGameStartGoldenHatter>d__176&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<OnWin>d__206>(TMGame.TMatchLogic.<OnWin>d__206&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseBroom>d__47>(TMGame.TMatchLogic.<UseBroom>d__47&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseFrozen>d__53>(TMGame.TMatchLogic.<UseFrozen>d__53&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseLevelBoostClock>d__35>(TMGame.TMatchLogic.<UseLevelBoostClock>d__35&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseLevelBoostLighting>d__36>(TMGame.TMatchLogic.<UseLevelBoostLighting>d__36&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseLighting>d__54>(TMGame.TMatchLogic.<UseLighting>d__54&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseMagnet>d__41>(TMGame.TMatchLogic.<UseMagnet>d__41&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.TMatchLogic.<UseWindmill>d__50>(TMGame.TMatchLogic.<UseWindmill>d__50&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIChapterItemView.<PlayFinishAnimation>d__17>(TMGame.UIChapterItemView.<PlayFinishAnimation>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIChapterItemView.<PlayUnlockAnimation>d__18>(TMGame.UIChapterItemView.<PlayUnlockAnimation>d__18&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UICommonClaimBoxReward.<ClaimOnClick>d__11>(TMGame.UICommonClaimBoxReward.<ClaimOnClick>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UICommonClaimBoxReward.<ContinueOnClick>d__13>(TMGame.UICommonClaimBoxReward.<ContinueOnClick>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIElementMenu.<ShowElementMenu>d__10>(TMGame.UIElementMenu.<ShowElementMenu>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIElementMenu.<WaitForHide>d__17>(TMGame.UIElementMenu.<WaitForHide>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIEmptyEnergy.<OnRefillBtnClicked>d__13>(TMGame.UIEmptyEnergy.<OnRefillBtnClicked>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIEntranceGroup.<UpdateWidgetGroupState>d__11>(TMGame.UIEntranceGroup.<UpdateWidgetGroupState>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIGetReward2.<OnContinueButtonClick>d__9>(TMGame.UIGetReward2.<OnContinueButtonClick>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIGetReward2.<ShowItemAppear>d__8>(TMGame.UIGetReward2.<ShowItemAppear>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UILikeUs.<PopCommonReward>d__9>(TMGame.UILikeUs.<PopCommonReward>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UILoading.<Init>d__15>(TMGame.UILoading.<Init>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UILoading.<OnAssetsLoadCompleted>d__25>(TMGame.UILoading.<OnAssetsLoadCompleted>d__25&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIManager.<CloseWindow>d__39>(TMGame.UIManager.<CloseWindow>d__39&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIManager.<OnWindowPrepare>d__41>(TMGame.UIManager.<OnWindowPrepare>d__41&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIMiniGameSuccess.<FlyCallback>d__6>(TMGame.UIMiniGameSuccess.<FlyCallback>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIRemoveAd.<DelayRefreshBuyText>d__16>(TMGame.UIRemoveAd.<DelayRefreshBuyText>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITransition.<OnUpdate>d__6>(TMGame.UITransition.<OnUpdate>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchBottomView.<FlyBoosterItemToTarget>d__21>(TMGame.UITripleMatchBottomView.<FlyBoosterItemToTarget>d__21&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchBottomView.<RefreshBoost>d__19>(TMGame.UITripleMatchBottomView.<RefreshBoost>d__19&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchBuyBoost.<BuyOnClick>d__10>(TMGame.UITripleMatchBuyBoost.<BuyOnClick>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchCollectItemView.<OnTMatchGameTripleBoost>d__9>(TMGame.UITripleMatchCollectItemView.<OnTMatchGameTripleBoost>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchLevelChoose.<OnPlayBtnClicked>d__23>(TMGame.UITripleMatchLevelChoose.<OnPlayBtnClicked>d__23&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchLevelRewards.<SettleActivityReward>d__13>(TMGame.UITripleMatchLevelRewards.<SettleActivityReward>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchOut.<OnQuitBtnClicked>d__57>(TMGame.UITripleMatchOut.<OnQuitBtnClicked>d__57&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchTaskView.<OnTMatchGameTriple>d__7>(TMGame.UITripleMatchTaskView.<OnTMatchGameTriple>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchTimeView.<FrozenTime>d__15>(TMGame.UITripleMatchTimeView.<FrozenTime>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchWinChest.<>c__DisplayClass17_0.<<PlayCollectAnim>b__0>d>(TMGame.UITripleMatchWinChest.<>c__DisplayClass17_0.<<PlayCollectAnim>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UITripleMatchWinChest.<PlayCollectAnim>d__17>(TMGame.UITripleMatchWinChest.<PlayCollectAnim>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UIWindow.<PlayOpenAni>d__54>(TMGame.UIWindow.<PlayOpenAni>d__54&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UserProfileSys.<OnEventTMatchResultStarRewardExecute>d__3>(TMGame.UserProfileSys.<OnEventTMatchResultStarRewardExecute>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<TMGame.UserSys.<OnLoginSuccess>d__4>(TMGame.UserSys.<OnLoginSuccess>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UISubView_CollectViewContent.<OnTMatchGameTripleBoost>d__11>(UISubView_CollectViewContent.<OnTMatchGameTripleBoost>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UISubView_PropGroup.<FlyBoosterItemToTarget>d__17>(UISubView_PropGroup.<FlyBoosterItemToTarget>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UISubView_PropGroup.<RefreshBoost>d__15>(UISubView_PropGroup.<RefreshBoost>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UISubView_TaskGroup.<OnTMatchGameTriple>d__6>(UISubView_TaskGroup.<OnTMatchGameTriple>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UISubView_TopGroup.<FrozenTime>d__16>(UISubView_TopGroup.<FrozenTime>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UIView_EmptyEnergy.<OnRefillBtnClicked>d__8>(UIView_EmptyEnergy.<OnRefillBtnClicked>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UIView_UIBuyProps.<BuyOnClick>d__3>(UIView_UIBuyProps.<BuyOnClick>d__3&)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// System.Void* System.Runtime.CompilerServices.Unsafe.AsPointer<object>(object&)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInChildren<object>()
		// object UnityEngine.Component.GetComponentInParent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>(bool)
		// bool UnityEngine.EventSystems.ExecuteEvents.Execute<object>(UnityEngine.GameObject,UnityEngine.EventSystems.BaseEventData,UnityEngine.EventSystems.ExecuteEvents.EventFunction<object>)
		// UnityEngine.GameObject UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy<object>(UnityEngine.GameObject,UnityEngine.EventSystems.BaseEventData,UnityEngine.EventSystems.ExecuteEvents.EventFunction<object>)
		// System.Void UnityEngine.EventSystems.ExecuteEvents.GetEventList<object>(UnityEngine.GameObject,System.Collections.Generic.IList<UnityEngine.EventSystems.IEventSystemHandler>)
		// bool UnityEngine.EventSystems.ExecuteEvents.ShouldSendToComponent<object>(UnityEngine.Component)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object UnityEngine.GameObject.GetComponentInParent<object>()
		// object UnityEngine.GameObject.GetComponentInParent<object>(bool)
		// System.Void UnityEngine.GameObject.GetComponents<object>(System.Collections.Generic.List<object>)
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Transform)
		// object UnityEngine.Resources.Load<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.ResourcePackage.LoadAssetAsync<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetAsync<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
	}
}