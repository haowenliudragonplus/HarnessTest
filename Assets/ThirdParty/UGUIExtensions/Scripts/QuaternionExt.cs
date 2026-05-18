using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class QuaternionExt : MonoBehaviour
{
    public Vector3 UIRotation;

    private void Update()
    {
        this.transform.localRotation = Quaternion.Euler(UIRotation);
    }
}
