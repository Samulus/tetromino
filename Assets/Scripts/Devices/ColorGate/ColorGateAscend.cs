/*
 * ColorGateAscend.cs
 * Author: Samuel Vargas
 *
 * When the player has exited the ColorGate zone switch to the Descend
 * state.
 */

using UnityEngine;

namespace Devices.ColorGate {

  public class ColorGateAscend : StateMachineBehaviour {
    private ColorGateController _colorGateController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _colorGateController = animator.gameObject.GetComponentInChildren<ColorGateController>();
      Debug.Assert(_colorGateController != null, "_colorGateController != null");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (!_colorGateController.IsOpen()) {
        animator.SetBool("isAscend", false);
        animator.SetBool("isDescend", true);
      }
    }
  }

}