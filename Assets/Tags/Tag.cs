/*
 * Tag.cs
 * Author: Samuel Vargas
 *
 * Every object in the game should be tagged. Tagging
 * allows objects to query tags on other objects to determine:
 *
 *  - The type of object they are.
 *  - The specific ID of the object they are.
 *
 * Note:
 *   DO NOT change the values of any enums in TagType or TagValue,
 *   they can be reordered but switching values will break game
 *   components.
 */

using TagEnums;
using UnityEngine;

namespace Tags {

  [System.Serializable]
  public class Tag : MonoBehaviour {
    [SerializeField] public TagType Type;
    [SerializeField] public TagId Id;
    
    public TagType GetType() {
      return Type;
    }

    public TagId GetId() {
      return Id;
    }

    public void SetType(TagType type) {
      Type = type;
    }

    public void SetId(TagId id) {
      Id = id;
    }
  }

}