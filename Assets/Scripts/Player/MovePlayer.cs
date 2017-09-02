using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
  private CharacterController controller;

  void Start() {
    controller = GetComponent<CharacterController>();
  }

  void Update() {
    float vertical = Input.GetAxis("Vertical");
    float horizontal = Input.GetAxis("Horizontal");
    
    if (Input.GetKeyDown("w")) {
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      controller.Move(forward);
    }

    else if (Input.GetKeyDown("s")) {
      Vector3 back = transform.TransformDirection(Vector3.back);
      controller.Move(back);
    }

    if (Input.GetKeyDown("d")) {
      transform.Rotate(Vector3.up, 90);
    }

    else if (Input.GetKeyDown("a")) {
      transform.Rotate(Vector3.up, -90);
    }
  }
}