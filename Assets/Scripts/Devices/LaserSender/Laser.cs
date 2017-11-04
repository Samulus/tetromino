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

namespace Devices.LaserSender {

  public class Laser : MonoBehaviour {
    private LineRenderer _lineRenderer;
    private const float Width = 0.25f;
    private const float MaxDistance = 100.0f;
    private Vector3 _laserPseudoIndefiniteEnd;
    private BoxCollider _laserCollider;
    
    private void Start() {
      // Setup laser properties
      _lineRenderer = gameObject.AddComponent<LineRenderer>();
      _lineRenderer.startWidth = Width;
      _lineRenderer.endWidth = Width;
      _lineRenderer.material = transform.parent.GetComponent<ColorTextureMapping>().GetTexture(transform.parent.GetComponent<GameObjectColor>().Value);
      _lineRenderer.shadowCastingMode = ShadowCastingMode.On;

      // Setup start and end of laser
      _laserPseudoIndefiniteEnd =
        new Vector3(transform.position.x, transform.position.y, transform.position.z - MaxDistance);
      _lineRenderer.SetPosition(0, transform.position);
      _lineRenderer.SetPosition(1, _laserPseudoIndefiniteEnd);

      // Setup Capsule Collider for laser instance.
      _laserCollider = gameObject.AddComponent<BoxCollider>();
      _laserCollider.isTrigger = true;
      _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
      _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
        transform.localScale.z * MaxDistance + 0.5f);
    }

    private void Update() {
      Vector3 point;
      // Update the laser's apperance and it's collider length.
      if (LaserHasCollision(out point)) {
        var distance = Math.Abs((transform.position - point).z);
        _lineRenderer.SetPosition(1, point);

        _laserCollider.center = new Vector3(0, 0, distance / 2.0f);
        _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
          transform.localScale.z * distance + 0.5f);
      }
      else {
        _laserCollider.center = new Vector3(0, 0, MaxDistance / 2.0f);
        _laserCollider.size = new Vector3(transform.localScale.x / 2.0f, transform.localScale.y / 2.0f,
          transform.localScale.z * MaxDistance + 0.5f);
      }
    }

    private bool LaserHasCollision(out Vector3 point) {
      var fwd = transform.TransformDirection(Vector3.forward);
      RaycastHit hit;
      point = Vector3.negativeInfinity;
      Debug.DrawRay(transform.position, fwd, Color.cyan, -1 * MaxDistance);
      var collides = Physics.Raycast(transform.position, fwd, out hit, MaxDistance);
      if (!collides) {
        return false;
      }
      point = hit.point;
      return true;
    }

  }

}