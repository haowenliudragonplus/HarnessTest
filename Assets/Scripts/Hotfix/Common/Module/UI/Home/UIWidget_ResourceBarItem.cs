using DG.Tweening;
using DragonPlus.Config.Common;
using UnityEngine;

public class UIWidget_ResourceBarItem : UIWidget_ResourceBarItemBase
{
    public class OpenData
    {
        public int itemId;
        public bool showAddBtn;
    }

    public int ItemType => openData.itemId;
    public Transform ResourceIconTrans => UIImg_Icon.transform;
    private OpenData openData;
    private ModBag modBag;
    private Table_Common_Item itemCfg;

    private bool inTimeLimit; //在限时状态中
    private bool inAutoAddTime; //在自动添加状态中

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        openData = viewData as OpenData;
        modBag = Game.GetMod<ModBag>();
        itemCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>(openData.itemId);
        currentCount = itemCfg.TimeLimitItemId > 0 ? 0 : modBag.GetItemCount((EItemType)itemCfg.Id);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        if (openData.showAddBtn)
        {
            UIBtn_Add.onClick.AddListener(OnAddBtn);
        }
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        RegisterEvent<EvtItemChange>(OnItemChange);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    private void RefreshView()
    {
        UIImg_TimeLimit.gameObject.SetActive(false);
        UITxt_IconNum.gameObject.SetActive(false);
        //
        UIBtn_Add.gameObject.SetActive(openData.showAddBtn);
        //UIImg_Add.gameObject.SetActive(openData.showAddBtn);
        CoreUtils.SetImg(UIImg_Icon, itemCfg.Icon);
        var leftTime = itemCfg.TimeLimitItemId > 0
            ? modBag.GetTimeLimitLeftTime((EItemType)itemCfg.Id)
            : 0;
        if (leftTime > 0)
        {
            inTimeLimit = true;
            UIImg_TimeLimit.gameObject.SetActive(true);
        }
        else
        {
            if (itemCfg.AutoAddInterval > 0)
            {
                var autoAddLeftTime = modBag.GetAutoAddLeftTime((EItemType)itemCfg.Id);
                if (autoAddLeftTime > 0)
                {
                    UITxt_IconNum.gameObject.SetActive(true);
                    UITxt_IconNum.SetText(modBag.GetItemCount((EItemType)itemCfg.Id).ToString());
                    inAutoAddTime = true;
                }
                else
                {
                    UITxt_Num.SetText(modBag.GetItemCount((EItemType)itemCfg.Id).ToString());
                }
            }
            else
            {
                UITxt_Num.SetText(modBag.GetItemCount((EItemType)itemCfg.Id).ToString());
            }
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (inTimeLimit && itemCfg.TimeLimitItemId > 0)
        {
            var leftTime = modBag.GetTimeLimitLeftTime((EItemType)itemCfg.Id);
            if (leftTime > 0)
            {
                UITxt_Num.text = TimeUtils.FormatTime((int)leftTime);
            }
            else
            {
                inTimeLimit = false;
                RefreshView();
            }
        }
        else if (inAutoAddTime)
        {
            var leftTime = modBag.GetAutoAddLeftTime((EItemType)itemCfg.Id);
            if (leftTime > 0)
            {
                UITxt_Num.text = TimeUtils.FormatTime((int)leftTime);
            }
            else
            {
                inAutoAddTime = false;
                RefreshView();
            }
        }
    }

    private void OnAddBtn()
    {
        Game.GetMod<ModFunctionJump>().Jump(itemCfg.Jump);
    }

    // todo logic 后面改成监听飞奖励事件
    private void OnItemChange(EvtItemChange evt)
    {
        if (!evt.isAutoAdd) return;
        var itemCfg_Change = Game.GetMod<ModConfig>().GetConfig<Table_Common_Item>((int)evt.itemType);
        if (itemCfg_Change.Id != itemCfg.Id
            && itemCfg_Change.Id != itemCfg.TimeLimitItemId)
            return;

        if (itemCfg_Change.IsTimeLimitItem)
        {
            inTimeLimit = true;
        }
        RefreshItemCount();
    }
    private Tween countToTween;
    private int currentCount;
    void RefreshItemCount()
    {
        var leftTime = itemCfg.TimeLimitItemId > 0 ? modBag.GetTimeLimitLeftTime((EItemType)itemCfg.Id) : 0;
        if(leftTime>0)
        {
            inTimeLimit = true;
            UIImg_TimeLimit.gameObject.SetActive(true);
            return;
        }
        if (countToTween == null)
        {
            var newCount = Game.GetMod<ModBag>().GetItemCount((EItemType)itemCfg.Id);
            if(currentCount == newCount) return;
            countToTween = DOTween.To(() => 0, v => { UITxt_Num.text = v.ToString(); }, newCount, 0.8f).From(currentCount).SetEase(Ease.Linear).OnComplete(() =>
            {
                currentCount = newCount;
                UITxt_Num.text = newCount.ToString();
                countToTween = null;
            });   
        }
    }
}