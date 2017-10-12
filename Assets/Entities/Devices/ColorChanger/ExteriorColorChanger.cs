/*
  ExteriorColorChanger.cs
  Author: Samuel Vargas
  
  Modifies the exterior color of the ColorChanger to the specified 
  TetrominoColor.
*/

using UnityEngine;
using Util;

namespace Entities.Devices.ColorChanger {
  public class ExteriorColorChanger : MonoBehaviour {
    private InputLaserReceptor _inputLaserReceptor;
    private OutputLaserReceptor _outputLaserReceptor;
    private MeshRenderer _meshRenderer;
    private Material _defaultMaterial;
    private ColorsEnumerationMap _colorsEnumerationMap;

    private void Start() {
      _meshRenderer = GetComponentInParent<MeshRenderer>();
      _defaultMaterial = _meshRenderer.material;
      _inputLaserReceptor = GetComponentInParent<InputLaserReceptor>();
      _outputLaserReceptor = GetComponentInParent<OutputLaserReceptor>();
      _colorsEnumerationMap = GameObject.Find("Util").GetComponentInChildren<ColorsEnumerationMap>();
    }

    public void TriggerExteriorRepaint() {
      var inputLaserColor = _inputLaserReceptor.GetColor();
      var outputLaserColor = _outputLaserReceptor.GetColor();


      if (inputLaserColor == ColorsEnumerationMap.TetrominoColor.NoColor ||
          outputLaserColor == ColorsEnumerationMap.TetrominoColor.NoColor) {
        _meshRenderer.material = _defaultMaterial;
      }
      else {
        _meshRenderer.material = _colorsEnumerationMap.GetMaterialFromColor(inputLaserColor);
      }
    }
  }
}