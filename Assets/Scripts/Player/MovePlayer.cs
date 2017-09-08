using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour {
  public bool debug;
  private CharacterController _controller;

  /*
    Chest / Waist / Floor / Backpedal Collision Cubes
   */

  private string _ChestCollisionName = "_ChestCollisionCube";
  private GameObject _ChestCollisionCube;
  private float _ChestCollisionRadius;

  private string _WaistCollisionName = "_WaistCollisionCube";
  private GameObject _WaistCollisionCube;
  private float _WaistCollisionRadius;

  private string _FloorCollisionName = "_FloorCollisionCube";
  private GameObject _FloorCollisionCube;
  private float _FloorCollisionRadius;

  private string _AbyssCollisionName = "_AbyssCollisionCube";
  private GameObject _AbyssCollisionCube;
  private float _AbyssCollisionRadius;

  private string _BackpedalCollisionName = "_BackpedalCollisionCube";
  private GameObject _BackpedalCollisionCube;
  private float _BackpedalCollisionRadius;

  void Start() {
    _controller = GetComponent<CharacterController>();

    // Set the position of the the climbCollisionOrigin to right infront of the player,
    // Set the half to 1 meter.
    _SetupChestCollisionCube();
    _SetupWaistCollisionCube();
    _SetupFloorCollisionCube();
    _SetupAbyssCollisionCube();
    _SetupBackpedalCollisionCube();
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
      Physics.OverlapBox(_FloorCollisionCube.transform.position, _FloorCollisionCube.transform.localScale);

    foreach (Collider c in tetrominos) {
      if (c.name != _FloorCollisionName && c.transform != transform) {
        return true;
      }
    }

    return false;
  }

  private bool CanMoveBack() {
    Collider[] tetrominos =
      Physics.OverlapBox(_BackpedalCollisionCube.transform.position, _BackpedalCollisionCube.transform.localScale);

    foreach (Collider c in tetrominos) {
      if (c.name != _BackpedalCollisionName && c.transform != transform) {
        return true;
      }
    }

    return false;
  }


  private bool CanDescend() {
    Collider[] tetrominos =
      Physics.OverlapBox(_FloorCollisionCube.transform.position, _FloorCollisionCube.transform.localScale);

    // If the floor piece collides with something we can't descend.
    foreach (Collider c in tetrominos) {
      if (c.name != _FloorCollisionName && c.transform != transform) {
        return false;
      }
    }

    // If the abyss piece collides with the floor then we can descend
    // another level
    Collider[] abyssPieces =
      Physics.OverlapBox(_AbyssCollisionCube.transform.position, _AbyssCollisionCube.transform.localScale);

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
      Physics.OverlapBox(_ChestCollisionCube.transform.position, _ChestCollisionCube.transform.localScale);

    foreach (Collider c in chestBlocks) {
      if (c.name != _ChestCollisionName && c.transform != transform) {
        return false;
      }
    }

    // If the player has a cube colliding with their waist checker then 
    // they are allowed to climb.
    Collider[] waistBlocks =
      Physics.OverlapBox(_WaistCollisionCube.transform.position, _WaistCollisionCube.transform.localScale);

    foreach (Collider c in waistBlocks) {
      if (c.name != _WaistCollisionName && c.transform != transform) {
        return true;
      }
    }

    // No collisions detected -> no climbing.
    return false;
  }
  
  private void _SetupWaistCollisionCube() {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 1.0f);
    float half = 0.25f;
    _WaistCollisionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    _WaistCollisionCube.transform.position = origin;
    _WaistCollisionCube.transform.localScale = new Vector3(half, half, half);
    _WaistCollisionCube.transform.parent = transform;
    _WaistCollisionCube.transform.name = _WaistCollisionName;
  }

  private void _SetupChestCollisionCube() {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1.0f);
    float half = 0.25f;
    _ChestCollisionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    _ChestCollisionCube.transform.position = origin;
    _ChestCollisionCube.transform.localScale = new Vector3(half, half, half);
    _ChestCollisionCube.transform.parent = transform;
    _ChestCollisionCube.transform.name = _ChestCollisionName;
  }

  private void _SetupFloorCollisionCube() {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z + 1.0f);
    float half = 0.25f;
    _FloorCollisionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    _FloorCollisionCube.transform.position = origin;
    _FloorCollisionCube.transform.localScale = new Vector3(half, half, half);
    _FloorCollisionCube.transform.parent = transform;
    _FloorCollisionCube.transform.name = _FloorCollisionName;
  }

  private void _SetupAbyssCollisionCube() {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z + 1.0f);
    float half = 0.25f;
    _AbyssCollisionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    _AbyssCollisionCube.transform.position = origin;
    _AbyssCollisionCube.transform.localScale = new Vector3(half, half, half);
    _AbyssCollisionCube.transform.parent = transform;
    _AbyssCollisionCube.transform.name = _AbyssCollisionName;
  }

  private void _SetupBackpedalCollisionCube() {
    Vector3 origin = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z - 1.0f);
    float half = 0.25f;
    _BackpedalCollisionCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    _BackpedalCollisionCube.transform.position = origin;
    _BackpedalCollisionCube.transform.localScale = new Vector3(half, half, half);
    _BackpedalCollisionCube.transform.parent = transform;
    _BackpedalCollisionCube.transform.name = _BackpedalCollisionName;
  }
}