using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour {
  public bool debug;
  private CharacterController _controller;
  
  private GameObject _ChestCollision;
  private GameObject _WaistCollision;
  private GameObject _FloorCollision;
  private GameObject _AbyssCollision;
  private GameObject _BackpedalCollision;

  private string _ChestCollisionName = "_ChestCollision";
  private string _WaistCollisionName = "_WaistCollision";
  private string _FloorCollisionName = "_FloorCollision";
  private string _AbyssCollisionName = "_AbyssCollision";
  private string _BackpedalCollisionName = "_BackpedalCollision";
  
  void Start() {
    _controller = GetComponent<CharacterController>();
    _SetupCollision(ref _ChestCollision, "_ChestCollision", +0.5f, +1.0f);
    _SetupCollision(ref _WaistCollision, "_WaistCollision", -0.5f, +1.0f);
    _SetupCollision(ref _FloorCollision, "_FloorCollision", -1.5f, +1.0f);
    _SetupCollision(ref _AbyssCollision, "_AbyssCollision", -2.5f, +1.0f);
    _SetupCollision(ref _BackpedalCollision, "_BackpedalCollision", -1.5f, -1.0f);
  }
  
  void Update() {
    // Cardinal Movement
    if (Input.GetKeyDown("w") && CanMoveForward()) {
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      _controller.Move(forward);
    }

    else if (Input.GetKeyDown("s") && CanMoveBack()) {
      Vector3 back = transform.TransformDirection(Vector3.back);
      _controller.Move(back);
    }

    // Rotations
    if (Input.GetKeyDown("d")) {
      transform.Rotate(Vector3.up, 90);
    }

    else if (Input.GetKeyDown("a")) {
      transform.Rotate(Vector3.up, -90);
    }

    // Ascend / Descend
    if (Input.GetKeyDown("space")) {
      if (CanClimb()) {
        transform.Translate(Vector3.up);
        transform.Translate(Vector3.forward);
      }
      else if (CanDescend()) {
        transform.Translate(Vector3.forward);
        transform.Translate(Vector3.down);
      }
    }
  }

  private bool CanMoveForward() {
    Collider[] tetrominos =
      Physics.OverlapBox(_FloorCollision.transform.position, _FloorCollision.transform.localScale);

    foreach (Collider c in tetrominos) {
      if (c.name != _FloorCollisionName && c.transform != transform) {
        return true;
      }
    }

    return false;
  }

  private bool CanMoveBack() {
    Collider[] tetrominos =
      Physics.OverlapBox(_BackpedalCollision.transform.position, _BackpedalCollision.transform.localScale);

    foreach (Collider c in tetrominos) {
      if (c.name != _BackpedalCollisionName && c.transform != transform) {
        return true;
      }
    }

    return false;
  }


  private bool CanDescend() {
    Collider[] tetrominos =
      Physics.OverlapBox(_FloorCollision.transform.position, _FloorCollision.transform.localScale);

    // If the floor piece collides with something we can't descend.
    foreach (Collider c in tetrominos) {
      if (c.name != _FloorCollisionName && c.transform != transform) {
        return false;
      }
    }

    // If the abyss piece collides with the floor then we can descend
    // another level
    Collider[] abyssPieces =
      Physics.OverlapBox(_AbyssCollision.transform.position, _AbyssCollision.transform.localScale);

    foreach (Collider c in abyssPieces) {
      if (c.name != _AbyssCollisionName && c.transform != transform) {
        return true;
      }
    }

    return false;
  }

  private bool CanClimb() {
    // If the player's chest collision cube collides with a wall then
    // they can NOT climb.
    Collider[] chestBlocks =
      Physics.OverlapBox(_ChestCollision.transform.position, _ChestCollision.transform.localScale);

    foreach (Collider c in chestBlocks) {
      if (c.name != _ChestCollisionName && c.transform != transform) {
        return false;
      }
    }

    // If the player has a cube colliding with their waist checker then 
    // they are allowed to climb.
    Collider[] waistBlocks =
      Physics.OverlapBox(_WaistCollision.transform.position, _WaistCollision.transform.localScale);

    foreach (Collider c in waistBlocks) {
      if (c.name != _WaistCollisionName && c.transform != transform) {
        return true;
      }
    }

    // No collisions detected -> no climbing.
    return false;
  }

  private void _SetupCollision(ref GameObject obj, string name, float yOffset, float zOffset) {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z + zOffset);
    float quarter = 0.25f;
    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    obj.transform.position = origin;
    obj.transform.localScale = new Vector3(quarter, quarter, quarter);
    obj.transform.parent = transform;
    obj.transform.name = name;
  }
}