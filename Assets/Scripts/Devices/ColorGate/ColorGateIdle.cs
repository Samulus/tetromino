/*
 * ColorGateIdle.cs
 * Author: Samuel Vargas
 *
 * Continually checks for the prescence of the player in the ColorGate
 * zone. If the player enters and has the correct color it transitions
 * to 
 */

using UnityEngine;

namespace Devices.ColorGate {

  public class ColorGateIdle : StateMachineBehaviour {
    private ColorGateController _colorGateController;

    private void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
      _colorGateController = animator.gameObject.GetComponentInChildren<ColorGateController>();
      Debug.Assert(_colorGateController != null, "_colorGateController != null");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_colorGateController.IsOpen()) {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAscend", true);
      }
    }
  }

}