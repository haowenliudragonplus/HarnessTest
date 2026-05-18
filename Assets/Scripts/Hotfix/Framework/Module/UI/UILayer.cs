using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class UILayer
    {
        private GameObject layerGo;
        public GameObject LayerGo => layerGo;

        private EUILayerType layerType;

        private List<UIViewBase> uiViewList; //当前层级下的所有界面

        public void Init(GameObject layerGo, EUILayerType layerType)
        {
            this.layerGo = layerGo;
            this.layerType = layerType;
            uiViewList = new List<UIViewBase>();
        }

        public void AddView(UIViewBase view)
        {
            uiViewList.Add(view);
            SetAllViewOrderInLayer();
        }

        public void RemoveView(UIViewBase view)
        {
            uiViewList.Remove(view);
            SetAllViewOrderInLayer();
        }

        /// <summary>
        /// 查找当前层级下的最顶部界面
        /// </summary>
        public UIViewBase GetTopView(List<int> ignoreViewList = null)
        {
            if (uiViewList.Count <= 0)
                return null;

            for (int i = uiViewList.Count - 1; i >= 0; i--)
            {
                var view = uiViewList[i];
                if (!view.Visible
                    || (ignoreViewList != null && ignoreViewList.Contains(view.ViewId)))
                    continue;
                return view;
            }
            return null;
        }

        /// <summary>
        /// 设置当前层级下所有界面的OrderInLayer
        /// </summary>
        private void SetAllViewOrderInLayer()
        {
            int originOrderInLayer = (int)layerType * ModUI.LAYERSTEP_ORDERINLAYER;
            for (int i = 0; i < uiViewList.Count; i++)
            {
                uiViewList[i].OrderInLayer = originOrderInLayer;
                originOrderInLayer += ModUI.VIEWSTEP_ORDERINLAYER;
            }
        }
        
        public List<UIViewBase> GetLayerUIList()
        {
            if (uiViewList == null || uiViewList.Count <= 0) return null;
            return uiViewList;
        }
    }
}