# 变更管理目录

> 每个需求的全流程产出物都按变更目录组织，构成完整的 Audit Trail。

---

## 变更目录命名

格式：`{type}-{name}-{YYYYMMDD}`

| 字段 | 取值 |
|---|---|
| type | `feat` / `fix` / `chore` / `refactor` / `docs` / `test` |
| name | 短横线分隔小写 kebab-case，描述简短（≤5 词） |
| date | 创建当日 YYYYMMDD（北京时间） |

### 范例（命名形式示意）
- `feat-<short-name>-YYYYMMDD`
- `fix-<short-name>-YYYYMMDD`
- `refactor-<short-name>-YYYYMMDD`

---

## 子目录模板

```
{type}-{name}-{YYYYMMDD}/
├── summary.md                          # 全流程追溯（Single Source of Truth）
├── request_analysis/                   # 阶段 1-2 产出
│   ├── spec.md                        # Planner 产出（最新版本）
│   ├── tasks.md                       # Planner 产出（最新版本）
│   └── review/
│       ├── spec_review_v1.md          # Evaluator 第 1 轮
│       ├── spec_review_v2.md          # （如有返工）
│       └── spec_review_v3.md          # （最多 3 轮）
├── coding/                             # 阶段 3-4 产出
│   ├── coding_report_v1.md            # Generator 第 1 轮
│   ├── coding_report_v2.md            # （如有返工）
│   └── review/
│       ├── code_review_v1.md          # Evaluator 第 1 轮
│       └── code_review_v2.md          # （最多 2 轮）
├── unit_test/                          # ⏭️ Phase 2 占位
└── deployment/                         # ⏭️ Phase 2 占位
```

---

## summary.md 模板

每个变更目录创建时由主会话写入空模板：

```markdown
# 变更摘要：{type}-{name}-{YYYYMMDD}

## 基本信息
- 创建时间：YYYY-MM-DD HH:MM
- 原始需求：<用户原话完整 quote>
- 当前状态：进行中

## 阶段执行记录

| 时间 | 阶段 | 执行者 | 结果 | 产出物 |
|---|---|---|---|---|
| HH:MM | 1.需求分析 | Planner | ✅ DONE | spec.md / tasks.md |
| ... | ... | ... | ... | ... |

## 最终 ACK
- 用户 ACK 时间：—
- 用户 ACK 备注：—
```

---

## 维护规范

- ✅ summary.md 每阶段完成后由**主会话**追加一行（不是 Agent 写）
- ❌ **绝不**重复写同一阶段记录（覆盖而非追加）
- ✅ 评审报告版本递增（v1 → v2 → v3），**旧版本永不删除**——构成完整 Audit Trail
- ❌ **绝不**修改已写入的 review 文件（评审记录是不可变的）
- ✅ spec.md / tasks.md / coding_report 在返工时**覆盖**最新版（不留 v2 后缀）

---

## 例外：升级人工后的处理

如某次评审进入 `ESCALATE_TO_HUMAN`：
- summary.md "当前状态" 改为 "已升级人工"
- 不要继续追加 Agent 调用记录（流程已暂停）
- 等用户决定后手动续接

---

## 已批准的变更目录范例（演示用）

实际目录将由主会话在收到新需求时创建。第一次 Dry Run 时建议用：

```
.harness/changes/feat-dry-run-demo-YYYYMMDD/
```
