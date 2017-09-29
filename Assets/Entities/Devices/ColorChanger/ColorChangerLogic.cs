/*
    ColorChangerLogic.cs
    Author: Samuel Vargas
*/

using UnityEngine;

namespace Models.Devices.ColorChanger {
  public class ColorChangerLogic : MonoBehaviour {
    private void Start() {
      var g = gameObject;
      ColorChangerIo.CreateAndAddAsChild(ColorChangerIo.IoType.Input, "Input", ref g);
      ColorChangerIo.CreateAndAddAsChild(ColorChangerIo.IoType.Output, "Output", ref g);
    }
  }

  /// <summary>
  /// ColorChangerIO spawns two GameObjects with BoxCollider + RigidBodies attached to them and 
  /// attaches them to the calling parent object at the upper and lower halves of the object.
  /// </summary>
  internal class ColorChangerIo : MonoBehaviour {
    public enum IoType {
      Input,
      Output
    }

    private Material _noLaser;
    private BoxCollider _collider;
    private Rigidbody _rigidbody;
    private IoType _type;

    private ColorChangerIo() {
    }

    public static GameObject CreateAndAddAsChild(IoType type, string name, ref GameObject parent) {
      var empty = new GameObject();
      empty.transform.SetParent(parent.transform, false);
      empty.transform.position = parent.transform.position;
      empty.transform.eulerAngles = parent.transform.eulerAngles;
      empty.name = name;
      var tmp = empty.AddComponent<ColorChangerIo>();
      tmp.Init(type);
      return empty;
    }

    private void Init(IoType type) {
      // Setup Rigidbody
      _rigidbody = gameObject.AddComponent<Rigidbody>();
      _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
      _rigidbody.isKinematic = true;

      // Setup collider
      _type = type;
      _collider = gameObject.AddComponent<BoxCollider>();
      _collider.isTrigger = true;
      var behind = gameObject.transform.TransformDirection(Vector3.back) / 24.0f;
      behind.z += (_type == IoType.Input) ? 0.5f : -0.5f;
      _collider.center = behind;
      _collider.size = new Vector3(gameObject.transform.localScale.x, transform.localScale.y / 24.0f,
        transform.localScale.z);
    }

    private void OnTriggerEnter(Collider other) {
      if (other.name != "Output" && other.name != "Input" && _type == IoType.Input) {
        Debug.Log(other.name);
        _noLaser = transform.parent.GetComponent<MeshRenderer>().material;
        transform.parent.GetComponent<MeshRenderer>().material = other.GetComponent<Laser>().LaserMaterial;
      }
      //var msg = string.Format("We '{0}' collided with {1}", gameObject.name, other.gameObject.name);
      //Debug.Log(msg);
    }

    private void OnTriggerExit(Collider other) {
      transform.parent.GetComponent<MeshRenderer>().material = _noLaser;
    }
  }

  internal class ColorChangerHumanChamber : MonoBehaviour {
  }
}