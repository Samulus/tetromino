/*
 * Laser.cs
 * Author: Samuel Vargas
 *
 * Lasers should be children of LaserSenders, they inherit their
 * color from their parents.
 */

using System;
using Colors;
using UnityEngine;
using UnityEngine.Rendering;
using Util;
using Tags;

namespace Devices.LaserSender {

  public class Laser : MonoBehaviour {
    private const float Width = 0.25f;
    private const float MaxDistance = 100.0f;
    private Vector3 _laserPseudoIndefiniteEnd;
    private BoxCollider _laserCollider;

    private void Start() {
      // Setup start and end of laser
      _laserPseudoIndefiniteEnd =
        new Vector3(transform.position.x, transform.position.y, transform.position.z - MaxDistance);
      
      // Laser Collisions
      _laserCollider = gameObject.AddComponent<BoxCollider>();
      _laserCollider.isTrigger = true;
      _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
        transform.localScale.z * MaxDistance + 0.5f);
    }

    private void OnTriggerStay(Collider other) {
      var maybeLaser = other.transform.GetComponent<Tag>();
      if (maybeLaser && maybeLaser.Type == TagType.Device && maybeLaser.DeviceId == DeviceId.Laser) return;
      var distance = Math.Abs((transform.position - other.transform.position).z);
      var endPoint = transform.position;
      endPoint.z = other.transform.position.z;
      _laserCollider.center = new Vector3(0, 0, distance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
        transform.localScale.z * distance);
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