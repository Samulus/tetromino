/*
  ColorGateOpener.cs
  Author: Samuel Vargas
  
  This module opens / closes the gate.
*/

using UnityEngine;

namespace Entities.Devices.ColorGate {

  public class ColorGateOpener : MonoBehaviour {
    private void Start() {
      var empty = new GameObject {name = typeof(__ColorGateOpener).Name};
      empty.transform.SetParent(transform, false);
      empty.AddComponent<__ColorGateOpener>();
    }

    private class __ColorGateOpener : MonoBehaviour {
      private static readonly Vector3 Center = new Vector3(0, 1, 0);
      private static readonly Vector3 Size = new Vector3(1, 2, 0.1f);
      public bool _isOpen;
      private ColorGateDetectionZone _colorGateDetectionZone;
      private BoxCollider _boxCollider;

      private void Start() {
        _boxCollider = gameObject.AddComponent<BoxCollider>();
        _boxCollider.center = Center;
        _boxCollider.size = Size;
        _boxCollider.isTrigger = false;
        _colorGateDetectionZone = GetComponentInParent<ColorGateDetectionZone>();
      }

      private void Update() {
        _isOpen = _colorGateDetectionZone.IsPlayerPresentWithCorrectColor();
        _boxCollider.isTrigger = _isOpen;
      }
    }
  }

}