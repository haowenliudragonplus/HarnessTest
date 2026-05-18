# Wiki 知识库（Phase 2 占位）

> 本目录用于沉淀业务领域知识——账号 / 背包 / IAP / 广告 / 网络协议等链路梳理。Agent 在需要业务背景时按需 Read。

---

## MVP 阶段：暂未填充

当前为 Phase 1 MVP 阶段，本目录仅作占位。**Agent 不需要读取这里的文件**——所有 MVP 必需的信息已在 `.harness/rules/` 和 `.harness/skills/` 中。

---

## Phase 2 待补内容

| 文件 | 内容 |
|---|---|
| `account.md` | ModAccount 登录链路、Facebook/Apple/Google 绑定流程、token 管理 |
| `bag.md` | ModBag 道具系统、ItemPackage、奖励发放链路（DispenseReward） |
| `iap.md` | ModIAP 内购流程、订单校验、退款 / 翻包处理 |
| `ad.md` | AppLovin MAX 广告链路、激励视频回调、广告 A/B 实验 |
| `storage.md` | StorageClientCommon 字段说明、跨设备同步、版本迁移 |
| `network.md` | RPC 协议、超时重试、错误码处理 |
| `fsm.md` | 主流程状态图（Login → Home → InGame → ...）|
| `hybridclr.md` | HybridCLR 热更流程、AOT / Interpreter 边界、热更不能做的事 |
| `yooasset.md` | YooAsset 资源分包策略、版本 / 清单更新 |

---

## 命名规范

- 文件名：小写 kebab-case，业务领域为单位（如 `account.md`、`iap.md`）
- 内容结构：
  1. 链路总览（带 ASCII 图或 mermaid）
  2. 关键 Module / 类清单
  3. 常见坑 / 历史事故
  4. 相关测试入口（如 GM 命令）

---

## 何时补充

每次实战中 Planner / Generator 因业务知识不足而出错时，把该问题对应的知识沉淀到这里——遵循文章 5.5 节"规范的每一行都对应一个历史失败案例"。
