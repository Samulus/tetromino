/*
  OutputLaserReceptor.cs
  Author: Samuel Vargas
 */

using UnityEngine;
using Util;

namespace Entities.Devices.ColorChanger {
  public class OutputLaserReceptor : MonoBehaviour {
    private __OutputLaserReceptor _outputLaserReceptor;

    private void Start() {
      var empty = new GameObject {name = typeof(__OutputLaserReceptor).Name};
      empty.transform.SetParent(transform, false);
      _outputLaserReceptor = empty.AddComponent<__OutputLaserReceptor>();
    }

    public ColorsEnumerationMap.TetrominoColor GetColor() {
      return _outputLaserReceptor.GetColor();
    }
    
    private class __OutputLaserReceptor : MonoBehaviour {
      private ColorsEnumerationMap.TetrominoColor _color;
      private Material _laserMaterial;
      private BoxCollider _boxCollider;
      private Rigidbody _rigidbody;
      private HumanColorChamber _humanColorChamber;

      public ColorsEnumerationMap.TetrominoColor GetColor() {
        return _color;
      }

      private void Start() {
        _humanColorChamber = GetComponentInParent<HumanColorChamber>();
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rigidbody.isKinematic = true;
        _boxCollider = gameObject.AddComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _boxCollider.center = new Vector3(-0.5f, -1.0f, 0.5f);
        _boxCollider.size = new Vector3(1, 0.1f, 1);
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Laser")) return;
        _color = other.GetComponent<Laser>().GetColor();
        _humanColorChamber.TriggerColorChamberRepaint();
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Laser")) return;
        _color = ColorsEnumerationMap.TetrominoColor.NoColor;
        _humanColorChamber.TriggerColorChamberRepaint();
      }
    }
  }
}