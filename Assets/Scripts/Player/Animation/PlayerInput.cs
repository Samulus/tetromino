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

    public static bool RequestToPushRotateableMirror(out GameObject rotateableMirror) {
      var tag = GameObject.Find("ItemPushZone").GetComponent<TagPrescenceZone>();

      // TODO: Silly workaround, right now the tag is attached to the back of the
      // rotateable mirror. If we use the found object directly then we move the 
      // back of the mirror without updating the front. Select the parent to account
      // for this.

      if (tag.ContainsAtLeastOnceDevice(DeviceId.RotateableMirrorLeft, out rotateableMirror)) {
        rotateableMirror = rotateableMirror.transform.parent.gameObject;
        return PushingButtonHeld();
      }

      if (tag.ContainsAtLeastOnceDevice(DeviceId.RotateableMirrorRight, out rotateableMirror)) {
        rotateableMirror = rotateableMirror.transform.parent.gameObject;
        return PushingButtonHeld();
      }


      return false;
    }

    public static bool RequestToPullRotateableMirror(out GameObject rotateableMirror) {
      var tag = GameObject.Find("ItemPushZone").GetComponent<TagPrescenceZone>();
      if (tag.ContainsAtLeastOnceDevice(DeviceId.RotateableMirrorLeft, out rotateableMirror)) {
        rotateableMirror = rotateableMirror.transform.parent.gameObject;
        return PullingButtonHeld();
      }

      if (tag.ContainsAtLeastOnceDevice(DeviceId.RotateableMirrorRight, out rotateableMirror)) {
        rotateableMirror = rotateableMirror.transform.parent.gameObject;
        return PullingButtonHeld();
      }

      return false;
    }
  }

}