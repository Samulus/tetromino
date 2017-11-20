/*
 * AutoSkinnedMeshRepainterFromParentColor.cs
 * Author: Samuel Vargas
 *
 * Retrieves the `GameObjecColor` from the first parent it
 * encounters and repaints the GameObject it's attached to by
 * using the `ColorTextureMapping` component that should be
 * attached to this object.
 */

using UnityEngine;
using Util;

namespace Colors {

  public class AutoSkinnedMeshRepainterFromParentColor : MonoBehaviour {
    private GameObjectColor _gameObjectColor;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private ColorTextureMapping _colorTextureMapping;
    private GameObjectColor.Colors _lastColor = GameObjectColor.Colors.NoColor;

    private void Start() {
      _gameObjectColor = GetComponentInParent<GameObjectColor>();
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