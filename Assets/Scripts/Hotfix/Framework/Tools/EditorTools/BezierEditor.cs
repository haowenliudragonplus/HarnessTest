#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可视化贝塞尔曲线编辑器
/// </summary>
/// 挂载到场景中，添加控制点
public class BezierEditor : MonoBehaviour
{
    public int curveDensity = 100; //曲线密度

    public List<Transform> controlPointList = new List<Transform>(); //所有的控制点

    public void OnDrawGizmos()
    {
        if (controlPointList.Count <= 1)
            return;

        List<Vector3> pointList = new List<Vector3>(); //曲线上的所有点
        List<Vector3> controlPosList = new List<Vector3>();
        List<Transform> tempControlPointList = new List<Transform>(controlPointList);
        foreach (var controlPoint in tempControlPointList)
        {
            controlPosList.Add(controlPoint.position);
        }
        for (int i = 0; i <= curveDensity; i++)
        {
            float t = i * 1f / curveDensity;
            Vector3 point = BezierUtils.BezierCurve(controlPosList, t);
            pointList.Add(point);
        }

        //绘制所有点
        foreach (var point in pointList)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
        //绘制控制点连线
        Gizmos.color = Color.red;
        for (int i = 0; i < controlPointList.Count - 1; i++)
        {
            Gizmos.DrawLine(controlPointList[i].position, controlPointList[i + 1].position);
        }
    }
}

#endif