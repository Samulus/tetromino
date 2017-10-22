/*
	ColorManipulator.cs
	Author: Samuel Vargas
*/

using System;
using UnityEditor;
using UnityEngine;
using Util;

namespace Entities.Player.Information {
  public class ColorManipulator : MonoBehaviour {
    private ColorsEnumerationMap.TetrominoColor _currentColor;
    private Material _defaultMaterial;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private ColorsEnumerationMap _colorsEnumerationMap;

    private void Start() {
      _skinnedMeshRenderer = transform.root.GetComponentInChildren<SkinnedMeshRenderer>();
      _defaultMaterial = _skinnedMeshRenderer.material;
      _colorsEnumerationMap = GameObject.Find("Util").GetComponentInChildren<ColorsEnumerationMap>();
    }

    public ColorsEnumerationMap.TetrominoColor GetColor() {
      return _currentColor;
    }
  
    public void SetColor(ColorsEnumerationMap.TetrominoColor color) {
      _currentColor = color;
      _skinnedMeshRenderer.material = _colorsEnumerationMap.GetMaterialFromColor(color);
    }

    public void ClearColor() {
      _currentColor = ColorsEnumerationMap.TetrominoColor.NoColor;
      _skinnedMeshRenderer.material = _defaultMaterial;
    }

    private void OnDrawGizmos() {
      Handles.Label(transform.root.position, Enum.GetName(typeof(ColorsEnumerationMap.TetrominoColor), _currentColor));
    }
  }
}