using UnityEngine;

public class CameraController : MonoBehaviour {
  public GameObject Player;
  public float SpeedMetersPerSec;
  public float EyeHeight;
  private Vector3 _eye;

  void Start() {
    _eye = Player.transform.position;
    _eye.y -= EyeHeight;
    transform.LookAt(_eye);
  }

  void Update() {
    if (Input.GetKey(KeyCode.LeftArrow)) {
      transform.RotateAround(_eye, Vector3.up, Time.deltaTime * SpeedMetersPerSec);
    }

    else if (Input.GetKey(KeyCode.RightArrow)) {
      transform.RotateAround(_eye, Vector3.up, -Time.deltaTime * SpeedMetersPerSec);
    }
    
    if (Input.GetKey(KeyCode.UpArrow)) {
      transform.RotateAround(_eye, Vector3.left, Time.deltaTime * SpeedMetersPerSec);
    }
    else if (Input.GetKey(KeyCode.DownArrow)) {
      transform.RotateAround(_eye, Vector3.left, -Time.deltaTime * SpeedMetersPerSec);
    }
  }
}