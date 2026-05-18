using UnityEngine;

/// <summary>
/// 2D相机适配
/// </summary>
public class CameraAdaption2D : MonoBehaviour
{
    [Header("Screen.width/Screen.height*2*orthographicSize")]
    [Header("参照宽度")]
    public float referWidth;

    private void Awake()
    {
        Adaptation();
    }

    /// <summary>
    /// 适配
    /// </summary>
    private void Adaptation()
    {
        if (GameUtils.IsPad())
            return;
        float resolutionRatio = Screen.width * 1f / Screen.height;
        float targetSize = referWidth / resolutionRatio / 2;
        GetComponent<Camera>().orthographicSize = targetSize;
    }
}