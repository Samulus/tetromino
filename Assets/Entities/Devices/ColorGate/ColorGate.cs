/*
	ColorGate.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorGate {

    public class ColorGate : MonoBehaviour {
        public Material ExpectedMaterial;
        private BoxCollider _gateCollider;
        private BoxCollider _colorDetectionZone;
        private Animation _animation;

        // Use this for initialization
        private void Start() {
            transform.GetComponentInChildren<SkinnedMeshRenderer>().material = ExpectedMaterial;
            SetupGateCollider();
            SetupColorDetectionZone();
            _animation.playAutomatically = false;
        }

        /// <summary>
        /// Creates a BoxCollider with (isTrigger=0) to prevent the 
        /// player from passing through the gate iff they are the
        /// incorrect color.
        /// </summary>
        private void SetupGateCollider() {
            _gateCollider = gameObject.AddComponent<BoxCollider>();
            // TODO: Magic constants 
            _gateCollider.center = new Vector3(0f, 1f, 0f); 
            _gateCollider.size = new Vector3(1f, 2f, 0.1f);
        }

        /// <summary>
        /// Creates a BoxCollider with (isTrigger=1) to check
        /// if the player is the correct color, if they are then the gate is opened.
        /// </summary>
        private void SetupColorDetectionZone() {
            _colorDetectionZone = gameObject.AddComponent<BoxCollider>();
            _colorDetectionZone.center = new Vector3(0f, 1.0f, -0.5f);
            _colorDetectionZone.size = new Vector3(1f, 2f, 1f);
            _colorDetectionZone.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                var omat = other.GetComponentInChildren<SkinnedMeshRenderer>().material;
                if (omat.color == ExpectedMaterial.color) {
                    _animation = GetComponent<Animation>();
                    _animation.Play("Ascend", PlayMode.StopAll);
                    _animation.
                }
            }
        }
        
    }

}