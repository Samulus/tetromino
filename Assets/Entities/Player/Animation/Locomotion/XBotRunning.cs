using System.Collections;
using System.Collections.Generic;
using Entities.Player.Sensors;
using UnityEngine;

public class XBotRunning : StateMachineBehaviour {
  
  public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    
    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
    animator.transform.root.Rotate(0, x, 0);
    
    var cliffDetect = animator.transform.root.GetComponentInChildren<CliffDetect>();
    if (!cliffDetect.IsFacingCliff()) {
      var forward = animator.transform.root.TransformDirection(Vector3.forward);
      animator.transform.root.GetComponentInChildren<CharacterController>().Move(2f * forward * Time.deltaTime);
      animator.transform.root.GetComponentInChildren<CharacterController>().SimpleMove(Vector3.zero);
    }

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
