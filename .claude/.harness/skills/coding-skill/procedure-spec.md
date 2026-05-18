# Procedure 层编码规范

> 用于改动启动流水线（`ProcedureXxx`）相关代码。

---

## 是什么

Procedure 是 HarnessTest 的**启动流水线步骤**。参见 [Boot.cs](../../../../Assets/Scripts/Main/Boot.cs)：

```csharp
private Procedure[] procedureList = new Procedure[]
{
    new ProcedureInitialize(new ProcedureContext(0)),
    new ProcedureFetchServerVersion(new ProcedureContext(10)),
    new ProcedureYooAssetInit(new ProcedureContext(20)),
    new ProcedureUpdateVersion(new ProcedureContext(30)),
    new ProcedureUpdateManifest(new ProcedureContext(40)),
    new ProcedureDownloadFiles(new ProcedureContext(50)),
    new ProcedureClearCache(new ProcedureContext(80)),
    new ProcedureLoadDll(new ProcedureContext(90)),
    new ProcedureDone(new ProcedureContext(100)),
};
```

主工程 Boot.cs 顺序执行每个 Procedure 的 `ExecuteProcedure()`。Procedure 数字越大越晚执行。

---

## 命名

- 类名前缀：`Procedure` + 大驼峰功能名（`ProcedureInitialize`, `ProcedureLoadDll`）
- 文件名与类名一致
- 不需要继承 `ModuleBase`（Procedure 在 ModuleBase 系统启动之前运行）

---

## 必须遵循

### 1. 顺序号（ProcedureContext 的数字）
- 现有 0/10/20/30/40/50/80/90/100 是预留间隔，**新增 Procedure 必须填到合适的间隔中**
- 不要改动已有 Procedure 的顺序号——可能影响热更兼容
- 新增范围内常用值：5（前置）/ 25/35/45 / 70/95

### 2. 异步执行
```csharp
public async UniTask ExecuteProcedure()
{
    // 串行步骤，必须 await
    await DoStep1();
    await DoStep2();
}
```

### 3. 失败处理
Procedure 失败应有明确策略：
- **致命**：弹错误 UI 并阻断启动（`UIView_Notice` + 退出 App）
- **可恢复**：retry + 用户重试入口
- **可跳过**：CLog.Warn 后继续下一步

### 4. UI 反馈
Boot 期间唯一可用的 UI 是 `UIView_Boot`（启动 splash）。其他 UI 在 `ProcedureLoadDll` 完成前都不可用。

### 5. 主工程依赖
Procedure 类**位于主工程**（`Assets/Scripts/Main/`），不能用热更工程的类。

---

## 禁止

- ❌ 在 Procedure 中调用 `Game.GetMod<>`（此时热更工程尚未启动）
- ❌ 改动顺序号导致流水线乱序
- ❌ 把 ModuleBase 派生类的初始化逻辑写在 Procedure 里（除 `ProcedureLoadDll` 之后）
- ❌ 同步阻塞主线程（必须 await）

---

## 范例（新增一个 Procedure）

需求：在 DLL 加载后、Done 之前，新增"上报启动耗时埋点"。

新增文件 `Assets/Scripts/Main/Procedure/ProcedureReportStartupTime.cs`：
```csharp
using Cysharp.Threading.Tasks;

public class ProcedureReportStartupTime : Procedure
{
    public ProcedureReportStartupTime(ProcedureContext context) : base(context) { }

    public override async UniTask ExecuteProcedure()
    {
        // 主工程能用的 SDK 调用
        StartupReporter.Send(Time.realtimeSinceStartup);
        await UniTask.CompletedTask;
    }
}
```

修改 [Boot.cs](../../../../Assets/Scripts/Main/Boot.cs) 的 procedureList：
```csharp
new ProcedureLoadDll(new ProcedureContext(90)),
new ProcedureReportStartupTime(new ProcedureContext(95)),  // 新增
new ProcedureDone(new ProcedureContext(100)),
```
