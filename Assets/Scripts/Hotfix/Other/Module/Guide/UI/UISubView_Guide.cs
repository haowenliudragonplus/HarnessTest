using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using TMGame;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_Guide : UISubView_GuideBase
{
    // private Image _guideMaskLayer;
    private Table_Common_Guide _tableGameGuide;

    private List<Transform> target;
    private TutorialMask mask;

    protected override void OnCreate()
    {
        base.OnCreate();
        _tableGameGuide = ViewData as Table_Common_Guide;
        // _guideMaskLayer = go.transform.Find("GuideMaskLayer");
        // _guideMaskLayer.enabled = _tableGameGuide.MaskEnable;
        // string shaderName =(GuideMaskShape)_tableGameGuide.MaskShape == GuideMaskShape.Rectangle ? "TutorialRectangleMask" : "TutorialCircleMask";
        // var mShader = Game.GetMod<ResMgr>().GetRes<Shader>(shaderName).GetInstance(go);
        // var mat = new Material(mShader);
        // _guideMaskLayer.material = mat;
        // // _guideMaskLayer.material = new Material(Shader.Find(
        // // (GuideMaskShape)_tableGameGuide.MaskShape == GuideMaskShape.Rectangle
        // //     ? "UI/TutorialRectangleMask"
        // //     : "UI/TutorialCircleMask"));
        // mask = UISubView_GuideMaskLayer.GO.GetComponentOrAdd<TutorialMask>();
        // mask.SetConfig(_tableGameGuide);
        CoreUtils.WaitSeconds(_tableGameGuide.DelayShowGuide * 0.001f, () =>
        {
            InitSubView();
        }).Forget();
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        Game.GetMod<ModEvent>().Register<EventTryShowGuideTip>(OnShowGuideCompleteTip);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        Game.GetMod<ModEvent>().UnRegister<EventTryShowGuideTip>(OnShowGuideCompleteTip);
    }

    async void InitSubView()
    {
        UISubView_GuideMaskLayer.OnInit(_tableGameGuide);
        UISubView_GuideNPCText_Mid.OnInit(_tableGameGuide);
        UISubView_GuideNPCText_Top.OnInit(_tableGameGuide);
        UISubView_GuideArrow.OnInit(_tableGameGuide);
        UISubView_GuideFinger.OnInit(_tableGameGuide);

        //todo miss sound
        // Game.GetMod<SoundMgr>().PlaySfx("sfx_ui_common_guide");

        await UniTask.DelayFrame(1);
        CheckTargetToTop();
        if (_tableGameGuide.SaveFlagWhenTrigger)
        {
            Game.GetMod<GuideSys>().SetFinished(_tableGameGuide);
        }
        GO.transform.Find("BlockerMask").gameObject.SetActive(false);
    }

    private void CheckTargetToTop()
    {
        if (_tableGameGuide.TargetToTop)
        {
            var canvas = UISubView_GuideMaskLayer.GO.transform.Bind<Canvas>(true);
            canvas.overrideSorting = true;
            var rootCanvas = GO.GetComponentInParent<Canvas>();
            canvas.sortingOrder = rootCanvas.sortingOrder - 2;
            canvas.sortingLayerName = rootCanvas.sortingLayerName;

            target = GetTarget();

            if (target != null)
            {
                foreach (var targetTransform in target)
                {
                    if (targetTransform is RectTransform)
                    {
                        if (targetTransform.gameObject.GetComponent<Canvas>() != null) continue;
                        var targetCanvas = targetTransform.gameObject.AddComponent<Canvas>();
                        targetCanvas.overrideSorting = true;
                        targetCanvas.sortingOrder = rootCanvas.sortingOrder - 1;
                        targetTransform.gameObject.AddComponent<GraphicRaycaster>();
                    }
                    else
                    {
                        targetTransform.gameObject.layer = 9;
                        var tra = targetTransform.GetComponentsInChildren<Transform>(true);
                        for (int i = 0; i < tra.Length; i++)
                        {
                            tra[i].gameObject.layer = 9;
                        }
                    }


                }
            }
        }
    }

    private void RecoverTarget()
    {
        if (target != null)
        {
            foreach (var targetTransform in target)
            {
                if (targetTransform is RectTransform)
                {
                    if (targetTransform.gameObject.GetComponent<GraphicRaycaster>() != null)
                    {
                        var graphicRayCaster = targetTransform.gameObject.GetComponent<GraphicRaycaster>();
                        GameObject.Destroy(graphicRayCaster);
                    }
                    if (targetTransform.gameObject.GetComponent<Canvas>() != null)
                    {
                        var targetCanvas = targetTransform.gameObject.GetComponent<Canvas>();
                        GameObject.Destroy(targetCanvas);
                    }
                }
            }

        }
    }

    private List<Transform> GetTarget()
    {
        if (_tableGameGuide.TargetTypes == null || _tableGameGuide.TargetTypes.Count == 0) return null;
        var targetList = Game.GetMod<GuideSys>()
            .GetTarget(_tableGameGuide.TargetTypes, _tableGameGuide.TargetParams);
        return targetList;
    }

    private void OnShowGuideCompleteTip(EventTryShowGuideTip obj)
    {
        UISubView_GuideMaskLayer.OnShowCompleteTip();
        UISubView_GuideArrow.OnShowCompleteTip();
        UISubView_GuideFinger.OnShowCompleteTip();
        UISubView_GuideNPCText_Mid.OnShowCompleteTip(_tableGameGuide);
        UISubView_GuideNPCText_Top.OnShowCompleteTip(_tableGameGuide);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        RecoverTarget();
        mask?.OnClose();
        mask = null;
    }



}