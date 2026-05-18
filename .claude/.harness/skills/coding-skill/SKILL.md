---
name: coding-skill
description: Generator Agent 的标准编码 SOP；包含 6 份分层 Spec 的导航
---

# Skill: 编码实现（Coding Skill）

> 由 Generator Agent 在阶段 3（编码）和阶段 4'（编码返工）启动时加载。

---

## 工作流

### Step 1: 理解 + 装备

按 tasks.md 列出的每项 Task，先识别**改动涉及的代码层级**，然后 Read 对应的分层 Spec：

| 改动涉及 | 必读分层 Spec |
|---|---|
| 启动流水线（`ProcedureXxx`） | [procedure-spec.md](procedure-spec.md) |
| 业务模块（`ModXxx`） | [module-spec.md](module-spec.md) |
| 状态机（`FsmState_Xxx`） | [fsm-spec.md](fsm-spec.md) |
| UI 视图（`UIView_Xxx`） | [ui-view-spec.md](ui-view-spec.md) |
| 主工程文件 / `Loader.OnXxx` 桥接 | [hotfix-boundary-spec.md](hotfix-boundary-spec.md) |
| 第三方 SDK 调用 / IAP / 广告 / 网络 | [adapter-spec.md](adapter-spec.md) |

跨层级改动 → 多份都读。

### Step 2: Read 同类型范例

对每类改动，至少 Read 一个现有的同类文件作风格参考：

| 层级 | 推荐范例 |
|---|---|
| Module | [ModAccount.cs](../../../../Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs) |
| Module 基类 | [ModuleBase.cs](../../../../Assets/Scripts/Hotfix/Framework/Module/ModuleBase.cs) |
| Procedure | [Boot.cs](../../../../Assets/Scripts/Main/Boot.cs) 中的 procedureList |
| FSM | [ModFsm.cs](../../../../Assets/Scripts/Hotfix/Framework/Module/FSM/ModFsm.cs) |
| UI View | Grep `class UIView_` 找一个简单的看 |

### Step 3: 改动前 Read 全文

**任何文件**在 Edit 之前必须先 Read 全文，**没有例外**（防 Anthropic Failure Mode 2: Premature Victory）。

### Step 4: 写代码

- 严格遵循 [项目编码规范.md](../../rules/项目编码规范.md) 的全部 14 条
- 不做超出 tasks.md 范围的改动（防熵累积）
- 每改完一个 Task，对照 tasks.md 的"验收"项自检：是否满足？

### Step 5: 写 coding_report

参考 [generator.md Step 3](../../agents/generator.md#step-3-完成所有-task-后回写-coding_report) 的模板。

必含：
- 改动文件清单（行数变化）
- 任务完成情况表（每项 ✅/⏸/❌ 状态）
- 关键决策（WHY 不显然时必写）
- 自测情况（哪些边界已验证）
- 已知未做（不在本次范围的事项，避免被误以为漏做）

---

## 常见错误（评审会打 MUST FIX）

| 错误 | 违反的规则 |
|---|---|
| `float price = 1.99f;` | 规范 §2 数值类型 |
| `Resources.Load<...>(...)` | 规范 §4 资源加载 |
| `Debug.Log("...")` | 规范 §6 日志 |
| `StartCoroutine(SomeOtherModule...)` | 规范 §5 异步与并发 |
| 主工程类 `new ModXxx()` | 规范 §7 主-热更边界 |
| `Mod` 类未继承 `ModuleBase` | 规范 §1 命名前缀 |
| 改了 `coding/review/` 下文件 | Generator 工具边界 |
| tasks.md 范围之外的"顺手重构" | 规范 §13 文件级约束 |

---

## 性能与稳定性自检（可选但推荐）

- 每帧执行的代码（Update / FixedUpdate）有没有不必要的分配？
- 监听事件的地方有没有对应的反注册（防内存泄漏）？
- async 方法的异常路径有没有 `try/catch`？
- 跨场景的 Module 引用会不会因为 Dispose 而失效？
