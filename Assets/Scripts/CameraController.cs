using UnityEngine;

public class CameraController : MonoBehaviour {
  public float speedH = 2.0f;
  public float speedV = 2.0f;

  private Vector3 pos;

  // Use this for initialization
  void Start () {
    pos = this.transform.position;
  }
	
  // Update is called once per frame
  void Update () {
    pos.x += speedH * Input.GetAxis("Horizontal");
    pos.z -= speedV * Input.GetAxis("Vertical");
    transform.position = new Vector3(pos.x, pos.y, pos.z);
    transform.LookAt(Vector3.zero);
  }
}