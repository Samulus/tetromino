/*
 * LaserReceptor.cs
 * Author: Samuel Vargas
 *
 * LaserReceptor provides an interface for determining
 * if a Laser is actively colliding with the
 * given GameObject.
 */

using Tags;
using UnityEngine;
using Util;

namespace Devices.ColorChanger {

  public class LaserReceptor : MonoBehaviour {
    public Vector3 Center;
    public Vector3 Size;
    private const bool IsTrigger = true;
    private const bool IsKinematic = true;
    private const CollisionDetectionMode CollisionDetectionMode = UnityEngine.CollisionDetectionMode.Discrete;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private ExteriorColorChanger _exteriorColorChanger;

    private void Awake() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
      _rigidbody = gameObject.AddComponent<Rigidbody>();
      _rigidbody.isKinematic = IsKinematic;
      _rigidbody.collisionDetectionMode = CollisionDetectionMode;
      _exteriorColorChanger = transform.parent.GetComponentInChildren<ExteriorColorChanger>();
    }

    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (!objTag || objTag.GetTagType() != TagType.Device || objTag.GetDeviceId() != DeviceId.Laser) return;
      var color = other.GetComponent<GameObjectColor>();
      if (!color) return;
      _exteriorColorChanger.TriggerExteriorRepaint(color.Value);
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Device || objTag.DeviceId != DeviceId.Laser) return;
      _exteriorColorChanger.TriggerExteriorRepaint(GameObjectColor.Colors.NoColor);
    }
  }

}