/*
 * XbotPushing.cs
 * Author: Samuel Vargas
 *
 * This module allows the player to push SokoBlocks around.
 */

using Devices.SokoBlock;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotPushing : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      SokoBlockPusher sokoBlockPusher;
      if (!PlayerInput.RequestToPushSokoBlock(out sokoBlockPusher)) {
        animator.SetBool("isWalking", false);
        animator.SetBool("isPushing", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", true);
      }
      else {
        PlayerActions.WalkForward(ref animator);
      }
    }
  }

}