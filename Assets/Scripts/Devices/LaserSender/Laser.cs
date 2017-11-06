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
      var distance = Math.Abs((transform.position.z - other.transform.position.z));
      Debug.Log(distance);
      var endPoint = transform.position;
      endPoint.z = other.transform.position.z;
      /*
       * Method:
       *
       *   Set the center 'Z' of the Box Component to somewhere BETWEEN the player and the origin
       *
       */
      
      /*
       * Patterns:
       *     Center: 1
       *     Size: 2
       *
       *     Center: 2
       *     Size: 4
       */

      var middle = (transform.position.z - other.transform.position.z) / 2.0f;
      Debug.Log(middle);
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