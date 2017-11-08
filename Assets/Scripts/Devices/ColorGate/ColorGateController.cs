/*
 * ColorGateController.cs
 * Author: Samuel Vargas
 *
 * Uses the TagPrescenceZone module to enable or disable
 * the attached BoxCollider iff the player enters
 * the DetectionZone and they're the correct color.
 */

using Tags;
using UnityEngine;
using Util;

namespace Devices.ColorGate {

  public class ColorGateController : MonoBehaviour {
    public GameObjectColor.Colors RequiredColor;
    private static readonly Vector3 Center = new Vector3(0, 1, 0);
    private static readonly Vector3 Size = new Vector3(1, 2, 0.1f);
    private TagPrescenceZone _tagPrescenceZone;
    private BoxCollider _boxCollider;
    private bool _isOpen;

    private void Start() {
      _tagPrescenceZone = transform.parent.GetComponentInChildren<TagPrescenceZone>();
      _tagPrescenceZone.SetOnEntry(OnItemEntry);
      _tagPrescenceZone.SetOnExit(OnItemExit);

      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = false;
    }

    private void OnItemEntry(GameObject item) {
      var ttag = item.GetComponent<Tag>();
      if (ttag.Type != TagType.Agent || ttag.AgentId != AgentId.Player) return;
      var color = item.GetComponentInChildren<GameObjectColor>();
      _boxCollider.isTrigger = color.Value == RequiredColor;
    }

    private void OnItemExit(GameObject item) {
      var ttag = item.GetComponent<Tag>();
      if (ttag.Type != TagType.Agent || ttag.AgentId != AgentId.Player) return;
      _boxCollider.isTrigger = false;
    }
    
  }

}