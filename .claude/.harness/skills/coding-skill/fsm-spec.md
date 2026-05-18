# FSM 层编码规范

> 用于新增或改动状态机相关代码（`FsmState_*`，`ModFsm` 的子状态）。

---

## 是什么

FSM 是 HarnessTest 的**游戏阶段状态机**。参见 [ModFsm.cs](../../../../Assets/Scripts/Hotfix/Framework/Module/FSM/ModFsm.cs)。

切换状态的唯一方式：
```csharp
Game.GetMod<ModFsm>().ChangeState<FsmState_Home>();
```

已知典型状态：
- `FsmState_Login` — 登录界面
- `FsmState_Home` — 主界面（参考 [ModAccount.OnLoginSuccess](../../../../Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs#L38) 里的 `Game.GetMod<ModFsm>().ChangeState<FsmState_Home>();`）

---

## 命名

| 项 | 规则 |
|---|---|
| 类名 | `FsmState_` + 大驼峰（`FsmState_Home`、`FsmState_InGame`） |
| 文件名 | 与类名一致 |
| 父类 | `FsmStateBase`（需 new() 构造） |

---

## 生命周期方法（基于 ModFsm 源码）

```csharp
public class FsmState_Example : FsmStateBase
{
    public override void OnInit(FsmStateInitParam initParam) { }
    public override async UniTask PreOnEnter(FsmStateEnterParam enterParam) { }
    public override void OnEnter(FsmStateEnterParam enterParam) { }
    public override async UniTask PreOnExit() { }
    public override void OnExit() { }
    public override void OnFixedUpdate(float dt) { }
    public override void OnUpdate(float dt) { }
    public override void OnLateUpdate(float dt) { }
    public override void OnPause() { }
    public override void OnResume() { }
    public override void Dispose() { }
}
```

执行序：
```
ChangeState 调用时：
  await PreOnExit() → OnExit()
  ↓ 切换 CurState
  await PreOnEnter() → OnEnter()
  IsFirstEnter = false
  ↓
  Resources.UnloadUnusedAssets()
```

### 用法分工
- **PreOnXxx**（async）：需要 await 的资源加载、网络请求
- **OnXxx**（同步）：UI 切换、纯逻辑
- **IsFirstEnter**：检查"是否首次进入"，用于新手引导等首次行为

---

## 必须遵循

### 1. 状态切换不要在 Enter 内同步触发
```csharp
public override void OnEnter(FsmStateEnterParam enterParam)
{
    // ❌ 错误：会触发递归切换
    Game.GetMod<ModFsm>().ChangeState<FsmState_Other>();
}
```

如必须切换，用 UniTask delay 一帧：
```csharp
public override void OnEnter(FsmStateEnterParam enterParam)
{
    DelayedSwitch().Forget();
}
private async UniTaskVoid DelayedSwitch()
{
    await UniTask.Yield();
    Game.GetMod<ModFsm>().ChangeState<FsmState_Other>();
}
```

### 2. 必须先 AddState 再 SetDefaultState
启动序参考 ModFsm.AddState 注释："默认第一个添加的状态为初始状态"。

### 3. 退出状态时清理订阅 / 释放资源
`OnExit` 是反向操作 `OnEnter` 的镜像点。任何在 `OnEnter` 中创建/订阅的，必须在 `OnExit` 中销毁/反订阅。

### 4. 不要在 OnUpdate 中分配对象
状态机的 OnUpdate 每帧调用，GC 敏感。

---

## 禁止

- ❌ 状态间共享可变静态字段（破坏状态机封装）
- ❌ 跨状态直接调方法（`FsmState_Home.Instance.DoSth()`）——通过 ModEvent 解耦
- ❌ 在 OnUpdate 中调 LINQ 等会分配内存的 API（用 for 循环）
- ❌ 不重写 OnExit 导致资源泄漏

---

## 范例：新增一个 FSM 状态

新增 `FsmState_Settings` 用于设置界面：

```csharp
using Framework;
using Cysharp.Threading.Tasks;

public class FsmState_Settings : FsmStateBase
{
    private UIView_Settings settingsView;

    public override async UniTask PreOnEnter(FsmStateEnterParam enterParam)
    {
        // 异步加载资源
        settingsView = await Game.GetMod<ModUI>().OpenAsync<UIView_Settings>(UIViewName.UIView_Settings);
    }

    public override void OnEnter(FsmStateEnterParam enterParam)
    {
        CLog.Info("Enter Settings");
        // 注册本状态特有的事件
    }

    public override void OnExit()
    {
        Game.GetMod<ModUI>().Close(UIViewName.UIView_Settings);
        settingsView = null;
    }
}
```

注册：
```csharp
Game.GetMod<ModFsm>().AddState<FsmState_Settings>();
```
