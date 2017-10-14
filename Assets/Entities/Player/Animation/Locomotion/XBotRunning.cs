using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBotRunning : StateMachineBehaviour {
  
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  //
  //}

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  }

  // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
  override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    
    // Go Back to Walking
    if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w")) {
      animator.SetBool("isRunning", false);
      animator.SetBool("isWalking", true);
    }
    
    else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey("w")) {
      animator.SetBool("isIdle", true);
      animator.SetBool("isWalking", false);
      animator.SetBool("isRunning", false);
    }
  }
}
