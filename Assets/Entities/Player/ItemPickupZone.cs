/*
	ItemPickupZone.cs
	Author: Samuel Vargas
	
	This module creates two BoxColliders on top of each other. 
	The BoxColliders are used in conjunction with each other to
	determine if the player is allowed to pick up an object infront 
	of them.
	
	(Objects with overhead obstructions cannot be picked up by the player).
*/

using UnityEngine;

namespace Entities.Player {

    public class ItemPickupZone : MonoBehaviour {
        private ObstructionChecker _obstructionChecker;
        private PickupChecker _pickupChecker;

        private void Start() {
            var g = gameObject;
            _obstructionChecker = ObstructionChecker.CreateAndAddAsChild(ref g);
            _pickupChecker = PickupChecker.CreateAndAddAsChild(ref g);
        }

        public bool CanPickupItem() {
            return !_obstructionChecker.IsObstructionPresent() &&
                   _pickupChecker.IsPickUpPresent();
        }

        private class ObstructionChecker : MonoBehaviour {
            private bool _isObstructionPresent;
            private static readonly Vector3 Center = new Vector3(0f, 1.5f, 0.4f); // TODO: Magic Constants
            private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f); // TODO: Magic Constants 

            public static ObstructionChecker CreateAndAddAsChild(ref GameObject g) {
                var gameObject = new GameObject();
                gameObject.transform.SetParent(g.transform, false);
                gameObject.name = typeof(ObstructionChecker).Name;
                var collider = gameObject.AddComponent<BoxCollider>();
                collider.center = Center;
                collider.size = Size;
                collider.isTrigger = true;
                return gameObject.AddComponent<ObstructionChecker>();
            }

            private void OnTriggerEnter(Collider other) {
                _isObstructionPresent = true;
            }

            private void OnTriggerExit(Collider other) {
                _isObstructionPresent = false;
            }

            public bool IsObstructionPresent() {
                return _isObstructionPresent;
            }
        }

        private class PickupChecker : MonoBehaviour {
            private static readonly Vector3 Center = new Vector3(0f, 0.5f, 0.4f); // TODO: Magic Constants
            private static readonly Vector3 Size = new Vector3(0.5f, 1f, 0.25f); // TODO: Magic Constants 
            private bool _isPickUpPresent;

            public static PickupChecker CreateAndAddAsChild(ref GameObject g) {
                var gameObject = new GameObject();
                gameObject.transform.SetParent(g.transform, false);
                gameObject.name = typeof(PickupChecker).Name;
                var collider = gameObject.AddComponent<BoxCollider>();
                collider.center = Center;
                collider.size = Size;
                collider.isTrigger = true;
                return gameObject.AddComponent<PickupChecker>();
            }

            private void OnTriggerEnter(Collider other) {
                if (!other.CompareTag("PickUp")) return;
                _isPickUpPresent = true;
            }

            private void OnTriggerExit(Collider other) {
                if (!other.CompareTag("PickUp")) return;
                _isPickUpPresent = false;
            }

            public bool IsPickUpPresent() {
                return _isPickUpPresent;
            }
        }
    }

}