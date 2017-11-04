/*
 * ColorTextureMapping.cs
 * Author: Samuel Vargas
 */

using System;
using UnityEngine;
using Util;

namespace Colors {

  public class ColorTextureMapping : MonoBehaviour {
    public Material NoColor;
    public Material Grey;
    public Material Red;
    public Material Blue;
    public Material Cyan;
    public Material Green;
    public Material Yellow;
    public Material Purple;
    
    public Material GetTexture(GameObjectColor.Colors color) {
      switch (color) {
        case GameObjectColor.Colors.NoColor:
          return NoColor;
        case GameObjectColor.Colors.Grey:
          return Grey;
        case GameObjectColor.Colors.Red:
          return Red;
        case GameObjectColor.Colors.Blue:
          return Blue;
        case GameObjectColor.Colors.Cyan:
          return Cyan;
        case GameObjectColor.Colors.Green:
          return Green;
        case GameObjectColor.Colors.Yellow:
          return Yellow;
        case GameObjectColor.Colors.Purple:
          return Purple;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

}