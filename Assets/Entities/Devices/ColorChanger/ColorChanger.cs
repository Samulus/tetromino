/*
    ColorChangerLogic.cs
    Author: Samuel Vargas
*/

using UnityEngine;

namespace Entities.Devices.ColorChanger {

    /// <summary>
    /// ColorChangerLogic is the overall attachable class that spawns two
    /// separate ColorReceptor objects
    /// </summary>
    public class ColorChanger : MonoBehaviour {
        private Material _defaultMaterial;
        private HumanChamber _humanChamber;
        private ColorReceptor _inputColorReceptor;
        private ColorReceptor _outputColorReceptor;

        private void Start() {
            _defaultMaterial = GetComponent<MeshRenderer>().material;

            var g = gameObject;
            _inputColorReceptor = ColorReceptor.CreateAndAddAsChild(ColorReceptor.IoType.Input, ref g);
            _outputColorReceptor = ColorReceptor.CreateAndAddAsChild(ColorReceptor.IoType.Output, ref g);
            _humanChamber = HumanChamber.CreateAndAddAsChild(ref g);
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
    }

    /// <summary>
    /// ColorReceptor spawns two GameObjects with BoxCollider + RigidBodies attached to them and 
    /// attaches them to the calling parent object at the upper and lower halves of the object.
    /// </summary>
    internal class ColorReceptor : MonoBehaviour {
        public enum IoType {
            Input,
            Output
        }

        private Material _laserMaterial;
        private BoxCollider _collider;
        private Rigidbody _rigidbody;
        private ColorChanger _colorChanger;

        private ColorReceptor() {
        }

        public static ColorReceptor CreateAndAddAsChild(IoType type, ref GameObject parent) {
            var empty = new GameObject();
            empty.transform.SetParent(parent.transform, false);
            empty.name = type.ToString();
            var tmp = empty.AddComponent<ColorReceptor>();
            tmp.Init(type);
            return tmp;
        }

        private void Init(IoType type) {
            _colorChanger = transform.parent.GetComponent<ColorChanger>();

            // Setup Rigidbody
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rigidbody.isKinematic = true;

            // Setup collider
            _collider = gameObject.AddComponent<BoxCollider>();
            _collider.isTrigger = true;
            transform.TransformDirection(Vector3.forward);

            // TODO: Magic constants for I/O panel colliders.
            // These depend on the origin of the ColorChanger object
            // and will break if its moved.
            var height = type == IoType.Input ? 0.5f : 1.5f;
            _collider.center = new Vector3(-0.5f, -1.0f, height);
            _collider.size = new Vector3(1, 0.1f, 1);
        }

        public Material GetLaserMaterial() {
            return _laserMaterial;
        }

        /// <summary>
        /// Switch the color of the ColorChanger box iff a Laser collides
        /// with our collision box.
        /// </summary>
        /// <param name="other">A Laser tagged with the 'Laser' property</param>
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Laser")) return;
            _laserMaterial = other.GetComponent<Laser>().LaserMaterial;
            _colorChanger.OnLaserReceptorEnter(other);
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Laser")) return;
            _laserMaterial = null;
            _colorChanger.OnLaserReceptorExit(other);
            //transform.parent.GetComponent<MeshRenderer>().material = _noLaser;
        }
    }

    /// <summary>
    /// Spawns a BoxCollider (isTrigger=1) inside the ColorChanger, notifies the 
    /// ColorChanger class when the Player (must be tagged with "Player") enters
    /// the chamber.
    /// </summary>
    internal class HumanChamber : MonoBehaviour {
        private const string Name = "HumanChamber";
        private ColorChanger _colorChanger;
        private bool _playerInHumanChamber;

        private HumanChamber() {
        }

        public static HumanChamber CreateAndAddAsChild(ref GameObject g) {
            var humanChamber = new GameObject {name = Name};
            humanChamber.transform.SetParent(g.transform, false);
            return humanChamber.AddComponent<HumanChamber>();
        }

        public void Start() {
            // TODO: Magic constants for HumanChamber collision box.
            // These depend on the origin of the ColorChanger object
            // and will break if its moved.
            _colorChanger = transform.parent.GetComponent<ColorChanger>();
            var humanBoxCollider = gameObject.AddComponent<BoxCollider>();
            humanBoxCollider.isTrigger = true;
            humanBoxCollider.center = new Vector3(-0.5f, -0.5f, 1.0f);
            humanBoxCollider.size = new Vector3(0.75f, 0.75f, 2.0f);
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            _colorChanger.OnPlayerEnteredColorChanger(other);
            _playerInHumanChamber = true;
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player")) return;
            _colorChanger.OnPlayerExitColorChanger(other);
            _playerInHumanChamber = false;
        }
    }

}