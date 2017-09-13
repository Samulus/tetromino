/*
	CliffEdgeDetection.cs
	Author: Samuel Vargas
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class CliffEdgeDetection : MonoBehaviour {
	
	public bool IsFacingCliff() {
		Vector3 start = transform.position + (transform.TransformDirection(Vector3.forward) / 4.0f);
		bool isCliff = !Physics.Raycast(start, Vector3.down, 1.0f);
    Debug.DrawRay(start, Vector3.down, isCliff ? Color.red : Color.blue);
		return isCliff;
	}
	
}
