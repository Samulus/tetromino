/*
	ColorManipulator.cs
	Author: Samuel Vargas
*/

using UnityEngine;
using Util;

namespace Entities.Player.Information {
  public class ColorManipulator : MonoBehaviour {
    public ColorsEnumerationMap.TetrominoColor CurrentColor;
    private Material _defaultMaterial;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private ColorsEnumerationMap _colorsEnumerationMap;

    private void Start() {
      _skinnedMeshRenderer = transform.root.GetComponentInChildren<SkinnedMeshRenderer>();
      _defaultMaterial = _skinnedMeshRenderer.material;
      _colorsEnumerationMap = GameObject.Find("Util").GetComponentInChildren<ColorsEnumerationMap>();
    }

    public ColorsEnumerationMap.TetrominoColor GetColor() {
      return CurrentColor;
    }
  
    public void SetColor(ColorsEnumerationMap.TetrominoColor color) {
      CurrentColor = color;
      _skinnedMeshRenderer.material = _colorsEnumerationMap.GetMaterialFromColor(color);
    }

    public void ClearColor() {
      CurrentColor = ColorsEnumerationMap.TetrominoColor.NoColor;
      _skinnedMeshRenderer.material = _defaultMaterial;
    }
  }
}