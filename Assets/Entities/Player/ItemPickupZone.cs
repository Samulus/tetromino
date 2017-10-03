/*
	ItemPickupZone.cs
	Author: Samuel Vargas
	
	The ItemPickupZone generates a BoxCollider infront 
	of the player at their waist height. Two BoxColliders
	are placed directly on top of each other to determine
	if the player is allowed to pick up an object.
	
*/

using UnityEngine;

public class ItemPickupZone : MonoBehaviour {
  private BoxCollider _topCollider, _bottomCollider;
  private GameObject _top, _bottom;
  private bool _canPickUpItem;

  private void Start() {
    _top = new GameObject();
    _top.transform.SetParent(transform, false);
    _top.name = "TopObjectZone";
    _bottom = new GameObject();
    _bottom.transform.SetParent(transform, false);
    _bottom.name = "BottomObjectZone";

    var g = gameObject;
    _top = Zone.CreateAndAddAsChild("TopObjectZone", new Vector3(0f, 0.5f, 0.4f), new Vector3(0.5f, 1.5f, 0.25f), g);
    _bottom = Zone.CreateAndAddAsChild("BottomObjectZone", new Vector3(0f, 0.5f, 0.4f), new Vector3(0.5f, 1f, 0.25f), g);

    _topCollider = _top.AddComponent<BoxCollider>();
    _topCollider.center = new Vector3(0f, 0.5f, 0.4f); // TODO: Magic Constants
    _topCollider.size = new Vector3(0.5f, 1.5f, 0.25f); // TODO: Magic Constants
    _topCollider.isTrigger = true;

    _bottomCollider = _bottom.AddComponent<BoxCollider>();
    _bottomCollider.center = new Vector3(0f, 0.5f, 0.4f); // TODO: Magic Constants
    _bottomCollider.size = new Vector3(0.5f, 1f, 0.25f); // TODO: Magic Constants
    _canPickUpItem = false;
  }

  private class Zone : MonoBehaviour {
    private GameObject _gameObject;

    public static GameObject CreateAndAddAsChild(string name, Vector3 size, Vector3 center, ref GameObject g) {
    _gameObject = new GameObject();
    _gameObject.transform.SetParent(transform, false);
    }
  }
}