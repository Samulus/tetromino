/*
 * XBotRunning.cs
 * Author: Samuel Vargas
 */

using Devices.SokoBlock;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotRunning : StateMachineBehaviour {
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      
      PlayerActions.MaybeRotate(ref animator);

      /*
      SokoBlockPusher sokoBlockPusher;
      if (PlayerInput.RequestToPushSokoBlock()) {
        animator.SetBool("isRunning", false);
        animator.SetBool("isPushing", true);
        return;
      }
      */

      if (PlayerInput.RequestToWalkForward(ref animator)) {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        return;
      }
      
      if (!PlayerInput.RequestToRunForward(ref animator)) {
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
      }
      
      else {
        PlayerActions.RunForward(ref animator);
      }
      
    }
  }

}