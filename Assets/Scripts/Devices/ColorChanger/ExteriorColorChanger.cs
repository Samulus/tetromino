/*
 * ExteriorColorChanger.cs
 * Author: Samuel Vargas
 *
 * Modifies the exterior color of the ColorChanger device
 * itself to match the corresponding TetrominoColor that
 * the device should appear as.
 */

using UnityEngine;
using Util;

namespace Devices.ColorChanger {

  public class ExteriorColorChanger : MonoBehaviour {
    private MeshRenderer _meshRenderer;
    private Material _defaultMaterial;
    private GameObjectColor _gameObjectColor;

    private void Start() {
      _meshRenderer = GetComponentInParent<MeshRenderer>();
      _gameObjectColor = GetComponentInParent<GameObjectColor>();
      _defaultMaterial = _meshRenderer.material;
    }

    public GameObjectColor GetColor() {
      return _gameObjectColor;
    }

    public void TriggerExteriorRepaint(GameObjectColor.Colors color) {
      _gameObjectColor.Value = color;
      if (color == GameObjectColor.Colors.NoColor) {
        _meshRenderer.material = _defaultMaterial;
      }
      else {
        // TODO: API so that the ExteriorColorChanger can switch to
        //       the appropriate texture given the active GameObjectColor
        //_meshRenderer.material = _colorsEnumerationMap.GetMaterialFromColor(color);
      }
    }
  }

}