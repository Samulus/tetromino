/*
 * HumanColorChamber.cs
 * Author: Samuel Vargas
 * 
 * Detects if the player has walked into the HumanColorChamber
 * and repaints them accordingly depending on the value of
 * the GameObjectColor of the device.
 */

using Tags;
using UnityEngine;
using Util;

namespace Devices.ColorChanger {

  public class HumanColorChamber : MonoBehaviour {
    private ExteriorColorChanger _exteriorColorChanger;

    public void Start() {
      var boxCollider = gameObject.AddComponent<BoxCollider>();
      boxCollider.isTrigger = true;
      boxCollider.center = new Vector3(-0.5f, -0.5f, 1.0f);
      boxCollider.size = new Vector3(0.75f, 0.75f, 2.0f);
      _exteriorColorChanger = transform.root.GetComponentInChildren<ExteriorColorChanger>();
    }
    
    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (!objTag || objTag.Type != TagType.Agent || objTag.AgentId != AgentId.Player) return;
      var color = other.GetComponentInChildren<GameObjectColor>();
      color.Value = _exteriorColorChanger.GetColor().Value;
      // TODO: Actually repaint the player once they walk into this box.
    }
  }
  
}