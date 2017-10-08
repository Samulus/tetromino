using UnityEngine;

namespace Entities.Player.States {
	public class PickUp : FiniteStateMonoBehaviour {
		
    private FiniteStateMachine _finiteStateMachine;
    private Animator _animator;

		public override void Enter() {
			/* Play Animation For Picking Up Item */
		}

		public override void Exit() {
		}
		
		private void Start () {
      _finiteStateMachine = transform.root.GetComponentInChildren<FiniteStateMachine>();
      _animator = transform.root.GetComponentInChildren<Animator>();
		}

		private void AddItemToInventory() {
      //var pickUp = transform.root.GetComponentInChildren<ItemPickupZone>();
		}
		
		void Update () {
		}

	}
}
