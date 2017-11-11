/*
 * PlayerActions.cs
 * Author: Samuel Vargas
 *
 * The `PlayerActions.cs` module contains misc methods that the Animator states
 * can invoke. Specifically this module helps prevent duplication of code.
 */

using UnityEngine;

namespace Player.Animation {

  public static class PlayerActions {
    public static void MaybeRotate(ref Animator animator) {
      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
      animator.transform.root.Rotate(0, x, 0);
    }

    public static void WalkForward(ref Animator animator) {
      var forward = animator.transform.root.TransformDirection(Vector3.forward);
      animator.transform.root.GetComponentInChildren<CharacterController>().Move(forward * Time.deltaTime);
      animator.transform.root.GetComponentInChildren<CharacterController>().SimpleMove(Vector3.zero);
    }
    
    public static void WalkBackward(ref Animator animator) {
      var back = animator.transform.root.TransformDirection(Vector3.back);
      animator.transform.root.GetComponentInChildren<CharacterController>().Move(back * Time.deltaTime);
      animator.transform.root.GetComponentInChildren<CharacterController>().SimpleMove(Vector3.zero);
    }

    public static void RunForward(ref Animator animator) {
      var forward = animator.transform.root.TransformDirection(Vector3.forward);
      animator.transform.root.GetComponentInChildren<CharacterController>().Move(2f * forward * Time.deltaTime);
      animator.transform.root.GetComponentInChildren<CharacterController>().SimpleMove(Vector3.zero);
    }
  }

}