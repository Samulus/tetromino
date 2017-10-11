/*
	ColorChamberBoundingBox.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorChanger {
  public class ColorChamberBoundingBox : MonoBehaviour {
    
    // Use this for initialization
    private void Start() {
      var empty = new GameObject {name = typeof(__ColorChamberBoundingBox).Name};
      empty.transform.SetParent(transform, false);
      empty.AddComponent<__ColorChamberBoundingBox>();
    }
    
    private class __ColorChamberBoundingBox : MonoBehaviour {
      private void Start() {
        var leftWall = gameObject.AddComponent<BoxCollider>();
        leftWall.center = new Vector3(-0.9426f, -0.52057f, 1.0557f);
        leftWall.size = new Vector3(0.0885f, 1.041144f, 2.1145f);

        var rightWall = gameObject.AddComponent<BoxCollider>();
        rightWall.center = new Vector3(-0.04695f, -0.520572f, 1.055725f);
        rightWall.size = new Vector3(0.093910f, 1.041144f, 2.1145f);

        var backWall = gameObject.AddComponent<BoxCollider>();
        backWall.center = new Vector3(-0.493852f, -0.9959415f, 1.0557525f);
        backWall.size = new Vector3(0.987705f, 0.090405f, 2.11145f);
      }
    }
  }
}