/*
  HumanColorChamber.cs
  Author: Samuel Vargas
*/

using UnityEngine;
using UnityEngine.Assertions.Must;
using Util;

namespace Entities.Devices.ColorChanger {
  public class HumanColorChamber : MonoBehaviour {
    private __HumanColorChamber _humanColorChamber;

    private void Start() {
      var empty = new GameObject {name = typeof(__HumanColorChamber).Name};
      empty.transform.SetParent(transform, false);
      _humanColorChamber = empty.AddComponent<__HumanColorChamber>();
    }

    public bool TriggerColorChamberRepaint() {
      return _humanColorChamber.TriggerColorChamberRepaint();
    }

    private class __HumanColorChamber : MonoBehaviour {
      private InputLaserReceptor _inputLaserReceptor;
      private OutputLaserReceptor _outputLaserReceptor;
      private Material _defaultMaterial;

      internal bool TriggerColorChamberRepaint() {
        var inputLaserColor = _inputLaserReceptor.GetColor();
        var outputLaserColor = _outputLaserReceptor.GetColor();

        var repaintWasDone = false;

        if (inputLaserColor != ColorsEnumerationMap.TetrominoColor.NoColor &&
            outputLaserColor != ColorsEnumerationMap.TetrominoColor.NoColor &&
            inputLaserColor == outputLaserColor) {
          GetComponentInParent<MeshRenderer>().material =
            GameObject.Find("Util").GetComponentInChildren<ColorsEnumerationMap>()
              .GetMaterialFromColor(inputLaserColor);
          repaintWasDone = true;
        }
        else {
          GetComponentInParent<MeshRenderer>().material = _defaultMaterial;
        }

        return repaintWasDone;
      }

      public void Start() {
        _defaultMaterial = GetComponentInParent<MeshRenderer>().material;
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(-0.5f, -0.5f, 1.0f);
        boxCollider.size = new Vector3(0.75f, 0.75f, 2.0f);
        _inputLaserReceptor = GetComponentInParent<InputLaserReceptor>();
        _outputLaserReceptor = GetComponentInParent<OutputLaserReceptor>();
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
      }

      private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player has exited ColorChanger");
      }
    }
  }
}