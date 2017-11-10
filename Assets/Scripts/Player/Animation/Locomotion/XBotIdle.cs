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
      SokoBlockPusher sokoBlockPusher;
      if (PlayerInput.RequestToPushSokoBlock(out sokoBlockPusher)) {
        sokoBlockPusher.StartPushing();
        animator.SetBool("isPushing", true);
        animator.SetBool("isIdle", false);
        return;
      }

      PlayerActions.MaybeRotate(ref animator);

      if (PlayerInput.RequestToWalkForward(ref animator)) {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
      }
    }
  }

}