/*
  Idling.cs
  Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Player.States {
  public class Idling : FiniteStateMonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;
    //private Animation _animation;

    public override void Enter() {
      //_animation.CrossFade("Idle");
    }

    public override void Exit() {
    }

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
      //_animation = GetComponentInParent<Animation>();
    }

    private void Update() {
      if (!_finiteStateMachine.IsActive(this)) return;

      if (Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon) {
        _finiteStateMachine.ChangeState(typeof(Walking).Name);
      }

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;

      // Rotate inplace
      if (Mathf.Abs(x) > float.Epsilon) {
        transform.parent.parent.Rotate(0, x, 0);
      }
    }
  }
}