/*
 * SkinnedMeshRepainter.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Colors {

  public class SkinnedMeshRepainter : MonoBehaviour {
    private GameObjectColor _gameObjectColor;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private ColorTextureMapping _colorTextureMapping;

    private void Start() {
      _gameObjectColor = GetComponent<GameObjectColor>();
      _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
      _colorTextureMapping = GetComponent<ColorTextureMapping>();
      _skinnedMeshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
    }

    public void TriggerRepaint() {
      _skinnedMeshRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
    }
    
  }

}