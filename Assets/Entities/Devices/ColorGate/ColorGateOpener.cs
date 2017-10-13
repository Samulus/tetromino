/*
  ColorGateOpener.cs
  Author: Samuel Vargas
  
  This module opens / closes the gate.
*/

using UnityEngine;

namespace Entities.Devices.ColorGate {
  public class ColorGateOpener : MonoBehaviour {
    
    private void Start() {
      var empty = new GameObject {name = typeof(__ColorGateOpener).Name};
      empty.transform.SetParent(transform, false);
      empty.AddComponent<__ColorGateOpener>();
    }
    
    private class __ColorGateOpener : MonoBehaviour {
    } 
    
  }
}