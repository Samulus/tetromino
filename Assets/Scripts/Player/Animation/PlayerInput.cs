/*
 * PlayerInput.cs
 * Author: Samuel Vargas
 */

using Entities.Player.Sensors;
using Tags;
using UnityEngine;

namespace Player.Animation {

  public static class PlayerInput {
    public static bool RequestToWalkForward(ref Animator animator) {
      var z = Input.GetAxis("Vertical") * Time.deltaTime * 250.0f;
      var cliffDetect = animator.transform.root.GetComponentInChildren<CliffDetect>();
      return z > 0.0f && !Input.GetKey(KeyCode.LeftShift) && !cliffDetect.IsFacingCliff();
    }
    
    public static bool RequestToWalkBackward(ref Animator animator) {
      var z = Input.GetAxis("Vertical") * Time.deltaTime * 250.0f;
      var cliffDetect = animator.transform.root.GetComponentInChildren<CliffDetect>();
      return z < 0.0f && Input.GetKey(KeyCode.S) && !cliffDetect.IsFacingCliff();
    }

    public static bool RequestToRunForward(ref Animator animator) {
      var z = Input.GetAxis("Vertical") * Time.deltaTime * 250.0f;
      var cliffDetect = animator.transform.root.GetComponentInChildren<CliffDetect>();
      return z > 0.0f && Input.GetKey(KeyCode.LeftShift) && !cliffDetect.IsFacingCliff();
    }

    public static bool PushingButtonHeld() {
      return Input.GetKey(KeyCode.Space);
    }
    
    public static bool PullingButtonHeld() {
      return Input.GetKey(KeyCode.S);
    }

    public static bool RequestToPushSokoBlock(out GameObject sokoBlock) {
      var tag = GameObject.Find("ItemPushZone").GetComponent<TagPrescenceZone>();
      var sokoBlockPresent = tag.ContainsAtLeastOnceDevice(DeviceId.SokoBlock, out sokoBlock);
      return PushingButtonHeld() && sokoBlockPresent;
    }

    public static bool RequestToPullSokoBlock(out GameObject sokoBlock) {
      var tag = GameObject.Find("ItemPushZone").GetComponent<TagPrescenceZone>();
      var sokoBlockPresent = tag.ContainsAtLeastOnceDevice(DeviceId.SokoBlock, out sokoBlock);
      return PullingButtonHeld() && sokoBlockPresent;
    }
  }

}