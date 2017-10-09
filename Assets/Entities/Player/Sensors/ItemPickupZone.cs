/*
	ItemPickupZone.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Player.Sensors {
  public class ItemPickupZone : MonoBehaviour {
    private __ItemPickUpZone _itemPickupZone;

    private void Start() {
      var empty = new GameObject {name = typeof(__ItemPickUpZone).Name};
      empty.transform.SetParent(transform, false);
      _itemPickupZone = empty.AddComponent<__ItemPickUpZone>();
    }

    public GameObject GetPickUp() {
      return _itemPickupZone.GetPickUp();
    }

    public bool IsPickUpPresent() {
      return _itemPickupZone.IsPickUpPresent();
    }

    private class __ItemPickUpZone : MonoBehaviour {
      private static readonly Vector3 Center = new Vector3(0f, 0.5f, 0.4f);
      private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f);
      private bool _isPickUpPresent = true;
      private GameObject _potentialPickUp;

      private void Start() {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = Center;
        boxCollider.size = Size;
        boxCollider.isTrigger = true;
      }

      internal GameObject GetPickUp() {
        Debug.Assert(_isPickUpPresent && _potentialPickUp != null);
        return _potentialPickUp;
      }

      internal bool IsPickUpPresent() {
        return _isPickUpPresent;
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("PickUp")) return;
        _isPickUpPresent = true;
        _potentialPickUp = other.gameObject;
      }

      private void OnTriggerExit(Collider other) {
        if (_potentialPickUp == null) return;
        _isPickUpPresent = false;
        _potentialPickUp = null;
      }
    }
  }
}