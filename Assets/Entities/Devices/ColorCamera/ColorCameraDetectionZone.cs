/*
 * ColorCameraDetectionZone.cs
 * Author: Samuel Vargas
 *
 * Detects when the player enters the ColorCameraDetectionZone
 * without the correct color. The level will be reset if they're
 * the wrong color.
 */

using Entities.Player.Information;
using Tags;
using UnityEngine;
using Util;

namespace Entities.Devices.ColorCamera {

  public class ColorCameraDetectionZone : MonoBehaviour {
    public ColorsEnumerationMap.TetrominoColor RequiredColor;
    private BoxCollider _colorDetectionCollider;
    public bool _IsPlayerPresentWithCorrectColor;

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