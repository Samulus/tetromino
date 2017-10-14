/*
    Walking.cs
    Author: Samuel Vargas
*/

using Entities.Player.Sensors;
using Player;
using UnityEngine;

namespace Entities.Player.States {
  public class Walking : FiniteStateMonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;
    private Animator _animator;
    private CharacterController _characterController;
    private CliffDetect _cliffDetect;
    private Inventory.Inventory _inventory;
    private ItemPickupZone _itemPickupZone;
    private ObstructionPickupZone _obstructionPickupZone;

    public override void Enter() {
      //_animator.SetTrigger("Walk");
    }

    public override void Exit() {
    }

    protected void Awake() {
      _finiteStateMachine = transform.root.GetComponentInChildren<FiniteStateMachine>();
      _animator = transform.root.GetComponentInChildren<Animator>();
      _characterController = transform.root.GetComponentInChildren<CharacterController>();
      _cliffDetect = transform.root.GetComponentInChildren<CliffDetect>();
      _itemPickupZone = transform.root.GetComponentInChildren<ItemPickupZone>();
      _obstructionPickupZone = transform.root.GetComponentInChildren<ObstructionPickupZone>();
      _inventory = transform.root.GetComponentInChildren<Inventory.Inventory>();
    }

    private void Update() {
      PickUpIfRequested();

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
      var z = Input.GetAxis("Vertical") * Time.deltaTime;

      // Transfer back to Idling if the user has stopped walking.
      if (Mathf.Abs(x) < float.Epsilon &&
          Mathf.Abs(z) < float.Epsilon) {
        _finiteStateMachine.ChangeState(typeof(Idling).Name);
        return;
      }

      transform.root.Rotate(0, x, 0);

      if (!_cliffDetect.IsCliffInfront()) {
        var forward = transform.root.TransformDirection(Vector3.forward);
        _characterController.Move(forward * Time.deltaTime);
        _characterController.SimpleMove(Vector3.zero);
        //_animator.SetTrigger("Walk");
      }
    }

    private void PickUpIfRequested() {
      var spacePressed = Input.GetKeyDown("space");
      if (!spacePressed) return;

      // Drop held item
      if (_inventory.HasItem()) {
        _inventory.DropItem();
        return;
      }

      var isObstructionPresent = _obstructionPickupZone.IsObstructionPresent();
      var isPickupPresent = _itemPickupZone.IsPickUpPresent();

      // Pickup a new item.
      if (!isObstructionPresent && isPickupPresent) {
        var item = _itemPickupZone.GetPickUp();
        _inventory.AddItem(item);
      }
    }
  }
}