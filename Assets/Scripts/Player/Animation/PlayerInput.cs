/*
 * PlayerInput.cs
 * Author: Samuel Vargas
 *
 * The `PlayerActions.cs` module contains misc methods that the Animator states
 * can invoke. Specifically this module allows the states to determine
 * when the user is pressing key inputs and behave / transition states
 * appropriately.
 *
 * Note that this module also checks if the Input is valid and returns false.
 *
 * If the user is standing at a cliff and hits 'w' AttemptToWalkForward
 * would return false.
 */

using Devices.SokoBlock;
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

    public static bool RequestToRunForward(ref Animator animator) {
      var z = Input.GetAxis("Vertical") * Time.deltaTime * 250.0f;
      var cliffDetect = animator.transform.root.GetComponentInChildren<CliffDetect>();
      return z > 0.0f && Input.GetKey(KeyCode.LeftShift) && !cliffDetect.IsFacingCliff();
    }

    public static bool RequestToPushSokoBlock(out SokoBlockPusher sokoBlockPusher) {
      sokoBlockPusher = null;
      
      var itemPrescenceZone = GameObject.Find("Player/Sensor/ItemPushZone").GetComponent<TagPrescenceZone>();
      GameObject maybeSokoBlockPusher = null;
      if (itemPrescenceZone.GetFirstDevice(DeviceId.SokoBlock, out maybeSokoBlockPusher)) {
        sokoBlockPusher = maybeSokoBlockPusher.GetComponent<SokoBlockPusher>();
      }
      
      if (!Input.GetKey(KeyCode.Space)) {
        if (sokoBlockPusher != null) {
          sokoBlockPusher.StopPushing();
        }
        return false;
      }

      return sokoBlockPusher != null;
    }
  }

}