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
    }

    public override void Exit() {
    }

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
      _animator = GetComponentInParent<Animator>();
    }

    private void Update() {
      //_animator.Play("Walk");
      if (!_finiteStateMachine.IsActive(this)) return;

      if (Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon) {
        _finiteStateMachine.ChangeState(typeof(Walking).Name);
      }

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;

      //transform.root.Rotate(0, x, 0);
      // Rotate inplace
      if (x > float.Epsilon) {
        //_animator.Play("RotateRight");
      }
      else if (x < -float.Epsilon) {
        //_animator.Play("RotateLeft");
      }
    }
  }
}