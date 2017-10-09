/*
  OutputLaserReceptor.cs
  Author: Samuel Vargas
 */

using UnityEngine;

namespace Entities.Devices.ColorChanger {
  public class OutputLaserReceptor : MonoBehaviour {
    private __OutputLaserReceptor _outputLaserReceptor;

    private void Start() {
      var empty = new GameObject {name = typeof(__OutputLaserReceptor).Name};
      empty.transform.SetParent(transform, false);
      _outputLaserReceptor = empty.AddComponent<__OutputLaserReceptor>();
    }

    private class __OutputLaserReceptor : MonoBehaviour {
      private Material _laserMaterial;
      private BoxCollider _boxCollider;
      private Rigidbody _rigidbody;
      private ColorChanger _colorChanger;

      private void Start() {
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
        //_colorChanger.OnLaserReceptorEnter(other);
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Laser")) return;
        //_colorChanger.OnLaserReceptorExit(other);
      }
    }
  }
}
