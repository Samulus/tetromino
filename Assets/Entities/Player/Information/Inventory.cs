/*
	Inventory.cs
	Author: Samuel Vargas
*/

using JetBrains.Annotations;
using UnityEngine;

namespace Entities.Player.Inventory {
  public class Inventory : MonoBehaviour {
    public GameObject Devices;
    
    [CanBeNull] private GameObject _heldItem;
    private GameObject _inventory;

    private void Start() {
      _heldItem = null;
      _inventory = new GameObject {name = typeof(Inventory).Name};
      _inventory.transform.SetParent(transform, true);
    }

    public void AddItem(GameObject item) {
      _heldItem = item;
      _heldItem.transform.SetParent(_inventory.transform, true);
    }

    public bool HasItem() {
      return _heldItem != null;
    }
    
    public void DropItem() {
      _heldItem.transform.SetParent(Devices.transform, true);
      _heldItem = null;
    }
  }
}