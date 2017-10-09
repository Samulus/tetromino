/*
    ColorChangerLogic.cs
    Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorChanger {
    
    public class ColorChanger : MonoBehaviour {
        /*
        private Material _defaultMaterial;
        private void Start() {
            _defaultMaterial = GetComponent<MeshRenderer>().material;
            SetupBoundingBox();
        }

        /// <summary>
        /// Dynamically create BoxColliders around the three sides of the ColorChanger
        /// to keep the player from entering anywhere other than the opening.
        /// </summary>
        private void SetupBoundingBox() {
            // TODO: Magic Constants that were determined by manually
            // resizing bounding boxes and recording the values.
            // I'm only putting them here to avoid cluttering the 
            // Inspector menu.

            var leftWall = gameObject.AddComponent<BoxCollider>();
            leftWall.center = new Vector3(-0.9426f, -0.52057f, 1.0557f);
            leftWall.size = new Vector3(0.0885f, 1.041144f, 2.1145f);

            var rightWall = gameObject.AddComponent<BoxCollider>();
            rightWall.center = new Vector3(-0.04695f, -0.520572f, 1.055725f);
            rightWall.size = new Vector3(0.093910f, 1.041144f, 2.1145f);

            var backWall = gameObject.AddComponent<BoxCollider>();
            backWall.center = new Vector3(-0.493852f, -0.9959415f, 1.0557525f);
            backWall.size = new Vector3(0.987705f, 0.090405f, 2.11145f);
        }

        /// <summary>
        /// Set the ColorChanger box to the color of the laser
        /// </summary>
        /// <param name="other"></param>
        public void OnLaserReceptorEnter(Collider other) {
            Debug.Assert(other.CompareTag("Laser"), "Don't call this method unless a laser enters!");
            transform.GetComponent<MeshRenderer>().material = other.GetComponent<Laser>().LaserMaterial;
        }

        public void OnLaserReceptorExit(Collider other) {
            Debug.Assert(other.CompareTag("Laser"), "Don't call this method unless a laser exits!");
            transform.GetComponent<MeshRenderer>().material = _defaultMaterial;
        }

        public void OnPlayerEnteredColorChanger(Collider other) {
            Debug.Assert(other.CompareTag("Player"), "Don't call this method unless the player enters!");
            var material = _outputColorReceptor.GetLaserMaterial();
            if (material == null) return;
            other.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
        }

        public void OnPlayerExitColorChanger(Collider other) {
            Debug.Assert(other.CompareTag("Player"), "Don't call this method unless the player exits!");
        }
        */
    }
}