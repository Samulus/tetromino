/*
  ColorGateDetectionZone.cs
  Author: Samuel Vargas
  
  Detects when the player has entered the
  ColorDetectionZone with the correct color.
*/

using Entities.Player.Information;
using UnityEngine;
using Util;

namespace Entities.Devices.ColorGate {
  public class ColorGateDetectionZone : MonoBehaviour {
    public ColorsEnumerationMap.TetrominoColor RequiredColor;
    private __ColorGateDetectionZone _colorGateDetectionZone;

    private void Start() {
      var empty = new GameObject {name = typeof(__ColorGateDetectionZone).Name};
      empty.transform.SetParent(transform, false);
      _colorGateDetectionZone = empty.AddComponent<__ColorGateDetectionZone>();
      _colorGateDetectionZone.RequiredColor = RequiredColor;
    }
    
    private class __ColorGateDetectionZone : MonoBehaviour {
      public ColorsEnumerationMap.TetrominoColor RequiredColor;
      private BoxCollider _colorDetectionCollider;
      private bool _isPlayerPresentWithCorrectColor;

      private void Start() {
        _colorDetectionCollider = gameObject.AddComponent<BoxCollider>();
        _colorDetectionCollider.size = new Vector3(1, 1.8986f, 1.0277f);
        _colorDetectionCollider.center = new Vector3(0, 0.9501185f, -0.48481f);
        _colorDetectionCollider.isTrigger = true;
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) ;
        var colorManipulator = other.GetComponentInChildren<ColorManipulator>();
        _isPlayerPresentWithCorrectColor = colorManipulator.GetColor() == RequiredColor;
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) ;
        _isPlayerPresentWithCorrectColor = false;
      }
    }
  }
}