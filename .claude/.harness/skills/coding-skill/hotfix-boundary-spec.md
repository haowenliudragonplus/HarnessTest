# 主工程 ↔ 热更工程 边界规范

> 用于改动需要跨主-热更工程边界的代码（修改 `Loader.OnXxx` 桥接、新增主工程 SDK 集成等）。
>
> 这是本项目最容易踩坑的地方，违反 → MUST FIX。

---

## 物理分离

| 工程 | 路径 | 是否热更 | 编译产物 |
|---|---|---|---|
| 主工程 | `Assets/Scripts/Main/` | ❌ 不能热更 | 打入 App 包，需重新发版才能更新 |
| 热更工程 | `Assets/Scripts/Hotfix/` | ✅ 通过 HybridCLR | 编译为 DLL，启动时从服务器下载 |

主工程是 Unity 默认编译的 C# Assembly，热更工程通过 HybridCLR 运行时加载。**两者命名空间隔离、引用方向单向**：主工程 ← 不能 ← 热更工程，热更工程可以引用主工程（少量公开接口）。

---

## 桥接模式：`Loader.OnXxx` 静态委托

主工程 [Boot.cs](../../../../Assets/Scripts/Main/Boot.cs) 把 MonoBehaviour 生命周期事件通过静态委托转发给热更：

```csharp
// 主工程 Boot.cs（不能修改频繁，慎改）
private void Update()
{
    Loader.OnUpdate?.Invoke();
    lastSecondsUpdateTime += Time.deltaTime;
    if (lastSecondsUpdateTime >= 1f)
    {
        Loader.OnSecondsUpdate?.Invoke();
        lastSecondsUpdateTime = 0;
    }
}

private void FixedUpdate()  { Loader.OnFixedUpdate?.Invoke(); }
private void LateUpdate()   { Loader.OnLateUpdate?.Invoke(); }
private void OnApplicationPause(bool p) { Loader.OnApplicatePause?.Invoke(p); }
private void OnApplicationQuit() { Loader.OnApplicateQuit?.Invoke(); }
```

热更工程通过 `+=` 订阅：
```csharp
// 热更工程某处（例如 Game 初始化）
Loader.OnUpdate += GlobalUpdate;
Loader.OnSecondsUpdate += OnSecondTick;
```

`Loader` 是位于**主工程**的静态类，它的字段是 `public static Action OnXxx`。

---

## 必须遵循

### 1. 主工程改动 = 重新打包
任何 `Assets/Scripts/Main/` 下的改动**不能通过热更生效**。这是高成本操作，必须在 spec.md 风险表中标"高"。

### 2. 热更工程不能继承 MonoBehaviour
HybridCLR 不支持热更类继承 MonoBehaviour。需要 MonoBehaviour 行为的，必须：
- 主工程定义 MonoBehaviour 类
- 热更通过 `Loader.OnXxx?.Invoke()` 桥接接收事件

### 3. 主工程接口要稳定
`Loader` 的字段一旦定义就不要轻易删除——可能有线上版本的热更 DLL 还在引用。新增字段没问题，删除字段是破坏性改动。

### 4. 主工程不能直接调热更类
错误：
```csharp
// 主工程 Boot.cs
var game = new Game();  // ❌ Game 在热更工程，编译时不可见
```

正确：主工程定义入口（如 `Loader.RunMain()`），热更工程订阅后调用。

### 5. 主工程的 Resources.Load 是允许的
启动期 ModAsset 未启动前，Boot 阶段可以用 `Resources.Load<>` 加载启动 UI（参考 [Boot.cs:39](../../../../Assets/Scripts/Main/Boot.cs#L39)）。

---

## 禁止

- ❌ 在主工程业务代码中用 `Game.GetMod<>`（编译失败 + 反模式）
- ❌ 在主工程类中 import / using 热更工程的命名空间
- ❌ 让热更工程的代码"反向"修改主工程行为（除通过 `Loader.OnXxx` 订阅）
- ❌ 主工程删除/重命名 `Loader` 的现有公开字段
- ❌ 修改 Boot.cs 的 procedureList 顺序号（用预留间隔填）

---

## 范例：新增一个生命周期事件

需求：游戏需要"网络状态变化"事件，主工程检测网络，热更工程响应。

**Step 1**：主工程 `Assets/Scripts/Main/Loader.cs`（推断）新增字段：
```csharp
public static Action<bool> OnNetworkChanged;  // 参数：是否连通
```

**Step 2**：主工程的网络检测 MonoBehaviour（如新增 `NetworkMonitor.cs`）：
```csharp
public class NetworkMonitor : MonoBehaviour
{
    private NetworkReachability lastState;
    private void Update()
    {
        var cur = Application.internetReachability;
        if (cur != lastState)
        {
            lastState = cur;
            Loader.OnNetworkChanged?.Invoke(cur != NetworkReachability.NotReachable);
        }
    }
}
```

**Step 3**：热更工程订阅（如在 ModNetwork.OnInit）：
```csharp
public class ModNetwork : ModuleBase
{
    public override void OnInit()
    {
        base.OnInit();
        Loader.OnNetworkChanged += OnNetChange;
    }
    public override void OnDispose()
    {
        Loader.OnNetworkChanged -= OnNetChange;
        base.OnDispose();
    }
    private void OnNetChange(bool reachable)
    {
        CLog.Info($"network reachable: {reachable}");
        Game.GetMod<ModEvent>().Dispatch(new EvtNetworkChanged(reachable));
    }
}
```

要点：
- Loader 新增字段（不删除现有）
- 主工程的 MonoBehaviour 检测，**不写任何业务逻辑**
- 热更工程订阅时记得在 OnDispose 反订阅
- 桥接事件用 Action / Action<T>，不要复杂泛型
- spec.md 风险表必须标"主工程改动 = 高风险"
