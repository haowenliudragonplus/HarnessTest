using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIView_CommonRewardParam
{
    public List<int> itemIds;
    public List<int> itemCount;
}
public class UIView_CommonReward : UIView_CommonRewardBase
{
    public UIGetRewardParam paramData;
    protected List<UICommonItem> itemViews = new List<UICommonItem>();
    private List<Transform> rootList;
    protected override void OnCreate()
    {
        base.OnCreate();
        InitView();
    }

    private void InitView()
    {
        paramData = ViewData as UIGetRewardParam;
        itemViews.Clear();
        var itemDatas = paramData.itemDatas;
        if (itemDatas == null || itemDatas.Count <= 0) return;
        rootList = new List<Transform>();
        rootList.Add(UINode_Reward);
        
        // Game.GetMod<ModBag>().AddItem(paramData.itemDatas,new BIHelper.ItemChangeReasonArgs());
        
        int rootCount = Mathf.CeilToInt(itemDatas.Count * 1f / 4);
        if (rootCount > 1)
        {
            for (int i = 1; i < rootCount; i++)
            {
                var goRoot = Object.Instantiate<GameObject>(UINode_Reward.gameObject, UINode_RewardGroup);
                rootList.Add(goRoot.transform);
            }
        }

        int group = 0;
        int index = 0;
        for (int i = 0; i < itemDatas.Count; i++)
        {
            var item = OpenUIWidget<UICommonItem>(rootList[group], false, itemDatas[i]);
            itemViews.Add(item);
            index += 1;
            if (index >= 4)
            {
                group += 1;
                index = 0;
            }
        }
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        UIBtn_ContinueButton.onClick.AddListener(ContinueOnClick);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        UIBtn_ContinueButton.onClick.RemoveListener(ContinueOnClick);
    }

    protected void ContinueOnClick()
    {
        Close();
        if (paramData.fly)
        {
            foreach (var itemView in itemViews)
            {
                itemView.Fly();
            }
        }
        else
        {
            foreach (var item in paramData.itemDatas)
            {
                Game.GetMod<ModEvent>().Dispatch(new EvtItemChange((EItemType)item.id, item.amount, false));
            }
        }

        paramData.closeAction?.Invoke();
    }
}
