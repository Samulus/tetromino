/*
 * ItemPrescenceZone.cs
 * Author: Samuel Vargas
 *
 * Provides an interface to check to see if there
 * is an object with the expected Tag(s) in the specificed
 * location using a BoxCollider
 *
 * The first item that collides is returned.
 * A second item cannot replace the first item until OnTriggerExit
 * is invoked for the first item.
 */

using Tags;
using UnityEngine;

namespace MultiPurpose {

  public class ItemPrescenceZone : MonoBehaviour {
    public Vector3 Center;
    public Vector3 Size;

    private const bool IsTrigger = false;
    private BoxCollider _boxCollider;
    private bool _isItemPresent;
    private GameObject _item;

    private void Start() {
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
    }

    public GameObject GetItem() {
      return _item;
    }

    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (_isItemPresent || !objTag) return;
      _isItemPresent = true;
      _item = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (!objTag) return;
      _isItemPresent = false;
      _item = null;
    }
  }

}