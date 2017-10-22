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
    private SerializedProperty _serialPickUpId;

    private void SetupValues() {
      _serialType = serializedObject.FindProperty("_type");
      _serialSensorId = serializedObject.FindProperty("_sensorId");
      _serialAgentId = serializedObject.FindProperty("_agentId");
      _serialGeometryId = serializedObject.FindProperty("_geometryId");
      _serialDeviceId = serializedObject.FindProperty("_deviceId");
      _serialPickUpId = serializedObject.FindProperty("_pickUpId");
    }

    public override void OnInspectorGUI() {
      SetupValues();
      serializedObject.Update();
      EditorGUILayout.PropertyField(_serialType);
      switch ((TagType) _serialType.intValue) {
        case TagType.Agent:
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
        case TagType.PickUp:
          EditorGUILayout.PropertyField(_serialPickUpId);
          break;
      }
      serializedObject.ApplyModifiedProperties();
    }
  }
}