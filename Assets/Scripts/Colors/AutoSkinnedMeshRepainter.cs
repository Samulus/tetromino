/*
 * AutoSkinnedMeshRepainter.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Colors {

  public class AutoSkinnedMeshRepainter : MonoBehaviour {
    private GameObjectColor _gameObjectColor;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private ColorTextureMapping _colorTextureMapping;
    private GameObjectColor.Colors _lastColor = GameObjectColor.Colors.NoColor;

    private void Start() {
      _gameObjectColor = GetComponent<GameObjectColor>();
      _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
      _colorTextureMapping = GetComponent<ColorTextureMapping>();
      _skinnedMeshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
    }

    private void Update() {
      if (_lastColor != _gameObjectColor.Value) {
        _skinnedMeshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
      }
      _lastColor = _gameObjectColor.Value;
    }
  }

}