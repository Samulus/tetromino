/*
	CliffDetect.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Player.Sensors {

  public class CliffDetect : MonoBehaviour {
    private Transform player;
    
    public bool IsFacingCliff() {
      var above = transform.root.position;
      above.y += 0.1f;
      var start = above + (transform.TransformDirection(Vector3.forward) / 4.0f);
      var isCliff = !Physics.Raycast(start, Vector3.down, 2.0f);
      Debug.DrawRay(start, Vector3.down, isCliff ? Color.red : Color.blue);
      return isCliff;
    }
  }
}