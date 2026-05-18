# Module 层编码规范

> 用于新增或改动业务模块（`ModXxx`）相关代码。

---

## 基类

所有 Module 继承 [ModuleBase](../../../../Assets/Scripts/Hotfix/Framework/Module/ModuleBase.cs)：

```csharp
public class ModuleBase : EventListenerHolder
{
    public virtual void OnInit()        { RegisterEvent(); }
    public virtual void RegisterEvent() { }
    public virtual void OnStart()       { }
    public virtual void FixedUpdate(float deltaTime) { }
    public virtual void Update(float deltaTime)      { }
    public virtual void LateUpdate(float deltaTime)  { }
    public virtual void OnLoginSuccess() { }
    public virtual void OnDispose()     { UnRegisterAllEvent(); }
}
```

---

## 命名与目录

| 项 | 规则 |
|---|---|
| 类名 | `Mod` + 大驼峰（`ModBag`、`ModTip`） |
| 文件名 | 与类名一致 |
| 目录 | `Assets/Scripts/Hotfix/{Framework/Common/Activity/Global/InGame}/Module/{ModuleName}/` |
| 命名空间 | 不强制，但与同目录现有文件保持一致 |

### 归类
- Framework：纯基础设施（如 ModTimer / ModEvent / ModUI），无业务字段
- Common：跨场景业务（如 ModAccount / ModBag），有业务状态
- Activity：临时活动，命名 `ModActivity_*`
- Global：全局 UI
- InGame：游戏内（拉弓射箭等）

---

## 生命周期使用

| 方法 | 何时被调 | 推荐用途 |
|---|---|---|
| `OnInit()` | Module 注册时（启动早期，登录前） | 初始化字段、注册事件（必须调 super 或显式调 RegisterEvent） |
| `RegisterEvent()` | OnInit 内被调 | 调 `Listen<EvtXxx>(handler)` 注册事件 |
| `OnStart()` | DLL 加载完毕、Game.Start() 后 | 跨模块协作的启动逻辑 |
| `FixedUpdate(dt)` | 主工程 FixedUpdate 桥接 | 物理逻辑（少用） |
| `Update(dt)` | 主工程 Update 桥接 | 帧逻辑（慎用，性能敏感） |
| `LateUpdate(dt)` | 主工程 LateUpdate 桥接 | 相机/UI 同步 |
| `OnLoginSuccess()` | ModAccount.OnLoginSuccess 触发 | 登录后才需要的数据初始化 |
| `OnDispose()` | 重启 / 退出游戏 | 反注册（必须调 super 或显式调 UnRegisterAllEvent） |

---

## 必须遵循

### 1. 访问其他 Module 唯一方式
```csharp
var bag = Game.GetMod<ModBag>();
bag.AddItem(...);
```

禁止：
```csharp
ModBag.Instance.AddItem(...);  // ❌ 单例反模式
new ModBag().AddItem(...);     // ❌ 重复实例
```

### 2. 事件订阅 / 反订阅成对
```csharp
public override void RegisterEvent()
{
    Listen<EvtLoginSuccess>(OnLogin);
    Listen<EvtItemAdded>(OnItemAdded);
}
// OnDispose 自动调用 UnRegisterAllEvent，无需手动反订阅
```

### 3. 字段命名
- 公开字段：`PascalCase`
- 私有字段：`camelCase` 或 `_camelCase`（参考同目录现有代码风格）
- 常量：`UPPER_SNAKE_CASE` 或 `PascalCase`（看现有代码）

### 4. 业务方法的标准签名（异步）
```csharp
public async UniTask<bool> DoSomething(int param)
{
    if (param < 0) { CLog.Warn("invalid param"); return false; }
    await SomeAsyncOp();
    return true;
}
```

---

## 禁止

- ❌ 继承 `MonoBehaviour`（HybridCLR 限制）
- ❌ 静态字段长期持有 Module 引用（Module 可能被 Dispose）
- ❌ 在 `OnInit` 中访问其他 Module（顺序未定，可能未注册）——跨模块协作放在 `OnStart` 或更晚
- ❌ 在 Module 内直接调 SDK（必须通过 Adapter Module，见 adapter-spec.md）
- ❌ 在 `Update` 中分配对象（GC 压力）

---

## 范例：新增一个 Module

新增 `ModTip` 的"最大并发显示数量"功能。

文件：`Assets/Scripts/Hotfix/Common/Module/Tip/ModTip.cs`（已存在，本例假设新增字段）

```csharp
using Framework;

public class ModTip : ModuleBase
{
    public long MaxConcurrent { get; private set; } = 3;    // 业务字段：long
    private readonly List<TipInstance> activeTips = new();

    public override void OnInit()
    {
        base.OnInit();   // 必须调，否则 RegisterEvent 不会被调
    }

    public override void RegisterEvent()
    {
        Listen<EvtTipRequested>(OnTipRequested);
    }

    public void SetMaxConcurrent(long max)
    {
        if (max <= 0) { CLog.Warn($"invalid max: {max}"); return; }
        MaxConcurrent = max;
        TrimActive();
    }

    private void OnTipRequested(EvtTipRequested evt)
    {
        if (activeTips.Count >= MaxConcurrent)
        {
            CLog.Info($"tip throttled, active: {activeTips.Count}");
            return;
        }
        // ... 显示 tip
    }

    private void TrimActive() { /* ... */ }
}
```

要点：
- `MaxConcurrent` 用 `long`（数量字段）
- `OnInit` 内 `base.OnInit()` 不能少
- `Listen<>` 在 `RegisterEvent` 内调用
- 边界检查（`max <= 0`）有 `CLog.Warn`
- 错误用 `CLog`，不用 `Debug.Log`
