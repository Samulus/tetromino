/*
	BoundingBox.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorChanger {

  public class BoundingBox : MonoBehaviour {
    private static readonly Vector3 LeftCenter = new Vector3(-0.9426f, -0.52057f, 1.0557f);
    private static readonly Vector3 LeftSize = new Vector3(0.0885f, 1.041144f, 2.1145f);
    private static readonly Vector3 RightCenter = new Vector3(-0.04695f, -0.520572f, 1.055725f);
    private static readonly Vector3 RightSize = new Vector3(0.093910f, 1.041144f, 2.1145f);
    private static readonly Vector3 BackCenter = new Vector3(-0.493852f, -0.9959415f, 1.0557525f);
    private static readonly Vector3 BackSize = new Vector3(0.987705f, 0.090405f, 2.11145f);
    
    private void Start() {
      var leftWall = gameObject.AddComponent<BoxCollider>();
      var rightWall = gameObject.AddComponent<BoxCollider>();
      var backWall = gameObject.AddComponent<BoxCollider>();
      leftWall.center = LeftCenter;
      leftWall.size = LeftSize;
      rightWall.center = RightCenter;
      rightWall.size = RightSize;
      backWall.center = BackCenter;
      backWall.size = BackSize;
    }
  }
}