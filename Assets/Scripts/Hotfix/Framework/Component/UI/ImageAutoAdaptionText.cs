using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 图片自动适配文本组件
/// </summary>
[ExecuteAlways]
public class ImageAutoAdaptionText : MonoBehaviour
{
    public float paddingX;
    public float paddingY;
    public bool updateRefresh = true;

    private Image img;
    private TextMeshProUGUI txt;

    private void Awake()
    {
        img = GetComponent<Image>();
        txt = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Main.GameUtils.IsInApplicationPlaying() && !updateRefresh)
            return;
        if (img == null || txt == null)
            return;

        Refresh();
    }

    public void Refresh()
    {
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, txt.preferredWidth + paddingX);
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, txt.preferredHeight + paddingY);
        LayoutRebuilder.ForceRebuildLayoutImmediate(img.rectTransform);
    }
}