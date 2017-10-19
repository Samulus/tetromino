/*
 * Tag.cs
 * Author: Samuel Vargas
 *
 * Every object in the game should be tagged. Tagging
 * allows objects to query tags on other objects to determine
 * how they should interact.
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tags {
  public enum TagType {
    Device,
    Sensor,
    Geometry,
    Solid,
    AI,
  }

  public enum TagValue {
    Cube,
    Crate,
    ColorChanger,
    Player,
    Stairs,
  }

  public class Tag : MonoBehaviour {
    public List<TagType> _tagTypeList;
    public List<TagValue> _tagValueList;
    
    private HashSet<TagType> _tagTypeSet;
    private HashSet<TagValue> _tagValueSet;

    private void Start() {
      _tagTypeSet = new HashSet<TagType>();
      _tagValueSet = new HashSet<TagValue>();
      
      foreach (var type in _tagTypeList) {
        _tagTypeSet.Add(type);
      }
      foreach (var type in _tagValueList) {
        _tagValueSet.Add(type);
      }
    }
    
    public bool HasTagType(IEnumerable<TagType> types) {
      return types.All(t => _tagTypeSet.Contains(t));
    }

    public bool HasTagType(TagType type) {
      return _tagTypeSet.Contains(type);
    }

    public bool HasTagValue(IEnumerable<TagValue> values) {
      return values.All(v => _tagValueSet.Contains(v));
    }

    public bool HasTagValue(TagValue value) {
      return _tagValueSet.Contains(value);
    }
  }
}