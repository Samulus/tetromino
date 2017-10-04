/*
  Idling.cs
  Author: Samuel Vargas
  
  This state represents Idling, the player may only leave this state by:
  
  * Walking
  * Sprinting
  * Picking an Item Up
*/

using UnityEngine;

namespace Entities.Player.States {
  public class Idling : MonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;
    private CharacterController _characterController;
    private Animation _animation;
    private bool rotateRight;

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
      _animation = GetComponentInParent<Animation>();
      _characterController = GetComponentInParent<CharacterController>();
    }

    private void Update() {
      if (!_finiteStateMachine.IsActive(this)) return;

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
      var z = Input.GetAxis("Vertical") * Time.deltaTime;
      if (x > 0.0f && z == 0.0f) {
        // TODO: Potential Floating Point Error _animation
        if (!_animation.isPlaying) {
          rotateRight = true;
          _animation.Play("RotateRight", PlayMode.StopAll);
        }
      }

      if (rotateRight && !_animation.isPlaying) {
        transform.parent.parent.Rotate(0, 90f, 0);
        rotateRight = false;
      }
    }
  }
  
}