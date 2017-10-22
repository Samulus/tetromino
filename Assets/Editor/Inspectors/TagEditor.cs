/*
 * TagEditor.cs
 * Author: Samuel Vargas
 */

using Tags;
using UnityEditor;

namespace Editor.Inspectors {

  [CustomEditor(typeof(Tag))]
  public class TagEditor : UnityEditor.Editor {
    private SerializedProperty _serialType;
    private SerializedProperty _serialSensorId;
    private SerializedProperty _serialAgentId;
    private SerializedProperty _serialGeometryId;
    private SerializedProperty _serialDeviceId;

    private void SetupValues() {
      _serialType = serializedObject.FindProperty("Type");
      _serialSensorId = serializedObject.FindProperty("SensorId");
      _serialAgentId = serializedObject.FindProperty("AgentId");
      _serialGeometryId = serializedObject.FindProperty("GeometryId");
      _serialDeviceId = serializedObject.FindProperty("DeviceId");
    }

    public override void OnInspectorGUI() {
      SetupValues();
      serializedObject.Update();
      EditorGUILayout.PropertyField(_serialType);
      switch ((TagType) _serialType.intValue) {
        case TagType.Agents:
          EditorGUILayout.PropertyField(_serialAgentId);
          break;
        case TagType.Geometry:
          EditorGUILayout.PropertyField(_serialGeometryId);
          break;
        case TagType.Device:
          EditorGUILayout.PropertyField(_serialDeviceId);
          break;
        case TagType.Sensor:
          EditorGUILayout.PropertyField(_serialSensorId);
          break;
      }
      serializedObject.ApplyModifiedProperties();
    }
  }
}