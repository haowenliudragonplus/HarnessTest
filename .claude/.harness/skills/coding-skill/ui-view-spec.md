# UI 层编码规范

> 用于新增或改动 UI 视图（`UIView_*`）相关代码。

---

## 命名

| 项 | 规则 |
|---|---|
| 类名 | `UIView_` + 大驼峰（`UIView_Notice`、`UIView_Boot`） |
| 文件名 | 与类名一致 |
| 在 `UIViewName` 常量中 | `UIViewName.UIView_Notice`（必须显式注册） |
| OpenData 类 | 内嵌在 UIView 类内：`UIView_Notice.OpenData` |

---

## 打开 / 关闭 UI 唯一方式

参考 [ModAccount.OnBindFbResult](../../../../Assets/Scripts/Hotfix/Common/Module/Account/ModAccount.cs#L84):

```csharp
UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
{
    content = CoreUtils.GetLocalization("UI_bind_fb_fail_authen_error_text"),
    showCloseBtn = false,
    showMidBtn = true,
};
Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
```

异步加载：
```csharp
await Game.GetMod<ModUI>().OpenAsync(UIViewName.UIView_XXX, openData);
```

关闭：
```csharp
Game.GetMod<ModUI>().Close(UIViewName.UIView_XXX);
```

---

## OpenData 结构

每个 UIView 类内必须定义一个 `OpenData` 嵌套类：

```csharp
public class UIView_Notice : UIViewBase
{
    public class OpenData
    {
        public string content;
        public bool showCloseBtn;
        public bool showMidBtn;
        // 仅放数据，无方法
    }

    protected override void OnOpen(object data)
    {
        var openData = data as OpenData;
        // 用 openData 初始化 UI
    }
}
```

---

## 必须遵循

### 1. 多语言用 `CoreUtils.GetLocalization(key)`
```csharp
content = CoreUtils.GetLocalization("UI_xxx_text");  // ✅
content = "登录失败";                                 // ❌ 硬编码中文
```

### 2. 用 ModUI 而非 Instantiate
```csharp
Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Tip, data);  // ✅
Instantiate(tipPrefab, parent);                              // ❌ 绕过框架
```

### 3. 关闭等待框（防卡死）
任何弹出 Loading 类 UI 的地方，回调路径必须有 `Game.GetMod<ModUI>().CloseWaiting()`（参考 ModAccount.OnBindFbResult 第一行）。

### 4. 事件订阅与 UI 生命周期同步
- 在 `OnOpen` 中订阅 → 在 `OnClose` 中反订阅
- 不要让关闭后的 UIView 还接收事件（可能 NRE）

---

## 禁止

- ❌ `GameObject.Find` 查找 UI（性能 + 脆弱）
- ❌ `Instantiate(uiPrefab)` 手动创建（绕过 ModUI）
- ❌ UI 类持有 Module 的长期引用（Module Dispose 后会失效）
- ❌ 直接在 UIView 内做业务逻辑（应通过事件或方法调用 Module）

---

## 范例：新增一个 UIView

需求：新增"内购提示弹窗" UIView_PurchasePrompt

文件：`Assets/Scripts/Hotfix/Common/UI/Purchase/UIView_PurchasePrompt.cs`

```csharp
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIView_PurchasePrompt : UIViewBase
{
    public class OpenData
    {
        public string productId;
        public long priceCent;       // 价格用 long
        public System.Action onConfirm;
        public System.Action onCancel;
    }

    [SerializeField] private Text contentText;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button cancelBtn;

    private OpenData openData;

    protected override void OnOpen(object data)
    {
        openData = data as OpenData;
        contentText.text = string.Format(
            CoreUtils.GetLocalization("UI_purchase_prompt_content"),
            openData.priceCent / 100f);  // 显示时转 float 仅用于格式化

        confirmBtn.onClick.AddListener(OnConfirm);
        cancelBtn.onClick.AddListener(OnCancel);
    }

    protected override void OnClose()
    {
        confirmBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        openData = null;
    }

    private void OnConfirm()
    {
        openData.onConfirm?.Invoke();
        Game.GetMod<ModUI>().Close(UIViewName.UIView_PurchasePrompt);
    }

    private void OnCancel()
    {
        openData.onCancel?.Invoke();
        Game.GetMod<ModUI>().Close(UIViewName.UIView_PurchasePrompt);
    }
}
```

调用方：
```csharp
Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_PurchasePrompt, new UIView_PurchasePrompt.OpenData
{
    productId = "remove_ad_pro",
    priceCent = 199,  // 1.99 元
    onConfirm = () => Game.GetMod<ModIAP>().Buy("remove_ad_pro"),
    onCancel = () => CLog.Info("user cancelled purchase"),
});
```

要点：
- 价格字段 `long priceCent`（最小分单位）
- 多语言走 `CoreUtils.GetLocalization`
- 按钮在 `OnOpen` 绑定，`OnClose` 解绑
- 用 `Game.GetMod<ModUI>().Close` 关闭，不要 `Destroy(gameObject)`
