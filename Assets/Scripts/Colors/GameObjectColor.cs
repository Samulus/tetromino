/*
 * GameObjectColor.cs
 * Author: Samuel Vargas
 */

using UnityEngine;

namespace Util {

  public class GameObjectColor : MonoBehaviour {
    public enum Colors {
      NoColor = 0,
      Grey = 1,
      Red = 2,
      Blue = 3,
      Cyan = 4,
      Green = 5,
      Yellow = 6,
      Purple = 7,
    }

    public Colors Value;
  }

}