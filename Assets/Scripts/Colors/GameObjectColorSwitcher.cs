/*
 * GameObjectColorSwitcher.cs
 * Author: Samuel Vargas
 *
 * This module constructs a BoxCollider at the provided Center
 * and Size. Whenever a GameObject with a GameObjectColor component
 * attached to it enters the collision zone the object is repainted
 * to the value of the GameObjectColor located in the parent.
 *
 * 0 -0.1 0
 * 0.9 2 0.75
 */

using Tags;
using UnityEngine;
using Util;

namespace Colors {

  public class GameObjectColorSwitcher : MonoBehaviour {
    private BoxCollider _boxCollider;
    private GameObjectColor _gameObjectColor;
    public Vector3 Center;
    public Vector3 Size;
    private const bool IsTrigger = true;

    private void Start() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
      _gameObjectColor = GetComponentInParent<GameObjectColor>();
    }

    private void OnTriggerStay(Collider other) {
      var maybeGameObjectColor = other.gameObject.GetComponentInChildren<GameObjectColor>();
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag != null && maybeTag.Type == TagType.Device) return; // avoid recoloring devices
      if (maybeGameObjectColor == null) return;
      maybeGameObjectColor.Value = _gameObjectColor.Value;
    }
  }

}