/*
	CliffEdgeDetection.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Player {
  public class CliffDetect : MonoBehaviour {
    public bool IsCliffInfront() {
      var start = transform.position + (transform.TransformDirection(Vector3.forward) / 4.0f);
      var isCliff = !Physics.Raycast(start, Vector3.down, 1.0f);
      Debug.DrawRay(start, Vector3.down, isCliff ? Color.red : Color.blue);
      return isCliff;
    }

    public bool IsCliffBehind() {
      var start = transform.position + (transform.TransformDirection(Vector3.back) / 4.0f);
      var isCliff = !Physics.Raycast(start, Vector3.down, 1.0f);
      Debug.DrawRay(start, Vector3.down, isCliff ? Color.red : Color.blue);
      return isCliff;
    }
  }
}