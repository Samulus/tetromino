/*
 * ColorGateController.cs
 * Author: Samuel Vargas
 *
 * Uses the RequiredColorZone module to enable
 * or disable the attached BoxCollider iff the player enters
 * the DetectionZone and they're the correct color.
 *
 * TODO: The Update() method could be improved performance-wise using a delegate.
 */

using MultiPurpose;
using Tags;
using UnityEngine;

namespace Devices.ColorGate {

  public class ColorGateController : MonoBehaviour {
    private static readonly Vector3 Center = new Vector3(0, 1, 0);
    private static readonly Vector3 Size = new Vector3(1, 2, 0.1f);
    private RequiredColorZone _requiredColorZone;
    private BoxCollider _boxCollider;
    private bool _isOpen;

    private void Start() {
      _requiredColorZone = transform.parent.GetComponentInChildren<RequiredColorZone>();
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = false;
    }

    private void Update() {
      var obj = _requiredColorZone.GetGameObject();
      var objTag = (obj) ? obj.GetComponent<Tag>() : null;
      if (!objTag) return;
      _isOpen = objTag.Type == TagType.Agent && objTag.AgentId == AgentId.Player;
      _boxCollider.isTrigger = _isOpen;
    }
  }

}