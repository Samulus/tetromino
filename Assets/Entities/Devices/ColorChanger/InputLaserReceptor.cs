/*
  InputLaserReceptor.cs
  Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Entities.Devices.ColorChanger {
  public class InputLaserReceptor : MonoBehaviour {
    private __InputLaserReceptor _inputLaserReceptor;

    private void Start() {
      var empty = new GameObject {name = typeof(__InputLaserReceptor).Name};
      empty.transform.SetParent(transform, false);
      _inputLaserReceptor = empty.AddComponent<__InputLaserReceptor>();
    }

    public ColorsEnumerationMap.TetrominoColor GetColor() {
      return _inputLaserReceptor.GetColor();
    }

    private class __InputLaserReceptor : MonoBehaviour {
      private ColorsEnumerationMap.TetrominoColor _color;
      private Material _laserMaterial;
      private BoxCollider _boxCollider;
      private Rigidbody _rigidbody;
      private ExteriorColorChanger _exteriorColorChanger;

      public ColorsEnumerationMap.TetrominoColor GetColor() {
        return _color;
      }
      
      private void Start() {
        _exteriorColorChanger = GetComponentInParent<ExteriorColorChanger>();
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rigidbody.isKinematic = true;
        _boxCollider = gameObject.AddComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _boxCollider.center = new Vector3(-0.5f, -1.0f, 1.5f);
        _boxCollider.size = new Vector3(1, 0.1f, 1);
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Laser")) return;
        _color = other.GetComponent<Laser>().GetColor();
        _exteriorColorChanger.TriggerExteriorRepaint();
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Laser")) return;
        _color = ColorsEnumerationMap.TetrominoColor.NoColor;
        _exteriorColorChanger.TriggerExteriorRepaint();
      }
    }
  }
}