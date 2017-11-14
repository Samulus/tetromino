/*
 * LaserReceptor.cs
 * Author: Samuel Vargas
 *
 * TODO:
 *   Right now we only support 1 laser shining on LaserReceptor.
 */

using Tags;
using UnityEngine;
using Util;

namespace Devices.ColorChanger {

  public class LaserReceptor : MonoBehaviour {
    private Rigidbody _rigidbody;
    private GameObjectColor _gameObjectColor;

    private void Start() {
      _gameObjectColor = gameObject.GetComponentInParent<GameObjectColor>();
      var meshCollider = gameObject.AddComponent<MeshCollider>();
      meshCollider.convex = true;
      meshCollider.inflateMesh = true;
      meshCollider.skinWidth = float.Epsilon;
      var rbody = gameObject.AddComponent<Rigidbody>();
      rbody.isKinematic = true;
      rbody.useGravity = false;
    }

    private void OnTriggerStay(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (!objTag || objTag.GetTagType() != TagType.Device || objTag.GetDeviceId() != DeviceId.Laser) return;
      var color = other.GetComponent<GameObjectColor>();
      Debug.AssertFormat(color != null, "Laser '{0}' is missing GameObjectColor component", other.name);
      _gameObjectColor.Value = color.Value;
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Device || objTag.DeviceId != DeviceId.Laser) return;
      _gameObjectColor.Value = GameObjectColor.Colors.NoColor;
    }
  }

}