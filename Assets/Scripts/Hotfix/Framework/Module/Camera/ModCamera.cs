using System.Collections.Generic;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ModCamera : ModuleBase
{
    private const string URPBaseCameraName = "URPBaseCamera";

    private UniversalAdditionalCameraData baseCameraData;

    public override void OnInit()
    {
        base.OnInit();
        InitURPBaseCamera();
    }

    public void AddOverlayCamera(Camera camera, int index = -1)
    {
        var data = camera.GetUniversalAdditionalCameraData().renderType;
        if (data != CameraRenderType.Overlay)
        {
            CLog.Error($"只能添加Overlay的相机，{camera.name}不是");
            return;
        }
        if (baseCameraData.cameraStack.Contains(camera))
            return;
        if (index == -1)
        {
            baseCameraData.cameraStack.Add(camera);
        }
        else
        {
            if (baseCameraData.cameraStack.Count < index)
            {
                CLog.Error($"{index}超出相机列表数量，无法添加{camera.name}");
                return;
            }
            baseCameraData.cameraStack.Insert(index, camera);
        }
    }

    public bool RemoveOverlayCamera(Camera camera)
    {
        var ret = baseCameraData.cameraStack.Remove(camera);
        return ret;
    }

    private void InitURPBaseCamera()
    {
        GameObject urpBaseCamera = GameObject.Find(URPBaseCameraName);
        if (urpBaseCamera == null)
        {
            CLog.Error("没有URPBase相机");
            return;
        }
        Camera com_Camera = urpBaseCamera.GetComponent<Camera>();
        Object.DontDestroyOnLoad(urpBaseCamera);
        com_Camera.cullingMask = 0;
        baseCameraData = com_Camera.GetUniversalAdditionalCameraData();
    }
}