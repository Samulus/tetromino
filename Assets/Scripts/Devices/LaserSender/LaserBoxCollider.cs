/*
 *  LaserBoxCollider.cs
 *  Author: Samuel Vargas
 */

using UnityEngine;

namespace Devices.LaserSender {

  public class LaserBoxCollider : MonoBehaviour {
    private BoxCollider _boxCollider;

    private void Start() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.size = Vector3.zero;
      _boxCollider.center = Vector3.zero;
      _boxCollider.isTrigger = true;
    }

    public void ResizeBoxCollider(Vector3 center, Vector3 size) {
      _boxCollider.center = center;
      _boxCollider.size = size;
    }
  }

}