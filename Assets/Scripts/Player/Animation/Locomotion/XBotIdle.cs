/*
 * XBotIdle.cs
 * Author: Samuel Vargas
 *
 * Animation state for when the player is not pressing any keys
 * at all.
 */

using Devices.SokoBlock;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotIdle : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      PlayerActions.MaybeRotate(ref animator);
      
      if (PlayerInput.RequestToWalkForward(ref animator)) {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
        return;
      }
      
      GameObject sokoBlock;
      if (PlayerInput.RequestToPushSokoBlock(out sokoBlock)) {
        animator.SetBool("isPushing", true);
        animator.SetBool("isIdle", false);
        var inventory = GameObject.Find("Inventory").transform;
        sokoBlock.transform.SetParent(inventory.transform, true);
      }
      
      else if (PlayerInput.RequestToPullSokoBlock(out sokoBlock)) {
        animator.SetBool("isPulling", true);
        animator.SetBool("isIdle", false);
        var inventory = GameObject.Find("Inventory").transform;
        sokoBlock.transform.SetParent(inventory.transform, true);
      }

    }
  }

}