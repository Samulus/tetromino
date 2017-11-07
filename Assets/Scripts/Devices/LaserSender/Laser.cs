/*
 * Laser.cs
 * Author: Samuel Vargas
 *
 * Lasers should be children of LaserSenders, they inherit their
 * color from their parents.
 */

using System;
using UnityEngine;
using Tags;

namespace Devices.LaserSender {

  internal enum Direction {
    PosZ,
    NegZ,
    PosX,
    NegX
  }

  public class Laser : MonoBehaviour {
    private const float MaxDistance = 100.0f;
    private BoxCollider _laserCollider;
    private Direction _facing;

    private void Start() {
      _laserCollider = gameObject.AddComponent<BoxCollider>();
      _laserCollider.isTrigger = true;
      _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
        transform.localScale.z * MaxDistance + 0.5f);
      SetupRigidbody();
      SetDirection();
    }

    private void SetupRigidbody() {
      var rigidBody = gameObject.AddComponent<Rigidbody>();
      rigidBody.isKinematic = true;
      rigidBody.useGravity = false;
    }

    private void SetDirection() {
      if (Math.Abs(transform.forward.z - -1.0) < 0.02) {
        _facing = Direction.NegZ;
      }
      else if (Math.Abs(transform.forward.z - 1.0) < 0.02) {
        _facing = Direction.PosZ;
      }
      else if (Math.Abs(transform.forward.x - -1.0) < 0.02) {
        _facing = Direction.NegX;
      }
      else if (Math.Abs(transform.forward.x - 1.0) < 0.02) {
        _facing = Direction.PosX;
      }
      else {
        Debug.Assert(false,
          string.Format("Parent LaserSender: '{0}' was not placed at a 90 degree angle.", transform.parent.name));
      }
    }

    private void OnTriggerEnter(Collider other) {
      var maybeLaser = other.transform.GetComponent<Tag>();
      if (maybeLaser && maybeLaser.Type == TagType.Device && maybeLaser.DeviceId == DeviceId.Laser) {
        Physics.IgnoreCollision(_laserCollider, other);
      }
    }

    private void OnTriggerStay(Collider other) {
      var maybeLaser = other.transform.GetComponent<Tag>();
      if (maybeLaser && maybeLaser.Type == TagType.Device && maybeLaser.DeviceId == DeviceId.Laser) return;

      var middle = 0.0f;
      if (_facing == Direction.PosZ || _facing == Direction.NegZ) {
        middle = Math.Abs(transform.position.z - other.transform.position.z) / 2.0f;
      }
      else if (_facing == Direction.PosX || _facing == Direction.NegX) {
        middle = Math.Abs(transform.position.x - other.transform.position.x) / 2.0f;
      }
      else {
        Debug.Assert(false,
          string.Format("Parent LaserSender: '{0}' was not placed at a 90 degree angle.", transform.parent.name));
      }

      _laserCollider.center = new Vector3(0, 0, middle);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f, middle * 2.0f);
    }

    private void OnTriggerExit(Collider other) {
      var maybeLaser = other.transform.GetComponent<Tag>();
      if (maybeLaser && maybeLaser.Type == TagType.Device && maybeLaser.DeviceId == DeviceId.Laser) return;
      _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
        transform.localScale.z * MaxDistance + 0.5f);
    }
  }

}