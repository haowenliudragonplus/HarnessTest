---
name: expert-reviewer
description: Evaluator Agent 的标准评审 SOP；包含 Plan Review 与 Execution Review 两套 checklist
---

# Skill: 专家评审（Expert Reviewer）

> 由 Evaluator Agent 在阶段 2（计划评审）、4（编码评审）启动时加载。

---

## 两种评审模式

### 模式 A: Plan Review（计划评审）

输入：`spec.md` + `tasks.md`
输出：`request_analysis/review/spec_review_v{N}.md`

#### Plan Review 检查清单

**spec.md 完整性**
- [ ] 7 章节齐全（背景 / 目标 / 不做什么 / 影响面 / 风险 / 待决议 / 验收）
- [ ] 背景含用户原话完整 quote
- [ ] 目标可测量（不是"提升体验"这种空话）
- [ ] 不做什么明确划界
- [ ] 影响面用 Grep/Glob 实测（**禁止**模糊词"少量""若干""一些"）
- [ ] 风险表识别了所有高敏感模块（ModAccount / ModBag / ModIAP / 广告 / 网络）
- [ ] 待决议问题未被 Planner 自答
- [ ] 验收标准可被机器或人工**明确验证**

**tasks.md 完整性**
- [ ] 每项 Task 含输入/输出/验收/依赖/预估
- [ ] 依赖关系无环（拓扑可调度）
- [ ] 拆分粒度合理（30 分钟 ~ 2 小时 / Task）
- [ ] 所有 Task 加起来能完成 spec 的"目标"
- [ ] 所有 Task 不超过 spec 的"不做什么"边界

**逻辑一致性**
- [ ] 影响面中提到的文件，在 tasks 中有对应改动
- [ ] tasks 中提到的改动，在 spec 影响面中已声明
- [ ] 风险表中的"高"风险项，tasks 中有对应缓解动作

---

### 模式 B: Execution Review（执行评审）

输入：`coding_report_v{N}.md` + 实际代码改动（用 Bash `git diff` 拿）
输出：`coding/review/code_review_v{N}.md`

#### Execution Review 检查清单

**报告 vs 实际一致性**
- [ ] coding_report 中宣称的改动文件清单 = `git diff --name-only` 的结果
- [ ] 每项 Task 在报告中有明确状态（✅/⏸/❌）
- [ ] 跳过/未完成的 Task 给了正当理由

**编码规范合规性（逐条检查 [项目编码规范.md](../../rules/项目编码规范.md)）**
- [ ] §1 命名前缀正确（Mod*/FsmState_*/UIView_*/Procedure*）
- [ ] §2 货币/数量字段是 long，非 float/double
- [ ] §3 模块访问用 `Game.GetMod<T>()`
- [ ] §4 资源加载走 ModAsset（除 Boot 阶段）
- [ ] §5 异步用 UniTask，无跨模块 coroutine
- [ ] §6 日志用 CLog
- [ ] §7 主工程未 new 热更类
- [ ] §8 第三方 SDK 调用有超时和降级，封装在 Adapter 层
- [ ] §9 事件用 ModEvent.Dispatch
- [ ] §10 UI 打开走 ModUI.OpenSync/OpenAsync
- [ ] §11 存档用 SDK<IStorage>，无 PlayerPrefs
- [ ] §12 commit message 格式（如已提交则查）
- [ ] §13 无 tasks.md 范围之外的"顺手重构"
- [ ] §14 注释克制、必要时加 WHY

**业务正确性**
- [ ] 改动是否兑现 spec.md 的"目标"
- [ ] 验收标准是否能在改动后达成
- [ ] 边界条件（null / 空集合 / 超时 / 重复触发）是否处理
- [ ] 高敏感模块（账号 / 内购 / 数据持久化）有无意外破坏
- [ ] 主-热更边界是否正确

**反熵检查**
- [ ] 没有引入新的"代码异味"（如重复定义、死代码、注释掉的代码段）
- [ ] 没有降级现有的代码风格（如把 `Game.GetMod<>` 改成 `static Instance`）

---

## 评审意见的写法

参考 [evaluator.md 第 3 节](../../agents/evaluator.md#3-评审意见格式硬约束)。

每条意见必含：**位置 / 现状 / 问题 / 建议**。

### 严重度分级

| 级别 | 触发条件 | 影响 |
|---|---|---|
| **MUST FIX** | 违反硬约束（编码规范任一条）/ 业务正确性错误 / 安全/稳定性风险 | 必须返工 |
| **LOW** | 风格不一致 / 可读性 / 非关键性能 | 不阻断，建议改 |
| **INFO** | 提醒 / 团队学习 / 可优化但非必须 | 仅供参考 |

### 通过时也要给 INFO

不要写"无问题，全通过"——这是空洞的。即使没有 MUST FIX，也要给至少 1 条 INFO（哪怕是"本次改动遵循了 X 模式，值得团队留意"）。

---

## 常见 MUST FIX 范例

```markdown
### 【MUST FIX】价格字段使用 float
- **位置**：Assets/Scripts/Hotfix/Common/Module/IAP/ModIAP.cs:87
- **现状**：
  ```csharp
  public float currentPrice;
  ```
- **问题**：违反 [项目编码规范.md §2](../../rules/项目编码规范.md)。
  IEEE 754 浮点表示无法精确存储 1.99 等常见价格值，长期会出现累积偏差，
  对内购金额来说是合规风险。
- **建议**：改为 `public long currentPriceCent;`（单位：分），UI 显示时再除以 100。

### 【MUST FIX】UI 视图直接 Instantiate prefab
- **位置**：Assets/Scripts/Hotfix/Common/Module/Tip/ModTip.cs:42
- **现状**：
  ```csharp
  Instantiate(tipPrefab, parent);
  ```
- **问题**：违反 §10，绕过 ModUI 生命周期管理，会导致：
  1. UI 关闭时不会自动清理
  2. 多语言切换不会重新刷新
- **建议**：改用 `Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Tip, openData);`
```
