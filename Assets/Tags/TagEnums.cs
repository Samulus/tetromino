/*
 * TagEnums.cs
 * Author: Samuel Vargas
 *
 * Notes:
 *   - The TagType value should match the first TagId value for that specific
 *     section.
 *
 *    - Each TagType can have TagId(s) that correspond from [n -> n+99]
 *
 *    - A TagType requires at least 1 corresponding TagId with the same value.
 *
 *    - There are no duplicates.
 * 
 *  Objects will deserialize incorrectly if these expectations are not met.
 */

using System;
using System.Collections.Generic;

namespace TagEnums {

  [Serializable]
  public enum TagType : int {
    NoType = -1,
    Agents = 100,
    Geometry = 200,
    Device = 300,
    Sensor = 400,
  }

  [Serializable]
  public enum TagId : int {
    NoId = -1,

    // Agents
    Player = 100,

    // Geometry
    Cube = 200,
    Stairs = 201,

    // Devices
    LaserSender = 300,
    ColorChanger = 301,
    Crate = 302,

    // Sensors
    ObstructionZone = 400,
    ItemPickUpZone = 401,
    LedgeDetectionSensor = 402,
  }

  public static class TagMethods {
    public static List<TagId> GetEnumsInRange(int min, int max) {
      var output = new List<TagId>();
      for (var i = min; i < max; ++i) {
        if (Enum.IsDefined(typeof(TagId), i)) {
          output.Add((TagId) i);
        }
      }
      return output;
    }
    
    public static int[] ListToValueArray(List<TagId> list) {
      var output = new int[list.Count];
      for (var i = 0; i < list.Count; ++i) {
        output[i] = (int) list[i];
      }
      return output;
    }

    public static string[] ListToStringArray(List<TagId> list) {
      var output = new string[list.Count];
      for (var i = 0; i < list.Count; ++i) {
        output[i] = Enum.GetName(typeof(TagId), list[i]);
      }
      return output;
    }
  }

}