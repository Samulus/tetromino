﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBotWalking : StateMachineBehaviour {
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //Debug.Log("Entered XBotWalking");
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  //
  //}

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  }

  // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
  public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
    animator.transform.root.Rotate(0, x, 0);
    
    var forward = animator.transform.root.TransformDirection(Vector3.forward);
    animator.transform.root.GetComponentInChildren<CharacterController>().Move(forward * Time.deltaTime);
    animator.transform.root.GetComponentInChildren<CharacterController>().SimpleMove(Vector3.zero);

    if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift)) {
      animator.SetBool("isIdle", false);
      animator.SetBool("isWalking", false);
      animator.SetBool("isRunning", true);
    }

    if (!Input.GetKey("w")) {
      animator.SetBool("isIdle", true);
      animator.SetBool("isWalking", false);
    }
  }
}