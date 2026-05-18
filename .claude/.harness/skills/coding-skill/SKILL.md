---
name: coding-skill
description: Generator Agent 的标准编码 SOP；包含分层 Spec 的导航
---

# Skill: 编码实现（Coding Skill）

> 由 Generator Agent 在编码阶段与编码返工阶段启动时加载。

---

## 工作流

### Step 1: 理解 + 装备

按 tasks.md 列出的每项 Task，先识别**改动涉及的代码分层**（依据 [.claude/.harness/rules/工程结构.md](../../rules/工程结构.md) 的分层定义），然后从本目录下选读对应的分层 Spec。

本目录提供以下分层 Spec（各项目按自己的分层情况裁剪/扩展）：

| 改动涉及 | 必读分层 Spec |
|---|---|
| 启动流水线 / 引导流程 | [procedure-spec.md](procedure-spec.md) |
| 业务模块 | [module-spec.md](module-spec.md) |
| 状态机 | [fsm-spec.md](fsm-spec.md) |
| UI 视图 | [ui-view-spec.md](ui-view-spec.md) |
| 主工程↔热更/插件边界 | [hotfix-boundary-spec.md](hotfix-boundary-spec.md) |
| 第三方 SDK / 适配器 | [adapter-spec.md](adapter-spec.md) |

跨分层改动 → 多份都读。如项目不存在某一层，对应 Spec 留空即可。

### Step 2: Read 同类型范例

对每类改动，至少 Read 项目内**一个现有的同类文件**作风格参考——具体选哪个由你按 [.claude/.harness/rules/工程结构.md](../../rules/工程结构.md) 中定义的分层定位。

### Step 3: 改动前 Read 全文

**任何文件**在 Edit 之前必须先 Read 全文，**没有例外**（防 Anthropic Failure Mode 2: Premature Victory）。

### Step 4: 写代码

- 严格遵循 [项目编码规范.md](../../rules/项目编码规范.md) 的全部条款
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

任何对 [项目编码规范.md](../../rules/项目编码规范.md) 中条款的违反都会被评审打 MUST FIX。
此外还需避免以下流程级错误：

| 错误 | 违反的规则 |
|---|---|
| 改了 `coding/review/` 下文件 | Generator 工具边界（[generator.md §4](../../agents/generator.md#4-硬性约束absolute-never)） |
| 改了 `spec.md` / `tasks.md` | 同上 |
| 触碰版本控制（任何写操作） | 同上 |
| tasks.md 范围之外的"顺手重构" | 防熵累积 |
| Edit 之前没 Read 全文 | 防 Premature Victory |

---

## 性能与稳定性自检（可选但推荐）

- 高频回调（每帧 / 每 tick / 每事件）中有没有不必要的分配？
- 监听/订阅的地方有没有对应的反注册（防内存泄漏）？
- async 方法的异常路径有没有保护（try/catch 或等价机制）？
- 跨生命周期的引用会不会因为对象销毁而失效？
