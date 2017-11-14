/*
 * AutoMeshRepainter.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Colors {

  public class AutoMeshRepainter : MonoBehaviour {
    private GameObjectColor _gameObjectColor;
    private MeshRenderer _meshRenderer;
    private ColorTextureMapping _colorTextureMapping;
    private GameObjectColor.Colors _lastColor = GameObjectColor.Colors.NoColor;

    private void Start() {
      _gameObjectColor = GetComponent<GameObjectColor>();
      Debug.AssertFormat(_gameObjectColor != null, "AutoMeshRepainter could not find a GameObjectColor attached" +
                                                   " to this object");
      _meshRenderer = GetComponent<MeshRenderer>();
      Debug.AssertFormat(_meshRenderer != null,
                         "AutoMeshRepainter could not find a MeshRenderer attached to this object");

      _colorTextureMapping = GetComponent<ColorTextureMapping>();
      Debug.AssertFormat(_colorTextureMapping != null,
                         "AutoMeshRepainter could not find a MeshRenderer attached to this object");

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