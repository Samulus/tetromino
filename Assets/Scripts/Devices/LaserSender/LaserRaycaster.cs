﻿/*
 * LaserRaycaster.cs
 * Author: Samuel Vargas
 *
 * This module provides an interface find the most immediate object
 * that the transform intersects with. It shoots a ray from the origin
 * of the object 100 meters out (more than enough for any situation in
 * this game) and reports the closest object on the 'Z' axis.
 *
 * Ensure that the object that this script is attached to is rotated
 * so that it's pointing in the positive Z direction relative to
 * the object. 
 */

using System;
using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Devices.LaserSender {

  internal class DistanceComparer : IComparer<RaycastHit> {
    public int Compare(RaycastHit a, RaycastHit b) {
      if (Math.Abs(a.distance - b.distance) < 0.02f) {
        return 0;
      }

      return a.distance < b.distance ? -1 : 1;
    }
  }

  public class LaserRaycaster : MonoBehaviour {
    private const float MaxDistance = 100.0f;
    private LaserBoxCollider _laserBoxCollider;

    private void Start() {
      _laserBoxCollider = transform.GetComponent<LaserBoxCollider>();
      Debug.AssertFormat(_laserBoxCollider != null,
                         "Caught attempt to use LaserRaycaster without sibling BoxCollider component");
    }

    public RaycastHit? ReverseHit(Transform theirTransform) {
      var results = Physics.RaycastAll(transform.position, transform.forward, Single.PositiveInfinity);
      Debug.DrawRay(transform.position, transform.forward, Color.green, Single.PositiveInfinity);

      foreach (var r in results) {
        if (r.transform == theirTransform) {
          return r;
        }
      }
      
      return null;
    }


    public bool LaserHasCollision(out Vector3 point) {
      point = Vector3.zero;
      var fwd = transform.TransformDirection(Vector3.forward);

      var hits = Physics.RaycastAll(transform.position, fwd, MaxDistance);
      if (hits.Length >= 1) {
        Array.Sort(hits, new DistanceComparer());
      }

      // If we find a collison resize the BoxCollider so that it 
      // stretches to fill the gap between the hit point and the
      // laser origin.

      foreach (var h in hits) {
        if (h.transform.GetComponent<LaserBoxCollider>() != null) continue;

        var maybeTag = h.transform.GetComponent<Tag>();
        if (maybeTag != null && maybeTag.Type == TagType.Sensor) continue;

        point = h.point;
        var center = new Vector3(0, 0, h.distance / 2.0f);
        var size = new Vector3(0.1f, 0.1f, h.distance);
        if (_laserBoxCollider)
          _laserBoxCollider.ResizeBoxCollider(center, size);
        return true;
      }

      // Otherwise if we didn't hit anything reset the BoxCollider
      // to be super long again.
      var longCenter = new Vector3(0, 0, MaxDistance);
      var longSize = new Vector3(0, 0, MaxDistance / 2.0f);
      if (_laserBoxCollider)
        _laserBoxCollider.ResizeBoxCollider(longCenter, longSize);
      return false;
    }
  }

}