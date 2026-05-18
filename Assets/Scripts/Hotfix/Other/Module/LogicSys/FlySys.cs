using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DragonPlus;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TMGame
{
    public class FlyTarget
    {
        private static Dictionary<EItemType, List<Transform>> _targets = new Dictionary<EItemType, List<Transform>>();

        public static void AddTarget(EItemType eItemType, Transform transform)
        {
            if (!_targets.ContainsKey(eItemType))
            {
                _targets.Add(eItemType, new List<Transform>());
            }

            _targets[eItemType].Add(transform);
        }

        public static void RemoveTarget(EItemType eItemType, Transform transform)
        {
            if (_targets.ContainsKey(eItemType))
            {
                var targets = _targets[eItemType];
                if (targets.Count > 0)
                    targets.Remove(transform);
            }
        }

        public static Transform GetTarget(int itemId)
        {
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);
            if (_targets.ContainsKey((EItemType)itemCfg.Id))
            {
                var targets = _targets[(EItemType)itemCfg.Id];
                if (targets.Count > 0)
                {
                    return targets[^1];
                }
            }

            Log.Error("TargetNotFound:" + itemId);

            return null;
        }

        public static Transform GetTarget(EItemType eItemType)
        {
            if (_targets.ContainsKey(eItemType))
            {
                var targets = _targets[eItemType];
                if (targets.Count > 0)
                    return targets[^1];
            }

            return null;
        }
    }

    public class FlySys : ModuleBase
    {
        private string CommonFlyItem = "FlyItem";
        private string SettleAnimItem = "FlyAnim";
        private string FlyCountItem = "FlyCount";

        private List<string> _list;

        private bool _isShocking;

        private void InitPool()
        {
            _list = new List<string>()
            {
                CommonFlyItem,
                SettleAnimItem,
                FlyCountItem,
            };
            foreach (var str in _list)
            {
                GameObjectPool.PreLoad(str,2);
            }
        }

        public override void OnInit()
        {
            base.OnInit();
            InitPool();
        }

        // TODO 重启的时候要释放缓存
        // public override void OnShutDown()
        // {
        //     foreach (var str in _list)
        //     {
        //         PoolMgr.Instance.
        //     }
        //     base.OnShutDown();
        // }

        public void FlyItem(int itemId, int itemNum, Vector2 srcPos, Vector2 destPos, Action action,
            bool playCoinAudio = true, float intervalTime = 0.15f, bool refreshInfo = true, float scl = 1.1f)
        {
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);
            int count = Math.Min(itemNum, 10);
            for (int i = 0; i < count; i++)
            {
                var flyObj = GameObjectPool.Get(CommonFlyItem);
                // if ((EItemType)itemCfg.Id == EItemType.WeeklyChallengeBuff)
                // {
                //     var weeklyChallenge = Active.WeekChallenge.Model.Instance.GetCurWeeklyChallengeCfg();
                //     Table_Common_Item collectItemCfg = Game.GetMod<ConfigMgr>()
                //         .GetConfig<Table_Common_Item>(weeklyChallenge.CollectItemId);
                //     atlaspath = collectItemCfg.Atlas;
                //     iconPath = collectItemCfg.Icon;
                // }

                flyObj.transform.Find("Node/UIImg_Icon").GetComponent<Image>().color = Color.white;
                flyObj.transform.SetParent(Game.GetMod<ModUI>().UICanvas.transform, false);
                CoreUtils.SetImg(flyObj.transform.Find("Node/UIImg_Icon").GetComponent<Image>(),itemCfg.Icon);
                flyObj.transform.Find("Node/UIImg_InfiniteTag").gameObject
                    .SetActive(false);
                flyObj.transform.Find("Node/UINode_TagImage").gameObject
                    .SetActive(false);
                if (flyObj.transform.Find("Node/UITxt_NumberText"))
                {
                    flyObj.transform.Find("Node/UITxt_NumberText").gameObject.SetActive(false);
                }

                if ((EItemType)itemCfg.Id == EItemType.Coin)
                {
                    // Game.GetMod<ModAudio>().PlaySound( AudioName.);
                }
                if ((EItemType)itemCfg.Id == EItemType.Key)
                {
                    // Game.GetMod<ModAudio>().PlaySound( AudioName.);
                }

                int index = i;
                FlyObject(flyObj, srcPos, destPos, true, 0.5f, intervalTime * i, () =>
                {
                    PlayHintStarsEffect(destPos);
                    PlayNeedUIEffect((EItemType)itemCfg.Id);
                    if (index == 0 && refreshInfo)
                    {
                        Game.GetMod<ModEvent>().Dispatch(new EvtItemChange((EItemType)itemCfg.Id, itemNum, true, true));
                    }

                    var fx = FlyTarget.GetTarget(itemId).transform.Find("vfx_star");
                    if (fx != null)
                    {
                        fx.gameObject.SetActive(true);
                        var par = fx.transform.Find("star").GetComponent<ParticleSystem>();
                        par.Stop();
                        par.Play();
                    }

                    if ((EItemType)itemCfg.Id == EItemType.Coin)
                    {
                        // Game.GetMod<ModAudio>().PlaySound( AudioName.);
                    }
                    if ((EItemType)itemCfg.Id == EItemType.Key)
                    {
                        // Game.GetMod<ModAudio>().PlaySound( AudioName.);
                    }
                    // 本次全部飞完
                    if (index == count - 1)
                    {
                        action?.Invoke();
                        var itemType = (EItemType)itemCfg.Id;
                        if (itemType == EItemType.Coin || itemType == EItemType.Energy)
                        {
                            Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd(itemType));
                        }

                        if ((itemType == EItemType.Avatar || itemType == EItemType.AvatarFrame) && playCoinAudio &&
                            index == 0)
                        {
                            // Game.GetMod<ModAudio>().PlaySound( AudioName.);
                        }

                        if (itemType == EItemType.EnergyInfinity && index == 0)
                        {
                            Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd(itemType));
                        }
                    }
                }, scl);
            }

            // 飘数量
            var flyCountGo = GameObjectPool.Get(FlyCountItem); 
            flyCountGo.transform.SetParent(Game.GetMod<ModUI>().UICanvas.transform, false);
            var flyCountTMP = flyCountGo.GetComponentInChildren<TextMeshProUGUI>();
            flyCountTMP.color = ColorUtils.SetColorA(flyCountTMP.color, 1);
            flyCountTMP.text = itemNum.ToString();
            var flyCountRectTransform = flyCountGo.GetComponent<RectTransform>();
            flyCountRectTransform.anchoredPosition = new Vector2(flyCountRectTransform.anchoredPosition.x + 100, flyCountRectTransform.anchoredPosition.y);
            float originPosY = flyCountRectTransform.anchoredPosition.y;
            flyCountRectTransform.DOAnchorPosY(originPosY + 200, 2.5f).SetUpdate(true)
                .OnComplete(() => { GameObjectPool.Put(flyCountGo);}); 
            flyCountTMP.DOFade(0, 2.5f).SetUpdate(true);
        }

        private void PlayNeedUIEffect(EItemType eItemType)
        {
            // if (eItemType == EItemType.Broom || eItemType == EItemType.Magnet || eItemType == EItemType.Compass ||
            //     eItemType == EItemType.Frozen)
            // {
            //     var baseMain = Game.Mgr<UIMgr>().FindViewFirst(UIViewName.UIView_TripleMatchMain);
            //     if (baseMain != null)
            //     {
            //         var main = (UIView_TripleMatchMain)baseMain;
            //         main.PlayCollectAnimtion(eItemType);
            //     }
            //     else
            //     {
            //         Game.GetMod<ModEvent>().Dispatch(new EventPlayButtonCollectAnimation());
            //     }
            // }
            // else if (eItemType == EItemType.Clock || eItemType == EItemType.Lightning)
            // {
            //     Game.GetMod<ModEvent>().Dispatch(new EventPlayButtonCollectAnimation());
            // }
        }

        public void FlyItemView(Transform transform, int itemId, int itemNum, Vector2 srcPos, Vector2 destPos,
            Action action, bool playCoinAudio = true)
        {
            Vector3 localPos = Game.GetMod<ModUI>().UICanvas.transform.InverseTransformPoint(srcPos);
            localPos.y -= 20;

            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(itemId);

            // if ((EItemType)itemCfg.Id == EItemType.Coin && playCoinAudio)
            //     Game.Mgr<SoundMgr>().PlaySfx("sfx_reward_fly");

            var flyObj = GameObject.Instantiate(transform.gameObject, Game.GetMod<ModUI>().UICanvas.transform);

            FlyObject(flyObj, srcPos, destPos, true, 0.8f, 0, () =>
            {
                PlayHintStarsEffect(destPos);

                Game.GetMod<ModEvent>().Dispatch(new EvtItemChange((EItemType)itemCfg.Id, itemNum, true));

                action?.Invoke();

                var itemType = (EItemType)itemCfg.Id;
                if (itemType == EItemType.Coin || itemType == EItemType.Energy)
                {
                    Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd(itemType));
                }

                if (itemType == EItemType.EnergyInfinity)
                {
                    Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd(itemType));
                }
            });
        }
        
        public void FlyObject(GameObject flyObj, Vector3 srcPos, Vector3 destPos, bool showEffect, float time = 0.5f,
            float delayTime = 0f, Action action = null, float scale = 1.1f)
        {
            if (!flyObj) return;

            var z = Game.GetMod<ModUI>().UICanvas.transform.position.z;
            srcPos.z = z;

            Vector3 startPos = srcPos;
            startPos.x += Random.Range(-0.5f, 0.5f);
            startPos.y += Random.Range(-0.2f, 0.2f);
            startPos.z = z;

            destPos.z = z;

            flyObj.transform.position = startPos;
            flyObj.transform.localScale = Vector3.zero;

            GameObject efPrefab = null;
            if (showEffect)
            {
                // efPrefab = PoolMgr.Instance.Spawn<GameObject>(CommonTrail);
                // if (efPrefab != null)
                // {
                //     efPrefab.gameObject.SetActive(false);
                //     efPrefab.transform.SetParent(flyObj.transform);
                //     efPrefab.transform.Reset();
                //     efPrefab.gameObject.SetActive(true);
                // }
            }

            Vector3 control = Vector3.zero;
            control.x = startPos.x + 0.3f;
            control.y = startPos.y - 0.3f;
            control.z = z;

            Vector3 control1 = Vector3.MoveTowards(control, destPos, 1);

            Sequence s = DOTween.Sequence();
            s.SetDelay(delayTime);
            s.Append(flyObj.transform.DOScale(new Vector3(scale, scale, scale), 0.3f));
            s.Append(flyObj.transform.DOPath(new[] { startPos, control, control1, destPos }, time, PathType.CatmullRom)
                .SetEase(Ease.InQuart));
            s.OnComplete(() =>
            {
                GameObjectPool.Put(flyObj);
                action?.Invoke();
            });
            s.Play();
        }

        public void NormalFly(GameObject flyObj, Vector3 srcPos, Vector3 destPos, bool showEffect, float time = 0.5f,
            float delayTime = 0f, Action action = null, float scale = 1.1f)
        {
            if (!flyObj) return;

            var z = Game.GetMod<ModUI>().UICanvas.transform.position.z;
            srcPos.z = z;

            Vector3 startPos = srcPos;
            startPos.x += Random.Range(-0.5f, 0.5f);
            startPos.y += Random.Range(-0.2f, 0.2f);
            startPos.z = z;

            destPos.z = z;

            flyObj.transform.position = startPos;
            flyObj.transform.localScale = Vector3.zero;

            GameObject efPrefab = null;
            if (showEffect)
            {
                // efPrefab = PoolMgr.Instance.Spawn<GameObject>(CommonTrail);
                // if (efPrefab != null)
                // {
                //     efPrefab.gameObject.SetActive(false);
                //     efPrefab.transform.SetParent(flyObj.transform);
                //     efPrefab.transform.Reset();
                //     efPrefab.gameObject.SetActive(true);
                // }
            }

            Vector3 control = Vector3.zero;
            control.x = startPos.x + 0.3f;
            control.y = startPos.y - 0.3f;
            control.z = z;

            Vector3 control1 = Vector3.MoveTowards(control, destPos, 1);

            Sequence s = DOTween.Sequence();
            s.SetDelay(delayTime);
            s.Append(flyObj.transform.DOScale(new Vector3(scale, scale, scale), 0.3f));
            s.Append(flyObj.transform.DOPath(new[] { startPos, control, control1, destPos }, time, PathType.CatmullRom)
                .SetEase(Ease.InQuart));
            s.OnComplete(() =>
            {
                //if (efPrefab != null) PoolMgr.Instance.DeSpawn(CommonTrail, efPrefab);
                action?.Invoke();
            });
            s.Play();
        }

        // public GameObject PlayNumEffect(Vector3 localPosition, string num)
        // {
        //     var efPrefab = PoolMgr.Instance.Spawn<GameObject>(CommonNum);
        //     if (efPrefab == null) return null;
        //     efPrefab.gameObject.SetActive(false);
        //     efPrefab.transform.SetParent(Game.Mgr<UIMgr>().UICanvas.transform);
        //     efPrefab.transform.Reset();
        //     efPrefab.gameObject.SetActive(true);
        //
        //     LocalizeTextMeshProUGUI addText =
        //         efPrefab.transform.Find("Root/UIAdd").GetComponent<LocalizeTextMeshProUGUI>();
        //     if (addText != null)
        //     {
        //         addText.SetTerm(num.ToString());
        //         localPosition.z = Game.Mgr<UIMgr>().UICanvas.transform.position.z;
        //         efPrefab.transform.localPosition = localPosition;
        //     }
        //
        //     TMUtility.WaitSeconds(2, () =>
        //     {
        //         if (efPrefab != null) PoolMgr.Instance.DeSpawn(CommonNum, efPrefab);
        //     }).Forget();
        //
        //     return efPrefab;
        // }

        public void PlayHintStarsEffect(Vector2 position)
        {
            // var efPrefab = PoolMgr.Instance.Spawn<GameObject>(CommonHintStars);
            // if (efPrefab == null) return;
            // efPrefab.gameObject.SetActive(false);
            // efPrefab.transform.SetParent(Game.Mgr<UIMgr>().UICanvas.transform);
            // efPrefab.transform.position = position;
            // var localPos = efPrefab.transform.localPosition;
            // localPos.z = 0;
            // efPrefab.transform.localPosition = localPos;
            // efPrefab.transform.localScale = Vector3.one;
            // efPrefab.gameObject.SetActive(true);
            //
            // TMUtility.WaitSeconds(0.5f, () =>
            // {
            //     if (efPrefab != null) PoolMgr.Instance.DeSpawn(CommonHintStars, efPrefab);
            // }).Forget();
        }

        public enum ControlType
        {
            Left,
            Right,
        }

        public static Vector2 GetControlType(Vector3 source, Vector3 target, ControlType controlType)
        {
            var vDis = target - source;
            var control = Vector2.zero;

            var direction = 1;
            switch (controlType)
            {
                case ControlType.Left:
                    direction = 1;
                    break;

                case ControlType.Right:
                    direction = -1;
                    break;
            }

            control.x = source.x + vDis.x / 2;
            control.y = source.y + vDis.y / 2f;

            var normal = new Vector2(vDis.y, -vDis.x);
            control = Vector3.MoveTowards(control, normal, direction * 1);

            return control;
        }

        public float Fly(Transform srcTransform, Transform destTransform, GameObject flyPrefab, GameObject hitPrefab,
            Transform parent, Action cb,
            int objCount = 3, float duration = 0.5f, int flyType = 0, bool needInstantiate = true, float scaleFrom = 1f,
            float scaleTo = 1f, bool destory = true)
        {
            var vDis = destTransform.position - srcTransform.position;
            var controlPos = GetControlType(srcTransform.position, destTransform.position, ControlType.Left);

            return FlyTransform(srcTransform, destTransform, controlPos, flyPrefab, hitPrefab, parent, cb, objCount,
                duration, needInstantiate, scaleFrom, scaleTo, destory);
        }

        public struct FlyData
        {
            public int objCount;
            public float duration;
            public bool scaleAnimate; //飞行中缩放到目标大小
            public float scaleTo;
            public ControlType controlType;
            public Transform targetTF;

            private static FlyData _defaultData = new FlyData(3, 0.5f, false, 1f, ControlType.Left);

            public static FlyData defaultOne
            {
                get => _defaultData;
            }

            public FlyData(int objCount, float duration, bool scaleAnimate, float scaleTo, ControlType controlType,
                Transform target = null)
            {
                this.objCount = objCount;
                this.duration = duration;
                this.scaleAnimate = scaleAnimate;
                this.scaleTo = scaleTo;
                this.controlType = controlType;
                this.targetTF = target;
            }
        }

        public float FlyToTargetPos(Vector3 srcPos, Vector3 targetPos, GameObject flyPrefab,
            GameObject attachEffect, Transform parent,
            FlyData data, Action<int> callback,
            float delayCallback = -1, float delayDestroy = 0)
        {
            var control = GetControlType(srcPos, targetPos, data.controlType);

            var flyDeltaDuration = 0.1f;

            for (var i = 0; i < data.objCount; i++)
            {
                var time = data.duration + flyDeltaDuration * i;

                //创建飞行物体
                var flyClone = GameObject.Instantiate(flyPrefab, Vector3.zero, Quaternion.identity, (Transform)parent);

                var canvas = flyClone.transform.Bind<Canvas>(true);
                canvas.overrideSorting = true;
                canvas.sortingLayerName = "UI";
                canvas.sortingOrder = 20;

                flyClone.transform.position = srcPos;
                flyClone.SetActive(true);
                if (attachEffect != null)
                {
                    var effect = GameObject.Instantiate(attachEffect, Vector3.zero, Quaternion.identity,
                        flyClone.transform);
                    effect.transform.localPosition = Vector3.zero;
                }

#if UNITY_EDITOR
                flyClone.name = "FLY_OBJ";
#endif

                //缩放
                if (data.scaleAnimate)
                {
                    flyClone.transform.DOScale(Vector3.one * 1.5f, 0.4f).OnComplete(() => { flyClone.transform.DOScale(Vector3.one * data.scaleTo, time - 0.4f); });
                }

                flyClone.transform.DOLocalRotate(new Vector3(0, 0, 360), time, RotateMode.FastBeyond360);
                //飞行
                var flyUnit = new FlyUnit();
                var flyIndex = i;

                flyUnit.Start(srcPos, targetPos, control, flyClone.transform, time, 0, () =>
                {
                    if (delayDestroy > 0)
                    {
                        CoreUtils.WaitSeconds(delayDestroy, () => { GameObject.Destroy(flyClone); }).Forget();
                    }
                    else
                    {
                        GameObject.Destroy(flyClone);
                    }

                    CoreUtils.WaitSeconds(delayCallback, () => { callback.Invoke(flyIndex); }).Forget();
                });
            }

            var totalDuration = data.duration + flyDeltaDuration * data.objCount;
            return totalDuration;
        }

        private float FlyTransform(Transform sourceTransform, Transform targetPos, Vector2 controlTransform,
            GameObject flyObj, GameObject hitObj, Transform parent, Action cb,
            int objCount = 3, float duration = 0.5f, bool needInstantiate = true, float scaleFrom = 1f,
            float scaleTo = 1f, bool destoty = false)
        {
            var soundEffectPlayed = false;
            var flyDeltaDuration = 0.15f;
            for (var i = 0; i < objCount; i++)
            {
                var time = duration + flyDeltaDuration * i;
                var flyClone = needInstantiate ? GameObject.Instantiate(flyObj, parent) : flyObj;
                // _flyObjs.Add(flyClone);
                flyClone.transform.position = sourceTransform.position;
                flyClone.gameObject.SetActive(true);

                //缩放
                flyClone.transform.localScale = Vector3.one * scaleFrom;
                flyClone.transform.DOScale(Vector3.one * scaleTo, duration);

                //飞行
                var effect = flyClone.AddComponent<FlyTransform>();
                // _flyAnimationObjs.Add(effect);
                Action secondAction = null;
                if (i == objCount - 1)
                {
                    secondAction = () => cb?.Invoke();
                }

                effect.InitData(sourceTransform, controlTransform, targetPos, time, 0, () =>
                {
                    if (destoty)
                    {
                        GameObject.Destroy(flyClone);
                    }
                    else
                    {
                        Component.Destroy(effect);
                    }

                    if (!soundEffectPlayed)
                    {
                        soundEffectPlayed = true;
                        // AudioSysManager.Instance.PlaySound(SfxNameConst.add_coins, 0.3f); //音量对安卓平台无效
                    }

                    if (hitObj != null)
                    {
                        var hitClone = GameObject.Instantiate(hitObj, parent);
                        // _flyObjs.Add(hitClone);
                        hitClone.transform.position = targetPos.position;
                        hitClone.transform.DOScale(1, 0.5f).OnComplete(() =>
                        {
                            if (destoty)
                            {
                                GameObject.Destroy(hitClone);
                            }
                            else
                            {
                                Component.Destroy(effect);
                            }

                            secondAction?.Invoke();
                        });
                    }
                    else
                    {
                        secondAction?.Invoke();
                    }
                });
            }

            return duration + flyDeltaDuration * objCount;
        }

        public async UniTask FlyRewardItems(List<ItemData> items, bool hideHomeWidget = true)
        {
            //Game.Mgr<UIMgr>().SetEventSystemEnable(false,this);
            // if (hideHomeWidget)
            //     Game.GetMod<ModEvent>().Dispatch(new EventToggleHomeUIState(false));
            GameUtils.SetEventSystemEnable(false);
            var viewBase = Game.GetMod<ModUI>().FindView(UIViewName.UIView_HomeMain);
            if (viewBase == null) return;
            var boosterItem = new List<ItemData>();
            var coinAmount = 0;
            var keyAmount = 0;
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].id == (int)EItemType.Coin)
                {
                    coinAmount += items[i].amount;
                }
                else if (items[i].id == (int)EItemType.Key)
                {
                    keyAmount += items[i].amount;
                }
                else
                {
                    boosterItem.Add(items[i]);
                }
            }
            if (coinAmount > 0)
            {
                var flyCoinAmount = Mathf.Min(coinAmount, 10);
                FlyItem((int)EItemType.Coin, coinAmount, Vector2.zero,
                    FlyTarget.GetTarget((int)EItemType.Coin).position, null);
                await CoreUtils.WaitSeconds(flyCoinAmount * 0.15f, true);
            }
            if (keyAmount > 0)
            {
                var flyCoinAmount = Mathf.Min(keyAmount, 10);
                FlyItem((int)EItemType.Key, keyAmount, Vector2.zero,
                    FlyTarget.GetTarget((int)EItemType.Key).position, null);
                await CoreUtils.WaitSeconds(flyCoinAmount * 0.15f, true);
            }
            var listPos = GetRewardPos(boosterItem);
            for (int i = 0; i < boosterItem.Count; i++)
            {
                var startPos = listPos[i];
                PlaySettleRewardAnimationForBooster(boosterItem[i], startPos, i + 1, false);

                await CoreUtils.WaitSeconds(0.15f, true);
            }

            await CoreUtils.WaitSeconds((float)37 / 30, true);
            GameUtils.SetEventSystemEnable(true);
        }

        public List<Vector3> GetRewardPos(List<ItemData> itemList)
        {
            if (itemList == null || itemList.Count <= 0) return null;
            float itemSize = 1f;
            float offsetY = -1.5f;
            var list = new List<Vector3>();
            for (int i = 0; i < itemList.Count; i++)
            {
                float offsetX = 0;
                if (i % 3 == 0)
                {
                    offsetX = 0;
                }
                else if (i % 3 == 1)
                {
                    offsetX = itemSize * -1;
                }
                else
                {
                    offsetX = itemSize * 1;
                }

                var pos = new Vector3(offsetX, offsetY, 0);
                list.Add(pos);
                if (i % 3 == 0 || i % 3 == 2)
                {
                    offsetY += (itemSize / 2);
                }
            }

            return list;
        }

        public static List<Vector3> GetCatmullRomFlyPath(Vector3 startPos, Vector3 endPos, float curveRatio = 0.1f)
        {
            var sourcePos = startPos;
            var targetPos = endPos;
            sourcePos.z = 0;
            targetPos.z = 0;
            var distance = (sourcePos - targetPos).magnitude;
            var middlePoint = (sourcePos + targetPos) / 2;

            var vec = targetPos - sourcePos;
            //closewise
            var perpendicularVector = new Vector2(-vec.y, vec.x).normalized;
            if (sourcePos.x > targetPos.x)
            {
                //CounterClockwise
                perpendicularVector = new Vector2(vec.y, -vec.x).normalized;
            }

            middlePoint += new Vector3(perpendicularVector.x * distance * curveRatio,
                perpendicularVector.y * distance * curveRatio, 0);

            middlePoint.z = (endPos.z + startPos.z) / 2;

            return new List<Vector3>() { startPos, middlePoint, endPos };
        }

        public void PlaySettleRewardAnimationForBooster(ItemData tableGameItem, Vector3 spawnPos,
            int orderOffset,
            bool playNumEffect)
        {
            var boosterItem = GameObjectPool.Get(SettleAnimItem);

            boosterItem.transform.SetParent(Game.GetMod<ModUI>().UICanvas.transform, false);
            var uiNodeCountless = boosterItem.transform.Find("Node/UINode_Countless");
            var uIImgInfiniteTag = boosterItem.transform.Find("Node/UIImg_InfiniteTag");
            var uINodeTagImage = boosterItem.transform.Find("Node/UINode_TagImage");
            var uITxtNumberText = boosterItem.transform.Find("Node/UITxt_NumberText");
            var uINodeDouble = boosterItem.transform.Find("Node/UINode_Double");
            var uINodeIcon = boosterItem.transform.Find("Node/UIImg_Icon");
            var localizeTextMeshProUGUI = uINodeTagImage.transform.Find("UITxt_TagText").GetComponent<LocalizeTextMeshProUGUI>();
            var itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(tableGameItem.id);
            uIImgInfiniteTag.gameObject.SetActive(false);
            uITxtNumberText.gameObject.SetActive(false);
            uINodeTagImage.gameObject.SetActive(true);
            uiNodeCountless.gameObject.SetActive(false);
            if (tableGameItem.id == (int)EItemType.WeeklyChallengeBuff)
            {
                var info = Game.GetMod<ModConfig>()
                    .GetConfig<Table_Common_Item>((int)EItemType.WeeklyChallengeCollect);
                uINodeDouble.gameObject.SetActive(true);
                uINodeTagImage.gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                uINodeDouble.gameObject.SetActive(false);
                uINodeTagImage.gameObject.GetComponent<Image>().enabled = false;
            }

            CoreUtils.SetImg(uINodeIcon.GetComponent<Image>(), itemCfg.Icon);
            boosterItem.transform.position = spawnPos;
            boosterItem.transform.GetComponent<Canvas>().sortingOrder += orderOffset;
            var animator = boosterItem.GetComponent<Animator>();
            animator.Play("Appear");
            //Game.Mgr<SoundMgr>().PlaySfx("sfx_booster_get_1");
            if (itemCfg.IsTimeLimitItem)
            {
                string textValue = TimeUtils.FormatTime((long)(tableGameItem.amount ));
                localizeTextMeshProUGUI.SetText(textValue);
            }
            else
            {
                string textValue = tableGameItem.id != (int)EItemType.Coin
                    ? "x" + tableGameItem.amount
                    : tableGameItem.amount.ToString();
                localizeTextMeshProUGUI.SetText(textValue);
            }

            CoreUtils.WaitSeconds((float)30 / 30, () =>
            {
                var targetTransform = FlyTarget.GetTarget(tableGameItem.id);
                var startPos = spawnPos;
                var destPos = targetTransform.position;
                Vector3 control = Vector3.zero;
                control.x = startPos.x + 0.3f;
                control.y = startPos.y - 0.3f;
                control.z = startPos.z;

                Vector3 control1 = Vector3.MoveTowards(control, destPos, 1);

                boosterItem.transform.DOPath(new[] { startPos, control, control1, destPos }, 0.3333f,
                        PathType.CatmullRom)
                    .OnComplete(
                        () =>
                        {
                            if (tableGameItem.id != (int)EItemType.EnergyInfinity
                                && tableGameItem.id != (int)EItemType.Energy)
                            {
                                var playButtonAnimator = targetTransform.GetComponentInChildren<Animator>();
                                if (playButtonAnimator)
                                {
                                    playButtonAnimator.Play("shake", 0, 0);
                                }
                                var fx = targetTransform.transform.Find("Root/vfx_star");
                                if (fx != null)
                                {
                                    fx.gameObject.SetActive(true);
                                    var par = fx.transform.Find("star").GetComponent<ParticleSystem>();
                                    par.Stop();
                                    par.Play();
                                }
                            }
                            else
                            {
                                if (targetTransform.parent.Find("Normal"))
                                {
                                    var hitAnimator = targetTransform.parent.Find("Normal").GetComponent<Animator>();
                                    hitAnimator.Play("Appear");
                                }

                                var fx = targetTransform.transform.Find("Root/vfx_star");
                                if (fx != null)
                                {
                                    fx.gameObject.SetActive(true);
                                    var par = fx.transform.Find("star").GetComponent<ParticleSystem>();
                                    par.Stop();
                                    par.Play();
                                }
                                Game.GetMod<ModEvent>().Dispatch(new EventCurrencyFlyAniEnd((EItemType)tableGameItem.id));
                            }

                            GameObjectPool.Put(boosterItem);
                        });
            }).Forget();
        }
    }
}