// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/06/29/14:05
// Ver : 1.0.0
// Description : GuideSys.cs
// ChangeLog :
// **********************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DragonPlus.Ad.Max.Tracking;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using UnityEngine;

namespace TMGame
{
    public class GuideSys : ModuleBase
    {
        private Dictionary<string, string> _cacheGuideFinished;
        private Dictionary<GuideTargetType, Dictionary<string, Transform>> _targetCacheDic;
        private Dictionary<string, Func<bool>> _conditionHandlerDic;

        private Table_Common_Guide _currentConfig;

        private const string TargetDefaultKey = "Empty";

        public string PreTMatchGuideItemOrderName2D = "";
        public int PreTMatchGuideItemLayer = -1;
        public bool CloseAllGuide;

        public override void OnInit()
        {
            _cacheGuideFinished = new Dictionary<string, string>();
            _targetCacheDic = new Dictionary<GuideTargetType, Dictionary<string, Transform>>();
            _conditionHandlerDic = new Dictionary<string, Func<bool>>();
            CloseAllGuide = false;
            base.OnInit();
        }

        /// <param name="position"></param>
        /// <param name="param"></param>
        public bool Trigger(GuideTrigger position, string param)
        {
            if (CloseAllGuide)
            {
                return false;
            }

            if (IsShowingGuide())
                return false;
            CLog.Warning("GuideTrigger:" + position.ToString());
            return Trigger(FindMatchConfig(position, param));
        }

        private bool Trigger(Table_Common_Guide config)
        {
            try
            {
                if (config == null)
                {
                    return false;
                }

                _currentConfig = config;

                SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuidingId = _currentConfig.Id;

                if (_currentConfig.TargetTypes != null
                    && _currentConfig.TargetTypes.Count > 0
                    && IsActionTargetType((GuideTargetType)_currentConfig.TargetTypes[0]))
                {
                    TriggerAction((GuideTargetType)_currentConfig.TargetTypes[0]);
                }
                else
                {
                    SDKUtil.Unity.StartCoroutine(ShowGuide());
                }

                // BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFunnelGuideStart,
                // _currentConfig.Id.ToString());
                return true;
            }
            catch (Exception e)
            {
                Log.Error("GuideSubSystem exception:" + e.StackTrace);
            }

            return false;
        }

        private bool IsActionTargetType(GuideTargetType guideTargetType)
        {
            // var actionTargetType = new[] { GuideTargetType.EnterASMR, GuideTargetType.MiniGameEnterComplete };
            // return actionTargetType.Contains(guideTargetType);
            return false;
        }

        private void TriggerAction(GuideTargetType guideTargetType)
        {
        }

        IEnumerator ShowGuide()
        {
            if (_currentConfig == null)
            {
                yield break;
            }

            Transform target = null;
            if (_currentConfig is { TargetTypes: not null })
            {
                var targetCount = _currentConfig.TargetParams?.Count ?? 1;

                for (int i = 0; i < targetCount; i++)
                {
                    var targetType = _currentConfig.TargetTypes[i];
                    while (_currentConfig != null && (target == null ||
                                                      (target != null && target.gameObject != null &&
                                                       !target.gameObject.activeSelf)))
                    {
                        yield return new WaitForEndOfFrame();
                        yield return new WaitForEndOfFrame();
                        if (_currentConfig == null) break;
                        try
                        {
                            if (_targetCacheDic != null &&
                                _targetCacheDic.TryGetValue((GuideTargetType)targetType, out var typeDic))
                            {
                                var targetParam = _currentConfig.TargetParams != null
                                    ? _currentConfig.TargetParams[i]
                                    : TargetDefaultKey;
                                typeDic.TryGetValue(targetParam, out target);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e.ToString());
                        }
                    }
                }
            }

            if (_currentConfig == null)
                yield break;

            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFunnelGuideStart,
                _currentConfig.Id.ToString());

            if (_currentConfig.CameraFocus)
            {
                FocusCamera(_currentConfig.CameraFocusPos,
                    () => { Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_GuideMain, _currentConfig); }).Forget();
            }
            else
            {
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_GuideMain, _currentConfig);
            }
        }

        private async UniTask FocusCamera(string posStr, Action callback)
        {
            try
            {
                if (string.IsNullOrEmpty(posStr)) return;
                var array = posStr.Split(",");
                if (array.Length != 3) return;
                float time = 0.3f;
                //MergeWorld.Instance.MergeControllerIns.LockTouch(true);
                GameUtils.SetEventSystemEnable(false);
                Vector3 targetPos = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
                //MergeWorld.Instance.MergeControllerIns.CameraMove(targetPos, time);
                //MergeWorld.Instance.MergeControllerIns.CameraZoom(10, time);
                await UniTask.WaitForSeconds(time);
                //MergeWorld.Instance.MergeControllerIns.LockTouch(false);
                GameUtils.SetEventSystemEnable(true);
                callback?.Invoke();
            }
            catch (Exception e)
            {
                //MergeWorld.Instance.MergeControllerIns.LockTouch(false);
                GameUtils.SetEventSystemEnable(true);
                callback?.Invoke();
            }
        }

        private Table_Common_Guide FindMatchConfig(GuideTrigger position, string param)
        {
            var guideList = GetGuidesByPosition((int)position);

            if (guideList == null)
            {
                return null;
            }

            var matchList = guideList.FindAll(c =>
                c.TriggerPosition == (int)position &&
                (string.IsNullOrEmpty(param) || c.TriggerParam == "*" || c.TriggerParam == param));

            matchList.Sort((a, b) =>
            {
                var aCon = !string.IsNullOrEmpty(a.TriggerCondition);
                var bCon = !string.IsNullOrEmpty(b.TriggerCondition);
                if (aCon != bCon)
                {
                    //有触发条件的优先
                    return aCon ? -1 : 1;
                }

                return a.Id - b.Id;
            });

            for (int i = 0; i < matchList.Count; i++)
            {
                if (matchList[i].IgnoreCache)
                {
                    return matchList[i];
                }

                if (!IsFinished(matchList[i].GuideId) && IsConditionMatch(matchList[i].TriggerCondition) &&
                    IsPreGuideConditionMatch(matchList[i].PreGuideId))
                {
                    return matchList[i];
                }
            }

            return null;
        }

        private bool IsConditionMatch(string triggerCondition)
        {
            if (string.IsNullOrEmpty(triggerCondition)) return true; //无条件
            return _conditionHandlerDic.ContainsKey(triggerCondition) &&
                   _conditionHandlerDic[triggerCondition].Invoke();
        }

        private bool IsPreGuideConditionMatch(string preGuidId)
        {
            return string.IsNullOrEmpty(preGuidId) || IsFinished(preGuidId);
        }

        public bool IsShowingGuide()
        {
            if (_currentConfig == null) return false;
            if (Game.GetMod<ModUI>().FindView(UIViewName.UIView_GuideMain) == null)
                return false;

            return true;
        }

        public void RegisterTarget(GuideTargetType targetType, Transform targetTransform, string targetParam = null)
        {
            if (string.IsNullOrEmpty(targetParam))
            {
                targetParam = TargetDefaultKey;
            }

            if (_targetCacheDic.ContainsKey(targetType))
            {
                if (_targetCacheDic[targetType].ContainsKey(targetParam))
                {
                    _targetCacheDic[targetType][targetParam] = targetTransform;
                }
                else
                {
                    _targetCacheDic[targetType].Add(targetParam, targetTransform);
                }
            }
            else
            {
                var temp = new Dictionary<string, Transform> { { targetParam, targetTransform } };
                _targetCacheDic.Add(targetType, temp);
            }
        }

        private Dictionary<int, List<Table_Common_Guide>> tableGuidesMap;

        private List<Table_Common_Guide> GetGuidesByPosition(int position)
        {
            if (tableGuidesMap == null)
            {
                tableGuidesMap = new Dictionary<int, List<Table_Common_Guide>>();
                var guideList = Game.GetMod<ModConfig>().GetConfigs<Table_Common_Guide>();
                guideList.ForEach(a =>
                {
                    if (!tableGuidesMap.ContainsKey(a.TriggerPosition))
                        tableGuidesMap[a.TriggerPosition] = new List<Table_Common_Guide>();

                    var guides = tableGuidesMap[a.TriggerPosition];
                    guides.Add(a);
                });
            }

            if (!tableGuidesMap.ContainsKey(position))
                return null;

            return tableGuidesMap[position];
        }

        public void RemoveRegisterTarget(GuideTargetType targetType)
        {
            if (_targetCacheDic.ContainsKey(targetType))
            {
                _targetCacheDic.Remove(targetType);
            }
        }

        public void FinishCurrent(GuideTargetType sourceTarget)
        {
            if (_currentConfig != null)
            {
                SDK<IMaxTracking>.Instance.TrackTutorialComplete();

                //检测目标是否为当前target
                if (sourceTarget != GuideTargetType.None && _currentConfig.TargetTypes != null &&
                    sourceTarget != (GuideTargetType)_currentConfig.TargetTypes[0]) return;

                var tempCfg = _currentConfig;
                _currentConfig = null;
                if (tempCfg.HasGuideCompleteDisplayTip)
                {
                    ShowGuideCompleteTip(tempCfg).Forget();
                }
                else
                {
                    // 有可能开启下一个guide，所以 之前就要把 当前guide清空
                    SetFinished(tempCfg);
                    Game.GetMod<ModUI>().Close(UIViewName.UIView_GuideMain);
                    TryTriggerNextGuide(tempCfg);
                }


                Game.GetMod<ModEvent>().Dispatch<FinishGuideEvent>(new FinishGuideEvent(tempCfg.GuideId));
                // Debug.LogError($"Guide----------FinishCurrent---------{tempCfg.GuideId}-----0000--------");
            }
            else
            {
                Game.GetMod<ModUI>().Close(UIViewName.UIView_GuideMain);
            }
        }

        private async UniTask ShowGuideCompleteTip(Table_Common_Guide cfg)
        {
            try
            {
                SetFinished(cfg);
                Game.GetMod<ModEvent>().Dispatch(new EventTryShowGuideTip());
                await UniTask.WaitForSeconds(cfg.GuideCompleteDisplayTipTime);
                Game.GetMod<ModUI>().Close(UIViewName.UIView_GuideMain);
                TryTriggerNextGuide(cfg);
            }
            catch (Exception e)
            {
                Game.GetMod<ModUI>().Close(UIViewName.UIView_GuideMain);
            }
        }

        private void SaveFinish(Table_Common_Guide config)
        {
            var storage = SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuideFinished;
            if (storage != null)
            {
                if (!storage.ContainsKey(config.GuideId))
                {
                    storage.Add(config.GuideId, config.GuideId);
                }

                if (!string.IsNullOrEmpty(config.SaveGroup))
                {
                    var saveGroups = config.SaveGroup.Split(',');
                    foreach (var childGuildId in saveGroups)
                    {
                        if (!storage.ContainsKey(childGuildId))
                        {
                            storage.Add(childGuildId, childGuildId);
                        }
                    }
                }
            }
        }

        public void SetFinished(Table_Common_Guide config)
        {
            if (config == null) return;
            var guideId = config.GuideId;
            Log.Info($"{GetType()} finish guide id = {guideId}.");
            SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuidingId = -1;

            if (!config.IgnoreCache && !_cacheGuideFinished.ContainsKey(guideId))
            {
                _cacheGuideFinished.Add(guideId, guideId);
            }

            //真正保存进度
            if (config.SaveFlag)
            {
                SaveFinish(config);
            }

            BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventFunnelGuideEnd,
                config.Id.ToString());
        }

        private void TryTriggerNextGuide(Table_Common_Guide config)
        {
            if (config == null) return;
            //多个引导完成都可以触发下一个引导的话，通过SaveGroup做为参数
            Trigger(GuideTrigger.GuideEnd, string.IsNullOrEmpty(config.SaveGroup) ? config.GuideId : config.SaveGroup);
        }

        public bool IsFinished(string guideId, bool checkSave = true)
        {
            if (_cacheGuideFinished.ContainsKey(guideId)) return true;

            var storage = SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuideFinished;
            return storage != null && storage.ContainsKey(guideId);
        }

        public void ClearCacheFinishedGuide()
        {
            _cacheGuideFinished.Clear();
        }

        public override void OnDispose()
        {
            base.OnDispose();
            _cacheGuideFinished.Clear();
            _targetCacheDic.Clear();
            _conditionHandlerDic.Clear();
            _currentConfig = null;
        }

        public void ClearCacheFinishedGuide(string guideId)
        {
            if (_cacheGuideFinished.ContainsKey(guideId))
            {
                _cacheGuideFinished.Remove(guideId);
            }
        }

        public Transform GetTarget(GuideTargetType targetType, string param)
        {
            if (!_targetCacheDic.ContainsKey(targetType)) return null;
            if (string.IsNullOrEmpty(param))
            {
                return _targetCacheDic[targetType][TargetDefaultKey];
            }
            else
            {
                if (_targetCacheDic[targetType].ContainsKey(param))
                {
                    return _targetCacheDic[targetType][param];
                }
            }

            return null;
        }

        public List<Transform> GetTarget(List<int> targetType, List<string> param)
        {
            var target = new List<Transform>();
            for (int i = 0; i < targetType.Count; i++)
            {
                var info = (GuideTargetType)targetType[i];
                var paramStr = param == null ? TargetDefaultKey : param[i];
                paramStr = string.IsNullOrEmpty(paramStr) ? TargetDefaultKey : paramStr;
                if (_targetCacheDic.ContainsKey(info))
                {
                    if (string.IsNullOrEmpty(paramStr))
                    {
                        target.Add(_targetCacheDic[info][TargetDefaultKey]);
                    }
                    else
                    {
                        if (_targetCacheDic[info].ContainsKey(paramStr))
                        {
                            target.Add(_targetCacheDic[info][paramStr]);
                        }
                    }
                }
            }

            return target;
        }

        public string GetCurGuideId()
        {
            return _currentConfig != null ? _currentConfig.GuideId : "";
        }

        public Table_Common_Guide GetCurGuide()
        {
            return _currentConfig;
        }

        public bool IsForceSelectHighLightItem_3D()
        {
            var result = false;
            if (this.IsShowingGuide())
            {
                if (_currentConfig != null)
                {
                    result = _currentConfig.MaskShape == (int)GuideMaskShape.HighLight;
                }
            }

            return result;
        }
    }
}