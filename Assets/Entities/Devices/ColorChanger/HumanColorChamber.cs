/*
  HumanColorChamber.cs
  Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorChanger {
  public class HumanColorChamber : MonoBehaviour {
    private ColorChanger _colorChanger;
    private __HumanColorChamber _humanColorChamber;

    private void Start() {
      var empty = new GameObject {name = typeof(__HumanColorChamber).Name};
      empty.transform.SetParent(transform, false);
      _humanColorChamber = empty.AddComponent<__HumanColorChamber>();
    }
    
    private class __HumanColorChamber : MonoBehaviour {
      public void Start() {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(-0.5f, -0.5f, 1.0f);
        boxCollider.size = new Vector3(0.75f, 0.75f, 2.0f);
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
      }
    }
  }
}