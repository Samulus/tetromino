/*
 * XBotWalking.cs
 * Author: Samuel Vargas
 */

using Devices.SokoBlock;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotWalking : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      PlayerActions.MaybeRotate(ref animator);
      
      SokoBlockPusher sokoBlockPusher;
      if (PlayerInput.RequestToPushSokoBlock(out sokoBlockPusher)) {
        animator.SetBool("isWalking", false);
        animator.SetBool("isPushing", true);
        sokoBlockPusher.StartPushing();
        return;
      }
      
      if (PlayerInput.RequestToRunForward(ref animator)) {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        return;
      }

      if (!PlayerInput.RequestToWalkForward(ref animator)) {
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
        return;
      }
      
      PlayerActions.WalkForward(ref animator);

    }
  }

}