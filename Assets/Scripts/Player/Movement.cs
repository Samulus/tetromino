using UnityEngine;

namespace Player {
  public class Movement : MonoBehaviour {
    private Animation _animation;
    private CharacterController _characterController;
    private State _state;
    private CliffDetect _cliffDetect;

    private void Awake() {
      _animation = GetComponent<Animation>();
      _characterController = GetComponent<CharacterController>();
      _cliffDetect = GetComponent<CliffDetect>();
      _state = GetComponent<State>();
    }

    private void Start() {
      _animation.Play("Idle", PlayMode.StopAll);
    }

    private void Update() {
      // Avoid moving if our instance isn't active.
      if (!_state.IsActive()) {
        return;
      }

      // Left / Right Rotations
      if (Input.GetKeyDown("d")) {
        transform.Rotate(Vector3.up, 90);
      }

      if (Input.GetKeyDown("a")) {
        transform.Rotate(Vector3.up, -90);
      }

      // Allow the robots to walk forward and backwards.
      if (!_cliffDetect.IsCliffInfront() && Input.GetKey("w")) {
        _animation.Play("Walk", PlayMode.StopAll);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        _characterController.SimpleMove(forward);
      }
      else if (!_cliffDetect.IsCliffBehind() && Input.GetKey("s")) {
        _animation.Play("Walk", PlayMode.StopAll); // TODO: Backpedaling animation.
        Vector3 forward = transform.TransformDirection(Vector3.back);
        _characterController.SimpleMove(forward);
      }
      else {
        _animation.Play("Idle", PlayMode.StopAll);
      }
    }
  }
}