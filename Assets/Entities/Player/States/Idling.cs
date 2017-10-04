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
    private Animation _animation;

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
      _animation = GetComponentInParent<Animation>();
      _animation.Play("Walk", PlayMode.StopAll);

      Animation anim = _animation;
      foreach(AnimationState state in anim)
      {
        Debug.Log(state.name);
      }
    }

    private void Update() {
      if (!_finiteStateMachine.IsActive(this)) return;
    }
  }
}