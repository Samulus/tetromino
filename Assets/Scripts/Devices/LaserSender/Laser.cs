/*
 * Laser.cs
 * Author: Samuel Vargas
 *
 * The laser works by stopping after 
 */

using System;
using Priority_Queue;
using UnityEngine;

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

    private SimplePriorityQueue<GameObject> _entries;


    private void Start() {
      _entries = new SimplePriorityQueue<GameObject>();
      _laserCollider = gameObject.AddComponent<BoxCollider>();
      _laserCollider.isTrigger = true;
      _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
                                        transform.localScale.z * MaxDistance);
      SetDirection();
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
                     string.Format("Parent LaserSender: '{0}' was not placed at a 90 degree angle.",
                                   transform.parent.name));
      }
    }

    private void OnTriggerEnter(Collider other) {
      var dt = Math.Abs(transform.position.z - other.transform.position.z);
      _entries.Enqueue(other.gameObject, dt);
    }

    private void OnTriggerExit(Collider other) {
      if (_entries.Contains(other.gameObject)) {
        _entries.Remove(other.gameObject);
      }
    }

    private void LateUpdate() {
      GameObject target;
      if (_entries.TryFirst(out target)) {
        var middle = 0.0f;
        if (_facing == Direction.PosZ || _facing == Direction.NegZ) {
          middle = Math.Abs(transform.position.z - target.transform.position.z) / 2.0f;
        }
        else if (_facing == Direction.PosX || _facing == Direction.NegX) {
          middle = Math.Abs(transform.position.x - target.transform.position.x) / 2.0f;
        }
        else {
          Debug.AssertFormat(false, "Parent LaserSender: '{0}' was not placed at a 90 degree angle.",
                             transform.parent.name);
        }
        _laserCollider.center = new Vector3(0, 0, middle);
        _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f, middle * 2.0f - 0.25f);
      }
      else {
        _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
        _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
                                          transform.localScale.z * MaxDistance);
      }
    }
  }

}