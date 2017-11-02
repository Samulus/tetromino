/*
 * ItemPushZone.cs
 * Author: Samuel Vargas
 */

using Tags;
using UnityEngine;

namespace Player.Sensor {

  public class ItemPushZone : MonoBehaviour {
    private static readonly Vector3 Center = new Vector3(0f, 0.5f, 0.4f);
    private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f);
    private static readonly bool IsTrigger = true;

    private BoxCollider _boxCollider;
    private bool _isPushableItemPresent;
    private GameObject _pushable;

    private void Start() {
      _boxCollider = GetComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
    }

    public bool IsPushableItemPresent {
      get { return _isPushableItemPresent; }
      set { _isPushableItemPresent = value; }
    }

    public GameObject Pushable {
      get { return _pushable; }
    }

    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Pushable) return;
      _isPushableItemPresent = true;
      _pushable = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Pushable) return;
      _isPushableItemPresent = false;
      _pushable = null;
    }
  }

}