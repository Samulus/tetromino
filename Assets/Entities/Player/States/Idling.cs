/*
  Idling.cs
  Author: Samuel Vargas
*/

using Entities.Player.Sensors;
using UnityEngine;

namespace Entities.Player.States {
  public class Idling : FiniteStateMonoBehaviour {
    private FiniteStateMachine _finiteStateMachine;
    private Animator _animator;
    private ItemPickupZone _itemPickupZone;
    private ObstructionPickupZone _obstructionPickupZone;
    private Inventory.Inventory _inventory;

    public override void Enter() {
      _animator.SetTrigger("Idle");
    }

    public override void Exit() {
    }

    private void Start() {
      _finiteStateMachine = transform.root.GetComponentInChildren<FiniteStateMachine>();
      _animator = transform.root.GetComponentInChildren<Animator>();
      _itemPickupZone = transform.root.GetComponentInChildren<ItemPickupZone>();
      _obstructionPickupZone = transform.root.GetComponentInChildren<ObstructionPickupZone>();
      _inventory = transform.root.GetComponentInChildren<Inventory.Inventory>();
    }

    private void Update() {
      PickUpIfRequested();
      MaybeTransitionToWalking();
    }

    private void MaybeTransitionToWalking() {
      if (Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon) {
        _finiteStateMachine.ChangeState(typeof(Walking).Name);
      }

      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
      transform.root.Rotate(0, x, 0);
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