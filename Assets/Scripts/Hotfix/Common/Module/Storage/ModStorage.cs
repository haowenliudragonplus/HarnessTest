using System.Collections.Generic;
using System.Reflection;
using DragonPlus.Core;
using DragonPlus.Save;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

/// <summary>
/// 存档模块
/// </summary>
public class ModStorage : ModuleBase
{
    public SDKRef<IUserProfile> Profile { get; private set; }

    public override void OnInit()
    {
        base.OnInit();
        RegisterAllStorage();

        // 必须使用SDK的事件系统，因为发送时是使用的EventBus
        EventBus.Subscribe<EventProfileGetSuccess>(OnProfileGetSucceed);
        EventBus.Subscribe<EventProfileResolved>(OnProfileResolved);
        EventBus.Subscribe<EventProfileReplaced>(OnProfileReplaced);
        EventBus.Subscribe<EventProfileConflict>(OnProfileConflict);
        EventBus.Subscribe<EventAccountLoginOtherDevice>(OnAccountLoginOtherDevice);
    }

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        Profile = SDK<IStorage>.Instance.Get<StorageCommon>().Profile;
    }

    /// <summary>
    /// 手动注册所有存档
    /// </summary>
    private void RegisterAllStorage()
    {
        List<StorageBase> storageList = new List<StorageBase>();
        storageList.Add(new StorageCommon()); //sdk中定义的通用存档结构
        storageList.Add(new StorageClientCommon()); //自定义的通用存档结构
        storageList.Add(new StorageMahjongScrew());
        storageList.Add(new StorageInGame());
        storageList.Add(new StorageActivity());
        storageList.Add(new StorageAd());
        SDK<IStorage>.Instance.InitializeStorage(storageList);
    }

    /// <summary>
    /// 获取存档
    /// </summary>
    public T GetStorage<T>()
        where T : StorageBase
    {
        var storage = SDK<IStorage>.Instance.Get<T>();
        if (storage == null)
            CLog.Error($"获取存档失败：[{typeof(T)}]");
        return storage;
    }

    /// <summary>
    /// 设置存档保存到本地的时间间隔
    /// </summary>
    public void SetSaveToLocalInterval(float saveToLocalInterval)
    {
        Profile.Instance.SaveToDiskInterval = saveToLocalInterval;
    }

    /// <summary>
    /// 设置存档上传到服务器的时间间隔
    /// </summary>
    public void SetSyncToRemote(float syncToRemoteInterval)
    {
        Profile.Instance.SyncToRemoteInterval = syncToRemoteInterval;
    }

    /// <summary>
    /// 强制保存到本地
    /// </summary>
    public void ForceSaveToLocal()
    {
        Profile.Instance.ForceSaveToDisk();
    }

    /// <summary>
    /// 强制上传到服务器
    /// </summary>
    public void ForceSyncToRemote()
    {
        Profile.Instance.ForceSyncToRemote();
    }

    /// <summary>
    /// 清空本地存档
    /// </summary>
    public void ClearLocalStorage()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// 清空服务器存档
    /// </summary>
    public void ClearServerStorage()
    {
        var storageSDK = SDK<IStorage>.Instance;
        if (storageSDK == null)
            return;
        var playerId = Game.GetMod<ModStorage>().GetStorage<StorageCommon>().PlayerId;
        var userProfile = Game.GetMod<ModStorage>().Profile;
        var userProfileType = userProfile.GetType();
        var storageType = storageSDK.GetType();
        var clearMethod = storageType.GetMethod("Clear", BindingFlags.Instance | BindingFlags.NonPublic);
        var uploadMethod = userProfileType.GetMethod("UploadProfile", BindingFlags.Instance | BindingFlags.NonPublic);
        clearMethod?.Invoke(storageSDK, null);
        Game.GetMod<ModStorage>().GetStorage<StorageCommon>().PlayerId = playerId;
        uploadMethod?.Invoke(userProfile, null);
    }

    /// <summary>
    /// 强制使用服务器存档
    /// </summary>
    public void OnProfileReplaced(EventProfileReplaced evt)
    {
        if (Game.GetMod<ModFsm>().CheckState<FsmState_Login>())
            return;

        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventChooseProfileForce);
        if (evt.clear)
        {
            Game.BackToLogin();
        }
        else
        {
            UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
            {
                content = CoreUtils.GetLocalization("UI_profile_force_use_server_profile_desc"),
                onMidBtn = () => { Game.BackToLogin(); },
                showCloseBtn = false,
                showMidBtn = true
            };
            Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
        }
    }

    /// <summary>
    /// 存档冲突
    /// </summary>
    public void OnProfileConflict(EventProfileConflict evt)
    {
        if (Game.GetMod<ModFsm>().CheckState<FsmState_Login>())
            return;

        Game.GetMod<ModUI>().CloseWaiting();
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIChooseProgress, evt.profile);
    }

    /// <summary>
    /// 解决存档冲突完成
    /// </summary>
    public void OnProfileResolved(EventProfileResolved evt)
    {
        var server = evt.userServer;
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventResolveProfile, server.ToString());
    }

    public void OnProfileGetSucceed(EventProfileGetSuccess profileFetchedEvent)
    {
        BIHelper.SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType.GameEventGetProfileFinish);
    }

    public void OnAccountLoginOtherDevice(EventAccountLoginOtherDevice evt)
    {
        if (Game.GetMod<ModFsm>().CheckState<FsmState_Login>())
            return;

        UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
        {
            content = CoreUtils.GetLocalization("UI_errmsg_duplicate_login_native"),
            title = CoreUtils.GetLocalization("UI_common_box_tittle"),
            showMidBtn = true,
            showCloseBtn = false,
            onMidBtn = () => { Game.BackToLogin(); }
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
    }
}