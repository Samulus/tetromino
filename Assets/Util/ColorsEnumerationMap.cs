/*
  ColorsEnumerationMap.cs
  Author: Samuel Vargas
*/

using System.Collections.Generic;
using UnityEngine;

namespace Util {
  public class ColorsEnumerationMap : MonoBehaviour {
    public enum TetrominoColor {
      NoColor,
      Grey,
      Red,
      Blue,
      Cyan,
      Green,
      Yellow,
      Purple
    }

    [System.Serializable]
    public class ColorEntry {
      public TetrominoColor color;
      public Material material;
    }
    
    public ColorEntry[] ColorMap;
    private Dictionary<TetrominoColor, Material> _colorTable;
    
    private void Awake() {
      _colorTable = new Dictionary<TetrominoColor, Material>();
      
      foreach (ColorEntry t in ColorMap) {
        var key = t.color;
        var value = t.material;
        _colorTable[key] = value;
      }
    }

    public Material GetMaterialFromColor(TetrominoColor color) {
      Debug.Assert(_colorTable.ContainsKey(color));
      return _colorTable[color];
    }
  }
}