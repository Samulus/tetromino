/*
 * TagEditor.cs
 * Author: Samuel Vargas
 */

using System;
using Tags;
using UnityEditor;
using UnityEngine;

namespace Editor.Inspectors {

  [CustomEditor(typeof(Tag))]
  public sealed class TagEditor : UnityEditor.Editor {
    [SerializeField] private SerializedProperty _serialType;
    [SerializeField] private SerializedProperty _serialSensorId;
    [SerializeField] private SerializedProperty _serialAgentId;
    [SerializeField] private SerializedProperty _serialGeometryId;
    [SerializeField] private SerializedProperty _serialDeviceId;

    [SerializeField] private TagType _selectedType;
    [SerializeField] private SensorId _selectedSensorId;
    [SerializeField] private AgentId _selectedAgentId;
    [SerializeField] private GeometryId _selectedGeometryId;
    [SerializeField] private DeviceId _selectedDeviceId;

    private void OnEnable() {
      _serialType = serializedObject.FindProperty("Type");
      _serialSensorId = serializedObject.FindProperty("SensorId");
      _serialAgentId = serializedObject.FindProperty("AgentId");
      _serialGeometryId = serializedObject.FindProperty("GeometryId");
      _serialDeviceId = serializedObject.FindProperty("DeviceId");
    }

    public override void OnInspectorGUI() {
      serializedObject.Update();

      var objectType = ShowObjectTypeMenu();
      EditorGUI.BeginChangeCheck();

      switch (objectType) {
        case TagType.Agents:
          _selectedAgentId = (AgentId) EditorGUILayout.EnumPopup("Agent", _selectedAgentId);
          if (EditorGUI.EndChangeCheck()) {
            _serialAgentId.intValue = (int) _selectedAgentId;
          }
          break;
        case TagType.Geometry:
          _selectedGeometryId = (GeometryId) EditorGUILayout.EnumPopup("Geometry", _selectedGeometryId);
          if (EditorGUI.EndChangeCheck()) {
            _serialGeometryId.intValue = (int) _selectedGeometryId;
          }
          break;
        case TagType.Device:
          _selectedDeviceId = (DeviceId) EditorGUILayout.EnumPopup("Device", _selectedDeviceId);
          if (EditorGUI.EndChangeCheck()) {
            _serialDeviceId.intValue = (int) _selectedDeviceId;
          }
          break;
        case TagType.Sensor:
          _selectedSensorId = (SensorId) EditorGUILayout.EnumPopup("Sensor", _selectedSensorId);
          if (EditorGUI.EndChangeCheck()) {
            _serialSensorId.intValue = (int) _selectedSensorId;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      serializedObject.ApplyModifiedProperties();
    }

    private TagType ShowObjectTypeMenu() {
      EditorGUI.BeginChangeCheck();
      _selectedType = (TagType) EditorGUILayout.EnumPopup("Type", _selectedType);
      if (EditorGUI.EndChangeCheck()) {
        _serialType.intValue = (int) _selectedType;
      }
      return _selectedType;
    }
  }

}