/*
  Idling.cs
  Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Player.States {
  public class Idling : FiniteStateMonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;
    private Animator _animator;

    public override void Enter() {
      _animator.SetTrigger("Idle");
    }

    public override void Exit() {
    }

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
      _animator = transform.root.GetComponentInChildren<Animator>();
    }

    private void Update() {
      MaybeTransitionToWalking();
      MaybeTransitionToPickUp();
    }

    private void MaybeTransitionToWalking() {
      if (Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon) {
        _finiteStateMachine.ChangeState(typeof(Walking).Name);
      }

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
      transform.root.Rotate(0, x, 0);
    }

    private void MaybeTransitionToPickUp() {
      var pickUp = transform.root.GetComponentInChildren<ItemPickupZone>();
      if (Input.GetKeyDown("space")) {
        Debug.Log(pickUp.CanPickupItem());
      }
      if (Input.GetKeyDown("space") && pickUp.CanPickupItem()) {
        pickUp.PickUpItem();
      }
    }
  }
}