using System;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public static Action OnFixedUpdate;
    public static Action OnUpdate;
    public static Action OnSecondsUpdate; //每秒执行一次update
    public static Action OnHalfSecondsUpdate; //半秒执行一次update
    public static Action OnLateUpdate;
    public static Action OnApplicateQuit;
    public static Action<bool> OnApplicatePause;
}