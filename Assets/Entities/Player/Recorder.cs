using UnityEngine;

namespace Player {
  public class Recorder : MonoBehaviour {
    private State _state;

    private void Awake() {
      _state = GetComponent<State>();
    }

    private void Start() {
      if (!_state.IsActive()) {
      }
    }
  }
}