/*
 * TagEditor.cs
 * Author: Samuel Vargas
 */

using System;
using System.Collections.Generic;
using TagEnums;
using Tags;
using UnityEditor;
using UnityEngine;

namespace Editor.Inspectors {

  [CustomEditor(typeof(Tag))]
  public sealed class TagEditor : UnityEditor.Editor {
    [SerializeField] private SerializedProperty _serialType;
    [SerializeField] private SerializedProperty _serialObjectId;
    private TagType _tagType;
    private TagId _tagId;
    private int _tagIdMenuIndex;

    private Dictionary<TagType, string[]> _objectIdDictionary;
    private Dictionary<TagType, int[]> _objectValueDictionary;

    public TagEditor() {
      _objectIdDictionary = new Dictionary<TagType, string[]>();
      _objectValueDictionary = new Dictionary<TagType, int[]>();
      
    }

    private void Awake() {

      foreach (TagType tagType in Enum.GetValues(typeof(TagType))) {
        var arr = TagMethods.GetEnumsInRange((int) tagType, (int) tagType + 100);
        _objectValueDictionary.Add(tagType, TagMethods.ListToValueArray(arr));
        _objectIdDictionary.Add(tagType, TagMethods.ListToStringArray(arr));
      }
      
      SetupValues();
    }
    
    private void SetupValues() {
      _serialType = serializedObject.FindProperty("Type");
      _serialObjectId = serializedObject.FindProperty("Id");
      _tagType = (TagType) _serialType.intValue;
      _tagId = (TagId) _serialObjectId.intValue;

      // Reset to 0 if the serialized enums is no longer valid due to
      // editing the enums.
      if (!Enum.IsDefined(typeof(TagType), _tagType)) {
        _tagType = 0;
      }

      if (!Enum.IsDefined(typeof(TagId), _tagId)) {
        _tagId = 0;
      }

      // Set tagIdMenuIndex to the correct value
      if (_objectValueDictionary.ContainsKey(_tagType)) {
        var active = _objectValueDictionary[_tagType];
        _tagIdMenuIndex = 0;
        for (var i = 0; i < active.Length; ++i) {
          if (active[i] == (int) _tagId) {
            _tagIdMenuIndex = i;
          }
        }
      }
    }

    public override void OnInspectorGUI() {
      //DrawDefaultInspector();
      serializedObject.Update();
      ShowObjectTypeMenu();
      ShowObjectIdMenu();
      serializedObject.ApplyModifiedProperties();
    }

    private void ShowObjectTypeMenu() {
      EditorGUI.BeginChangeCheck();
      _tagType = (TagType) EditorGUILayout.EnumPopup("Type:", _tagType);
      if (EditorGUI.EndChangeCheck()) {
        _serialType.intValue = (int) _tagType;
        _serialObjectId.intValue = _objectValueDictionary[_tagType][0];
      }
    }

    private void ShowObjectIdMenu() {
      EditorGUI.BeginChangeCheck();
      if (_objectIdDictionary.ContainsKey(_tagType)) {
        _tagIdMenuIndex = EditorGUILayout.Popup("Id: ", _tagIdMenuIndex, _objectIdDictionary[_tagType]);
        if (EditorGUI.EndChangeCheck()) {
          _serialObjectId.intValue = _objectValueDictionary[_tagType][_tagIdMenuIndex];
        }
      }
    }
  }

}