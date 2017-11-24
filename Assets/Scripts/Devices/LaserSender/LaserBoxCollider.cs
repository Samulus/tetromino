/*
 * LaserBoxCollider.cs
 * Author: Samuel Vargas
 * 
 * This module should be used in Conjunction with `Laser.cs`.
 * It should not be added to a component manually, Laser.cs
 * will do it for you.
 *
 * Unity doesn't fire OnTriggerExit events for disabled colldiers
 * so we resize it to a Size and Center of Vector3.zero so that
 * the chain OnTriggerExit events can fire when Lasers are used
 * with ReflectableMirrors.
 */

using UnityEngine;

namespace Devices.LaserSender {

  public class LaserBoxCollider : MonoBehaviour {
    private BoxCollider _boxCollider;
    private bool _isDisabled;

    private void Start() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.size = Vector3.zero;
      _boxCollider.center = Vector3.zero;
      _boxCollider.isTrigger = true;
    }

    public void ResizeBoxCollider(Vector3 center, Vector3 size) {
      if (!_isDisabled) {
        _boxCollider.center = center;
        _boxCollider.size = size;
      }
    }

    private void OnEnable() {
      if (!_boxCollider) return;
      _isDisabled = false;
    }

    private void OnDisable() {
      if (!_boxCollider) return;
      _boxCollider.center = Vector3.zero;
      _boxCollider.size = Vector3.zero;
      _isDisabled = true;
    }
  }

}