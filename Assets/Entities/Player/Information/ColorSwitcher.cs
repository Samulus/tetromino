/*
	ColorSwitcher.cs
	Author: Samuel Vargas
	
	Exposes SetColor / ClearColor methods to modify the color
	of the player.
*/

using UnityEngine;
using Util;

public class ColorSwitcher : MonoBehaviour {
  private ColorsEnumerationMap.TetrominoColor _currentColor;
  private Material _defaultMaterial;
  private SkinnedMeshRenderer _skinnedMeshRenderer;
  private ColorsEnumerationMap _colorsEnumerationMap;

  private void Start() {
    _skinnedMeshRenderer = transform.root.GetComponentInChildren<SkinnedMeshRenderer>();
    _defaultMaterial = _skinnedMeshRenderer.material;
    _colorsEnumerationMap = GameObject.Find("Util").GetComponentInChildren<ColorsEnumerationMap>();
  }

  public void SetColor(ColorsEnumerationMap.TetrominoColor color) {
    _skinnedMeshRenderer.material = _colorsEnumerationMap.GetMaterialFromColor(color);
  }

  public void ClearColor() {
    _skinnedMeshRenderer.material = _defaultMaterial;
  }
}