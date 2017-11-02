/*
 * ObstructionPushZone.cs
 * Author: Samuel Vargas
 */

using Tags;
using UnityEngine;

namespace Player.Sensor {

  public class ObstructionPushZone : MonoBehaviour {
    private static readonly Vector3 Center = new Vector3(0f, 1.5f, 0.4f);
    private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f);
    private static readonly bool IsTrigger = false;
    private bool _isObstructionPresent;
    private BoxCollider _boxCollider;

    private void Start() {
      _boxCollider = GetComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
    }

    internal bool IsObstructionPresent() {
      return _isObstructionPresent;
    }

    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Pushable) return;
      _isObstructionPresent = true;
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (objTag == null || objTag.Type != TagType.Pushable) return;
      _isObstructionPresent = false;
    }
  }

}