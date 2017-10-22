/*
  ObstructionPickupZone.cs
  Author: Samuel Vargas
*/

using Tags;
using UnityEngine;

namespace Entities.Player.Sensors {

  public class ObstructionPickupZone : MonoBehaviour {
    private __ObstructionPickupZone _obstructionPickupZone;

    private void Start() {
      var empty = new GameObject();
      empty.transform.SetParent(transform, false);
      empty.name = typeof(__ObstructionPickupZone).Name;
      _obstructionPickupZone = empty.AddComponent<__ObstructionPickupZone>();
    }

    public bool IsObstructionPresent() {
      return _obstructionPickupZone.IsObstructionPresent();
    }

    private class __ObstructionPickupZone : MonoBehaviour {
      private static readonly Vector3 Center = new Vector3(0f, 1.5f, 0.4f);
      private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f);
      private bool _isObstructionPresent;

      private void Start() {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = Center;
        boxCollider.size = Size;
        boxCollider.isTrigger = true;
      }

      internal bool IsObstructionPresent() {
        return _isObstructionPresent;
      }

      private void OnTriggerEnter(Collider other) {
        var objTag = other.GetComponent<Tag>();
        if (objTag == null || objTag.Type != TagType.PickUp) return;
        _isObstructionPresent = true;
      }

      private void OnTriggerExit(Collider other) {
        var objTag = other.GetComponent<Tag>();
        if (objTag == null || objTag.Type != TagType.PickUp) return;
        _isObstructionPresent = false;
      }
    }
  }

}