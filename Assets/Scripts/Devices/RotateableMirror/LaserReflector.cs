/*
 * LaserReflector.cs
 * Author: Samuel Vargas
 */

using Devices.LaserSender;
using Tags;
using UnityEngine;
using Util;

namespace Devices.RotateableMirror {

  public class LaserReflector : MonoBehaviour {
    private Laser _laser;
    private Vector3 _reflectionDirection;
    private Collider _incomingLaser;

    private void Start() {
      _laser = gameObject.GetComponentInChildren<Laser>();
      _laser.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
      if (_incomingLaser != null) return;
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Device || maybeTag.DeviceId != DeviceId.Laser) return;
      _incomingLaser = other;
      _laser.enabled = true;
      _laser.GetComponent<GameObjectColor>().Value = other.GetComponent<GameObjectColor>().Value;
    }

    private void OnTriggerStay(Collider other) {
      if (_incomingLaser == other)
        _laser.GetComponent<GameObjectColor>().Value = other.GetComponent<GameObjectColor>().Value;
    }

    private void OnTriggerExit(Collider other) {
      if (_incomingLaser != other) return;
      var maybeTag = other.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Device || maybeTag.DeviceId != DeviceId.Laser) return;
      _incomingLaser = null;
      _laser.enabled = false;
    }
  }

}