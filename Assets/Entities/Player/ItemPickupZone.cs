/*
	ItemPickupZone.cs
	Author: Samuel Vargas
	
	This module creates two BoxColliders on top of each other. 
	The BoxColliders are used in conjunction with each other to
	determine if the player is allowed to pick up an object infront 
	of them.
	
	(Objects with overhead obstructions cannot be picked up by the player).
*/

using UnityEngine;

namespace Entities.Player {
  public class ItemPickupZone : MonoBehaviour {
    private ObstructionChecker _obstructionChecker;
    private PickupChecker _pickupChecker;
    private GameObject _inventory;

    private void Start() {
      var itemPickupZone = new GameObject {name = typeof(ItemPickupZone).Name};
      itemPickupZone.transform.SetParent(transform, false);
      _inventory = new GameObject {name = "Inventory"};
      _inventory.transform.SetParent(transform, false);
      _obstructionChecker = ObstructionChecker.CreateAndAddAsChild(ref itemPickupZone);
      _pickupChecker = PickupChecker.CreateAndAddAsChild(ref itemPickupZone);
    }
    
    public bool CanPickupItem() {
      return !_obstructionChecker.IsObstructionPresent() &&
             _pickupChecker.IsPickUpPresent();
    }

    public void PickUpItem() {
      Debug.Assert(CanPickupItem(), "No PickUp-Able item found, call CanPickUpItem first.");
      var item = _pickupChecker.GetPickUp().transform;
      item.transform.SetParent(_inventory.transform, true);
      Debug.Log("Added Item " + item.name + " to inventory.");
    }


    private class PickupChecker : MonoBehaviour {
      private static readonly Vector3 Center = new Vector3(0f, 0.5f, 0.4f); // TODO: Magic Constants
      private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.55f); // TODO: Magic Constants 
      private GameObject _potentialPickUp;
      private bool _isPickUpPresent;

      public static PickupChecker CreateAndAddAsChild(ref GameObject g) {
        var gameObject = new GameObject();
        gameObject.transform.SetParent(g.transform, false);
        gameObject.name = typeof(PickupChecker).Name;
        var collider = gameObject.AddComponent<BoxCollider>();
        collider.center = Center;
        collider.size = Size;
        collider.isTrigger = true;
        return gameObject.AddComponent<PickupChecker>();
      }

      private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<CharacterController>() != null) return;
        if (!other.CompareTag("PickUp")) return;
        _isPickUpPresent = true;
        _potentialPickUp = other.gameObject;
      }

      private void OnTriggerExit(Collider other) {
        if (other.GetComponent<CharacterController>() != null) return;
        if (!other.CompareTag("PickUp")) return;
        _isPickUpPresent = false;
        _potentialPickUp = null;
      }

      public GameObject GetPickUp() {
        Debug.Assert(_isPickUpPresent && _potentialPickUp != null);
        return _potentialPickUp;
      }

      public bool IsPickUpPresent() {
        return _isPickUpPresent;
      }
    }
  }
}