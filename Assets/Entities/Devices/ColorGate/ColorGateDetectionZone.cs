/*
  ColorGateDetectionZone.cs
  Author: Samuel Vargas
  
  Detects when the player has entered the
  ColorDetectionZone with the correct color.
*/

using Entities.Player.Information;
using Tags;
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

    public bool IsPlayerPresentWithCorrectColor() {
      return _colorGateDetectionZone._IsPlayerPresentWithCorrectColor;
    }

    private class __ColorGateDetectionZone : MonoBehaviour {
      public ColorsEnumerationMap.TetrominoColor RequiredColor;
      private BoxCollider _colorDetectionCollider;

      private static readonly Vector3 Center = new Vector3(0, 1, 0);
      private static readonly Vector3 Size = new Vector3(1, 1.8986f, 2.0277f);

      internal bool _IsPlayerPresentWithCorrectColor;

      private void Start() {
        _colorDetectionCollider = gameObject.AddComponent<BoxCollider>();
        _colorDetectionCollider.center = Center;
        _colorDetectionCollider.size = Size;
        _colorDetectionCollider.isTrigger = true;
      }

      private void OnTriggerEnter(Collider other) {
        var objTag = other.GetComponent<Tag>();
        if (objTag == null || objTag.Type != TagType.Agent || objTag.AgentId != AgentId.Player) return;
        var colorManipulator = other.GetComponentInChildren<ColorManipulator>();
        _IsPlayerPresentWithCorrectColor = colorManipulator.GetColor() == RequiredColor;
      }

      private void OnTriggerExit(Collider other) {
        var objTag = other.GetComponent<Tag>();
        if (objTag == null || objTag.Type != TagType.Agent || objTag.AgentId != AgentId.Player) return;
        _IsPlayerPresentWithCorrectColor = false;
      }
    }
  }

}