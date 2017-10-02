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
            _animation = GetComponent<Animation>();
            _animation.playAutomatically = false;
        }

        /// <summary>
        /// Creates a BoxCollider with (isTrigger=0) to prevent the 
        /// player from passing through the gate iff they are the
        /// incorrect color.
        /// </summary>
        private void SetupGateCollider() {
            _gateCollider = gameObject.AddComponent<BoxCollider>();
            _gateCollider.center = new Vector3(0f, 1f, 0f); // TODO: Magic Constants
            _gateCollider.size = new Vector3(1f, 2f, 0.1f); // TODO: Magic Constants
        }

        /// <summary>
        /// Creates a BoxCollider with (isTrigger=1) to check
        /// if the player is the correct color, if they are then the gate is opened.
        /// </summary>
        private void SetupColorDetectionZone() {
            _colorDetectionZone = gameObject.AddComponent<BoxCollider>();
            _colorDetectionZone.center = new Vector3(0f, 1.0f, -0.5f); // TODO: Magic Constants
            _colorDetectionZone.size = new Vector3(1f, 2f, 1f); // TODO: Magic Constants
            _colorDetectionZone.isTrigger = true;
        }

        /// <summary>
        /// Play the animation for opening the BoxCollider iff
        /// the player is the correct color.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            var omat = other.GetComponentInChildren<SkinnedMeshRenderer>().material;
            if (omat.color == ExpectedMaterial.color) {
                //_animation["Ascend"].normalizedTime = 1.0f;
                //_animation["Ascend"].speed = 1.0f;
                //_animation.Play("Ascend", PlayMode.StopAll);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player")) return;
            Debug.Log(_animation["Ascend"].time);
            _animation["Ascend"].normalizedTime = 1.0f;
            _animation["Ascend"].speed *= -1.0f;
            //_animation.CrossFade("Ascend");
            _animation.Play("Ascend", PlayMode.StopAll);
        }
        
    }

}