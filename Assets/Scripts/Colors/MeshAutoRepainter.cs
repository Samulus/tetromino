/*
 * MeshAutoRepainter.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Colors {

  public class MeshAutoRepainter : MonoBehaviour {
    private GameObjectColor _gameObjectColor;
    private MeshRenderer _meshRenderer;
    private ColorTextureMapping _colorTextureMapping;

    private void Start() {
      _gameObjectColor = GetComponent<GameObjectColor>();
      _meshRenderer = GetComponent<MeshRenderer>();
      _colorTextureMapping = GetComponent<ColorTextureMapping>();
      _meshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
    }
  }

}