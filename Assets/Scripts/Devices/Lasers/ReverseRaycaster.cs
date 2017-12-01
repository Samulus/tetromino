/*
 * ReverseRaycaster.cs
 * Author: Samuel Vargas
 *
 * This module is a workaround for the fact that Unity
 * doesn't allow us to get the exact Vector3 point where
 * one BoxCollider made contact with another.
 */

using UnityEngine;

namespace Devices.Lasers {

  public class ReverseRaycaster : MonoBehaviour {
  
    /*
   * When a GameObject collides with a Laser, the GameObject can use the
   * GetComponent<LaserRaycaster>.ReverseGetImpactPoint(transform) to figure
   * out the exact point at which the collision occured. Unity doesn't provide
   * a way to get the exact point that caused a OnTriggerXXX() event to occur
   * so this is a workaround.
   */

    public Vector3 ReverseGetImpactPoint(Transform them, out Vector3 point, out Vector3 normal) {
      var hits = Physics.RaycastAll(transform.position, transform.forward, 100);
      Debug.DrawRay(transform.position, transform.forward, Color.cyan, 100);
      point = Vector3.negativeInfinity;
      normal = Vector3.negativeInfinity;

      foreach (var h in hits) {
        Debug.Log(h.transform);
        if (h.transform == them) {
          return h.point;
        }
      }

      Debug.AssertFormat(false,
                         "{0} called ReverseGetImpactPoint but {1}' shot a ray and couldn't find a collision point",
                         them.name, transform.name);

      return Vector3.negativeInfinity;
    }
  }

}