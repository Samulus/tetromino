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
    private Vector3 _laserPseudoIndefiniteEnd;
    private LineRenderer _lineRenderer;
    private LaserRaycaster _laserRaycaster;
    private LaserBoxCollider _laserBoxCollider;
    private ColorTextureMapping _colorTextureMapping;
    private GameObjectColor _gameObjectColor;

    private void Start() {
      // Add sibling components
      _laserRaycaster = gameObject.AddComponent<LaserRaycaster>();
      _laserBoxCollider = gameObject.AddComponent<LaserBoxCollider>();

      // Get required components from parent
      _colorTextureMapping = transform.GetComponentInParent<ColorTextureMapping>();
      _gameObjectColor = transform.GetComponentInParent<GameObjectColor>();

      Debug.AssertFormat(_colorTextureMapping, "Lasers must have a ColorTextureMapping in their parent" +
                                               "so they can paint themselves the correct color.");

      Debug.AssertFormat(_gameObjectColor, "Lasers must have a GameObjectColor in their parent" +
                                           "so they can paint themselves the correct color.");

      // Setup LineRenderer
      _lineRenderer = gameObject.AddComponent<LineRenderer>();
      _lineRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
      _lineRenderer.startWidth = 0.1f;
      _lineRenderer.endWidth = 0.1f;

      // Setup Color Information
      _lineRenderer.shadowCastingMode = ShadowCastingMode.On;
      _laserPseudoIndefiniteEnd = transform.position + (transform.forward * MaxDistance);
      _lineRenderer.SetPosition(0, transform.position);
      _lineRenderer.SetPosition(1, _laserPseudoIndefiniteEnd);
    }

    private void OnEnable() {
      if (!_lineRenderer || !_laserRaycaster || !_laserBoxCollider) return;
      _lineRenderer.enabled = true;
      _laserRaycaster.enabled = true;
      _laserBoxCollider.enabled = true;
    }

    private void OnDisable() {
      if (!_lineRenderer || !_laserRaycaster || !_laserBoxCollider) return;
      _lineRenderer.enabled = false;
      _laserRaycaster.enabled = false;
      _laserBoxCollider.enabled = false;
    }

    private void Update() {
      _lineRenderer.SetPosition(0, transform.position);
      _laserPseudoIndefiniteEnd = transform.position + transform.forward * MaxDistance;
      _lineRenderer.material = _colorTextureMapping.GetTexture(_gameObjectColor.Value);
      

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