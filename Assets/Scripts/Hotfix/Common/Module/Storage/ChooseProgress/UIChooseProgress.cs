using System;
using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Core;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChooseProgressData
{
    public int Level { get; set; }
    public double Time { get; set; }
    public int CoinNum { get; set; }
    public int DecoCoinNum { get; set; }

    public string json;

    public ChooseProgressData(string json)
    {
        this.json = json;
        JObject obj = JObject.Parse(json);
        Time = double.Parse(obj["StorageCommon"]["updatedAt"].ToString());
        Level = int.Parse(obj["StorageMahjongScrew"]["levelIndexDict"][((int)EInGameModeType.Main).ToString()].ToString()) + 1;
        string coinKey = "currency_1";
        if (obj["StorageClientCommon"]["currency"][coinKey] != null)
        {
            float vc0 = float.Parse(obj["StorageClientCommon"]["currency"][coinKey]["_vc0"].ToString());
            int vc1 = int.Parse(obj["StorageClientCommon"]["currency"][coinKey]["_vc1"].ToString());
            CoinNum = (int)Math.Round(8.0f * vc0 + vc1);
        }
        // string decoCoinKey = "currency_8";
        // if (obj["StorageClientCommon"]["currency"][decoCoinKey] != null)
        // {
        //     float vc0 = float.Parse(obj["StorageClientCommon"]["currency"][decoCoinKey]["_vc0"].ToString());
        //     int vc1 = int.Parse(obj["StorageClientCommon"]["currency"][decoCoinKey]["_vc1"].ToString());
        //     DecoCoinNum = (int)Math.Round(8.0f * vc0 + vc1);
        // }
    }
}

public class UIChooseProgress : UIViewBase
{
    private ChooseProgressData _serverData;
    private ChooseProgressData _localData;
    private EventProfileConflict.ProfileInfo _profileInfo;
    private Button _localButton;
    private Button _localButtonGreen;
    private LocalizeTextMeshProUGUI _localCoinText;
    private LocalizeTextMeshProUGUI _localDecoCoinText;
    private LocalizeTextMeshProUGUI _localLevelText;
    private LocalizeTextMeshProUGUI _localTimeText;
    private Transform _localNewTag;
    private Button _serverButton;
    private Button _serverButtonGreen;
    private LocalizeTextMeshProUGUI _serverCoinText;
    private LocalizeTextMeshProUGUI _serverDecoCoinText;
    private LocalizeTextMeshProUGUI _serverLevelText;
    private LocalizeTextMeshProUGUI _serverTimeText;
    private Transform _serverNewTag;
    private Button btnClose;

    protected override void BindComponent()
    {
        base.BindComponent();
        _localButton = GO.transform.Find("Root/Progress2/Button").GetComponent<Button>();
        //_localButtonGreen = GO.transform.Find("Root/Progress2/ButtonGreen").GetComponent<Button>();
        // _localCoinText = GO.transform.Find("Root/Progress2/RewardCell/Text").GetComponent<LocalizeTextMeshProUGUI>();
        // _localDecoCoinText = GO.transform.Find("Root/Progress2/LevelCell/Level").GetComponent<LocalizeTextMeshProUGUI>();
        _localLevelText = GO.transform.Find("Root/Progress2/UITxt_LevelText2").GetComponent<LocalizeTextMeshProUGUI>();
        _localTimeText = GO.transform.Find("Root/Progress2/Time").GetComponent<LocalizeTextMeshProUGUI>();
        //_localNewTag = GO.transform.Find("Root/Progress2/LevelCell/TagGroup");

        _serverButton = GO.transform.Find("Root/Progress1/Button").GetComponent<Button>();
        //_serverButtonGreen = GO.transform.Find("Root/Progress1/ButtonGreen").GetComponent<Button>();
        // _serverCoinText = GO.transform.Find("Root/Progress1/RewardCell/Text").GetComponent<LocalizeTextMeshProUGUI>();
        // _serverDecoCoinText = GO.transform.Find("Root/Progress1/LevelCell/Level").GetComponent<LocalizeTextMeshProUGUI>();
        _serverLevelText = GO.transform.Find("Root/Progress1/UITxt_LevelText1").GetComponent<LocalizeTextMeshProUGUI>();
        _serverTimeText = GO.transform.Find("Root/Progress1/Time").GetComponent<LocalizeTextMeshProUGUI>();
        //_serverNewTag = GO.transform.Find("Root/Progress1/LevelCell/TagGroup");
        btnClose = GO.transform.Find("Root/CloseButton").GetComponent<Button>();
    }

    protected override void RegisterGameEvent()
    {
        base.RegisterGameEvent();
        _serverButton.onClick.AddListener(OnClickServerButton);
        _localButton.onClick.AddListener(OnClickLocalButton);
    }

    protected override void RemoveGameEvent()
    {
        base.RemoveGameEvent();
        _serverButton.onClick.RemoveListener(OnClickServerButton);
        _serverButtonGreen.onClick.RemoveListener(OnClickServerButton);
        _localButtonGreen.onClick.RemoveListener(OnClickLocalButton);
        _localButton.onClick.RemoveListener(OnClickLocalButton);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        InitData();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventChooseProfilePop);
        RefreshView();
    }

    private void InitData()
    {
        var profile = ViewData as EventProfileConflict.ProfileInfo;
        _profileInfo = profile;
        var localJson = profile.localProfile;
        var serverJson = profile.serverProfile;
        _localData = new ChooseProgressData(localJson);
        _serverData = new ChooseProgressData(serverJson);
    }

    private void RefreshView()
    {
        // _localNewTag.gameObject.SetActive(false);
        // _serverNewTag.gameObject.SetActive(false);
        // _localButtonGreen.gameObject.SetActive(false);
        // _serverButtonGreen.gameObject.SetActive(false);
        btnClose.gameObject.SetActive(false);

        // 本地存档展示
        // _localCoinText.SetText(_localData.CoinNum.ToString());
        // _localDecoCoinText.SetText(_localData.DecoCoinNum.ToString());
        _localLevelText.SetText(CoreUtils.GetLocalization("UI_common_level", _localData.Level));
        _localTimeText.SetText(CoreUtils.GetLocalization("UI_choose_progress_last")
                               + "\n" + TimeUtils.FormatDateTime(_localData.Time / 1000));
        // 服务器存档展示
        // _serverCoinText.SetText(_serverData.CoinNum.ToString());
        // _serverDecoCoinText.SetText(_serverData.DecoCoinNum.ToString());
        _serverLevelText.SetText(CoreUtils.GetLocalization("UI_common_level", _serverData.Level));
        _serverTimeText.SetText(CoreUtils.GetLocalization("UI_choose_progress_last") + "\n" +
                                TimeUtils.FormatDateTime(_serverData.Time / 1000));
    }

    void OnClickLocalButton()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIChooseTip, new List<object>() { _profileInfo, false });
    }

    void OnClickServerButton()
    {
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIChooseTip, new List<object>() { _profileInfo, true });
    }
}