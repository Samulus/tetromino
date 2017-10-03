using UnityEngine;

namespace Entities.Player.States {
  public class Idling : MonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;

    private void Start() {
      _finiteStateMachine = GetComponentInParent<FiniteStateMachine>();
    }

    private void Update() {
      if (!_finiteStateMachine.IsActive(this)) return;
    }
    
  }
}