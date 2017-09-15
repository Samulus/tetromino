using UnityEngine;
using MoonSharp.Interpreter;

public class AnimationTest : MonoBehaviour {
  private Animation _animation;
  private CharacterController _characterController;
  private CliffEdgeDetection _cliffEdgeDetection;
  private ItemDetection _itemDetection;
  private Inventory _inventory;

  private static AnimationTest selected;

  void Awake() {
    _animation = GetComponent<Animation>();
    _characterController = GetComponent<CharacterController>();
    _cliffEdgeDetection = GetComponent<CliffEdgeDetection>();
    _itemDetection = GetComponent<ItemDetection>();
    _inventory = GetComponent<Inventory>();
  }
  
  void OnMouseDown() {
    selected = this;
  }
  
  void Update() {
    if (selected == null || selected != this) {
      return;
    }
    // Rotations
    if (Input.GetKeyDown("d")) {
      transform.Rotate(Vector3.up, 90);
    }

    if (Input.GetKeyDown("a")) {
      transform.Rotate(Vector3.up, -90);
    }

    if (_itemDetection.isInfrontOfItem() && Input.GetKey("e")) {
      _animation.Play("PickUp", PlayMode.StopAll);
      //_inventory.pickUp();
      return;
    }

    if (!_cliffEdgeDetection.IsFacingCliff() && Input.GetKey("space")) {
      _animation.Play("Walk", PlayMode.StopAll);
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      _characterController.SimpleMove(forward);
    }
    else {
      _animation.Play("Idle", PlayMode.StopAll);
    }
  }
}