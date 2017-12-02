/*
 * LaserReflector.cs
 * Author: Samuel Vargas
 */

using Devices.Lasers;
using Devices.LaserSender;
using Tags;
using UnityEngine;
using Util;

namespace Devices.RotateableMirror {

  public class LaserReflector : MonoBehaviour {
    private Laser _laser;
    private Vector3 _reflectionDirection;
    private Collider _incomingLaser;
    private ReverseRaycaster _reverseRaycaster;

    private void Start() {
      _laser = gameObject.GetComponentInChildren<Laser>();
      _laser.enabled = false;
    }

    private bool ColliderIsALaser(Collider other) {
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Device || maybeTag.DeviceId != DeviceId.Laser) return false;
      if (maybeTag.Type == TagType.Sensor) return false;
      if (other.name == "_origin") return false; // TODO: Ensure we aren't colliding with ourselves in a better way
      if (other.name == "_normal") return false; // TODO: Ensure we aren't colliding with ourselves in a better way
      return true;
    }

    private void OnTriggerEnter(Collider other) {
      if (ColliderIsALaser(other)) {
        Debug.Log(gameObject + " collided with laser", this);
      }
      // Avoid setting _incomingLaser if one already exists or if the object that triggered
      // the collision is not a laser.
      if (_incomingLaser != null || !ColliderIsALaser(other)) return;

      _incomingLaser = other;
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Device || maybeTag.DeviceId != DeviceId.Laser) return;
      if (maybeTag.Type == TagType.Sensor) return;
      if (other.name == "_origin") return; // TODO: Ensure we aren't colliding with ourselves in a better way
      if (other.name == "_normal") return; // TODO: Ensure we aren't colliding with ourselves in a better way

      var hit = other.GetComponent<LaserRaycaster>().ReverseHit(transform);
      if (hit.HasValue) {
        _laser.transform.position = hit.Value.point;
        _laser.enabled = true;
        _laser.GetComponent<GameObjectColor>().Value = other.GetComponent<GameObjectColor>().Value;
        _incomingLaser = other;
      }
    }

    private void OnTriggerStay(Collider other) {
      //if (ColliderIsALaser(other)) Debug.Log(gameObject.name + "has a collision " + gameObject.GetInstanceID());
      if (!ColliderIsALaser(other)) return;
      _incomingLaser = other;
      var hit = _incomingLaser.GetComponent<LaserRaycaster>().ReverseHit(transform);
      if (hit.HasValue) {
        if (!_laser.enabled) _laser.enabled = true;
        _laser.transform.position = hit.Value.point;
        _laser.GetComponent<GameObjectColor>().Value = _incomingLaser.GetComponent<GameObjectColor>().Value;
      }
      else {
        Debug.Log(gameObject.name + " failed to find reverse collision.");
      }
    }

    private void OnTriggerExit(Collider other) {
      //if (_incomingLaser != other) return;
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Device || maybeTag.DeviceId != DeviceId.Laser) return;
      _incomingLaser = null;
      _laser.enabled = false;
    }
  }

}