using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {
  
  private Animation _animation;
  private CharacterController _characterController;

  void Start() {
    _animation = GetComponent<Animation>();
    _characterController = GetComponent<CharacterController>();
  }
  
  void Update() {
    // Rotations
    if (Input.GetKeyDown("d")) {
      transform.Rotate(Vector3.up, 90);
    }

    if (Input.GetKeyDown("a")) {
      transform.Rotate(Vector3.up, -90);
    }
    
    if (Input.GetKey("e")) {
      Debug.Log("PickUp");
      _animation.Play("PickUp", PlayMode.StopAll);
      return;
    }

    if (Input.GetKey("space")) {
      _animation.Play("Walk", PlayMode.StopAll);
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      _characterController.SimpleMove(forward);
    }
    else {
      _animation.Play("Idle", PlayMode.StopAll);

    }
  }
}