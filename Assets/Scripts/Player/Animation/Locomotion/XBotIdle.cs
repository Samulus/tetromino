using Devices.SokoBlock;
using MultiPurpose;
using UnityEngine;

namespace Player.Animation.Locomotion {

  public class XBotIdle : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      animator.SetBool("isIdle", true);
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
      animator.transform.root.Rotate(0, x, 0);

      var itemPrescenceZone = GameObject.Find("Player/Sensor/ItemPushZone").GetComponent<ItemPrescenceZone>();
      var obstructionPrescenceZone =
        GameObject.Find("Player/Sensor/ObstructionPushZone").GetComponent<ItemPrescenceZone>();


      // Check if we can push a SokoBlock
      if (Input.GetKey("space")) {
        Debug.Log("Space");
        var obstructionPresent = obstructionPrescenceZone.GetItem() != null;
        var itemPresent = itemPrescenceZone.GetItem() != null;

        if (!obstructionPresent && itemPresent) {
          itemPrescenceZone.GetItem().GetComponent<SokoBlockPusher>().Push();
        }
      }

      // Move forward
      if (Input.GetKeyDown("w")) {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
      }
    }
  }

}