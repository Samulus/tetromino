using System.Collections;
using System.Collections.Generic;
using Entities.Player.Inventory;
using Entities.Player.Sensors;
using UnityEngine;

public class XBotIdle : StateMachineBehaviour {
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.SetBool("isIdle", true);
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  //
  //}

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  //
  //}

  // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here

  public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
    animator.transform.root.Rotate(0, x, 0);

    var itemPickupZone = animator.transform.root.GetComponentInChildren<ItemPickupZone>();
    var obstructionPickupZone = animator.transform.root.GetComponentInChildren<ObstructionPickupZone>();
    var inventory = animator.transform.root.GetComponentInChildren<Inventory>();
    
    // Drop Items
    if (Input.GetKeyDown(KeyCode.Space) && inventory.HasItem()) {
      inventory.DropItem();
    }

    // Pick Up items
    else if (Input.GetKeyDown(KeyCode.Space) && !obstructionPickupZone.IsObstructionPresent() &&
        itemPickupZone.IsPickUpPresent()) {
      inventory.AddItem(itemPickupZone.GetPickUp());
    }

    // Move forward
    if (Input.GetKeyDown("w")) {
      animator.SetBool("isIdle", false);
      animator.SetBool("isWalking", true);
    }
  }
}