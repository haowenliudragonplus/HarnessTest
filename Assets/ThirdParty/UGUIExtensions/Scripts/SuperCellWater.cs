using System;
using System.Collections.Generic;
using UnityEngine;

public class SuperCellWater : MonoBehaviour
{
	private Material mMateiral;

	void Start()
	{
		MeshRenderer meshrenderer = GetComponent<MeshRenderer>();
		mMateiral = meshrenderer.material;
	}

	void FixedUpdate()
	{
		mMateiral.SetFloat("u_time", Time.time*1.5f);
	}
}
