# Harness 体系搭建（MVP 版）

> **参考文章**：[Harness Engineering：耗时一周，我是如何将应用的 AI Coding 率提升至 90%的](https://mp.weixin.qq.com/s/rlIyIIZOXFObNIXbPI7gDg)
>
> 目标项目：HarnessTest（Unity 2022.3.62f2c1 + HybridCLR 热更 + YooAsset）

---

## Context

**为什么做这个改动**

当前项目 `c:\Users\haowen.liu\Documents\HarnessTest` 没有任何 AI Coding 工程化配置——没有 `CLAUDE.md`、没有 `.harness/`、没有 agents/rules/skills、没有规则文档。开发者每次使用 Claude Code 都要靠口头说明项目结构和约束，存在文章中描述的四种典型失败模式：one-shot syndrome、premature victory、premature completion、cold start。

参考文章方法论：把"标准是什么 / 该怎么做 / 系统是什么样 / 做了什么"四类隐性知识显式化到 `.claude/.harness/` 目录，搭配 Anthropic 提出的 **Planner / Generator / Evaluator** 三角色独立分工（文章 2.3 节明确"将做事的 Agent 和评判的 Agent 分开，是一个强有力的杠杆"），形成"需求 → Planner 出计划 → Generator 写代码 → Evaluator 评审 → 用户手动提交"的多 Agent 流水线。

**核心交付物**：三个真正独立执行的 Agent（通过 Claude Code Task 工具调用，各自拥有独立上下文窗口，从根上规避 Failure Mode 1: one-shot syndrome）。

**MVP 范围**
- `HarnessTest/.claude/.harness/` 单一目录承载所有 Harness 资产（agents / skills / rules / wiki / changes 五个子目录）
- `.claude/CLAUDE.md` 作为 Index & Map + 多 Agent 编排入口（~150 行）
- 三个独立 Agent 定义：**Planner / Generator / Evaluator**（中文名：规划者 / 实现者 / 评审者）
- 3 个支撑 Skill（由 Agent 按需 Read）：`request-analysis` / `coding-skill` / `expert-reviewer`
- 3 份 Rules：工程结构 / 开发流程规范 / Unity 项目编码规范
- 10 阶段流水线，CI/部署/git 提交三项均为人工 checklist

**MVP 不做**（留给 Phase 2）
- 6 个扩展 Skill（unit-test-write/ci/deploy-verify/code-review/project-analysis/aone-ci-generate）
- `.claude/settings.json` hook 自动化门禁
- 接入 Unity Test Framework
- 填充 Wiki 业务知识库

**语言**：中文为主，`Skill / Procedure / Module / Quality Gate / Audit Trail / Planner / Generator / Evaluator` 等术语保留英文。

---

## 目录结构（终态）

按用户要求，全部资产收敛到 `.claude/.harness/` 单一目录下。CLAUDE.md 放在 `.claude/CLAUDE.md`（与 `.claude/.harness/` 同层；该位置 Claude Code 会在**会话启动时自动加载**，等同于项目根的 CLAUDE.md，依据 [官方 memory 文档](https://code.claude.com/docs/en/memory.md)）。

```
HarnessTest/
└── .claude/
    ├── CLAUDE.md                          # 新增：Index & Map + 自动编排逻辑（~150 行）
    ├── skills/                            # 已有（docx/pdf/pptx/xlsx/skill-creator/find-skills，不动）
    └── .harness/                          # 新增 Harness 根目录
        ├── agents/                        # 三个独立 Agent 角色定义
        │   ├── planner.md                 # 规划者（Planner）
        │   ├── generator.md               # 实现者（Generator / Coder）
        │   └── evaluator.md               # 评审者（Evaluator / Reviewer）
        ├── skills/                        # 支撑性 SOP，由 Agent 按需 Read
        │   ├── request-analysis/SKILL.md
        │   ├── coding-skill/
        │   │   ├── SKILL.md
        │   │   ├── procedure-spec.md
        │   │   ├── module-spec.md
        │   │   ├── fsm-spec.md
        │   │   ├── ui-view-spec.md
        │   │   ├── hotfix-boundary-spec.md
        │   │   └── adapter-spec.md
        │   └── expert-reviewer/SKILL.md
        ├── rules/
        │   ├── 工程结构.md
        │   ├── 开发流程规范.md
        │   └── 项目编码规范.md
        ├── wiki/
        │   └── README.md                  # Phase 2 占位
        └── changes/
            └── README.md                  # 变更目录命名与模板说明
```

---

## 多 Agent 执行模型（核心设计）

### 三个 Agent 的真实执行机制

按用户要求"多个 agent 自动执行"，本方案采用 **Claude Code Task 工具调用 + 主会话自动编排** 的多 Agent 架构。**主会话在两个 HITL 确认点之间自动串起所有 Agent 调用，用户不需要敲 `/generator`、`/evaluator` 等中间指令**：

```
用户：「我有个需求：XXX」
   │
   ▼  主会话自动调用（不需用户指令）
┌──────────────┐
│   Planner    │  独立上下文｜Read 角色+Rules｜产出 spec.md/tasks.md
└──────┬───────┘
       │ 主会话自动接力
       ▼
┌──────────────────────────┐
│ Evaluator: Plan Review   │  独立上下文｜Read spec/tasks｜产出 spec_review_vN.md
└──────┬───────────────────┘
       ├── MUST FIX → 主会话自动调 Planner-revise → 回循环（≤3 轮）
       └── APPROVED ▼
═══════════════ HITL #1：用户确认 spec ═══════════════
       用户："确认" / "通过" / "继续"
       ▼  主会话自动接力
┌──────────────┐
│  Generator   │  独立上下文｜Read spec/tasks/coding-skill｜产出代码+coding_report
└──────┬───────┘
       │ 主会话自动接力
       ▼
┌────────────────────────────────┐
│ Evaluator: Execution Review    │  独立上下文｜Read code diff｜产出 code_review_vN.md
└──────┬─────────────────────────┘
       ├── MUST FIX → 主会话自动调 Generator-revise → 回循环（≤2 轮）
       └── APPROVED ▼
═══════════════ HITL #2：用户确认代码 ═══════════════
       用户："确认"
       ▼  主会话输出
┌────────────────────────────────────────────┐
│ 人工 checklist：                             │
│   1. git add ... && git commit -m '...'    │
│   2. Unity Editor 点 Play 验证 X/Y/Z         │
└────────────────────────────────────────────┘
```

**关键特性**：
1. **每次 Task 调用 = 独立上下文窗口**——三个 Agent 不共享对话历史，只通过文件系统传递产出物（spec.md / tasks.md / 代码 / review.md）。这正是文章 4.3 节"持久化在文件系统而非上下文窗口"的落地。
2. **主会话只做编排不做实际工作**——它不写代码、不写计划、不写评审；它的核心职责是"读取 Agent 产出 → 判断分支 → 自动触发下一个 Agent"。
3. **HITL 收敛到 2 个关键点**（替代文章原 5 个）——只在"看完计划准备开始写代码"和"看完代码准备交付"两处暂停等用户。MUST FIX 返工循环全自动（≤上限），超限才升级人工。

### 主会话自动编排的判定逻辑

主会话每次收到来自 Agent 的 Task 返回后，按以下规则决定下一步动作（写入 `.claude/CLAUDE.md`）：

| 当前状态（文件系统） | 下一步动作 |
|---|---|
| Planner 刚返回，`spec.md` + `tasks.md` 存在，无 `spec_review_v*.md` | 自动调 Evaluator(Plan Review) |
| `spec_review_v{N}.md` 含 MUST FIX，N < 3 | 自动调 Planner-revise（携带 review 文件路径） |
| `spec_review_v{N}.md` 含 MUST FIX，N ≥ 3 | 升级人工：暂停并向用户汇报 |
| `spec_review_v{N}.md` 全 APPROVED，无 `coding_report_v*.md` | **暂停 HITL #1**：摘要展示 spec + review，等用户回复 |
| 用户回复"确认"，无 `coding_report_v*.md` | 自动调 Generator |
| Generator 刚返回，`coding_report_v{N}.md` 存在，无 `code_review_v{N}.md` | 自动调 Evaluator(Execution Review) |
| `code_review_v{N}.md` 含 MUST FIX，N < 2 | 自动调 Generator-revise |
| `code_review_v{N}.md` 含 MUST FIX，N ≥ 2 | 升级人工 |
| `code_review_v{N}.md` 全 APPROVED | **暂停 HITL #2**：摘要展示 diff + review，等用户回复 |
| 用户回复"确认"且 `code_review` 已 APPROVED | 输出人工 checklist（git commit + Unity 验证），更新 `summary.md` 标 ACK |

### 三个 Agent 的职责与工具边界

| Agent | 中文名 | 职责（涉及的 10 阶段） | 允许的工具 | 禁止的工具 |
|---|---|---|---|---|
| **Planner** | 规划者 | 阶段 1 需求分析、阶段 2 计划返工 | Read, Glob, Grep, Write（仅限 `.claude/.harness/changes/*/request_analysis/`） | Edit（任何 `Assets/`）、Bash |
| **Generator** | 实现者 | 阶段 3 编码、阶段 5 单测编写（MVP 内人工）、产出 coding_report | Read, Edit, Write, Bash（仅限只读命令如 `dotnet build`、git 状态查询；**禁止 git commit/push**） | Write 评审报告路径 |
| **Evaluator** | 评审者 | 阶段 2/4/6 评审 | Read, Glob, Grep, Write（仅限 `.claude/.harness/changes/*/review/` 和 `.../coding/review/`） | Edit（任何代码）、Bash |

**关键约束（强杠杆）**：
- Evaluator **物理上没有 Edit 权限** → 它即使想改也改不了，必然只能输出评审意见
- Planner **物理上没有 Bash + 不能 Edit Assets/** → 它不能"提前实现"，只能产文档
- Generator **物理上不能写 review/ 目录** → 它不能给自己写"通过评审"的报告
- 所有 Agent **物理上不能 git commit/push** → 提交永远由人手动做

### 编排流程（10 阶段映射 + 谁触发）

| # | 阶段 | 触发方式 | 主会话动作 | Agent 上下文输入 |
|---|---|---|---|---|
| 1 | 需求分析 | 用户首次输入需求 | **自动** Task → Planner | 用户原需求 + 必读 `.claude/.harness/rules/*` |
| 2 | 需求评审 | **自动**（Planner 返回后立即） | Task → Evaluator(Plan Review) | spec.md + tasks.md 路径 |
| 2'| 计划返工 | **自动**（review 含 MUST FIX，N<3） | Task → Planner（带 review 反馈） | 上轮 spec.md + spec_review_vN.md |
| —— | **HITL #1** | 主会话暂停等用户 | 摘要展示 spec + review，等"确认" | — |
| 3 | 编码实现 | **自动**（用户 HITL #1 确认后） | Task → Generator | 已 APPROVED 的 spec.md + tasks.md |
| 4 | 编码评审 | **自动**（Generator 返回后立即） | Task → Evaluator(Execution Review) | 代码 diff + coding_report.md |
| 4'| 编码返工 | **自动**（review 含 MUST FIX，N<2） | Task → Generator（带 review 反馈） | 上轮代码 + code_review_vN.md |
| 5 | 单元测试编写 | ⏭️ Phase 2 | — | — |
| 6 | 单元测试评审 | ⏭️ Phase 2 | — | — |
| —— | **HITL #2** | 主会话暂停等用户 | 摘要展示 diff + review，等"确认" | — |
| 7 | **代码推送** | **用户手动 git** | 主会话输出"请手动 git add/commit"提示 | — |
| 8 | CI 验证 | 用户手动 | 主会话输出 Unity Editor checklist | — |
| 9 | 部署验证 | 用户手动 | 主会话输出 checklist | — |
| 10 | 用户最终 ACK | 用户回复 | 主会话更新 summary.md | — |

**自动化与人工的边界**：
- ✅ 自动：Planner / Evaluator / Generator / Evaluator 之间的接力、MUST FIX 返工循环
- ⏸ 暂停等人：HITL #1（确认计划进入编码）、HITL #2（确认代码进入交付）
- 🚨 升级人工：spec 评审 ≥3 轮仍 MUST FIX、code 评审 ≥2 轮仍 MUST FIX
- 👤 完全人工：git commit / Unity Editor 实跑 / 部署

### Agent 间不共享上下文如何避免重复工作

每个 Agent 启动时 Read 三类文件即可获得完整上下文：
1. 自己的角色定义（`.claude/.harness/agents/<name>.md`）
2. 全局规则（`.claude/.harness/rules/*.md`）
3. 本次变更目录下的前序产出物（`spec.md` / `tasks.md` / `code_review_v1.md`）

文件系统 = 持久化记忆，这正是文章 2.3 节"支柱三：持久化记忆"的实现。

---

## 关键设计决策

### 1. 为何把 `.harness/` 放在 `.claude/` 下

用户指定路径 `HarnessTest/.claude/.harness/`。优点：
- 与 Claude Code 配置同层，物理上表明"这是给 AI 用的"
- 不污染项目根目录
- 仍然会被 git 跟踪（与 `.claude/skills/` 一样）

**注意**：Claude Code 原生只自动发现 `.claude/skills/*/SKILL.md` 和 `.claude/agents/*.md`。`.claude/.harness/` 下的内容 Claude Code **不会自动加载**——这正是我们想要的：所有 Harness 资产只在 Agent 被调用时按需 Read，避免 Failure Mode 1 上下文过载。`.claude/CLAUDE.md` 是唯一的 always-loaded 入口（依据[官方 memory 文档](https://code.claude.com/docs/en/memory.md)）。

### 2. Unity 项目的"分层编码 Spec"映射

文章给出 Java 项目 8 份分层 Spec。本项目重新映射为 6 份，放在 `.claude/.harness/skills/coding-skill/`：

| 文章（Java 企业级） | 本项目（Unity 热更游戏） | 对应代码路径 |
|---|---|---|
| Controller / RPC Provider | Procedure 层 | `Assets/Scripts/Main/Boot.cs` + `Hotfix/.../Procedure*.cs` |
| Service 业务逻辑 | Module 层 | `Assets/Scripts/Hotfix/Common/Module/` |
| Domain 流程编排 | FSM 状态机 | `Hotfix/.../FsmState_*.cs` |
| 接口文档 / View | UI 层 | `Hotfix/.../UIView_*.cs` |
| Adapter 外部依赖 | 第三方 SDK 封装 | IAP / AppLovin / Firebase 等模块 |
| —（Unity 特有） | 主工程↔热更工程边界 | `Loader.OnUpdate?.Invoke()` 桥接、HybridCLR DLL 加载 |

Generator Agent 在编码时按改动涉及的层级按需 Read 对应 Spec。

### 3. Git 提交完全人工

文章原文 Generator 包含 commit 动作，本方案**显式收回**：
- 三个 Agent 的工具集都不含 git 写操作
- 阶段 7 改为主会话输出提示："Evaluator 已通过，请你手动执行 `git add` / `git commit` / `git push`，commit message 格式建议 `【feat/fix/...】<描述>`"
- 用户提交后回到主会话继续阶段 8-10

### 4. 质量门禁（MVP 不上 hook）

文章强调 "If it can't be mechanically enforced, the agent will drift"。MVP 阶段先用 Agent prompt 内的"必须校验"清单约束，**不**写 `.claude/settings.json` hook。Phase 2 把以下检查机械化：
- `summary.md` 必填章节存在性
- 评审报告必须包含 MUST FIX / LOW / INFO 三级标签
- 变更目录命名格式 `{type}-{name}-{YYYYMMDD}`

---

## 关键文件内容草案

### `.claude/CLAUDE.md`（~150 行 Index & Map + 自动编排逻辑）

放在 `.claude/CLAUDE.md`（不在项目根，不在 `.harness/` 内）—— Claude Code 会在会话启动时自动加载该位置的文件，等同于根目录优先级。

6 段结构：

1. **项目身份**（10 行）：Unity 2022.3.62f2c1 / HybridCLR / YooAsset / 主-热更分离 / `Loader.OnXxx` 桥接

2. **主会话角色与自动编排原则**（15 行）：
   ```
   你是 HarnessTest 项目的主会话 Orchestrator。你的核心职责是：
   - 接收用户需求和确认
   - 通过 Task 工具调用 Planner / Generator / Evaluator
   - 读取 Agent 产出文件，按状态机自动判定下一步
   - 在 HITL 点暂停等用户，其他时候自动接力，不要求用户敲中间指令
   你自己不写代码、不写计划、不写评审。
   ```

3. **自动编排状态机**（40 行）—— 完整 11 行判定表（与"主会话自动编排的判定逻辑"一致），包括：
   - Planner→Evaluator(Plan)→Generator→Evaluator(Code)→人工 checklist 的自动接力规则
   - MUST FIX 返工的自动 retry 与上限
   - HITL #1 / #2 的具体触发条件和暂停时展示什么
   - 每条规则附带"如何用 Task 工具调用"的示例 prompt

4. **配置中枢索引表**（30 行）：列出 `.claude/.harness/{agents,skills,rules,wiki,changes}` 路径与触发场景

5. **硬性约束**（25 行）：主会话**绝不**自己 Edit `Assets/`、绝不写评审报告、绝不 git commit；所有实际工作必须通过 Task 调 Agent 完成

6. **核心命令快查**（15 行）：Unity Editor 启动方式、HybridCLR DLL 编译、commit message 格式（`【chore/feat/fix】<中文>`）

### `.claude/.harness/agents/planner.md`（~250 行）

- 模块一：角色背景——"你是 Planner，运行在独立 Task 上下文中。你只产出文档（spec.md / tasks.md），永远不写代码、不评审。"
- 模块二：启动序列——
  1. Read 三份 Rules（必读）
  2. Read `.claude/.harness/skills/request-analysis/SKILL.md`
  3. Glob `.claude/.harness/changes/` 找今天是否已存在变更目录
- 模块三：四项核心职责——需求澄清 / 任务拆解 / 风险识别 / 待决议问题列表
- 模块四：调度指令——
  - spec.md 必填章节：背景 / 目标 / 不做什么 / 影响面 / 风险 / 待决议
  - tasks.md 每项必含：输入 / 输出 / 验收标准 / 依赖
  - 完成后必须以**机器可解析的结束语句**收尾：`✅ Planner DONE. Outputs: <路径>。Status: READY_FOR_REVIEW.` —— 主会话依此自动接力调 Evaluator
- 模块五：硬性约束——Edit/Bash 工具不可用；禁止替用户决策待决议问题，必须列出来请人决定

### `.claude/.harness/agents/generator.md`（~280 行）

- 模块一：角色背景——"你是 Generator，唯一允许修改 Assets/ 下代码的 Agent。永远不写评审、不 git commit。"
- 模块二：启动前置检查（任一失败立即终止并报错）——
  1. 存在已 APPROVED 的 `spec.md` + `tasks.md`（必读全文）
  2. Read `.claude/.harness/rules/项目编码规范.md`
  3. Read `coding-skill/SKILL.md` 并按改动层级 Read 对应 *-spec.md
  4. Read 任务所涉及的既有代码（强制 Anthropic Failure Mode 2 防御）
- 模块三：编码流程
  - 一次只完成 tasks.md 中的一项，完成后回写 coding_report_v1.md（含改动文件清单 / 关键决策 / 自测情况）
  - 每改一个文件前必须先 Read 全文
  - 价格/货币字段强制 `long`（单位最小分），禁用 `float/double`
- 模块四：完成动作——
  - 完成后以机器可解析的结束语句收尾：`✅ Generator DONE. Outputs: <路径>。Modified files: <文件清单>。Status: READY_FOR_REVIEW.` —— 主会话依此自动接力调 Evaluator(Execution Review)
  - **绝不 git commit**——所有提交在 HITL #2 通过后由用户手动执行
- 模块五：硬性约束——
  - 不能 Write 到 `review/` 目录
  - 不能执行 `git add/commit/push`（Bash 工具范围限制）
  - 禁止跳过 Read 直接 Edit
  - 禁止超出 tasks.md 范围的"顺手重构"

### `.claude/.harness/agents/evaluator.md`（~250 行）

- 模块一：角色背景——"你是 Evaluator，唯一允许写评审报告的 Agent。你没有 Edit 工具，无法改代码——你的力量来自这种物理隔离。"
- 模块二：两种评审模式
  - **Plan Review**：触发条件 `spec_review_v*.md` 缺失；输入 spec.md + tasks.md；输出 `spec_review_v{N}.md`
  - **Execution Review**：触发条件 `code_review_v*.md` 缺失；输入 coding_report.md + `git diff`（用 Bash 调用）+ 相关代码；输出 `code_review_v{N}.md`
- 模块三：评审意见格式（硬约束）
  ```
  ### 【MUST FIX | LOW | INFO】<问题简述>
  - 位置：<文件路径:行号>
  - 现状：<引用代码或文档片段>
  - 问题：<具体描述>
  - 建议：<修改方向>
  ```
- 模块四：循环上限与结束语句（主会话靠这条解析下一步）——
  - spec 评审 ≤3 轮、code 评审 ≤2 轮
  - 评审通过收尾：`✅ Evaluator DONE. Verdict: APPROVED. Mode: <plan|code>. Round: N/M.` → 主会话据此暂停 HITL #1（plan）或 HITL #2（code）
  - 评审驳回未超限：`⚠️ Evaluator DONE. Verdict: REVISION_REQUIRED. Mode: <plan|code>. Round: N/M. Must-fix count: X.` → 主会话自动调原作者 Agent 返工
  - 超限：`🚨 Evaluator DONE. Verdict: ESCALATE_TO_HUMAN. Mode: <plan|code>. Round: N/M (cap reached).` → 主会话暂停等人工
- 模块五：硬性约束
  - 不可 Edit/Write 任何 `Assets/` 或 `.claude/.harness/agents|skills|rules/` 下文件
  - 不可判定通过未做端到端验证的功能（防 Failure Mode 3）
  - 必须给每个 tasks.md 中的任务项独立判定通过/驳回

### `.claude/.harness/rules/工程结构.md`

记录 `Assets/` 物理分层：
- `Scripts/Main/` — MonoBehaviour 入口（参考 `Boot.cs`）
- `Scripts/Hotfix/Framework/Module/` — 基础设施模块（Asset/Audio/Camera/Config/FSM/UI/Timer/Event）
- `Scripts/Hotfix/Common/Module/` — 业务模块（Account/Bag/IAP/Language/Storage/...）
- `Scripts/Hotfix/Activity/` — 活动模块
- `Scripts/Hotfix/InGame/` — 游戏内逻辑
- `Scripts/Hotfix/Global/` — 全局 UI（Fly/Popup）

### `.claude/.harness/rules/开发流程规范.md`

10 阶段流水线 + 三 Agent 编排的人类可读说明（与 CLAUDE.md 的 Agent 调度表互为镜像，但更详细）。

### `.claude/.harness/rules/项目编码规范.md`

不变约束：
- 命名：`Mod*` / `FsmState_*` / `UIView_*` / `Procedure*` 前缀强制
- 主工程禁止直接 new 热更工程类；热更工程通过 `Loader.OnXxx?.Invoke()` 接收 MonoBehaviour 事件
- 资源加载统一走 `ModAsset`（除 Boot 阶段外禁裸调 `Resources.Load`）
- 异步统一用 UniTask
- commit message 格式：`【chore/feat/fix/refactor】<中文描述>`（沿用 `c6f7c99f 【chore】添加常用的skills` 的现有约定）

### `.claude/.harness/skills/{request-analysis,coding-skill,expert-reviewer}/SKILL.md`

由对应 Agent 启动时 Read。详见前文"分层编码 Spec 映射"和各 Agent 调度指令。

### `.claude/.harness/changes/README.md`

定义变更目录命名 `{type}-{name}-{YYYYMMDD}/` 和子目录模板：
```
{type}-{name}-{YYYYMMDD}/
├── summary.md
├── request_analysis/
│   ├── spec.md
│   ├── tasks.md
│   └── review/spec_review_v1.md
├── coding/
│   ├── coding_report_v1.md
│   └── review/code_review_v1.md
└── (后续 unit_test/ ci_result/ deployment/ 留空给 Phase 2)
```

---

## 关键参考代码

设计时需要直接 Read 以保证准确：
- `Assets/Scripts/Main/Boot.cs` — 主工程入口、MonoBehaviour 生命周期桥接
- `Assets/Scripts/Hotfix/Framework/Module/ModuleBase.cs` — Module 基类，写 module-spec.md 前必读
- `Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs` — 典型业务 Module 范例
- `Assets/Scripts/Hotfix/Framework/Module/FSM/ModFsm.cs` — FSM 框架，写 fsm-spec.md 前必读

---

## Verification（如何端到端验证）

MVP 完成后，按以下顺序 Dry Run（文章 5.1 节方法）：

### 验证 1：完整自动链式流水线
1. 启动新 Claude Code 会话 → 确认 `.claude/CLAUDE.md` 自动加载（检查首轮上下文）
2. 抛虚拟需求一次："给 ModTip 增加一个限制最大并发显示数量的配置"
3. 观察主会话**自动**依次调用 Task：Planner → Evaluator(Plan) — **用户全程不需要再输入指令**
4. 主会话暂停于 HITL #1，展示 spec + review 摘要，等用户回复
5. 用户回复"确认"
6. 观察主会话**自动**依次调用 Task：Generator → Evaluator(Code)
7. 主会话暂停于 HITL #2，展示 diff + review 摘要
8. 用户回复"确认"
9. 主会话输出人工 checklist（git commit + Unity Editor 验证），**不替你执行 git**

预期：从抛出需求到收到 checklist，用户只输入了 3 次（需求 + 两次"确认"），其他全自动。

### 验证 2：上下文隔离（多 Agent 真实独立）
- Planner Task 调用结束后，问主会话"刚才 Planner 详细看了哪些文件？" → 主会话不应有详细记忆（因为 Task 子上下文隔离）
- 这正是想要的效果——避免主会话上下文被各 Agent 的细节污染

### 验证 3：工具边界
- Evaluator 被调用时尝试让它 Edit 一个文件 → 它应拒绝（工具不可用）
- Generator 被调用时尝试让它写 `review/code_review_v2.md` → 它应拒绝
- Planner 被调用时尝试让它 git commit → 它应拒绝

### 验证 4：硬性约束（业务层）
- 让 Generator 写 `float price = 1.99f;` → Evaluator 阶段必拦截
- 让 Planner 自作主张回答"待决议问题" → 它应改成列出问题等用户

### 验证 5：人工 checklist
- 阶段 7-9 应输出明确的人工 checklist（如"请手动：`git add Assets/Scripts/Hotfix/Common/Module/Tip/` && `git commit -m ...`；然后在 Unity Editor 中点 Play 验证 X / Y / Z"），不替用户执行

### 验证 6：自动返工循环与上限
- 故意制造一个会被 Evaluator MUST FIX 的 spec → 观察主会话**自动**调 Planner-revise（不需用户指令）
- 让它失败 3 次 → 主会话收到 `ESCALATE_TO_HUMAN` 后暂停升级人工

### 验证 7：CLAUDE.md 加载位置
- 删除 `.claude/CLAUDE.md`，启动新会话 → 验证主会话不再自动编排（确认我们的加载位置依赖是正确的）
- 恢复后再次启动 → 验证恢复正常

---

## Phase 2 扩展项（本计划不实现）

1. 增补 6 个 Skill：unit-test-write / unit-test-ci / deploy-verify / code-review / project-analysis / aone-ci-generate
2. 把 `.claude/.harness/agents/*.md` 同步到 `.claude/agents/` 作为薄壳，让 Claude Code 原生 subagent 触发也能用
3. `.claude/settings.json` hooks：变更目录命名校验、summary.md 必填章节、commit message 格式
4. 接入 Unity Test Framework，把 CI 阶段从人工 checklist 升级为机械化门禁
5. 填充 `.claude/.harness/wiki/`（账号/背包/IAP/广告链路梳理）
6. 扩展 Agent 矩阵：Performance Auditor / Security Scanner / Documentation Sync

---

## 实施进度（已完成）

✅ **本计划已于 2026-05-18 落地完成**，共写入 18 个文件：

| 类别 | 数量 | 路径 |
|---|---|---|
| 主入口 | 1 | `.claude/CLAUDE.md`（含 11 条状态机判定表） |
| Agents | 3 | `.claude/.harness/agents/{planner,generator,evaluator}.md` |
| Rules | 3 | `.claude/.harness/rules/{工程结构,开发流程规范,项目编码规范}.md` |
| 主 SKILL | 3 | `.claude/.harness/skills/{request-analysis,coding-skill,expert-reviewer}/SKILL.md` |
| 分层 Spec | 6 | `.claude/.harness/skills/coding-skill/{procedure,module,fsm,ui-view,hotfix-boundary,adapter}-spec.md` |
| 占位 README | 2 | `.claude/.harness/{changes,wiki}/README.md` |

**未修改任何 Unity 工程代码**（`Assets/`、`ProjectSettings/`、`Packages/`）。
**未执行 git 提交**——留给用户手动操作。

### 下一步建议

1. 开新 Claude Code 会话（让 `.claude/CLAUDE.md` 自动加载）
2. 执行上述"验证 1"——抛一个虚拟需求观察自动链式流水线
3. 实战中遇到问题 → 修 Harness 文件而非绕过（参考文章 5.5 节：每发现一个错误就工程化消除）
