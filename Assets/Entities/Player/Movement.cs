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
            /*
            if (!_state.IsActive()) {
                return;
            }
            */
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime;
            if (x != 0.0f) {
                transform.Rotate(0, x, 0);
            }
            if (z > 0.0f) {
                _animation.Play("Walk", PlayMode.StopAll);
                if (!_cliffDetect.IsCliffInfront()) {
                    var forward = transform.TransformDirection(Vector3.forward);
                    _characterController.SimpleMove(forward * 1.5f);
                }
            }
            if (x == 0.0f && z == 0.0f) {
                _animation.Play("Idle", PlayMode.StopAll);
            }
            
            if (Input.GetKey("space")) {
                _animation.Play("PickUp", PlayMode.StopAll);
            }
        }
    }

}