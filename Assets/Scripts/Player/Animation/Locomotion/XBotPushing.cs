/*
 * XbotPushing.cs
 * Author: Samuel Vargas
 *
 * This module allows the player to push SokoBlocks around.
 */

using Tags;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotPushing : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      
      PlayerActions.MaybeRotate(ref animator);
      
      if (!PlayerInput.PushingButtonHeld()) {
        animator.SetBool("isPushing", false);
        animator.SetBool("isIdle", true);

        // Find Player Inventory
        var inventory = GameObject.Find("Inventory").transform;
        Debug.AssertFormat(inventory.childCount <= 1, "We only support 1 item in the inventory right now");

        if (inventory.childCount >= 1) {
          // Get the Childer Pusher and put it 
          var pusher = GameObject.Find("Inventory").transform.GetChild(0);
          var tag = pusher.GetComponent<Tag>();

          if (tag.Type != TagType.Device || tag.DeviceId != DeviceId.SokoBlock) return;
          var devices = GameObject.Find("Devices").transform;

          pusher.SetParent(devices, true);
        }
        return;
      }

      PlayerActions.WalkForward(ref animator);
    }
  }

}