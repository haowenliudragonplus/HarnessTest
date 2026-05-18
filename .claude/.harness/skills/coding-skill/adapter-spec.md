# 第三方 SDK 适配层规范

> 用于改动涉及第三方 SDK（IAP / 广告 / Firebase / 推送 / 账号绑定等）的代码。

---

## 适配层（Adapter）的目的

第三方 SDK 的调用接口、回调时序、错误码、平台差异等不应污染业务代码。所有 SDK 必须封装在专用 Module（Adapter）中，业务代码只调 Adapter。

已有典型 Adapter：
- [ModAccount](../../../../Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs) — 账号 SDK 适配（Facebook / Apple 绑定）
- `ModIAP` — 内购 SDK 适配
- 广告 SDK 通常封装在专用 Module 中

---

## 必须遵循

### 1. 平台差异隔离
SDK 调用前用 `#if UNITY_EDITOR` / `#if UNITY_ANDROID` / `#if UNITY_IOS` 隔离。在 Editor 中用 mock，避免开发卡死。

参考 [ModAccount.BindFacebook](../../../../Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs#L44):
```csharp
public void BindFacebook(Action cancelCallback)
{
#if UNITY_EDITOR
    OnBindFbResult(true, "");  // Editor 直接走成功 mock
#else
    SDK<IAccount>.Instance.BindFacebook(OnBindFbResult, cancelCallback);
#endif
}
```

### 2. 超时机制
任何 SDK 同步等待调用必须设超时（一般 ≤30s）。超时后走降级回调。

```csharp
public async UniTask<bool> Login()
{
    var loginTask = DoSDKLogin();
    var timeoutTask = UniTask.Delay(TimeSpan.FromSeconds(30));
    var (winner, _, _) = await UniTask.WhenAny(loginTask, timeoutTask);
    if (winner == 1)  // 超时
    {
        CLog.Error("SDK login timeout");
        OnLoginFailed("timeout");
        return false;
    }
    return true;
}
```

### 3. 降级方案
SDK 不可用 / 失败时给玩家明确反馈（弹 UIView_Notice），不能默默卡死。

参考 ModAccount.OnBindFbResult：
```csharp
private void OnBindFbResult(bool isSuccess, string reason)
{
    Game.GetMod<ModUI>().CloseWaiting();
    if (isSuccess) { /* ... */ }
    else
    {
        UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
        {
            content = CoreUtils.GetLocalization("UI_bind_fb_fail_authen_error_text"),
            showCloseBtn = false,
            showMidBtn = true,
        };
        Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
    }
    Game.GetMod<ModEvent>().Dispatch(new EvtBindFacebook(isSuccess));
}
```

### 4. 状态事件化
SDK 调用结果通过 `ModEvent.Dispatch` 派发事件，让上层模块响应。Adapter 不直接调上层业务方法。

### 5. 等待框（Loading UI）配对
打开 Waiting → 必须有对应关闭路径（成功 / 失败 / 取消 / 超时）。漏一条就卡死。

---

## 禁止

- ❌ 业务模块（如 ModBag / ModFsm）直接调 `SDK<IXxx>.Instance.Foo()`——必须经 Adapter
- ❌ Adapter 内做与 SDK 无关的业务逻辑
- ❌ 无超时的同步等待 SDK 回调
- ❌ Adapter 内 throw 异常给上层（用回调 / 事件传错误）
- ❌ 让 SDK 失败后 UI 卡在 Loading
- ❌ Editor 中没有 mock（导致开发期卡住）

---

## 范例：新增一个 SDK 适配

需求：接入"成就 SDK"，玩家解锁成就后上报到 Google Play / Game Center。

文件：`Assets/Scripts/Hotfix/Common/Module/Achievement/ModAchievementSDK.cs`（假设已存在 ModAchievement 的业务版，本例新建 Adapter）

```csharp
using System;
using Cysharp.Threading.Tasks;
using Framework;

public class ModAchievementSDK : ModuleBase
{
    private const int TIMEOUT_SECONDS = 15;

    public async UniTask<bool> Unlock(string achievementId)
    {
        if (string.IsNullOrEmpty(achievementId))
        {
            CLog.Warn("empty achievementId");
            return false;
        }

#if UNITY_EDITOR
        // Editor mock：直接成功
        CLog.Info($"[mock] unlock achievement: {achievementId}");
        Game.GetMod<ModEvent>().Dispatch(new EvtAchievementUnlocked(achievementId, true));
        return true;
#else
        var tcs = new UniTaskCompletionSource<bool>();
        SDK<IAchievement>.Instance.Unlock(achievementId, (success, reason) =>
        {
            if (!success) CLog.Warn($"unlock fail: {achievementId}, reason: {reason}");
            Game.GetMod<ModEvent>().Dispatch(new EvtAchievementUnlocked(achievementId, success));
            tcs.TrySetResult(success);
        });

        var timeoutTask = UniTask.Delay(TimeSpan.FromSeconds(TIMEOUT_SECONDS));
        var (winner, result, _) = await UniTask.WhenAny(tcs.Task, timeoutTask);
        if (winner == 1)
        {
            CLog.Error($"unlock timeout: {achievementId}");
            Game.GetMod<ModEvent>().Dispatch(new EvtAchievementUnlocked(achievementId, false));
            return false;
        }
        return result;
#endif
    }
}
```

要点：
- 入参校验（empty）
- Editor 平台 mock
- 真实平台用 UniTaskCompletionSource 包装 callback
- 超时机制 + 超时也派发失败事件（不卡死）
- 错误日志走 CLog
- 通过 Event 而非直接调上层
- 不 throw 异常给调用方
