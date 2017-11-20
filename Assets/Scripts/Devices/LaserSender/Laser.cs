/*
 * Laser.cs
 * Author: Samuel Vargas
 */

using Colors;
using UnityEngine;
using UnityEngine.Rendering;
using Util;

namespace Devices.LaserSender {

  public class Laser : MonoBehaviour {
    private const float MaxDistance = 100.0f;
    private LineRenderer _lineRenderer;
    private Vector3 _laserPseudoIndefiniteEnd;
    private LaserRaycaster _laserRaycaster;

    private void Start() {
      _laserRaycaster = transform.parent.GetComponentInChildren<LaserRaycaster>();
      _lineRenderer = gameObject.AddComponent<LineRenderer>();
      _lineRenderer.startWidth = 0.25f;
      _lineRenderer.endWidth = 0.25f;
      _lineRenderer.material = transform.parent.GetComponent<ColorTextureMapping>()
                                        .GetTexture(transform.parent.GetComponent<GameObjectColor>().Value);
      _lineRenderer.shadowCastingMode = ShadowCastingMode.On;
      _laserPseudoIndefiniteEnd = transform.position + (transform.forward * MaxDistance);
      _lineRenderer.SetPosition(0, transform.position);
      _lineRenderer.SetPosition(1, _laserPseudoIndefiniteEnd);
    }
    
    private void Update() {
      _lineRenderer.SetPosition(0, transform.position);
      _laserPseudoIndefiniteEnd = transform.position + (transform.forward * MaxDistance);
      
      Vector3 point;
      if (_laserRaycaster.LaserHasCollision(out point)) {
        _lineRenderer.SetPosition(1, point);
      }
      else {
        _lineRenderer.SetPosition(1, _laserPseudoIndefiniteEnd);
      }
    }
  }

}