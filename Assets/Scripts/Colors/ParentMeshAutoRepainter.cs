/*
 * ParentMeshAutoRepainter.cs
 * Author: Samuel Vargas
 *
 * Identical to MeshAutoRepainter but obtains the MeshRenderer
 * component
 */

using UnityEngine;
using Util;

namespace Colors {

  public class ParentMeshAutoRepainter : MonoBehaviour {
    
    private GameObjectColor _gameObjectColor;
    private MeshRenderer _meshRenderer;
    private ColorTextureMapping _colorTextureMapping;
    private GameObjectColor.Colors _lastColor = GameObjectColor.Colors.NoColor;

    private void Start() {
      _gameObjectColor = GetComponentInParent<GameObjectColor>();
      _meshRenderer = GetComponentInParent<MeshRenderer>();
      _colorTextureMapping = GetComponent<ColorTextureMapping>();
      _meshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
    }

    private void Update() {
      if (_lastColor != _gameObjectColor.Value) {
        _meshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
      }
      _lastColor = _gameObjectColor.Value;
    }
  }

}