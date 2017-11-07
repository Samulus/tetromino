/*
 * RequiredColorZone.cs
 * Author: Samuel Vargas
 *
 * Provides an interface to see if the BoxCollider
 * that this modules creates is colliding with an
 * object that has a Tag with the requested
 * TetrominoColor.
 */

using UnityEngine;
using Util;

namespace MultiPurpose {

  public class RequiredColorZone : MonoBehaviour {
    public Vector3 Center;
    public Vector3 Size;
    public GameObjectColor.Colors RequiredColor;
    private const bool IsTrigger = true;
    private BoxCollider _boxCollider;
    private GameObject _gameObjectWithExpectedColor;

    private void Start() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
    }

    public GameObject GetGameObject() {
      return _gameObjectWithExpectedColor;
    }
    
    public bool GameObjectPresentWithExpectedColor() {
      if (!_gameObjectWithExpectedColor) {
        return false;
      }

      var color = _gameObjectWithExpectedColor.GetComponent<GameObjectColor>().Value;
      return color == RequiredColor;
    }
    
    private void OnTriggerEnter(Collider other) {
      var color = other.GetComponent<GameObjectColor>();
      if (_gameObjectWithExpectedColor || !color || color.Value != RequiredColor) return;
      _gameObjectWithExpectedColor = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {
      if (_gameObjectWithExpectedColor != other) return;
      _gameObjectWithExpectedColor = null;
    }
  }

}