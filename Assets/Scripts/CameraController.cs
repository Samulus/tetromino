using UnityEngine;

public class CameraController : MonoBehaviour {
  public GameObject Player;
  public float SpeedMetersPerSec;
  public float EyeHeight;
  private Vector3 _eye;

  void Start() {
    /*
    _eye = Player.transform.position;
    _eye.y -= EyeHeight;
    _eye = Vector3.zero;
    */
  }

  void Update() {
    /*
    if (Input.GetKey("q")) {
      transform.RotateAround(_eye, Vector3.up, Time.deltaTime * SpeedMetersPerSec);
    }

    else if (Input.GetKey("e")) {
      transform.RotateAround(_eye, Vector3.up, -Time.deltaTime * SpeedMetersPerSec);
    }
    */
  }
}