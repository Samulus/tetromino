using System.Collections;
using System.Collections.Generic;
using Entities.Player.Inventory;
using Entities.Player.Sensors;
using UnityEngine;

public class XBotIdle : StateMachineBehaviour {
  
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.SetBool("isIdle", true);
  }
  
  public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
    animator.transform.root.Rotate(0, x, 0);

    //var itemPickupZone = animator.transform.root.GetComponentInChildren<ItemPickupZone>();
    //var obstructionPickupZone = animator.transform.root.GetComponentInChildren<ObstructionPickupZone>();
    var inventory = animator.transform.root.GetComponentInChildren<Inventory>();
    
    // Drop Items
    if (Input.GetKeyDown(KeyCode.Space) && inventory.HasItem()) {
      inventory.DropItem();
    }

    // Pick Up items
    /*
    else if (Input.GetKeyDown(KeyCode.Space) && !obstructionPickupZone.IsObstructionPresent() &&
        itemPickupZone.IsPickUpPresent()) {
      inventory.AddItem(itemPickupZone.GetPickUp());
    }
    */

    // Move forward
    if (Input.GetKeyDown("w")) {
      animator.SetBool("isIdle", false);
      animator.SetBool("isWalking", true);
    }
  }
}