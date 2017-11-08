using Tags;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotIdle : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      animator.SetBool("isIdle", true);
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
      animator.transform.root.Rotate(0, x, 0);


      // Check if we're allowed to push a Sokoblock.
      if (Input.GetKey("space")) {
        PushSokoBlock();
      }

      // Move forward
      if (Input.GetKeyDown("w")) {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
      }
    }

    private void PushSokoBlock() {
      var itemPrescenceZone = GameObject.Find("Player/Sensor/ItemPushZone").GetComponent<TagPrescenceZone>();
      var obstructionPrescenceZone =
        GameObject.Find("Player/Sensor/ObstructionPushZone").GetComponent<TagPrescenceZone>();
      var obstructionPresent = obstructionPrescenceZone.IsEmpty();
      var itemPresent = itemPrescenceZone.IsEmpty();

      /*
      if (!obstructionPresent && itemPresent && itemPrescenceZone.Cont) {
      }
      */
    }
  }

}