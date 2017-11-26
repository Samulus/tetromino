/*
 * CameraController.cs
 * Author: Samuel Vargas
 *
 * Allows the player to rotate the Camera around the
 * level.
 *
 * Ensures that all of the level is visible at the same
 * time.
 */

using UnityEngine;

namespace Camera {

  public class CameraController : MonoBehaviour {
    public UnityEngine.Camera Cam;
    public GameObject lookAt;
    public float SpeedMetersPerSec;
    private float maxPercent = 0.7f;

    private bool OrbitCamera() {
      if (Input.GetMouseButton(2)) {
        if (Input.GetAxis("Mouse X") < 0) {
          transform.RotateAround(lookAt.transform.position, Vector3.up, Time.deltaTime * SpeedMetersPerSec);
          return true;
        }
        if (Input.GetAxis("Mouse X") > 0) {
          transform.RotateAround(lookAt.transform.position, Vector3.up, -Time.deltaTime * SpeedMetersPerSec);
          return true;
        }
      }
      return false;
    }

    private void Update() {
      if (Input.GetKey("q")) {
        transform.RotateAround(lookAt.transform.position, Vector3.up, Time.deltaTime * SpeedMetersPerSec);
      }

      else if (Input.GetKey("e")) {
        transform.RotateAround(lookAt.transform.position, Vector3.up, -Time.deltaTime * SpeedMetersPerSec);
      }


      if (Input.GetAxis("Mouse ScrollWheel") > 0) {
        Cam.orthographicSize -= 0.1f;
      }
      else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
        Cam.orthographicSize += 0.1f;
      }
    }
  }

}