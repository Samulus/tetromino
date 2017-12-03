using Tags;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotPulling : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      PlayerActions.MaybeRotate(ref animator);

      if (!PlayerInput.PullingButtonHeld()) {
        animator.SetBool("isPulling", false);
        animator.SetBool("isIdle", true);

        // Find Player Inventory
        var inventory = GameObject.Find("Inventory").transform;
        Debug.AssertFormat(inventory.childCount <= 1, "We only support 1 item in the inventory right now");

        if (inventory.childCount >= 1) {
          // Get the Childer Pusher and put it 
          var pusher = GameObject.Find("Inventory").transform.GetChild(0);
          var devices = GameObject.Find("Devices").transform;
          pusher.SetParent(devices, true);
        }
        return;
      }

      PlayerActions.WalkBackward(ref animator);
    }
  }

}