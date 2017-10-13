/*
  HumanColorChamber.cs
  Author: Samuel Vargas
  
  Requests that the player switch its color if 
  the player enters the collider.
*/

using Entities.Player.Information;
using UnityEngine;
using Util;

namespace Entities.Devices.ColorChanger {
  public class HumanColorChamber : MonoBehaviour {
    private void Start() {
      var empty = new GameObject {name = typeof(HumanColorChamberInternal).Name};
      empty.transform.SetParent(transform, false);
      empty.AddComponent<HumanColorChamberInternal>();
    }

    private class HumanColorChamberInternal : MonoBehaviour {
      private InputLaserReceptor _inputLaserReceptor;
      private OutputLaserReceptor _outputLaserReceptor;
      private ColorManipulator _playerColorManipulator;

      public void Start() {
        SetupCollider();
        SetupComponents();
      }

      private void SetupCollider() {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(-0.5f, -0.5f, 1.0f);
        boxCollider.size = new Vector3(0.75f, 0.75f, 2.0f);
      }

      private void SetupComponents() {
        _inputLaserReceptor = GetComponentInParent<InputLaserReceptor>();
        _outputLaserReceptor = GetComponentInParent<OutputLaserReceptor>();
        _playerColorManipulator = GameObject.Find("Player").GetComponentInChildren<ColorManipulator>();
      }

      private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        if (_inputLaserReceptor.GetColor() == ColorsEnumerationMap.TetrominoColor.NoColor ||
            _outputLaserReceptor.GetColor() == ColorsEnumerationMap.TetrominoColor.NoColor) {
          _playerColorManipulator.ClearColor();
        }

        else if (_inputLaserReceptor.GetColor() != ColorsEnumerationMap.TetrominoColor.NoColor &&
                 _outputLaserReceptor.GetColor() != ColorsEnumerationMap.TetrominoColor.NoColor) {
          _playerColorManipulator.SetColor(_inputLaserReceptor.GetColor());
        }
      }
    }
  }
}